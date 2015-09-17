<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="JqueryUpload.aspx.cs" Inherits="WebApp_Test.WebControls.Test.JqueryUpload" %>

<%@ Register Src="~/WebControls/JqueryUploadControl.ascx" TagPrefix="uc1" TagName="JqueryUploadControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:JqueryUploadControl runat="server" id="JqueryUploadControl" />
</asp:Content>
