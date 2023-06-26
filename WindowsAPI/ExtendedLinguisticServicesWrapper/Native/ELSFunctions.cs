using System;
using System.Runtime.InteropServices;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;
using static WindowsAPI.ErrorHandlingWrapper.Native.HRESULTErrorConstants;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.Native
{
    /// <summary>
    /// Funzioni ELS.
    /// </summary>
    internal static class ELSFunctions
    {
        /// <summary>
        /// Recupera una lista di servizi ELS supportati dalla piattaforma in base ai criteri specificati.
        /// </summary>
        /// <param name="OptionsPointer">Puntatore a struttura <see cref="MAPPING_ENUM_OPTIONS"/> contenente i criteri da usare durante l'enumerazione dei servizi.</param>
        /// <param name="ServicesDataPointer">Puntatore a un array di strutture <see cref="MAPPING_SERVICE_INFO"/> che contengono le informazioni dei servizi che corrispondono ai criteri.</param>
        /// <param name="ServicesCount">Numero di servizi recuperati.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        /// <remarks>Per recuperare tutti i servizi <paramref name="OptionsPointer"/> deve essere <see cref="IntPtr.Zero"/>.</remarks>
        [DllImport("Elscore.dll", EntryPoint = "MappingGetServices", SetLastError = true)]
        internal static extern uint GetServices(IntPtr OptionsPointer, out IntPtr ServicesDataPointer, out uint ServicesCount);

        /// <summary>
        /// Esegue il riconoscimento di testo utilizzando un servizio ELS.
        /// </summary>
        /// <param name="ServiceInfo">Puntatore a struttura <see cref="MAPPING_SERVICE_INFO"/> che descrive il servizio da utilizzare.</param>
        /// <param name="Text">Testo da riconoscere.</param>
        /// <param name="Length">Lunghezza, in caratteri, del testo.</param>
        /// <param name="Index">Indice nel testo usato dal servizio.</param>
        /// <param name="Options">Puntatore a struttura <see cref="MAPPING_OPTIONS"/> contenente le opzioni che influenzano i risultati e il comportamento dell'operazione di riconoscimento testo.</param>
        /// <param name="Bag">Puntatore a struttura <see cref="MAPPING_PROPERTY_BAG"/> che memorizza i risultati del riconoscimento.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        /// <remarks><paramref name="ServiceInfo"/> e <paramref name="Text"/> non possono essere nulli.<br/><br/>
        /// Il valore di <paramref name="Index"/> deve essere compreso tra 0 e <paramref name="Length"/> - 1, per elaborare tutto il testo, questo parametro deve essere 0.<br/><br/>
        /// <paramref name="Options"/> può essere nullo per usare le opzioni di mappatura predefinite.<br/><br/>
        /// Il campo <see cref="MAPPING_PROPERTY_BAG.Size"/> del parametro <paramref name="Bag"/> deve essere impostato prima della chiamata alla funzione, in output il parametro riceve i risultati del riconoscimento del testo, non può essere nullo.</remarks>
        [DllImport("Elscore.dll", EntryPoint = "MappingRecognizeText", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint RecognizeText(IntPtr ServiceInfo, string Text, uint Length, uint Index, IntPtr Options, IntPtr Bag);

        /// <summary>
        /// Fa in modo che un servizio ELS esegua un'azione dopo il riconoscimento del testo.
        /// </summary>
        /// <param name="Bag">Puntatore a struttura <see cref="MAPPING_PROPERTY_BAG"/> contenente i risultati di una precedente chiamata a <see cref="RecognizeText"/>.</param>
        /// <param name="RangeIndex">Indice di partenza all'interno dei risultati del riconoscimento per una sezione del testo riconosciuto.</param>
        /// <param name="ActionID">Identificatore dell'azione da eseguire.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Elscore.dll", EntryPoint = "MappingDoAction", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint DoAction(IntPtr Bag, uint RangeIndex, string ActionID);

        /// <summary>
        /// Libera la memoria e le risorse allocate durante un'operazione di riconoscimento testo.
        /// </summary>
        /// <param name="Bag">Puntatore a struttura <see cref="MAPPING_PROPERTY_BAG"/> contenente le proprietà per cui le risorse vanno liberate.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Elscore.dll", EntryPoint = "MappingFreePropertyBag", SetLastError = true)]
        internal static extern uint FreePropertyBag(IntPtr Bag);

        /// <summary>
        /// Libera la memoria e le risorse allocate per un'applicazione per interagire con uno o più dei servizi ELS.
        /// </summary>
        /// <param name="ServiceInfo">Puntatore a un array di strutture <see cref="MAPPING_SERVICE_INFO"/> precedentemente recuperati tramite una chiamata a <see cref="GetServices"/>.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Elscore.dll", EntryPoint = "MappingFreeServices", SetLastError = true)]
        internal static extern uint FreeServices(IntPtr ServiceInfo);
    }
}