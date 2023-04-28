using System.Runtime.InteropServices;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses
{
    /// <summary>
    /// Proprietà operazione riconoscimento testo.
    /// </summary>
    public class TextRecognitionProperties
    {
        /// <summary>
        /// Array di istanze di <see cref="SubrangeResult"/> contenenti i risultati dell'elaborazione di una sezione del testo.
        /// </summary>
        public SubrangeResult[] ResultRanges { get; }

        /// <summary>
        /// Dati privati del servizio.
        /// </summary>
        public byte[] ServiceData { get; }

        /// <summary>
        /// Dati privati dell'applicazione.
        /// </summary>
        public byte[] CallerData { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="TextRecognitionOptions"/>.
        /// </summary>
        /// <param name="PropertyBag">Struttura <see cref="MAPPING_PROPERTY_BAG"/> con le informazioni.</param>
        internal TextRecognitionProperties(MAPPING_PROPERTY_BAG PropertyBag)
        {
            MAPPING_DATA_RANGE[] Ranges = UtilityMethods.ReadUnmanagedArray<MAPPING_DATA_RANGE>(PropertyBag.ResultRanges, (int)PropertyBag.RangesCount);
            ResultRanges = new SubrangeResult[Ranges.Length];
            for (int i = 0; i < ResultRanges.Length; i++)
            {
                ResultRanges[i] = new SubrangeResult(Ranges[i]);
            }
            if (PropertyBag.ServiceDataSize > 0)
            {
                ServiceData = new byte[PropertyBag.ServiceDataSize];
                Marshal.Copy(PropertyBag.ServiceData, ServiceData, 0, ServiceData.Length);
            }
            else
            {
                ServiceData = null;
            }
            if (PropertyBag.CallerDataSize > 0)
            {
                CallerData = new byte[PropertyBag.CallerDataSize];
                Marshal.Copy(PropertyBag.CallerData, CallerData, 0, CallerData.Length);
            }
            else
            {
                CallerData = null;
            }
        }
    }
}