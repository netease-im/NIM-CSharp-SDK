using System;
using System.Runtime.InteropServices;
using NIM.Session;

namespace NIM
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void UploadFileCallback(long uploadedSize, long totalSize, [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonExtension, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void IMMessageArcCallback([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonArcResult, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void IMReceiveMessageCallback([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string content,
                                                  [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string jsonArcResult, IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate bool NIMTeamNotificationFilterFunc(
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
        string content,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
        string jsonExtension,
        IntPtr userData);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_talk_recall_msg_func(int rescode,
         [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string content,
         [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension,
         IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_talk_receive_broadcast_cb_func([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string content,
         [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension, 
         IntPtr user_data);

    class TalkNativeMethods
    {
        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_send_msg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_send_msg(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string jsonMsg,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string jsonExtension,
            UploadFileCallback cb, IntPtr userData);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_stop_send_msg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_stop_send_msg(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string jsonMsg,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string jsonExtension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_ack_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_ack_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string jsonExtension,
            IMMessageArcCallback cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_receive_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_receive_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string jsonExtension,
            IMReceiveMessageCallback cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_receive_msgs_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_receive_msgs_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string json_extension,
            IMReceiveMessageCallback cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_notification_filter_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_notification_filter_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]
            string json_extension,
            NIMTeamNotificationFilterFunc cb, IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_create_retweet_msg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr nim_talk_create_retweet_msg(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string src_msg_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string client_msg_id,
            NIMSessionType retweet_to_session_type,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string retweet_to_session_id,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string msg_setting,
            long timetag);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_recall_msg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr nim_talk_recall_msg(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string msg_json,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string notify,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_talk_recall_msg_func cb,
            IntPtr user_data);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_recall_msg_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_recall_msg_cb(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension,
            nim_talk_recall_msg_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_get_attachment_path_from_msg", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr nim_talk_get_attachment_path_from_msg(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_msg);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_receive_broadcast_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_receive_broadcast_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))] string json_extension, 
            nim_talk_receive_broadcast_cb_func cb, 
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, EntryPoint = "nim_talk_reg_receive_broadcast_msgs_cb", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void nim_talk_reg_receive_broadcast_msgs_cb([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(NimUtility.Utf8StringMarshaler))]string json_extension, 
            nim_talk_receive_broadcast_cb_func cb, 
            IntPtr user_data);
    }
}
