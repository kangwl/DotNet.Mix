using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using XK.Common;

namespace XK.Redis.Extension {
    /// <summary>
    /// hash 操作扩展
    /// </summary>
    public static class HashExtension {

        public static bool HashSetModel<TModel>(this IDatabase db, string key, string hashField, TModel value,
            When when = When.Always, CommandFlags flags = CommandFlags.None) where TModel : class, new() {

            return db.HashSet(key, hashField, value.ToJson(), when, flags);
        }

        public static TModel HashGetModel<TModel>(this IDatabase db, string key, string hashField, CommandFlags flags = CommandFlags.None) where TModel:class {
            return db.HashGet(key, hashField, flags).ToModel<TModel>();
        }

        public static void HashSetModels<TModel>(this IDatabase db, string key, Dictionary<string, TModel> dicModels,
            CommandFlags flags = CommandFlags.None) where TModel : class, new() {
            List<HashEntry> hashEntries =
                dicModels.Select(pair => new HashEntry(pair.Key, pair.Value.ToJson())).ToList();

            db.HashSet(key, hashEntries.ToArray(), flags);
        }

        public static List<TModel> HashGetModels<TModel>(this IDatabase db,string key)where TModel:class {

            HashEntry[] hashEntries = db.HashGetAll(key);

            List<TModel> tModels =
                hashEntries.Select(hashEntry => hashEntry.Value)
                    .Select(redisValue => redisValue.ToModel<TModel>())
                    .ToList();
            return tModels;
        }

    }
}
