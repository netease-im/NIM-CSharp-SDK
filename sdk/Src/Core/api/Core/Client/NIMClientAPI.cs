/** @file NIMClientAPI.cs
  * @brief NIM SDK提供的Client接口，主要包括SDK初始化/清理、客户端登录/退出/重连/掉线/被踢等流程
  * NIM SDK所有接口命名说明: nim.***(模块).***(功能)，如nim.ClientAPI.Init
  * NIM SDK所有接口参数说明: C#层的字符串参数全部指定用UTF-16编码
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */
using NimUtility;
using System;
using System.Diagnostics;

namespace NIM
{
    public delegate void ConfigMultiportPushDelegate(ResponseCode response, ConfigMultiportPushParam param);
    /// <summary>
    /// NIM SDK提供的Client接口，主要包括SDK初始化/清理、客户端登录/退出/重连/掉线/被踢等流程
    /// </summary>
    public class ClientAPI
    {
        public static EventHandler<LoginResultEventArgs> LoginResultHandler;
        private static bool _sdkInitialized = false;

        public delegate void KickOtherClientResultHandler(NIMKickOtherResult result);

        public delegate void MultiSpotLoginNotifyResultHandler(NIMMultiSpotLoginNotifyResult result);

        public delegate void KickoutResultHandler(NIMKickoutResult result);

        public delegate void LogoutResultDelegate(NIMLogoutResult result);

        public delegate void LoginResultDelegate(NIMLoginResult result);

        public delegate void DndConfigureDelegate(ResponseCode resCode, DndConfigParam config);
        /// <summary>
        /// SDK是否已经初始化
        /// </summary>
        public static bool SdkInitialized
        {
            get { return _sdkInitialized; }
        }

        /// <summary>
        /// NIM SDK初始化
        /// </summary>
        /// <param name="appDataDir">使用默认路径时只需传入单个目录名（不以反斜杠结尾)，使用自定义路径时需传入完整路径（以反斜杠结尾，并确保有正确的读写权限！）.</param>
        /// <param name="appInstallDir">目前不需要传入（SDK可以自动获取）.</param>
        /// <param name="config">The config.</param>
        /// <returns><c>true</c> 成功, <c>false</c> 失败</returns>
#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        [Obsolete]
#endif
        public static bool Init(string appDataDir, string appInstallDir = "", NimUtility.NimConfig config = null)
        {
            if (_sdkInitialized)
            {
                RegisterSdkCallbacks();//需要重新注册；
                return true;
            }
            string configJson = null;
            if (config != null && config.IsValiad())
                configJson = config.Serialize();

            _sdkInitialized = ClientNativeMethods.nim_client_init(appDataDir, appInstallDir, configJson);

            if (_sdkInitialized)
            {
                RegisterSdkCallbacks();
            }
            //调用com.netease.nimlib.SystemUtil的初始化接口
            InitSystemUtil();
            return _sdkInitialized;
        }

#if UNITY_ANDROID
        /// <summary>
        /// 连接华为推送并获取token，必须集成华为推送SDK
        /// </summary>
        public static void InitHuaweiPush()
        {
            using (var actClass = new UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                UnityEngine.AndroidJavaObject curActivityContext = actClass.GetStatic<UnityEngine.AndroidJavaObject>("currentActivity");
                if (curActivityContext != null)
                {
                    UnityEngine.AndroidJavaClass clsSystemUtil = new UnityEngine.AndroidJavaClass("com.netease.hwpushwrapper.HWPush");
                    if (clsSystemUtil != null)
                    {
                        clsSystemUtil.CallStatic("initHuaweiPush", curActivityContext);
                        UnityEngine.Debug.Log("call java method initHuaweiPush");
                    }
                    else
                    {
                        UnityEngine.Debug.Log("can't find class com.netease.hwpushwrapper.HWPush");
                    }
                }
            }
        }

