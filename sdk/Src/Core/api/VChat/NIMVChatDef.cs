/** @file NIMVChatDef.cs
  * @brief NIM VChat提供的音视频接口定义，
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author gq
  * @modify leewp
  * @date 2015/12/8
  */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NIM
{
	/// <summary>
	/// NIMVideoChatSessionType 音视频通话状态通知类型 
	/// </summary>
	public enum NIMVideoChatSessionType
	{
		/// <summary>
		/// 创建通话结果
		/// </summary>
		kNIMVideoChatSessionTypeStartRes = 1,
		/// <summary>
		/// 通话邀请
		/// </summary>
		kNIMVideoChatSessionTypeInviteNotify = 2,
		/// <summary>
		/// 确认通话，接受拒绝结果
		/// </summary>
		kNIMVideoChatSessionTypeCalleeAckRes = 3,
		/// <summary>
		/// 确认通话，接受拒绝通知
		/// </summary>
		kNIMVideoChatSessionTypeCalleeAckNotify = 4,
		/// <summary>
		/// NIMVChatControlType 结果
		/// </summary>
		kNIMVideoChatSessionTypeControlRes = 5,
		/// <summary>
		/// NIMVChatControlType 通知
		/// </summary>
		kNIMVideoChatSessionTypeControlNotify = 6,
		/// <summary>
		/// 通话中链接状态通知
		/// </summary>
		kNIMVideoChatSessionTypeConnect = 7,
		/// <summary>
		/// 通话中成员状态
		/// </summary>
		kNIMVideoChatSessionTypePeopleStatus = 8,
		/// <summary>
		/// 通话中网络状态
		/// </summary>
		kNIMVideoChatSessionTypeNetStatus = 9,
		/// <summary>
		/// 通话挂断结果
		/// </summary>
		kNIMVideoChatSessionTypeHangupRes = 10,
		/// <summary>
		/// 通话被挂断通知
		/// </summary>
		kNIMVideoChatSessionTypeHangupNotify = 11,
		/// <summary>
		/// 通话接听挂断同步通知
		/// </summary>
		kNIMVideoChatSessionTypeSyncAckNotify = 12,
        /// <summary>
        /// 通知MP4录制状态，包括开始录制和结束录制
        /// </summary>
        kNIMVideoChatSessionTypeMp4Notify = 13,
        /// <summary>
        /// 通知实时音视频数据状态
        /// </summary>
        kNIMVideoChatSessionTypeInfoNotify = 14,
        /// <summary>
        /// 通知实时音频发送和混音的音量状态
        /// </summary>
        kNIMVideoChatSessionTypeVolumeNotify = 15,
        /// <summary>
        /// 通知音频录制状态，包括开始录制和结束录制
        /// </summary>
        kNIMVideoChatSessionTypeAuRecordNotify = 16,
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
		/// <summary>
		/// 通知直播推流的服务器状态
		/// </summary>
		kNIMVideoChatSessionTypeLiveState = 17,
#else
        /// <summary>
        /// 通话中伴音事件状态通知,Untiy aos,ios有效
        /// </summary>
        kNIMVideoChatSessionTypeAuMixingEventNotify = 30,
        /// <summary>
        /// 客户端网络类型发生了变化,Untiy aos,ios有效
        /// </summary>
        kNIMVideoChatSessionTypeNetConnectionTypeChangedNotify = 32,
        /// <summary>
        /// 语音静音状态,Untiy aos,ios有效
        /// </summary>
        kNIMVideoChatSessionTypeAudioMuteStatus = 34,
#endif
    };

	/// <summary>
	/// 音视频通话控制类型
	/// </summary>
	public enum NIMVChatControlType
	{
		/// <summary>
		/// 打开音频
		/// </summary>
		kNIMTagControlOpenAudio = 1,
		/// <summary>
		/// 关闭音频
		/// </summary>
		kNIMTagControlCloseAudio = 2,
		/// <summary>
		/// 打开视频
		/// </summary>
		kNIMTagControlOpenVideo = 3,
		/// <summary>
		/// 关闭视频
		/// </summary>
		kNIMTagControlCloseVideo = 4,
		/// <summary>
		/// 请求从音频切换到视频
		/// </summary>
		kNIMTagControlAudioToVideo = 5,
		/// <summary>
		/// 同意从音频切换到视频
		/// </summary>
		kNIMTagControlAgreeAudioToVideo = 6,
		/// <summary>
		/// 拒绝从音频切换到视频
		/// </summary>
		kNIMTagControlRejectAudioToVideo = 7,
		/// <summary>
		/// 从视频切换到音频
		/// </summary>
		kNIMTagControlVideoToAudio = 8,
		/// <summary>
		/// 占线
		/// </summary>
		kNIMTagControlBusyLine = 9,
		/// <summary>
		/// 告诉对方自己的摄像头不可用
		/// </summary>
		kNIMTagControlCamaraNotAvailable = 10,
		/// <summary>
		/// 告诉对方自已处于后台
		/// </summary>
		kNIMTagControlEnterBackground = 11,
		/// <summary>
		/// 告诉发送方自己已经收到请求了（用于通知发送方开始播放提示音）
		/// </summary>
		kNIMTagControlReceiveStartNotifyFeedback = 12,
		/// <summary>
		/// 告诉发送方自己开始录制 
		/// </summary>
		kNIMTagControlMp4StartRecord = 13,
		/// <summary>
		/// 告诉发送方自己结束录制
		/// </summary>
		kNIMTagControlMp4StopRecord = 14,
	};

	/// <summary>
	/// 音视频通话类型
	/// </summary>
	public enum NIMVideoChatMode
	{
		/// <summary>
		/// 语音通话模式
		/// </summary>
		kNIMVideoChatModeAudio = 1,
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
		/// <summary>
		/// 视频通话模式
		/// </summary>
		kNIMVideoChatModeVideo = 2,
#endif
	};

	/// <summary>
	/// 音视频通话成员变化类型
	/// </summary>
	public enum NIMVideoChatSessionStatus
	{
		/// <summary>
		/// 成员进入
		/// </summary>
		kNIMVideoChatSessionStatusJoined = 0,
		/// <summary>
		/// 成员离开
		/// </summary>
		kNIMVideoChatSessionStatusLeaved = 1,
        /// <summary>
        /// 成员超时掉线
        /// </summary>
        kNIMVideoChatSessionStatusTimeOutLeaved = 2,
    };

	/// <summary>
	/// 音视频通话网络变化类型
	/// </summary>
	public enum NIMVideoChatSessionNetStat
	{
        /// <summary>
        /// 网络状态极差，视频强制关闭
        /// </summary>
        kNIMVChatNetStatusVideoClose = -1,
        /// <summary>
        /// 网络状态较好
        /// </summary>
        kNIMVideoChatSessionNetStatGood = 0,
        /// <summary>
        /// 网络状态较差
        /// </summary>
        kNIMVideoChatSessionNetStatPoor = 1,
        /// <summary>
        /// 网络状态很差
        /// </summary>
        kNIMVideoChatSessionNetStatBad = 2,
    };
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
	/// <summary>
	/// 视频通话分辨率，最终长宽比不保证
	/// </summary>
	public enum NIMVChatVideoQuality
	{
		/// <summary>
		/// 视频默认分辨率 480x320
		/// </summary>
		kNIMVChatVideoQualityNormal = 0,
		///<summary>
		///视频低分辨率176x144
		/// </summary>       
		kNIMVChatVideoQualityLow = 1,
		///<summary>
		///视频中分辨率 352x288
		/// </summary>      
		kNIMVChatVideoQualityMedium = 2,
		///<summary>
		///视频高分辨率 480x320
		/// </summary>    
		kNIMVChatVideoQualityHigh = 3,
		///<summary>
		///视频超高分辨率 640x480
		/// </summary>       
		kNIMVChatVideoQualitySuper = 4,
        /// <summary>
        /// 介于720p与480p之间的类型，默认960*540
        /// </summary>
        kNIMVChatVideoQuality540p = 5,
        ///<summary>
        ///用于桌面分享级别的分辨率1280x720，需要使用高清摄像头并指定对应的分辨率，或者自定义通道传输 
        /// </summary>    
        kNIMVChatVideoQuality720p = 6,
	};

	/// <summary>
	/// NIMVChatVideoFrameRate 视频通话帧率，实际帧率因画面采集频率和机器性能限制可能达不到期望值
	/// </summary>
	public enum NIMVChatVideoFrameRate
	{
		/// <summary>
		/// 视频通话帧率默认值,最大取每秒15帧
		/// </summary>
		kNIMVChatVideoFrameRateNormal = 0,
		/// <summary>
		/// 视频通话帧率 最大取每秒5帧
		/// </summary>
		kNIMVChatVideoFrameRate5 = 1,
		/// <summary>
		/// 视频通话帧率 最大取每秒10帧
		/// </summary>
		kNIMVChatVideoFrameRate10 = 2,
		/// <summary>
		/// 视频通话帧率 最大取每秒15帧
		/// </summary>
		kNIMVChatVideoFrameRate15 = 3,
		/// <summary>
		/// 视频通话帧率 最大取每秒20帧
		/// </summary>
		kNIMVChatVideoFrameRate20 = 4,
		/// <summary>
		/// 视频通话帧率 最大取每秒25帧
		/// </summary>
		kNIMVChatVideoFrameRate25 = 5,
	};

    /// <summary>
    /// 音视频服务器连接状态类型
    /// </summary>
    public enum NIMVChatConnectErrorCode
    {
        /// <summary>
        /// 断开连接
        /// </summary>
        kNIMVChatConnectDisconn = 0,
        /// <summary>
        /// 启动失败
        /// </summary>
        kNIMVChatConnectStartFail = 1,
        /// <summary>
        /// 超时
        /// </summary>
        kNIMVChatConnectTimeout = 101,
        /// <summary>
        /// 会议模式错误
        /// </summary>
        kNIMVChatConnectMeetingModeError = 102,
        /// <summary>
        /// 非rtmp用户加入rtmp频道
        /// </summary>	  
        kNIMVChatConnectRtmpModeError = 103,
        /// <summary>
        /// 超过频道最多rtmp人数限制
        /// </summary>
        kNIMVChatConnectRtmpNodesError = 104,
        /// <summary>
        /// 已经存在一个主播
        /// </summary>     
        kNIMVChatConnectRtmpHostError = 105,
        /// <summary>
        /// 需要旁路直播, 但频道创建者非主播
        /// </summary>
        kNIMVChatConnectRtmpCreateError = 106,
        /// <summary>
        /// 服务器验证内部错误
        /// </summary>
        kNIMVChatJoinServerValidError = 107,
        /// <summary>
        /// 频道已经关闭
        /// </summary>
        kNIMVChatJoinChannelClosed = 108,
        /// <summary>
        /// 成功
        /// </summary>
        kNIMVChatConnectSuccess = 200,
        /// <summary>
        /// 主播自定义布局错误
        /// </summary>
        kNIMVChatConnectLayoutError = 208,
        /// <summary>
        /// 错误参数
        /// </summary>
        kNIMVChatConnectInvalidParam = 400,
        /// <summary>
        /// 密码加密错误
        /// </summary>
        kNIMVChatConnectDesKey = 401,
        /// <summary>
        /// 错误请求
        /// </summary>
        kNIMVChatConnectInvalidRequst = 417,
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        kNIMVChatConnectServerUnknown = 500,
        /// <summary>
        /// 退出
        /// </summary>
        kNIMVChatConnectLogout = 1001,
        /// <summary>
        /// 发起失败
        /// </summary>
        kNIMVChatChannelStartFail = 11000,
        /// <summary>
        /// 断开连接
        /// </summary>
        kNIMVChatChannelDisconnected = 11001,
        /// <summary>
        /// 本人SDK版本太低不兼容
        /// </summary>
        kNIMVChatVersionSelfLow = 11002,
        /// <summary>
        /// 对方SDK版本太低不兼容
        /// </summary>
        kNIMVChatVersionRemoteLow = 11003,
        /// <summary>
        /// 通道被关闭
        /// </summary>
        kNIMVChatLocalChannelClosed = 11004,
        /// <summary>
        /// 账号被踢
        /// </summary>
        kNIMVChatLocalChannelKicked = 11005
    };

	/// <summary>
	/// NIMVChatMp4RecordCode mp4录制状态
	/// </summary>
	public enum NIMVChatMp4RecordCode
	{
		/// <summary>
		/// MP4结束
		/// </summary>
		kNIMVChatMp4RecordClose = 0,
		/// <summary>
		/// MP4结束，视频画面大小变化
		/// </summary>	       
		kNIMVChatMp4RecordVideoSizeError = 1,
		/// <summary>
		/// MP4结束，磁盘空间不足
		/// </summary>
		kNIMVChatMp4RecordOutDiskSpace = 2,
        /// <summary>
        /// MP4结束，录制线程繁忙 
        /// </summary>
        kNIMVChatMp4RecordThreadBusy = 3,       
        /// <summary>
        /// MP4文件创建
        /// </summary>    
        kNIMVChatMp4RecordCreate = 200,
		/// <summary>
		/// MP4文件已经存在
		/// </summary>
		kNIMVChatMp4RecordExsit = 400,
		/// <summary>
		/// MP4文件创建失败
		/// </summary>   
		kNIMVChatMp4RecordCreateError = 403,
		/// <summary>
		/// 通话不存在
		/// </summary>
		kNIMVChatMp4RecordInvalid = 404,
	};

	/// <summary>
	/// NIMVChatAudioRecordCode 音频录制状态 */
	/// </summary>
	public enum NIMVChatAudioRecordCode
	{
		/// <summary>
		/// 录制正常结束
		/// </summary>
		kNIMVChatAudioRecordClose = 0,
		/// <summary>
		/// 录制结束，磁盘空间不足
		/// </summary>
		kNIMVChatAudioRecordOutDiskSpace = 2,
		/// <summary>
		/// 文件创建成功
		/// </summary>
		kNIMVChatAudioRecordCreate = 200,
		/// <summary>
		/// 已经存在
		/// </summary>
		kNIMVChatAudioRecordExsit = 400,
		/// <summary>
		/// 文件创建失败
		/// </summary>
		kNIMVChatAudioRecordCreateError = 403,
		/// <summary>
		/// 通话不存在
		/// </summary>
		kNIMVChatAudioRecordInvalid = 404,
	};

	/* 3.6.0 sdk已不支持
	/// <summary>
	/// NIMVChatSetStreamingModeCode 设置推流模式返回码
	/// </summary>
	public enum NIMVChatSetStreamingModeCode
    {
        /// <summary>
        /// 无效的操作
        /// </summary>
        kNIMVChatBypassStreamingInvalid = 0,	
		/// <summary>
        /// 设置成功
		/// </summary>
        kNIMVChatBypassStreamingSuccess = 200,	
		/// <summary>
        /// 超过最大允许直播节点数量
		/// </summary>
        kNIMVChatBypassStreamingErrorExceedMax = 202,	
	    /// <summary>
        /// 必须由主播第一个开启直播
	    /// </summary>
        kNIMVChatBypassStreamingErrorHostNotJoined = 203,
		/// <summary>
		///  互动直播服务器错误
		/// </summary>
        kNIMVChatBypassStreamingErrorServerError = 204,	
		/// <summary>
		/// 互动直播其他错误
		/// </summary>
        kNIMVChatBypassStreamingErrorOtherError = 205,			
        /// <summary>
        ///  互动直播服务器没有响应
        /// </summary>
        kNIMVChatBypassStreamingErrorNoResponse = 404,			
        /// <summary>
        /// 重连过程中无法进行相关操作，稍后再试
        /// </summary>
        kNIMVChatBypassStreamingErrorReconnecting = 405,	
		/// <summary>
		/// 互动直播设置超时
		/// </summary>
        kNIMVChatBypassStreamingErrorTimeout = 408,	
    };
	*/

	/// <summary>
	/// @enum NIMVChatVideoFrameScaleType 视频画面长宽比，裁剪时不改变横竖屏，如4：3，代表宽高横屏4：3或者竖屏3：4  *
	/// </summary>
	public enum NIMVChatVideoFrameScaleType
	{
		/// <summary>
		/// 默认，不裁剪
		/// </summary>
		kNIMVChatVideoFrameScaleNone = 0,
		/// <summary>
		///  裁剪成1：1的形状
		/// </summary>   
		kNIMVChatVideoFrameScale1x1 = 1,
		/// <summary>
		/// 裁剪成4：3的形状，如果是
		/// </summary>
		kNIMVChatVideoFrameScale4x3 = 2,
		/// <summary>
		/// 裁剪成16：9的形状
		/// </summary>
		kNIMVChatVideoFrameScale16x9 = 3,
	};

    /// <summary>
    ///  NIMVChatVideoSplitMode 主播设置的直播分屏模式 
    /// </summary>
    public enum NIMVChatVideoSplitMode
    {
        /// <summary>
        ///  底部横排浮窗
        /// </summary>
        kNIMVChatSplitBottomHorFloating = 0,
        /// <summary>
        /// 顶部横排浮窗
        /// </summary>
        kNIMVChatSplitTopHorFloating = 1,
        /// <summary>
        /// 平铺
        /// </summary>
        kNIMVChatSplitLatticeTile = 2,
        /// <summary>
        /// 裁剪平铺
        /// </summary>
        kNIMVChatSplitLatticeCuttingTile = 3,
        /// <summary>
        /// 自定义布局
        /// </summary>
        kNIMVChatSplitCustomLayout = 4,
        /// <summary>
        /// 纯音频布局
        /// </summary>
        kNIMVChatSplitAudioLayout = 5,
    };

	/// <summary>
	/// NIMVChatLiveStateCode 直播时的状态码。服务器定时更新，一些存在时间短的状态会获取不到
	/// </summary>
	public enum NIMVChatLiveStateCode
	{
		kNIMVChatLiveStateInitial = 500,
		/// <summary>
		/// 主播设置定制布局，布局参数错误
		/// </summary>
		kNIMVChatLiveStateLayoutError = 501,
		/// <summary>
		/// 开始连接
		/// </summary>
		kNIMVChatLiveStateStartConnecting = 502,
		/// <summary>
		/// 连接成功
		/// </summary>
		kNIMVChatLiveStateConnectted = 503,
		/// <summary>
		/// 连接失败
		/// </summary>
		kNIMVChatLiveStateConnectFail = 504,
		/// <summary>
		/// 推流中
		/// </summary>
		kNIMVChatLiveStatePushing = 505,
		/// <summary>
		/// 互动直播推流失败
		/// </summary>
		kNIMVChatLiveStatePushFail = 506,
		/// <summary>
		/// 内部错误
		/// </summary>
		kNIMVChatLiveStateInnerError = 507,
		/// <summary>
		/// 人数超出限制
		/// </summary>
		kNIMVChatLiveStatePeopleLimit = 508
	};
#else

    /// <summary>
    /// NIMVChatMp4RecordCode mp4录制状态
    /// </summary>
    public enum NIMVChatMp4RecordCode
    {
        /// <summary>
        /// MP4结束
        /// </summary>
        kNIMVChatMp4RecordClose = 0,
        /// <summary>
        /// MP4结束，视频画面大小变化
        /// </summary>	       
        kNIMVChatMp4RecordVideoSizeError = 1,
        /// <summary>
        /// MP4结束，磁盘空间不足
        /// </summary>
        kNIMVChatMp4RecordOutDiskSpace = 2,
        /// <summary>
        /// MP4结束，录制线程繁忙 
        /// </summary>
        kNIMVChatMp4RecordThreadBusy = 3,
        /// <summary>
        /// MP4文件创建
        /// </summary>    
        kNIMVChatMp4RecordCreate = 200,
        /// <summary>
        /// MP4文件已经存在
        /// </summary>
        kNIMVChatMp4RecordExsit = 400,
        /// <summary>
        /// MP4文件创建失败
        /// </summary>   
        kNIMVChatMp4RecordCreateError = 403,
        /// <summary>
        /// 通话不存在
        /// </summary>
        kNIMVChatMp4RecordInvalid = 404,
    };

    /// <summary>
    /// NIMVChatAudioRecordCode 音频录制状态 */
    /// </summary>
    public enum NIMVChatAudioRecordCode
    {
        /// <summary>
        /// 录制正常结束
        /// </summary>
        kNIMVChatAudioRecordClose = 0,
        /// <summary>
        /// 录制结束，磁盘空间不足
        /// </summary>
        kNIMVChatAudioRecordOutDiskSpace = 2,
        /// <summary>
        /// 文件创建成功
        /// </summary>
        kNIMVChatAudioRecordCreate = 200,
        /// <summary>
        /// 已经存在
        /// </summary>
        kNIMVChatAudioRecordExsit = 400,
        /// <summary>
        /// 文件创建失败
        /// </summary>
        kNIMVChatAudioRecordCreateError = 403,
        /// <summary>
        /// 通话不存在
        /// </summary>
        kNIMVChatAudioRecordInvalid = 404,
    };
#endif
    /// <summary>
    /// NIMVChatVideoEncodeMode 视频编码策略
    /// </summary>
    public enum NIMVChatVideoEncodeMode
    {
        /// <summary>
        /// 默认值，清晰优先
        /// </summary>
        kNIMVChatVEModeNormal = 0,
        /// <summary>
        /// 流畅优先
        /// </summary>
        kNIMVChatVEModeFramerate = 1,
        /// <summary>
        /// 清晰优先
        /// </summary>
        kNIMVChatVEModeQuality = 2,
        /// <summary>
        /// 录屏模式,sdk不会根据网络调整分辨率
        /// </summary>
        kNIMVChatVEModeScreen = 3
    };


    /// <summary>
    /// NIMNetDetectType 探测类型
    /// </summary>
    public enum NIMNetDetectType
    {
        /// <summary>
        /// 默认值，音频探测
        /// </summary>
        kNIMNetDetectTypeAudio = 0,
        /// <summary>
        /// 视频探测 
        /// </summary>
        kNIMNetDetectTypeVideo = 1,     
    };

    /// <summary>
    /** NIMNetDetectVideoQuality 视频探测类型  */
    /// </summary>
    public enum NIMNetDetectVideoQuality
    {
        /// <summary>
        /// 480p
        /// </summary>
        kNIMNDVideoQualityDefault = 0,
        /// <summary>
        /// 176*144
        /// </summary>
        kNIMNDVideoQualityLow = 1,
        /// <summary>
        /// 352*288
        /// </summary>
        kNIMNDVideoQualityMedium = 2,
        /// <summary>
        /// 480*320
        /// </summary>
        kNIMNDVideoQualityHigh = 3,
        /// <summary>
        /// 640*480
        /// </summary>
        kNIMNDVideoQuality480p = 4,
        /// <summary>
        /// 960*540
        /// </summary>
        kNIMNDVideoQuality540p = 5,
        /// <summary>
        /// 1280*720
        /// </summary>
        kNIMNDVideoQuality720p = 6,
    };

    /// <summary>
    /** NIMVideoChatUserLeftType 成员退出类型 */
    /// 
    /// </summary>
    public enum NIMVideoChatUserLeftType
    {
        /// <summary>
        /// 成员超时掉线
        /// </summary>
        kNIMVChatUserLeftTimeout = -1,
        /// <summary>
        ///  成员离开
        /// </summary>
        kNIMVChatUserLeftNormal = 0,     
    };


    /// <summary>
    /// NIMMainPictureOptCode 房间主画面设置返回码
    /// </summary>
    public enum NIMMainPictureOptCode
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        kNIMMainPictureOptSucess = 200,
        /// <summary>
        /// 认证错误
        /// </summary>
        kNIMMainPictureOptAuthError = 401,
        /// <summary>
        /// 房间不存在
        /// </summary>
        kNIMMainPictureOptRoomNotExist = 404,
        /// <summary>
        /// 房间下的uid不存在
        /// </summary>
        kNIMMainPictureOptUidNotExist = 405,
        /// <summary>
        /// 请求数据不对
        /// </summary>
        kNIMMainPictureOptDataError = 417,
        /// <summary>
        /// 内部错误
        /// </summary>
        kNIMMainPictureOptError = 500,
        /// <summary>
        /// 服务器内部错误
        /// </summary>
        kNIMMainPictureOptServerError = 600,
        /// <summary>
        /// 无效的操作
        /// </summary>
        kNIMMainPictureOptInvilid = 11403,
    };

    /// <summary>
    /// NIMVChatAudioMode 音频模式 
    /// </summary>
    public enum NIMVChatAudioMode
    {
        /// <summary>
        /// 默认值，此时参考kNIMVChatAudioHighRate
        /// </summary>
        kNIMVChatAdModeDefault = 0,
        /// <summary>
        /// 窄带，kNIMVChatAudioHighRate无效
        /// </summary>
        kNIMVChatAdModeNormal = 1,
        /// <summary>
        /// 高清语音，kNIMVChatAudioHighRate无效
        /// </summary>
        kNIMVChatAdModeHighVoip = 2,
        /// <summary>
        /// 高清音乐，kNIMVChatAudioHighRate无效
        /// </summary>
        kNIMVChatAdModeHighMusic = 3,
    }

    /// <summary>
    /// 服务器录制模式，用于指定本人数据录制选择 
    /// </summary>
    public enum NIMVChatServRecordType
    {
        /// <summary>
        /// 服务器录制混录并带单人文件
        /// </summary>
        kNIMVChatServRecordMixedSingle = 0,
        /// <summary>
        /// 服务器录制只混录
        /// </summary>
        kNIMVChatServRecordMixed = 1,
        /// <summary>
        /// 服务器录制只录本人单人文件
        /// </summary>
        kNIMVChatServRecordSingle = 2, 
    }

    /// <summary>
    /// 发起和接受通话时的参数
    /// </summary>
    public class NIMVChatInfo : NimUtility.NimJsonObject<NIMVChatInfo>
	{
		/// <summary>
		/// 成员id列表，主动发起非空(必填)
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "uids", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public List<string> Uids { get; set; }

        /// <summary>
        /// 发起会话的标识id，将在创建通话及结束通话时有效，帮助针对无channelid的情况下进行映射
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "session_id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SessionId { get; set; }

        /// <summary>
        /// 是否用自定义音频数据（PCM）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_audio")]
		public int CustomAudio { get; set; }

		/// <summary>
		/// 是否用自定义视频数据（i420）
		/// </summary>
		[Newtonsoft.Json.JsonProperty("custom_video")]
		public int CustomVideo { get; set; }

		/// <summary>
		/// 是否需要录制音频数据 >0表示是 （需要服务器配置支持，本地录制直接调用接口函数）
		/// </summary>
		[Newtonsoft.Json.JsonProperty("record")]
		public int ServerAudioRecord { get; set; }
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
		/// <summary>
		/// 是否需要录制视频数据 >0表示是 （需要服务器配置支持，本地录制直接调用接口函数）
		/// </summary>
		[Newtonsoft.Json.JsonProperty("video_record")]
		public int ServerVideoRecord { get; set; }

        /// <summary>
        /// 服务器录制模式NIMVChatServRecordType，默认为0 （需要服务器配置支持，并且开kNIMVChatRecord，kNIMVChatSingleRecord其中一个）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("record_type")]
        public int ServerSingleRecord { get; set; }

        /// <summary>
        /// 服务器混录时指定本人主画面，默认为0, 非0表示打开（需要服务器配置支持，及录制模式打开混录，并且为多人中的第一个主画面配置
        /// </summary>
        [Newtonsoft.Json.JsonProperty("r_host_speaker")]
        public int ServerHostSpeaker { get; set; }

        /// <summary>
        ///  视频发送编码码率 [100000,600000]有效
        /// </summary>
        [Newtonsoft.Json.JsonProperty("max_video_rate")]
		public int MaxVideoRate { get; set; }

		/// <summary>
		/// 视频聊天分辨率选择 NIMVChatVideoQuality
		/// </summary>
		[Newtonsoft.Json.JsonProperty("video_quality")]
		public int VideoQuality { get; set; }

		/// <summary>
		/// 视频画面帧率 NIMVChatVideoFrameRate
		/// </summary>
		[Newtonsoft.Json.JsonProperty("frame_rate")]
		public int FrameRate { get; set; }

        /// <summary>
        /// 音频模式选择，非默认时kNIMVChatAudioHighRate无效
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_mode")]
        public int AudioMode { get; set; }

                /// <summary>
        /// 直播推流地址(加入多人时有效)，非空代表主播旁路直播， kNIMVChatBypassRtmp决定是否开始推流
        /// </summary>
        [Newtonsoft.Json.JsonProperty("rtmp_url")]
		public string RtmpUrl { get; set; }

		/// <summary>
		/// 是否是旁路直播观众，此时MeetingMode无效 >0表示是
		/// </summary>
		[Newtonsoft.Json.JsonProperty("bypass_rtmp")]
		public int BypassRtmp { get; set; }

		/// <summary>
		/// 是否开启服务器对直播推流录制（需要开启服务器能力）， >0表示是
		/// </summary>
		[Newtonsoft.Json.JsonProperty("rtmp_record")]
		public int RtmpRecord { get; set; }

		/// <summary>
		/// 主播控制的直播推流时的分屏模式 NIMVChatVideoSplitMode
		/// </summary>
		[Newtonsoft.Json.JsonProperty("split_mode")]
		public int SplitMode { get; set; }

        /// <summary>
        /// 自定义布局，当主播选择kNIMVChatSplitCustomLayout或kNIMVChatSplitAudioLayout模式时生效
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "custom_layout", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Layout { get; set; }

        /// <summary>
        /// 无效已经默认支持
        /// 是否支持webrtc互通,1表示是，0表示否。默认否
        /// </summary>
        // [Newtonsoft.Json.JsonProperty("webrtc")]
        // public int Webrtc { get; set; }

        /// <summary>
        /// 使用的视频编码策略NIMVChatVideoEncodeMode， 默认kNIMVChatVEModeNormal
        /// </summary>
        [Newtonsoft.Json.JsonProperty("v_encode_mode")]
        public int VEncodeMode { get; set; }
#else
        /// <summary>
        /// 语音音量信息是否需要上报 >0表示是
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_report_volume")]
        public int AudioReportVolume { get; set; }
#endif
        /// <summary>
        /// 是否使用语音高清模式 >0表示是（默认关闭）3.3.0 之前的版本无法加入已经开启高清语音的多人会议
        /// </summary>
        [Newtonsoft.Json.JsonProperty("high_rate")]
        public int AudioHighRate { get; set; }

        /// <summary>
        /// 是否需要推送 >0表示是 默认是
        /// </summary>
        [Newtonsoft.Json.JsonProperty("push_enable")]
		public int PushEnable { get; set; }

		/// <summary>
		/// 是否需要角标计数 >0表示是 默认是
		/// </summary>
		[Newtonsoft.Json.JsonProperty("need_badge")]
		public int NeedBadge { get; set; }

		/// <summary>
		/// 是否需要推送昵称 >0表示是 默认是
		/// </summary>
		[Newtonsoft.Json.JsonProperty("need_nick")]
		public int NeedNick { get; set; }

		/// <summary>
		/// JSON格式,推送payload
		/// </summary>
		[Newtonsoft.Json.JsonProperty("payload")]
		public string PayLoad { get; set; }

		/// <summary>
		/// 推送声音
		/// </summary>
		[Newtonsoft.Json.JsonProperty("sound")]
		public string Sound { get; set; }

		/// <summary>
		/// int, 是否强制持续呼叫（对方离线也会呼叫）,1表示是，0表示否。默认是
		/// </summary>
		[Newtonsoft.Json.JsonProperty("keepcalling")]
		public int KeepCalling { get; set; }

        /// <summary>
        /// 是否呼叫超时
        /// </summary>
        [Newtonsoft.Json.JsonProperty("time_out")]
        public bool CallingTimeout { get; set; }

		public NIMVChatInfo()
		{
            Uids = new List<string>();
            SessionId = "";
            CustomAudio = 0;
			CustomVideo = 0;
			ServerAudioRecord = 0;
            AudioHighRate = 0;
            PushEnable = 1;
			NeedBadge = 1;
			NeedNick = 1;
			PayLoad = "";
			Sound = "";
			KeepCalling = 1;

            //Webrtc = 0;
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
            ServerVideoRecord = 0;
            ServerVideoRecord = 0;
            MaxVideoRate = 0;
            VideoQuality = 0;
            FrameRate = 0;
            RtmpUrl = "";
            BypassRtmp = 0;
            RtmpRecord = 0;
            SplitMode = 0;
            Layout = null;
            VEncodeMode = 0;
            ServerHostSpeaker = 0;
#else
            AudioReportVolume = 0;//默认关闭
#endif

        }
	}

	public class NIMVChatSessionInfo : NimUtility.NimJsonObject<NIMVChatSessionInfo>
	{
		/// <summary>
		/// 用户账号uid
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "uid", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string Uid { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "status", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Status { get; set; }

		/// <summary>
		/// 录制地址（服务器开启录制时有效）
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "record_addr", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string RecordAddr { get; set; }

		/// <summary>
		/// 服务器音频录制文件名（服务器开启录制时有效）
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "record_file", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string RecordFile { get; set; }

		/// <summary>
		/// 服务器视频录制文件名（服务器开启录制时有效）
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "video_record_file", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public string VideoRecordFile { get; set; }

		/// <summary>
		/// 状态
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "type", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Type { get; set; }

		/// <summary>
		/// 时间 单位毫秒
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "time", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public long Time { get; set; }

		/// <summary>
		/// 是否接受 >0表示接受
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "accept", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Accept { get; set; }

		/// <summary>
		/// 客户端类型 NIMClientType 见NIMClientDef.cs
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "client", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int Client { get; set; }

		/// <summary>
		/// 自定义数据
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "custom_info", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public String CustomInfo { get; set; }

        /// <summary>
        /// 下行流量
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "trafficstat_rx", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ulong NIMVChatTrafficStatRX { get; set; }

        /// <summary>
        /// 上行流量
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "trafficstat_tx", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public ulong NIMVChatTrafficStatTX { get; set; }


        public NIMVChatSessionInfo()
		{
			Uid = null;
			Status = 0;
			RecordAddr = null;
			RecordFile = null;
			VideoRecordFile = null;
			Type = 0;
			Time = 0;
			Accept = 0;
			Client = 0;
			CustomInfo = null;
            NIMVChatTrafficStatRX = 0;
            NIMVChatTrafficStatTX = 0;

        }
	}

	/// <summary>
	/// 音量状态
	/// </summary>
	public class NIMVchatAudioVolumeState : NimUtility.NimJsonObject<NIMVchatAudioVolumeState>
	{
		public class AudioVolume
		{
			public class State
			{
				[Newtonsoft.Json.JsonProperty(PropertyName = "status", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public int Status { get; set; }
			}

			public class ReceiverState : State
			{
				[Newtonsoft.Json.JsonProperty(PropertyName = "uid", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public string Uid { get; set; }
			}

			/// <summary>
			/// 自己的状态
			/// </summary>
			[Newtonsoft.Json.JsonProperty(PropertyName = "self", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
			public State Self { get; set; }

			/// <summary>
			/// 接收方状态
			/// </summary>
			[Newtonsoft.Json.JsonProperty(PropertyName = "receiver", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
			public ReceiverState[] Receivers { get; set; }
		}

		/// <summary>
		/// 音频实时音量通知，包含发送的音量kNIMVChatSelf和接收音量kNIMVChatReceiver，kNIMVChatStatus的音量值是pcm的平均值最大为int16_max
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "audio_volume", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public AudioVolume Volume { get; set; }
	}

	/// <summary>
	/// 实时状态 
	/// </summary>
	public class NIMVChatRealtimeState : NimUtility.NimJsonObject<NIMVChatRealtimeState>
	{
		public class StateInfo : NimUtility.NimJsonObject<StateInfo>
        {
			public class Video : Audio
			{
				[Newtonsoft.Json.JsonProperty(PropertyName = "width", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public int Width { get; set; }

				[Newtonsoft.Json.JsonProperty(PropertyName = "height", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public int Height { get; set; }
			}

			public class Audio: NimUtility.NimJsonObject<Audio>
            {
				/// <summary>
				/// 每秒帧率或者每秒发包数
				/// </summary>
				[Newtonsoft.Json.JsonProperty(PropertyName = "fps", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public int FPS { get; set; }

				/// <summary>
				/// 每秒流量，单位为“千字节”
				/// </summary>
				[Newtonsoft.Json.JsonProperty(PropertyName = "KBps", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public int KBps { get; set; }

				/// <summary>
				/// 丢包率，单位是百分比
				/// </summary>
				[Newtonsoft.Json.JsonProperty(PropertyName = "lost_rate", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
				public int LostRate { get; set; }
			}

			/// <summary>
			/// 网络延迟
			/// </summary>
			[Newtonsoft.Json.JsonProperty(PropertyName = "rtt", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
			public int Rtt { get; set; }

			/// <summary>
			/// 视频信息
			/// </summary>
			[Newtonsoft.Json.JsonProperty(PropertyName = "video", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
			public Video VideoState { get; set; }

			/// <summary>
			/// 语音信息
			/// </summary>
			[Newtonsoft.Json.JsonProperty(PropertyName = "audio", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
			public Audio AudioState { get; set; }
		}

		/// <summary>
		/// 音视频实时状态
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "static_info", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public StateInfo Info { get; set; }
	}

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
	/// <summary>
	/// 直播状态
	/// </summary>
	public class NIMVChatLiveState : NimUtility.NimJsonObject<NIMVChatLiveState>
	{
		public class StateInfo: NimUtility.NimJsonObject<StateInfo>
        {
			[Newtonsoft.Json.JsonProperty(PropertyName = "status", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
			public NIMVChatLiveStateCode status { get; set; }
		}
		/// <summary>
		/// 直播状态
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "live_state", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public StateInfo info { get; set; }
	}


#endif
    /// <summary>
    /// 录制MP4文件接口封装的json类
    /// </summary>
    public class NIMVChatMP4RecordJsonEx : NimUtility.NimJsonObject<NIMVChatMP4RecordJsonEx>
    {
        /// <summary>
        /// kNIMVChatUid录制的成员，如果是自己填空，(录制时允许同时混音对端声音，需要填kNIMVChatMp4AudioType)
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "uid", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string RecordUid { get; set; }

        /// <summary>
        /// kNIMVChatMp4AudioType mp4录制时音频情况，0标识只录制当前成员，1标识录制通话全部混音（等同音频文件录制的声音）
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_audio", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int RecordPeopleType { get; set; }

        /// <summary>
        /// RecordRecode mp4录制重编码开关
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_recode", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool RecordRecode { get; set; }

        /// <summary>
        /// RecordMp4Width mp4录制文件的宽,不填或者数据无效[16-1280有效]时以收到的第一帧画面宽为默认
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_width", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int RecordMp4Width { get; set; }

        /// <summary>
        ///  RecordMp4Height mp4录制文件的高,不填或者数据无效[16-1280有效]时以收到的第一帧画面高为默认
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_height", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int RecordMp4Height { get; set; }


        public NIMVChatMP4RecordJsonEx()
        {
            RecordUid = "";
            RecordPeopleType = 0;
        }
    }

    /// <summary>
    /// 创建聊天室的json扩展封装类
    /// </summary>
    public class NIMCreateRoomJsonEx: NimUtility.NimJsonObject<NIMCreateRoomJsonEx>
	{

		/// <summary>
        /// 无效,已经默认支持
		/// 是否支持webrtc互通,1表示是，0表示否。默认否
		/// </summary>
        //[Newtonsoft.Json.JsonProperty(PropertyName = "webrtc", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        //public int webrtc { get; set; }

		public NIMCreateRoomJsonEx()
		{
			//webrtc = 0;
		}
	}

    public class HostArea : NimUtility.NimJsonObject<HostArea>
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "adaption", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Adaption { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "height_rate", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int HeightRate { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "position_x", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int PositionX { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "position_y", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int PositionY { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "width_rate", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int WidthRate { get; set; }
        public HostArea()
        {
            Adaption = 1;
            HeightRate = 8000;
            PositionX = 1000;
            PositionY = 1000;
            WidthRate = 8000;
        }
    }

    public class BackGround : NimUtility.NimJsonObject<BackGround>
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "rgb_b", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Rgb_B { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "rgb_g", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Rgb_G { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "rgb_r", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int Rgb_R { get; set; }
        public BackGround()
        {
            Rgb_B = 128;
            Rgb_G = 128;
            Rgb_R = 128;
        }

    }
    public class CustomLayout : NimUtility.NimJsonObject<CustomLayout>
    {
    
        [Newtonsoft.Json.JsonProperty(PropertyName = "background", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public BackGround Background { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "host_area", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public HostArea Hostarea { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "n_host_area_0", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public HostArea NHostArea0 { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "main_height", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int MainHeight { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "main_width", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int MainWidth { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "n_host_area_number", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int NHostAreaNumber { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "set_host_as_main", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool SetHostAsMain { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "special_show_mode", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public bool SpecialShowMode { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "version", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public int version { get; set; }

        public CustomLayout()
        {
            Background = null;
            Hostarea = null;
            NHostArea0 = null;
            MainHeight = 720;
            MainWidth = 1280;
            NHostAreaNumber = 1;
            SetHostAsMain = false;
            SpecialShowMode = true;
            version = 0;

        }
    }

    /// <summary>
    /// 加入聊天室的josn拓展封装类
    /// </summary>
    public class NIMJoinRoomJsonEx:NimUtility.NimJsonObject<NIMJoinRoomJsonEx>
	{
        /// <summary>
        /// 发起会话的标识id，将在创建通话及结束通话时有效，帮助针对无channelid的情况下进行映射
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "session_id", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string SessionId { get; set; }

        /// <summary>
        /// 是否用自定义音频数据（PCM）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_audio")]
        public int CustomAudio { get; set; }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 是否用自定义视频数据（i420）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("custom_video")]
        public int CustomVideo { get; set; }

        /// <summary>
        /// 是否需要录制音频数据 >0表示是 （需要服务器配置支持，本地录制直接调用接口函数）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("record")]
        public int ServerAudioRecord { get; set; }

        /// <summary>
        /// 是否需要录制视频数据 >0表示是 （需要服务器配置支持，本地录制直接调用接口函数）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("video_record")]
        public int ServerVideoRecord { get; set; }

        /// <summary>
        /// 服务器录制模式NIMVChatServRecordType，默认为0 （需要服务器配置支持，并且开kNIMVChatRecord，kNIMVChatSingleRecord其中一个）
        /// </summary>
        [Newtonsoft.Json.JsonProperty("record_type")]
        public int ServerSingleRecord { get; set; }

        /// <summary>
        /// 服务器混录时指定本人主画面，默认为0, 非0表示打开（需要服务器配置支持，及录制模式打开混录，并且为多人中的第一个主画面配置
        /// </summary>
        [Newtonsoft.Json.JsonProperty("r_host_speaker")]
        public int ServerHostSpeaker { get; set; }

        /// <summary>
        ///  视频发送编码码率 [100000,600000]有效
        /// </summary>
        [Newtonsoft.Json.JsonProperty("max_video_rate")]
        public int MaxVideoRate { get; set; }

        /// <summary>
        /// 视频聊天分辨率选择 NIMVChatVideoQuality
        /// </summary>
        [Newtonsoft.Json.JsonProperty("video_quality")]
        public int VideoQuality { get; set; }

        /// <summary>
        /// 视频画面帧率 NIMVChatVideoFrameRate
        /// </summary>
        [Newtonsoft.Json.JsonProperty("frame_rate")]
        public int FrameRate { get; set; }

        /// <summary>
        /// 音频模式选择，非默认时kNIMVChatAudioHighRate无效
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_mode")]
        public int AudioMode { get; set; }

        /// <summary>
        /// 直播推流地址(加入多人时有效)，非空代表主播旁路直播， kNIMVChatBypassRtmp决定是否开始推流
        /// </summary>
        [Newtonsoft.Json.JsonProperty("rtmp_url")]
        public string RtmpUrl { get; set; }

        /// <summary>
        /// 是否是旁路直播观众，此时MeetingMode无效 >0表示是
        /// </summary>
        [Newtonsoft.Json.JsonProperty("bypass_rtmp")]
        public int BypassRtmp { get; set; }

        /// <summary>
        /// 是否开启服务器对直播推流录制（需要开启服务器能力）， >0表示是
        /// </summary>
        [Newtonsoft.Json.JsonProperty("rtmp_record")]
        public int RtmpRecord { get; set; }
 

        /// <summary>
        /// 主播控制的直播推流时的分屏模式 NIMVChatVideoSplitMode
        /// </summary>
        [Newtonsoft.Json.JsonProperty("split_mode")]
        public int SplitMode { get; set; }

        /// <summary>
        /// 自定义布局
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "custom_layout", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string Layout { get; set; }

        /// <summary>
        /// 使用的视频编码策略NIMVChatVideoEncodeMode， 默认kNIMVChatVEModeNormal
        /// </summary>
        [Newtonsoft.Json.JsonProperty("v_encode_mode")]
        public int VEncodeMode { get; set; }
#else
        /// <summary>
        /// 语音音量信息是否需要上报 >0表示是
        /// </summary>
        [Newtonsoft.Json.JsonProperty("audio_report_volume")]
        public int AudioReportVolume { get; set; }
#endif
        /// <summary>
        /// 是否使用语音高清模式 >0表示是（默认关闭）3.3.0 之前的版本无法加入已经开启高清语音的多人会议
        /// </summary>
        [Newtonsoft.Json.JsonProperty("high_rate")]
        public int AudioHighRate { get; set; }

        public NIMJoinRoomJsonEx()
		{
            //Layout.MainHeight = 0;
            SessionId = "";
            CustomAudio = 0;
            AudioHighRate = 0;
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
            CustomVideo = 0;
			VideoQuality = 0;
            ServerAudioRecord = 0;
            ServerVideoRecord = 0;
            ServerSingleRecord = 0;
            MaxVideoRate = 0;
            AudioMode = 0;
            RtmpUrl = "";
            RtmpRecord = 0;
            BypassRtmp = 0;
            SplitMode = 0;
            VEncodeMode = 0;
            ServerHostSpeaker = 0;
#else
            AudioReportVolume = 0;//默认关闭
#endif
        }
	}


	public class NIMVChatCustomAudioJsonEx : NimUtility.NimJsonObject<NIMVChatCustomAudioJsonEx>
	{

		/// <summary>
		/// 采样频率
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "sample_rate", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SampleRate { get; set; }

		/// <summary>
		/// 采样位深
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "sample_bit", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int SampleBit { get; set; }

		public NIMVChatCustomAudioJsonEx()
		{
			SampleRate = 16000;
			SampleBit = 16;
		}
	}

	public class NIMVChatResourceJsonEx : NimUtility.NimJsonObject<NIMVChatResourceJsonEx>
	{
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 服务器配置文件路径
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "server_setting_path", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#else
        /// <summary>
        /// nrtc相关库资源路径,Unity PC平台下有效
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "path", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
#endif
        public string Path { get; set; }

		public NIMVChatResourceJsonEx()
		{
			Path = "";
		}
	}

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
	public class NIMVChatCustomVideoJsonEx: NimUtility.NimJsonObject<NIMVChatCustomVideoJsonEx>
	{
		/// <summary>
		/// 视频数据类型，NIMVideoSubType
		/// </summary>
		[Newtonsoft.Json.JsonProperty(PropertyName = "subtype", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public int VideoSubType { get; set; }
		public NIMVChatCustomVideoJsonEx()
		{
			VideoSubType = Convert.ToInt32(NIMVideoSubType.kNIMVideoSubTypeI420);
		}
	}
#endif

    public class NIMVChatNetDetectJsonEx : NimUtility.NimJsonObject<NIMVChatNetDetectJsonEx>
    {
        /// <summary>
        /// 用户的app_key
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "app_key", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string AppKey { get; set; }

        /// <summary>
        /// 毫秒级的探测时长限制,设置时长为0，采用sdk默认时长
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "time", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Int32 DetectTime { get; set; }

        /// <summary>
        /// 探测类型NIM NIMNetDetectType
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "type", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Int32 DetectType { get; set; }

        /// <summary>
        /// kNIMNetDetectTypeVideo时有效，默认为0，视频探测类型NIMNetDetectVideoQuality
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "quality_type", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public Int32 VideoDetectQualityType { get; set; }

        public NIMVChatNetDetectJsonEx()
        {
            DetectTime = 0;
            DetectType = Convert.ToInt32(NIMNetDetectType.kNIMNetDetectTypeAudio);
            VideoDetectQualityType = 0;
        }

    }

   
    public class NIMVChatMP4State : NimUtility.NimJsonObject<NIMVChatMP4State>
    {
        public class NIMVChatMP4Info : NimUtility.NimJsonObject<NIMVChatMP4Info>
        {
            [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_file", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string FilePath { get; set; }

            [Newtonsoft.Json.JsonProperty(PropertyName = "time", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public long Duration { get; set; }
            [Newtonsoft.Json.JsonProperty(PropertyName = "status", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public Int32 Status { get; set; }

            [Newtonsoft.Json.JsonProperty(PropertyName = "uid", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string Uid { get; set; }
            public NIMVChatMP4Info()
            {
                FilePath = null;
                Duration = 0;
                Uid = null;
                Status = 0;
            }
        }

        [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_start", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public NIMVChatMP4Info MP4_Start { get; set; }

        [Newtonsoft.Json.JsonProperty(PropertyName = "mp4_close", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public NIMVChatMP4Info MP4_Close { get; set; }
    }

    public class NIMVChatAuRecordState: NimUtility.NimJsonObject<NIMVChatAuRecordState>
    {
        public class NIMVChatAudioInfo: NimUtility.NimJsonObject<NIMVChatAudioInfo>
        {
            [Newtonsoft.Json.JsonProperty(PropertyName = "file", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public string FilePath { get; set; }
            [Newtonsoft.Json.JsonProperty(PropertyName = "time", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
            public long duration { get; set; }
            
        }

        [Newtonsoft.Json.JsonProperty(PropertyName = "audio_record_start", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public NIMVChatAudioInfo AudioRecordStart { get; set; }
        [Newtonsoft.Json.JsonProperty(PropertyName = "audio_record_close", NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public NIMVChatAudioInfo AudioRecordClose { get; set; }
    }

   


    /// <summary>
    /// 调用接口回调
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionHandler(long channel_id, int code);

    /// <summary>
    /// 收到邀请
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="mode">通话类型</param>
    /// <param name="time">毫秒级 时间戳</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionInviteNotifyHandler(long channel_id, string uid, int mode, long time,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string customInfo);

    /// <summary>
    /// 确认通话，接受拒绝通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="mode">通话类型</param>
    /// <param name="accept">结果</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionCalleeAckNotifyHandler(long channel_id,
          [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]  string uid, int mode, bool accept);

    /// <summary>
    /// 控制操作结果
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果</param>
    /// <param name="type">操作类型</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionControlResHandler(long channel_id, int code, int type);

    /// <summary>
    /// 控制操作通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="type">操作类型</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionControlNotifyHandler(long channel_id,
		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string uid, int type);

    /// <summary>
    /// 通话中链接状态通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果状态</param>
    /// <param name="record_addr">录制音频文件名（服务器开启录制时有效）</param>
    /// <param name="record_file">录制视频文件名（服务器开启录制时有效）</param>
    /// <param name="chat_time">通话本地时长，code为1001有效</param>
    /// <param name="chat_rx">下行数据量 code为1001有效</param>
    /// <param name="chat_tx">上行数据量 code 为1001有效</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionConnectNotifyHandler(long channel_id, int code,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string record_file,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string video_record_file,
        long chat_time, ulong chat_rx, ulong chat_tx);
    /// <summary>
    /// 通话中成员状态
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="status">状态 NIMVideoChatSessionStatus</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionPeopleStatusHandler(long channel_id,
		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string uid, int status);

    /// <summary>
    /// 通话中网络状态
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="status">状态</param>
	/// <param name="uid">账号</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionNetStatusHandler(long channel_id, int status,
		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string uid);

    /// <summary>
    /// 其他端接听挂断后的同步通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="uid">对方uid</param>
    /// <param name="mode">通话类型</param>
    /// <param name="accept">结果</param>
    /// <param name="time">毫秒级 时间戳</param>
    /// <param name="client">客户端类型</param>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void onSessionSyncAckNotifyHandler(long channel_id, int code,
		[MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string uid, int mode, bool accept, long time, int client);

    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// </summary>
    /// <param name="ret">结果代码，true表示成功</param>
    /// <param name="code">暂时无效</param>
    /// <param name="json_extension">json_extension Json string 扩展字段</param>
    public delegate void NIMVChatOptHandler(bool ret, int code, string json_extension);

    /// <summary>
    /// 操作回调，通用的操作回调接口
    /// </summary>
    /// <param name="code">结果代码，code==200表示成功</param>
    /// <param name="channel_id">channel_id 通道id</param>
    /// <param name="json_extension">son_extension Json string 扩展字段kNIMVChatSessionId，加入多人的返回中带有kNIMVChatCustomInfo</param>
    public delegate void NIMVChatOpt2Handler(int code, long channel_id, string json_extension);

    /// <summary>
    /// 音量状态通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果状态</param>
    /// <param name="state">音量状态信息</param>
    public delegate void onSessionVolumeNotifyHandler(long channel_id, int code, NIMVchatAudioVolumeState state);

    /// <summary>
    /// 视频实时状态信息通知
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">结果状态</param>
    /// <param name="state">实时状态信息</param>
    public delegate void onSessionRealtimeInfoNotifyHandler(long channel_id, int code, NIMVChatRealtimeState state);

    /// <summary>
    /// 通知MP4录制状态，包括开始录制和结束录制
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">无效</param>
    /// <param name="mp4_info">mp4状态信息</param>
    public delegate void OnSessionMP4InfoNotifyHandler(long channel_id, int code, NIMVChatMP4State mp4_info);

    /// <summary>
    /// 通知音频录制状态
    /// </summary>
    /// <param name="channel_id">频道id</param>
    /// <param name="code">无效</param>
    /// <param name="record_info">音频录制状态信息</param>
    public delegate void OnSessionAuRecordInfoNotifyHandler(long channel_id, int code, NIMVChatAuRecordState record_info);

    /// <summary>
    /// MP4操作回调，实际的开始录制和结束都会在NIMVChatSessionStatus.onSessionMp4InfoStateNotify中返回
    /// </summary>
    /// <param name="ret"> 结果代码，true表示成功</param>
    /// <param name="code">对应NIMVChatMp4RecordCode，用于获得失败时的错误原因</param>
    /// <param name="file">文件路径</param>
    /// <param name="time">录制结束时有效，对应毫秒级的录制时长</param>
    /// <param name="json_extension">json_extension Json string 无效扩展字段</param>
    public delegate void NIMVChatMp4RecordOptHandler(bool ret, int code, string file, Int64 time, string json_extension);

    /// <summary>
    /// 音频录制操作回调，实际的开始录制和结束都会在NIMVChatSessionStatus.onSessionAuRecordInfoStateNotify中返回
    /// </summary>
    /// <param name="ret">结果代码，true表示成功</param>
    /// <param name="code">对应NIMVChatAudioRecordCode，用于获得失败时的错误原因</param>
    /// <param name="file">文件路径</param>
    /// <param name="time">录制结束时有效，对应毫秒级的录制时长</param>
    /// <param name="json_extension">json_extension Json string 无效扩展字段</param>
    public delegate void NIMVChatAudioRecordOptHandler(bool ret, int code, string file, Int64 time, string json_extension);

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY

	/// <summary>
	/// 直播状态信息通知
	/// </summary>
	/// <param name="channel_id">频道id</param>
	/// <param name="code">结果类型或错误类型</param>
	/// <param name="state">直播状态</param>
	public delegate void OnSessionLiveStateInfoNotifyHandler(long channel_id, int code, NIMVChatLiveState state);
#endif
}
