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
using XK.Common;

namespace ConsoleApp_Redis {
    class Program {
        private static void Main(string[] args) {

             string host = "192.168.3.66";
            //string host = "192.168.1.78";
            int port = 6379;
            ConfigurationOptions options = new ConfigurationOptions {
                AllowAdmin = true,
                EndPoints = {new IPEndPoint(IPAddress.Parse(host), port)},
                Password = "abc123"
            };

            ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            IDatabase database = connection.GetDatabase();
            //string val = database.StringGet("name");
            //Console.WriteLine(val);


            //var eds = connection.GetEndPoints();
            //foreach (EndPoint ed in eds) {
            //    Console.WriteLine(ed);
            //}

            var server = connection.GetServer(options.EndPoints[0]);
            //Console.WriteLine(server.Version.ToString());
            IEnumerable<RedisKey> redisKeys = server.Keys();
            foreach (RedisKey key in redisKeys) {
                Console.WriteLine(key);

               // RedisType redisType = database.KeyType(key);

                //HashEntry[] hashEntries = database.HashGetAll(key);
            
                //Console.WriteLine($"{key}----{redisType}");
                //string str = $"{string.Join(",", hashEntries)}";
                //Console.WriteLine(str);
            
                //insert
                //HashEntry[] insertEntries=new HashEntry[2] {
                //    new HashEntry("name","kangwl"), 
                //    new HashEntry("age",12)
                //};
                //database.HashSet("user.121212122", insertEntries);

                //database.ListLeftPush("kkk", "sd");

              //Console.WriteLine($"{key}---{database.StringGet(key)}");
            }
           
            Console.Read();
        }
 

        public class Student { 
            public dynamic Name { get; set; }
            public dynamic Age { get; set; }
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

                Console.WriteLine(ret > 0);

            }
        }
    }
}
