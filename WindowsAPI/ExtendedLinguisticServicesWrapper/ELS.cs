using System;
using System.Runtime.InteropServices;
using WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses;
using WindowsAPI.ExtendedLinguisticServicesWrapper.Native;
using WindowsAPI.SafeHandles;
using static WindowsAPI.ErrorHandlingWrapper.Native.HRESULTErrorConstants;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper
{
    /// <summary>
    /// Metodi per l'interazione con le funzionalità ELS.
    /// </summary>
    public static class ELS
    {
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
                    ELSMetadata.ServicesArrayPointer = ServicesDataPointer;
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
                using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(Options)))
                {
                    StructurePointer.WriteToMemory<MAPPING_ENUM_OPTIONS>(Options);
                    Result = ELSFunctions.GetServices(StructurePointer, out IntPtr ServicesDataPointer, out uint ServicesCount);
                    if (Result is S_OK)
                    {
                        ELSMetadata.ServicesArrayPointer = ServicesDataPointer;
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
        }

        /// <summary>
        /// Esegue l'operazione di riconoscimento testo utilizzando il servizio indicato.
        /// </summary>
        /// <param name="Service">Servizio da utilizzare.</param>
        /// <param name="Text">Testo su cui eseguire l'operazione.</param>
        /// <param name="Index">Indice all'interno del testo che il servizio deve usare.</param>
        /// <param name="Options">Opzioni di riconoscimento.</param>
        /// <returns>Un'istanza di una classe <see cref="TextRecognitionProperties"/> con i risultati dell'operazione.</returns>
        public static TextRecognitionProperties RecognizeText(ServiceInfo Service, string Text, int Index, TextRecognitionOptions Options = null) 
        {
            IntPtr ServiceDataStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(Service.ServiceInfoStructure));
            Marshal.StructureToPtr(Service.ServiceInfoStructure, ServiceDataStructurePointer, false);
            ELSMetadata.ServiceInfoPointer = ServiceDataStructurePointer;
            IntPtr OptionsStructurePointer = IntPtr.Zero;
            if (Options != null)
            {
                MAPPING_OPTIONS OptionsStructure = Options.ToStructure();
                OptionsStructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(OptionsStructure));
                Marshal.StructureToPtr(OptionsStructure, OptionsStructurePointer, false);
                ELSMetadata.RecognitionOptionsPointer = OptionsStructurePointer;
            }
            IntPtr BagPointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MAPPING_PROPERTY_BAG)));
            MAPPING_PROPERTY_BAG Bag = new MAPPING_PROPERTY_BAG()
            {
                Size = new IntPtr(Marshal.SizeOf(typeof(MAPPING_PROPERTY_BAG)))
            };
            Marshal.StructureToPtr(Bag, BagPointer, false);
            ELSMetadata.BagPointer = BagPointer;
            uint Result = ELSFunctions.RecognizeText(ServiceDataStructurePointer, Text, (uint)Text.Length, (uint)Index, OptionsStructurePointer, BagPointer);
            if (Result is S_OK)
            {
                Bag = (MAPPING_PROPERTY_BAG)Marshal.PtrToStructure(BagPointer, typeof(MAPPING_PROPERTY_BAG));
                return new TextRecognitionProperties(Bag);
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return null;
            }
        }

        /// <summary>
        /// Esegue un'azione.
        /// </summary>
        /// <param name="StartingIndex">Indice di partenza all'interno del testo.</param>
        /// <param name="ActionID">ID dell'azione da eseguire.</param>
        public static void DoAction(int StartingIndex, string ActionID)
        {
            uint Result = ELSFunctions.DoAction(ELSMetadata.BagPointer, (uint)StartingIndex, ActionID);
            if (Result != S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Libera la memoria e le risorse allocate durante un'operazione di riconoscimento testo.
        /// </summary>
        public static void FreePropertiesData()
        {
            uint Result = ELSFunctions.FreePropertyBag(ELSMetadata.BagPointer);
            if (Result != S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
            else
            {
                ELSMetadata.TextRecognitionManagedCallback = null;
                if (ELSMetadata.RecognitionOptionsPointer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ELSMetadata.RecognitionOptionsPointer);
                }
                Marshal.FreeHGlobal(ELSMetadata.BagPointer);
            }
        }

        /// <summary>
        /// Libera la memoria e le risorse allocate per permettere all'applicazione di interagire con i servizi ELS.
        /// </summary>
        public static void FreeServices()
        {
            uint Result = ELSFunctions.FreeServices(ELSMetadata.ServicesArrayPointer);
            if (Result != S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
            else
            {
                Marshal.FreeHGlobal(ELSMetadata.ServiceInfoPointer);
            }
        }
    }
}