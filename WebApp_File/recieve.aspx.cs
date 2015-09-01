using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Common;

namespace WebApp_File {
    public partial class recieve : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(Request.QueryString["name"])) {
                SaveFile();
            }
        }

        private void SaveFile() {
            string filename = Request.QueryString["name"].Trim();
            Stream stream = Request.InputStream;
            int fileLen = Request.ContentLength;
            using (stream) {

                var path = Server.MapPath("~/Files/");

                if (!System.IO.Directory.Exists(path)) {
                    System.IO.Directory.CreateDirectory(path);
                }
                FileHelper.WriteFile(stream, path + filename);
            }
        }

    }
}