using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using XK.Common;

namespace XK.Redis.Extension {
    public static class StringExtension {

        public static bool StringSetModel<TModel>(this IDatabase db, string key, TModel tModel,
            TimeSpan? expiry = default(TimeSpan?), When when = When.Always, CommandFlags flags = CommandFlags.None)
            where TModel : class, new() {
            string json = tModel.ToJson();
            return db.StringSet(key, json, expiry, when, flags);
        }

        public static TModel StringGetModel<TModel>(this IDatabase db, string key,
            CommandFlags flags = CommandFlags.None) where TModel : class {

            RedisValue redisValue = db.StringGet(key, flags);

            return redisValue.ToModel<TModel>();
        }
        


    }
}
