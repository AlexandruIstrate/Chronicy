using System.Diagnostics;

namespace Chronicy.Excel.App
{
    public class AppExtension : IExtension
    {
        public override void OnStart()
        {
            Debug.WriteLine("Extension OnStart");
        }

        public override void OnShutdown()
        {
            Debug.WriteLine("Extension OnShutdown");
        }
    }
}
