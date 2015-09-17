using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using XK.DataApi.Logic;

namespace XK.DataApi.Source {
    public class News {
        private static Act act;

        public static Act ActMethod
        {
            get { return act ?? (act = new Act()); }
        }

        public static string Init(string _act, HttpContext context) {
            Func<HttpContext, string> actFunc = dicJsonRes.FirstOrDefault(dic => dic.Key == _act).Value;
            return actFunc(context);
        }

        private static readonly Dictionary<string, Func<HttpContext, string>> dicJsonRes =
            new Dictionary<string, Func<HttpContext, string>> {
                {"add", ActMethod.Add},
                {"list", ActMethod.GetList}
            };

        public class Act {
            public string Add(HttpContext context) {
               ApiInfo info=new ApiInfo();
                return "news add";
            }

            public string GetList(HttpContext context) {
                ApiInfo exception = new ApiInfo(2, "news list err");
                string extjson = Common.json.JsonHelper<ApiInfo>.Serialize2Object(exception);
                return extjson;
            }

            public string Delete(HttpContext context) {
                return "news delete";
            }

            public string Update(HttpContext context) {
                return "news update";
            }
        }
    }

}

