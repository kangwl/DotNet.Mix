<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="XK.Web.Admin.User.List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .panel-footer{padding: 0 15px}
        .panel-footer .pagination{ margin: 15px 0;margin-bottom: 10px}
    </style>
    <script src="/Scripts/bootstrap.page.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            <div class="panel-title">
                user list
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
                    </tr>
                </thead>
                <tbody>
                    
                </tbody>
            </table>
        </div>
        <div id="pageContent" class="panel-footer">
            
        </div>
    </div>
    <script> 
       
        dataMainOperate.require("/Scripts/logic/user.js", function(script, state) {
            if (state === "success") {
                var userOperateObj = dataMainOperate.getUserOperateObj();
                userOperateObj.getUserList();
            }
        });
       

    </script>
</asp:Content>
