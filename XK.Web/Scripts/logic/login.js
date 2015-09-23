
dmo.getLoginOperatObj = function () {
    /// <summary>获取登录的操作对象，并返回此操作对象</summary>
    var loginOperateObj = {};
    loginOperateObj.login = function (dataObj, fnSuccess, fnError, fnComplete) {
        /// <summary>登录函数</summary>
        /// <param name="dataObj" type="Object">ajax的data参数</param>
        /// <param name="fnSuccess" type="Function">处理成功的 success 方法</param>
        /// <param name="fnError" type="Function">处理失败的 error 方法</param>
        /// <param name="fnComplete" type="Function">处理完成的 complete 方法 </param>

        dmo.reqServer("/api/login/check", "post", dataObj, fnSuccess, fnError, fnComplete);
    }
    return loginOperateObj;
}