using System.Collections.Generic;
using System.Threading.Tasks;
using FreedomDB.Bridge;
using MyTestShop.Dal;
using MyTestShop.IDal;
using MyTestShop.Model;

namespace MyTestShop.Bll {
    public class UserBll {

        private static readonly IUser Dal = new UserDal();

        public static async Task<bool> InsertUser(User tModel) {
            return await Task<bool>.Factory.StartNew(() => Dal.Insert(tModel));
        }

        public static async Task<bool> UpdateUser(Update update) {
            return await Task<bool>.Factory.StartNew(() => Dal.Update(update));
        }

        public static async Task<bool> DeleteUser(Where where) {
            return await Task<bool>.Factory.StartNew(() => Dal.Delete(where));
        }

        public static async Task<Model.User> GetOneUser(Where @where, string fields) {
            return await Task<Model.User>.Factory.StartNew(() => Dal.GetOne(where, fields));
        }

        public static async Task<List<Model.User>> GetListUser(Where where, string fields, string orderby, int pageIndex,
            int pageSize) {
            return
                await
                    Task<List<Model.User>>.Factory.StartNew(
                        () => Dal.GetList(where, fields, orderby, pageIndex, pageSize));
        }


        public static async Task<int> InsertBatch(List<User> tModels, bool tran = false) {
            return await Task<int>.Factory.StartNew(() => Dal.InsertBatch(tModels, tran));
        }

    }
}
