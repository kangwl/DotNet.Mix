using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Web;
using XK.DataApi.Core;
using XK.DataApi.Logic;

namespace XK.DataApi.Source.Act {
    /// <summary>
    /// 操作用户的一些方法
    /// </summary>
    public class UserAct  {
        
        public static string Add(HttpContext context) {
            var session = context.Session;
            Logic.JsonTpl<string> jsonTemplate = new JsonTpl<string>();
            jsonTemplate.info = new ApiInfo(-11, "添加失败");
            jsonTemplate.data = "";
            bool addSucess = true;
            if (addSucess) {
                jsonTemplate.info = new ApiInfo(1, "添加成功");
            }
            string jsonResult = Common.json.JsonHelper<JsonTpl<string>>.Serialize2Object(jsonTemplate);
            return jsonResult;
        }

        public static string List(HttpContext context) {

            if (string.IsNullOrEmpty(App.UserID))
                throw new AuthenticationException("用户未登录");

            Logic.JsonTpl<List<Logic.Model.User>> json = new JsonTpl<List<Logic.Model.User>>();
            json.info = new ApiInfo(1, "操作成功");
            json.data = new List<Logic.Model.User>() {
                new Logic.Model.User() {ID = 1, Name = "k1", Email = "kangwl2009@163.com", Sex = "男"},
                new Logic.Model.User() {ID = 2, Name = "k2", Email = "kangwl2009@163.com", Sex = "男"}
            };
            string extjson = Common.json.JsonHelper<JsonTpl<List<Logic.Model.User>>>.Serialize2Object(json);
            return extjson;
        }

        public static string Delete(HttpContext request) {
            return "delete";
        }

        public static string Update(HttpContext request) {
            return "update";
        }

        public static string GetOne(HttpContext request) {
            return "getone";
        }
    }
}
