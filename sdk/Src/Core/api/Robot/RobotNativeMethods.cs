#if NIMAPI_UNDER_WIN_DESKTOP_ONLY

using NimUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace NIM.Robot
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_robot_change_cb_func(int rescode,
        NIMRobotInfoChangeType type,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension,
        IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    delegate void nim_robot_query_cb_func(int rescode,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string result,
        [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))] string json_extension, 
        IntPtr user_data);

    class RobotNativeMethods
    {
        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_robot_reg_changed_callback(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension,
            nim_robot_change_cb_func cb,
            IntPtr user_data);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr nim_robot_query_all_robots_block(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension);


        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr nim_robot_query_robot_by_accid_block(
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string accid,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension);

        [DllImport(NIM.NativeConfig.NIMNativeDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void nim_robot_get_robots_async(long timetag,
            [MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(Utf8StringMarshaler))]string json_extension, 
            nim_robot_query_cb_func cb, 
            IntPtr user_data);
    }
}

#endif
