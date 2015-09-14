﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp_Test.WebControls.uploadify.Core;
using XK.Common;

namespace WebApp_Test.WebControls {
    public partial class FlashUploadify : System.Web.UI.UserControl {

        public string UploadifyConfig { get; set; }

        private uploadify.Core.UploadifyModel model;

        public FlashUploadify() {
            model = new UploadifyModel();
            model.buttonClass = "upload_button_class";
            UploadID = "file_upload";
        }
        protected void Page_Load(object sender, EventArgs e) {
            if(string.IsNullOrEmpty(UploadID))throw new ArgumentNullException("UploadID","不能为空");
            if (string.IsNullOrEmpty(swf)) throw new ArgumentNullException("swf", "不能为空");
            if (string.IsNullOrEmpty(uploader)) throw new ArgumentNullException("uploader", "不能为空");

            UploadifyConfig = model.ToJson();
        }

        public string UploadID { get; set; }

        public string swf
        {
            get { return model.swf; }
            set { model.swf = value; }
        }

        public string uploader
        {
            get { return model.uploader; }
            set { model.uploader = value; }
        }

        public bool auto
        {
            get { return model.auto; }
            set { model.auto = value; }
        }

        public string buttonClass
        {
            get { return model.buttonClass; }
            set { model.buttonClass = value; }
        }

        public string buttonText
        {
            get { return model.buttonText; }
            set { model.buttonText = value; }
        }

        public string buttonImage
        {
            get { return model.buttonImage; }
            set { model.buttonImage = value; }
        }

        public string fileSizeLimit
        {
            get { return model.fileSizeLimit; }
            set { model.fileSizeLimit = value; }
        }

        public string fileTypeDesc
        {
            get { return model.fileTypeDesc; }
            set { model.fileTypeDesc = value; }
        }

        public string fileTypeExts
        {
            get { return model.fileTypeExts; }
            set { model.fileTypeExts = value; }
        }

        public Dictionary<string, dynamic> formData
        {
            get { return model.formData; }
            set { model.formData = value; }
        }

        public bool multi
        {
            get { return model.multi; }
            set { model.multi = value; }
        }

        public int queueSizeLimit
        {
            get { return model.queueSizeLimit; }
            set { model.queueSizeLimit = value; }
        }

        public int uploadLimit
        {
            get { return model.uploadLimit; }
            set { model.uploadLimit = value; }
        }

        public string queueID
        {
            get { return model.queueID; }
            set { model.queueID = value; }
        }

        public int width
        {
            get { return model.width; }
            set { model.width = value; }
        }

        public int height
        {
            get { return model.height; }
            set { model.height = value; }
        }
    }
}