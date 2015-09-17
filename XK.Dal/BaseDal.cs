using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreedomDB.Helper;

namespace XK.Dal {
    public class BaseDal {
        public BaseDal() {
            DbHelper.connStr =
                System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
        }
    }
}
