using System;
using System.Runtime.InteropServices;
using WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Callbacks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSCallbacks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;
using static WindowsAPI.DiagnosticsWrapper.Native.HRESULTErrorConstants;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.Native
{
    /// <summary>
    /// Dati di lavoro ELS.
    /// </summary>
    internal static class ELSMetadata
    {
        /// <summary>
        /// Callback che riceve i risultati del riconoscimento testo.
        /// </summary>
        internal static MappingCallback TextRecognitionCallback = new MappingCallback(RecognizeCallback);

        /// <summary>
        /// Callback gestito per i risultati del riconoscimento testo.
        /// </summary>
        internal static RecognitionResultCallback TextRecognitionManagedCallback = null;

        /// <summary>
        /// Opzioni per il riconoscimento del testo.
        /// </summary>
        internal static IntPtr RecognitionOptionsPointer = IntPtr.Zero;

        /// <summary>
        /// Informazioni sul servizio.
        /// </summary>
        internal static IntPtr ServiceInfoPointer;

        /// <summary>
        /// Risultati del riconoscimento testo.
        /// </summary>
        internal static IntPtr BagPointer;

        /// <summary>
        /// Puntatore all'array di struttura che descrivono i servizi.
        /// </summary>
        internal static IntPtr ServicesArrayPointer;

        /// <summary>
        /// Callback eseguito quando il riconoscimento del testo è asincrono.
        /// </summary>
        /// <param name="BagPointer">Puntatore a struttura <see cref="MAPPING_PROPERTY_BAG"/> con i risultati della chiamata.</param>
        /// <param name="ApplicationDataPointer">Puntatore ai dati privati dell'applicazione.</param>
        /// <param name="DataSize">Dimensione, in byte, dei dati dell'applicazione.</param>
        /// <param name="Result">Risulato della chiamata.</param>
        internal static void RecognizeCallback(IntPtr BagPointer, IntPtr ApplicationDataPointer, uint DataSize, uint Result)
        {
            MAPPING_PROPERTY_BAG PropertyBag = (MAPPING_PROPERTY_BAG)Marshal.PtrToStructure(BagPointer, typeof(MAPPING_PROPERTY_BAG));
            TextRecognitionProperties Properties = new TextRecognitionProperties(PropertyBag);
            byte[] ApplicationData = null;
            if (DataSize > 0)
            {
                ApplicationData = new byte[DataSize];
                Marshal.Copy(ApplicationDataPointer, ApplicationData, 0, ApplicationData.Length);
            }
            bool Success = Result is S_OK;
            TextRecognitionManagedCallback.Invoke(Properties, ApplicationData, Success);
        }
    }
}