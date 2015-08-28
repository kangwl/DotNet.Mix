using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using StackExchange.Redis;
using XK.Common;
using XK.Redis;
using XK.Redis.Extension;

namespace ConsoleApp_Redis {
    class Program {
        private static void Main(string[] args) {

            // string host = "192.168.3.66";
            //string host = "192.168.1.78";
            //int port = 6379;
            //ConfigurationOptions options = new ConfigurationOptions {
            //    AllowAdmin = true,
            //    EndPoints = {new IPEndPoint(IPAddress.Parse(host), port)},
            //    Password = ""
            //};

            //ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
            //IDatabase database = connection.GetDatabase();
            ////string val = database.StringGet("name");
            ////Console.WriteLine(val);


            ////var eds = connection.GetEndPoints();
            ////foreach (EndPoint ed in eds) {
            ////    Console.WriteLine(ed);
            ////}

            //var server = connection.GetServer(options.EndPoints[0]);

            ////database.ListRangeModels<Student>("users").OrderBy(stu=>stu.ID).ToList().ForEach(stu=>Console.WriteLine(stu.ID));
            ////long len = database.ListLeftPushModel("users", new Student() {ID = 12, Name = "kwl", Age = 12});

            ////Console.WriteLine(len);
            //Dictionary<Student,double> dic=new Dictionary<Student, double>() {
            //    {new Student() {ID = 1,Name = "xk",Age = 12}, 92 },
            //    {new Student() {ID = 1,Name = "xk1",Age = 13}, 88 },
            //    {new Student() {ID = 1,Name = "xk2",Age = 12}, 56 }
            //};
            ////Console.WriteLine(database.SortedSetAddModels("users_sort", dic));
            ////database.SortedSetRangeByScoreModels<Student>("users_sort").ForEach(s=>Console.WriteLine(s.Name));
            ////Dictionary<Student, double> dicrange = database.SortedSetRangeByRankWithScoresModels<Student>("users_sort",0L,-1L,Order.Descending);
            ////foreach (KeyValuePair<Student, double> pair in dicrange) {
            ////    Console.WriteLine(pair.Key.Name + "--" + pair.Value);
            ////}

            RedisHelper redisHelper = new RedisHelper();
            redisHelper.Db.SortedSetRangeByScoreModels<Student>("users_sort").ForEach(
                one => Console.WriteLine(one.Name)
                );
            
            Console.Read();
        }


        public class Student { 
            public int ID { get; set; }
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
