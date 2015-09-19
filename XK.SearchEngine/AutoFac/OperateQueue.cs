using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XK.SearchEngine.AutoFac {
    public class OperateQueue {
        private OperateQueue() { }
        static OperateQueue() { }

        public static OperateQueue Instance = new OperateQueue();

        private readonly Queue<OperateModel> _operateQueue = new Queue<OperateModel>();
        public Queue<OperateModel> OperateQueueModels { get { return _operateQueue; } }

    }
}
