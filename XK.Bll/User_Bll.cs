using System.Collections.Generic;
using System.Threading.Tasks;
using FreedomDB.Bridge;
using XK.Dal;
using XK.Model;

namespace XK.Bll {
    public class User_Bll {

        private static readonly User_Dal Dal = new User_Dal();

        public static bool InsertUser(User_Model tModel) {
            return Dal.Insert(tModel);
        }

        public static bool UpdateUser(Update update) {
            return Dal.Update(update);
        }

        public static bool DeleteUser(Where where) {
            return Dal.Delete(where);
        }

        public static User_Model GetOneUser(Where @where, string fields) {
            return Dal.GetOne(where, fields);
        }

        public static List<User_Model> GetListUser(Where where, string fields, string orderby, int pageIndex,
            int pageSize) {
            return Dal.GetList(where, fields, orderby, pageIndex, pageSize);
        }

        public static int GetRecordCount(Where where) {
            return Dal.GetRecordCount(where);
        }
    }
}
