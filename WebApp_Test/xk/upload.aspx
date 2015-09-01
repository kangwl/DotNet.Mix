<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="WebApp_Test.xk.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Content/bs_file_input/fileinput.min.css" rel="stylesheet" />
    <script src="/Scripts/bs_file_input/fileinput.js"></script>
    <script src="/Scripts/bs_file_input/fileinput_locale_zh.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="panel panel-success form-group">
        <div class="panel-heading">
            <div class="panel-title">
                文件上传
            </div>
        </div>
        <div class="panel-body">
            <input id="input-id" type="file" accept="image/*" class="file" multiple data-preview-file-type="text"/>
        </div>
        <div class="panel-footer" id="upload_foot">
            
        </div>
    </div>
    <script>

        // with plugin options
        $("#input-id").fileinput({
            showCaption: false,
            language: "zh",
            browseClass: "btn btn-success",
            browseLabel: "选择图片",
            browseIcon: "<i class=\"glyphicon glyphicon-picture\"></i>",
            previewFileType: "image",
            showUpload: true,
            allowedFileExtensions: ['jpg', 'gif', 'png', 'doc', 'docx'],
            previewSettings: {
                // image: { width: "200px", height: "160px" }
            },
            //allowedFileTypes: ["image"], //['image', 'html', 'text', 'video', 'audio', 'flash', 'object']
            //'previewFileType': 'any',
            initialCaption: "请选择文件",
            removeClass: "btn btn-danger",
            removeLabel: "删除",
            removeIcon: "<i class=\"glyphicon glyphicon-trash\"></i> ",
            uploadClass: "btn btn-info",
            uploadLabel: "上传",
            uploadIcon: "<i class=\"glyphicon glyphicon-upload\"></i> ",
            minImageWidth: 50,
            minImageHeight: 50,
            //maxImageWidth: 2500,
            //maxImageHeight: 2500,
            maxFileCount: 4,
            validateInitialCount: true,
            uploadUrl: "uprecieve.aspx",
            uploadAsync: true,
            dropZoneEnabled: true,
            maxFileSize: 0 //不限制大小，单位：kb
        });

        /*
         清除队列：$('#input-id').fileinput('clear');
         选择文件后触发：      
         $('#input-id').on('filebatchselected', function(event, files) {
            $('#input-id').fileinput('clear');
         });
        */

        $('#input-id').on('filebatchuploadcomplete', function(event, files, extra) {
            $.bsAlertSuccess("上传成功", "#upload_foot", 3);
        });
 
    </script>
 
</asp:Content>
