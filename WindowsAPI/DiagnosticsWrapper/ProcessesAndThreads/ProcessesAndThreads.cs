using WindowsAPI.DiagnosticsWrapper.Native.ProcessesAndThreads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.DiagnosticsWrapper.ProcessesAndThreads
{
    /// <summary>
    /// Metodi per la gestione dei processi e dei thread.
    /// </summary>
    public static class ProcessesAndThreads
    {
        /// <summary>
        /// Recupera l'ID del processo corrente.
        /// </summary>
        /// <returns>L'ID del processo.</returns>
        public static int GetCurrentProcessID()
        {
            return (int)ProcessesAndThreadsFunctions.GetCurrentPID();
        }
    }
}