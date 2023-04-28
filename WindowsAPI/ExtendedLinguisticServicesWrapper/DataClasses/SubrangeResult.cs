using System.Runtime.InteropServices;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses
{
    /// <summary>
    /// Risultati dell'elaborazione di una sezione di un testo.
    /// </summary>
    public class SubrangeResult
    {
        /// <summary>
        /// Indice iniziale della sezione del testo.
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// Indice finale della sezione del testo.
        /// </summary>
        public int EndIndex { get; }

        /// <summary>
        /// Dati risultato dell'elaborazione da parte del servizio della sezione del testo.
        /// </summary>
        public byte[] Data { get; }

        /// <summary>
        /// Tipo di contenuto MIME dei dati.
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// ID delle azioni disponibile per la sezione.
        /// </summary>
        public string[] ActionIDs { get; }

        /// <summary>
        /// Nomi delle azioni disponibili per la sezione.
        /// </summary>
        public string[] ActionDisplayNames { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SubrangeResult"/>.
        /// </summary>
        /// <param name="RangeResults">Struttura <see cref="MAPPING_DATA_RANGE"/> con i dati.</param>
        internal SubrangeResult(MAPPING_DATA_RANGE RangeResults)
        {
            StartIndex = (int)RangeResults.StartIndex;
            EndIndex = (int)RangeResults.EndIndex;
            if (RangeResults.DataSize > 0)
            {
                Data = new byte[RangeResults.DataSize];
                Marshal.Copy(RangeResults.Data, Data, 0, Data.Length);
            }
            else
            {
                Data = null;
            }
            ContentType = RangeResults.ContentType;
            ActionIDs = UtilityMethods.ReadUnmanagedArray<string>(RangeResults.ActionIds, (int)RangeResults.ActionsCount);
            ActionDisplayNames = UtilityMethods.ReadUnmanagedArray<string>(RangeResults.ActionDisplayNames, (int)RangeResults.ActionDisplayNames);
        }
    }
}