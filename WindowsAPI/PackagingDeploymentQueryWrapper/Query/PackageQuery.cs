using WindowsAPI.PackagingDeploymentQueryWrapper.Native;
using WindowsAPI.PackagingDeploymentQueryWrapper.Native.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;

namespace WindowsAPI.PackagingDeploymentQueryWrapper.Query
{
    /// <summary>
    /// Metodi per la raccolta di informazioni sui pacchetti applicazioni.
    /// </summary>
    public static class PackageQuery
    {
        /// <summary>
        /// Recupera il nome completo del pacchetto dell'applicazione corrente.
        /// </summary>
        /// <returns>Il nome completo del pacchetto.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string GetCurrentApplicationFullPackageName()
        {
            StringBuilder PackageFullName = new StringBuilder(Constants.PACKAGE_FULL_NAME_MAX_LENGTH);
            uint BufferSize = Constants.PACKAGE_FULL_NAME_MAX_LENGTH;
            int Result = QueryFunctions.GetCurrentPackageFullName(ref BufferSize, PackageFullName);
            if (Result != ERROR_SUCCESS)
            {
                if (Result is ERROR_INSUFFICIENT_BUFFER)
                {
                    return null;
                }
                else if (Result is APPMODEL_ERROR_NO_PACKAGE)
                {
                    throw new InvalidOperationException("The process has no package identity.");
                }
                else
                {
                    throw new Win32Exception(Result);
                }
            }
            else
            {
                return PackageFullName.ToString();
            }
        }
    }
}