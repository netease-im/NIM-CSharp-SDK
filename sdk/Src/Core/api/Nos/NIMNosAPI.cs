/** @file NIMNosAPI.cs
  * @brief NIM SDK提供的NOS云存储服务接口 
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author gq
  * @date 2015/12/8
  */

using System;
using NimUtility;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NIM.Nos
{
    public class UploadResultParam
    {
        /// <summary>
        /// 上传文件的id
        /// </summary>
        [JsonProperty("res_id")]
        public string ResId { get; set; }

        /// <summary>
        /// 上传文件的会话id
        /// </summary>
        [JsonProperty("call_id")]
        public string CallId { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("response")]
        public string ResponseMsg { get; set; }
    }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
    public delegate void InitConfigHandler(NIMNosInitConfigResultType type, InitConfigResultParams json_result, string json_extension);
  
   
#endif
    /// <summary>
    ///     下载结果回调
    /// </summary>
    /// <param name="rescode">下载结果，一切正常200</param>
    /// <param name="filePath">下载资源文件本地绝对路径</param>
    /// <param name="callId">如果下载的是消息中的资源，则为消息所属的会话id，否则为空</param>
    /// <param name="resId">如果下载的是消息中的资源，则为消息id，否则为空</param>
    public delegate void DownloadResultHandler(int rescode, string filePath, string callId, string resId);

    /// <summary>
    ///     上传结果回调
    /// </summary>
    /// <param name="rescode">上传结果，一切正常200</param>
    /// <param name="url">url地址</param>
    public delegate void UploadResultHandler(int rescode, string url);

    public delegate void UploadResultHandler2(int rescode, string url, UploadResultParam param);

    /// <summary>
    /// 上传/下载进度回调数据
    /// </summary>
    public class ProgressData
    {
        /// <summary>
        /// 目标url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public NIMIMMessage Message { get; set; }

        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 用户定义数据
        /// </summary>
        public object UserData { get; set; }

        /// <summary>
        /// 当前字节数
        /// </summary>
        public long CurrentSize { get; set; }

        /// <summary>
        /// 数据总字节数
        /// </summary>
        public long TotalSize { get; set; }
    }

    /// <summary>
    ///     传输进度回调
    /// </summary>
    /// <param name="ProgressData">回调数据</param>
    public delegate void ProgressResultHandler(ProgressData prgData);

    class ProgressPair
    {
        public ProgressData Data { get; set; }
        public ProgressResultHandler Action { get; set; }

        public ProgressPair(ProgressData data, ProgressResultHandler action)
        {
            Data = data;
            Action = action;
        }
    }

    public class NosAPI
    {
        private static readonly DownloadCb DownloadCb = DownloadCallback;

        private static readonly DownloadPrgCb DownloadPrgCb = DownloadProgressCallback;

        private static readonly UploadCb UploadCb = UploadCallback;

        private static readonly UploadCb UploadCb2 = UploadCallback2;

        private static readonly UploadPrgCb UploadPrgCb = UploadProgressCallback;


 #if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        private static readonly InitConfigCb InitConfigCbFunc = InitConfigCallback;
        private static void InitConfigCallback(NIMNosInitConfigResultType type, string json_result, string json_extension, IntPtr user_data)
        {
            InitConfigResultParams result_params = InitConfigResultParams.Deserialize(json_result);
            user_data.Invoke<InitConfigHandler>(type, result_params, json_extension);
        }

#endif


        /// <summary>
        ///     注册下载回调，通过注册回调获得http下载结果通知，刷新资源
        /// </summary>
        /// <param name="handler">下载的结果回调</param>
        /// 

        public static void RegDownloadCb(DownloadResultHandler handler)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(handler);
            NosNativeMethods.nim_nos_reg_download_cb(DownloadCb, ptr);
        }

        /// <summary>
        ///     获取资源
        /// </summary>
        /// <param name="msg">消息体,NIMVideoMessage NIMAudioMessage NIMFileMessage等带msg_attach属性的有下载信息的消息</param>
        /// <param name="resHandler">下载的结果回调</param>
        /// <param name="prgHandler">下载进度的回调</param>
        public static void DownloadMedia(NIMIMMessage msg, DownloadResultHandler resHandler, ProgressResultHandler prgHandler, object userData = null)
        {
            ProgressData data = new ProgressData();
            data.Message = msg;
            data.UserData = userData;
            ProgressPair pair = new ProgressPair(data, prgHandler);
            var ptr1 = DelegateConverter.ConvertToIntPtr(resHandler);
            var ptr2 = DelegateConverter.ConvertToIntPtr(pair);
            var msgJson = msg.Serialize();
            NosNativeMethods.nim_nos_download_media(msgJson, DownloadCb, ptr1, DownloadPrgCb, ptr2);
        }

        /// <summary>
        ///     停止获取资源（目前仅对文件消息类型有效）
        /// </summary>
        /// <param name="msg">消息体</param>
        [Obsolete]
        public static void StopDownloadMedia(NIMIMMessage msg)
        {
            var msgJson = msg.Serialize();
            NosNativeMethods.nim_nos_stop_download_media(msgJson);
        }

        /// <summary>
        ///     上传资源
        /// </summary>
        /// <param name="localFile">本地文件的完整路径</param>
        /// <param name="resHandler">上传的结果回调</param>
        /// <param name="prgHandler">上传进度的回调</param>
        public static void Upload(string localFile, UploadResultHandler resHandler, ProgressResultHandler prgHandler, object userData = null)
        {
            ProgressData data = new ProgressData();
            data.FilePath = localFile;
            data.UserData = userData;
            ProgressPair pair = new ProgressPair(data, prgHandler);
            var ptr1 = DelegateConverter.ConvertToIntPtr(resHandler);
            var ptr2 = DelegateConverter.ConvertToIntPtr(pair);
            NosNativeMethods.nim_nos_upload(localFile, UploadCb, ptr1, UploadPrgCb, ptr2);
        }

        /// <summary>
        /// 上传资源
        /// </summary>
        /// <param name="localFile">本地文件的完整路径</param>
        /// <param name="tag">场景标签，主要用于确定文件的保存时间</param>
        /// <param name="resHandler">上传的结果回调</param>
        /// <param name="prgHandler">上传进度的回调</param>
        public static void Upload2(string localFile, string tag, UploadResultHandler resHandler, ProgressResultHandler prgHandler, object userData = null)
        {
            ProgressData data = new ProgressData();
            data.FilePath = localFile;
            data.UserData = userData;
            ProgressPair pair = new ProgressPair(data, prgHandler);
            var ptr1 = DelegateConverter.ConvertToIntPtr(resHandler);
            var ptr2 = DelegateConverter.ConvertToIntPtr(pair);
            NosNativeMethods.nim_nos_upload2(localFile, tag, UploadCb, ptr1, UploadPrgCb, ptr2);
        }

        /// <summary>
        ///     上传资源
        /// </summary>
        /// <param name="localFile">本地文件的完整路径</param>
        /// <param name="resHandler">上传的结果回调</param>
        /// <param name="prgHandler">上传进度的回调</param>
        public static void Upload(string localFile, UploadResultHandler2 resHandler, ProgressResultHandler prgHandler, object userData = null)
        {
            ProgressData data = new ProgressData();
            data.FilePath = localFile;
            data.UserData = userData;
            ProgressPair pair = new ProgressPair(data, prgHandler);
            var ptr1 = DelegateConverter.ConvertToIntPtr(resHandler);
            var ptr2 = DelegateConverter.ConvertToIntPtr(pair);
            NosNativeMethods.nim_nos_upload(localFile, UploadCb2, ptr1, UploadPrgCb, ptr2);
        }



        /// <summary>
        ///     下载资源
        /// </summary>
        /// <param name="nosUrl">下载资源的URL</param>
        /// <param name="resHandler">下载的结果回调</param>
        /// <param name="prgHandler">下载进度的回调</param>
        public static void Download(string nosUrl, DownloadResultHandler resHandler, ProgressResultHandler prgHandler, object userData = null)
        {
            ProgressData data = new ProgressData();
            data.Url = nosUrl;
            data.UserData = userData;
            ProgressPair pair = new ProgressPair(data, prgHandler);
            var ptr1 = DelegateConverter.ConvertToIntPtr(resHandler);
            var ptr2 = DelegateConverter.ConvertToIntPtr(pair);

            NosNativeMethods.nim_nos_download(nosUrl, DownloadCb, ptr1, DownloadPrgCb, ptr2);
        }

        [MonoPInvokeCallback(typeof(DownloadCb))]
        private static void DownloadCallback(int rescode, string filePath, string callId, string resId, string jsonExtension, IntPtr userData)
        {
            userData.Invoke<DownloadResultHandler>(rescode, filePath, callId, resId);
        }



        [MonoPInvokeCallback(typeof(DownloadPrgCb))]
        private static void DownloadProgressCallback(long curSize, long fileSize, string jsonExtension, IntPtr userData)
        {
            var pair = DelegateConverter.ConvertFromIntPtr<ProgressPair>(userData);
            if (pair != null)
            {
                pair.Data.CurrentSize = curSize;
                pair.Data.TotalSize = fileSize;
                if (pair.Action != null)
                {
                    pair.Action(pair.Data);
                }
            }
        }

        [MonoPInvokeCallback(typeof(UploadCb))]
        private static void UploadCallback(int rescode, string url, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<UploadResultHandler>(rescode, url);
        }

        [MonoPInvokeCallback(typeof(UploadCb))]
        private static void UploadCallback2(int rescode, string url, string jsonExtension, IntPtr userData)
        {
            var param = NimUtility.Json.JsonParser.Deserialize<UploadResultParam>(jsonExtension);
            userData.InvokeOnce<UploadResultHandler2>(rescode, url, param);
        }

        [MonoPInvokeCallback(typeof(UploadPrgCb))]
        private static void UploadProgressCallback(long curSize, long fileSize, string jsonExtension, IntPtr userData)
        {
            var pair = DelegateConverter.ConvertFromIntPtr<ProgressPair>(userData);
            if (pair != null)
            {
                pair.Data.CurrentSize = curSize;
                pair.Data.TotalSize = fileSize;
                if (pair.Action != null)
                {
                    pair.Action(pair.Data);
                }
            }
        }

        private class CallbackDataPair
        {
            public Delegate Callback { get; set; }
            public IntPtr Data { get; set; }

            public CallbackDataPair() { }
            public CallbackDataPair(Delegate cb, IntPtr ptr)
            {
                Callback = cb;
                Data = ptr;
            }

            public IntPtr ToIntPtr()
            {
                return DelegateConverter.ConvertToIntPtr(this);
            }

            public static CallbackDataPair FromIntPtr(IntPtr ptr)
            {
                var obj = DelegateConverter.ConvertFromIntPtr(ptr);
                var pair = obj as CallbackDataPair;
                return pair;
            }
        }  

        private static DownloadCb DownloadResultCallbackEx = OnDownloadCompleted;

        private static void OnDownloadCompleted(int rescode, string file_path, string call_id, string res_id, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((DownloadCb)pair.Callback)(rescode, file_path, call_id, res_id, json_extension, pair.Data);
        }

        private static DownloadPrgCb DownloadPrgCallbackEx = OnReportDownloadPrg;

        private static void OnReportDownloadPrg(long downloaded_size, long file_size, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((DownloadPrgCb)pair.Callback)(downloaded_size, file_size, json_extension, pair.Data);
        }

        private static DownloadSpeedCb DownloadSpeedCallbackEx = OnReportDownloadSpeed;

        private static void OnReportDownloadSpeed(long download_speed, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((DownloadSpeedCb)pair.Callback)(download_speed, json_extension, pair.Data);
        }

        private static DownloadInfoCb DownloadInfoCallbackEx = OnReportDownloadInfo;

        private static void OnReportDownloadInfo(long actual_download_size, long download_speed, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((DownloadInfoCb)pair.Callback)(actual_download_size, download_speed, json_extension, pair.Data);
        }  

        private static UploadCb UploadCallbackEx = OnUploadCompleted;

        private static void OnUploadCompleted(int rescode, string url, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((UploadCb)pair.Callback)(rescode, url, json_extension, pair.Data);
        }

        private static UploadPrgCb UploadPrgCallbackEx = OnReportUploadProgress;

        private static void OnReportUploadProgress(long uploaded_size, long file_size, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((UploadPrgCb)pair.Callback)(uploaded_size, file_size, json_extension, pair.Data);
        }

        private static UploadSpeedCb UploadSpeedCallbackEx = OnReportUploadSpeed;

        private static void OnReportUploadSpeed(long upload_speed, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((UploadSpeedCb)pair.Callback)(upload_speed, json_extension, pair.Data);
        }

        private static UploadInfoCb UploadInfoCallbackEx = OnReportUploadInfo;

        private static void OnReportUploadInfo(long actual_upload_size, long upload_speed, string json_extension, IntPtr user_data)
        {
            var pair = CallbackDataPair.FromIntPtr(user_data);
            if (pair.Callback != null)
                ((UploadInfoCb)pair.Callback)(actual_upload_size, upload_speed, json_extension, pair.Data);
        }
        
        /// <summary>
        /// (全局回调)注册上传回调，通过注册回调获得HTTP上传结果通知（所有触发HTTP上传任务的接口的参数列表里无法设置通知回调处理函数的通知都走这个通知，比如发送文件图片语音消息等）
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="data"></param>
        public static void RegisterDefaultUploadCallback(UploadCb callback, IntPtr data)
        {
            CallbackDataPair pair = new CallbackDataPair(callback, data);
            NosNativeMethods.nim_nos_reg_upload_cb(UploadCallbackEx, pair.ToIntPtr());
        }

        /// <summary>
        /// 设置是否支持快速上传
        /// </summary>
        /// <param name="trans">0：不支持，1：支持</param>
        public static void SetQuickTrans(int trans)
        {
            NosNativeMethods.nim_nos_set_quick_trans(trans);
        }

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// Nos模块初始化接口，对上传资源时使用的各场景资源生命周期进行初始化，开发者最多可自定义10个场景，并指定场景资源的生命周期，并可以对缺省场景（kNIMNosDefaultTagResource、kNIMNosDefaultTagIM）进行覆盖（重新指定生命周期）
        /// </summary>
        /// <param name="json_tags">tags 标签（json形式）</param>
        /// <param name="handler">回调函数</param>
        /// <param name="json_extension">拓展参数</param>
        public static void InitConfig(List<InitConfigParams> tags, InitConfigHandler handler, string json_extension=null)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(handler);
            string json_tags = "";
            if(tags!=null)
            {
                json_tags = JsonConvert.SerializeObject(tags);
            }
            NosNativeMethods.nim_nos_init_config(json_tags, InitConfigCbFunc, json_extension, ptr);
        }

        /// <summary>
        /// 上传资源
        /// </summary>
        /// <param name="localFile">本地文件的完整路径</param>
        /// <param name="tag">场景标签，主要用于确定文件的保存时间</param>
        /// <param name="resHandler">上传的结果回调</param>
        /// <param name="prgHandler">上传进度的回调</param>
        public static void Upload2(string localFile, string tag, UploadResultHandler2 resHandler, ProgressResultHandler prgHandler, object userData = null)
        {
            ProgressData data = new ProgressData();
            data.FilePath = localFile;
            data.UserData = userData;
            ProgressPair pair = new ProgressPair(data, prgHandler);
            var ptr1 = DelegateConverter.ConvertToIntPtr(resHandler);
            var ptr2 = DelegateConverter.ConvertToIntPtr(pair);
            NosNativeMethods.nim_nos_upload2(localFile, tag, UploadCb2, ptr1, UploadPrgCb, ptr2);
        }

        /// <summary>
        /// 停止上传资源(只能用于调用了nim_nos_upload_ex接口的上传任务)
        /// </summary>
        /// <param name="taskId">停止上传任务的ID</param>
        /// <param name="ext"></param>
        public static void StopUploadEx(string taskId, string ext = null)
        {
            NosNativeMethods.nim_nos_stop_upload_ex(taskId, ext);
        }

        /// <summary>
        /// 停止下载资源(只能用于调用了nim_nos_download_ex接口的下载任务)
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="ext"></param>
        public static void StopDownloadEx(string taskId, string ext = null)
        {
            NosNativeMethods.nim_nos_stop_download_ex(taskId, ext);
        }

        /// <summary>
        /// 上传资源(扩展)
        /// </summary>
        /// <param name="localFile">本地文件的完整路径</param>
        /// <param name="param">扩展参数</param>
        /// <param name="resCb"></param>
        /// <param name="resData"></param>
        /// <param name="prgCb"></param>
        /// <param name="prgData"></param>
        /// <param name="speedCb"></param>
        /// <param name="speedData"></param>
        /// <param name="infoCb"></param>
        /// <param name="infoData"></param>
        public static void UploadEx(string localFile,
            HttpExtendedParameters param,
            UploadCb resCb, IntPtr resData,
            UploadPrgCb prgCb, IntPtr prgData,
            UploadSpeedCb speedCb, IntPtr speedData,
            UploadInfoCb infoCb, IntPtr infoData)
        {
            CallbackDataPair resPair = new CallbackDataPair(resCb, resData);
            CallbackDataPair prgPair = new CallbackDataPair(prgCb, prgData);
            CallbackDataPair speedPair = new CallbackDataPair(speedCb, speedData);
            CallbackDataPair infoPair = new CallbackDataPair(infoCb, infoData);

            NosNativeMethods.nim_nos_upload_ex(localFile,
                param != null ? param.Serialize() : string.Empty,
                UploadCallbackEx, resPair.ToIntPtr(),
                UploadPrgCallbackEx, prgPair.ToIntPtr(),
                UploadSpeedCallbackEx, speedPair.ToIntPtr(),
                UploadInfoCallbackEx, infoPair.ToIntPtr());
        }

        /// <summary>
        /// 上传资源(扩展)
        /// </summary>
        /// <param name="localFile">本地文件的完整路径</param>
        /// <param name="tag">场景标签，主要用于确定文件的保存时间</param>
        /// <param name="param">参数配置</param>
        /// <param name="resCb">上传的回调函数</param>
        /// <param name="resData">APP的自定义用户数据，SDK只负责传回给回调函数resCb，不做任何处理！</param>
        /// <param name="prgCb">上传进度的回调函数</param>
        /// <param name="prgData">APP的自定义用户数据，SDK只负责传回给回调函数prgCb，不做任何处理！</param>
        /// <param name="speedCb">上传速度的回调函数</param>
        /// <param name="speedData">peed_user_data APP的自定义用户数据，SDK只负责传回给回调函数speedCb，不做任何处理！</param>
        /// <param name="infoCb">info_cb 返回最终上传信息的回调函数</param>
        /// <param name="infoData"> APP的自定义用户数据，SDK只负责传回给回调函数infoCb，不做任何处理！</param>
        public static void UploadEx2(string localFile,
            string tag,
            HttpExtendedParameters param,
            UploadCb resCb, IntPtr resData,
            UploadPrgCb prgCb, IntPtr prgData,
            UploadSpeedCb speedCb, IntPtr speedData,
            UploadInfoCb infoCb, IntPtr infoData)
        {
            CallbackDataPair resPair = new CallbackDataPair(resCb, resData);
            CallbackDataPair prgPair = new CallbackDataPair(prgCb, prgData);
            CallbackDataPair speedPair = new CallbackDataPair(speedCb, speedData);
            CallbackDataPair infoPair = new CallbackDataPair(infoCb, infoData);

            NosNativeMethods.nim_nos_upload_ex2(localFile,tag,
                param != null ? param.Serialize() : string.Empty,
                UploadCallbackEx, resPair.ToIntPtr(),
                UploadPrgCallbackEx, prgPair.ToIntPtr(),
                UploadSpeedCallbackEx, speedPair.ToIntPtr(),
                UploadInfoCallbackEx, infoPair.ToIntPtr());
        }


        /// <summary>
        /// 获取资源(扩展)
        /// </summary>
        /// <param name="msg">包含附件的消息体</param>
        ///<param name="param">下载扩展信息</param> 
        /// <param name="resCb"></param>
        /// <param name="resData"></param>
        /// <param name="prgCb"></param>
        /// <param name="prgData"></param>
        /// <param name="speedCb"></param>
        /// <param name="speedData"></param>
        /// <param name="infoCb"></param>
        /// <param name="infoData"></param>
        public static void DownloadMediaEx(NIMIMMessage msg,
            HttpExtendedParameters param,
            DownloadCb resCb, IntPtr resData,
            DownloadPrgCb prgCb, IntPtr prgData,
            DownloadSpeedCb speedCb, IntPtr speedData,
            DownloadInfoCb infoCb, IntPtr infoData)
        {
            var msgJson = msg.Serialize();
            CallbackDataPair resPair = new CallbackDataPair(resCb, resData);
            CallbackDataPair prgPair = new CallbackDataPair(prgCb, prgData);
            CallbackDataPair speedPair = new CallbackDataPair(speedCb, speedData);
            CallbackDataPair infoPair = new CallbackDataPair(infoCb, infoData);
            NosNativeMethods.nim_nos_download_media_ex(msgJson,
                param == null ? null : param.Serialize(),
                DownloadResultCallbackEx, resPair.ToIntPtr(),
                DownloadPrgCallbackEx, prgPair.ToIntPtr(),
                DownloadSpeedCallbackEx, speedPair.ToIntPtr(),
                DownloadInfoCallbackEx, infoPair.ToIntPtr());
        }

        /// <summary>
        ///下载资源(扩展) 
        /// </summary>
        /// <param name="url">下载资源的URL</param>
        /// <param name="param">http 扩展参数</param>
        /// <param name="resCb">下载结果的回调函数</param>
        /// <param name="resData">下载结果回调自定义用户数据，</param>
        /// <param name="prgCb">下载进度的回调函数</param>
        /// <param name="prgData">进度回调自定义用户数据</param>
        /// <param name="speedCb">下载速度的回调函数</param>
        /// <param name="speedData">下载速度回调自定义用户数据</param>
        /// <param name="infoCb">返回最终下载信息的回调函数</param>
        /// <param name="infoData">下载信息回调的自定义用户数据</param>
        public static void DownloadEx(string url,
            HttpExtendedParameters param,
            DownloadCb resCb, IntPtr resData,
            DownloadPrgCb prgCb, IntPtr prgData,
            DownloadSpeedCb speedCb, IntPtr speedData,
            DownloadInfoCb infoCb, IntPtr infoData)
        {
            CallbackDataPair resPair = new CallbackDataPair(resCb, resData);
            CallbackDataPair prgPair = new CallbackDataPair(prgCb, prgData);
            CallbackDataPair speedPair = new CallbackDataPair(speedCb, speedData);
            CallbackDataPair infoPair = new CallbackDataPair(infoCb, infoData);

            NosNativeMethods.nim_nos_download_ex(url,
                param != null ? param.Serialize() : string.Empty,
                DownloadResultCallbackEx, resPair.ToIntPtr(),
                DownloadPrgCallbackEx, prgPair.ToIntPtr(),
                DownloadSpeedCallbackEx, speedPair.ToIntPtr(),
                DownloadInfoCallbackEx, infoPair.ToIntPtr());
        }

