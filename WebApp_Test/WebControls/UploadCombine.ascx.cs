using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Common;

namespace WebApp_Test.WebControls {
    public partial class UploadCombine : System.Web.UI.UserControl {
        protected void Page_Load(object sender, EventArgs e) {

        }

        public bool SupportHtml5 { get { return Request.QueryString["html5"].ToInt() == 1; } }

        private void LoadUpload() {
            string uploadControlUrl = "~/WebControls/FlashUploadify.ascx";
            if (SupportHtml5) {
                uploadControlUrl = "~/WebControls/Html5UploadControl.ascx";
            }
            Control control = Page.LoadControl(uploadControlUrl);
        }
    }
}