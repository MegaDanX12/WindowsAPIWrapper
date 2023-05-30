using System;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni formato valuta.
    /// </summary>
    public class CurrencyFormat
    {
        /// <summary>
        /// Numero di cifre decimali.
        /// </summary>
        public int FractionalDigitsCount { get; }

        /// <summary>
        /// Indica se esistono degli zeri iniziali nei campi decimali.
        /// </summary>
        public bool UseLeadingZeroes { get; }

        /// <summary>
        /// Raggruppamento cifre decimali.
        /// </summary>
        /// <remarks>La prima cifra indica il numero di cifre nel gruppo immediatamente a sinistra del separatore decimale, le cifre seguenti indicano il gruppo alla sinistra del gruppo precedente.<br/>
        /// Se l'ultima cifra non è 0, i gruppi rimanenti sono uguali all'ultimo indicato.</remarks>
        public int Grouping { get; }

        /// <summary>
        /// Separatore decimale.
        /// </summary>
        public string DecimalSeparator { get; }

        /// <summary>
        /// Separatore delle migliaia.
        /// </summary>
        public string ThousandSeparator { get; }

        /// <summary>
        /// Formato dei valori monetari negativi.
        /// </summary>
        public Enumerations.NegativeCurrencyFormat NegativeCurrencyFormat { get; }

        /// <summary>
        /// Formato dei valori monetari positivi.
        /// </summary>
        public Enumerations.PositiveCurrencySymbolPosition PositiveCurrencyFormat { get; }

        /// <summary>
        /// Simbolo della valuta.
        /// </summary>
        public string CurrencySymbol { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="CurrencyFormat"/>.
        /// </summary>
        /// <param name="fractionalDigitsCount">Numero di cifre decimali.</param>
        /// <param name="useLeadingZeroes">Indica se esistono degli zeri iniziali nei campi decimali.</param>
        /// <param name="grouping">Raggruppamento cifre decimali.</param>
        /// <param name="decimalSeparator">Separatore decimale.</param>
        /// <param name="thousandSeparator">Separatore delle migliaia.</param>
        /// <param name="negativeCurrencyFormat">Formato dei valori monetari negativi.</param>
        /// <param name="positiveCurrencyFormat">Formato dei valori monetari positivi.</param>
        /// <param name="currencySymbol">Simbolo della valuta.</param>
        public CurrencyFormat(int fractionalDigitsCount, bool useLeadingZeroes, int grouping, string decimalSeparator, string thousandSeparator, Enumerations.NegativeCurrencyFormat negativeCurrencyFormat, Enumerations.PositiveCurrencySymbolPosition positiveCurrencyFormat, string currencySymbol)
        {
            FractionalDigitsCount = fractionalDigitsCount;
            UseLeadingZeroes = useLeadingZeroes;
            Grouping = grouping;
            DecimalSeparator = decimalSeparator;
            ThousandSeparator = thousandSeparator;
            NegativeCurrencyFormat = negativeCurrencyFormat;
            PositiveCurrencyFormat = positiveCurrencyFormat;
            CurrencySymbol = currencySymbol;
        }

        /// <summary>
        /// Converte i dati di questa istanza di <see cref="CURRENCYFMT"/>.
        /// </summary>
        /// <returns>Struttura <see cref="CURRENCYFMT"/> risultato della conversione.</returns>
        internal CURRENCYFMT ToStructure()
        {
            CURRENCYFMT Structure = new CURRENCYFMT()
            {
                FractionDigitsCount = (uint)FractionalDigitsCount,
                LeadingZero = Convert.ToUInt32(UseLeadingZeroes),
                Grouping = (uint)Grouping,
                DecimalSeparator = DecimalSeparator,
                ThousandSeparator = ThousandSeparator,
                NegativeOrder = (uint)NegativeCurrencyFormat,
                PositiveOrder = (uint)PositiveCurrencyFormat,
                CurrencySymbol = CurrencySymbol
            };
            return Structure;
        }
    }
}