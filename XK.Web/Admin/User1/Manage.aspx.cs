using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using XK.Common.web;
using XK.Web.WebControls;

namespace XK.Web.Admin.User1 {
    public partial class Manage : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            BindData();
        }

        private void GetAscxData() {
            WebControls.UEditor flashUploadify = LoadControl("/WebControls/UEditor.ascx") as UEditor;
            StringWriter stringWriter = new StringWriter();
            System.Web.UI.Page page = new Page();
            page.Controls.Add(flashUploadify);
            Server.Execute(page, stringWriter, false);
            string res = stringWriter.ToString();
            Response.Write(res);

        }
        private void BindData() {
            List<Data> datas = new List<Data> { new Data() { ID = 1 }, new Data() { ID = 2 } };
            WebControls.Testc testc=LoadControl("/WebControls/Testc.ascx") as Testc;
            Repeater rptList = testc.FindControl("rptList") as Repeater;
            rptList.BindExt(datas);
            System.Web.UI.Page page = new Page();
            page.Controls.Add(testc);
            StringWriter stringWriter = new StringWriter();
            Server.Execute(page, stringWriter,false);
            string ascxHtml = stringWriter.ToString();
            litContent.Text = ascxHtml;
            //Response.Write();
        }

        private class Data {
            public int ID { get; set; }
        }
    }
}