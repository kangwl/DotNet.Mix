using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using XK.Common.json;

namespace XK.DataProcess.Core {
    public class App {

        public static int User_ID
        {
            get
            {
                LoginUser_Model user = GetLoginUserFromCookie();
                if (user == null) return 0;
                return user.ID;
            }
        }

        public static string UserID
        {
            get
            {
                LoginUser_Model user = GetLoginUserFromCookie();
                if (user == null) return "";
                return user.UserID;
            }
        }

        public static int RoleID
        {
            get
            {
                LoginUser_Model user = GetLoginUserFromCookie();
                if (user == null) return 0;
                return user.RoleID;
            }
        }


        public static LoginUser_Model GetLoginUserFromCookie() {
            if (HttpContext.Current == null) {
                return null;
            }
            var currentUser = HttpContext.Current.User;
            if (currentUser == null) {
                return null;
            }
            if (!currentUser.Identity.IsAuthenticated) return null;
            string strUser = ((FormsIdentity) currentUser.Identity).Ticket.UserData;
            return JsonHelper<LoginUser_Model>.DeserializeFromStr(strUser);
        }


        public class LoginUser_Model {
            /// <summary>
            /// 用户 ID 唯一
            /// </summary>
            public int ID { get; set; }
            /// <summary>
            /// 用户账号（根据系统设计，可能唯一）
            /// </summary>
            public string UserID { get; set; }
             /// <summary>
             /// 账号密码
             /// </summary>
            public string Password { get; set; }
            /// <summary>
            /// 用户称呼
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// 角色ID
            /// </summary>
            public int RoleID { get; set; }
        }
    }
}
