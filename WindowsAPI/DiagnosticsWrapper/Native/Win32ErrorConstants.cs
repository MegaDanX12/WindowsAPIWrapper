namespace WindowsAPI.ErrorHandlingWrapper.Native
{
    /// <summary>
    /// Errori Win32.
    /// </summary>
    internal static class Win32ErrorConstants
    {
        /// <summary>
        /// Operazione completata.
        /// </summary>
        internal const int ERROR_SUCCESS = 0;

        /// <summary>
        /// Parametro non corretto.
        /// </summary>
        internal const int ERROR_INVALID_PARAMETER = 87;

        /// <summary>
        /// Accesso negato.
        /// </summary>
        internal const int ERROR_ACCESS_DENIED = 5;

        /// <summary>
        /// L'area dati passata a una system call è troppo piccola.
        /// </summary>
        internal const int ERROR_INSUFFICIENT_BUFFER = 122;

        /// <summary>
        /// Il processo non ha un'identità pacchetto.
        /// </summary>
        internal const int APPMODEL_ERROR_NO_PACKAGE = 15700;

        /// <summary>
        /// Handle non valido.
        /// </summary>
        internal const int ERROR_INVALID_HANDLE = 6;

        /// <summary>
        /// Flag non valide.
        /// </summary>
        internal const int ERROR_INVALID_FLAGS = 1004;

        /// <summary>
        /// Non ci sono più file.
        /// </summary>
        internal const int ERROR_NO_MORE_FILES = 18;

        /// <summary>
        /// Il database di configurazione del registro è corrotto.
        /// </summary>
        internal const int ERROR_BADDB = 1009;

        /// <summary>
        /// Dati non validi.
        /// </summary>
        internal const int ERROR_INVALID_DATA = 13;

        /// <summary>
        /// Il modulo specificato non è stato trovato.
        /// </summary>
        internal const int ERROR_MOD_NOT_FOUND = 126;

        /// <summary>
        /// La procedura specificata non è stata trovata.
        /// </summary>
        internal const int ERROR_PROC_NOT_FOUND = 127;

        /// <summary>
        /// Memoria insufficiente per completare l'operazione.
        /// </summary>
        internal const int ERROR_OUTOFMEMORY = 14;

        /// <summary>
        /// La sintassi del nome del file, della directory o dell'etichetta del volume non è corretta.
        /// </summary>
        internal const int ERROR_INVALID_NAME = 123;

        /// <summary>
        /// Non esiste una mappatura per il carattere Unicode nella code page multi-byte.
        /// </summary>
        internal const int ERROR_NO_UNICODE_TRANSLATION = 1113;

        /// <summary>
        /// Si è verificato un errore interno.
        /// </summary>
        internal const int ERROR_INTERNAL_ERROR = 1359;

        /// <summary>
        /// Il programma è bloccato da criteri di gruppo.
        /// </summary>
        internal const int ERROR_ACCESS_DISABLED_BY_POLICY = 1260;

        /// <summary>
        /// Il codice di accesso non è valido.
        /// </summary>
        internal const int ERROR_INVALID_ACCESS = 12;

        /// <summary>
        /// Richiesta non supportata.
        /// </summary>
        internal const int ERROR_NOT_SUPPORTED = 50;
    }
}