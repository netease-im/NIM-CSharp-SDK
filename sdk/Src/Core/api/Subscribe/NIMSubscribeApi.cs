#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NimUtility;

namespace NIM
{
    public class NIMSubscribeApi
    {
        /// <summary>
        /// 接收订阅的事件的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="info">事件信息</param>
        public delegate void PushEventDelegate(ResponseCode code,NIMEventInfo info);

        /// <summary>
        /// 批量接收订阅的事件的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="infoList"事件信息列表</param>
        public delegate void BatchPushEventDelegaet(ResponseCode code,List<NIMEventInfo> infoList);

        /// <summary>
        /// 发布事件的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="info">发布的事件信息</param>
        public delegate void PublishEventDelegate(ResponseCode code,NIMEventInfo info);

        /// <summary>
        /// 订阅事件的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="type">订阅的事件类型</param>
        /// <param name="failedIDList">订阅失败的帐号列表</param>
        public delegate void SubscribeEventDelegate(ResponseCode code,int type,List<string> failedIDList);

        /// <summary>
        /// 按账号取消指定事件的订阅关系的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="type">取消订阅的事件类型</param>
        /// <param name="failedIDList">取消订阅失败的帐号列表</param>
        public delegate void UnSubscribeEventDelegate(ResponseCode code,int type,List<string> failedIDList);

        /// <summary>
        /// 取消指定事件的全部订阅关系的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="type">取消的事件类型</param>
        public delegate void BatchUnscribeEventDelegate(ResponseCode code,int type);

        /// <summary>
        /// 查询指定事件的全部订阅关系的回调函数定义
        /// </summary>
        /// <param name="code">错误码</param>
        /// <param name="subscribeList">订阅关系信息列表</param>
        public delegate void QuerySubscribeDelegate(ResponseCode code,List<NIMSubscribeStatus> subscribeList);

        /// <summary>
        /// (全局回调)统一注册接收订阅的事件的回调函数
        /// </summary>
        /// <param name="cb"></param>
        public static void RegPushEventCb(PushEventDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_subscribe_event_reg_push_event_cb("", PushEventCallback, ptr);
        }

        private static readonly nim_push_event_cb_func PushEventCallback = NIMPushEventCbFunc;

        private static void NIMPushEventCbFunc(int res_code, string event_info_json, string json_extension, IntPtr user_data)
        {
            if(user_data != IntPtr.Zero)
            {
                var info = NIMEventInfo.Deserialize(event_info_json);
                DelegateConverter.Invoke<PushEventDelegate>(user_data, (ResponseCode)res_code, info);
            }
        }

        /// <summary>
        /// (全局回调)统一注册批量接收订阅的事件的回调函数
        /// </summary>
        /// <param name="cb"></param>
        public static void RegBatchPushEventCb(BatchPushEventDelegaet cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_subscribe_event_reg_batch_push_event_cb(null, BatchPushEventCallback, ptr);
        }

        private static readonly nim_batch_push_event_cb_func BatchPushEventCallback = NIMBatchPushEventCbFunc;
        private static readonly nim_publish_event_cb_func PublishResultCallback = NIMPublishResultCbFunc;
        private static readonly nim_subscribe_event_cb_func SubscribeEventCallback = NIMSubscribeResultCbFunc;
        private static readonly nim_unsubscribe_event_cb_func UnsubscribeEventCallback = NIMUnsubscribeResultCbFunc;
        private static readonly nim_batch_unsubscribe_event_cb_func BatchUnScribeCallback = NIMBatchUnsubscribeResultCbFunc;
        private static readonly nim_query_subscribe_event_cb_func QuerySubscribeCallback = NIMQuerySubscribeResultCbFunc;
        private static readonly nim_batch_query_subscribe_event_cb_func BatchQuerySubscribeCallback = NIMBatchQueryResultCbFunc;

        private static void NIMBatchQueryResultCbFunc(int res_code, int event_type, string subscribe_list_json, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                var list = NimUtility.Json.JsonParser.Deserialize<List<NIMSubscribeStatus>>(subscribe_list_json);
                DelegateConverter.InvokeOnce<QuerySubscribeDelegate>(user_data, (ResponseCode)res_code, list);
            }
        }

        private static void NIMQuerySubscribeResultCbFunc(int res_code, int event_type, string subscribe_list_json, string json_extension, IntPtr user_data)
        {
            if(user_data != IntPtr.Zero)
            {
                var list = NimUtility.Json.JsonParser.Deserialize<List<NIMSubscribeStatus>>(subscribe_list_json);
                DelegateConverter.InvokeOnce<QuerySubscribeDelegate>(user_data, (ResponseCode)res_code, list);
            }
        }

