using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TaskManager
{
    public class Job : EventArgs, IDisposable
    {
        public string Name { get; set; }
    
        public DateTime Execution { get; private set; }
        public bool Triggered { get; set; }
        public bool Repeating { get; set; }
        private int _interval;
        private Timer _triggerTimer;

        public Job(DateTime executionTime, string name = null)
        {
            Name = name;
          
            Execution = executionTime;
        }

        public Job TriggerEvery(int interval)
        {
            _interval = interval;
            Repeating = true;
            return this;
        }

        public Job Seconds()
        {
            _interval = (_interval) * 1000;
            return this;
        }

        public Job Minutes()
        {
            _interval = _interval * (1000 * 60);
            return this;
        }

        public Job Hours()
        {
            _interval = _interval * (60 * 60 * 1000);
            return this;
        }

        public Job Days()
        {
            _interval = _interval * (60 * 60 * 24 * 1000);
            return this;
        }

        public void TriggerTimerCallBack(object sender)
        {
            if (Triggered)
                Triggered = false;
        }

        public void StartRepeating()
        {
            if (null != _triggerTimer) return;
            _triggerTimer = new Timer(TriggerTimerCallBack, null, _interval, _interval);
        }

        public void StopRepeating()
        {
            if (null == _triggerTimer) return;
            _triggerTimer.Dispose();
            _triggerTimer = null;
        }

        public void Dispose()
        {
            StopRepeating();
        }
    }
}
