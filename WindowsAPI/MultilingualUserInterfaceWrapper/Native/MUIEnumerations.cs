using System;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIStructures;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.Native
{
    /// <summary>
    /// Enumerazioni relative alle funzionalità MUI.
    /// </summary>
    internal static class MUIEnumerations
    {
        /// <summary>
        /// Formato della lingua.
        /// </summary>
        internal enum LanguageFormat : uint
        {
            /// <summary>
            /// Restituisce l'ID lingua.
            /// </summary>
            MUI_LANGUAGE_ID = 4,
            /// <summary>
            /// Restituisce il nome della lingua.
            /// </summary>
            MUI_LANGUAGE_NAME = 8,
        }

        /// <summary>
        /// Tipo di file MUI.
        /// </summary>
        internal enum MUIFileType
        {
            /// <summary>
            /// Il file non contiene dati di configurazione risorse.
            /// </summary>
            MUI_FILETYPE_NOT_LANGUAGE_NEUTRAL = 1,
            /// <summary>
            /// Il file è un file di lingua neutrale.
            /// </summary>
            MUI_FILETYPE_LANGUAGE_NEUTRAL_MAIN,
            /// <summary>
            /// Il file è un file di lingua specifico.
            /// </summary>
            MUI_FILETYPE_LANGUAGE_NEUTRAL_MUI = 4
        }

        /// <summary>
        /// Informazione da recuperare da un file MUI.
        /// </summary>
        [Flags]
        internal enum MUIFileInfo
        {
            /// <summary>
            /// Tipo di file.
            /// </summary>
            /// <remarks>L'informazione viene recuperata nel campo <see cref="FILEMUIINFO.FileType"/>.</remarks>
            MUI_QUERY_TYPE = 1,
            /// <summary>
            /// Checksum del file.
            /// </summary>
            /// <remarks>L'informazione viene recuperata nel campo <see cref="FILEMUIINFO.Checksum"/>.</remarks>
            MUI_QUERY_CHECKSUM,
            /// <summary>
            /// Lingua associata con il file.
            /// </summary>
            /// <remarks>L'informazione viene inserita nel buffer e la sua posizione è indicata nel campo <see cref="FILEMUIINFO.LanguageNameOffset"/>.</remarks>
            MUI_QUERY_LANGUAGE_NAME = 4,
            /// <summary>
            /// Le liste di tipi di risorse.
            /// </summary>
            /// <remarks>Se il file è specifico di una lingua i seguenti campi di <see cref="FILEMUIINFO"/> sono validi:<br/><br/>
            /// <see cref="FILEMUIINFO.TypeIDMUIOffset"/><br/>
            /// <see cref="FILEMUIINFO.TypeIDMUISize"/><br/>
            /// <see cref="FILEMUIINFO.TypeNameMUIOffset"/><br/><br/>
            /// Se il file è neutrale i seguenti campi di <see cref="FILEMUIINFO"/> sono validi:<br/><br/>
            /// <see cref="FILEMUIINFO.TypeIDMainOffset"/><br/>
            /// <see cref="FILEMUIINFO.TypeIDMainSize"/><br/>
            /// <see cref="FILEMUIINFO.TypeNameMainOffset"/></remarks>
            MUI_QUERY_RESOURCE_TYPES = 8
        }

        /// <summary>
        /// Filtro ricerca.
        /// </summary>
        internal enum SearchFilter : uint
        {
            /// <summary>
            /// Tutti i file di risorse senza considerare la licenza.
            /// </summary>
            MUI_USE_SEARCH_ALL_LANGUAGES = 64,
            /// <summary>
            /// Solo file che implementano lingue nella lista di fallback.
            /// </summary>
            MUI_USER_PREFERRED_UI_LANGUAGES = 16,
            /// <summary>
            /// Solo i file per le lingue installate nel computer.
            /// </summary>
            MUI_USE_INSTALLED_LANGUAGES = 32
        }

        /// <summary>
        /// Tipo di file MUI.
        /// </summary>
        internal enum MUIPEFileType : uint
        {
            /// <summary>
            /// Non verifica il file e aggiunge l'estensione .mui al nome prima dell'elaborazione.
            /// </summary>
            MUI_LANG_NEUTRAL_PE_FILE = 256,
            /// <summary>
            /// Non verifica il file e non aggiugne l'estensione .mui al nome prima dell'elaborazione.
            /// </summary>
            MUI_NON_LANG_NEUTRAL_FILE = 512
        }

        /// <summary>
        /// Opzioni di filtraggio per le lingue del thread.
        /// </summary>
        [Flags]
        internal enum ThreadLanguagesFilter : uint
        {
            /// <summary>
            /// Usa il fallback di sistema per recuperare una lista che corrisponde esattamente a quella usata dal caricatore di risorse.
            /// </summary>
            /// <remarks>Questo valore può essere usato insieme a <see cref="MUI_MERGE_USER_FALLBACK"/>, in questo caso l'effetto di questo valore viene alterato per includere anche lingue di fallback e neutrali nella lista.</remarks>
            MUI_MERGE_SYSTEM_FALLBACK = 16,
            /// <summary>
            /// Recupera una lista composita che consiste delle lingue preferite dal thread seguite da quelle preferite dal processo a loro volta seguite dalle lingue preferite dall'utente, se non già incluse, e infine seguite dalla lingue predefinita dell'interfaccia utente del sistema se non è già presente.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MUI_THREAD_LANGUAGES"/>.</remarks>
            MUI_MERGE_USER_FALLBACK = 32,
            /// <summary>
            /// Recupera solo le lingue preferite per il thread corrente o una lista vuota se nessuna lingua è impostata.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MUI_MERGE_USER_FALLBACK"/> e <see cref="MUI_MERGE_SYSTEM_FALLBACK"/>.</remarks>
            MUI_THREAD_LANGUAGES = 64,
            /// <summary>
            /// Recupera una lista completa di lingue preferite del thread insieme alle lingue di fallback e neutrali associate.
            /// </summary>
            MUI_UI_FALLBACK = 
                MUI_MERGE_SYSTEM_FALLBACK |
                MUI_MERGE_USER_FALLBACK
        }

        /// <summary>
        /// Attributi di una lingua.
        /// </summary>
        [Flags]
        internal enum LanguageAttributes
        {
            /// <summary>
            /// Lingua completamente localizzata.
            /// </summary>
            MUI_FULL_LANGUAGE = 1,
            /// <summary>
            /// Lingua parzialmente localizzata.
            /// </summary>
            MUI_PARTIAL_LANGUAGE,
            /// <summary>
            /// Lingua implementata da un Language Interface Pack.
            /// </summary>
            MUI_LIP_LANGUAGE = 4,
            /// <summary>
            /// Lingua installata nel computer.
            /// </summary>
            MUI_LANGUAGE_INSTALLED = 32,
            /// <summary>
            /// Lingua concessa in licenza all'utente corrente.
            /// </summary>
            MUI_LANGUAGE_LICENSED = 64
        }

        /// <summary>
        /// Filtro per l'impostazione delle lingue preferite di un thread.
        /// </summary>
        internal enum ThreadLanguageSetFilter
        {
            /// <summary>
            /// Tutte le lingue che hanno script complessi dovrebbero essere sostituite con le appropriate lingue di fallback.
            /// </summary>
            MUI_COMPLEX_SCRIPT_FILTER = 512,
            /// <summary>
            /// Tutte le lingue che non possono essere visualizzate correttamente in una finestra della console, in base alle attuali impostazioni del sistema, devono essre sostituite con le appropriate lingue di fallback.
            /// </summary>
            MUI_CONSOLE_FILTER = 256,
            /// <summary>
            /// Resetta i filtri.
            /// </summary>
            MUI_RESET_FILTERS = 1
        }
    }
}