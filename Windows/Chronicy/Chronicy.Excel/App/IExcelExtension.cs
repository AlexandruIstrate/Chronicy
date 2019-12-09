using Chronicy.Authentication;
using Chronicy.Data;
using Chronicy.Data.Managers;
using Chronicy.Data.Storage;
using Chronicy.Excel.History;
using Chronicy.Excel.Tracking;
using System.Collections.Generic;

namespace Chronicy.Excel.App
{
    /// <summary>
    /// Provides operations for working with an Excel extension.
    /// </summary>
    public abstract class IExcelExtension
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

        // Change these to abstract types
        public abstract TrackingSystem Tracking { get; }
        public abstract NotebookManager Notebooks { get; }
        public abstract HistoryManager History { get; }
        public abstract IAuthenticationManager CredentialsManager { get; }

        public delegate void StateUpdateHandler(bool enabled);
        public delegate void ConnectionUpdateHandler(bool connected);
        public delegate void AvailableNotebooksHandler(IEnumerable<Notebook> notebooks);

        public event StateUpdateHandler StateChanged;
        public event ConnectionUpdateHandler ConnectionChanged;
        public event AvailableNotebooksHandler NotebooksUpdated;

        public virtual void OnStart() { }
        public virtual void OnShutdown() { }

        public virtual void OnRibbonLoad() { }
        public virtual void OnRibbonUnload() { }

        public abstract void Connect();
        public abstract void Sync();

        public abstract void SelectDataSource(DataSourceType dataSource);
        public abstract void SelectNotebook(Notebook notebook);
        public abstract void SelectStack(Stack stack);

        protected void OnNotebooksUpdated(IEnumerable<Notebook> notebooks = null)
        {
            NotebooksUpdated?.Invoke(notebooks ?? Notebooks.GetNotebooks());
        }
    }
}
