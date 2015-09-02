<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Html5UploadControl.ascx.cs" Inherits="WebApp_Test.WebControls.Html5UploadControl" %>
<link href="/WebControls/Html5Upload/css/fileinput.min.css" rel="stylesheet" />
 <script src="/WebControls/Html5Upload/js/fileinput.js"></script>
<script src="/WebControls/Html5Upload/js/fileinput_locale_zh.js"></script>

<input id="upload_id" type="file" accept="<%=Accept %>" class="file" <%=MutiStr %> />
<script>
    $("#upload_id").fileinput(<%=UploadJson%>);

    //$('#upload_id').on('filebatchuploadcomplete', function(event, files, extra) {
    //    $.bsAlertSuccess("上传成功", "#upload_foot", 3);
    //});
 
</script>