using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSCallbacks;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.Native
{
    /// <summary>
    /// Strutture ELS.
    /// </summary>
    internal static class ELSStructures
    {
        /// <summary>
        /// Opzioni di enumerazione dei servizi ELS.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MAPPING_ENUM_OPTIONS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public IntPtr Size;
            /// <summary>
            /// Categoria del servizio.
            /// </summary>
            /// <remarks>Questo campo è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Category;
            /// <summary>
            /// Lingua di input.
            /// </summary>
            /// <remarks>Questo campo deve seguire le convenzioni IETF, è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string InputLanguage;
            /// <summary>
            /// Lingua di output.
            /// </summary>
            /// <remarks>Questo campo deve seguie le convenzioni IETF, è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string OutputLanguage;
            /// <summary>
            /// Nome standard script Unicode accettabile dai servizi.
            /// </summary>
            /// <remarks>Questo campo è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string InputScript;
            /// <summary>
            /// Nome standard script Unicode usato dai servizi.
            /// </summary>
            /// <remarks>Questo campo è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string OutputScript;
            /// <summary>
            /// Tipo di contenuto MIME che identifica il formato interpretabile dal servizio.
            /// </summary>
            /// <remarks>Questo campo è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string InputContentType;
            /// <summary>
            /// Tipo di contenuto MIME che identifica il formato dei dati del servizio recuperati.
            /// </summary>
            /// <remarks>Questo campo è opzionale.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string OutputContentType;
            /// <summary>
            /// Puntatore a una struttura <see cref="General.Native.GeneralStructures.GUID"/> per un servizio specifico.
            /// </summary>
            /// <remarks>Questo campo è opzionale.</remarks>
            public IntPtr GUID;
            /// <summary>
            /// Riservato, deve essere 0.
            /// </summary>
            private int OnlineService;
            /// <summary>
            /// Riservato, deve essere 0.
            /// </summary>
            private int ServiceType;
        }

        /// <summary>
        /// Informazioni su un servizio ELS.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MAPPING_SERVICE_INFO
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public IntPtr Size;
            /// <summary>
            /// Informazioni di copyright del servizio.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Copyright;
            /// <summary>
            /// Numero di versione maggiore.
            /// </summary>
            public ushort MajorVersion;
            /// <summary>
            /// Numero di versione minore.
            /// </summary>
            public ushort MinorVersion;
            /// <summary>
            /// Numero di build.
            /// </summary>
            public ushort BuildVersion;
            /// <summary>
            /// Numero di step.
            /// </summary>
            public ushort StepVersion;
            /// <summary>
            /// Numero di tipi di contenuti MIME che il servizio può ricevere.
            /// </summary>
            public uint InputContentTypesCount;
            /// <summary>
            /// Puntatore a un array di tipi di contenuti MIME supportati dal servizio.
            /// </summary>
            public IntPtr InputContentTypes;
            /// <summary>
            /// Numero di tipi di contenuti MIME nei quali il servizio può formattare i risultati.
            /// </summary>
            public uint OutputContentTypesCount;
            /// <summary>
            /// Puntatore a un array di tipi di contenuti MIME nei quali il servizio può formattare i risultati.
            /// </summary>
            public IntPtr OutputContentTypes;
            /// <summary>
            /// Numero di lingue di input supportate dal servizio.
            /// </summary>
            /// <remarks>Questo campo ha valore 0 se il servizio accetta dati in tutte le lingue.</remarks>
            public uint InputLanguagesCount;
            /// <summary>
            /// Puntatore a un array di lingue di input accettate dal servizio.
            /// </summary>
            /// <remarks>I nomi delle lingue di input seguono le convenzioni IETF, questo campo è nullo se sono supportate tutte le lingue.</remarks>
            public IntPtr InputLanguages;
            /// <summary>
            /// Numero di lingue di output supportate dal servizio.
            /// </summary>
            /// <remarks>Questo campo ha valore 0 se il servizio può recuperare dati in tutte le lingue o se ignora la lingua di output.</remarks>
            public uint OutputLanguagesCount;
            /// <summary>
            /// Puntatore a un array di lingue di output supportate dal servizio.
            /// </summary>
            /// <remarks>I nomi delle lingua di output seguono le convezioni IETF, questo campo è nullo se il servizio supporta tutte le lingue o se ignora la lingua di output.</remarks>
            public IntPtr OutputLanguages;
            /// <summary>
            /// Numero di script supportato in input dal servizio.
            /// </summary>
            /// <remarks>Questo campo è impostato a 0 se il servizio accetta dati in qualunque script.</remarks>
            public uint InputScriptsCounts;
            /// <summary>
            /// Puntatore a un array di script di input supportati dal servizio.
            /// </summary>
            /// <remarks>I nomi degli script seguono lo standard Unicode, questo campo è nullo se il servizio supporta tutti gli script o se li ignora.</remarks>
            public IntPtr InputScripts;
            /// <summary>
            /// Numero di script supportato in output dal servizio.
            /// </summary>
            /// <remarks>Questo campo è impostato a 0 se il servizio recupera da in qualunque script o se gli ignora.</remarks>
            public uint OutputScriptsCount;
            /// <summary>
            /// Puntatore a un array di script di output supportati dal servizio.
            /// </summary>
            /// <remarks>I nomi degli script seguono lo standard Unicode, questo campo è nullo se il servizio supporta tutti gli script o se li ignora.</remarks>
            public IntPtr OutputScripts;
            /// <summary>
            /// Identificatore univoco globale del servizio.
            /// </summary>
            public Guid GUID;
            /// <summary>
            /// Categoria a cui appartiene il servizio.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Category;
            /// <summary>
            /// Descrizione del servizio.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Description;
            /// <summary>
            /// Dimensione, in byte, dei dati privati del servizio.
            /// </summary>
            /// <remarks>Questo campo è impostato a 0 se non ci sono dati privati.</remarks>
            public uint PrivateDataSize;
            /// <summary>
            /// Puntatore ai dati privati del servizio.
            /// </summary>
            public IntPtr PrivateData;
            /// <summary>
            /// Riservato per uso interno.
            /// </summary>
            private IntPtr Context;
            /// <summary>
            /// Indica il tipo di mappatura tra la lingua di input e quella di output supportata dal servizio.
            /// </summary>
            /// <remarks>Questo campo può avere i seguenti valori:<br/><br/>
            /// 0: non c'è nessun collegamento tra lingua di input e output<br/>
            /// 1: la lingua di input è collegata a una specifica lingua di output<br/><br/>
            /// Solo il primo bit di questo valore ha significato.</remarks>
            public int IsOneToOneLanguageMapping;
            /// <summary>
            /// Indica se il servizio ha altri servizi che da esso dipendono.
            /// </summary>
            /// <remarks>Questo campo può avere i seguenti valori:<br/><br/>
            /// 0: il servizio è stand alone<br/>
            /// 1: il servizio agisce come padre per altri servizi<br/><br/>
            /// Solo il primo bit di questo valore ha significato.</remarks>
            public int HasSubservices;
            /// <summary>
            /// Riservato per uso futuro.
            /// </summary>
            private int OnlineOnly;
            /// <summary>
            /// Riservato per uso futuro.
            /// </summary>
            private int ServiceType;
        }

        /// <summary>
        /// Opzioni di riconoscimento del testo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MAPPING_OPTIONS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public IntPtr Size;
            /// <summary>
            /// Lingua di input accettata dal servizio.
            /// </summary>
            /// <remarks>Il nome segue il formato IETF, può essere nullo se il servizio è libero di interpretare l'input come una qualunque delle lingue supportate.</remarks>
            [MarshalAs (UnmanagedType.LPWStr)]
            public string InputLanguage;
            /// <summary>
            /// Lingua di output con la quale il servizio produce risultati.
            /// </summary>
            /// <remarks>Il nome segue il formato IETF, può essere nullo se il servizio dovrebbe decidere la lingua di output.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string OutputLanguage;
            /// <summary>
            /// Nome script Unicode di input che il servizio dovrebbe accettare.
            /// </summary>
            /// <remarks>Questo campo può essere nullo se si vuole lasciare decidere al servizio il modo in cui gestire l'input.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string InputScript;
            /// <summary>
            /// Nome script Unicode che il servizio dovrebbe usare per recuperare risultati.
            /// </summary>
            /// <remarks>Questo campo può essere nullo se si vuole lasciare decidere al servizio lo script di output.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string OutputScript;
            /// <summary>
            /// Tipo di contenuto MIME supportato dal servizio.
            /// </summary>
            /// <remarks>Questo campo può essere nullo, in questo caso il tipo di contenuto indicato è "text/plain".</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string InputContentType;
            /// <summary>
            /// Tipo di contenuto MIME restituito dal servizio.
            /// </summary>
            /// <remarks>Questo campo può essere nullo, in questo caso il servizio decide il tipo di contenuto di output.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string OutputContentType;
            /// <summary>
            /// Riservato.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            private string UILanguage;
            /// <summary>
            /// Funzione che riceve callbacks con i risultati alla chiamata di <see cref="ELSFunctions.RecognizeText"/>.
            /// </summary>
            /// <remarks>Se viene specificato un callback, il riconoscimento del testo viene eseguito in modalità asincrona e l'applicazione riceve i risultati tramite la funzione.<br/>
            /// Questo campo deve essere nullo se il riconoscimento del testo deve essere sincrono.</remarks>
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public MappingCallback RecognizeCallback;
            /// <summary>
            /// Puntatore ai dati privati dell'applicazione passati alla funzione di callback dal servizio dopo il riconoscimento del testo.
            /// </summary>
            /// <remarks>Questo campo deve essere nullo se non ci sono dati privati.</remarks>
            public IntPtr RecognizeCallerData;
            /// <summary>
            /// Dimensione, in byte, dei dati privati dell'applicazione.
            /// </summary>
            public uint RecognizeCallerDataSize;
            /// <summary>
            /// Risevato.
            /// </summary>
            [MarshalAs(UnmanagedType.FunctionPtr)]
            private MappingCallback ActionCallback;
            /// <summary>
            /// Riservato.
            /// </summary>
            private IntPtr ActionCallerData;
            /// <summary>
            /// Riservato.
            /// </summary>
            private uint ActionCallerDataSize;
            /// <summary>
            /// Flag che influenza il comportamento del servizio.
            /// </summary>
            /// <remarks>I servizi possono interpretare questo campo come vogliono.</remarks>
            public uint ServiceFlag;
            /// <summary>
            /// Riservato.
            /// </summary>
            private int GetActionDisplayName;
        }

        /// <summary>
        /// Proprietà del testo riconosciuto.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MAPPING_PROPERTY_BAG
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public IntPtr Size;
            /// <summary>
            /// Puntatore a un array di strutture <see cref="MAPPING_DATA_RANGE"/> contenenti tutte le sezioni riconosciute di testo.
            /// </summary>
            public IntPtr ResultRanges;
            /// <summary>
            /// Numero di sezioni riconosciute di testo.
            /// </summary>
            public uint RangesCount;
            /// <summary>
            /// Puntatore ai dati privati del servizio.
            /// </summary>
            public IntPtr ServiceData;
            /// <summary>
            /// Dimensione, in byte, dei dati privati del servizio.
            /// </summary>
            /// <remarks>Questo campo ha valore 0 se non ci sono dati privati.</remarks>
            public uint ServiceDataSize;
            /// <summary>
            /// Puntatore ai dati privati dell'applicazione da passare al servizio.
            /// </summary>
            public IntPtr CallerData;
            /// <summary>
            /// Dimensione, in byte, dei dati privati dell'applicazione.
            /// </summary>
            /// <remarks>Questo campo ha valore 0 se non ci sono dati privati.</remarks>
            public uint CallerDataSize;
            /// <summary>
            /// Riservato per uso interno.
            /// </summary>
            private IntPtr Context;
        }

        /// <summary>
        /// Risultati del riconoscimento di una sezione del testo.
        /// </summary>
        [StructLayout (LayoutKind.Sequential)]
        internal readonly struct MAPPING_DATA_RANGE
        {
            /// <summary>
            /// Indice dell'inizio della sezione del testo.
            /// </summary>
            /// <remarks>0 indica il carattere che si trova al puntatore passato a <see cref="ELSFunctions.RecognizeText"/>, il suo valore dovrebbe essere inferiore della lunghezza del testo.</remarks>
            public readonly uint StartIndex;
            /// <summary>
            /// Indice alla fine della sezione del testo.
            /// </summary>
            /// <remarks>0 indica il carattere che si trova al puntatore passato a <see cref="ELSFunctions.RecognizeText"/>, il suo valore dovrebbe essere inferiore della lunghezza del testo.</remarks>
            public readonly uint EndIndex;
            /// <summary>
            /// Riservato.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            private readonly string Description;
            /// <summary>
            /// Riservato.
            /// </summary>
            private readonly uint DescriptionLength;
            /// <summary>
            /// Puntatore ai dati recuperati come output del servizio associati alla sezione.
            /// </summary>
            public readonly IntPtr Data;
            /// <summary>
            /// Dimensione, in byte, dei dati.
            /// </summary>
            public readonly uint DataSize;
            /// <summary>
            /// Tipo di contenuto MIME dei dati.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public readonly string ContentType;
            /// <summary>
            /// ID delle azioni disponibili per la sezione.
            /// </summary>
            public readonly IntPtr ActionIds;
            /// <summary>
            /// Numero di azioni disponibili.
            /// </summary>
            public readonly uint ActionsCount;
            /// <summary>
            /// Nomi comuni delle azioni.
            /// </summary>
            public readonly IntPtr ActionDisplayNames;
        }
    }
}