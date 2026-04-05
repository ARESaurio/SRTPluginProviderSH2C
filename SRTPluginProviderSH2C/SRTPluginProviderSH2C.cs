using ProcessMemory;
using SRTPluginBase;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace SRTPluginProviderSH2C
{
    public class SRTPluginProviderSH2C : IPluginProvider
    {
        private Process process;
        private GameMemorySH2CScanner gameMemoryScanner;
        private IPluginHostDelegates hostDelegates;

        public IPluginInfo Info => new PluginInfo();

        public bool GameRunning
        {
            get
            {
                try { return process != null && !process.HasExited; }
                catch { return false; }
            }
        }

        public int Startup(IPluginHostDelegates hostDelegates)
        {
            this.hostDelegates = hostDelegates;
            gameMemoryScanner = new GameMemorySH2CScanner(); // Always create the scanner first.
            process = GetProcess();
            if (process != null)
                gameMemoryScanner.Initialize(process); // Initialize only if game is running.
            return 0;
        }

        public int Shutdown()
        {
            gameMemoryScanner?.Dispose();
            gameMemoryScanner = null;
            process = null;
            return 0;
        }

        public object PullData()
        {
            try
            {
                // If the game process disappeared, try to find it again.
                if (!GameRunning)
                {
                    process = GetProcess();
                    if (process != null)
                        gameMemoryScanner.Initialize(process);
                }

                if (!gameMemoryScanner.ProcessRunning)
                    return null;

                return gameMemoryScanner.Refresh();
            }
            catch (Win32Exception ex)
            {
                // ERROR_PARTIAL_COPY happens normally when the game exits or pointer tables shift.
                if ((Win32Error)ex.NativeErrorCode != Win32Error.ERROR_PARTIAL_COPY)
                    hostDelegates.ExceptionMessage.Invoke(ex);
                return null;
            }
            catch (Exception ex)
            {
                hostDelegates.ExceptionMessage.Invoke(ex);
                return null;
            }
        }

        private Process GetProcess() => Process.GetProcesses()
            .Where(a => a.ProcessName.StartsWith("sh2pc", StringComparison.InvariantCultureIgnoreCase))
            .FirstOrDefault();
    }
}
