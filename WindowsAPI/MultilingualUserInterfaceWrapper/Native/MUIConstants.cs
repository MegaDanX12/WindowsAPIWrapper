using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIStructures;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.Native
{
    /// <summary>
    /// Costanti relative alle funzionalità MUI.
    /// </summary>
    internal static class MUIConstants
    {
        /// <summary>
        /// Recupera la lista di lingue dell'interfaccia utente preferite del sistema.
        /// </summary>
        /// <remarks>Il sistema verifica che ogni lingua corrisponda a una località NLS valida, se questo valore non è impostato, la lista viene recuperata sempre che non sia vuota e la convalida abbia successo.<br/>
        /// Se la lista è vuota o la convalida fallisce viene recuperata la lingua utente predefinita dell'interfaccia utente.</remarks>
        internal const uint MUI_MACHINE_LANGUAGE_SETTINGS = 1024;

        /// <summary>
        /// Versione della struttura <see cref="FILEMUIINFO"/>.
        /// </summary>
        internal const uint MUI_FILEINFO_VERSION = 1;
    }
}