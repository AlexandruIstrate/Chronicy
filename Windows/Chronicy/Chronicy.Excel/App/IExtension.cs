namespace Chronicy.Excel.App
{
    public abstract class IExtension
    {
        public bool Enabled { get; set; }

        public virtual void OnStart()
        {
            
        }

        public virtual void OnShutdown()
        {

        }

        public virtual void OnRibbonLoad()
        {

        }
    }
}
