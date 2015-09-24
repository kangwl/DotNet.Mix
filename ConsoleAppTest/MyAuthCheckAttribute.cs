using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace ConsoleAppTest {
    public class MyAuthCheckAttribute:Attribute {
        public bool AuthorizeCore(HttpContext httpContext) {
            if (!httpContext.User.Identity.IsAuthenticated) //判断用户是否通过验证
                return false;
            string[] roleArr = new string[] {};
            bool isAccess = JudgeAuthorize(httpContext.User.Identity.Name, roleArr);
            if (roleArr.Length > 0 && isAccess) //先判断是否有设用户权限，如果没有不允许访问
                return false;

            return true;
        }

                /// <summary>
         /// 根据用户ID判断用户是否有对应的权限
         /// </summary>
         /// <param name="UserID"></param>
         /// <param name="StrRoles"></param>
         /// <returns></returns>
         private bool JudgeAuthorize(string UserID, string[] StrRoles)
         {
             string UserAuth = GetRole(UserID);  //从数据库中读取用户的权限
             return StrRoles.Contains(UserAuth,    //将用户的权限跟权限列表中做比较
                              StringComparer.OrdinalIgnoreCase);  //忽略大小写
         }
 
    
 
         // 返回用户对应的角色， 在实际中， 可以从SQL数据库中读取用户的角色信息  
         private string GetRole(string name)
         {
             switch (name)
             {
                 case "aaa": return "User";
                 case "bbb": return "Admin";
                 case "ccc": return "God";
                 default: return "Fool";
             }
         }  
    }

}
