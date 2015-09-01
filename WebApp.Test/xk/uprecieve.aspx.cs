using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Common;

namespace WebApp.Test.xk {
    public partial class uprecieve : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            HttpFileCollection files = Request.Files;
            var path = Server.MapPath("~/Files/");
            if (files.Count > 0) {
                for (int i = 0; i < files.Count; i++) {
                    var file = files[i];
                    string saveFile = path + file.FileNameExt();
                    //file.SaveAs(saveFile);
                    file.SaveAsExt(saveFile);
                }
                var obj = new {success = "1"};
                Response.Write(obj.ToJson());
            }
             
            //


        }
    }
}