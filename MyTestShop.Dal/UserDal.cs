using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreedomDB.Bridge;
using FreedomDB.Helper;
using FreedomDB.Helper.Extension;
using MyTestShop.IDal;
using MyTestShop.Model;

namespace MyTestShop.Dal {
    public class UserDal : IUser{

        public string Table => "eduuser";

        private SqlParameter[] CreateSqlParameters(User tModel) {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@userid", SqlDbType.VarChar),
                new SqlParameter("@userpass", SqlDbType.VarChar),
                new SqlParameter("@name", SqlDbType.NVarChar),
                new SqlParameter("@age", SqlDbType.Int),
                new SqlParameter("@birthday", SqlDbType.DateTime)
            };
            parameters[0].Value = tModel.UserID;
            parameters[1].Value = tModel.UserPass;
            parameters[2].Value = tModel.Name;
            parameters[3].Value = tModel.Age;
            parameters[4].Value = tModel.Birthday;

            return parameters;
        }

        public bool Insert(User tModel) {
            using (SqlCommand command = new DbHelper().Command) {
                command.CommandText =
                    "insert into eduuser(userid,userpass,name,age,birthday) values (@userid,@userpass,@name,@age,@birthday)";
                command.Parameters.AddRange(CreateSqlParameters(tModel));
                int ret = command.ExecuteNonQuery();

                return ret > 0;
            }
        }

        public bool Update(Update update) {
            Dictionary<string, dynamic> dic = update.Dic;
            string where = update.WhereCore.Result;
            string fv = string.Join(",", dic.Select(pair => $"{pair.Key}=@{pair.Key}"));


            string sql = $"update {Table} set {fv} where {where};";
            using (SqlCommand command = new DbHelper().Command) {
                command.CommandText = sql;
                int ret = command.ExecuteNonQueryExt(sql, update);
                return ret > 0;
            }
        }


        public bool Delete(Where where) {
 
            using (SqlCommand command = new DbHelper().Command) {
                string sql = $"delete from {Table} where {where.Result};";
                return command.ExecuteNonQueryExt(sql, where) > 0;
            }
        }

        public User GetOne(Where @where, string fields) {
            using (SqlCommand command = new DbHelper().Command) {
                User user = new User();
                string sql = $"select top 1 {fields} from {Table} where {where.Result};";
                IDataReader reader = command.ExecuteReaderExt(sql, where);

                if (reader.Read()) {
                    user = ReaderModel(reader, fields);
                }
                return user;
            }
        }

        public User ReaderModel(IDataReader reader, string fields) {

            #region check fields

            string flds = (string.IsNullOrEmpty(fields)) ? "*" : fields.Trim();
            IEnumerable<string> fieldList = new List<string>();
            if (flds != "*") {
                fieldList = flds.Split(',').Select(field => field.Trim());
            }

            #endregion

            User user = new User();
            if (flds == "*" || fieldList.Contains("ID")) {
                user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
            }
            if (flds == "*" || fieldList.Contains("Age")) {
                user.Age = reader.GetInt32(reader.GetOrdinal("Age"));
            }
            if (flds == "*" || fieldList.Contains("Birthday")) {
                user.Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"));
            }
            if (flds == "*" || fieldList.Contains("Name")) {
                user.Name = reader.GetString(reader.GetOrdinal("Name"));
            }
            if (flds == "*" || fieldList.Contains("UserID")) {
                user.UserID = reader.GetString(reader.GetOrdinal("UserID"));
            }
            if (flds == "*" || fieldList.Contains("UserPass")) {
                user.UserPass = reader.GetString(reader.GetOrdinal("UserPass"));
            }

            return user;
        }


        public List<User> GetList(Where where, string fields, string orderby, int pageIndex, int pageSize) {

            string sqlBase = $"select {fields} from {Table} where {@where.Result} ";
            const string sqlPageBase = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

            int startPageIndex = (pageIndex - 1)*pageSize + 1;
            int endPageIndex = pageIndex*pageSize;

            string sqlPage = string.Format(sqlPageBase, sqlBase, orderby, startPageIndex, endPageIndex);
            using (SqlCommand command = new DbHelper().Command) {
                List<User> users = new List<User>();
                IDataReader reader = command.ExecuteReaderExt(sqlPage, where);
                while (reader.Read()) {

                    User user = ReaderModel(reader, fields);
                    users.Add(user);
                }
                return users;
            }
        }

        public int InsertBatch(List<User> tModels, bool tran = false) {
            using (SqlCommand command = new DbHelper().Command) {

                try {
                    if (tran) {
                        command.Transaction = command.Connection.BeginTransaction();
                    }
                    int ret = 0;
                    foreach (User tModel in tModels) {
                        command.CommandText =
                            "insert into eduuser(userid,userpass,name,age,birthday) values (@userid,@userpass,@name,@age,@birthday)";
                        command.Parameters.Clear();
                        command.Parameters.AddRange(CreateSqlParameters(tModel));
                        ret += command.ExecuteNonQuery();
                    }
                    if (tran) {
                        command.Transaction.Commit();
                    }
                    return ret;
                }
                catch (Exception ex) {
                    if (tran) {
                        command.Transaction.Rollback();
                    }
                    return 0;
                }
            }
        }
    }
}
