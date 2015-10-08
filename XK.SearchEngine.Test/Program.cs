using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XK.SearchEngine.AutoFac;
using XK.SearchEngine.Model;

namespace XK.SearchEngine.Test {
    class Program {
        static void Main(string[] args) {

            //XK.SearchEngine.AutoFac.Enter.InitLuceneWorkThread();
            // XK.SearchEngine.IndexManage indexManage=new IndexManage("Test");

            //indexManage.CreateLuceneIndex(new Dictionary<string, string>() { {"ID","1"},{ "Title", "中秋节快乐" }, { "Content", "这是一个节日，中秋节哦" } });
            //indexManage.CreateLuceneIndex(new Dictionary<string, string>() { { "ID", "2" }, { "Title", "中秋节快乐" }, { "Content", "这是一个节日，中秋节哦" } });

            //indexManage.UpdateLuceneIndex(new Dictionary<string, string>() { {"ID","2"} },new Dictionary<string, string>() { { "ID", "2" }, { "Title", "中秋节快乐111" }, { "Content", "这是一个节日，中秋节哦" } });
            //XK.SearchEngine.test.Run();

            //XK.SearchEngine.AutoFac.Enter enter = new Enter("Test");
            //XK.SearchEngine.AutoFac.OperateModel operateModel = new OperateModel();

            ////add
            //operateModel.OperateEnum = OperateEnum.Add;
            //operateModel.Dic = new Dictionary<string, string>() {
            //    {"ID", "1"},
            //    {"Title", "中秋节快乐"},
            //    {"Content", "这是一个节日，中秋节哦"}
            //};
            //enter.Add(operateModel);

            //operateModel = new OperateModel();
            //operateModel.OperateEnum = OperateEnum.Add;
            //operateModel.Dic = new Dictionary<string, string>() {
            //    {"ID", "2"},
            //    {"Title", "中秋节快乐2"},
            //    {"Content", "这是一个节日，中秋节哦2"}
            //};
            //enter.Add(operateModel);
            //delete
            //operateModel.OperateEnum = OperateEnum.Delete;
            //operateModel.Dic = new Dictionary<string, string>() { { "ID", "1" } };
            //enter.Add(operateModel);

            //DocIndex docIndex = new DocIndex("Test");
            //docIndex.ClearLuceneIndex();
            //Dictionary<string, string> dic = new Dictionary<string, string>();
            //dic.Add("ID", "1");
            //dic.Add("Title", "这是中秋节1");
            //dic.Add("Content", "中秋节快乐1，这是一个阖家团圆的日子1");
            //docIndex.AddLuceneIndex(dic);
            //dic.Clear();
            //dic.Add("ID", "3");
            //dic.Add("Title", "这是中秋节3");
            //dic.Add("Content", "中秋节快乐3，这是一个阖家团圆的日子3");
            //docIndex.AddLuceneIndex(dic);

            while (true) {
               
                Console.WriteLine("请输入关键词：");
                string keyword = Console.ReadLine();

                Search_Model searchModel = new Search_Model {
                    Fields = new string[] { "Title", "Content" },
                    Words = keyword
                };
                XK.SearchEngine.DocSearch doc = new DocSearch(searchModel,"Test");

               // bool success = doc.SetDocBoost("ID", "1", 5);
              //  Console.WriteLine(success);

                var result = doc.Search<News>();
                List<News> news = result.Data;

                if (news.Count < 1) {
                    Console.WriteLine("null");
                }
                else {
                    Console.WriteLine(result.Total + "---" + news[0].Title + Environment.NewLine + news[0].Content);
                }
            }
           
        }

        class News {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Content { get; set; } 
        }
    }
}
