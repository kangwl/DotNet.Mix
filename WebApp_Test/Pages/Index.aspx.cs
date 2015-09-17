using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApp_Test.App;

namespace WebApp_Test.Pages {
    public partial class Index : BasePage {
        public Index() {
            CheckLogin = false;
            CheckPower = false;
        }
        protected void Page_Load(object sender, EventArgs e) {

        }
    }
}