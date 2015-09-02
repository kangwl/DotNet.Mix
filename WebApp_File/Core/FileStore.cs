using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using XK.Common;

namespace WebApp_File.Core {
    /// <summary>
    /// 保存文件
    /// </summary>
    public class FileStore {
        private const string root = "~/Files/";
        private const string unknow = "unknow";
        public const string address = "http://localhost:41496/";

        public static string Save(HttpServerUtility server, Stream stream, string fileName) {

            string fileExtension = Path.GetExtension(fileName);
            string savePath = server.MapPath(root + unknow);

            if (!string.IsNullOrEmpty(fileExtension)) {
                //去掉extension的.
                savePath = server.MapPath(root + fileExtension.Substring(1));
            }
            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }
            string newFileName = Guid.NewGuid().ToString("N") + fileExtension;
            string saveFile = Path.Combine(savePath, newFileName);
            bool success = FileHelper.WriteFile(stream, saveFile);
            string retFile = "";
            if (success) {
                retFile = address + saveFile;
            }

            return retFile;
        }


    }
}