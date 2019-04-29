#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NIM
{
    /// <summary>
    /// 事件信息 
    /// </summary>
    public class NIMEventInfo : NimUtility.NimJsonObject<NIMEventInfo>
    {

        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonProperty(PropertyName = "event_type")]
        public int Type { get; set; }

        /// <summary>
        /// 事件值
        /// </summary>
        [JsonProperty(PropertyName = "event_value")]
        public int Value { get; set; }

        /// <summary>
        /// 客户端生成的消息id
        /// </summary>
        [JsonProperty(PropertyName = "msgid_client")]
        public string ClientMsgID { get; set; }


        /// <summary>
        /// 用户自定义事件扩展属性，最长4K
        /// </summary>
        [JsonProperty(PropertyName = "config")]
        public string Config { get; set; }

        /// <summary>
        /// 事件有效期，单位：秒，时间范围：60s到7天
        /// </summary>
        [JsonProperty(PropertyName = "ttl")]
        public long ValidityPeriod { get; set; }

        /// <summary>
        /// 事件广播类型：1:仅在线 2:在线和离线
        /// </summary>
        [JsonProperty(PropertyName = "broadcast_type")]
        public int BroadcastType { get; set; }

        /// <summary>
        /// 0:不同步给自己的其他端，1：同步给自己的其他端
        /// </summary>
        [JsonProperty(PropertyName = "sync_seft")]
        public int Sync { get; set; }

        /// <summary>
        /// 预定义事件的扩展字段（在线状态事件：在线的客户端类型Json)
        /// </summary>
        [JsonProperty(PropertyName = "nim_config")]
        public string NimConfig { get; set; }

        /// <summary>
        /// 客户端不填写
        /// </summary>
        [JsonProperty(PropertyName = "ttltype")]
        public int TTLType { get; set; }

        /// <summary>
        /// 是否需要持久化(可选字段)，默认为需要持久化,0:不需要持久化，1：需要持久化(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "durable")]
        public int Durable { get; set; }

        /// <summary>
        /// 事件发布的时间戳，服务器补充(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "event_time")]
        public long EventTime { get; set; }

        /// <summary>
        /// 服务端生成的消息id(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "msgid_server")]
        public string ServerMsgID { get; set; }

        /// <summary>
        /// 发送客户端类型(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "client_type")]
        public int ClientType { get; set; }

        /// <summary>
        /// 多端配置信息字段，JSON格式{"clent_type":"clent_config","1":"xxx","2":"xxx"}
        /// </summary>
        [JsonProperty(PropertyName = "multi_config")]
        public string MultiPortConfig { get; set; }

        /// <summary>
        /// 事件发布者的accid(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "publisher_accid")]
        public string PublisherID { get; set; }

        /// <summary>
        /// 发送设备id(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "consid")]
        public string DeviceID { get; set; }

        protected override bool IgnoreDefauleValue { get; set; }

        public NIMEventInfo()
        {
            IgnoreDefauleValue = true;
        }

    }

    public class NIMEventConfig: NimUtility.NimJsonObject<NIMEventConfig>
    {
        [JsonProperty(PropertyName = "online")]
        public List<int> OnlineClients { get; set; }
    }

    /// <summary>
    /// 事件订阅信息
    /// </summary>
    public class NIMSubscribeStatus:NimUtility.NimJsonObject<NIMSubscribeStatus>
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public int EventType { get; set; }

        /// <summary>
        /// 订阅有效期，单位：秒，范围：60s到30天
        /// </summary>
        [JsonProperty(PropertyName = "ttl")]
        public long ValidityPeriod { get; set; }

        /// <summary>
        /// 订阅后是否立即同步最新事件
        /// </summary>
        [JsonProperty(PropertyName = "syncevent")]
        public int SyncEvent { get; set; }

        /// <summary>
        /// 被订阅人（事件发布人）的accid(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "publisher_accid")]
        public string PublisherID { get; set; }

        /// <summary>
        /// 订阅人的accid(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "subscribe_accid")]
        public string SubscriberID { get; set; }

        /// <summary>
        /// 订阅时间戳(客户端不填写)
        /// </summary>
        [JsonProperty(PropertyName = "subscribe_time")]
        public long SubscribeTimetag { get; set; }
    }


    /// <summary>
    /// 事件广播类型
    /// </summary>
    public enum NIMEventBroadcastType
    {
        /// <summary>
        ///仅在线 
        /// </summary>
        kNIMEventBroadcastTypeOnline = 1,
        /// <summary>
        ///在线和离线 
        /// </summary>
        kNIMEventBroadcastTypeAll = 2
    }

    /// <summary>
    /// 事件同步类型
    /// </summary>
    public enum NIMEventSyncType
    {
        /// <summary>
        ///事件不同步给自己其他端 
        /// </summary>
        kNIMEventSyncTypeNoSelf = 0,
        /// <summary>
        ///事件同步给自己其他端 
        /// </summary>
        kNIMEventSyncTypeSelf = 1
    }

    /// <summary>
    /// 订阅的事件的同步类型
    /// </summary>
    public enum NIMEventSubscribeSyncEventType
    {
        /// <summary>
        ///订阅后不同步最新事件 
        /// </summary>
        kNIMEventSubscribeSyncTypeUnSync = 0,
        ///订阅后立即同步最新事件
        kNIMEventSubscribeSyncTypeSync = 1
    }

    /// <summary>
    /// 服务器预定义的事件类型
    /// </summary>
    public enum NIMEventType
    {
        /// <summary>
        ///在线状态事件(客户端可发送) 
        /// </summary>
        kNIMEventTypeOnlineState = 1,
        /// <summary>
        /// 同步“订阅事件”事件(客户端不可发送)
        /// </summary>
        kNIMEventTypeSyncEvent = 2,
        /// <summary>
        /// 服务器保留1～99999的事件类型，客户端自定义事件类型需大于99999
        /// </summary>
        kNIMEventTypeCustom = 100000
    }

    /// <summary>
    /// 在线状态事件值
    /// </summary>
    public enum NIMEventOnlineStateValue
    {
        /// <summary>
        ///登录 
        /// </summary>
        kNIMEventOnlineStateValueLogin = 1,
        /// <summary>
        /// 登出
        /// </summary>
        kNIMEventOnlineStateValueLogout = 2,
        /// <summary>
        ///断开连接 
        /// </summary>
        kNIMEventOnlineStateValueDisconnect = 3,
        /// <summary>
        ///在线状态事件服务器保留1～9999的事件值，客户端自定义事件值需大于9999 
        /// </summary>
        kNIMEventOnlineStateValueCustom = 10000,
        /// <summary>
        ///自己的其他端更新了自己端的multi_config信息 
        /// </summary>
        kNIMEventOnlineStateValueUpdateConfig = kNIMEventOnlineStateValueCustom + 1
    }
}

#endif