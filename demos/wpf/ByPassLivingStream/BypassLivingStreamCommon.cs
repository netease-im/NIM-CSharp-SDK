using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIMDemo.BypassLSCommon
{
	public enum NTESLiveMicState
	{
		NTESLiveMicStateWaiting,    //队列等待
		NTESLiveMicStateConnecting, //连接中
		NTESLiveMicStateConnected,  //已连接
	}

	public enum NTESAnchorOperate
	{
		NTESAnchorAgree = 2,
		kNTESAnchorRejectOrDisConnect, //主播拒绝或者是断开连接操作
	}

	public enum PushMicNotificationType
	{
		kJoin_Queue = 1,     //加入连麦队列通知
		kExit_Queue = 2,     //退出连麦队列通知
		kConnecting_MIC = 3, //主播选择某人连麦
		kDisConnecting_MIC = 4,//主播断开连麦通知
		kRejectConnecting = 5 //观众拒绝主播的连麦请求 ps.观众申请连麦->观众退出房间->主播连麦->观众发送系统通知拒绝主播连麦->主播从列表中删掉
	}

	public  enum NotifyType
	{
		kP2P,//点对点通知
		kTeam,//通知组内的全部人
	}

	public enum InactionType
	{
		kAudio = 1,//音频互动
		kVedio = 2, //视频互动
	}

	public enum BypassMemberListOpt
	{
		kAdd = 0, //增加成员
		kUpdate = 1,//更新成员
		kRemove = 2,//删除成员
	};

}
