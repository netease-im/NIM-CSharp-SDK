#if NIMAPI_UNDER_WIN_DESKTOP_ONLY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace NIM.Robot
{
    public delegate void RobotChangedCallback(ResponseCode result, NIMRobotInfoChangeType type, List<RobotInfo> robots, object data);

    public delegate void GetRobotsDelegate(ResponseCode result, List<RobotInfo> robots);

    /// <summary>
    /// 机器人接口
    /// </summary>
    public class NIMRobotAPI
    {
        /// <summary>
        /// 获取全部机器人信息(同步接口，堵塞NIM内部线程)
        /// </summary>
        /// <returns></returns>
        public static List<RobotInfo> QueryAllRobots()
        {
            List<RobotInfo> robots = null;
            var ptr = RobotNativeMethods.nim_robot_query_all_robots_block(null);
            var obj = NimUtility.Utf8StringMarshaler.GetInstance(null).MarshalNativeToManaged(ptr);
            var result = obj as string;
            if (!string.IsNullOrEmpty(result))
            {
                var list = NimUtility.Json.JsonParser.Deserialize<List<string>>(result);
                if (list != null && list.Any())
                {
                    robots = new List<RobotInfo>();
                    foreach (var jstr in list)
                    {
                        var robot = RobotInfo.Deserialize(jstr);
                        robots.Add(robot);
                    }
                }
            }
            return robots;
        }

        /// <summary>
        /// 获取指定机器人信息(同步接口，堵塞NIM内部线程)
        /// </summary>
        /// <param name="accid"></param>
        /// <returns></returns>
        public static RobotInfo QueryRobotByAccid(string accid)
        {
            RobotInfo robot = null;
            var ptr = RobotNativeMethods.nim_robot_query_robot_by_accid_block(accid, null);
            var obj = NimUtility.Utf8StringMarshaler.GetInstance(null).MarshalNativeToManaged(ptr);
            var result = obj as string;
            if (!string.IsNullOrEmpty(result))
            {
                robot = NimUtility.Json.JsonParser.Deserialize<RobotInfo>(result);
            }
            return robot;
        }

        private static nim_robot_change_cb_func RobotChangedCbFunc = OnRobotInfoChanged;

        private static void OnRobotInfoChanged(int rescode, NIMRobotInfoChangeType type, string result, string json_extension, IntPtr user_data)
        {
            var baton = NimUtility.DelegateBaton<RobotChangedCallback>.FromIntPtr(user_data);
            if (baton != null && baton.Action != null)
            {
                List<RobotInfo> robots = NimUtility.Json.JsonParser.Deserialize<List<RobotInfo>>(result);
                baton.Action((ResponseCode)rescode, type, robots, baton.Data);
            }
        }

        /// <summary>
        /// 注册机器人变更广播通知
        /// </summary>
        /// <param name="cb"></param>
        /// <param name="data"></param>
        public static void RegRobotChangedCb(RobotChangedCallback cb, object data = null)
        {
            NimUtility.DelegateBaton<RobotChangedCallback> baton = new NimUtility.DelegateBaton<RobotChangedCallback>();
            baton.Data = data;
            baton.Action = cb;
            var ptr = baton.ToIntPtr();
            RobotNativeMethods.nim_robot_reg_changed_callback(null, RobotChangedCbFunc, ptr);
        }


        /// <summary>
        /// 向机器人账号发送消息
        /// </summary>
        /// <param name="robotID">机器人Accid</param>
        /// <param name="message"></param>
        public static void SendMessage(string robotID, Robot.RobotMessageContent message)
        {
            var tmp = new { param = message, robotAccid = robotID };

            Robot.RobotMessage robotMsg = new Robot.RobotMessage();
            robotMsg.ReceiverID = robotID;
            robotMsg.Attach = tmp;
            robotMsg.TextContent = message.Text; 
            TalkAPI.SendMessage(robotMsg);
        }

        private static nim_robot_query_cb_func _getRobotsCallback = new nim_robot_query_cb_func(OnGetRobotsCompleted);

        private static void OnGetRobotsCompleted(int rescode, string result, string json_extension, IntPtr user_data)
        {
            var robots = NimUtility.Json.JsonParser.Deserialize<List<RobotInfo>>(result);
            NimUtility.DelegateConverter.InvokeOnce<GetRobotsDelegate>(user_data, rescode, robots);
        }

        /// <summary>
        /// 获取全部机器人信息
        /// </summary>
        /// <param name="timetag">时间戳</param>
        /// <param name="cb"></param>
        public static void GetRobots(long timetag, GetRobotsDelegate cb)
        {
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(cb);
            RobotNativeMethods.nim_robot_get_robots_async(timetag, null, _getRobotsCallback, ptr);
        }
    }
}

#endif