        private static void NIMBatchUnsubscribeResultCbFunc(int res_code, int event_type, string json_extension, IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<BatchUnscribeEventDelegate>(user_data, (ResponseCode)res_code, event_type);
        }

        private static void NIMUnsubscribeResultCbFunc(int res_code, int event_type, string faild_list_json, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                List<string> failed = NimUtility.Json.JsonParser.Deserialize<List<string>>(faild_list_json);
                DelegateConverter.InvokeOnce<UnSubscribeEventDelegate>(user_data, (ResponseCode)res_code, event_type, failed);
            }
        }

        private static void NIMSubscribeResultCbFunc(int res_code, int event_type, string faild_list_json, string json_extension, IntPtr user_data)
        {
           if(user_data != IntPtr.Zero)
            {
                List<string> failed = NimUtility.Json.JsonParser.Deserialize<List<string>>(faild_list_json);
                DelegateConverter.InvokeOnce<SubscribeEventDelegate>(user_data, (ResponseCode)res_code, event_type, failed);
            } 
        }

        private static void NIMPublishResultCbFunc(int res_code, int event_type, string event_info_json, string json_extension, IntPtr user_data)
        {
            if(user_data != IntPtr.Zero)
            {
                var info = NIMEventInfo.Deserialize(event_info_json);
                DelegateConverter.InvokeOnce<PublishEventDelegate>(user_data, (ResponseCode)res_code, info);
            }
        }

        private static void NIMBatchPushEventCbFunc(int res_code, string event_list_json, string json_extension, IntPtr user_data)
        {
            if (user_data != IntPtr.Zero)
            {
                var info = NimUtility.Json.JsonParser.Deserialize<List<NIMEventInfo>>(event_list_json);
                DelegateConverter.Invoke<BatchPushEventDelegaet>(user_data, (ResponseCode)res_code, info);
            }
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <param name="info">事件信息</param>
        /// <param name="cb"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Publish(NIMEventInfo info, PublishEventDelegate cb,object data = null)
        {
            if((info.Type == (int)NIMEventType.kNIMEventTypeOnlineState && info.Value >= (int)NIMEventOnlineStateValue.kNIMEventOnlineStateValueCustom) ||
                info.Value >= (int)NIMEventType.kNIMEventTypeCustom)
            {
                var ptr = DelegateConverter.ConvertToIntPtr(cb);
                var json = info.Serialize();
                NIMSubscribeNativeMethods.nim_publish_event(json, null, PublishResultCallback, ptr);
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="period">订阅有效期，单位：秒，范围：60s到30天</param>
        /// <param name="syncType">订阅后是否立即同步最新事件，见NIMEventSubscribeSyncType定义</param>
        /// <param name="idList"用户列表></param>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool Subscribe(int eventType,long period, NIMEventSubscribeSyncEventType syncType,List<string> idList,SubscribeEventDelegate cb)
        {
            if (eventType == 0 || period == 0 || idList == null || idList.Count > 100)
                return false;
            var json = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_subscribe_event(eventType, period, (int)syncType, json, null, SubscribeEventCallback, ptr);
            return true;
        }

        /// <summary>
        /// 按账号取消指定事件的订阅关系
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="idList">用户列表</param>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool UnSubscribe(int eventType,List<string> idList, UnSubscribeEventDelegate cb)
        {
            if (eventType == 0 || idList == null || idList.Count > 100)
                return false;
            var json = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_unsubscribe_event(eventType, json, null, UnsubscribeEventCallback, ptr);
            return true;
        }

        /// <summary>
        /// 取消指定事件的全部订阅关系
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool BatchUnSubscribe(int eventType, BatchUnscribeEventDelegate cb)
        {
            if (eventType == 0)
                return false;
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_batch_unsubscribe_event(eventType, null, BatchUnScribeCallback, ptr);
            return true;
        }

        /// <summary>
        /// 按账号查询指定事件订阅关系
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="idList">用户列表</param>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool QuerySubscribe(int eventType,List<string> idList, QuerySubscribeDelegate cb)
        {
            if (eventType == 0 || idList == null || idList.Count > 100)
                return false;
            var json = NimUtility.Json.JsonParser.Serialize(idList);
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_query_subscribe_event(eventType, json, null, QuerySubscribeCallback, ptr);
            return true;
        }

        /// <summary>
        /// 查询指定事件的全部订阅关系
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="cb"></param>
        /// <returns></returns>
        public static bool BatchQuerySubscribe(int eventType, QuerySubscribeDelegate cb)
        {
            if (eventType == 0)
                return false;
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NIMSubscribeNativeMethods.nim_batch_query_subscribe_event(eventType, null, BatchQuerySubscribeCallback, ptr);
            return true;
        }
    }
}
#endif