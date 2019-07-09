using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NimUtility
{
    public class NativeDependentInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }

    /// <summary>
    /// 获取sdk配置
    /// </summary>
    public class ConfigReader
    {
        const string SettingFile = "config.json";

        static NimConfig _defaultConfig;
        public static NimConfig DefaultConfig
        {
            get
            {
                if(_defaultConfig == null)
                {
                    _defaultConfig = new NimConfig();
                    _defaultConfig.AppKey = "45c6af3c98409b18a84451215d0bdd6e";
                    _defaultConfig.CommonSetting = new SdkCommonSetting();
                    _defaultConfig.CommonSetting.LogLevel = SdkLogLevel.Pro;
                }
                return _defaultConfig;
            }
        }

        public static string DefaultChatroomUrl
        {
            get
            {
                return "https://app.netease.im/api/chatroom/homeList";
            }
        }

        private static JObject GetConfigRoot()
        {
            if (!File.Exists(SettingFile))
                return null;
            using (var stream = File.OpenRead(SettingFile))
            {
                StreamReader reader = new StreamReader(stream);
                var content = reader.ReadToEnd();
                reader.Close();
                return JObject.Parse(content);
            }
        }

        private static JToken GetConfigToken()
        {
            var root = GetConfigRoot();
            if (root == null)
                return null;
            var indexToken = root.SelectToken("configs.index");
            int index = indexToken == null ? 0 : indexToken.ToObject<int>();
            var configsToken = root.SelectToken("configs.list");
            if (configsToken == null)
                return null;
            var configList = configsToken.ToArray();
            if (index >= configList.Count())
                return null;
            return configList[index];
        }

        public static string GetSdkVersion()
        {
            var root = GetConfigRoot();
            if (root == null)
                return null;
            var token = root.SelectToken("sdk.version");
            return token.ToObject<string>();
        }

        private static T GetConfigItem<T>(string jsonPath)
        {
            var configToken = GetConfigToken();
            if (configToken == null)
                return default(T);
            var token = configToken.SelectToken(jsonPath);
            return token == null ? default(T) : token.ToObject<T>();
        }

        public static string GetAppKey()
        {
            var appkey = GetConfigItem<string>("app_key");
            return !string.IsNullOrEmpty(appkey) ? appkey : DefaultConfig.AppKey;
        }

        public static string GetChatRommListServerUrl()
        {
            var url = GetConfigItem<string>("roomlistserver");
            return !string.IsNullOrEmpty(url) ? url : DefaultChatroomUrl;
        }

        public static NimConfig GetSdkConfig()
        {
            var configToken = GetConfigToken();
            var cfg = configToken.ToObject<NimConfig>();
            return cfg != null ? cfg : DefaultConfig;
        }
    }
}
