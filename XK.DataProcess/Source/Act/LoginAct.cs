using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using XK.Common;
using XK.DataProcess.Core;
using XK.DataProcess.Logic;

namespace XK.DataProcess.Source.Act {
    public class LoginAct {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string CheckLogin(HttpContext context) {
            string method = context.Request.HttpMethod.ToUpper();
            if (method != "POST")
                throw new ArgumentException("HttpMethod 必须为 POST", "HttpMethod");
            string userid = context.Request.Form["uid"].ToStringExt();
            if (string.IsNullOrEmpty(userid))
                throw new ArgumentNullException("uid", "用户账号不能为空");
            string password = context.Request.Form["pwd"].ToStringExt();
            if(string.IsNullOrEmpty(password))
                throw new ArgumentNullException("pwd","密码不能为空");
            if(!userid.Contains("kang"))
                return new ApiInfo(Core.SystemCode.Error,"用户名或密码不对").ToJson();
            App.LoginUser_Model loginUser = new App.LoginUser_Model();
            loginUser.UserID = userid;
            loginUser.UserName = "kangwl";
            Common.LoginCookieHelper.RecordLogined(1, loginUser.ToJson(), DateTime.Now.AddDays(30));

            return new ApiInfo(Core.SystemCode.Success,"登陆成功").ToJson();
        }

    

    }
}
