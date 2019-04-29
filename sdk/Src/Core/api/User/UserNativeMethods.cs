using System;
using System.Runtime.InteropServices;
using NimUtility;
using NIM.User.Delegate;

namespace NIM.User
{
    namespace Delegate
    {
        /// <summary>
        ///     用户多端同步类型（黑名单、静音名单）的监听回调函数定义
        /// </summary>
        /// <param name="type">用户多端同步类型</param>
        /// <param name="resultJson">
        ///     同步内容 kNIMUserSpecialRelationshipChangeTypeMarkBlack: {"accid" : "abc", "black" : bool} ,
        ///     kNIMUserSpecialRelationshipChangeTypeMarkMute: {"accid" : "abc", "mute" : bool},
        ///     kNIMUserSpecialRelationshipChangeTypeSyncMuteAndBlackList: {a, b, c ...}(a,b,c为json value array - 用户特殊关系（黑名单、静音名单）
        ///     Json Keys)
        /// </param>
        /// <param name="jsonExtension">json扩展数据（备用）</param>
        /// <param name="userData">APP的自定义用户数据，SDK只负责传回给回调函数，不做任何处理！</param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserSpecialRelationshipChangedDelegate(NIMUserRelationshipChangeType type, string resultJson, string jsonExtension, IntPtr userData);

        /// <summary>
        ///     用户操作（加黑或取消加黑，加静音或取消静音）的回调函数定义
        /// </summary>
        /// <param name="response">结果代码，一切正常200</param>
        /// <param name="accid">好友id</param>
        /// <param name="opt">mark（加黑或取消加黑，加静音或取消静音）</param>
        /// <param name="jsonExtension">json扩展数据（备用）</param>
        /// <param name="userData"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UserOperationDelegate(ResponseCode response, string accid, bool opt, string jsonExtension, IntPtr userData);

        /// <summary>
        ///     获取黑名单、静音列表
        /// </summary>
        /// <param name="response">结果代码，一切正常200</param>
        /// <param name="blacklistJson">黑名单和静音列表Json字符串（{a , b , ...}(a,b,c为json value array - 用户特殊关系（黑名单、静音名单） Json Keys)）</param>
        /// <param name="jsonExtension"></param>
        /// <param name="userData"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void SyncMutedAndBlacklistDelegate(ResponseCode response, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string blacklistJson, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string jsonExtension, IntPtr userData);

        /// <summary>
        ///     用户名片的监听回调函数定义
        /// </summary>
        /// <param name="resultJson">
        ///     result_json	json object array 例 [{ "accid" : "litianyi01", "create_timetag" : 1430101821372,
        ///     "gender" : 0, "name" : "oleg01", "update_timetag" : 1430101821372 }, ...]
        /// </param>
        /// <param name="jsonExtension"></param>
        /// <param name="userData"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void UserNameCardChangedDelegate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string resultJson, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string jsonExtension, IntPtr userData);

        /// <summary>
        ///     获取用户名片回调函数
        /// </summary>
        /// <param name="resultJson">
        ///     json object array 例 [{ "accid" : "litianyi01", "create_timetag" : 1430101821372, "gender" : 0,
        ///     "name" : "oleg01", "update_timetag" : 1430101821372 }, ...]
        /// </param>
        /// <param name="jsonExtension"></param>
        /// <param name="userData"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void GetUserNameCardDelegate([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string resultJson, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string jsonExtension, IntPtr userData);

        /// <summary>
        ///     更新用户名片回调函数
        /// </summary>
        /// <param name="response"></param>
        /// <param name="jsonExtension"></param>
        /// <param name="userData"></param>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate void UpdateUserNameCardDelegate(ResponseCode response, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string jsonExtension, IntPtr userData);
    }

    internal class UserNativeMethods
    {
        /// <summary>
        ///     顺风环境专用更新用户自定义在线状态（公网版不开发该接口）.
        /// </summary>
        /// <param name="user_stat"></param>
        /// <param name="json_extension"></param>
        /// <param name="cb"></param>
        /// <param name="user_data"></param>
        //[DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_set_stat", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        //public static extern void nim_user_set_stat(uint user_stat, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, NIMGlobal.JsonTransportCb cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_reg_special_relationship_changed_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_reg_special_relationship_changed_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, UserSpecialRelationshipChangedDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_set_black", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_set_black([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string accid, bool set_black, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, UserOperationDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_set_mute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_set_mute([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string accid, bool set_mute, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, UserOperationDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_get_mute_blacklist", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_get_mute_blacklist([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, SyncMutedAndBlacklistDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_reg_user_name_card_changed_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_reg_user_name_card_changed_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, UserNameCardChangedDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_get_user_name_card", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_get_user_name_card([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string accids, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, GetUserNameCardDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_get_user_name_card_online", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_get_user_name_card_online([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string accids, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, GetUserNameCardDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_update_my_user_name_card", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_update_my_user_name_card([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string info_json, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string json_extension, UpdateUserNameCardDelegate cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_user_update_push_token", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_user_update_push_token([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string cer_name,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string token, 
            int type);
    }
}