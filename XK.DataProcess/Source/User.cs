using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using XK.Common;
using XK.DataProcess.Constraint;
using XK.DataProcess.Core;
using XK.DataProcess.DataModel;

namespace XK.DataProcess.Source {
    public class User {
 
        //根据act添加对应的方法
        private static readonly Dictionary<string, Func<HttpContext, string>> dicJsonRes =
            new Dictionary<string, Func<HttpContext, string>> {
                {"add", Act.UserAct.Add},
                {"edit",Act.UserAct.Edit },
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
            //check power
            if (!Constraint.PowerCst.HasPower) {
                return new ApiInfo(SystemCode.Error, "你没有操作权限").ToJson();
            }

            Func<HttpContext, string> actFunc = dicJsonRes.FirstOrDefault(dic => dic.Key == _act).Value;
            return actFunc(context);
        }


    }


}
