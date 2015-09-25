using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FreedomDB.Bridge;
using XK.Redis;

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

            //RedisHelper redisHelper = new RedisHelper();
            //redisHelper.Db.SortedSetRangeByScoreModels<Student>("users_sort").ForEach(
            //    one => Console.WriteLine(one.Name)
            //    ); 

            //DapperTest();

            // A();
            //XK.Redis.RedisHelper.HashGetMuti("user.767474055", "name", "age", "sex");
            //Dictionary<string, string> dic = XK.Redis.RedisHelper.HashGetDic("user.767474055", "name", "age", "sex");
            //foreach (KeyValuePair<string, string> pair in dic) {
            //    Console.WriteLine(pair.Key + " : " + pair.Value);
            //}
            //var users = XK.Redis.RedisHelper.ListRange("users",0,2);
            //foreach (string user in users) {
            //    Console.WriteLine(user);
            //}

            string hashKey = "user.767474055";
          //var d=  RedisHelper.HashIncrement(hashKey, "name", 3D);
          //  Console.WriteLine(d);
 
           // string sortedSetRankKey = "user.score.rank";
            //var removeCount = RedisHelper.SortedSetIncrement(sortedSetRankKey, "xiaoming", 12);
           // Console.WriteLine(removeCount);
            //bool success = RedisHelper.SortedSetAdd(sortedSetRankKey, new Dictionary<string, double>() {
            //    {"xiaoming", 90},
            //    {"xiaokang", 100},
            //    {"xiaosan", 80},
            //    {"xiaoqiang", 88}
            //});
            //Console.WriteLine(success);
            // RedisHelper.SortedSetAdd(sortedSetRankKey, "xiaoming", 90);
            //RedisHelper.SortedSetAdd(sortedSetRankKey, "xiaoming1", 50);
            //RedisHelper.SortedSetAdd(sortedSetRankKey, "xiaoming2", 100);
            //var len = RedisHelper.SortedSetRemove(sortedSetRankKey, new List<string>() {"xiaoming1", "xiaoming2"});
            //Console.WriteLine(len);
            //bool success = RedisHelper.SortedSetRemove(sortedSetRankKey, "xiaoming");
            // Console.WriteLine(success);
         //   IEnumerable<string> entities = RedisHelper.SortedSetRangeByScore(sortedSetRankKey,false);
          //  foreach (string entity in entities) {
          //      Console.WriteLine(entity);
          //  }
            Console.Read(); 
        }

        private async static void A() {
            //Stopwatch stopwatch=new Stopwatch();
            //stopwatch.Start();
            //Task<bool> t = userDal.Insert(
            //    new Test.Model.User {
            //        UserID = "kangwl",
            //        UserPass = "1234",
            //        Name = "weeee",
            //        Age = 22,
            //        Birthday = DateTime.Now.AddYears(-10)
            //    }); 
            //Console.WriteLine(t.Result);

            //var update= userDal.Update(new Update() {
            //     Dic = new Dictionary<string, dynamic> { {"UserID","qqq"} },
            //     WhereCore = new Where().Add(new Where.Item("ID", "=", 2))
            //                            .Add(new Where.Item("Name", "=", "weeee"))
            // }); 
            //Console.WriteLine(update.Result);


            //var delete = userDal.Delete(new Where(
            //    new Where.Item("ID", "=", 2)
            //    )
            //    );
            //Console.WriteLine(delete.Result);

            //Where.Item item = new Where.Item("ID", "=", 1);
            //var user = userDal.GetOne(new Where(item), "*");
            //Console.WriteLine(user.Result.Name); 
            //List<Test.Model.User> userTask =await userDal.GetList("", "", 1, 1);

           // var taskelist = await UserBll.GetListUser(new Where().Add(new Where.Item("ID", ">", "0")), "ID,Name,Age", "ID DESC", 1, 2);
            //taskelist.ForEach(one => Console.WriteLine(one.Name));

            //int ret = await userDal.InsertBatch(new List<User>() {
            //    new User() {
            //        UserID = "aaa",
            //        UserPass = "1234",
            //        Name = "weeee",
            //        Age = 22,
            //        Birthday = DateTime.Now.AddYears(-10)
            //    },
            //    new User() {
            //        UserID = "bbb",
            //        UserPass = "3344",
            //        Name = "dfeee",
            //        Age = 33,
            //        Birthday = DateTime.Now.AddYears(-10)
            //    }
            //}, true);

            //Console.WriteLine(ret);
        }

     


        public class Student { 
            public int ID { get; set; }
            public dynamic Name { get; set; }
            public dynamic Age { get; set; }
        }
 




    }
}
