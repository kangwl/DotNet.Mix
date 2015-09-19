using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XK.SearchEngine.AutoFac;
using XK.SearchEngine.Model;

namespace XK.SearchEngine.Test {
    class Program {
        private static void Main(string[] args) {
           // XK.SearchEngine.AutoFac.Enter.InitLuceneWorkThread();
           // XK.SearchEngine.IndexManage indexManage=new IndexManage("Test");
            //indexManage.ClearLuceneIndex();
            //indexManage.CreateLuceneIndex(new Dictionary<string, string>() { {"ID","1"},{ "Title", "中秋节快乐" }, { "Content", "这是一个节日，中秋节哦" } });
            //indexManage.CreateLuceneIndex(new Dictionary<string, string>() { { "ID", "2" }, { "Title", "中秋节快乐" }, { "Content", "这是一个节日，中秋节哦" } });

            //indexManage.UpdateLuceneIndex(new Dictionary<string, string>() { {"ID","2"} },new Dictionary<string, string>() { { "ID", "2" }, { "Title", "中秋节快乐111" }, { "Content", "这是一个节日，中秋节哦" } });
            //XK.SearchEngine.test.Run();

            //XK.SearchEngine.AutoFac.Enter enter = new Enter("Test");
            //XK.SearchEngine.AutoFac.OperateModel operateModel = new OperateModel();
            //operateModel.OperateEnum = OperateEnum.Delete;
            //operateModel.Dic = new Dictionary<string, string>() { { "ID", "2" } };
            //enter.Add(operateModel);
            while (true) {

                Console.WriteLine("请输入关键词：");
                string keyword = Console.ReadLine();

                Search_Model searchModel = new Search_Model {
                    Fields = new string[] { "Title", "Content" },
                    Words = keyword
                };
                XK.SearchEngine.Doc doc = new Doc("Test", searchModel);
                //doc.CreateLuceneIndex(new Dictionary<string, dynamic>() { { "Title", "中秋节快乐" }, { "Content", "这是一个节日，中秋节哦" } });


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
            public string Title { get; set; }
            public string Content { get; set; } 
        }
    }
}
