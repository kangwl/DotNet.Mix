using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace XK.DataApi {
    /// <summary>
    /// api总入口
    /// 由网站ApiModule调用
    /// </summary>
    public class Enter {


        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="source">要操作的对象</param>
        /// <param name="act">要操作对象的方法</param>
        /// <param name="context">HttpRequest</param>
        /// <returns></returns>
        public static string Process(string source, string act, HttpContext context) {
            Func<string, HttpContext, string> sourceCall = Map.SourceMap.Instance.DicFunc.FirstOrDefault(dic => dic.Key == source).Value;
            string json = sourceCall(act, context);
            return json;
        }


    }
}
