using System;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration
{
    /// <summary>
    /// Enumerazioni usate dalle funzioni di configurazione.
    /// </summary>
    internal static class ConfigurationEnumerations
    {
        /// <summary>
        /// Opzioni di aggiornamento profilo utente.
        /// </summary>
        [Flags]
        internal enum UserProfileUpdateOptions : uint
        {
            /// <summary>
            /// Scrive la nuova impostazione nel profilo utente.
            /// </summary>
            SPIF_UPDATEINIFILE = 1,
            /// <summary>
            /// Invia il messaggio WM_SETTINGCHANGE dopo aver aggiornato il profilo utente.
            /// </summary>
            SPIF_SENDWININICHANGE = 2
        }
    }
}