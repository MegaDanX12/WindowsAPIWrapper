using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WindowsAPI.MultilingualUserInterfaceWrapper.Native;
using WindowsAPI.SafeHandles;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Enumerations;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIEnumerations;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.DataClasses
{
    /// <summary>
    /// Informazioni su una lingua dell'interfaccia utente.
    /// </summary>
    public class UILanguageInfo
    {
        /// <summary>
        /// Lingua.
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// Livello di localizzazione.
        /// </summary>
        public LanguageLocalizationLevel LocalizationLevel { get; }

        /// <summary>
        /// Indica se la lingua è installata.
        /// </summary>
        public bool IsInstalled { get; }

        /// <summary>
        /// Indica se la lingua è concessa in licenza all'utente.
        /// </summary>
        public bool IsLicensed { get; }

        /// <summary>
        /// Lingue di fallback.
        /// </summary>
        public string[] FallbackLanguages { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="UILanguageInfo"/>.
        /// </summary>
        /// <param name="Language">Lingua.</param>
        /// <param name="Attributes">Attributi della lingua.</param>
        /// <param name="FallbackLanguagesPointer">Puntatore all'inizio della multistringa che contiene le lingue di fallback.</param>
        internal UILanguageInfo(string Language, LanguageAttributes Attributes, SafeMultistringPointer FallbackLanguagesPointer)
        {
            this.Language = Language;
            IsInstalled = Attributes.HasFlag(LanguageAttributes.MUI_LANGUAGE_INSTALLED);
            IsLicensed = Attributes.HasFlag(LanguageAttributes.MUI_LANGUAGE_LICENSED);
            if (Attributes.HasFlag(LanguageAttributes.MUI_FULL_LANGUAGE))
            {
                LocalizationLevel = LanguageLocalizationLevel.FullyLocalized;
            }
            if (Attributes.HasFlag(LanguageAttributes.MUI_PARTIAL_LANGUAGE))
            {
                LocalizationLevel = LanguageLocalizationLevel.PartiallyLocalized;
            }
            if (Attributes.HasFlag(LanguageAttributes.MUI_LIP_LANGUAGE))
            {
                LocalizationLevel = LanguageLocalizationLevel.LIPLanguage;
            }
            FallbackLanguages = FallbackLanguagesPointer.ReadStringsFromMemory();
        }
    }
}