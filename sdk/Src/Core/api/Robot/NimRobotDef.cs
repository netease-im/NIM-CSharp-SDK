#if NIMAPI_UNDER_WIN_DESKTOP_ONLY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NIM
{
    /// <summary>
    /// 机器人信息
    /// </summary>
    public class RobotInfo : NimUtility.NimJsonObject<RobotInfo>
    {
        [JsonProperty("accid")]
        public string Accid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("intro")]
        public string Introduction { get; set; }

        [JsonProperty("rid")]
        public string RID { get; set; }

        [JsonProperty("create_timetag")]
        public ulong CreateTimetag { get; set; }

        [JsonProperty("update_timetag")]
        public ulong UpdateTimetag { get; set; }
    }

    /// <summary>
    /// 机器人信息变更类型
    /// </summary>
    public enum NIMRobotInfoChangeType
    {
        /// <summary>
        /// 全量更新
        /// </summary>
        kNIMRobotInfoChangeTypeAll = 0 
    }

}

#endif
