using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NIM;
using NIM.Messagelog;

namespace NIMDemo
{
    class MessageLogForm:ApiTestForm
    {
        public MessageLogForm()
            :base(typeof(NIM.Messagelog.MessagelogAPI))
        {
            base.Text = "消息记录";
        }

        protected override object GenerateParamerte(Type paramType, string value)
        {
            Object obj = null;
            if (typeof(Delegate).IsAssignableFrom(paramType))
            {
                if (paramType == typeof(QueryMsglogResultDelegate))
                {
                    obj = new QueryMsglogResultDelegate(OnQueryMsgLogCompleted);
                }
                else if (paramType == typeof(QueryLogByMsgIdResultDelegate))
                {
                    obj = new QueryLogByMsgIdResultDelegate(OnQueryLogByMsgIdCompleted);
                }
                else if (paramType == typeof(OperateMsglogResultDelegate))
                {
                    obj = new OperateMsglogResultDelegate(OnOperateMsglogCompleted);
                }
                else if (paramType == typeof(OperateSingleLogResultDelegate))
                {
                   obj = new OperateSingleLogResultDelegate(OnOperateSingleLogCompleted);
                }
                else if (paramType == typeof(CommonOperationResultDelegate))
                {
                    obj = new CommonOperationResultDelegate(OnCommonOperationCompleted);
                }
                else if (paramType == typeof(ImportProgressDelegate))
                {
                    obj = new ImportProgressDelegate(ImportProgress);
                }
            }
            else
                obj = base.GenerateParamerte(paramType, value);
            return obj;
        }

        void OnQueryMsgLogCompleted(ResponseCode code, string accountId, NIM.Session.NIMSessionType sType, MsglogQueryResult result)
        {
            var x = new { Code = code, AccountId = accountId, Result = result };
            ShowOperationResult(x);
        }

        void OnQueryLogByMsgIdCompleted(ResponseCode code, string msdId, NIMIMMessage msg)
        {
            ShowOperationResult(new { Code = code, Result = msg });
        }

        void OnOperateMsglogCompleted(ResponseCode code, string uid, NIM.Session.NIMSessionType sType)
        {
            ShowOperationResult(new { Code = code, Id = uid, SessionType = sType });
        }

        void OnOperateSingleLogCompleted(ResponseCode code, string msgId)
        {
            ShowOperationResult(new { Code = code, MsgId = msgId });
        }

        void OnCommonOperationCompleted(ResponseCode code)
        {
            ShowOperationResult(new { Code = code });
        }

        void ImportProgress(long importedCount, long totalCount)
        {
            ShowOperationResult(new { Current = importedCount, TotalCount = totalCount });
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MessageLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(858, 614);
            this.Name = "MessageLogForm";
            this.ResumeLayout(false);
        }
    }
}
