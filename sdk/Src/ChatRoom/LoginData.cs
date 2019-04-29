using Newtonsoft.Json;

namespace NIMChatRoom
{
    /// <summary>
    /// 聊天室登录信息
    /// </summary>
    public class LoginData : NimUtility.NimJsonObject<LoginData>
    {
        /// <summary>
        /// 进入聊天室后展示的昵称,选填
        /// </summary>
        [JsonProperty("nick")]
        public string Nick { get; set; }

        /// <summary>
        /// 进入聊天室后展示的头像,选填
        /// </summary>
        [JsonProperty("avatar")]
        public string Icon { get; set; }

        /// <summary>
        /// 聊天室可用的扩展字段,选填
        /// </summary>
        [JsonProperty("ext")]
        public string Extension { get; set; }

        /// <summary>
        /// 进入聊天室通知开发者扩展字段
        /// </summary>
        [JsonProperty("notify_ext")]
        public NimUtility.Json.JsonExtension NotifyExtension { get; set; }
    }

    /// <summary>
    /// 聊天室登录状态
    /// </summary>
    public enum NIMChatRoomLoginStep
    {
        /// <summary>
        ///本地服务初始化 
        /// </summary>
        kNIMChatRoomLoginStepInit = 1,

        /// <summary>
        ///服务器连接中 
        /// </summary>
        kNIMChatRoomLoginStepServerConnecting = 2,

        /// <summary>
        ///服务器连接结束,连接结果error_code 
        /// </summary>
        kNIMChatRoomLoginStepServerConnectOver = 3,

        /// <summary>
        ///聊天室鉴权中 
        /// </summary>
        kNIMChatRoomLoginStepRoomAuthing = 4,

        /// <summary>
        ///聊天室鉴权结束,鉴权结果见error_code, error_code非408则需要开发者重新请求聊天室登录信息 
        /// </summary>
        kNIMChatRoomLoginStepRoomAuthOver = 5,
    }


    public class AnonymousInfo : NimUtility.NimJsonObject<AnonymousInfo>
    {
        /// <summary>
        /// 应用appkey
        /// </summary>
        [JsonProperty("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 本地缓存目录
        /// </summary>
        [JsonProperty("app_data_path")]
        public string AppDataPath { get; set; }

        /// <summary>
        /// 日志等级(1-6)
        /// </summary>
        [JsonProperty("log_level")]
        public int LogLevel { get; set; }

        /// <summary>
        /// 聊天室地址
        /// </summary>
        [JsonProperty("address")]
        public string[] LinkAddrs { get; set; }


        public AnonymousInfo()
        {
            LogLevel = 5;
        }
    }

    public class PrivateSetting : NimUtility.NimJsonObject<PrivateSetting>
    {
        /// <summary>
        ///获取link server的应用服务器地址(仅供demo测试使用，用户需要自己的应用服务器来获取聊天室地址)
        /// </summary>
        [JsonProperty("lbs")]
        public string LbsSerer { get; set; }

        /// <summary>
        /// RSA RSAPublicKey，如果选择私有化服务，则必填，须与IM配置保持一致
        /// </summary>
        [JsonProperty("rsa_public_key_module")]
        public string RSAPublicKey { get; set; }

        /// <summary>
        /// RSA version，如果选择私有化服务，则必填，须与IM配置保持一致
        /// </summary>
        [JsonProperty("rsa_version")]
        public int RSAVersion { get; set; }

        public PrivateSetting()
        {
            RSAVersion = 0;
        }
    }
    
    /// <summary>
    /// 暂时仅应用于匿名登录方式的配置
    /// </summary>
	public class NIMChatRoomConfig : NimUtility.NimJsonObject<NIMChatRoomConfig>
    {
        /// <summary>
        /// 应用appkey，须与IM配置保持一致
        /// </summary>
        [JsonProperty("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 私有化配置
        /// </summary>
        [JsonProperty("private_server_setting")]
        public PrivateSetting privateSetting { get; set; }
    }
      
}
