using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FreedomDB.Helper;
using FreedomDB.Helper.Extension;

namespace WinFormEntityCreate {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void btn_Connect_Click(object sender, EventArgs e) {
            FreedomDB.Helper.DbHelper dbHelper = new DbHelper(txt_ConnStr.Text.Trim());
            using (SqlCommand command= dbHelper.Command) {
                string sql = "SELECT Name FROM SysObjects Where XType='U' ORDER BY Name";
                IDataReader reader = command.ExecuteReaderExt(sql);
                 
                while (reader.Read()) {
                    string tableName = reader.GetString(reader.GetOrdinal("Name")); 
                    list_Tables.Items.Add(tableName);
                }
               
               
            }
        }

        private void 生成代码ToolStripMenuItem_Click(object sender, EventArgs e) {

            FreedomDB.Helper.DbHelper dbHelper = new DbHelper(txt_ConnStr.Text.Trim());
            string table = list_Tables.Text;
            string sql =
                $"SELECT syscolumns.name,systypes.name typename,syscolumns.isnullable,syscolumns.length FROM syscolumns, systypes WHERE syscolumns.xusertype = systypes.xusertype AND syscolumns.id = object_id('{table}')  ";

            using (SqlCommand command = dbHelper.Command) {
                IDataReader reader = command.ExecuteReaderExt(sql);
                Dictionary<string,string> dic=new Dictionary<string, string>();
                while (reader.Read()) {
                    string field = reader.GetString(reader.GetOrdinal("name"));
                    string typename = reader.GetString(reader.GetOrdinal("typename"));

                    dic.Add(field,typename);
                }


            }
        }

 
   
    }
}
