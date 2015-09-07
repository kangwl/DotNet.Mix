<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="WebApp_Test.xk.replace.test" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/replaceElementUtil.js"></script>
    <script>
        $(function() {
            replaceElementUtil.init({ mainSelector: "#table", itemsSelector: "#table .td" });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="table table-bordered" id="table">
        <tr>
            <td class="td">1</td>
            <td class="td">2</td>
            <td class="td">3</td>
        </tr>
        <tr>
            <td class="td">4</td>
            <td class="td">5</td>
            <td class="td">6</td>
        </tr>
    </table>
</asp:Content>
