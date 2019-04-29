/** @file NimConfig.cs
  * @brief NIM SDK提供的SDK配置定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System.Collections.Generic;

namespace NimUtility
{
    /// <summary>
    /// SDK log级别，级别越高，log越详细 
    /// </summary>
    public enum SdkLogLevel
    {
        /// <summary>
        /// SDK Fatal级别Log
        /// </summary>
        Fatal = 1,

        /// <summary>
        /// SDK Error级别Log
        /// </summary>
        Error = 2,

        /// <summary>
        /// SDK Warn级别Log
        /// </summary>
        Warn = 3,

        /// <summary>
        /// 应用级别Log，正式发布时为了精简sdk log，可采用此级别
        /// </summary>
        App = 5,

        /// <summary>
        /// SDK调试过程级别Log，更加详细，更有利于开发调试
        /// </summary>
        Pro = 6
    };

    public class SdkCommonSetting
    {
        /// <summary>
        /// 数据库秘钥，目前只支持最多32个字符的加密密钥！建议使用32个字符
        /// </summary>
        [Newtonsoft.Json.JsonProperty("db_encrypt_key")]
        public string DataBaseEncryptKey { get; set; }

        /// <summary>
        /// 必填，是否需要预下载附件缩略图，默认为true
        /// </summary>
        [Newtonsoft.Json.JsonProperty("preload_attach")]
        public bool PredownloadAttachmentThumbnail { get; set; }

        /// <summary>
        /// 定义见SdkLogLevel选填，SDK默认的内置级别为Pro
        /// </summary>
        [Newtonsoft.Json.JsonProperty("sdk_log_level")]
        public SdkLogLevel LogLevel { get; set; }

        /// <summary>
        /// 选填，是否使用私有服务器
        /// </summary>
        [Newtonsoft.Json.JsonProperty("private_server_setting")]
        public bool UsePriviteServer { get; set; }

        /// <summary>
        /// 登录超时，单位秒，默认30
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_timeout")]
        public int CustomTimeout { get; set; }

        /// <summary>
        /// 登录重试最大次数，如需设置建议设置大于3次
        /// </summary>
        [Newtonsoft.Json.JsonProperty("login_retry_max_times")]
        public int MaxLoginRetry { get; set; }

#if UNITY_IPHONE || UNITY_IOS || UNITY_ANDROID
		/// <summary>
		/// iOS 推送证书名配置
		/// </summary>
		[Newtonsoft.Json.JsonProperty("push_cer_name")]
		public string PushCerName { get; set;}
#endif
        /// <summary>
        /// 预下载图片质量,选填,范围0-100
        /// </summary>
        [Newtonsoft.Json.JsonProperty("preload_image_quality")]
        public int PreloadImageQuality { get; set; }

        /// <summary>
        /// 预下载图片基于长宽做内缩略,选填,比如宽100高50,则赋值为100x50,中间为字母小写x 
        /// </summary>
        [Newtonsoft.Json.JsonProperty("preload_image_resize")]
        public string PreloadImageResize { get; set; }

        /// <summary>
        /// 设置是否已读未读状态多端同步，默认true
        /// </summary>
        [Newtonsoft.Json.JsonProperty("sync_session_ack")]
        public bool SyncSessionAck { get; set; }
#if !NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 设置是否不保存自定义(kNIMMessageTypeCustom)消息（对PC版本SDK不支持，仅对Unity、cocos版本有效），默认为false
        /// </summary>
        [Newtonsoft.Json.JsonProperty("not_need_save_custom_msg")]
        public bool NotNeedSaveCustomMsg { get; set; }

        /// <summary>
        /// 选填，消息重发队列容量限制,仅对Unity版本有效
        /// </summary>
        public int ResendListCapacity { get; set; }

         /// <summary>
        /// nos 下载地址拼接模板，用于拼接最终得到的下载地址
        /// </summary>
        [Newtonsoft.Json.JsonProperty("download_address_template")]
        public string[] DownloadAddrTemplate { get; set; }

        /// <summary>
        /// 需要被加速的主机名
        /// </summary>
        [Newtonsoft.Json.JsonProperty("accelerate_host")]
        public string[] AccelerateHost { get; set; }

        /// <summary>
        /// nos 加速地址拼接模板，用于获得加速后的下载地址
        /// </summary>
        [Newtonsoft.Json.JsonProperty("accelerate_address_template")]
        public string[] AccelerateAddrTemplate { get; set; }
#endif

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 是否启用HTTPS协议，默认为false
        /// </summary>
        [Newtonsoft.Json.JsonProperty("use_https")]
        public bool UseHttps { get; set; }

        /// <summary>
        /// 群通知是否计入未读数，默认为false
        /// </summary>
        [Newtonsoft.Json.JsonProperty("team_notification_unread_count")]
        public bool CountingTeamNotification { get; set; }

        /// <summary>
        /// 开启对动图缩略图的支持
        /// </summary>
        [Newtonsoft.Json.JsonProperty("animated_image_thumbnail_enabled")]
        public bool AnimatedImageEnabled { get; set; }

        /// <summary>
        /// 客户端反垃圾，默认为false，如需开启请提前咨询技术支持或销售
        /// </summary>
        [Newtonsoft.Json.JsonProperty("client_antispam")]
        public bool ClientAntiSpam { get; set; }

        /// <summary>
        /// 群消息已读功能开关，默认为false，如需开启请提前咨询技术支持或销售
        /// </summary>
        [Newtonsoft.Json.JsonProperty("team_msg_ack")]
        public bool TeamMsgAckEnabled { get; set; }

        /// <summary>
        /// 是否开启用户数据备份(本地)功能  缺省true
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enable_user_datafile_backup")]
        public bool AppDataBackup { get; set; }

        /// <summary>
        /// 用户数据文件备份（本地）目录，缺省在数据文件所在目录创建一个db_file.back目录
        /// </summary>
        [Newtonsoft.Json.JsonProperty("user_datafile_localbackup_folder")]
        public string DataBackupPath { get; set; }

        /// <summary>
        /// 是否开启用户数据恢复(本地)功能  缺省false
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enable_user_datafile_restore")]
        public bool AppDataRestore { get; set; }

        /// <summary>
        /// 是否使用缺省的用户数据恢复(本地)方案  缺省false AppDataRestore == true 生效
        /// </summary>
        [Newtonsoft.Json.JsonProperty("enable_user_datafile_defrestoreproc")]
        public bool UseDefaultRestore { get; set; }

        /// <summary>
        /// 是否开启缓存式“已接收回执”发送，程序可能收到大量消息以至触发频控时可以考虑开启此开关 缺省 false 关闭
        /// </summary>
        [Newtonsoft.Json.JsonProperty("caching_markread_enabled")]
        public bool CachingMarkreadEnabled { get; set; }

        /// <summary>
        /// caching_markread_ == true 时有效 缓存时间 单位ms 缺省 1000
        /// </summary>
        [Newtonsoft.Json.JsonProperty("caching_markread_time")]
        public int CachingMarkreadTime { get; set; }

        /// <summary>
        /// caching_markread_ == true 时有效 缓存的最大消息条数  缺省 10
        /// </summary>
        [Newtonsoft.Json.JsonProperty("caching_markread_count")]
        public int CachingMarkreadCount { get; set; }

        /// <summary>
        /// 撤回消息是否重新计算未读消息计数
        /// </summary>
        [Newtonsoft.Json.JsonProperty("reset_unread_count_when_recall")]
        public bool ResetUnreadCountWhenRecall { get; set; }

#endif



        public SdkCommonSetting()
        {
            PredownloadAttachmentThumbnail = true;
            UsePriviteServer = false;
            LogLevel = SdkLogLevel.App;
            PreloadImageQuality = -1;
            SyncSessionAck = true;
#if !NIMAPI_UNDER_WIN_DESKTOP_ONLY
			NotNeedSaveCustomMsg = false;
#endif
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY            
            CustomTimeout = 30;
            UseHttps = false;
            AnimatedImageEnabled = false;
            AppDataBackup = true;
            AppDataRestore = false;
            UseDefaultRestore = false;
            ResetUnreadCountWhenRecall = false;
#endif
        }
    }

    public class SdkPrivateServerSetting
    {
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY 
        /// <summary>
        /// lbs地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("lbs")]
        public string LbsAddress { get; set; }

        /// <summary>
        /// nos lbs地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_lbs")]
        public string NOSLbsAddress { get; set; }

        /// <summary>
        /// 默认link服务器地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("link")]
        public string LinkServer { get; set; }

        /// <summary>
        /// 默认nos 上传服务器地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_uploader")]
        public string NosUploadServer { get; set; }

        /// <summary>
        /// （默认nos 上传服务器主机地址，仅 kNIMUseHttps设置为true 时有效，用作 https 上传时的域名校验及 http header host 字段填充）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_uploader_host")]
        public string NosUploadHost { get; set; }

        /// <summary>
        /// RSA public key，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("module")]
        public string RSAPublicKey { get; set; }

        /// <summary>
        /// RSA version，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("version")]
        public int RsaVersion { get; set; }

        /// <summary>
        /// 下载地址拼接模板，用于拼接最终得到的下载地址
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_downloader")]
        public string DownloadAddrTemplate { get; set; }

        /// <summary>
        /// 需要被加速主机名
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_accelerate_host")]
        public string AccelerateHost { get; set; }

        /// <summary>
        /// 加速地址拼接模板，用于获得加速后的下载地址
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_accelerate")]
        public string AccelerateTemplate { get; set; }

        /// <summary>
        /// 部分 IM 错误信息统计上报地址
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nt_server")]
        public string ErrorReportServer { get; set; }

        /// <summary>
        /// 错误信息统计是否上报,私有化如果不上传相应数据，此项配置应为false
        /// </summary>
        [Newtonsoft.Json.JsonProperty("is_upload_statistics_data")]
        public bool UploadStatisticsData { get; set; }

    
#else
        /// <summary>
        /// lbs地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("lbs")]
        public string LbsAddress { get; set; }

        /// <summary>
        /// nos lbs地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("nos_lbs")]
        public string NOSLbsAddress { get; set; }

        /// <summary>
        /// 默认link服务器地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("default_link")]
        public List<string> LinkServerList { get; set; }

        /// <summary>
        /// 默认nos 上传服务器地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("default_nos_upload")]
        public List<string> UploadServerList { get; set; }

        /// <summary>
        /// 默认nos 下载服务器地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("default_nos_download")]
        public List<string> DownloadServerList { get; set; }

        /// <summary>
        /// 默认nos access服务器地址，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("default_nos_access")]
        public List<string> AccessServerList { get; set; }

        /// <summary>
        /// RSA public key，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("rsa_public_key_module")]
        public string RSAPublicKey { get; set; }

        /// <summary>
        /// RSA version，如果选择使用私有服务器，则必填
        /// </summary>
        [Newtonsoft.Json.JsonProperty("rsa_version")]
        public int RsaVersion { get; set; }
#endif
        public SdkPrivateServerSetting()
        {
            RsaVersion = 0;

        }
    }

    public class NimConfig : NimUtility.NimJsonObject<NimConfig>
    {
        [Newtonsoft.Json.JsonProperty("global_config")]
        public SdkCommonSetting CommonSetting { get; set; }

        /// <summary>
        /// 私有服务器配置（一旦设置了私有服务器，则全部连私有服务器，必须确保配置正确！
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "private_server_setting", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public SdkPrivateServerSetting PrivateServerSetting { get; set; }

        /// <summary>
        /// AppKey
        /// </summary>
        [Newtonsoft.Json.JsonProperty("app_key")]
        public string AppKey { get; set; }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 私有云服务器相关地址配置文件本地绝对路径，如果不填默认执行文件目录下的nim_server.conf
        /// </summary>
        [Newtonsoft.Json.JsonProperty("server_conf_file_path")]
        public string ServerConfFilePath { get; set; }
#endif

        public bool IsValiad()
        {
            return CommonSetting != null;
        }
    }
}
