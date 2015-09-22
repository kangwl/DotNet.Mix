using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace XK.DataProcess.Source {
    public class User {
 
        //根据act添加对应的方法
        private static readonly Dictionary<string, Func<HttpContext, string>> dicJsonRes =
            new Dictionary<string, Func<HttpContext, string>> {
                {"add", Act.UserAct.Add},
                {"list", Act.UserAct.List},
                {"del", Act.UserAct.Delete},
                {"getone", Act.UserAct.GetOne},
                {"update", Act.UserAct.Update}
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
