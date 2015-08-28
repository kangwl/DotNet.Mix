using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using XK.Common;

namespace XK.Redis.Extension {
    public static class CommonExtension {

        public static TModel ToModel<TModel>(this RedisValue redisValue) where TModel:class {
            if (redisValue.HasValue) {
                return redisValue.ToString().ToModel<TModel>();
            }
            return default(TModel);
        }
 


    }
}
