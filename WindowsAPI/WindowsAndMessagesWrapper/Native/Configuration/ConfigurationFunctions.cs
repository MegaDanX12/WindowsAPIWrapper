using System;
using System.Runtime.InteropServices;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration.ConfigurationEnumerations;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration
{
    /// <summary>
    /// Funzioni di configurazione.
    /// </summary>
    internal static class ConfigurationFunctions
    {
        /// <summary>
        /// Recupera o imposta i parametri di sistema.
        /// </summary>
        /// <param name="Parameter">Parametro da recuperare o da impostare.</param>
        /// <param name="ParameterData1">Dati relativi al parametro.</param>
        /// <param name="ParameterData2">Dati relativi al parametro.</param>
        /// <param name="Options">Opzioni di aggiornamento profilo utente.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SystemParametersInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfo(uint Parameter, uint ParameterData1, IntPtr ParameterData2, UserProfileUpdateOptions Options);
    }
}