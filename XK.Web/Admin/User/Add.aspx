﻿<%@ Page Title="add uer" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="XK.Web.Admin.User.Add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-default">
        <div class="panel-heading">
            add user
        </div>
        <table id="reguser" class="table table-hover">
            <tr>
                <td class="text-right">UserID</td>
                <td>
                    <input type="text" id="txtUserID" name="uid" class="form-control" />
                </td>
            </tr>
            <tr>
                <td class="text-right">Password</td>
                <td>
                    <input type="password" id="txtPassword" name="pwd" class="form-control" />
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
                    <input type="text" id="txtBirthday" name="birthday" class="form-control input_datePicker" />
                </td>
            </tr>
            <tr>
                <td class="text-right">Email</td>
                <td>
                    <input type="text" id="txtEmail" name="email" class="form-control" />
                </td>
            </tr>
        </table>
        <div class="panel-footer text-center">
            <button type="button" id="btnAdd" class="btn btn btn-primary">添加</button> &nbsp;
            <a class="btn btn btn-default" href="List.aspx">返回</a>
        </div>
    </div>
    <script>
 
       // $("#reguser").serializeArray
        $(document).on("click", "#btnAdd", function () {
            bootbox.confirm("确定添加？", function(sure) {
                if (sure) { 
                    addUser();
                }
            });
        });

        function addUser() {
            //需要 /scripts/logic/user.js
            dmo.require(dmo.logic.userJS, function(success) {
                if (success) { //如果引用JS成功
                    var userOperate = dmo.getUserOperateObj();
                    var data = $("#reguser input,select").serialize(); //序列化需要的数据
                    userOperate.addUser(data, function(res) {
                        if (res.code === 1) {
                            //success 
                        }
                        bootbox.alert(res.msg);
                    });
                }
            });
        }
    </script>
</asp:Content>