#endif
        private static InitConfigCb NosInitCallback = OnNosInitCompleted;

        public delegate void NIMNosInitResultCallback(NIMNosInitResult result);

        private static void OnNosInitCompleted(NIMNosInitConfigResultType rescode, string json_result, string json_extension, IntPtr user_data)
        {
            NIMNosInitResult initResult = NIMNosInitResult.Deserialize(json_result);
            DelegateConverter.InvokeOnce<NIMNosInitResultCallback>(user_data, initResult);
        }

        public static void InitNosTags(List<NIMNosTagInfo> tags, NIMNosInitResultCallback cb)
        {
            var json = NimUtility.Json.JsonParser.Serialize(tags);
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NosNativeMethods.nim_nos_init_config(json, NosInitCallback, null, ptr);
        }

        private static SafeUrlConverterCb SafeUrlConverterCallback = OnConvertSafeUrlResult;

        private static void OnConvertSafeUrlResult(int rescode, string originUrl, IntPtr userData)
        {
            DelegateConverter.InvokeOnce<SafeUrlConverterCb>(userData, rescode, originUrl, IntPtr.Zero);
        }

        /// <summary>
        /// 安全链接(短链)换源链接
        /// </summary>
        /// <param name="safeUrl">安全链接(短链)</param>
        /// <param name="cb"></param>
        public static void ConvertSafeUrlToOrigin(string safeUrl, SafeUrlConverterCb cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            NosNativeMethods.nim_nos_safeurl_to_originurl(safeUrl, null, SafeUrlConverterCallback,ptr);
        }
    }

    public class NIMNosInitResult:NimJsonObject<NIMNosInitResult>
    {
        /// <summary>
        /// 初始化成功了的tag
        /// </summary>
        [JsonProperty("nim_nos_init_config_succeed")]
        public List<string> Succeed { get; set; }

        /// <summary>
        /// 初始化失败了的tag
        /// </summary>
        [JsonProperty("nim_nos_init_config_failure")]
        public List<string> Failure { get; set; }

        /// <summary>
        /// 因为指定的survival_time 相同而被忽略了的tag
        /// </summary>
        [JsonProperty("nim_nos_init_config_ignore")]
        public List<string> Ignore { get; set; }

        /// <summary>
        /// 初始化tag失败时的错误码
        /// </summary>
        [JsonProperty("kNIMNosInitConfigErrcode")]
        public int Error { get; set; }

        /// <summary>
        /// 初始化结果
        /// </summary>
        [JsonProperty("nim_nos_init_config_retcode")]
        public NIMNosInitConfigResultType Result { get; set; }
    }

    public class NIMNosTagInfo:NimJsonObject<NIMNosTagInfo>
    {
        /// <summary>
        /// tag的名称
        /// </summary>
        [JsonProperty("nim_nos_tag_name")]
        public string Name { get; set; }

        /// <summary>
        /// 资源所对应的tag生命周期
        /// </summary>
        [JsonProperty("nim_nos_tag_survival_time")]
        public int ExpirSeconds { get; set; }
    }

    public enum NIMNosUploadType
    {
        /// <summary>
        ///普通文件上传 
        /// </summary>
        kNIMNosUploadTypeNormal = 0,

        /// <summary>
        ///文档转换上传 
        /// </summary>
        kNIMNosUploadTypeDocTrans = 1
    }

    /// <summary>
    /// NOS扩展上传\下载接口参数
    /// </summary>
    public class HttpExtendedParameters : NimJsonObject<HttpExtendedParameters>
    {
        /// <summary>
        /// HTTP通用配置，传输速度，每秒字节数（默认10）
        /// </summary>
        [JsonProperty("low_limit")]
        public int LowTrafficRate { get; set; }

        /// <summary>
        /// HTTP通用配置，传输过程中当LowTrafficTimeLimit秒时间内传输速度小于LowTrafficRate时(字节每秒)，下载任务会返回超时而取消（默认60）
        /// </summary>
        [JsonProperty("low_time")]
        public int LowTrafficTimeLimit { get; set; }

        /// <summary>
        /// HTTP通用配置，超时时间，单位ms，最小1000
        /// </summary>
        [JsonProperty("timeout")]
        public int TimeoutMS { get; set; }

        /// <summary>
        /// HTTP通用配置，任务UUID，上传下载断点续传必填，如果传入的ID是曾经未完成的传输任务，则会开始续传（用户需要保证ID的唯一性）
        /// </summary>
        [JsonProperty("task_id")]
        public string TaskID { get; set; }

        /// <summary>
        /// HTTP通用配置，任务是否需要续传功能
        /// </summary>
        [JsonProperty("continue_trans")]
        public bool ContinueTrans { get; set; }

        /// <summary>
        /// HTTP下载任务的文件大小，需要续传功能必填，单位kb，其他情况不需要填
        /// </summary>
        [JsonProperty("download_filesize")]
        public long DownloadedSize { get; set; }

        /// <summary>
        /// HTTP下载任务的文件存放本地路径，不填则默认路径回调中返回
        /// </summary>
        [JsonProperty("saveas_filepath")]
        public string SaveasPath { get; set; }

        /// <summary>
        /// HTTP上传任务的类型
        /// </summary>
        [JsonProperty("upload_type")]
        public NIMNosUploadType UploadType { get; set; }

    }


