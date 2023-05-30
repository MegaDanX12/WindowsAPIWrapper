using System;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    public class NumberFormat
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
        /// Formato dei valori negativi.
        /// </summary>
        public Enumerations.NegativeNumberFormat NegativeValueFormat { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="NumberFormat"/>.
        /// </summary>
        /// <param name="fractionalDigitsCount">Numero di cifre decimali.</param>
        /// <param name="useLeadingZeroes">Indica se esistono degli zeri iniziali nei campi decimali.</param>
        /// <param name="grouping">Raggruppamento cifre decimali.</param>
        /// <param name="decimalSeparator">Separatore decimale.</param>
        /// <param name="thousandSeparator">Separatore delle migliaia.</param>
        /// <param name="negativeNumberFormat">Formato dei valori monetari negativi.</param>
        public NumberFormat(int fractionalDigitsCount, bool useLeadingZeroes, int grouping, string decimalSeparator, string thousandSeparator, Enumerations.NegativeNumberFormat negativeNumberFormat)
        {
            FractionalDigitsCount = fractionalDigitsCount;
            UseLeadingZeroes = useLeadingZeroes;
            Grouping = grouping;
            DecimalSeparator = decimalSeparator;
            ThousandSeparator = thousandSeparator;
            NegativeValueFormat = negativeNumberFormat;
        }

        /// <summary>
        /// Converte i dati di questa istanza di <see cref="NUMBERFMT"/>.
        /// </summary>
        /// <returns>Struttura <see cref="NUMBERFMT"/> risultato della conversione.</returns>
        internal NUMBERFMT ToStructure()
        {
            NUMBERFMT Structure = new NUMBERFMT()
            {
                DigitsCount = (uint)FractionalDigitsCount,
                LeadingZero = Convert.ToUInt32(UseLeadingZeroes),
                Grouping = (uint)Grouping,
                DecimalSeparator = DecimalSeparator,
                ThousandSeparator = ThousandSeparator,
                NegativeNumberMode = (uint)NegativeValueFormat
            };
            return Structure;
        }
    }
}