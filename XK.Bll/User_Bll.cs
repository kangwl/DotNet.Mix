using System.Collections.Generic;
using System.Threading.Tasks;
using FreedomDB.Bridge;
using XK.Dal;
using XK.Model;

namespace XK.Bll {
    public class User_Bll {

        private static readonly User_Dal Dal = new User_Dal();

        public static bool InsertUser(User_Model tModel) {
            return Task<bool>.Factory.StartNew(() => Dal.Insert(tModel)).Result;
        }

        public static async Task<bool> UpdateUser(Update update) {
            return await Task<bool>.Factory.StartNew(() => Dal.Update(update));
        }

        public static async Task<bool> DeleteUser(Where where) {
            return await Task<bool>.Factory.StartNew(() => Dal.Delete(where));
        }

        public static async Task<User_Model> GetOneUser(Where @where, string fields) {
            return await Task<User_Model>.Factory.StartNew(() => Dal.GetOne(where, fields));
        }

        public static async Task<List<User_Model>> GetListUser(Where where, string fields, string orderby, int pageIndex,
            int pageSize) {
            return
                await
                    Task<List<User_Model>>.Factory.StartNew(
                        () => Dal.GetList(where, fields, orderby, pageIndex, pageSize));
        }


        public static async Task<int> InsertBatch(List<User_Model> tModels, bool tran = false) {
            return await Task<int>.Factory.StartNew(() => Dal.InsertBatch(tModels, tran));
        }

    }
}
