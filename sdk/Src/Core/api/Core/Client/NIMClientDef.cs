/** @file NIMClientDef.cs
  * @brief NIM SDK提供的Client相关定义（如登录、注销、被踢、掉线等功能）
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

//定义NIM Client相关的数据类型
namespace NIM
{
    /// <summary>
    /// Logout类型
    /// </summary>
    public enum NIMLogoutType
    {
        /// <summary>
        /// 注销/切换帐号（返回到登录界面）
        /// </summary>
        kNIMLogoutChangeAccout = 1,

        /// <summary>
        /// 被踢（返回到登录界面）
        /// </summary>
        kNIMLogoutKickout = 2,

        /// <summary>
        /// 程序退出
        /// </summary>
        kNIMLogoutAppExit = 3,

        /// <summary>
        /// 重连操作，包括保存密码时启动程序伪登录后的重连操作以及掉线后的重连操作（帐号未变化）
        /// </summary>
        kNIMLogoutRelogin = 4,
    }

    /// <summary>
    /// 被踢原因
    /// </summary>
    public enum NIMKickReason
    {
        /// <summary>
        /// 互斥类型的客户端互踢
        /// </summary>
        kNIMKickReasonSameGeneric = 1,

        /// <summary>
        /// 服务器端发起踢客户端指令
        /// </summary>
        kNIMKickReasonServerKick = 2,

        /// <summary>
        /// 被自己的其他端踢掉
        /// </summary>
        kNIMKickReasonKickBySelfOtherClient = 3,
    }

    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum NIMClientType
    {
        /// <summary>
        /// Android
        /// </summary>
        kNIMClientTypeAndroid = 1,

        /// <summary>
        /// iOS
        /// </summary>
        kNIMClientTypeiOS = 2,

        /// <summary>
        /// PC Windows
        /// </summary>
        kNIMClientTypePCWindows = 4,

        /// <summary>
        /// Web
        /// </summary>
        kNIMClientTypeWeb = 16,

        /// <summary>
        /// RestAPI 
        /// </summary>
        kNIMClientTypeRestAPI = 32,

        /// <summary>
        /// Mac
        /// </summary>
        kNIMClientTypeMacOS = 64
    }

    /// <summary>
    /// 登录步骤
    /// </summary>
    public enum NIMLoginStep
    {
        /// <summary>
        /// 正在连接
        /// </summary>
        kNIMLoginStepLinking = 0,

        /// <summary>
        /// 连接服务器
        /// </summary>
        kNIMLoginStepLink = 1,

        /// <summary>
        /// 正在登录
        /// </summary>
        kNIMLoginStepLogining = 2,

        /// <summary>
        /// 登录验证
        /// </summary>
        kNIMLoginStepLogin = 3,
    }

    /// <summary>
    /// 多点登录通知类型
    /// </summary>
    public enum NIMMultiSpotNotifyType
    {
        /// <summary>
        /// 通知其他在线端自己登录了
        /// </summary>
        kNIMMultiSpotNotifyTypeImIn = 2,

        /// <summary>
        /// 通知其他在线端自己退出
        /// </summary>
        kNIMMultiSpotNotifyTypeImOut = 3,
    }

    public class NIMMultiClientLoginInfo : NimUtility.NimJsonObject<NIMMultiClientLoginInfo>
    {
        /// <summary>
        /// 第三方账号
        /// </summary>
        [JsonProperty("app_account")]
        public string AppAccount { get; set; }

        /// <summary>
        /// 客户端类型<see cref="NIMClientType"/>
        /// </summary>
        [JsonProperty("client_type")]
        public NIMClientType ClientType { get; set; }

        /// <summary>
        /// 登录系统类型,比如ios 6.0.1 
        /// </summary>
        [JsonProperty("client_os")]
        public string OperateSystem { get; set; }

        /// <summary>
        /// 登录设备的mac地址
        /// </summary>
        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        /// <summary>
        /// 设备id，uuid
        /// </summary>
        [JsonProperty("device_id")]
        public string DeviceID { get; set; }

        /// <summary>
        /// 本次登陆时间, 精度到ms
        /// </summary>
        [JsonProperty("login_time")]
        public long LoginTimeStamp { get; set; }

        /// <summary>
        /// 本次登录用户自定义字段
        /// </summary>
        [JsonProperty("custom_tag")]
        public string CustomTag { get; set; }
    }

    public class NIMLoginResult : NimUtility.NimJsonObject<NIMLoginResult>
    {
        /// <summary>
        /// 返回的错误码
        /// </summary>
        [JsonProperty("err_code")]
        public ResponseCode Code { get; set; }

        /// <summary>
        /// 是否为重连过程
        /// </summary>
        [JsonProperty("relogin")]
        public bool IsRelogin { get; set; }

        /// <summary>
        /// 登录步骤
        /// </summary>
        [JsonProperty("login_step")]
        public NIMLoginStep LoginStep { get; set; }

        /// <summary>
        /// 其他端的在线状态列表，登录成功才会返回这部分内容
        /// </summary>
        [JsonProperty("other_clients_pres")]
        public List<NIMMultiClientLoginInfo> LoginedClients { get; set; }
    }

    /// <summary>
    /// 登录状态
    /// </summary>
    public enum NIMLoginState
    {
        /// <summary>
        /// 已登录
        /// </summary>
        kNIMLoginStateLogin = 1,

        /// <summary>
        /// 未登录
        /// </summary>
        kNIMLoginStateUnLogin = 2
    }

    public class NIMLogoutResult : NimUtility.NimJsonObject<NIMLogoutResult>
    {
        /// <summary>
        /// 返回的错误码
        /// </summary>
        [JsonProperty("err_code")]
        public ResponseCode Code { get; set; }
    }

    public class NIMKickoutOtherDeviceInfo : NimUtility.NimJsonObject<NIMKickoutOtherDeviceInfo>
    {
        [JsonProperty("device_ids")]
        public List<string> DeviceIDs { get; set; }
    }

    public class NIMKickoutResult : NimUtility.NimJsonObject<NIMKickoutResult>
    {
        /// <summary>
        /// 客户端类型NIMClientType
        /// </summary>
        [JsonProperty("client_type")]
        public NIMClientType ClientType { get; set; }

        /// <summary>
        /// 返回的被踢原因NIMKickReason
        /// </summary>
        [JsonProperty("reason_code")]
        public NIMKickReason KickReason { get; set; }
    }

    public class NIMMultiSpotLoginNotifyResult : NimUtility.NimJsonObject<NIMMultiSpotLoginNotifyResult>
    {
        /// <summary>
        /// 客户端类型NIMClientType
        /// </summary>
        [JsonProperty("multi_spot_notiy_type")]
        public NIMMultiSpotNotifyType NotifyType { get; set; }

        /// <summary>
        /// 其他端的在线状态列表，登录成功才会返回这部分内容
        /// </summary>
        [JsonProperty("other_clients_pres")]
        public List<NIMMultiClientLoginInfo> OtherClients { get; set; }
    }

    public class NIMKickOtherResult : NimUtility.NimJsonObject<NIMKickOtherResult>
    {
        /// <summary>
        /// 返回的错误码
        /// </summary>
        [JsonProperty("err_code")]
        public ResponseCode ResCode { get; set; }

        /// <summary>
        /// 设备id，uuid
        /// </summary>
        [JsonProperty("device_ids")]
        public List<string> DeviceIDs { get; set; }
    }

    public class LoginResultEventArgs : EventArgs
    {
        public NIMLoginResult LoginResult { get; private set; }

        public LoginResultEventArgs(NIMLoginResult result)
        {
            LoginResult = result;
        }
    }

    public class ConfigMultiportPushParam : NimUtility.NimJsonObject<ConfigMultiportPushParam>
    {
        //1开启，即桌面端在线时移动端不需推送；2关闭，即桌面端在线时移动端需推送 */
        [JsonProperty("switch_open")]
        private int _enabled { get; set; }

        [JsonIgnore]
        public bool Enabled
        {
            get { return _enabled != 1; }
            set { _enabled = value ? 2 : 1; }
        }
    }

    /// <summary>
    /// 客户端传入的属性（如果开启免打扰，请让第三方确保把时间转成东八区，即北京时间，小时是24小时制)
    /// </summary>
    public class DndConfigParam : NimUtility.NimJsonObject<DndConfigParam>
    {
        /// <summary>
        /// 是否显示详情，1显示详情，2不显示详情，其它按1处理
        /// </summary>
        [JsonProperty("show_detail")]
        private int _showDetail { get; set; }

        /// <summary>
        /// 是否开启免打扰，1开启，2关闭，其它按2处理
        /// </summary>
        [JsonProperty("switch_open")]
        private int _isOpened { get; set; }

        /// <summary>
        /// 如果开启免打扰，开始小时数
        /// </summary>
        [JsonProperty("fromh")]
        public int FromHours { get; set; }

        /// <summary>
        /// 如果开启免打扰，开始分钟数
        /// </summary>
        [JsonProperty("fromm")]
        public int FromMinutes { get; set; }

        /// <summary>
        /// 如果开启免打扰，截止小时数
        /// </summary>
        [JsonProperty("toh")]
        public int ToHours { get; set; }

        /// <summary>
        /// 如果开启免打扰，截止分钟数
        /// </summary>
        [JsonProperty("tom")]
        public int ToMunutes { get; set; }

        [JsonIgnore]
        public bool ShowDetail
        {
            get { return _showDetail != 2; }
            set { _showDetail = value ? 1 : 2; }
        }

        [JsonIgnore]
        public bool IsOpened
        {
            get { return _isOpened == 1; }
            set { _isOpened = value ? 1 : 2; }
        }
    }
}
