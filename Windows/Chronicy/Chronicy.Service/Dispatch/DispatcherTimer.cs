using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace Chronicy.Service.Dispatch
{
    public class DispatcherTimer : IDispatcher
    {
        private Timer timer;
        private object actionsLock;

        public List<Action> Actions { get; }

        public DispatcherTimer(double interval = 3 * 1000)
        {
            timer = new Timer
            {
                Interval = interval,
                AutoReset = true
            };
            timer.Elapsed += OnTimerElapsed;
        }

        public void Submit(Action action)
        {
            lock (actionsLock)
            {
                Actions.Add(action);
            }
        }

        public void Remove(Action action)
        {
            lock (actionsLock)
            {
                Actions.Remove(action);
            }
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (actionsLock)
            {
                foreach (Action action in Actions)
                {
                    Task.Run(action);
                }
            }
        }
    }
}