        /// <summary>
        /// 设置接收华为推送设置结果的game object
        /// </summary>
        /// <param name="objName">game object 名称</param>
        public static void SetHuaweiPushReceiverObject(string objName)
        {
            UnityEngine.AndroidJavaClass clsSystemUtil = new UnityEngine.AndroidJavaClass("com.netease.hwpushwrapper.HWPush");
            if(clsSystemUtil != null)
            {
                clsSystemUtil.CallStatic("setReceiverObject", objName);
            }
        }

#endif

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// NIM SDK初始化 
        /// </summary>
        /// <param name="appKey">AppKey,必填</param>
        /// <param name="appDataDir">使用默认路径时只需传入单个目录名（不以反斜杠结尾)，使用自定义路径时需传入完整路径（以反斜杠结尾，并确保有正确的读写权限！）.</param>
        /// <param name="appInstallDir">目前不需要传入（SDK可以自动获取）.</param>
        /// <param name="config">The config.</param>
        /// <returns><c>true</c> 成功, <c>false</c> 失败</returns>
        public static bool Init(string appKey, string appDataDir, string appInstallDir = "", NimUtility.NimConfig config = null)
        {
            if (_sdkInitialized)
            {
                RegisterSdkCallbacks();//需要重新注册；
                return true;
            }
            string configJson = null;
            if (config != null && config.IsValiad())
            {
                if (string.IsNullOrEmpty(appKey))
                    config.AppKey = appKey;
                configJson = config.Serialize();
            }
            else
            {
                config = new NimConfig();
                config.AppKey = appKey;
                configJson = config.Serialize();
            }

            _sdkInitialized = ClientNativeMethods.nim_client_init(appDataDir, appInstallDir, configJson);
            if (_sdkInitialized)
            {
                RegisterSdkCallbacks();
            }
            //调用com.netease.nimlib.SystemUtil的初始化接口
            InitSystemUtil();

            return _sdkInitialized;
        }

#endif
        private static void InitSystemUtil()
        {
#if UNITY_ANDROID
            try
            {
                /* The Mono garbage collector should release all created instances of AndroidJavaObject and AndroidJavaClass after use, 
                 * but it is advisable to keep them in a using(){} statement to ensure they are deleted as soon as possible. 
                 * Without this, you cannot be sure when they will be destroyed.
                 */
                using (var actClass = new UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    UnityEngine.AndroidJavaObject curActivityContext = actClass.GetStatic<UnityEngine.AndroidJavaObject>("currentActivity");
                    if (curActivityContext != null)
                    {
                        UnityEngine.AndroidJavaClass clsSystemUtil = new UnityEngine.AndroidJavaClass("com.netease.nimlib.NIMSDK");
                        if (clsSystemUtil != null)
                        {
                            NimUtility.Log.Info("com.netease.nimlib.NIMSDK found");
							Boolean init = clsSystemUtil.CallStatic<Boolean>("init", curActivityContext, "fjni_wrapper");
                            NimUtility.Log.Info("init:" + init);
                            //string androidId = clsSystemUtil.CallStatic<String>("getAndroidId");
                            //NimUtility.Log.Info("androidId:" + androidId);
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                System.Console.Write("initAndroidExceptionHandler failed, an unexpected error: " + e.ToString());
            }
#endif

#if UNITY_IPHONE || UNITY_IOS
		if(UnityEngine.Application.platform == UnityEngine.RuntimePlatform.IPhonePlayer){
            //TODO:
		}
#endif
        }

        /// <summary>
        /// 注册全局回调函数，在切换账号都需要重新注册
        /// </summary>
        public static void RegisterSdkCallbacks()
        {
            NIM.Friend.FriendAPI.RegisterCallbacks();
            NIM.TalkAPI.RegisterCallbacks();
            NIM.Session.SessionAPI.RegisterCallbacks();
            NIM.SysMessage.SysMsgAPI.RegisterCallbacks();
            NIM.Team.TeamAPI.RegisterCallbacks();
            NIM.User.UserAPI.RegisterCallbacks();
        }

        /// <summary>
        /// NIM SDK清理
        /// </summary>
        public static void Cleanup()
        {
            if (!_sdkInitialized) return;
            ClientNativeMethods.nim_client_cleanup(null);
            _sdkInitialized = false;
        }

        public static void CleanupEx()
        {
            if (!_sdkInitialized) return;
            ClientNativeMethods.nim_client_cleanup2(NimClientCleanup2Callback, null);
            _sdkInitialized = false;
        }

        private static void NimClientCleanup2Callback(string json_params, IntPtr user_data)
        {

        }

        /// <summary>
        /// NIM客户端登录
        /// </summary>
        /// <param name="appKey">The app key.</param>
        /// <param name="account">The account.</param>
        /// <param name="token">令牌 (在后台绑定的登录token).</param>
        /// <param name="handler">登录流程的回调函数</param>
        public static void Login(string appKey, string account, string token, LoginResultDelegate handler = null)
        {
            if (!CheckLoginParams(appKey, account, token))
                return;
            var ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            ClientNativeMethods.nim_client_login(appKey, account, token, null, LoginResultCallback, ptr);
        }

        static bool CheckLoginParams(string appkey, string account, string token)
        {
            if (string.IsNullOrEmpty(appkey) || string.IsNullOrEmpty(account) || string.IsNullOrEmpty(token))
                return false;
            return true;
        }

        /// <summary>
        /// NIM客户端手动重连（注意 APP需要统一处理自动重连/手动重连的回调，因为如果处于某次自动重连的过程中调用手动重连接口，不起作用！）   .
        /// </summary>
        /// <param name="jsonExtension"> json扩展参数（备用，目前不需要）.</param>
        public static void Relogin(string jsonExtension = null)
        {
            ClientNativeMethods.nim_client_relogin(jsonExtension);
        }

        /// <summary>
        /// NIM客户端注销/退出，异步方法，回调函数中报告执行结果
        /// </summary>
        /// <param name="logoutType">Logout操作类型</param>
        /// <param name="delegate">注销/退出的回调函数.</param>
        public static void Logout(NIMLogoutType logoutType, LogoutResultDelegate @delegate)
        {
            IntPtr ptr = NimUtility.DelegateConverter.ConvertToIntPtr(@delegate);
            ClientNativeMethods.nim_client_logout(logoutType, null, LogoutResultCallback, ptr);
        }

        /// <summary>
        /// NIM客户端注销/退出，同步方法
        /// </summary>
        /// <param name="logoutType"></param>
        /// <param name="waitSeconds"></param>
        public static void Logout(NIMLogoutType logoutType,int waitSeconds = 10)
        {
            System.Threading.Semaphore semaphore = new System.Threading.Semaphore(0, 1);
            NIM.ClientAPI.Logout(logoutType, (r) =>
            {
                semaphore.Release();
            });
            semaphore.WaitOne(TimeSpan.FromSeconds(waitSeconds));
        }

        /// <summary>
        /// 将本帐号的其他端踢下线.通过注册RegKickOtherClientCb回调得到结果
        /// </summary>
        /// <param name="devices">设备标识</param>
        public static void KickOtherClients(NIMKickoutOtherDeviceInfo devices)
        {
            var json = devices.Serialize();
            ClientNativeMethods.nim_client_kick_other_client(json);
        }

        /// <summary>
        /// 注册NIM客户端自动重连回调。重连失败时，如果不是网络错误引起的（网络相关的错误号为kNIMResTimeoutError和kNIMResConnectionError），而是服务器返回了非kNIMResSuccess的错误号,
        /// 则说明重连的机制已经失效，需要APP层调用Logout执行注销操作并退回到登录界面后进行重新登录.
        /// </summary>
        /// <param name="jsonExtension">json扩展参数（备用，目前不需要）</param>
        /// <param name="handler">自动重连的回调函数
        /// 如果返回错误号kNIMResExist，说明无法继续重连，App层必须调用Logout退出到登录界面，以便用户重新进行登录.
        /// </param>
        public static void RegAutoReloginCb(LoginResultDelegate handler, string jsonExtension = null)
        {
            IntPtr ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            ClientNativeMethods.nim_client_reg_auto_relogin_cb(jsonExtension, LoginResultCallback, ptr);
        }

        /// <summary>
        /// 注册NIM客户端被踢回调.
        /// </summary>
        /// <param name="handler">被踢回调</param>
        public static void RegKickoutCb(KickoutResultHandler handler)
        {
            IntPtr ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            ClientNativeMethods.nim_client_reg_kickout_cb(null, BeKickedOfflineCallback, ptr);
        }

        /// <summary>
        ///  注册NIM客户端掉线回调.
        /// </summary>
        /// <param name="handler">掉线的回调函数.</param>
        public static void RegDisconnectedCb(Action handler)
        {
            IntPtr ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            ClientNativeMethods.nim_client_reg_disconnect_cb(null, DisconnectedCallback, ptr);
        }

        /// <summary>
        /// 注册NIM客户端多点登录通知回调.
        /// </summary>
        /// <param name="handler">多点登录通知的回调函数.</param>
        public static void RegMultiSpotLoginNotifyCb(MultiSpotLoginNotifyResultHandler handler)
        {
            IntPtr ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            ClientNativeMethods.nim_client_reg_multispot_login_notify_cb(null, MultiSpotLoginNotifyCallback, ptr);
        }

        /// <summary>
        /// 注册NIM客户端将本帐号的其他端踢下线结果回调.
        /// </summary>
        /// <param name="handler">操作结果的回调函数.</param>
        public static void RegKickOtherClientCb(KickOtherClientResultHandler handler)
        {
            IntPtr ptr = NimUtility.DelegateConverter.ConvertToIntPtr(handler);
            ClientNativeMethods.nim_client_reg_kickout_other_client_cb(null, KickOtherClientCb, ptr);
        }

        private static readonly NIMGlobal.JsonTransportCb LoginResultCallback = OnLoginResultCallback;

        [MonoPInvokeCallback(typeof(NIMGlobal.JsonTransportCb))]
        static void OnLoginResultCallback(string jsonResult, IntPtr ptr)
        {
            var loginResult = NIMLoginResult.Deserialize(jsonResult);

            ptr.Invoke<LoginResultDelegate>(loginResult);
            if (LoginResultHandler != null)
            {
                LoginResultHandler(null, new LoginResultEventArgs(loginResult));
            }
        }

        private static readonly NIMGlobal.JsonTransportCb LogoutResultCallback = OnLogoutResultCallback;
        [MonoPInvokeCallback(typeof(NIMGlobal.JsonTransportCb))]
        static void OnLogoutResultCallback(string jsonResult, IntPtr ptr)
        {
            var result = NIMLogoutResult.Deserialize(jsonResult);
            ptr.InvokeOnce<LogoutResultDelegate>(result);
        }

        private static readonly NIMGlobal.JsonTransportCb BeKickedOfflineCallback = OnBeKickedOfflineCallback;
        [MonoPInvokeCallback(typeof(NIMGlobal.JsonTransportCb))]
        static void OnBeKickedOfflineCallback(string jsonResult, IntPtr ptr)
        {
            var result = NIMKickoutResult.Deserialize(jsonResult);
            ptr.InvokeOnce<KickoutResultHandler>(result);
        }

        private static readonly NIMGlobal.JsonTransportCb DisconnectedCallback = OnDisconnectedCallback;
        [MonoPInvokeCallback(typeof(NIMGlobal.JsonTransportCb))]
        static void OnDisconnectedCallback(string jsonResult, IntPtr ptr)
        {
            ptr.Invoke<Action>();
        }

        private static readonly NIMGlobal.JsonTransportCb MultiSpotLoginNotifyCallback = OnMultiSpotLoginNotifyCallback;
        [MonoPInvokeCallback(typeof(NIMGlobal.JsonTransportCb))]
        static void OnMultiSpotLoginNotifyCallback(string jsonResult, IntPtr handler)
        {
            var result = NIMMultiSpotLoginNotifyResult.Deserialize(jsonResult);
            handler.Invoke<MultiSpotLoginNotifyResultHandler>(result);
        }

        private static readonly NIMGlobal.JsonTransportCb KickOtherClientCb = OnKickOtherClientCb;
        [MonoPInvokeCallback(typeof(NIMGlobal.JsonTransportCb))]
        static void OnKickOtherClientCb(string jsonResult, IntPtr handler)
        {
            var result = NIMKickOtherResult.Deserialize(jsonResult);
            handler.InvokeOnce<KickOtherClientResultHandler>(result);
        }

        /// <summary>
        /// 开启多端推送
        /// </summary>
        /// <param name="cb">操作结果委托</param>
        public static void EnableMultiportPush(ConfigMultiportPushDelegate cb)
        {
            ConfigMultiportPushParam param = new ConfigMultiportPushParam();
            param.Enabled = true;
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            ClientNativeMethods.nim_client_set_multiport_push_config(param.Serialize(), null, ConfigMultiportPushCb, ptr);
        }

        /// <summary>
        /// 禁止多端推送
        /// </summary>
        /// <param name="cb">操作结果委托</param>
        public static void DisableMultiportPush(ConfigMultiportPushDelegate cb)
        {
            ConfigMultiportPushParam param = new ConfigMultiportPushParam();
            param.Enabled = false;
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            ClientNativeMethods.nim_client_set_multiport_push_config(param.Serialize(), null, ConfigMultiportPushCb, ptr);
        }

        /// <summary>
        /// 获取多端推送控制开关
        /// </summary>
        /// <param name="cb"></param>
        public static void IsMultiportPushEnabled(ConfigMultiportPushDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            ClientNativeMethods.nim_client_get_multiport_push_config(null, ConfigMultiportPushCb, ptr);
        }

        /// <summary>
        /// 注册多端推送设置同步回调
        /// </summary>
        /// <param name="cb"></param>
        public static void RegMulitiportPushEnableChangedCb(ConfigMultiportPushDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            ClientNativeMethods.nim_client_reg_sync_multiport_push_config_cb(null, OnMultiportPushEnableChanged, ptr);
        }

        private static readonly nim_client_multiport_push_config_cb_func ConfigMultiportPushCb = MultiportPushChanged;
        [MonoPInvokeCallback(typeof(nim_client_multiport_push_config_cb_func))]
        private static void MultiportPushChanged(int resCode, string content, string jsonExt, IntPtr ptr)
        {
            ConfigMultiportPushParam param = ConfigMultiportPushParam.Deserialize(content);
            ptr.InvokeOnce<ConfigMultiportPushDelegate>((ResponseCode)resCode, param);
        }

        private static readonly nim_client_multiport_push_config_cb_func OnMultiportPushEnableChanged = MultiportPushChanged;

        /// <summary>
        /// 更新ios推送token
        /// </summary>
        /// <param name="token"></param>
        public static void UpdateApnsToken(string token)
        {
            ClientNativeMethods.nim_client_update_apns_token(token);
        }

        /// <summary>
        /// ios 免打扰设置
        /// </summary>
        /// <param name="param"></param>
        /// <param name="cb"></param>
        public static void SetDndConfig(DndConfigParam param, DndConfigureDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            ClientNativeMethods.nim_client_set_dnd_config(param.Serialize(), null, UpdateDndConfigCb, ptr);
        }

        /// <summary>
        /// 获取ios 免打扰设置
        /// </summary>
        /// <param name="cb"></param>
        public static void GetDndConfig(DndConfigureDelegate cb)
        {
            var ptr = DelegateConverter.ConvertToIntPtr(cb);
            ClientNativeMethods.nim_client_get_dnd_config(UpdateDndConfigCb, ptr);
        }

        private static readonly nim_client_dnd_cb_func UpdateDndConfigCb = UpdateDndConfigCompleted;

        [MonoPInvokeCallback(typeof(nim_client_dnd_cb_func))]
        private static void UpdateDndConfigCompleted(int rescode, string content, string json_params, IntPtr user_data)
        {
            DndConfigParam param = DndConfigParam.Deserialize(content);
            user_data.InvokeOnce<DndConfigureDelegate>((ResponseCode)rescode, param);
        }

        /// <summary>
        /// 获取NIM客户端登录状态
        /// </summary>
        /// <param name="jsonExt"></param>
        /// <returns></returns>
        public static NIMLoginState GetLoginState(string jsonExt = null)
        {
            var ret = ClientNativeMethods.nim_client_get_login_state(jsonExt);
            return (NIMLoginState)ret;
        }

        /// <summary>
        /// 获取NIM SDK 版本号
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            var ptr = ClientNativeMethods.nim_client_version();
            NimUtility.Utf8StringMarshaler marshaler = new Utf8StringMarshaler();
            var ret = marshaler.MarshalNativeToManaged(ptr);
            return ret != null ? ret.ToString() : null;
        }
    }
}
