using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.Native
{
    /// <summary>
    /// Callback relativi alle funzionalità MUI.
    /// </summary>
    internal static class MUICallbacks
    {
        /// <summary>
        /// Elabora le informazioni sulle lingue dell'interfaccia utente.
        /// </summary>
        /// <param name="UILanguage">Lingua interfaccia.</param>
        /// <param name="lParam">Valore definito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool UILanguageEnumProcedure([MarshalAs(UnmanagedType.LPWStr)] string UILanguage, IntPtr lParam);
    }
}