#region

using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

#endregion

namespace XK.Common {
    public class FileHelper {
        /// <summary>
        ///     写日志文件
        /// </summary>
        /// <param name="sPath">    年月  例  2011-04</param>
        /// <param name="sFileName">月日  例  04-22</param>
        /// <param name="content">时间+  内容</param>
        /// <returns></returns>
        public static bool WriteLog(string sPath, string sFileName, string content) {
            try {
                StreamWriter sr;
                if (!Directory.Exists(sPath)) {
                    Directory.CreateDirectory(sPath);
                }
                var v_filename = sPath + "\\" + sFileName;


                if (!File.Exists(v_filename)) //如果文件存在,则创建File.AppendText对象
                {
                    sr = File.CreateText(v_filename);
                    sr.Close();
                }
                using (
                    var fs = new FileStream(v_filename, FileMode.Append, FileAccess.Write,
                        FileShare.Write)) {
                    using (sr = new StreamWriter(fs)) {
                        sr.WriteLine(DateTime.Now.ToString("hh:mm:ss") + "     " + content);
                        sr.Close();
                    }
                    fs.Close();
                }
                return true;
            }
            catch {
                return false;
            }
        }


        /// <summary>
        ///     读取文本文件内容,每行存入arrayList 并返回arrayList对象
        /// </summary>
        /// <param name="sFileName"></param>
        /// <returns>arrayList</returns>
        public static ArrayList ReadFileRow(string sFileName) {
            ArrayList alTxt = null;
            try {
                using (var sr = new StreamReader(sFileName)) {
                    alTxt = new ArrayList();

                    while (!sr.EndOfStream) {
                        var sLine = sr.ReadLine();
                        if (sLine != "") {
                            if (sLine != null) alTxt.Add(sLine.Trim());
                        }
                    }
                    sr.Close();
                }
            }
            catch (Exception) {
            }
            return alTxt;
        }


        /// <summary>
        ///     备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <param name="overwrite">当目标文件存在时是否覆盖</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite) {
            if (!File.Exists(sourceFileName))
                throw new FileNotFoundException(sourceFileName + "文件不存在！");

            if (!overwrite && File.Exists(destFileName))
                return false;

            try {
                File.Copy(sourceFileName, destFileName, true);
                return true;
            }
            catch (Exception e) {
                throw e;
            }
        }


        /// <summary>
        ///     备份文件,当目标文件存在时覆盖
        /// </summary>
        /// <param name="sourceFileName">源文件名</param>
        /// <param name="destFileName">目标文件名</param>
        /// <returns>操作是否成功</returns>
        public static bool BackupFile(string sourceFileName, string destFileName) {
            return BackupFile(sourceFileName, destFileName, true);
        }


        /// <summary>
        ///     恢复文件
        /// </summary>
        /// <param name="backupFileName">备份文件名</param>
        /// <param name="targetFileName">要恢复的文件名</param>
        /// <param name="backupTargetFileName">要恢复文件再次备份的名称,如果为null,则不再备份恢复文件</param>
        /// <returns>操作是否成功</returns>
        public static bool RestoreFile(string backupFileName, string targetFileName, string backupTargetFileName) {
            try {
                if (!File.Exists(backupFileName))
                    throw new FileNotFoundException(backupFileName + "文件不存在！");

                if (backupTargetFileName != null) {
                    if (!File.Exists(targetFileName))
                        throw new FileNotFoundException(targetFileName + "文件不存在！无法备份此文件！");
                    File.Copy(targetFileName, backupTargetFileName, true);
                }
                File.Delete(targetFileName);
                File.Copy(backupFileName, targetFileName);
            }
            catch (Exception e) {
                throw e;
            }
            return true;
        }

        public static bool RestoreFile(string backupFileName, string targetFileName) {
            return RestoreFile(backupFileName, targetFileName, null);
        }

