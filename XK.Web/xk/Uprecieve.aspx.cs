using System;
using System.Web;
using XK.Common;

namespace XK.Web.xk {
    public partial class UpRecieve : System.Web.UI.Page {


        protected void Page_Load(object sender, EventArgs e) {

            RecieveFile();
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        private void RecieveFile() {

            HttpFileCollection files = Request.Files;

            if (files.Count > 0) {

                int errCount = 0;

                if (files.Count == 1) {
                    //如果文件数量是1，就当成异步上传
                    //异步批量上传
                    var file = files[0];
                    errCount += Upload(file);

                    if (errCount == 0) {
                        //成功
                        var obj = new {success = "1"};
                        Response.Write(obj.ToJson());
                    }
                    //失败不用返回
                }
                else if (files.Count > 1) {
                    //同步批量上传

                    for (int i = 0; i < files.Count; i++) {
                        var file = files[i];
                        errCount += Upload(file);
                    }

                    if (errCount == 0) {
                        //成功
                        var obj = new {success = "1"};
                        Response.Write(obj.ToJson());
                    }
                    //失败不用返回
                }
            }
        }

        private readonly string fileServer = System.Configuration.ConfigurationManager.AppSettings["FileServer"];

        /// <summary>
        /// do upload
        /// </summary>
        /// <param name="postedFile"></param>
        /// <returns></returns>
        private int Upload(HttpPostedFile postedFile) {

            string retFile = FileHelper.Upload2Server(
                fileServer,
                postedFile.FileNameExt(),
                postedFile.InputStream);

            return (retFile.Length > 0) ? 0 : 1;
        }


    }
}