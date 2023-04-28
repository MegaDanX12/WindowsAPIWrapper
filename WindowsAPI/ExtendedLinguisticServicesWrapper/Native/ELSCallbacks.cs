using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.Native
{
    /// <summary>
    /// Callback ELS.
    /// </summary>
    internal static class ELSCallbacks
    {
        /// <summary>
        /// Delegato che elabora in modo asincrono dati prodotti dalla funzione <see cref="ELSFunctions.RecognizeText"/>.
        /// </summary>
        /// <param name="Bag">Puntatore a struttura <see cref="ELSStructures.MAPPING_PROPERTY_BAG"/> che contine i risultati della chiamata.</param>
        /// <param name="PrivateData">Puntatore a dati privati dell'applicazione.</param>
        /// <param name="DataSize">Dimensione, in byte, dei dati privati dell'applicazione.</param>
        /// <param name="Result">Risultato della chiamata.</param>
        /// <remarks>L'applicazione dovrebbe controllare il valore del parametro Result come prima cosa.</remarks>
        internal delegate void MappingCallback(IntPtr Bag, IntPtr PrivateData, uint DataSize, uint Result);
    }
}