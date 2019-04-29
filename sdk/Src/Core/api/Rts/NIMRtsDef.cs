/** @file NIMRtsDef.cs
  * @brief NIM RTS提供的实时会话（数据通道）接口定义
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author gq
  * @date 2015/12/8
  */
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
using System;
using Newtonsoft.Json;
using NimUtility;

namespace NIM
{
    namespace NIMRts
    {
        /// <summary>
        ///     rts通道类型
        /// </summary>
        [Flags]
        public enum NIMRtsChannelType
        {
            /// <summary>
            ///     无通道
            /// </summary>
            kNIMRtsChannelTypeNone = 0,

            /// <summary>
            ///     tcp通道
            /// </summary>
            kNIMRtsChannelTypeTcp = 1,

            /// <summary>
            ///     udp通道 暂不支持
            /// </summary>
            kNIMRtsChannelTypeUdp = 2,

            /// <summary>
            ///     音视频通道
            /// </summary>
            kNIMRtsChannelTypeVchat = 4
        }

        /// <summary>
        ///     成员变化类型
        /// </summary>
        public enum NIMRtsMemberStatus
        {
            /// <summary>
            ///     成员进入
            /// </summary>
            kNIMRtsMemberStatusJoined = 0,

            /// <summary>
            ///     成员退出
            /// </summary>
            kNIMRtsMemberStatusLeaved = 1
        }

        /// <summary>
        /// 成员退出类型
        /// </summary>
        public enum NIMRtsMemberLeftType
        {
            /// <summary>
            /// 成员超时掉线 
            /// </summary>
            kNIMRtsMemberLeftTimeout = -1,

            /// <summary>
            /// 成员离开 
            /// </summary>
            kNIMRtsMemberLeftNormal = 0
        }

        /// <summary>
        ///     音视频通话类型
        /// </summary>
        public enum NIMRtsVideoChatMode
        {
            /// <summary>
            ///     语音通话模式
            /// </summary>
            kNIMRtsVideoChatModeAudio = 1,

            /// <summary>
            ///     视频通话模式
            /// </summary>
            kNIMRtsVideoChatModeVideo = 2
        }


        /// <summary>
        ///     音视频服务器连接状态类型
        /// </summary>
        public enum NIMRtsConnectStatus
        {
            /// <summary>
            ///     断开连接
            /// </summary>
            kNIMRtsConnectStatusDisconn = 0,

            /// <summary>
            ///     启动失败
            /// </summary>
            kNIMRtsConnectStatusStartFail = 1,

            /// <summary>
            ///     超时
            /// </summary>
            kNIMRtsConnectStatusTimeout = 101,

            /// <summary>
            ///     成功
            /// </summary>
            kNIMRtsConnectStatusSuccess = 200,

            /// <summary>
            ///     错误参数
            /// </summary>
            kNIMRtsConnectStatusInvalidParam = 400,

            /// <summary>
            ///     密码加密错误
            /// </summary>
            kNIMRtsConnectStatusDesKey = 401,

            /// <summary>
            ///     错误请求
            /// </summary>
            kNIMRtsConnectStatusInvalidRequst = 417,

            /// <summary>
            ///     服务器内部错误
            /// </summary>
            kNIMRtsConnectStatusServerUnknown = 500,

            /// <summary>
            ///     退出
            /// </summary>
            kNIMRtsConnectStatusLogout = 1001
        }

        /// <summary>
        ///     发起rts或者接起rts时的配置参数
        /// </summary>
        public class RtsStartInfo : NimJsonObject<RtsStartInfo>
        {
            public RtsStartInfo()
            {
                Mode = (int)NIMRtsVideoChatMode.kNIMRtsVideoChatModeAudio;
                CustomAudio = 0;
                CustomVideo = 0;
                KeepCalling = 1;
            }

            /// <summary>
            ///     视频通道的发起模式NIMRtsVideoChatMode，非视频模式时不会发送视频数据
            /// </summary>
            [JsonProperty("mode")]
            public int Mode { get; set; }

            /// <summary>
            ///     是否用自定义音频数据（PCM）
            /// </summary>
            [JsonProperty("custom_audio")]
            public int CustomAudio { get; set; }

            /// <summary>
            ///     是否用自定义视频数据（i420）
            /// </summary>
            [JsonProperty("custom_video")]
            public int CustomVideo { get; set; }

