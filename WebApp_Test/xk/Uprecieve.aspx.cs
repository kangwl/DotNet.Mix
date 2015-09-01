using System;
using System.Web;
using XK.Common;

namespace WebApp_Test.xk {
    public partial class UpRecieve : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            HttpFileCollection files = Request.Files;
            var path = Server.MapPath("~/Files/");
            if (files.Count > 0) {
                if (!System.IO.Directory.Exists(path)) {
                    System.IO.Directory.CreateDirectory(path);
                }
                for (int i = 0; i < files.Count; i++) {
                    var file = files[i];
                    string saveFile = path + file.FileNameExt();
                    //file.SaveAs(saveFile);
                    //file.SaveAsExt(saveFile);
                    FileHelper.Upload2FileServer("http://localhost:41496/recieve.aspx", file.FileNameExt(),
                        file.ContentLength, file.InputStream);
                }
                var obj = new {success = "1"};
                Response.Write(obj.ToJson());

                
            }
             
            //


        }
    }
}