<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="uploadify.aspx.cs" Inherits="WebApp_Test.WebControls.Test.uploadify" %>

<%@ Register Src="~/WebControls/FlashUploadify.ascx" TagPrefix="uc1" TagName="FlashUploadify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:FlashUploadify runat="server" ID="FlashUploadify"
        buttonText="<span class='glyphicon glyphicon-folder-open'></span> &nbsp;&nbsp;选择文件"
        width="135" />

    <style type="text/css">
        .uploadify .uploadify-button {
            /*background: none*/
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px;
        }

        .uploadify:hover .uploadify-button {
            background-color: #286CA7;
            background-image: linear-gradient(top, #286CA7 0%, #808080 100%);
            background-image: -o-linear-gradient(top, #286CA7 0%, #808080 100%);
            background-image: -moz-linear-gradient(top, #286CA7 0%, #808080 100%);
            background-image: -webkit-linear-gradient(top, #286CA7 0%, #808080 100%);
            background-image: -ms-linear-gradient(top, #286CA7 0%, #808080 100%);
            background-image: -webkit-gradient( linear, left bottom, left top, color-stop(0, #286CA7), color-stop(1, #295A84) );
            background-position: center bottom;
        }
    </style>
</asp:Content>
