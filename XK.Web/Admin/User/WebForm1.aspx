<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="XK.Web.Admin.User.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        //var modalAlert = new ModalAlert();
       // var obj = modalAlert.create("test","contentttt");
        //console.log(obj);
        var ModalAlert = (function () {
            function ModalAlert() {
            }
            ModalAlert.prototype.ModalAlert = function (name) {
                this.name = name;
            };
            ModalAlert.prototype.create = function (title, content) {
                var head = document.createElement("div");
                head.innerText = title;
                var container = document.createElement("div");
                container.innerHTML = content;
                var main = document.createElement("div");
                var obj = main.appendChild(head).appendChild(container);
            };
            return ModalAlert;
        })();


        var myAlert = new ModalAlert("xiaokang");
        console.log(myAlert);
        bootbox.alert(myAlert.name);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
