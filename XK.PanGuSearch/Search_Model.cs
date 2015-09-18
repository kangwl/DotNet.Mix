namespace XK.PanGuSearch {
    public class Search_Model {

        public Search_Model() {
            PageIndex = 0;
            PageSize = 10;
        }
        
        public string Words { get; set; }
        public string[] Fields { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
 
 
    }
}
