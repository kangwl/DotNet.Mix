namespace XK.SearchEngine.Model {
    public class Search_Model {

        public Search_Model() {
            PageIndex = 0;
            PageSize = 10;
            //BasePath = "LuceneData";
            //DocFilePath = "Test";
        }
        
        public string Words { get; set; }
        public string[] Fields { get; set; }
        /// <summary>
        /// 从0开始
        /// </summary>
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        ///// <summary>
        ///// 默认LuceneData
        ///// </summary>
        //public string BasePath { get; set; }
        ///// <summary>
        ///// 默认Test
        ///// </summary>
        //public string DocFilePath { get; set; }
    }
}
