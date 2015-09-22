dataMainOperate.getUserOperateObj = function() {

    var userOperateObj = {};

    userOperateObj.data = { pageIndex: 1, pageSize: 5 }
    userOperateObj.getSex = function(sex) {
        if (sex === 1) {
            return "男";
        } else if (sex === 2) {
            return "女";
        }
        return "未知";
    }
    userOperateObj.getBirthday = function(val) {
        if (val === "9999-12-31 23:59:59") {
            return "";
        }
        return val;
    }
    userOperateObj.getUserList = function() {
        $("#tabe_list").find("tbody").html("<h4>数据加载中...</h4>");
        $.getJSON("/api/user/list", this.data, function(res) {
            if (res.code === 1) {
                //success
                var trs = [];

                var userJsonArr = res.data;
                $(userJsonArr).each(function(i, n) {
                    var trContainer = [];
                    trContainer.push("<tr>");
                    trContainer.push("<td>" + n.UserID + "</td>");
                    trContainer.push("<td>" + n.Name + "</td>");
                    trContainer.push("<td>" + userOperateObj.getSex(n.Sex) + "</td>");
                    trContainer.push("<td>" + userOperateObj.getBirthday(n.Birthday) + "</td>");
                    trContainer.push("</tr>");
                    var tr = trContainer.join("");
                    trs.push(tr);
                });
                $("#tabe_list").find("tbody").html(trs.join(""));
                var max = Math.ceil(parseInt(res.total) / parseInt(userOperateObj.data.pageSize));

                dataMainOperate.setPager("pageContent", userOperateObj.data.pageIndex, max, userOperateObj.onPageClick);
            } else {
                bootbox.alert(res.msg);
            }
        });
    }
    userOperateObj.onPageClick = function(pageIndex) {
        userOperateObj.data.pageIndex = pageIndex;
        userOperateObj.getUserList();
    }
    //add

    userOperateObj.addUser= function(fn_success, fn_error,fn_complete) {
        $.ajax({
            type: "post",
            url: "/api/user/add",
            data: $("#reguser input,select").serialize(),
            dataType: "json",
            success: fn_success,
            error: function(e) {
                if (typeof fn_error === "function") {
                    fn_error(e);
                }
                console.error(e);
            },
            complete:function(e) {
                if (typeof fn_complete === "function") {
                    fn_complete(e);
                }
            }
        });
    }


    return userOperateObj;
}


