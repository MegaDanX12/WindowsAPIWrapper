using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni su una località.
    /// </summary>
    public class LocaleInfo
    {
        /// <summary>
        /// Nome località.
        /// </summary>
        public string LocaleName { get; }

        /// <summary>
        /// Indica se la località usa regole di ordinamento alternativo.
        /// </summary>
        public bool UsesAlternateSortRules { get; }
        
        /// <summary>
        /// Indica se la località contiene dati neutrali (solo lingua).
        /// </summary>
        public bool ContainsNeutralLocaleData { get; }

        /// <summary>
        /// Indica se la località ne sostituisce una pre-esistente.
        /// </summary>
        public bool IsReplacementLocale { get; }

        /// <summary>
        /// Indica se è una località aggiuntiva.
        /// </summary>
        public bool IsSupplementalLocale { get; }

        /// <summary>
        /// Indica se la località è inclusa in Windows.
        /// </summary>
        public bool IsIncludedInWindows { get; }

        /// <summary>
        /// Indica se la località contiene dati specifici (lingua e paese/regione).
        /// </summary>
        public bool ContainsSpecificLocaleData { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="LocaleInfo"/>.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="LocaleData">Valore contenente i dati sulla località.</param>
        internal LocaleInfo(string LocaleName, uint LocaleData)
        {
            this.LocaleName = LocaleName;
            if ((LocaleData & LOCALE_ALTERNATE_SORTS) != 0)
            {
                UsesAlternateSortRules = true;
            }
            if ((LocaleData & LOCALE_NEUTRALDATA) != 0)
            {
                ContainsNeutralLocaleData = true;
            }
            if ((LocaleData & LOCALE_REPLACEMENT) != 0)
            {
                IsReplacementLocale = true;
            }
            if ((LocaleData & LOCALE_SUPPLEMENTAL) != 0)
            {
                IsSupplementalLocale = true;
            }
            if ((LocaleData & LOCALE_WINDOWS) != 0)
            {
                IsIncludedInWindows = true;
            }
            if ((LocaleData & LOCALE_SPECIFICDATA) != 0)
            {
                ContainsSpecificLocaleData = true;
            }
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="LocaleInfo"/>.
        /// </summary>
        /// <param name="UsesAlternateSortRules">Indica se la località usa regole di ordinamento alternativo.</param>
        /// <param name="ContainsNeutralLocaleData">Indica se la località contiene dati neutrali (solo lingua).</param>
        /// <param name="IsSupplementalLocale">Indica se è una località aggiuntiva.</param>
        /// <param name="IsIncludedInWindows">Indica se la località è inclusa in Windows.</param>
        public LocaleInfo(bool UsesAlternateSortRules, bool ContainsNeutralLocaleData, bool IsSupplementalLocale, bool IsIncludedInWindows)
        {
            this.UsesAlternateSortRules = UsesAlternateSortRules;
            this.ContainsNeutralLocaleData = ContainsNeutralLocaleData;
            this.IsSupplementalLocale = IsSupplementalLocale;
            this.IsIncludedInWindows = IsIncludedInWindows;
        }

        /// <summary>
        /// Crea un valore composito in base ai dati di questa istanza.
        /// </summary>
        /// <returns>Il valore composito.</returns>
        internal uint ToCompositedValue()
        {
            uint Value = 0;
            if (UsesAlternateSortRules)
            {
                Value |= LOCALE_ALTERNATE_SORTS;
            }
            if (ContainsNeutralLocaleData)
            {
                Value |= LOCALE_NEUTRALDATA;
            }
            if (IsReplacementLocale) 
            {
                Value |= LOCALE_REPLACEMENT;
            }
            if (IsSupplementalLocale)
            {
                Value |= LOCALE_SUPPLEMENTAL;
            }
            if (IsIncludedInWindows)
            {
                Value |= LOCALE_WINDOWS;
            }
            if (ContainsSpecificLocaleData)
            {
                Value |= LOCALE_SPECIFICDATA;
            }
            return Value;
        }
    }
}