#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
    /// <summary>
    /// HTTP上传转码文档参数
    /// </summary>
    public class DocTransitionParams : HttpExtendedParameters
    {
        /// <summary>
        /// (HTTP上传转码文档使用)名称
        /// </summary>
        [JsonProperty("name")]
        public string TransitionFileName { get; set; }

        /// <summary>
        /// (HTTP上传转码文档使用)转码源文档的文件类型
        /// </summary>
        [JsonProperty("source_type")]
        public DocTransition.NIMDocTranscodingFileType SourceType { get; set; }

        /// <summary>
        /// (HTTP上传转码文档使用)转码目标图片的文件类型
        /// </summary>
        [JsonProperty("pic_type")]
        public DocTransition.NIMDocTranscodingImageType PictureType { get; set; }

        /// <summary>
        /// (HTTP上传转码文档使用)文档转换时的扩展参数，在成功后能查询到
        /// </summary> 
        [JsonProperty("doc_trans_ext")]
        public string DocTransitionExt { get; set; }

        /// <summary>
        /// 上传文件时使用的场景标签(可参见nos删除策略)
        /// </summary>
        [JsonProperty("upload_tag")]
        public string UploadTag { get; set; }
        public DocTransitionParams()
        {
            UploadType = NIMNosUploadType.kNIMNosUploadTypeDocTrans;
        }

        /** @name NOS扩展上传回调参数json_extension, Json key for upload cb */
        //static const char* kNIMNosResId = "res_id";     /**< string 上传文件的id，如果是文档转换则为服务器的文档id */
        /** @}*/ //NOS扩展上传回调参数json_extension, Json key for upload cb
    }

    public class InitConfigParams : NimJsonObject<InitConfigParams>
    {
        [JsonProperty("nim_nos_tag_name")]
        public string TagName { get; set; }

        [JsonProperty("nim_nos_tag_survival_time")]
        public Int32 TagSurvivalTime { get; set; }
    }


    public class InitConfigResultParams: NimJsonObject<InitConfigResultParams>
    {
        public class InitConfigError: NimJsonObject<InitConfigError>
        {
            [JsonProperty("nim_nos_tag_name")]
            public string TagName { get; set; }

            [JsonProperty("nim_nos_init_config_errcode")]
            public int ConfigErrcode { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("nim_nos_init_config_succeed")]
        public List<string> ConfigSucceed { get; set; }

        [JsonProperty("nim_nos_init_config_failure")]
        public List<InitConfigError> ConfigFailure { get; set; }

        [JsonProperty("nim_nos_init_config_ignore")]
        public List<string> ConfigIgnore { get; set; }


        [JsonProperty("nim_nos_init_config_retcode")]
        public int ConfigRetcode { get; set; }
    }
#endif
}