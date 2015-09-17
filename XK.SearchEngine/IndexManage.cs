using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using JiebaNet.Segmenter;
using Lucene.Net.Analysis; 
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using XK.Common;
using XK.Common.json;
using XK.SearchEngine.Util;
using Version = Lucene.Net.Util.Version;

namespace XK.SearchEngine {
    public class IndexManage {

        public static string _luceneDir = @"D:\GITProjects\_DataSearch\_Index";

        private static FSDirectory _directoryTemp;

        private static FSDirectory _directory {
            get {
                if (_directoryTemp == null) {
                    _directoryTemp = FSDirectory.Open(new DirectoryInfo(_luceneDir));
                }
                if (IndexWriter.IsLocked(_directoryTemp)) {
                    IndexWriter.Unlock(_directoryTemp);
                }

                var lockFilePath = Path.Combine(_luceneDir, "write.lock");
                if (File.Exists(lockFilePath)) {
                    File.Delete(lockFilePath);
                }

                return _directoryTemp;
            }
        }

        public static void CreateIndex(string title, string content) {
            //var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var analyzer = GetAnalyzer();

            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED)) {

                // add new index entry
                var doc = new Document();
                doc.Add(new Field("Title", title, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("Content", content, Field.Store.YES, Field.Index.ANALYZED));

                writer.AddDocument(doc);

                analyzer.Close();
            }
        }

        /// <summary>
        /// 创建索引文档
        /// </summary>
        /// <param name="indexPrefix">EXP:new_ ，book_ 前缀</param>
        /// <param name="dic"></param>
        public static void CreateIndex(string indexPrefix, Dictionary<string, dynamic> dic) {
            //var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var analyzer = GetAnalyzer();

            using (var writer = new IndexWriter(_directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED)) {
                var doc = new Document();
                foreach (KeyValuePair<string, dynamic> pair in dic) {
                    // add new index entry
                    doc.Add(new Field(indexPrefix + pair.Key, pair.Value, Field.Store.YES, Field.Index.ANALYZED));
                }
                writer.AddDocument(doc);
                analyzer.Close();
            }
        }

        /// <summary>
        /// 批量创建
        /// </summary>
        /// <param name="indexPrefix"></param>
        /// <param name="dicList"></param>
        public static void CreateIndex(string indexPrefix,List<Dictionary<string,dynamic>> dicList) {
            foreach (Dictionary<string, dynamic> dictionary in dicList) {
                CreateIndex(indexPrefix, dictionary);
            }
        }

        public static IEnumerable<Dictionary<string, string>> Search(string searchWord, string[] fieldName, int pageSize,
            int pageIndex,
            out int total) {
            if (string.IsNullOrEmpty(searchWord)) {
                total = 0;
                return new List<Dictionary<string, string>>();
            }

            var kwords = searchWord;
            kwords = GetKeyWordsSplitBySpace(kwords, new JiebaTokenizer(new JiebaSegmenter(), kwords));

            var terms = kwords.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim());
            searchWord = string.Join(" ", terms);

            List<Document> documents = SearchQuery(searchWord, fieldName, pageSize, pageIndex, out total);

            var dicList = Docs2Dic(documents);
            return dicList;
        }

        public static List<TModel> Search<TModel>(string searchWord, string[] fieldName, int pageSize,
            int pageIndex,
            out int total) {
            if (string.IsNullOrEmpty(searchWord)) {
                total = 0;
                return new List<TModel>();
            }

            var kwords = searchWord;
            kwords = GetKeyWordsSplitBySpace(kwords, new JiebaTokenizer(new JiebaSegmenter(), kwords));

            var terms = kwords.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim());
            searchWord = string.Join(" ", terms);

            IEnumerable<Document> documents = SearchQuery(searchWord, fieldName, pageSize, pageIndex, out total);

            List<TModel> models = documents.Select(Doc2TModel<TModel>).ToList();
            return models;
        }

        private static Dictionary<string, string> Doc2Dic(Document document) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var fields = document.GetFields();
            foreach (IFieldable field in fields) {
                string name = field.Name;
                string value = field.StringValue;
                dic.Add(name, value);
            }
            return dic;
        }

        private static TModel Doc2TModel<TModel>(Document document) {
            string json = Doc2Dic(document).ToJson();
            return JsonHelper<TModel>.DeserializeFromStr(json);
        }

        private static Document ScoreDoc2Document(IndexSearcher searcher,ScoreDoc scoreDoc) {
            return searcher.Doc(scoreDoc.Doc);
        }


        private static List<Document> ScoreDocs2Doc(IndexSearcher searcher, IEnumerable<ScoreDoc> scoreDocs) {
            return scoreDocs.Select(one => ScoreDoc2Document(searcher, one)).ToList();
        }

        private static IEnumerable<Dictionary<string,string>> Docs2Dic(List<Document> documents) {
            return documents.Select(Doc2Dic);
        }

        private static List<Document> SearchQuery(string searchQuery, string[] searchField,
            int pageSize, int pageIndex, out int total) {
            List<Document> documents = new List<Document>();
            if (string.IsNullOrEmpty(searchQuery.Replace("*", "").Replace("?", ""))) {
                total = 0;
                return documents;
            }
            using (var searcher = new IndexSearcher(_directory, false)) {
                var hitsLimit = 1000;
                //var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                var analyzer = GetAnalyzer();
                var parser = new MultiFieldQueryParser(Version.LUCENE_30, searchField, analyzer);
                var query = ParseQuery(searchQuery, parser);
                TopScoreDocCollector collector = TopScoreDocCollector.Create(hitsLimit, true);
                searcher.Search(query, null, collector);
                total = collector.TotalHits;
                //TopDocs 指定0到GetTotalHits() 即所有查询结果中的文档 如果TopDocs(20,10)则意味着获取第20-30之间文档内容 达到分页的效果
                int start = pageIndex*pageSize;
                ScoreDoc[] scoreDocs = collector.TopDocs(start, pageSize).ScoreDocs;

                documents = ScoreDocs2Doc(searcher, scoreDocs);

                analyzer.Close();

                return documents;
            }
        }


        private static string GetKeyWordsSplitBySpace(string keywords, JiebaTokenizer tokenizer) {
            StringBuilder result = new StringBuilder();

            var words = tokenizer.Tokenize(keywords);

            foreach (var word in words) {
                if (string.IsNullOrWhiteSpace(word.Word)) {
                    continue;
                }

                result.AppendFormat("{0} ", word.Word);
            }

            return result.ToString().Trim();
        }


        private static Analyzer GetAnalyzer() {
            return new JiebaAnalyzer();
        }

        private static Query ParseQuery(string searchQuery, QueryParser parser) {
            Query query;
            try {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException pe) {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim() + "*"));
            }

            return query;
        }
     

    }
}
