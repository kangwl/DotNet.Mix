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
        private const string RootFileName = "Files"; 
        private const string Unknow = "unknow";
        public const string Address = "http://localhost：8082/";

        public static string Save(HttpServerUtility server, Stream stream, string fileName) {

            string fileExtension = Path.GetExtension(fileName);
            string relativePath = $"{RootFileName}/{Unknow}/";
            string savePath = server.MapPath($"~/{relativePath}");

            if (!string.IsNullOrEmpty(fileExtension)) {
                //去掉extension的.
                relativePath = $"{RootFileName}/{fileExtension.Substring(1)}/";
                savePath = server.MapPath($"~/{relativePath}/");
            }

            if (!Directory.Exists(savePath)) {
                Directory.CreateDirectory(savePath);
            }

            string newFileName = Guid.NewGuid().ToString("N") + fileExtension;
            string saveFile = Path.Combine(savePath, newFileName);
            bool success = FileHelper.WriteFile(stream, saveFile);
            string retFile = "";
            if (success) {
                retFile = $"{Address}{relativePath}{newFileName}";
            }

            return retFile;
        }


    }
}