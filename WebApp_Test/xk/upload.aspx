<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApp_Test.xk.Upload" %>

<%--<%@ Register Src="~/WebControls/Html5UploadControl.ascx" TagPrefix="uc1" TagName="Html5UploadControl" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%-- <uc1:Html5UploadControl runat="server" ID="Html5UploadControl" 
      UploadUrl="/xk/uprecieve.aspx"
      Accept="*/*"
      MutiEnable="True"
      BrowseLabel="请选择文件"
      UploadAsync="False"
     />--%>
    <asp:PlaceHolder runat="server" ID="UploadControl"></asp:PlaceHolder>

</asp:Content>
