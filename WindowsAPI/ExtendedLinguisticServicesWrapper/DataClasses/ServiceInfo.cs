using System;
using System.Runtime.InteropServices;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses
{
    /// <summary>
    /// Informazioni su un servizio ELS.
    /// </summary>
    public class ServiceInfo
    {
        /// <summary>
        /// Informazioni di copyright.
        /// </summary>
        public string Copyright { get; }

        /// <summary>
        /// Versione del servizio.
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Tipi di contenuto MIME interpretabili dal servizio.
        /// </summary>
        public string[] InputContentTypes { get; }

        /// <summary>
        /// Tipi di contenuto MIME nei quali il servizio recupera i dati.
        /// </summary>
        public string[] OutputContentTypes { get; }

        /// <summary>
        /// Lingue accettate in input dal servizio.
        /// </summary>
        /// <remarks>Il nome delle lingue segue le convenzioni IETF, questo campo ha un valore di "Any" se il servizio può lavorare con qualunque lingua.</remarks>
        public string[] InputLanguages { get; }

        /// <summary>
        /// Lingue nelle quali il servizio può restiuire risultati.
        /// </summary>
        /// <remarks>Il nome delle lingue segue le convenzioni IETF, questo campo ha un valore di "Any" se il servizio può recuperare risultato in qualunque lingua o se la ignora.</remarks>
        public string[] OutputLanguages { get; }

        /// <summary>
        /// Nome Unicode di script supportati in input dal servizio.
        /// </summary>
        /// <remarks>Questo campo ha un valore di "Any" se il servizio supporta qualunque script o se lo ignora.</remarks>
        public string[] InputScripts { get; }

        /// <summary>
        /// Nome Unicode di script di output supportati dal servizio.
        /// </summary>
        /// <remarks>Questo campo ha un valore di "Any" se il servizio supporta qualunque script o se lo ignora.</remarks>
        public string[] OutputScripts { get; }

        /// <summary>
        /// Identificatore univoco globale del servizio.
        /// </summary>
        public Guid Guid { get; }

        /// <summary>
        /// Categoria a cui appartiene il servizio.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Descrizione del servizio.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Puntatore ai dati privati del servizio.
        /// </summary>
        public byte[] PrivateData { get; }

        /// <summary>
        /// Indica il tipo di mappatura tra la lingua di input e quella di output supportata dal servizio.
        /// </summary>
        /// <remarks>Questo campo può avere i seguenti valori:<br/><br/>
        /// 0: non c'è nessun collegamento tra lingua di input e output<br/>
        /// 1: la lingua di input è collegata a una specifica lingua di output</remarks>
        public bool OneToOneLanguageMapping { get; }

        /// <summary>
        /// Indica se il servizio ha altri servizi che da esso dipendono.
        /// </summary>
        /// <remarks>Questo campo può avere i seguenti valori:<br/><br/>
        /// 0: il servizio è stand alone<br/>
        /// 1: il servizio agisce come padre per altri servizi</remarks>
        public bool HasSubservices { get; }

        /// <summary>
        /// Struttura che descrive il servizio.
        /// </summary>
        internal MAPPING_SERVICE_INFO ServiceInfoStructure { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="ServiceInfo"/>.
        /// </summary>
        /// <param name="ServiceInfo">Struttura <see cref="MAPPING_SERVICE_INFO"/> con le informazioni sul servizio.</param>
        internal ServiceInfo(MAPPING_SERVICE_INFO ServiceInfo)
        {
            Copyright = ServiceInfo.Copyright;
            Version = new Version(ServiceInfo.MajorVersion, ServiceInfo.MinorVersion, ServiceInfo.BuildVersion, ServiceInfo.StepVersion);
            InputContentTypes = UtilityMethods.ReadUnmanagedArray<string>(ServiceInfo.InputContentTypes, (int)ServiceInfo.InputContentTypesCount);
            OutputContentTypes = UtilityMethods.ReadUnmanagedArray<string>(ServiceInfo.OutputContentTypes, (int)ServiceInfo.OutputContentTypesCount);
            InputLanguages = UtilityMethods.ReadUnmanagedArray<string>(ServiceInfo.InputLanguages, (int)ServiceInfo.InputLanguagesCount);
            OutputLanguages = UtilityMethods.ReadUnmanagedArray<string>(ServiceInfo.OutputLanguages, (int)ServiceInfo.InputLanguagesCount);
            InputScripts = UtilityMethods.ReadUnmanagedArray<string>(ServiceInfo.InputScripts, (int)ServiceInfo.InputScriptsCounts);
            OutputScripts = UtilityMethods.ReadUnmanagedArray<string>(ServiceInfo.OutputScripts, (int)ServiceInfo.OutputScriptsCount);
            Guid = ServiceInfo.GUID;
            Category = ServiceInfo.Category;
            Description = ServiceInfo.Description;
            if (ServiceInfo.PrivateDataSize > 0)
            {
                PrivateData = new byte[ServiceInfo.PrivateDataSize];
                Marshal.Copy(ServiceInfo.PrivateData, PrivateData, 0, PrivateData.Length);
            }
            else
            {
                PrivateData = null;
            }
            OneToOneLanguageMapping = (ServiceInfo.IsOneToOneLanguageMapping & 1) != 0;
            HasSubservices = (ServiceInfo.HasSubservices & 1) != 0;
            ServiceInfoStructure = ServiceInfo;
        }
    }
}