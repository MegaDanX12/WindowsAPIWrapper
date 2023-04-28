﻿namespace WindowsAPI.DiagnosticsWrapper.Native
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
    }
}