using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses;
using WindowsAPI.ExtendedLinguisticServicesWrapper.Native;
using static WindowsAPI.DiagnosticsWrapper.Native.HRESULTErrorConstants;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Callbacks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSCallbacks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper
{
    /// <summary>
    /// Metodi per l'interazione con le funzionalità ELS.
    /// </summary>
    public static class ELS
    {
        /// <summary>
        /// Callback che riceve i risultati del riconoscimento testo.
        /// </summary>
        internal static MappingCallback TextRecognitionCallback = new MappingCallback(RecognizeCallback);

        /// <summary>
        /// Callback gestito per i risultati del riconoscimento testo.
        /// </summary>
        internal static RecognitionResultCallback TextRecognitionManagedCallback;

        /// <summary>
        /// Recupera i servizi ELS disponibili supportati dalla piattaforma.
        /// </summary>
        /// <param name="EnumerationOptions">Istanza di <see cref="ServiceEnumerationOptions"/> con le opzioni di enumerazione.</param>
        /// <returns></returns>
        public static ServiceInfo[] GetServices(ServiceEnumerationOptions EnumerationOptions = null)
        {
            uint Result;
            if (EnumerationOptions is null)
            {
                Result = ELSFunctions.GetServices(IntPtr.Zero, out IntPtr ServicesDataPointer, out uint ServicesCount);
                if (Result is S_OK)
                {
                    MAPPING_SERVICE_INFO[] ServiceStructures = UtilityMethods.ReadUnmanagedArray<MAPPING_SERVICE_INFO>(ServicesDataPointer, (int)ServicesCount);
                    ServiceInfo[] Services = new ServiceInfo[ServiceStructures.Length];
                    for (int i = 0; i < Services.Length; i++)
                    {
                        Services[i] = new ServiceInfo(ServiceStructures[i]);
                    }
                    return Services;
                }
                else
                {
                    Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                    return null;
                }
            }
            else
            {
                MAPPING_ENUM_OPTIONS Options = EnumerationOptions.ToStructure();
                IntPtr StructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(Options));
                Marshal.StructureToPtr(Options, StructurePointer, false);
                Result = ELSFunctions.GetServices(StructurePointer, out IntPtr ServicesDataPointer, out uint ServicesCount);
                Marshal.FreeHGlobal(StructurePointer);
                if (Result is S_OK)
                {
                    MAPPING_SERVICE_INFO[] ServiceStructures = UtilityMethods.ReadUnmanagedArray<MAPPING_SERVICE_INFO>(ServicesDataPointer, (int)ServicesCount);
                    ServiceInfo[] Services = new ServiceInfo[ServiceStructures.Length];
                    for (int i = 0; i < Services.Length; i++)
                    {
                        Services[i] = new ServiceInfo(ServiceStructures[i]);
                    }
                    return Services;
                }
                else
                {
                    Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                    return null;
                }
            }
        }

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