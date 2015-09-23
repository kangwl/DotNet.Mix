//总操作对象
var dmo = {}; 
dmo.logic = {};//储存处理操作的js,方便调用 
dmo.logic.loginJS = "/scripts/logic/login.js";
dmo.logic.userJS = "/scripts/logic/user.js";

dmo.setPager = function(pageContentId, pageIndex, pageSize, total, fnPageClick) {
    /// <summary>设置分页</summary>
    /// <param name="pageContentId" type="String">存放分页的地方，比如是div的ID</param>
    /// <param name="pageIndex" type="Int">当前页码</param>
    /// <param name="pageSize" type="Int">一页显示的数量</param>
    /// <param name="total" type="Int">全部记录数</param>
    /// <param name="fnPageClick" type="Function">点击分页触发的函数，带要跳转的页码的参数</param>

    var max = Math.ceil(parseInt(total) / parseInt(pageSize));
    $.mypage(pageContentId, pageIndex, max, fnPageClick);
}

dmo.require = function (scriptUrl, fn) {
    ///<summary>动态加载需要的JS文件</summary>
    ///<param name="scriptUrl" type="String">js所在路径</param>
    ///<param name="fn" type="Function">带bool值的回调函数</param>
    ///<field name="sd"></field>
    myJQUtil.cacheScript(scriptUrl).done(function(scriptText, state) {
        var success = (state === "success");
        fn(success);
    });
}
dmo.reqServer = function(reqUrl, reqType, reqData,fnSuccess,fnBeforeSend, fnError, fnComplete) {
    /// <summary>统一请求函数 json</summary>
    /// <param name="reqUrl" type="String">请求地址</param>
    /// <param name="reqType" type="String">请求类型 get/post</param>
    /// <param name="reqData" type="String">请求参数</param>
    /// <param name="fnSuccess" type="Function">成功后回调函数</param>
    /// <param name="fnError" type="Function">失败后回调函数</param>
    /// <param name="fnComplete" type="Function">完成后回调函数</param>
    $.ajax({
        type: reqType,
        url: reqUrl,
        data: reqData,
        dataType: "json",
        beforeSend: function() {
            if (typeof fnBeforeSend === "function") {
                fnBeforeSend();
            }
        },
        success: function(res) {
            if (typeof fnSuccess === "function") {
                fnSuccess(res);
            }
        },
        error: function(e) {
            if (typeof fnError === "function") {
                fnError(e);
            }
            console.error(e);
        },
        complete: function() {
            if (typeof fnComplete === "function") {
                fnComplete();
            }
        }
    });
}

dmo.getQueryParamValue = function (paramKey) {
    /// <summary>根据 location.search 获取指定参数的值</summary>
    /// <param name="paramKey" type="String">参数名</param>
    var search = location.search;
    //去掉?
    var paramStr = decodeURI(search.substr(1));
    var paramArr = paramStr.split("&");
    for (var index in paramArr) { 
        if (paramArr.hasOwnProperty(index)) {
            var item = paramArr[index];
            var itemArr = item.split("=");
            for (var propIndex in itemArr) {
                if (itemArr.hasOwnProperty(propIndex)) {
                    if (itemArr[propIndex] === paramKey) {
                        return itemArr[parseInt(propIndex) + 1];
                    }
                }
            }
        }
    }
    return "";
}

