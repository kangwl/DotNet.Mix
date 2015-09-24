/*
操作user
*/
dmo.getUserOperateObj = function () {
    /// <summary>获取用户操作对象</summary>
    var userOperateObj = {};

    userOperateObj.data = { pageIndex: 1, pageSize: 5 }
    userOperateObj.getSex = function (sex) {
        /// <summary>获取用户性别</summary>
        /// <param name="sex" type="Int">性别代码值</param>
        if (sex === 1) {
            return "男";
        } else if (sex === 2) {
            return "女";
        }
        return "未知";
    }
    userOperateObj.getBirthday = function (val) {
        /// <summary>获取用户生日，只保留年月日</summary>
        /// <param name="val" type="String">生日值</param>
        var birthday = "";
        if (val === "9999-12-31 23:59:59") {
            return birthday;
        }
        var arr = val.split(" ");
        if (arr.length > 1) {
            birthday = arr[0];
        } else {
            birthday = val;
        }
        return $.trim(birthday);
    }
    userOperateObj.getUserList = function (fnGetRes,fnBeforeSend) {
        /// <summary>获取用户列表</summary>
        /// <param name="fnGetRes" type="Function">处理请求到数据的回调函数</param>
        /// <param name="fnBeforeSend" type="Function">处理请求前执行的方法</param>

        dmo.reqServer("/api/user/list", "get", this.data, function(res) {
            if (res.code === 1) {
                //success
                fnGetRes(res);
            } else {
                bootbox.alert(res.msg);
            }
        }, fnBeforeSend);
    }
    userOperateObj.onPageClick = function (pageIndex, fnGetRes, fnBeforeSend) {
        /// <summary>用于点击分页</summary>
        /// <param name="pageIndex" type="Int">要请求的页码值</param>
        /// <param name="fnGetRes" type="Function">处理请求到数据的回调函数</param>
        /// <param name="fnBeforeSend" type="Function">处理请求前执行的方法</param>

        userOperateObj.data.pageIndex = pageIndex;
        userOperateObj.getUserList(fnGetRes, fnBeforeSend);
    }

    //add
    userOperateObj.addUser = function (data, fnSuccess,fnBeforeSend, fnError, fnComplete) {
        /// <summary>添加用户</summary>
        /// <param name="fnSuccess" type="Function">成功后回调函数</param>
        /// <param name="fnBeforeSend" type="Function">处理请求前执行的方法</param>
        /// <param name="fnError" type="Function">失败后回调函数</param>
        /// <param name="fnComplete" type="Function">完成后回调函数</param>

        dmo.reqServer("/api/user/add", "post", data, fnSuccess, fnBeforeSend, fnError, fnComplete);
    }

    userOperateObj.editUser = function(data, fnSuccess,fnBeforeSend, fnError, fnComplete) {
        /// <summary>修改用户</summary>
        /// <param name="fn_success" type="Function">成功后回调函数</param>
        /// <param name="fnBeforeSend" type="Function">处理请求前执行的方法</param>
        /// <param name="fn_error" type="Function">失败后回调函数</param>
        /// <param name="fn_complete" type="Function">完成后回调函数</param>
        
        dmo.reqServer("/api/user/edit", "post", data, fnSuccess, fnBeforeSend, fnError, fnComplete);
    }

    userOperateObj.getUser= function(data,fnSuccess,fnBeforeSend, fnError, fnComplete) {
        /// <summary>根据用户ID获取用户信息</summary>
        /// <param name="data" type="Object">请求参数</param>
        /// <returns type="Object">用户json</returns>
       
        dmo.reqServer("/api/user/getone", "get", data, fnSuccess, fnBeforeSend, fnError, fnComplete);
    }

    userOperateObj.deleteUser = function (data, fnSuccess, fnBeforeSend, fnError, fnComplete) {
        /// <summary>删除用户</summary>

        dmo.reqServer("/api/user/del", "post", data, fnSuccess, fnBeforeSend, fnError, fnComplete);
    }


    return userOperateObj;
}


