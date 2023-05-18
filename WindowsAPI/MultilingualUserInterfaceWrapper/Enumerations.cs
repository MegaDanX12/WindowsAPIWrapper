using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.MultilingualUserInterfaceWrapper.Native;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIEnumerations;

namespace WindowsAPI.MultilingualUserInterfaceWrapper
{
    /// <summary>
    /// Enumerazioni relative alle funzionalità MUI.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Formato della lingua.
        /// </summary>
        public enum LanguageFormat : uint
        {
            /// <summary>
            /// Identificativo della lingua.
            /// </summary>
            ID = MUIEnumerations.LanguageFormat.MUI_LANGUAGE_ID,
            /// <summary>
            /// Nome della lingua.
            /// </summary>
            Name = MUIEnumerations.LanguageFormat.MUI_LANGUAGE_NAME
        }

        /// <summary>
        /// Informazioni su un file MUI.
        /// </summary>
        [Flags]
        public enum MuiFileInfo : uint
        {
            /// <summary>
            /// Tipo di file.
            /// </summary>
            Type = MUIEnumerations.MUIFileInfo.MUI_QUERY_TYPE,
            /// <summary>
            /// Checksum del file.
            /// </summary>
            Checksum = MUIEnumerations.MUIFileInfo.MUI_QUERY_CHECKSUM,
            /// <summary>
            /// Nome della lingua.
            /// </summary>
            LanguageName = MUIEnumerations.MUIFileInfo.MUI_QUERY_LANGUAGE_NAME,
            /// <summary>
            /// Informazioni sulle risorse.
            /// </summary>
            ResourceTypes = MUIEnumerations.MUIFileInfo.MUI_QUERY_RESOURCE_TYPES
        }

        /// <summary>
        /// Tipo di file MUI.
        /// </summary>
        public enum MuiFileType
        {
            /// <summary>
            /// Il file non contiene dati di configurazione delle risorse.
            /// </summary>
            NonLanguageNeutral = MUIEnumerations.MUIFileType.MUI_FILETYPE_NOT_LANGUAGE_NEUTRAL,
            /// <summary>
            /// Il file è un file di lingua neutrale.
            /// </summary>
            LanguageNeutral = MUIEnumerations.MUIFileType.MUI_FILETYPE_LANGUAGE_NEUTRAL_MAIN,
            /// <summary>
            /// Il file è un file specifico della lingua.
            /// </summary>
            LanguageSpecific = MUIEnumerations.MUIFileType.MUI_FILETYPE_LANGUAGE_NEUTRAL_MUI
        }

        /// <summary>
        /// Filtro di ricerca per file MUI.
        /// </summary>
        public enum MuiSearchFilter : uint
        {
            /// <summary>
            /// Recupera tutti i file senza considerare la licenza.
            /// </summary>
            AllLanguages = SearchFilter.MUI_USE_SEARCH_ALL_LANGUAGES,
            /// <summary>
            /// Recupera solo i file che implementano lingue nella lista di fallback.
            /// </summary>
            UserPreferredUILanguages = SearchFilter.MUI_USER_PREFERRED_UI_LANGUAGES,
            /// <summary>
            /// Recupera solo i file per lingue installate nel computer.
            /// </summary>
            InstalledLanguages = SearchFilter.MUI_USE_INSTALLED_LANGUAGES
        }

        /// <summary>
        /// Opzioni di verifica file MUI.
        /// </summary>
        public enum MuiPEFileType : uint
        {
            /// <summary>
            /// Il file non viene verificato e viene aggiunta l'estensione .mui al nome del file.
            /// </summary>
            NeutralFile = MUIPEFileType.MUI_LANG_NEUTRAL_PE_FILE,
            /// <summary>
            /// Il file non viene verificato e non viene aggiunta l'estensione .mui al nome del file.
            /// </summary>
            NonNeutralFile = MUIPEFileType.MUI_NON_LANG_NEUTRAL_FILE
        }

        /// <summary>
        /// Livello di localizzazione di una lingua.
        /// </summary>
        public enum LanguageLocalizationLevel
        {
            /// <summary>
            /// La lingua è completamente localizzata.
            /// </summary>
            FullyLocalized = LanguageAttributes.MUI_FULL_LANGUAGE,
            /// <summary>
            /// La lingua è parzialmente localizzata.
            /// </summary>
            PartiallyLocalized = LanguageAttributes.MUI_PARTIAL_LANGUAGE,
            /// <summary>
            /// La lingua è parte di un Language Interface Pack.
            /// </summary>
            LIPLanguage = LanguageAttributes.MUI_LIP_LANGUAGE
        }

        /// <summary>
        /// Filtro per l'impostazione delle lingue preferite di un thread.
        /// </summary>
        public enum ThreadLanguageFilterSet
        {
            /// <summary>
            /// Tutte le lingue che hanno script complessi dovrebbero essere sostituite con le appropriate lingue di fallback.
            /// </summary>
            ReplaceComplexScriptsLanguages = ThreadLanguageSetFilter.MUI_COMPLEX_SCRIPT_FILTER,
            /// <summary>
            /// Tutte le lingue che non possono essere visualizzate correttamente in una finestra della console, in base alle attuali impostazioni del sistema, devono essre sostituite con le appropriate lingue di fallback.
            /// </summary>
            ReplaceConsoleIncompatibleLanguages = ThreadLanguageSetFilter.MUI_CONSOLE_FILTER,
            /// <summary>
            /// Resetta i filtri.
            /// </summary>
            ResetFilters = ThreadLanguageSetFilter.MUI_RESET_FILTERS
        }
    }
}