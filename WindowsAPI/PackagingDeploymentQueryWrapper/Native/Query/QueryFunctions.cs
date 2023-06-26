using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;

namespace WindowsAPI.PackagingDeploymentQueryWrapper.Native.Query
{
    /// <summary>
    /// Funzioni per richiesta di informazioni sui pacchetti applicazioni.
    /// </summary>
    internal static class QueryFunctions
    {
        /// <summary>
        /// Recupera il nome completo del pacchetto per il processo chiamante.
        /// </summary>
        /// <param name="NameLength">In input, la dimensione del buffer in caratteri, in output, la dimensione, in caratteri, del nome incluse il terminatore nullo.</param>
        /// <param name="PackageFullName">Nome completo del pacchetto.</param>
        /// <returns><see cref="ERROR_SUCCESS"/> se l'operazione è riuscita, <see cref="ERROR_INSUFFICIENT_BUFFER"/> se il buffer è troppo piccolo, <see cref="APPMODEL_ERROR_NO_PACKAGE"/> se il processo non ha un'identità di pacchetto.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetCurrentPackageFullName", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetCurrentPackageFullName(ref uint NameLength, StringBuilder PackageFullName);
    }
}