using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XK.DataApi.Map {
    public class SourceMap {

        private SourceMap() {
        }

        static SourceMap() {
            dicFunc = new Dictionary<string, Func<string, HttpContext, string>>();
            dicFunc.Add("user", Source.User.Init); 
            dicFunc.Add("power",Source.Power.Init);
            dicFunc.Add("login",Source.Login.Init);
        }


        private static readonly SourceMap _instance = new SourceMap();

        public static SourceMap Instance => _instance;

        private static Dictionary<string, Func<string, HttpContext, string>> dicFunc;

        public Dictionary<string, Func<string, HttpContext, string>> DicFunc => dicFunc;
    }
}
