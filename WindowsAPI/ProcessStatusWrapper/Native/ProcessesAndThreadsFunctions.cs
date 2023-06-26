using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.ErrorHandlingWrapper.Native
{
    /// <summary>
    /// Funzioni relative ai processi e ai thread.
    /// </summary>
    internal static class ProcessesAndThreadsFunctions
    {
        /// <summary>
        /// Recupera l'identificatore del processo chiamante.
        /// </summary>
        /// <returns>L'identificatore del processo.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetCurrentProcessId", SetLastError = true)]
        internal static extern uint GetCurrentPID();
    }
}