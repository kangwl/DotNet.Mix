using System;
using System.Linq;
using System.Web;
using XK.DataProcess.Core;

namespace XK.DataProcess.Constraint {
    public class MyPowerCheckAttribute : Attribute {

        public MyPowerCheckAttribute(string role) {
            Roles = role;
            AuthorizeCore();
        }
        public string Roles { get; set; }
        private bool hasPower;
        public bool HasPower { get { return hasPower; } } 
        public void AuthorizeCore() {
            string[] roleArr = new string[] {};
            bool isAccess = JudgeAuthorize(App.UserID, roleArr);
            if (roleArr.Length > 0 && isAccess) //先判断是否有设用户权限，如果没有不允许访问
                hasPower = false;

            hasPower = true;
        }

        /// <summary>
        /// 根据用户ID判断用户是否有对应的权限
        /// </summary>
        /// <param name="user_ID"></param>
        /// <param name="strRoles"></param>
        /// <returns></returns>
        private bool JudgeAuthorize(string user_ID, string[] strRoles) {
            string UserAuth = GetRole(user_ID); //从数据库中读取用户的权限
            return strRoles.Contains(UserAuth, //将用户的权限跟权限列表中做比较
                StringComparer.OrdinalIgnoreCase); //忽略大小写
        }



        // 返回用户对应的角色， 在实际中， 可以从SQL数据库中读取用户的角色信息  
        private string GetRole(string user_ID) {
            switch (user_ID) {
                case "aaa":
                    return "User";
                case "bbb":
                    return "Admin";
                case "ccc":
                    return "God";
                default:
                    return "Fool";
            }
        }
    }
    [MyPowerCheck("sd")]
    public class TestAuth {
        
    }

}
