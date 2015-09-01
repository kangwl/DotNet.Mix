using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreedomDB.Bridge;
using MySql.Data.MySqlClient;

namespace FreedomDB.Helper {
    public class DbHelper  {

        private static string connStr = "server=192.168.3.66;database=myedu;uid=sa;pwd=abc123;";

        public DbHelper() : this(connStr) {

        }

        public SqlParameter[] CreateWhereSqlParameters(Where where) {
            List<Where.Item> items = where.WhereItems;
            List<SqlParameter> parameters = items.Select(item => new SqlParameter($"@{item.Field}", item.Value)).ToList();
            return parameters.ToArray();
        }

        public SqlCommand Command {
            get {
                SqlCommand command = Connection.CreateCommand();
                if (command.Connection.State != ConnectionState.Open) {
                    command.Connection.Open();
                }
                return command;
            }
        }

        public MySqlCommand MySqlCommand {
            get {
                MySqlCommand command = MySqlConnection.CreateCommand();
                if (command.Connection.State != ConnectionState.Open) {
                    command.Connection.Open();
                }
                return command;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接串</param>
        /// <param name="dbEnum">数据库类型</param>
        public DbHelper(string connStr, DbEnum dbEnum = DbEnum.MsSql) {
            switch (dbEnum) {
                case DbEnum.MsSql:
                    Connection = new SqlConnection(connStr);
                    SqlSpecialChar = "@";
                    break;
                case DbEnum.MySql:
                    MySqlConnection = new MySqlConnection(connStr);
                    SqlSpecialChar = "?";
                    break;
                default:
                    Connection = new SqlConnection(connStr);
                    break;
            }
        }

        private string SqlSpecialChar { get; set; }

        public SqlConnection Connection { get; set; }

        public MySqlConnection MySqlConnection { get; set; }

        public enum DbEnum {
            /// <summary>
            /// ms 数据库
            /// </summary>
            MsSql,

            /// <summary>
            /// Mysql 数据库
            /// </summary>
            MySql
        }
 
    }
}
