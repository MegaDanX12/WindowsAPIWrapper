namespace WindowsAPI.MultilingualUserInterfaceWrapper
{
    /// <summary>
    /// Callback usati dalle funzionalità MUI.
    /// </summary>
    public static class Callbacks
    {
        /// <summary>
        /// Elabora le informazioni sulle lingue dell'interfaccia utente.
        /// </summary>
        /// <param name="Language">Lingua interfaccia.</param>
        /// <param name="Object">Oggetto fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        public delegate bool UILanguageEnumerationCallback(string Language, object Object);
    }
}