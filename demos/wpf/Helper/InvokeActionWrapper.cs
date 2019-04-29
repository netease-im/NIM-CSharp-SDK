using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NIMDemo
{
    public class InvokeActionWrapper
    {
        private readonly Control _control;
        private readonly System.Threading.Semaphore _semaphore;
        private readonly Dictionary<Control, List<Action>> _actionCollection;

        public InvokeActionWrapper(Control control)
        {
            _control = control;
            _control.HandleCreated += _control_HandleCreated;
            _semaphore = new System.Threading.Semaphore(0, short.MaxValue);
            _actionCollection = new Dictionary<Control, List<Action>>();
            _actionCollection[control] = new List<Action>();
        }

        private void _control_HandleCreated(object sender, EventArgs e)
        {
            var control = sender as Control;
            _semaphore.Release(short.MaxValue);
            lock (_actionCollection)
            {
                var acList = _actionCollection[control];
                foreach (var ac in acList)
                {
                    ac();
                }
                acList.Clear();
            }
        }

        public void InvokeAction(Action action)
        {
            if (!_control.IsHandleCreated)
            {
                lock (_actionCollection)
                {
                    _actionCollection[_control].Add(action);
                }
            }
            else
            {
                _control.Invoke(action);
            }
        }

        public void InvokeAction(Delegate d, params object[] args)
        {
            if (!_control.IsHandleCreated)
            {
                _semaphore.WaitOne();
            }
            _control.Invoke(d, args);
        }
    }
}
