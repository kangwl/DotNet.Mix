using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using StackExchange.Redis;

namespace ConsoleApp_Redis {
    class Program {
        static void Main(string[] args) {

            //ConfigurationOptions options = new ConfigurationOptions {
            //    AllowAdmin = true,
            //    EndPoints = {new IPEndPoint(IPAddress.Parse("192.168.3.66"), 6379)},
            //    Password = "abc123"
            //};

            //ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            //IDatabase database = connection.GetDatabase();
            //string val = database.StringGet("name"); 
            //Console.WriteLine(val);


            //var eds = connection.GetEndPoints();
            //foreach (EndPoint ed in eds) {
            //    Console.WriteLine(ed);
            //}

            //var server = connection.GetServer(options.EndPoints[0]);
            //Console.WriteLine(server.Version.ToString());

            DapperTest();

            Console.Read();
        }

        private static void DapperTest() {
            using (IDbConnection connection = new SqlConnection("server=192.168.3.66;database=myedu;uid=sa;pwd=abc123;")
                ) {

                int ret = connection.Execute("insert into eduuser(userid,userpass,name,age,birthday) values" +
                                             "(@uid,@upass,@name,@age,@birthday)",
                    new {
                        uid = "kangwl",
                        upass = "kangwl123",
                        name = "康文立",
                        age = 12,
                        birthday = DateTime.Now.AddYears(-20)
                    });

                Console.WriteLine(ret>0);

            }
        }
    }
}
