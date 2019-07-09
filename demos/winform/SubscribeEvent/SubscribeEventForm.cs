using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NIM;

namespace NIMDemo
{
    public partial class SubscribeEventForm : Form
    {
        public SubscribeEventForm()
        {
            InitializeComponent();
            this.Load += SubscribeEventForm_Load;
        }

        private void SubscribeEventForm_Load(object sender, EventArgs e)
        {
            NIM.NIMSubscribeApi.RegPushEventCb(PushEventCallback);
            NIM.NIMSubscribeApi.RegBatchPushEventCb(BatchPushEventCallback);
        }

        private void BatchPushEventCallback(ResponseCode code, List<NIMEventInfo> infoList)
        {
            NimUtility.Log.Info(string.Format("[BatchPushEventCallback code = {0},infoList:{1}]", infoList.Dump()));
        }

        private void PushEventCallback(ResponseCode code, NIMEventInfo info)
        {
            NimUtility.Log.Info(string.Format("[PushEventCallback code = {0},infoList:{1}]", info.Dump()));
        }

        private void SubscribeEventForm_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            NIMEventInfo info = new NIMEventInfo();
            int type = 0;
            int.TryParse(textBox1.Text, out type);
            int value = 0;
            int.TryParse(textBox2.Text, out value);
            long period = 0;
            long.TryParse(textBox3.Text, out period);
            int broadcastType = 0;
            int.TryParse(textBox4.Text, out broadcastType);
            int sync = 0;
            int.TryParse(textBox5.Text, out sync);
            info.Type = type;
            info.Value = value;
            info.ValidityPeriod = period;
            info.BroadcastType = broadcastType;
            info.Sync = sync;

            //info.Config = string.Format("event config - {0}", DateTime.Now.ToLongTimeString());
            info.ClientMsgID = Guid.NewGuid().ToString();
            info.NimConfig = "online";
            //info.NimConfig.OnlineClients = new List<int>() { 1, 2, 4 };

            NIM.NIMSubscribeApi.Publish(info, OnPublishCompleted);
        }

        private void OnPublishCompleted(ResponseCode code, NIMEventInfo info)
        {
            NimUtility.Log.Info(string.Format("[OnPublishCompleted :code = {0},info:{1}]", code, info.Dump()));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int type = 0;
            int.TryParse(textBox6.Text, out type);
            int sync = 0;
            int.TryParse(textBox7.Text, out sync);
            long period = 0;
            long.TryParse(textBox9.Text, out period);

            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                var idlist = textBox8.Text.Split(new char[] { ',' });
                NIM.NIMSubscribeApi.Subscribe(type, period, (NIMEventSubscribeSyncEventType)sync, idlist.ToList(), OnSubscribeCompleted);
            }
        }

        private void OnSubscribeCompleted(ResponseCode code, int type, List<string> failedIDList)
        {
            NimUtility.Log.Info(string.Format("[OnPublishCompleted :code = {0},type = {1} failed:{2}]", code, type,
                failedIDList != null ? failedIDList.Aggregate((a, b) => a + "," + b) : "null"));
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int type = 0;
            int.TryParse(textBox6.Text, out type);
            int sync = 0;
            int.TryParse(textBox7.Text, out sync);
            long period = 0;
            long.TryParse(textBox9.Text, out period);

            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                var idlist = textBox8.Text.Split(new char[] { ',' });
                NIM.NIMSubscribeApi.UnSubscribe(type, idlist.ToList(), OnUnsubscribeCompleted);
            }
        }

        private void OnUnsubscribeCompleted(ResponseCode code, int type, List<string> failedIDList)
        {
            NimUtility.Log.Info(string.Format("[OnUnsubscribeCompleted :code = {0},type = {1} failed:{2}]", code, type,
                 failedIDList != null ? failedIDList.Aggregate((a, b) => a + "," + b) : "null"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int type = 0;
            int.TryParse(textBox6.Text, out type);
            NIM.NIMSubscribeApi.BatchUnSubscribe(type, OnBatchUnsubscribeCompleted);
        }

        private void OnBatchUnsubscribeCompleted(ResponseCode code, int type)
        {
            NimUtility.Log.Info(string.Format("[OnBatchUnsubscribeCompleted :code = {0},type = {1}", code, type));

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int type = 0;
            int.TryParse(textBox6.Text, out type);
            int sync = 0;
            int.TryParse(textBox7.Text, out sync);
            long period = 0;
            long.TryParse(textBox9.Text, out period);

            if (!string.IsNullOrEmpty(textBox8.Text))
            {
                var idlist = textBox8.Text.Split(new char[] { ',' });
                NIM.NIMSubscribeApi.QuerySubscribe(type, idlist.ToList(), OnQueryCompleted);
            }
        }

        private void OnQueryCompleted(ResponseCode code, List<NIMSubscribeStatus> subscribeList)
        {
            NimUtility.Log.Info(string.Format("[OnQueryCompleted :code = {0},subscribe list:{1}", code, subscribeList.Dump()));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int type = 0;
            int.TryParse(textBox6.Text, out type);
            NIM.NIMSubscribeApi.BatchQuerySubscribe(type, OnBatchQueryCompleted);
        }

        private void OnBatchQueryCompleted(ResponseCode code, List<NIMSubscribeStatus> subscribeList)
        {
            NimUtility.Log.Info(string.Format("[OnBatchQueryCompleted :code = {0},subscribe list:{1}", code, subscribeList.Dump()));
        }
    }
}
