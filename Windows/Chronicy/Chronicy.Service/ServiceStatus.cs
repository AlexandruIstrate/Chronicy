using System;
using System.Runtime.InteropServices;

namespace Chronicy.Service
{
    public enum ServiceState
    {
        SERVICE_STOPPED             = 0x00000001,
        SERVICE_START_PENDING       = 0x00000002,
        SERVICE_STOP_PENDING        = 0x00000003,
        SERVICE_RUNNING             = 0x00000004,
        SERVICE_CONTINUE_PENDING    = 0x00000005,
        SERVICE_PAUSE_PENDING       = 0x00000006,
        SERVICE_PAUSED              = 0x00000007
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServiceStatus
    {
        public int dwServiceType;
        public ServiceState dwCurrentState;
        public int dwControlsAccepted;
        public int dwWin32ExitCode;
        public int dwServiceSpecificExitCode;
        public int dwCheckPoint;
        public int dwWaitHint;
    };

    public class Status
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);

        public static ServiceStatus Stopped => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_STOPPED };
        public static ServiceStatus StartPending => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_START_PENDING };
        public static ServiceStatus StopPending => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_STOP_PENDING };
        public static ServiceStatus Running => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_RUNNING };
        public static ServiceStatus ContinuePending => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_CONTINUE_PENDING };
        public static ServiceStatus PausePending => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_PAUSE_PENDING };
        public static ServiceStatus Paused => new ServiceStatus() { dwCurrentState = ServiceState.SERVICE_PAUSED };
    }
}
