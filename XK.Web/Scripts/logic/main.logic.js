//操作对象
var dataMainOperate = {};
//设置分页
dataMainOperate.setPager = function(pageContentID, pageIndex, maxPage, fn) {
    $.mypage(pageContentID, pageIndex, maxPage, fn);
}
//动态加载JS文件
//fn 函数有两个参数(scriptText,state)
//当state==="success"时，调用成功
dataMainOperate.require= function(scriptUrl, fn) {
    myJQUtil.cacheScript(scriptUrl).done(fn);
}

