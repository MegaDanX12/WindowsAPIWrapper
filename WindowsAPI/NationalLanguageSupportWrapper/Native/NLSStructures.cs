using System;
using System.Runtime.InteropServices;
using WindowsAPI.General.Native;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;

namespace WindowsAPI.NationalLanguageSupportWrapper.Native
{
    /// <summary>
    /// Strutture NLS.
    /// </summary>
    internal static class NLSStructures
    {
        /// <summary>
        /// Informazioni di versione di una funzionalità NLS.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NLSVERSIONINFO
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Versione.
            /// </summary>
            public uint Version;
            /// <summary>
            /// Deprecato.
            /// </summary>
            public uint DefinedVersion;
            /// <summary>
            /// Deprecato.
            /// </summary>
            public uint EffectiveID;
            /// <summary>
            /// GUID per il comportamento di un ordinamento personalizzato usata dalla località per la versione.
            /// </summary>
            public Guid CustomVersionGuid;
        }

        /// <summary>
        /// Informazioni su una code page.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CPINFO
        {
            /// <summary>
            /// Dimensione massima, in byte, di un carattere in una code page.
            /// </summary>
            /// <remarks>Il valore di questo campo può essere 1 per set di caratteri a singolo byte (SBCS), 2 per set di caratteri a due byte (DBCS) oppure un valore superiore a 2 per altri tipi di set di caratteri.</remarks>
            public uint MaxCharSize;
            /// <summary>
            /// Carattere predefinito usato durante la traduzione di stringhe a una code page specifica.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DEFAULTCHAR)]
            public byte[] DefaultChar;
            /// <summary>
            /// Array di serie di byte iniziali, per i quali il numero di byte iniziali è variabile.
            /// </summary>
            /// <remarks>Se la code page non ha byte iniziali, tutti gli elementi di questo array sono 0, in caso contrario, l'array specifica un valore iniziale e un valore finale per ogni serie.<br/>
            /// Il numero massimo di serie è 5 e i valori sono inclusi.<br/>
            /// Vengono usati due byte per descrivere ogni serie, l'array termina con due byte nulli dopo l'ultima serie.</remarks>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_LEADBYTES)]
            public byte[] LeadByte;
            /// <summary>
            /// Carattere predefinito Unicode usato in traduzioni da una specifica code page.
            /// </summary>
            public char UnicodeDefaultChar;
            /// <summary>
            /// Valore code page.
            /// </summary>
            public uint CodePage;
            /// <summary>
            /// Nome completo della code page.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = GeneralConstants.MAX_PATH)]
            public char[] CodePageName;
        }

        /// <summary>
        /// Informazioni sul formato di una stringa di valuta.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct CURRENCYFMT
        {
            /// <summary>
            /// Numero di cifre decimali.
            /// </summary>
            public uint FractionDigitsCount;
            /// <summary>
            /// Indica se i decimali hanno degli zeri iniziali.
            /// </summary>
            public uint LeadingZero;
            /// <summary>
            /// Numero di cifre in ogni gruppo alla sinistra del separatore decimale.
            /// </summary>
            /// <remarks>La prima cifra indica il numero di cifre nel gruppo immediatamente a sinistra del separatore decimale, le cifre seguenti indicano il gruppo alla sinistra del gruppo precedente.<br/>
            /// Se l'ultima cifra non è 0, i gruppi rimanenti sono uguali all'ultimo indicato.</remarks>
            public uint Grouping;
            /// <summary>
            /// Separatore decimale.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string DecimalSeparator;
            /// <summary>
            /// Separatore delle migliaia.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string ThousandSeparator;
            /// <summary>
            /// Formato valuta negativa.
            /// </summary>
            /// <remarks>Equivalente a <see cref="LOCALE_INEGCURR"/>.</remarks>
            public uint NegativeOrder;
            /// <summary>
            /// Formato valuta positiva.
            /// </summary>
            /// <remarks>Equivalente ai valori dell'enumerazione <see cref="PositiveCurrencyMonetarySymbolPosition"/> associato con la costante <see cref="LOCALE_ICURRENCY"/></remarks>
            public uint PositiveOrder;
            /// <summary>
            /// Simbolo di valuta.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string CurrencySymbol;
        }

        /// <summary>
        /// Informazioni estese sulla firma del font.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct LOCALESIGNATURE
        {
            /// <summary>
            /// Bitfield di 128 bit che identifica fino a 122 sottosezioni Unicode.
            /// </summary>
            /// <remarks>Ogni bit, eccetto i primi 5, rappresentano una singola sezione.<br/>
            /// Il primo bit è sempre 1, il secondo è riservato e deve essere 0.</remarks>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public uint[] UnicodeSubsetBitfield;
            /// <summary>
            /// Code pages predefinite OEM e ANSI per la località.
            /// </summary>
            /// <remarks></remarks>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] DefaultCodePages;
            /// <summary>
            /// Code pages che supportano la località.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] LocaleSupportedCodePages;
        }

        /// <summary>
        /// Informazioni sul formato di una stringa numerica.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NUMBERFMT
        {
            /// <summary>
            /// Numero di cifre decimali.
            /// </summary>
            public uint DigitsCount;
            /// <summary>
            /// Indica se i decimali hanno degli zeri iniziali.
            /// </summary>
            public uint LeadingZero;
            /// <summary>
            /// Numero di cifre in ogni gruppo alla sinistra del separatore decimale.
            /// </summary>
            /// <remarks>La prima cifra indica il numero di cifre nel gruppo immediatamente a sinistra del separatore decimale, le cifre seguenti indicano il gruppo alla sinistra del gruppo precedente.<br/>
            /// Se l'ultima cifra non è 0, i gruppi rimanenti sono uguali all'ultimo indicato.</remarks>
            public uint Grouping;
            /// <summary>
            /// Separatore decimale.
            /// </summary>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string DecimalSeparator;
            /// <summary>
            /// Separatore delle migliaia.
            /// </summary>
            [MarshalAs (UnmanagedType.LPWStr)]
            public string ThousandSeparator;
            /// <summary>
            /// Formato valuta negativa.
            /// </summary>
            /// <remarks>Equivalente a <see cref="LOCALE_INEGCURR"/>.</remarks>
            public uint NegativeNumberMode;
        }
    }
}