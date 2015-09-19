using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace XK.SearchEngine.AutoFac {

    /*例子：
            //XK.SearchEngine.AutoFac.Enter enter = new Enter("Test");
            //XK.SearchEngine.AutoFac.OperateModel operateModel = new OperateModel();
            //operateModel.OperateEnum = OperateEnum.Delete;
            //operateModel.Dic = new Dictionary<string, string>() { { "ID", "1" } };
            //enter.Enqueue(operateModel);
        
        */

    /// <summary>
    /// 自动化处理
    /// </summary>
    public class Enter {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataPath">用于不同类别存放不同目录下</param>
        public Enter(string dataPath) {
            IndexManage indexManage = new IndexManage(dataPath);
            dicFunc.Add(OperateEnum.Add, indexManage.CreateLuceneIndex);
            dicFunc.Add(OperateEnum.Delete, indexManage.DeleteLuceneIndexRecord);
        }


        public void Add(OperateModel operateModel) {
            Enqueue(new List<OperateModel>() {operateModel});
        }

        public void Enqueue(List<OperateModel> operateModel) {
            operateModel.ForEach(OperateQueue.Instance.OperateQueueModels.Enqueue); 
        }

        /// <summary>
        /// 启动lucene工作线程
        /// </summary>
        public static void InitLuceneWorkThread() {
            new Thread(DoWork).Start();
        }

        private static Dictionary<OperateEnum, Action<Dictionary<string, string>>> dicFunc =
            new Dictionary<OperateEnum, Action<Dictionary<string, string>>>();

        private static void DoWork() {
            while (true) {
                try {
                    if (OperateQueue.Instance.OperateQueueModels.Count > 0) {
                        OperateModel model = OperateQueue.Instance.OperateQueueModels.Dequeue();
                        var func = dicFunc[model.OperateEnum];
                        func(model.Dic);
                    }
                    Thread.Sleep(200);
                }
                catch (Exception ex) {
                    XK.Common.Log4net.Error(ex);
                }
            }
        }

    }

    public class OperateModel { 
        public OperateEnum OperateEnum { get; set; }
        public Dictionary<string, string> Dic { get; set; }
    }


    public enum OperateEnum {
        Add = 1,
        Delete = 2
    }
}
