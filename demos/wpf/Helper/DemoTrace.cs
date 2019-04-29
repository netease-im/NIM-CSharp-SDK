using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace NIMDemo
{
    static class DemoTrace
    {
        public static void WriteLine(params object[] args)
        {
            StackTrace st = new StackTrace();
            var index = Math.Min(1, st.FrameCount);
            var frame = st.GetFrame(index);
            string msg = string.Empty;
            if(frame != null)
            {
                var m = frame.GetMethod().Name;
                msg = string.Format("{0} [{1}] ({2})", DateTime.Now.ToLongTimeString(), System.Threading.Thread.CurrentThread.ManagedThreadId, m);
            }
            else
            {
                msg = string.Format("{0} [{1}]", DateTime.Now.ToLongTimeString(), System.Threading.Thread.CurrentThread.ManagedThreadId);
            }
            if (args != null)
            {
                msg += " : ";
                msg = args.Aggregate(msg, (current, arg) => current + arg.ToString() + ',');
            }
            msg = msg.TrimEnd(',');
            System.Diagnostics.Debug.WriteLine(msg);
            Console.WriteLine(msg);
            OutputForm.SetText(msg);
        }
    }
}
