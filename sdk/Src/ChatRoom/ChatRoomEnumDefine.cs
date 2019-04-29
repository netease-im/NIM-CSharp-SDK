namespace NIMChatRoom
{
    /// <summary>
    /// 消息属性
    /// </summary>
    public enum NIMChatRoomMsgFeature
    {
        kNIMChatRoomMsgFeatureDefault = 0
    }

    /// <summary>
    /// 聊天室离开原因
    /// </summary>
    public enum NIMChatRoomExitReason
    {
        /// <summary>
        ///自行退出,重登前需要重新请求登录 
        /// </summary>
        kNIMChatRoomExitReasonExit = 0,

        /// <summary>
        ///聊天室已经被解散,重登前需要重新请求登录 
        /// </summary>
        kNIMChatRoomExitReasonRoomInvalid = 1,

        /// <summary>
        ///被管理员踢出,重登前需要重新请求登录 
        /// </summary>
        kNIMChatRoomExitReasonKickByManager = 2,

        /// <summary>
        ///多端被踢 
        /// </summary>
        kNIMChatRoomExitReasonKickByMultiSpot = 3,

        /// <summary>
        ///当前链接状态异常 
        /// </summary>
        kNIMChatRoomExitReasonIllegalState = 4,

        /// <summary>
        ///被加黑了 
        /// </summary>
        kNIMChatRoomExitReasonBeBlacklisted = 5
    }

    /// <summary>
    /// 聊天室链接情况，一般都是有本地网路情况引起
    /// </summary>
    public enum NIMChatRoomLinkCondition
    {
        /// <summary>
        ///链接正常 
        /// </summary>
        kNIMChatRoomLinkConditionAlive = 0,

        /// <summary>
        ///链接失败,sdk尝试重链 
        /// </summary>
        kNIMChatRoomLinkConditionDeadAndRetry = 1,

        /// <summary>
        ///链接失败,开发者需要重新申请聊天室登录信息 
        /// </summary>
        kNIMChatRoomLinkConditionDead = 2
    }

    /// <summary>
    /// 聊天室进入状态
    /// </summary>
    public enum NIMChatRoomEnterStep
    {
        /// <summary>
        /// 本地服务初始化
        /// </summary>
        kNIMChatRoomEnterStepInit = 1,
        /// <summary>
        /// 服务器连接中
        /// </summary>
        kNIMChatRoomEnterStepServerConnecting = 2,
        /// <summary>
        /// 服务器连接结束,连接结果见error_code
        /// </summary>
        kNIMChatRoomEnterStepServerConnectOver = 3,
        /// <summary>
        /// 聊天室鉴权中
        /// </summary>
        kNIMChatRoomEnterStepRoomAuthing = 4,
        /// <summary>
        /// 聊天室鉴权结束,鉴权结果见error_code, error_code非408则需要开发者重新请求聊天室进入信息
        /// </summary>
        kNIMChatRoomEnterStepRoomAuthOver = 5,
    }

    /// <summary>
    /// 代理类型
    /// </summary>
    public enum NIMChatRoomProxyType
    {
        /// <summary>
        /// 不使用代理
        /// </summary>
        kNIMChatRoomProxyNone = 0,
        /// <summary>
        /// HTTP 1.1 Proxy（暂不支持）
        /// </summary>
        kNIMChatRoomProxyHttp11 = 1,
        /// <summary>
        /// Socks4 Proxy
        /// </summary>
        kNIMChatRoomProxySocks4 = 4,
        /// <summary>
        /// Socks4a Proxy
        /// </summary>
        kNIMChatRoomProxySocks4a = 5,
        /// <summary>
        /// Socks5 Proxy
        /// </summary>
        kNIMChatRoomProxySocks5 = 6,
    }
}
