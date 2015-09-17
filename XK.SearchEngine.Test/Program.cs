using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XK.SearchEngine.Test {
    class Program {
        static void Main(string[] args) {
            // XK.SearchEngine.IndexManage.CreateIndex("中秋节快乐", "这是一个节日，中秋节哦");
            // Console.WriteLine("ok");

            string[] fields = {"Title", "Content" };
            int total;
            List<News> news = XK.SearchEngine.IndexManage.Search<News>("中秋", fields,10,0, out total);
            Console.WriteLine(news[0].Content);
            Console.Read();
        }

        class News {
            public string Title { get; set; }
            public string Content { get; set; } 
        }
    }
}