            /// <summary>
            /// 是否需要服务器录制白板数据,大于0表示是
            /// </summary>
            [JsonProperty("data_record")]
            public int DataRecord { get; set; }

            /// <summary>
            /// 是否需要服务器录制音频数据,大于0表示是
            /// </summary>
            [JsonProperty("audio_record")]
            public int AudioRecord { get; set; }

            /// <summary>
            ///     推送用的文本，接收方无效
            /// </summary>
            [JsonProperty("apns")]
            public string ApnsText { get; set; }

            /// <summary>
            ///     自定义数据，透传给被邀请方，接收方无效
            /// </summary>
            [JsonProperty("custom_info")]
            public string CustomInfo { get; set; }

            /// <summary>
            /// 是否需要推送 ,大于0表示是
            /// </summary>
            [JsonProperty("push_enable")]
            public int PushEnable { get; set; }

            /// <summary>
            /// 是否需要角标计数,大于0表示是
            /// </summary>
            [JsonProperty("need_badge")]
            public int NeedBadge { get; set; }

            /// <summary>
            /// 是否需要推送昵称,大于0表示是
            /// </summary>
            [JsonProperty("need_nick")]
            public int NeedNick { get; set; }

            /// <summary>
            /// JSON格式,推送payload
            /// </summary>
            [JsonProperty("payload")]
            public string Payload { get; set; }

            /// <summary>
            /// 推送声音
            /// </summary>
            [JsonProperty("sound")]
            public string PushSound { get; set; }

            /// <summary>
            /// 视频发送编码码率 100000-5000000有效
            /// </summary>
            [JsonProperty("max_video_rate")]
            public int MaxVideoRate { get; set; }

            /// <summary>
            /// 视频画面帧率
            /// </summary>
            [JsonProperty("frame_rate")]
            public NIMVChatVideoFrameRate FrameRate { get; set; }

            /// <summary>
            /// 视频聊天分辨率选择
            /// </summary>
            [JsonProperty("video_quality")]
            public NIMVChatVideoQuality VideoQuality { get; set; }

            /// <summary>
            /// 是否使用语音高清模式,大于0表示是（默认关闭）3.3.0 之前的版本无法加入已经开启高清语音的多人会议
            /// </summary>
            [JsonProperty("high_rate")]
            public int HDAudio { get; set; }

            /// <summary>
            /// 是否强制持续呼叫（对方离线也会呼叫）,1表示是，0表示否。默认是
            /// </summary>
            [JsonProperty("keepcalling")]
            public int KeepCalling { get; set; }

            /// <summary>
            /// 无效已经默认支持
            /// 是否支持webrtc互通（针对点对点中的音频通话）,1表示是，0表示否。默认否 
            /// </summary>
            //[JsonProperty("webrtc")]
            //public int Webrtc { get; set; }
        }

        /// <summary>
        ///     收到本人其他端已经处理的通知
        /// </summary>
        public class RtsSyncAckInfo : NimJsonObject<RtsSyncAckInfo>
        {
            /// <summary>
            ///     客户端类型
            /// </summary>
            [JsonProperty("client_type")]
            public int client { get; set; }
        }

        /// <summary>
        ///     通道连接成功后会返回服务器录制信息
        /// </summary>
        public class RtsConnectInfo : NimJsonObject<RtsConnectInfo>
        {
            private RtsConnectInfo()
            {
                RecordAddr = null;
                RecordFile = null;
            }

            /// <summary>
            ///     录制地址（服务器开启录制时有效）
            /// </summary>
            [JsonProperty("record_addr")]
            public string RecordAddr { get; set; }

            /// <summary>
            ///     录制文件名（服务器开启录制时有效）
            /// </summary>
            [JsonProperty("record_file")]
            public string RecordFile { get; set; }
        }

        public class RtsMemberChangeInfo : NimJsonObject<RtsMemberChangeInfo>
        {
            /// <summary>
            /// 成员退出类型
            /// </summary>
            [JsonProperty("leave_type")]
            public NIMRtsMemberLeftType LeaveType { get; set; }
        }

        public class RtsSendDataInfo: NimJsonObject<RtsMemberChangeInfo>
        {
            /// <summary>
            /// id信息 （指定发送某人，不填则群发）
            /// </summary>
            [JsonProperty("uid")]
            public string RtsUid { get; set; }
        }
    }
}
#endif