/** @file NIMDataSyncDef.cs
  * @brief data sync define
  * @copyright (c) 2015, NetEase Inc. All rights reserved
  * @author Harrison
  * @date 2015/12/8
  */


namespace NIM.DataSync
{
    /// <summary>
    /// 数据同步类型
    /// </summary>
    public enum NIMDataSyncType
    {
        /// <summary>
        /// 未读消息同步
        /// </summary>
        kNIMDataSyncTypeUnreadMsg = 2,

        /// <summary>
        /// 所有群的信息同步
        /// </summary>
        kNIMDataSyncTypeTeamInfo = 3,

        /// <summary>
        /// 漫游消息同步,每个会话同步到漫游消息都会触发该类通知
        /// </summary>
        kNIMDataSyncTypeRoamMsg = 7,

        /// <summary>
        /// 群成员列表同步
        /// </summary>
        kNIMDataSyncTypeTeamUserList = 1000,

#if NIMAPI_UNDER_WIN_DESKTOP_ONLY
        /// <summary>
        /// 所有群的成员列表同步完毕, json_attachment为空 
        /// </summary>
        kNIMDataSyncTypeAllTeamUserList = 1001,
#endif
    }

    /// <summary>
    /// 数据同步状态
    /// </summary>
    public enum NIMDataSyncStatus
    {
        /// <summary>
        /// 同步完成
        /// </summary>
	    kNIMDataSyncStatusComplete = 1,		
    }
}
