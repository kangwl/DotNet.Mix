using System.Net;
using StackExchange.Redis;

namespace XK.Redis {
    public class RedisHelper {

        public RedisHelper(string host = "192.168.1.78", int port = 6379, string password = "") {
            options = new ConfigurationOptions {
                AllowAdmin = true,
                EndPoints = {new IPEndPoint(IPAddress.Parse(host), port)},
                Password = password
            };

            ServerEndPoint = options.EndPoints[0];
        }

        private ConfigurationOptions options { get; set; }

        private ConnectionMultiplexer connection => ConnectionMultiplexer.Connect(options);


        public EndPoint ServerEndPoint { get; set; }

        /// <summary>
        /// 数据库操作
        /// </summary>
        public IDatabase Db => connection.GetDatabase();

        /// <summary>
        /// Server操作
        /// </summary>
        public IServer Server => connection.GetServer(ServerEndPoint);

    }
}