        /// <summary>
        ///     获取文件MD5值
        /// </summary>
        /// <param name="pathName">文件路径</param>
        /// <returns></returns>
        public static string GetMd5Hash(string pathName) {
            string strResult;
            var oMD5Hasher = new MD5CryptoServiceProvider();
            try {
                var oFileStream = new FileStream(pathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var arrbytHashValue = oMD5Hasher.ComputeHash(oFileStream);
                oFileStream.Close();
                //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
                var strHashData = BitConverter.ToString(arrbytHashValue);
                //替换-
                strHashData = strHashData.Replace("-", "");
                strResult = strHashData;
            }
            catch (Exception ex) {
                strResult = "";
            }
            return strResult;
        }

        /// <summary>
        ///     根据流写入文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath">EXP:[d:\aaa.jpg]</param>
        /// <param name="bufferSize"></param>
        public static void WriteFile(Stream stream, string filePath, int bufferSize = 2048 * 5) {
            using (stream) { 
                var bytes = new byte[bufferSize];
                var readCount = 0;

                readCount = stream.Read(bytes, 0, bufferSize);

                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) {
                    while (readCount > 0) {
                        fileStream.Write(bytes, 0, readCount);
                        fileStream.Flush();
                        readCount = stream.Read(bytes, 0, bufferSize);
                    }
                }
            }
        }

        public static long GetFileLeng(string filePath) {
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read)) {
                return fileStream.Length;
            }
        }


        public static void CopyFile(string filePath, string destFilePath) {
            var bufferSize = 102400; //100k
            var buffer = new byte[bufferSize];
            using (var fileStream = new FileStream(filePath, FileMode.Open)) {
                var fileLen = fileStream.Length;
                using (var writeStream = new FileStream(destFilePath, FileMode.Create)) {
                    while (fileLen > 0) {
                        var readBytes = fileStream.Read(buffer, 0, bufferSize); //从当前流读取指定字节
                        writeStream.Write(buffer, 0, readBytes);
                        writeStream.Flush();
                        fileLen = fileLen - readBytes; //剩余的字节数
                    }
                }
            }
        }

        public static void CopyFilePos(string filePath, string destFilePath) {
            const int bufferSize = 102400; //100k
            var buffer = new byte[bufferSize];
            using (var fileStream = new FileStream(filePath, FileMode.Open)) {
                var fileLen = fileStream.Length;
                var readTotal = 0;
                using (var writeStream = new FileStream(destFilePath, FileMode.Create)) {
                    while (readTotal < fileLen) {
                        var readCount = fileStream.Read(buffer, 0, bufferSize);

                        writeStream.Write(buffer, 0, readCount);
                        writeStream.Flush();
                        readTotal += readCount;
                    }
                }
            }
        }

        /// <summary>
        ///     限速下载 100k/s
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destFilePath"></param>
        public static void DownControl(string filePath, string destFilePath) {
            const int bufferSize = 102400; //100k
            var buffer = new byte[bufferSize];
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileLen = fileStream.Length;
            var readTotal = 0;
            var writeStream = new FileStream(destFilePath, FileMode.Create, FileAccess.Write);

            var timer = new Timer(call => {
                if (readTotal < fileLen) {
                    var readCount = fileStream.Read(buffer, 0, bufferSize);

                    writeStream.Write(buffer, 0, readCount);
                    writeStream.Flush();
                    readTotal += readCount;
                }
                else {
                    fileStream.Close();
                    writeStream.Close();
                }
            }, null, 0, 1000);
        }

        /// <summary>
        ///     限速下载 较快 最高下载速度500k/s
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destFilePath"></param>
        public static void DownControlVip(string filePath, string destFilePath) {
            const int bufferSize = 102400; //100k
            var buffer = new byte[bufferSize];
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileLen = fileStream.Length;
            var readTotal = 0;
            var writeStream = new FileStream(destFilePath, FileMode.Create, FileAccess.Write);

            var timer = new Timer(call => {
                if (readTotal < fileLen) {
                    var readCount = fileStream.Read(buffer, 0, bufferSize);

                    writeStream.Write(buffer, 0, readCount);
                    writeStream.Flush();
                    readTotal += readCount;
                }
                else {
                    fileStream.Close();
                    writeStream.Close();
                }
            }, null, 0, 200);
        }
    }
}