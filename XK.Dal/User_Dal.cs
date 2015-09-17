using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreedomDB.Bridge;
using FreedomDB.Helper;
using FreedomDB.Helper.Extension;
using XK.Common;
using XK.Model;

namespace XK.Dal {
    public class User_Dal : BaseDal {



        public string Table => "[User]";

        private SqlParameter[] CreateSqlParameters(User_Model tModel) {
            SqlParameter[] parameters = new SqlParameter[] {
                new SqlParameter("@UserID", SqlDbType.VarChar),
                new SqlParameter("@Password", SqlDbType.VarChar),
                new SqlParameter("@Name", SqlDbType.NVarChar),
                new SqlParameter("@Sex", SqlDbType.Int),
                new SqlParameter("@Birthday", SqlDbType.DateTime),
                new SqlParameter("@Email", SqlDbType.VarChar)
            };
            parameters[0].Value = tModel.UserID;
            parameters[1].Value = tModel.Password;
            parameters[2].Value = tModel.Name;
            parameters[3].Value = tModel.Sex;
            parameters[4].Value = tModel.Birthday;
            parameters[5].Value = tModel.Email;

            return parameters;
        }

        public bool Insert(User_Model tModel) {
            using (SqlCommand command = new DbHelper().Command) {
                command.CommandText =
                    $"insert into {Table}(UserID,[Password],Name,Sex,Birthday,Email) values (@UserID,@Password,@Name,@Sex,@Birthday,@Email)";
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

        public User_Model GetOne(Where @where, string fields) {
            using (SqlCommand command = new DbHelper().Command) {
                User_Model user = new User_Model();
                string sql = $"select top 1 {fields} from {Table} where {where.Result};";
                IDataReader reader = command.ExecuteReaderExt(sql, where);

                if (reader.Read()) {
                    user = ReaderModel(reader, fields);
                }
                return user;
            }
        }

        public User_Model ReaderModel(IDataReader reader, string fields) {

            #region check fields

            string flds = (string.IsNullOrEmpty(fields)) ? "*" : fields.Trim();
            IEnumerable<string> fieldList = new List<string>();
            if (flds != "*") {
                fieldList = flds.Split(',').Select(field => field.Trim());
            }

            #endregion

            User_Model user = new User_Model();
            if (CheckField(flds, fieldList, "ID")) {
                user.ID = reader.GetInt32(reader.GetOrdinal("ID"));
            }
            if (CheckField(flds, fieldList, "Sex")) {
                user.Sex = reader.GetInt32(reader.GetOrdinal("Sex"));
            }
            if (CheckField(flds, fieldList, "Birthday")) {
                user.Birthday = reader.GetDateTime(reader.GetOrdinal("Birthday"));
            }
            if (CheckField(flds, fieldList, "Name")) {
                user.Name = reader.GetString(reader.GetOrdinal("Name"));
            }
            if (CheckField(flds, fieldList, "UserID")) {
                user.UserID = reader.GetString(reader.GetOrdinal("UserID"));
            }
            if (CheckField(flds, fieldList, "Password")) {
                user.Password = reader.GetString(reader.GetOrdinal("Password"));
            }
            if (CheckField(flds, fieldList, "Email")) {
                user.Email = reader.GetString(reader.GetOrdinal("Email"));
            }

            return user;
        }

        private bool CheckField(string flds, IEnumerable<string> fieldList, string field) {
            return flds == "*" || fieldList.Contains(field);
        }


        public List<User_Model> GetList(Where where, string fields, string orderby, int pageIndex, int pageSize) {

            string sqlBase = $"select {fields} from {Table} where {@where.Result} ";
            const string sqlPageBase = @"select * from( 
                            select *,ROW_NUMBER() OVER (ORDER BY {1}) as rank from ({0})a 
                          )as t where t.rank between {2} and {3}";

            int startPageIndex = (pageIndex - 1)*pageSize + 1;
            int endPageIndex = pageIndex*pageSize;

            string sqlPage = string.Format(sqlPageBase, sqlBase, orderby, startPageIndex, endPageIndex);
            using (SqlCommand command = new DbHelper().Command) {
                List<User_Model> users = new List<User_Model>();
                IDataReader reader = command.ExecuteReaderExt(sqlPage, where);
                while (reader.Read()) {

                    User_Model user = ReaderModel(reader, fields);
                    users.Add(user);
                }
                return users;
            }
        }

        public int InsertBatch(List<User_Model> tModels, bool tran = false) {
            using (SqlCommand command = new DbHelper().Command) {

                try {
                    if (tran) {
                        command.Transaction = command.Connection.BeginTransaction();
                    }
                    int ret = 0;
                    foreach (User_Model tModel in tModels) {
                        command.CommandText =
                            $"insert into {Table}(UserID,[Password],Name,Sex,Birthday,Email) values (@UserID,@Password,@Name,@Sex,@Birthday,@Email)";
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
                    XK.Common.Log4net.Error(ex);
                    return 0;
                }
            }
        }

        public int GetRecordCount(Where @where) {
            string sql = $"select count(1) from {Table} where {where.Result};";
            using (SqlCommand command = new DbHelper().Command) {
                command.CommandText = sql;
                command.CommandType = CommandType.Text;
                object objCount = command.ExecuteScalar();
                return objCount.ToInt();
            }
        }
    }
}
