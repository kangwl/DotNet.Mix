using System.Collections.Generic;
using System.Data;
using FreedomDB.Bridge;

namespace MyTestShop.IDal.IBase {
    public interface IBase<TModel> where TModel : class {
        string Table { get; } 
        bool Insert(TModel tModel);
        bool Update(Update update);
        bool Delete(FreedomDB.Bridge.Where where);
        TModel GetOne(Where where, string fields = "*");
        List<TModel> GetList(Where where, string fields, string orderby, int pageIndex, int pageSize);
        TModel ReaderModel(IDataReader reader, string fields);
        int InsertBatch(List<TModel> tModels, bool tran = false);
        int GetRecordCount(Where where);
    }
}