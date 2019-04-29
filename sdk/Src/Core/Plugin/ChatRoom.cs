using System;
using System.Runtime.InteropServices;
using NimUtility;

namespace NIM.Plugin
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void NimPluginChatroomRequestLoginCbFunc(int errorCode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string jsonExtension, IntPtr userData);

    /// <summary>
    ///     请求获取登录聊天室信息的委托定义
    /// </summary>
    /// <param name="code">错误码</param>
    /// <param name="result">登录信息，直接作为参数调用聊天室登录接口</param>
    public delegate void RequestChatRoomLoginInfoDelegate(ResponseCode code, string result);

    public class ChatRoom
    {
        private static readonly NimPluginChatroomRequestLoginCbFunc OnRequestLoginInfoCompleted = OnRequestLoginInfoCompletedCallback;

        [MonoPInvokeCallbackAttribute(typeof(NimPluginChatroomRequestLoginCbFunc))]
        static void OnRequestLoginInfoCompletedCallback(int errorCode, string result, string jsonExtension, IntPtr userData)
        {
            userData.InvokeOnce<RequestChatRoomLoginInfoDelegate>((ResponseCode)errorCode, result);
        }
        
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_plugin_chatroom_request_enter_async", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void nim_plugin_chatroom_request_enter_async(long roomId,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof (Utf8StringMarshaler))] string jsonExtension,
            NimPluginChatroomRequestLoginCbFunc cb, IntPtr ptr);

        /// <summary>
        ///     获取登陆聊天室需要的授权信息
        /// </summary>
        /// <param name="roomId">房间ID</param>
        /// <param name="cb">操作结果委托</param>
        public static void RequestLoginInfo(long roomId, RequestChatRoomLoginInfoDelegate cb,string json_ext="")
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            nim_plugin_chatroom_request_enter_async(roomId, json_ext, OnRequestLoginInfoCompleted, ptr);
        }
    }
}