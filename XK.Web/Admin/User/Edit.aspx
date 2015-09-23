<%@ Page Title="修改用户" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="XK.Web.Admin.User.Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div class="panel panel-default">
        <div class="panel-heading">
            edit user
        </div>
        <div class="panel-body">
            <table id="reguser" class="table table-hover">
                <tr style="display: none">
                    <td>
                        <input type="hidden" name="_id" id="hidUID" />
                    </td>
                </tr>
                <tr>
                    <td class="text-right">UserID</td>
                    <td>
                        <label id="userid"></label>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">Name</td>
                    <td>
                        <input type="text" id="txtName" name="name" class="form-control" />
                    </td>
                </tr>
                <tr>
                    <td class="text-right">Sex</td>
                    <td>
                        <select id="selSex" name="sex" class="form-control">
                            <option value="0">请选择</option>
                            <option value="1">男</option>
                            <option value="2">女</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">Birthday</td>
                    <td>
                        <input type="text" id="txtBirthday" name="birthday" class="form-control" />
                    </td>
                </tr>
                <tr>
                    <td class="text-right">Email</td>
                    <td>
                        <input type="text" id="txtEmail" name="email" class="form-control" />
                    </td>
                </tr> 
            </table> 
        </div>
        <div class="panel-footer text-center">
            <button type="button" id="btnEdit" class="btn btn btn-primary">修改</button> &nbsp;
            <a class="btn btn btn-default" href="List.aspx">返回</a>
        </div>
    </div>
    <script> 
        var _id = dmo.getQueryParamValue("uid");
        $("#hidUID").val(_id);

        function startReq() {
            $("#reguser").hide();
            $(".panel-footer").find("button").hide();
            $("#reguser").after("<h4>加载中...</h4>");
        }
        function completeReq() {
            $("#reguser").show();
            $(".panel-footer").find("button").show();
            $("#reguser").next("h4").remove();
            dmo.setDatePicker("#txtBirthday");
        }

        function getUser() {
            dmo.require(dmo.logic.userJS, function(success) {
                if (!success)return false;
                var userOperate = dmo.getUserOperateObj();
                var data= { id: _id }
                userOperate.getUser(data, function(res) {
                    if (res.code === 1) {
                        var userJson = res.data;
                        $("#userid").text(userJson.UserID);
                        $("#txtName").val(userJson.Name);
                        $("#selSex").val(userJson.Sex);
                        $("#txtBirthday").val(userOperate.getBirthday(userJson.Birthday)); 
                        $("#txtEmail").val(userJson.Email);
                    }
                }, startReq,null,completeReq);

                return true;
            });
        }
        //req
        getUser();

        $(document).on("click", "#btnEdit", function(e) {
            bootbox.confirm("确定修改？", function(sure) {
                if (sure) {
                    updateUser();
                }
            });
        });

        function updateUser() {
            dmo.require(dmo.logic.userJS, function(success) {
                if (!success) return false;
                var userOperate = dmo.getUserOperateObj();
                var data = $("#reguser input,select").serialize();

                userOperate.editUser(data, function(res) {
                    bootbox.alert(res.msg);
                }, startUpdate, null, completeUpdate);

                return true;
            });
        }

        function startUpdate() {
            $("#btnEdit").text("更新中...");
            $("#btnEdit").attr("disabled", "disabled");
        }

        function completeUpdate() {
            $("#btnEdit").text("修改");
            $("#btnEdit").removeAttr("disabled");
        }
 
    </script>
</asp:Content>
