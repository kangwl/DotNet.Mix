using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FreedomDB.Bridge;
using XK.Common;
using XK.DataProcess.Core;
using XK.DataProcess.DataModel;
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
             

            JsonTpl<string> jsonTemplate = new JsonTpl<string>();
            jsonTemplate.msg = "添加失败";
            jsonTemplate.code = -11;
            jsonTemplate.data = "";


            bool addSucess = Bll.User_Bll.InsertUser(user);
            if (addSucess) {
                jsonTemplate.msg = "添加成功";
                jsonTemplate.code = 1;
            }
            string jsonResult = Common.json.JsonHelper<JsonTpl<string>>.Serialize2Object(jsonTemplate);
            return jsonResult;
        }

        public static string Edit(HttpContext context) {
            if (string.IsNullOrEmpty(App.UserID))
                throw new AuthenticationException("用户未登录");
            string id = context.Request.Form["_id"].ToStringExt();
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "缺少参数_id");
         
            string name = context.Request.Form["name"];
            string sex = context.Request.Form["sex"];
            string birthday = context.Request.Form["birthday"];
            string email = context.Request.Form["email"];
            Where @where = new Where(new Where.Item("ID", "=", id));
    
            FreedomDB.Bridge.Update update = new Update();
            update.Dic = new Dictionary<string, dynamic>() {
                {"Email", email},
                {"Birthday", birthday.StrToDateTime(DateTime.MaxValue)},
                {"Name", name},
                {"Sex", sex.ToInt(0)}
            };
            update.WhereCore = where;
            bool success = Bll.User_Bll.UpdateUser(update);
            ApiInfo apiInfo = new ApiInfo(SystemCode.Error, "更新失败");
            if (success) {
                apiInfo = new ApiInfo(SystemCode.Success, "更新成功");
            }
            return apiInfo.ToJson();
        }

        public static string List(HttpContext context) {
     
            if (string.IsNullOrEmpty(App.UserID))
                throw new AuthenticationException("用户未登录");
            int pageIndex = context.Request.QueryString["pageIndex"].ToInt(0);
            int pageSize = context.Request.QueryString["pageSize"].ToInt(10);
            FreedomDB.Bridge.Where where = new Where();
            where.Add(new Where.Item("1", "=", "1"));
            var listUser = Bll.User_Bll.GetListUser(where, "*", "ID DESC", pageIndex, pageSize);
            PageJsonTpl<List<XK.Model.User_Model>> listJsonTpl = new PageJsonTpl<List<User_Model>>();
            listJsonTpl.data = listUser;
            listJsonTpl.msg = "";
            listJsonTpl.code = 1;
            listJsonTpl.total = Bll.User_Bll.GetRecordCount(where);
            listJsonTpl.pageindex = pageIndex;

            string extjson = Common.json.JsonHelper<PageJsonTpl<List<XK.Model.User_Model>>>.Serialize2Object(listJsonTpl);
            return extjson;
        }

        public static string Delete(HttpContext context) {
            return "delete";
        }

        public static string Update(HttpContext context) {
            return "update";
        }

        public static string GetOne(HttpContext context) {
            string id = context.Request.GetReqValExt("id");
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id", "缺少参数id");
            Where where = new Where(new Where.Item("ID", "=", id));
            Model.User_Model userModel = Bll.User_Bll.GetOneUser(where, "*");
            JsonTpl<Model.User_Model> jsonTpl = new JsonTpl<User_Model>() {code = SystemCode.Error, msg = "exception"};
            if (userModel.ID > 0) {
                jsonTpl.data = userModel;
                jsonTpl.code = SystemCode.Success;
                jsonTpl.msg = "success";
            }
            return jsonTpl.ToJson();
        }
    }
}
