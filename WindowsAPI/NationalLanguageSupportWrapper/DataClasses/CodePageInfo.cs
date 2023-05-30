using System;
using System.Linq;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni su una code page.
    /// </summary>
    public class CodePageInfo
    {
        /// <summary>
        /// Dimensione, in byte, di un carattere nella code page.
        /// </summary>
        public int CharSize { get; }

        /// <summary>
        /// Carattere predefinito usato durante la traduzione di stringhe alla code page.
        /// </summary>
        public char CodePageDefaultChar { get; }

        /// <summary>
        /// Array di serie di byte iniziali.
        /// </summary>
        public byte[] LeadBytes { get; }

        /// <summary>
        /// Carattere predefinito usato durante la traduzione di stringhe dalla code page.
        /// </summary>
        public char UnicodeDefaultChar { get; }

        /// <summary>
        /// Code page.
        /// </summary>
        public int CodePage { get; }

        /// <summary>
        /// Nome completo code page.
        /// </summary>
        public string CodePageName { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="CodePageInfo"/>.
        /// </summary>
        /// <param name="Info">Struttura <see cref="CPINFO"/> con le informazioni.</param>
        internal CodePageInfo(CPINFO Info)
        {
            CharSize = (int)Info.MaxCharSize;
            CodePageDefaultChar = BitConverter.ToChar(Info.DefaultChar, 0);
            LeadBytes = Info.LeadByte;
            UnicodeDefaultChar = Info.UnicodeDefaultChar;
            CodePage = (int)Info.CodePage;
            CodePageName = new string(Info.CodePageName.Where((character) => character != '\0').ToArray());
        }
    }
}