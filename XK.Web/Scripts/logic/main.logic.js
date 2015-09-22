//操作对象
var dmo = {};

dmo.logic = {};//储存处理操作的js
dmo.logic.userJS = "/scripts/logic/user.js";
//设置分页
dmo.setPager = function(pageContentId, pageIndex, maxPage, fn) {
    $.mypage(pageContentId, pageIndex, maxPage, fn);
}
//动态加载JS文件
//fn 函数有两个参数(scriptText,state)
//当state==="success"时，调用成功
dmo.require= function(scriptUrl, fn) {
    myJQUtil.cacheScript(scriptUrl).done(function(scriptText, state) {
        var success = (state === "success");
        fn(success);
    });
}

