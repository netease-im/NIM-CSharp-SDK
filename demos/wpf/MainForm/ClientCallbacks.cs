using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIM;
using NIM.DataSync;

namespace NIMDemo
{
    class ClientCallbacks
    {
        public static void Register()
        {
            NIM.ClientAPI.RegMultiSpotLoginNotifyCb(OnMultiSpotLogin);
            NIM.ClientAPI.RegKickOtherClientCb(OnKickOtherClient);
            NIM.DataSync.DataSyncAPI.RegCompleteCb(OnDataSyncCompleted);
        }

        private static void OnDataSyncCompleted(NIMDataSyncType syncType, NIMDataSyncStatus status, string jsonAttachment)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("同步完成:{0} {1} {2}", syncType, status, jsonAttachment));
        }

        private static void OnKickOtherClient(NIMKickOtherResult result)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("OnKickOtherClient:{0}-{1}", result.ResCode, result.Serialize()));
        }

        private static void OnMultiSpotLogin(NIMMultiSpotLoginNotifyResult result)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("多端登录:{0}-{1}", result.NotifyType, result.Serialize()));
        }
    }
}
