﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace XK.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e) {
            Common.Log4net.Init();
           // XK.SearchEngine.AutoFac.Enter.InitLuceneWorkThread();
        }

        
    }
}