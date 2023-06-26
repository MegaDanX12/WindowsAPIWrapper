using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.ErrorHandlingWrapper.Native
{
    /// <summary>
    /// Funzioni per la gestione degli errori.
    /// </summary>
    internal static class ErrorHandlingFunctions
    {
        /// <summary>
        /// Imposta il codice di errore per il thread chiamante.
        /// </summary>
        /// <param name="ErrorCode">Codice di errore.</param>
        [DllImport("Kernel32.dll", EntryPoint = "SetLastError")]
        internal static extern void SetLastError(uint ErrorCode);
    }
}