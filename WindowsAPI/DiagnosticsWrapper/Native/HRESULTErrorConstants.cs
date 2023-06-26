namespace WindowsAPI.ErrorHandlingWrapper.Native
{
    /// <summary>
    /// Codici di errore HRESULT.
    /// </summary>
    internal static class HRESULTErrorConstants
    {
        /// <summary>
        /// Operazione completata.
        /// </summary>
        internal const uint S_OK = 0;

        /// <summary>
        /// Parametri non validi.
        /// </summary>
        internal const uint E_INVALIDARG = 2147942487;

        /// <summary>
        /// Accesso negato.
        /// </summary>
        internal const uint E_ACCESSDENIED = 2147942405;
    }
}