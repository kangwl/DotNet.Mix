﻿namespace XK.PanGuSearch {
    public class SearchResult_Model<TModel> where TModel :class {
 
        public TModel Data { get; set; }
        public int Total { get; set; }
    }
}