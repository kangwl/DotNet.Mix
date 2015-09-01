using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreedomDB.Bridge;
using MySql.Data.MySqlClient;

namespace FreedomDB.Helper.Extension {
    public static class MyDbCommand {

        /// <summary>
        /// 主要用于delete
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryExt(this System.Data.Common.DbCommand command, string sql,FreedomDB.Bridge.Where where) {
            command.CommandText = sql; 
            command.Parameters.AddRange(command.CreateWhereSqlParameters(where));
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 主要用于update
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static int ExecuteNonQueryExt(this System.Data.Common.DbCommand command, string sql, FreedomDB.Bridge.Update update) {
            command.CommandText = sql;
            command.Parameters.AddRange(command.CreateUpdateSqlParams(update));
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 用于读取数据
        /// </summary>
        /// <param name="command"></param>
        /// <param name="sql"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReaderExt(this System.Data.Common.DbCommand command, string sql, Where where) {
            command.CommandText = sql;
            command.Parameters.AddRange(command.CreateWhereSqlParameters(where));
            return command.ExecuteReader();
        }
        public static IDataReader ExecuteReaderExt(this System.Data.Common.DbCommand command, string sql) {
            command.CommandText = sql;
            return command.ExecuteReader();
        }

        /// <summary>
        /// 用于执行多个表更新
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dic">key:sql,value:sqlparameters</param>
        /// <param name="tran">是否使用事务</param>
        /// <returns></returns>
        public static int ExecuteNonQueryMutiSql(this System.Data.Common.DbCommand command,
            Dictionary<string, IDbDataParameter[]> dic, bool tran = true) {
            using (command) {
                try {

                    if (tran) {
                        command.Transaction = command.Connection.BeginTransaction();
                    }
                    int success = 0;
                    foreach (KeyValuePair<string, IDbDataParameter[]> pair in dic) {
                        string sql = pair.Key;
                        IDbDataParameter[] parameters = pair.Value;

                        command.CommandText = sql;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Clear();
                        command.Parameters.AddRange(parameters);
                        success += command.ExecuteNonQuery();
                    }
                    command.Transaction.Commit();
                    return success;
                }
                catch (Exception ex) {
                    if (tran) {
                        command.Transaction.Rollback();
                    }
                    return 0;
                }
            }
        }

        /// <summary>
        /// 生成 where sqlparameter参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static SqlParameter[] CreateWhereSqlParameters(this IDbCommand command,Where where) {
            List<Where.Item> items = where.WhereItems;
            List<SqlParameter> parameters = items.Select(item => new SqlParameter($"@{item.Field}", item.Value)).ToList();
            return parameters.ToArray();
        }

        /// <summary>
        /// 生成 update sqlparameter参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public static SqlParameter[] CreateUpdateSqlParams(this IDbCommand command,Update update) {
            Dictionary<string, dynamic> dic = update.Dic;
            List<SqlParameter> parameters = dic.Select(pair => new SqlParameter($"@{pair.Key}", pair.Value)).ToList();
            Where where = update.WhereCore;
            List<Where.Item> items = where.WhereItems;
            parameters.AddRange(items.Select(item => new SqlParameter($"@{item.Field}", item.Value)));
            return parameters.ToArray();
        }
    }
}
