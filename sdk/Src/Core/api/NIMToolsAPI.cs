
using NimUtility;
/** @file NIMToolsAPI.cs
* @brief NIM SDK提供的一些工具接口，主要包括获取SDK里app account对应的app data目录，计算md5等
* @copyright (c) 2015, NetEase Inc. All rights reserved
* @author Harrison
* @date 2015/12/8
*/
using System;
using System.Runtime.InteropServices;

namespace NIM
{
    public class ToolsAPI
    {
        /// <summary>
        /// 语音转文字结果委托
        /// </summary>
        /// <param name="rescode">错误码</param>
        /// <param name="text">转换后的文字</param>
        /// <param name="userData">自定义数据</param>
        public delegate void Audio2TextDelegate(int rescode, string text, object userData); 

        /// <summary>
        ///     获取SDK里app account对应的app data目录（各个帐号拥有独立的目录，其父目录相同）
        /// </summary>
        /// <param name="appAccount">APP account。如果传入空字符串，则将获取到各个帐号目录的父目录（谨慎删除！）</param>
        /// <returns>返回的目录路径</returns>
        public static string GetUserAppDataDir(string appAccount)
        {
            var outStrPtr = nim_tool_get_user_appdata_dir(appAccount);
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     获取SDK里app account对应的具体类型的app data目录（如图片消息文件存放目录，语音消息文件存放目录等）
        /// </summary>
        /// <param name="appAccount">APP account。如果传入空字符串，则返回结果为空</param>
        /// <param name="appdataType">具体类型的app data。见NIMAppDataType定义</param>
        /// <returns>返回的目录路径（目录可能未生成，需要app自行判断是否已生成）</returns>
        public static string GetUserSpecificAppDataDir(string appAccount, NIMAppDataType appdataType)
        {
            var outStrPtr = nim_tool_get_user_specific_appdata_dir(appAccount, appdataType);
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     获取本地存储路径
        /// </summary>
        /// <returns>返回的目录路径</returns>
        public static string GetLocalAppDataDir()
        {
            var outStrPtr = nim_tool_get_local_appdata_dir();
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     获取安装目录（SDK DLL所在的当前目录）
        /// </summary>
        /// <returns>返回的目录路径</returns>
        public static string GetCurModuleDir()
        {
            var outStrPtr = nim_tool_get_cur_module_dir();
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     计算md5
        /// </summary>
        /// <param name="input">需要计算md5的内容</param>
        /// <returns>返回的md5</returns>
        public static string GetMd5(string input)
        {
            var outStrPtr = nim_tool_get_md5(input);
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     计算文件的md5
        /// </summary>
        /// <param name="filePath">文件完整路径</param>
        /// <returns>返回的md5</returns>
        public static string GetFileMd5(string filePath)
        {
            var outStrPtr = nim_tool_get_file_md5(filePath);
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     生成UUID
        /// </summary>
        /// <returns>返回的UUID</returns>
        public static string GetUuid()
        {
            var outStrPtr = nim_tool_get_uuid();
            NimUtility.Utf8StringMarshaler marshaler = new NimUtility.Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(outStrPtr) as string;
            GlobalAPI.FreeStringBuffer(outStrPtr);
            return ret;
        }

        /// <summary>
        ///     语音转文字
        /// </summary>
        /// <param name="audioInfo">语音信息</param>
        /// <param name="jsonExtension">json_extension json扩展参数（备用，目前不需要）</param>
        /// <param name="cb">语音转文字回调</param>
        /// <param name="userData">APP的自定义用户数据，SDK只负责传回给回调函数cb，不做任何处理！</param>
        public static void GetAudioTextAsync(NIMAudioInfo audioInfo, string jsonExtension, NIMTools.GetAudioTextCb cb)
        {
            var json = audioInfo.Serialize();
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            nim_tool_get_audio_text_async(json, jsonExtension, ConverteAudio2TextDelegate, ptr);
        }

        private static readonly NIMTools.GetAudioTextCb ConverteAudio2TextDelegate = OnConverteAudio2TextCompleted;

        [MonoPInvokeCallback(typeof(NIMTools.GetAudioTextCb))]
        private static void OnConverteAudio2TextCompleted(int rescode, string text, string json_extension, IntPtr user_data)
        {
            NimUtility.DelegateConverter.InvokeOnce<NIMTools.GetAudioTextCb>(user_data, rescode, text, json_extension, IntPtr.Zero);
        }

        /// <summary>
        /// 语音转文字
        /// </summary>
        /// <param name="audioInfo">语音信息</param>
        /// <param name="cb">转换结果回调</param>
        /// <param name="userData">自定义数据，在回调函数中使用</param>
        public static void ConverteAudio2Text(NIMAudioInfo audioInfo, Audio2TextDelegate cb, object userData = null)
        {
            NimUtility.DelegateBaton<Audio2TextDelegate> baton = new NimUtility.DelegateBaton<Audio2TextDelegate>();
            baton.Action = cb;
            baton.Data = userData;
            var json = audioInfo.Serialize();
            var ptr = baton.ToIntPtr();
            nim_tool_get_audio_text_async(json, null, ConverteAudio2TextCallback2, ptr);
        }

        private static readonly NIMTools.GetAudioTextCb ConverteAudio2TextCallback2 = OnConverteAudio2TextCompleted2;

        [MonoPInvokeCallback(typeof(NIMTools.GetAudioTextCb))]
        private static void OnConverteAudio2TextCompleted2(int rescode, string text, string json_extension, IntPtr user_data)
        {
            var baton = NimUtility.DelegateBaton<Audio2TextDelegate>.FromIntPtr(user_data);
            if (baton != null)
            {
                baton.Action(rescode, text, baton.Data);
            }
            NimUtility.DelegateConverter.FreeMem(user_data);
        }



#if !NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 字符串是否匹配词库中的模式
        /// </summary>
        /// <param name="text">目标文本</param>
        /// <param name="libName">词库名称</param>
        /// <returns></returns>
        public static bool IsTextMatchedKeywords(string text,string libName)
        {
            var ret = nim_tool_is_text_matched_keywords(text, libName);
            return ret > 0;
        }

         /// <summary>
        /// 替换在词库中匹配的字符串
        /// </summary>
        /// <param name="text">目标文本</param>
        /// <param name="replace">替换字符串</param>
        /// <param name="libName">词库名称</param>
        /// <returns>替换后的字符串(WIN32平台 返回字符串为"2"时表明含有敏感词不允许发送；"3"表明需要将内容设置在消息结构的反垃圾字段里，由服务器过滤；其他内容可以作为消息正常发送)</returns>
        public static string ReplaceTextMatchedKeywords(string text,string replace,string libName)
        {
            var ptr = nim_tool_replace_text_matched_keywords(text, replace, libName);
            Utf8StringMarshaler marshaler = new Utf8StringMarshaler();
            var result = marshaler.MarshalNativeToManaged(ptr) as string;
            GlobalAPI.FreeBuffer(ptr);
            return result;
        }

#else
        /// <summary>
        /// 客户端反垃圾回调
        /// </summary>
        /// <param name="succeed">本地反垃圾成功</param>
        /// <param name="code">本地反垃圾状态，1-敏感词已被替换，替换后的内容可以发送
        /// 2：表明含有敏感词不允许发送
        /// 3：表明发送时需要将内容设置在消息结构的反垃圾字段里，由服务器过滤；</param>
        /// <param name="result">反垃圾处理后的内容</param>
        public delegate void AntispamFilterDelegate(bool succeed,int code,string result);

        private static readonly nim_tool_filter_client_antispam_cb_func antispam_filter_cb = OnAntispamFilterCompleted;

        private static void OnAntispamFilterCompleted(bool succeed, int ret, string text, string json_extension, IntPtr user_data)
        {
            DelegateConverter.InvokeOnce<AntispamFilterDelegate>(user_data,succeed, ret, text);
        }

        /// <summary>
        /// 客户端反垃圾
        /// </summary>
        /// <param name="text">文本内容</param>
        /// <param name="replaceText">进行替换的字符串</param>
        /// <param name="libName">词库名称</param>
        /// <param name="cb">处理结果回调</param>
        public static void FilterClientAntispamAsync(string text,string replaceText,string libName, AntispamFilterDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            nim_tool_filter_client_antispam_async(text, replaceText, libName, null, antispam_filter_cb, ptr);
        }
#endif

        #region NIM C SDK native methods

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_user_appdata_dir", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_user_appdata_dir(string app_account);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_user_specific_appdata_dir", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_user_specific_appdata_dir(string app_account, NIMAppDataType appdata_type);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_local_appdata_dir", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_local_appdata_dir();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_cur_module_dir", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_cur_module_dir();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_md5", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_md5(string input);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_file_md5", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_file_md5(string file_path);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_uuid", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_get_uuid();

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_tool_get_audio_text_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void nim_tool_get_audio_text_async(string json_audio_info, string json_extension, NIMTools.GetAudioTextCb cb, IntPtr user_data);



#if !NIMAPI_UNDER_WIN_DESKTOP_ONLY
        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern int nim_tool_is_text_matched_keywords([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string text,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string lib_name);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr nim_tool_replace_text_matched_keywords([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string text,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string replace_str,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string lib_name);
#else
        internal delegate void nim_tool_filter_client_antispam_cb_func(bool succeed, int ret,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string text,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void nim_tool_filter_client_antispam_async([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string text,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string replace_str,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string lib_name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, 
            nim_tool_filter_client_antispam_cb_func cb, 
            IntPtr user_data);
#endif

        #endregion
    }
}