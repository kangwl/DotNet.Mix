﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlashUploadify.ascx.cs" Inherits="WebApp_Test.WebControls.FlashUploadify" %>
<script src="/WebControls/uploadify/jquery.uploadify.zh.min.js"></script>
<link href="/WebControls/uploadify/uploadify.css" rel="stylesheet" />
<style>
    .upload_button_class{clear: both;background: #3175AF;border: none;}
    /*重写样式*/
    .uploadify-queue-item{ width: 100% !important;max-width: 100% !important}

</style>
 
<div class="panel panel-default">
    <div class="panel-heading">
        文件上传
    </div>
    <div class="panel-body">
        <div style="height: 70px;background: #fff !important" class="well">
            <div style="width: 200px;float: left">
                <input type="file"  name="<%=UploadID %>" id="<%=UploadID %>" />
            </div>
             <div style="width: 200px;float: left">
                <button class="btn btn-sm btn-info" onclick="upload();" type="button">
                    <span class="glyphicon glyphicon-upload"></span> 上传
                </button> &nbsp; 
                <button class="btn btn-sm btn-danger" onclick="cancelUpload();" type="button">
                    <span class="glyphicon glyphicon-remove-circle"></span> 取消
                </button>
             </div>
        </div> 
        <div id="upload_queue">
            
        </div>
    </div>
    <div class="panel-footer">

    </div>
</div>
<script>
    var uploadID = "<%=UploadID %>";
    $(function () {

        $('#' + uploadID).uploadify(<%=UploadifyConfig%>);
    });

    function upload() {
        $("#" + uploadID).uploadify("upload", "*");
    }
    function cancelUpload() {
        $("#" + uploadID).uploadify("cancel", "*");
    }
</script>