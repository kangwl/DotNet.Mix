﻿using System;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;

namespace XK.Web.WebControls.ueditor.net.App_Code {
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class UEConfig
    {
        private static bool noCache = true;
        private static JObject BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("config.json"));
            return JObject.Parse(json);
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _Items == null)
                {
                    _Items = BuildItems();
                }
                return _Items;
            }
        }
        private static JObject _Items;


        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}