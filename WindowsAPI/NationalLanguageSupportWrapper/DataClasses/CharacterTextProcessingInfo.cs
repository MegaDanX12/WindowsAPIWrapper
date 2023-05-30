using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni di elaborazione del testo per un carattere.
    /// </summary>
    public class CharacterTextProcessingInfo
    {
        /// <summary>
        /// Carattere.
        /// </summary>
        public char Character { get; }

        /// <summary>
        /// Indica se il carattere è un segno senza spaziatura.
        /// </summary>
        public bool IsNonspacingMark { get; }

        /// <summary>
        /// Indica se il carattere è un segno diacritico senza spaziatura.
        /// </summary>
        public bool IsDiacriticNonspacingMark { get; }

        /// <summary>
        /// Indica se il carattere è un segno di una vocale senza spaziatura.
        /// </summary>
        public bool IsVowelNonspacingMark { get; }

        /// <summary>
        /// Indica se il carattere è un simbolo.
        /// </summary>
        public bool IsSymbol { get; }

        /// <summary>
        /// Indica se il carattere è parte dell'alfabeto Katakana.
        /// </summary>
        public bool IsKatakanaChar { get; }

        /// <summary>
        /// Indica se il carattere è parte dell'alfabeto Hiragana.
        /// </summary>
        public bool IsHiraganaChar { get; }

        /// <summary>
        /// Indica se il carattere è half-width.
        /// </summary>
        public bool IsNarrowChar { get; }

        /// <summary>
        /// Indica se il carattere è full-width.
        /// </summary>
        public bool IsWideChar { get; }

        /// <summary>
        /// Indica se il carattere è un ideogramma.
        /// </summary>
        public bool IsIdeographicChar { get; }

        /// <summary>
        /// Indica se il carattere è un kashida arabo.
        /// </summary>
        public bool IsArabicKashidaChar { get; }

        /// <summary>
        /// Indica se il carattere è punteggiatura considerata come parte della parola.
        /// </summary>
        public bool IsPunctuationAsPartOfWord { get; }

        /// <summary>
        /// Indica se il carattere è linguistico.
        /// </summary>
        public bool IsLinguisticChar { get; }

        /// <summary>
        /// Indica se il carattere è un'unità di codice high surrogate.
        /// </summary>
        public bool IsHighSurrogate { get; }

        /// <summary>
        /// Indica se il carattere è un'unità di codice low surrogate.
        /// </summary>
        public bool IsLowSurrogate { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="CharacterTextProcessingInfo"/>.
        /// </summary>
        /// <param name="Character">Carattere.</param>
        /// <param name="Info">Valore dell'enumerazione <see cref="TextProcessing"/> con le informazioni sul carattere.</param>
        internal CharacterTextProcessingInfo(char Character, TextProcessing Info)
        {
            this.Character = Character;
            if (Info.HasFlag(TextProcessing.C3_NONSPACING))
            {
                IsNonspacingMark = true;
            }
            if (Info.HasFlag(TextProcessing.C3_DIACRITIC))
            {
                IsDiacriticNonspacingMark = true;
            }
            if (Info.HasFlag(TextProcessing.C3_VOWELMARK))
            {
                IsVowelNonspacingMark = true;
            }
            if (Info.HasFlag(TextProcessing.C3_SYMBOL))
            {
                IsSymbol = true;
            }
            if (Info.HasFlag(TextProcessing.C3_KATAKANA))
            {
                IsKatakanaChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_HIRAGANA))
            {
                IsHiraganaChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_HALFWIDTH))
            {
                IsNarrowChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_FULLWIDTH))
            {
                IsWideChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_IDEOGRAPH))
            {
                IsIdeographicChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_KASHIDA))
            {
                IsArabicKashidaChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_LEXICAL))
            {
                IsPunctuationAsPartOfWord = true;
            }
            if (Info.HasFlag(TextProcessing.C3_ALPHA))
            {
                IsLinguisticChar = true;
            }
            if (Info.HasFlag(TextProcessing.C3_HIGHSURROGATE))
            {
                IsHighSurrogate = true;
            }
            if (Info.HasFlag(TextProcessing.C3_LOWSURROGATE))
            {
                IsLowSurrogate = true;
            }
        }
    }
}