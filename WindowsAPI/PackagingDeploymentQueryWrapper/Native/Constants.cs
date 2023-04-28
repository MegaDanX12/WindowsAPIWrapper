using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.PackagingDeploymentQueryWrapper.Native
{
    /// <summary>
    /// Costanti usate dalle API.
    /// </summary>
    internal static class Constants
    {
        /// <summary>
        /// Lunghezza minima del nome completo del pacchetto.
        /// </summary>
        internal const int PACKAGE_FULL_NAME_MIN_LENGTH = 30;

        /// <summary>
        /// Lunghezza massima del nome completo del pacchetto.
        /// </summary>
        internal const int PACKAGE_FULL_NAME_MAX_LENGTH = 127;
    }
}