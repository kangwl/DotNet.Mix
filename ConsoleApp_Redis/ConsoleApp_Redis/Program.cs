using System;
using FreedomDB.Bridge;
using MyTestShop.Bll;
using XK.Common;

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

            A();
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

            // var taskelist =await UserBll.GetListUser(new Where().Add(new Where.Item("ID", ">", "0")), "ID,Name,Age", "ID DESC", 1, 2);
            // taskelist.ForEach(one => Console.WriteLine(one.Name));

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
