<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XK.Web.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-lg-3 col-md-3">
        </div> 
        <div class=" col-lg-6 col-md-6">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <div class="panel-title">
                        用户登录
                    </div>
                </div>
                <div class="panel-body">
                    <div class="input-group form-group">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-user"></span></span>
                        <input type="text" id="uid" class="form-control"/>
                    </div>
                    <div class="input-group form-group">
                        <span class="input-group-addon"><span class="glyphicon glyphicon-lock"></span></span>
                        <input type="password" id="pwd" class="form-control"/>
                    </div>
                    <div class="text-center form-group">
                        <button type="button" class="btn btn-default btn-block" id="login">登录</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3">
        </div> 
    </div>
    <script>
        $(document).on("click", "#login", function() {
            login();
        });

        function login() {
            dmo.require(dmo.logic.loginJS, function(ok) {
                if (ok) {
                    var loginOperate = dmo.getLoginOperatObj();
                    var data = { uid: $("#uid").val(), pwd: $("#pwd").val() };
                    loginOperate.login(data, function(res) {
                        if (res.code === 1) {
                            //登录成功
                            $.bsAlertSuccess("登录成功，跳转中...");
                            location.href = "/admin/user/list.aspx";
                        } else {
                            bootbox.alert(res.msg);
                        }
                    }, function(e) {
                        console.error(e);
                    });
                }
            });
        }
    </script>
</asp:Content>
