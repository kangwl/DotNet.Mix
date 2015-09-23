<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="XK.Web.Admin.User.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-footer{padding: 0 15px}
        .panel-footer .pagination{ margin: 15px 0;margin-bottom: 10px}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                <p>
                    user list
                    <a class="btn btn-sm btn-primary pull-right" href="Add.aspx">添加</a>
                </p>
            </div>
        </div>
        <div class="panel-body">
            <table id="tabe_list" class="table table-hover">
                <thead>
                    <tr>
                        <th>账号</th>
                        <th>用户名</th>
                        <th>性别</th>
                        <th>生日</th>
                        <th>操作</th>
                    </tr>
                </thead>
                <tbody>
                    
                </tbody>
            </table>
        </div>
        <div id="pageContent" class="panel-footer">
            
        </div>
    </div>
    
    <script src="/Scripts/bootstrap.page.js"></script> 
    <script>
        var useroperate;
        function loadTip() { 
            $("#tabe_list").find("tbody").html("<h4>数据加载中...</h4>");
        }

        dmo.require(dmo.logic.userJS, function (success) {
            if (success) {
                useroperate = dmo.getUserOperateObj();
                useroperate.getUserList(getRes, loadTip);
            }
        });

        function getRes(res) {
            var trs = [];

            var userJsonArr = res.data;
            $(userJsonArr).each(function(i, n) {
                var trContainer = [];
                trContainer.push("<tr>");
                trContainer.push("<td>" + n.UserID + "</td>");
                trContainer.push("<td>" + n.Name + "</td>");
                trContainer.push("<td>" + useroperate.getSex(n.Sex) + "</td>");
                trContainer.push("<td>" + useroperate.getBirthday(n.Birthday) + "</td>");
                //添加操作
                trContainer.push("<td data-id=" + n.ID + ">");
                trContainer.push("<button type='button' class='btn btn-xs btn-info edit'><span class=\"glyphicon glyphicon-edit\"></span> 修改</button> &nbsp;");
                trContainer.push("<button type='button' class='btn btn-xs btn-danger delete'><span class=\"glyphicon glyphicon-remove\"></span> 删除</button>");
                trContainer.push("</td>");
                //添加操作 结束
                trContainer.push("</tr>");
                var tr = trContainer.join("");
                trs.push(tr);
            });
            $("#tabe_list").find("tbody").html(trs.join(""));
            //设置分页
            dmo.setPager("pageContent", useroperate.data.pageIndex, useroperate.data.pageSize, parseInt(res.total), function(pageIndex) {
                useroperate.onPageClick(pageIndex, getRes, loadTip);
            }); 
        }

        $(document).on("click", "#tabe_list .edit", function(e) {
            var id = $(e.target).parent("td").attr("data-id");
            location.href = "edit.aspx?uid=" + id;
        });
    </script>
</asp:Content>
