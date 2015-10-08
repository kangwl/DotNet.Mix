using System.Collections.Concurrent;

namespace XK.SearchEngine.AutoFac {
    public class OperateQueue {
        private OperateQueue() { }
        static OperateQueue() { }

        public static OperateQueue Instance = new OperateQueue();

        private static readonly ConcurrentQueue<OperateModel> _operateQueue = new ConcurrentQueue<OperateModel>();
        public ConcurrentQueue<OperateModel> OperateQueueModels { get { return _operateQueue; } }

    }
}
