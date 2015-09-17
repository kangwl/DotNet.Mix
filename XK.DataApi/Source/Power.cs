using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace XK.DataApi.Source {
    public class Power {
        //根据act添加对应的方法
        public static readonly Dictionary<string, Func<HttpContext, string>> dicJsonRes =
            new Dictionary<string, Func<HttpContext, string>> {
                {"haspower", Act.PowerAct.HasPower}
            };
        /// <summary>
        /// 根据act初始调用对应的方法
        /// </summary>
        /// <param name="_act"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string Init(string _act, HttpContext context) {
            Func<HttpContext, string> actFunc = dicJsonRes.FirstOrDefault(dic => dic.Key == _act).Value;
            return actFunc(context);
        }
    }
}
