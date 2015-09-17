using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XK.Common;
using XK.DataProcess.Core;
using XK.DataProcess.Logic;
using XK.Model;

namespace XK.DataProcess.Source.Act {
    /// <summary>
    /// 操作用户的一些方法
    /// </summary>
    public class UserAct  {
        
        public static string Add(HttpContext context) {
            string uid = context.Request.Form["uid"].ToStringExt();
            if (string.IsNullOrEmpty(uid))
                throw new ArgumentNullException("uid", "不能为空");
            string pwd = context.Request.Form["pwd"].ToStringExt();
            if (string.IsNullOrEmpty(pwd))
                throw new ArgumentNullException("pwd", "不能为空");
            string name = context.Request.Form["name"];
            string sex = context.Request.Form["sex"];
            string birthday = context.Request.Form["birthday"];
            string email = context.Request.Form["email"];

            Model.User_Model user = new User_Model();
            user.Email = email;
            user.Birthday = birthday.StrToDateTime(DateTime.MaxValue);
            user.Name = name;
            user.Password = pwd;
            user.UserID = uid;
            user.Sex = sex.ToInt(0);
             

            Logic.JsonTpl<string> jsonTemplate = new JsonTpl<string>();
            jsonTemplate.info = new ApiInfo(-11, "添加失败");
            jsonTemplate.data = "";


            bool addSucess = Bll.User_Bll.InsertUser(user);
            if (addSucess) {
                jsonTemplate.info = new ApiInfo(1, "添加成功");
            }
            string jsonResult = Common.json.JsonHelper<JsonTpl<string>>.Serialize2Object(jsonTemplate);
            return jsonResult;
        }

        public static string List(HttpContext context) {

            if (string.IsNullOrEmpty(App.UserID))
                throw new AuthenticationException("用户未登录");

            Logic.JsonTpl<List<XK.Model.User_Model>> json = new JsonTpl<List<XK.Model.User_Model>>();
            json.info = new ApiInfo(1, "操作成功");
            json.data = new List<XK.Model.User_Model>() {
                new XK.Model.User_Model() {ID = 1, Name = "k1", Email = "kangwl2009@163.com", Sex = 1},
                new XK.Model.User_Model() {ID = 2, Name = "k2", Email = "kangwl2009@163.com", Sex = 1}
            };
            string extjson = Common.json.JsonHelper<JsonTpl<List<XK.Model.User_Model>>>.Serialize2Object(json);
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
