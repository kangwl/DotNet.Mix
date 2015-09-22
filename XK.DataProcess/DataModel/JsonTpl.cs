namespace XK.DataProcess.DataModel {
    /// <summary>
    /// 输出的json模板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonTpl<T> : ApiInfo {
        public T data { get; set; }
    }


}
