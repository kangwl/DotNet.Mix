using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Common.web;
using XK.SearchEngine;
using XK.SearchEngine.AutoFac;
using XK.SearchEngine.Model;

namespace XK.Web.xk {
    public partial class Search : System.Web.UI.Page {
        string basepath = @"D:\GITProjects\DotNet.Mix\XK.SearchEngine.Test\bin\Debug\LuceneData";
        protected void Page_Load(object sender, EventArgs e) {
           
 
          //  AddDocIndex();
            XK.SearchEngine.Model.Search_Model searchModel = new Search_Model();
            searchModel.Words = "中秋";
            searchModel.Fields = new string[] { "Title", "Content" };
            searchModel.PageIndex = 0;
            searchModel.PageSize = 100;
            XK.SearchEngine.DocSearch doc = new DocSearch(searchModel,filePath:"Test", baseDataPath: basepath);

            SearchResult_Model<List<News>> searchResult = doc.Search<News>();
            List<News> news = searchResult.Data;
           // txt_KeyWord.Text = searchResult.Total.ToString();
            rpt_SearchResult.BindExt(news);

        }

        private void AddDocIndex() { 
       
            XK.SearchEngine.AutoFac.OperateModel operateModel = new OperateModel();

            //add
            operateModel.OperateEnum = OperateEnum.Add;
            operateModel.DataBasePath = basepath;
            operateModel.FilePath = "Test";
            operateModel.Dic = new Dictionary<string, string>() {
                {"ID", "9"},
                {"Title", "中秋节快乐9"},
                {"Content", "这是一个节日，中秋节哦9"}
            };
            Enter.Instance.Add(operateModel);

            operateModel = new OperateModel();
            operateModel.DataBasePath = basepath;
            operateModel.FilePath = "Test";
            operateModel.OperateEnum = OperateEnum.Add;
            operateModel.Dic = new Dictionary<string, string>() {
                {"ID", "94"},
                {"Title", "中秋节快乐9"},
                {"Content", "这是一个节日，中秋节哦94"}
            };
            Enter.Instance.Add(operateModel);
        }

        class News {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            XK.SearchEngine.Model.Search_Model searchModel = new Search_Model();
            searchModel.Words = txt_KeyWord.Text;
            searchModel.Fields = new string[] { "Title", "Content" };
            searchModel.PageIndex = 0;
            searchModel.PageSize = 10;
            XK.SearchEngine.DocSearch doc = new DocSearch(searchModel, filePath: "Test", baseDataPath: basepath);

            SearchResult_Model<List<News>> searchResult = doc.Search<News>();
            List<News> news = searchResult.Data;

            rpt_SearchResult.BindExt(news);
        }
    }
}