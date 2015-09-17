using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace XK.Web.App {
    /// <summary>
    /// 继承与此类，可用于检查权限
    /// </summary>
    public class BasePage : Page {

        public BasePage() {
            CheckLogin = true;
            CheckPower = true;
        }

        public bool CheckLogin { get; set; }

        public bool CheckPower { get; set; }

        protected override void OnInit(EventArgs e) {
            JudgeLogin();
            JudgePower();
        }

        private void JudgeLogin() {
            if (CheckLogin) {
                if (!HasLogin) {
                    Response.Redirect(XK.Common.LoginCookieHelper.LoginUrl, true);
                }
            }
        }

        private bool HasLogin {
            get { return !string.IsNullOrEmpty(XK.Common.LoginCookieHelper.GetCurrentUser()); }
        }

        private void JudgePower() {
            if (CheckPower) {
                //
            }
        }

        /// <summary>
        /// 获取当前页的链接地址包含参数
        /// </summary>
        public string UrlRelative {
            get {
                string relativeUrl = Request.Url.PathAndQuery;
                return relativeUrl;
            }
        }
        /// <summary>
        /// 不含参数
        /// </summary>
        public string UrlNoQuery {
            get {
                string urlNoQuery = Request.Url.AbsolutePath;
                return urlNoQuery;
            }
        }

    }
}