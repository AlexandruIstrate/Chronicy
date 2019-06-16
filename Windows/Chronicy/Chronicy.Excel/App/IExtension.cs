using Chronicy.Excel.Tracking;

namespace Chronicy.Excel.App
{
    public abstract class IExtension
    {
        private bool enabled;
        public bool Enabled
        {
            get => enabled;
            set { enabled = value; StateChanged?.Invoke(Enabled); }
        }

        private bool connected;
        public bool Connected
        {
            get => connected;
            set { connected = value; ConnectionChanged?.Invoke(Connected); }
        }

        public ExcelTracker Tracker { get; } = new ExcelTracker();

        public delegate void StateUpdateHandler(bool enabled);
        public event StateUpdateHandler StateChanged;

        public delegate void ConnectionUpdateHandler(bool connected);
        public event ConnectionUpdateHandler ConnectionChanged;

        public virtual void OnStart() { }
        public virtual void OnShutdown() { }

        public virtual void OnRibbonLoad() { }
        public virtual void OnRibbonUnload() { }

        public abstract void Connect();
        public abstract void Sync();
    }
}
