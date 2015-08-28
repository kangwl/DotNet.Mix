using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace XK.DB {
    public class DapperHelper {

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connStr">连接串</param>
        /// <param name="dbEnum">数据库类型</param>
        public DapperHelper(string connStr, DbEnum dbEnum = DbEnum.MSSql) {
            switch (dbEnum) {
                case DbEnum.MSSql:
                    Connection = new SqlConnection(connStr);
                    break;
                case DbEnum.MYSql:
                    Connection = new MySqlConnection(connStr);
                    break;
                default:
                    Connection = new SqlConnection(connStr);
                    break;
            }
        }

        public IDbConnection Connection { get; set; }

        public enum DbEnum {
            /// <summary>
            /// ms数据库
            /// </summary>
            MSSql,
            /// <summary>
            /// Mysql数据库
            /// </summary>
            MYSql
        }

    }
}
