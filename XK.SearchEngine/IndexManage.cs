using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JiebaNet.Segmenter;
using Lucene.Net.Analysis;  
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using XK.SearchEngine.Util;

namespace XK.SearchEngine {
    public class IndexManage {
        /// <summary>
        /// 基目录
        /// </summary>
        public string BasePath = "LuceneData";
        public IndexManage(string filePath) {
            _luceneDir = $"{BasePath}\\{filePath}";
        }

        private readonly string _luceneDir;

        private  FSDirectory _directoryTemp;

        protected  FSDirectory Directory {
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
 
        /// <summary>
        /// 创建索引文档
        /// </summary>
        /// <param name="dic"></param>
        public  void CreateLuceneIndex(Dictionary<string, string> dic) {
            //var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var analyzer = GetAnalyzer();

            using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED)) {
                var doc = new Document();
                foreach (KeyValuePair<string, string> pair in dic) {
                    // add new index entry
                    //Field.Store.YES:表示是否存储原值。
                    //只有当Field.Store.YES在后面才能用doc.Get("number")取出值来
                    //Field.Index. NOT_ANALYZED:不进行分词保存
                    if (NotAnalyzeFields.Contains(pair.Key.ToLower())) {
                        doc.Add(new Field(pair.Key, pair.Value, Field.Store.YES, Field.Index.NOT_ANALYZED));
                    }
                    else {
                        doc.Add(new Field(pair.Key, pair.Value, Field.Store.YES, Field.Index.ANALYZED));
                    }
                }
                writer.AddDocument(doc);
                writer.Commit();
                analyzer.Close();
            }
        }

        private static List<string> _notAnalyzeFields = new List<string>() {"id"};
        /// <summary>
        /// 不进行分词保存
        /// </summary>
        public static List<string> NotAnalyzeFields
        {
            get { return _notAnalyzeFields; }
        }

        /// <summary>
        /// 批量创建
        /// </summary>
        /// <param name="dicList"></param>
        public  void CreateLuceneIndex(List<Dictionary<string,string>> dicList) {
            foreach (Dictionary<string, string> dictionary in dicList) {
                CreateLuceneIndex(dictionary);
            }
        }

        public void UpdateLuceneIndex(Dictionary<string,string> dicPos,Dictionary<string,string> dic) {
     
            DeleteLuceneIndexRecord(dicPos);
            CreateLuceneIndex(dic);
        }
 

        public void DeleteLuceneIndexRecord(Dictionary<string, string> dic) {
            var analyzer = GetAnalyzer();
            using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED)) {
                foreach (KeyValuePair<string, string> pair in dic) {
                    Term[] terms = dic.Select(one => new Term(one.Key, one.Value)).ToArray();
                    writer.DeleteDocuments(terms);
                }
                writer.Commit();
                analyzer.Close();
            }
        }

        public bool ClearLuceneIndex() {
            try {
                //var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                var analyzer = GetAnalyzer();
                using (var writer = new IndexWriter(Directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED)) {
                    writer.DeleteAll();
                    writer.Commit();
                    analyzer.Close();
                }
            }
            catch (Exception e) {
                return false;
            }

            return true;
        }

        public void OptimizeLuceneIndex() {
            //var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            var analyzer = GetAnalyzer();
            using (var writer = new IndexWriter(Directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED)) {
                analyzer.Close();
                writer.Commit();
                writer.Optimize();
            }
        }


        protected static Analyzer GetAnalyzer() {
 
            return new JiebaAnalyzer();
        }

        protected static Query ParseQuery(string searchQuery, QueryParser parser) {
            Query query;
            try {
                query = parser.Parse(searchQuery.Trim());
            }
            catch (ParseException pe) {
                query = parser.Parse(QueryParser.Escape(searchQuery.Trim() + "*"));
            }

            return query;
        }

        protected string GetKeyWordsSplitBySpace(string keywords) {
            StringBuilder result = new StringBuilder();
            var tokenizer = new JiebaTokenizer(new JiebaSegmenter(), keywords);
            var words = tokenizer.Tokenize(keywords);
            foreach (var word in words) {
                if (string.IsNullOrWhiteSpace(word.Word)) {
                    continue;
                }
                result.AppendFormat("{0} ", word.Word);
            }

            string kwords = result.ToString().Trim();
            var terms = kwords.Trim().Replace("-", " ").Split(' ')
                .Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim() + "*");
            return string.Join(" ", terms);
        }




    }
}
