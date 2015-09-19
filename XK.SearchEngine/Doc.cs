using System.Collections.Generic;
using System.Linq;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Highlight;
using XK.Common;
using XK.Common.json;
using XK.SearchEngine.Model;
using Version = Lucene.Net.Util.Version;

namespace XK.SearchEngine {
    /* 用例：
            Search_Model searchModel = new Search_Model {
                Fields = new string[] { "Title", "Content" },
                Words = "中秋"
            };
            XK.SearchEngine.Doc doc = new Doc("Test", searchModel);
            //doc.CreateLuceneIndex(new Dictionary<string, dynamic>() { { "Title", "中秋节快乐" }, { "Content", "这是一个节日，中秋节哦" } });
             
            
            var result = doc.Search<News>();
            List<News> news = result.Data;
        */

    public class Doc : IndexManage {

        public Doc(string dataPath, Search_Model searchModel) : base(dataPath) {
            SearchModel = searchModel;
            InitSearchModel();
        }

        private Search_Model SearchModel { get; set; }

        private void InitSearchModel() {
 
            SearchModel.Words = GetKeyWordsSplitBySpace(SearchModel.Words);
        }

        /// <summary>
        /// 返回json
        /// </summary>
        /// <returns></returns>
        public string SearchJson() {
            if (string.IsNullOrEmpty(SearchModel.Words)) {
                return "";
            }
            SearchResult_Model<IEnumerable<Dictionary<string, string>>> searchResult = SearchDic();
            return searchResult.ToJson();
        }
        /// <summary>
        /// 返回list dic
        /// </summary>
        /// <returns></returns>
        public SearchResult_Model<IEnumerable<Dictionary<string, string>>> SearchDic() {
            SearchResult_Model<IEnumerable<Dictionary<string, string>>> searchResult =
                new SearchResult_Model<IEnumerable<Dictionary<string, string>>>();
            if (string.IsNullOrEmpty(SearchModel.Words)) {
                return searchResult;
            }

            SearchedDocResult searchedDoc = SearchInter();

            searchResult.Data = Docs2Dic(searchedDoc.Documents);
            searchResult.Total = searchedDoc.Total;
            return searchResult;
        }
        /// <summary>
        /// 返回强类型
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public SearchResult_Model<List<TModel>> Search<TModel>() {
            SearchResult_Model<List<TModel>> searchResultModel = new SearchResult_Model<List<TModel>>();
            if (string.IsNullOrEmpty(SearchModel.Words)) {
                return searchResultModel;
            }

            SearchedDocResult searchedDoc = SearchInter();

            List<TModel> models = searchedDoc.Documents.Select(Doc2TModel<TModel>).ToList();
            searchResultModel.Data = models;
            searchResultModel.Total = searchedDoc.Total;

            return searchResultModel;
        }

 
        protected Query Query(Analyzer analyzer) { 
            var parser = new MultiFieldQueryParser(Version.LUCENE_30, SearchModel.Fields, analyzer);
            return ParseQuery(SearchModel.Words, parser); 
        }

        /// <summary>
        /// 不对外开放
        /// </summary>
        /// <returns></returns>
        private SearchedDocResult SearchInter() {

            SearchedDocResult searchResult = new SearchedDocResult();
            using (var searcher = new IndexSearcher(Directory, false)) {
                var hitsLimit = 1000;
        
                TopScoreDocCollector collector = TopScoreDocCollector.Create(hitsLimit, true);
                Analyzer analyzer = GetAnalyzer();
                searcher.Search(Query(analyzer), null, collector);
                
                searchResult.Total = collector.TotalHits;

                //TopDocs 指定0到GetTotalHits() 即所有查询结果中的文档 如果TopDocs(20,10)则意味着获取第20-30之间文档内容 达到分页的效果
                int start = SearchModel.PageIndex*SearchModel.PageSize;
                ScoreDoc[] scoreDocs = collector.TopDocs(start, SearchModel.PageSize).ScoreDocs;

                searchResult.Documents = ScoreDocs2Doc(searcher, scoreDocs);

                analyzer.Close(); 
                return searchResult;
            }
        }

        private IEnumerable<Dictionary<string, string>> Docs2Dic(List<Document> documents) {
            return documents.Select(Doc2Dic);
        }

        private List<Document> ScoreDocs2Doc(IndexSearcher searcher, IEnumerable<ScoreDoc> scoreDocs) {
            Lucene.Net.Search.Highlight.SimpleHTMLFormatter htmlFormatter = new SimpleHTMLFormatter("<b>", "</b>");
            Analyzer analyzer = GetAnalyzer();
            Highlighter highlighter = new Highlighter(htmlFormatter, new QueryScorer(Query(analyzer)));
 
            using (analyzer) {
                return scoreDocs.Select(one => ScoreDoc2Document(searcher, one, highlighter, analyzer)).ToList();
            }
        }

        private Document ScoreDoc2Document(IndexSearcher searcher, ScoreDoc scoreDoc, Highlighter highlighter,
            Analyzer analyzer) {
            Document newdoc = new Document();
            Document document = searcher.Doc(scoreDoc.Doc);
            var fields = document.GetFields();
            foreach (IFieldable field in fields) {
                string name = field.Name;
                string value = field.StringValue;
                string text = highlighter.GetBestFragment(analyzer, name, value)??"";
                newdoc.Add(new Field(name, text, Field.Store.YES, Field.Index.ANALYZED));
            }
            return newdoc;
        }

        private TModel Doc2TModel<TModel>(Document document) {
            string json = Doc2Dic(document).ToJson();
            return JsonHelper<TModel>.DeserializeFromStr(json);
        }

        private Dictionary<string, string> Doc2Dic(Document document) {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var fields = document.GetFields();
            foreach (IFieldable field in fields) {
                string name = field.Name;
                string value = field.StringValue;
                dic.Add(name, value);
            }
            return dic;
        }

        /// <summary>
        /// Document 模型
        /// </summary>
        private class SearchedDocResult {
            public SearchedDocResult() {
                Total = 0;
                Documents = new List<Document>();
            }
            /// <summary>
            /// 搜索总数
            /// </summary>
            public int Total { get; set; }

            public List<Document> Documents { get; set; }
        }


    }
}
