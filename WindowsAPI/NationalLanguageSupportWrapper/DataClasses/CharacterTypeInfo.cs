using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sul tipo di carattere
    /// </summary>
    public class CharacterTypeInfo
    {
        /// <summary>
        /// Carattere.
        /// </summary>
        public char Character { get; }

        /// <summary>
        /// Indica se il carattere è maiuscolo.
        /// </summary>
        public bool IsUppercase { get; }

        /// <summary>
        /// Indica se il carattere è minuscolo.
        /// </summary>
        public bool IsLowercase { get; }

        /// <summary>
        /// Indica se il carattere è un numero decimale.
        /// </summary>
        public bool IsDecimalDigit { get; }

        /// <summary>
        /// Indica se il carattere è uno spazio.
        /// </summary>
        public bool IsSpace { get; }

        /// <summary>
        /// Indica se il carattere è punteggiatura.
        /// </summary>
        public bool IsPunctuation { get; }

        /// <summary>
        /// Indica se il carattere è un carattere di controllo.
        /// </summary>
        public bool IsControlChar { get; }

        /// <summary>
        /// Indica se il carattere è vuoto.
        /// </summary>
        public bool IsBlankChar { get; }

        /// <summary>
        /// Indica se il carattere è un numero esadecimale.
        /// </summary>
        public bool IsHexadecimalDigit { get; }

        /// <summary>
        /// Indica se il carattere è linguistico (alfabetico, sillabario, ideografico).
        /// </summary>
        public bool IsLinguisticChar { get; }

        /// <summary>
        /// Indica se il carattere è definito.
        /// </summary>
        public bool IsDefined { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="CharacterTypeInfo"/>.
        /// </summary>
        /// <param name="Character">Carattere.</param>
        /// <param name="Info">Valore che contiene le informazioni di tipo.</param>
        internal CharacterTypeInfo(char Character, CharacterTypes Info)
        {
            this.Character = Character;
            if (Info.HasFlag(CharacterTypes.C1_UPPER))
            {
                IsUppercase = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_LOWER))
            {
                IsLowercase = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_DIGIT))
            {
                IsDecimalDigit = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_SPACE))
            {
                IsSpace = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_PUNCT))
            {
                IsPunctuation = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_CNTRL))
            {
                IsControlChar = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_BLANK))
            {
                IsBlankChar = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_XDIGIT))
            {
                IsHexadecimalDigit = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_ALPHA))
            {
                IsLinguisticChar = true;
            }
            if (Info.HasFlag(CharacterTypes.C1_DEFINED))
            {
                IsDefined = true;
            }
        }
    }
}