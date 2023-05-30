using System;
using System.Collections;
using System.Collections.Generic;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla relazione tra la copertura dei caratteri necessari a supportare una località è i contenuti di un font.
    /// </summary>
    public class LocaleSignature
    {
        /// <summary>
        /// Nome località.
        /// </summary>
        public string LocaleName { get; }

        /// <summary>
        /// Sottoinsiemi Unicode.
        /// </summary>
        public UnicodeSubset[] UnicodeSubsets { get; }

        /// <summary>
        /// Code pages OEM e ANSI predefinite.
        /// </summary>
        public CodePageInfo[] CodePages { get; }

        /// <summary>
        /// Code pages in cui la località è supportata.
        /// </summary>
        public CodePageInfo[] SupportedCodePages { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="LocaleSignature"/>.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Structure">Struttura <see cref="LOCALESIGNATURE"/> con le informazioni.</param>
        internal LocaleSignature(string LocaleName, LOCALESIGNATURE Structure)
        {
            this.LocaleName = LocaleName;
            BitArray UnicodeSubsets = BuildUnicodeSubsetArray(Structure.UnicodeSubsetBitfield);
            List<UnicodeSubset> Subsets = new List<UnicodeSubset>();
            Tuple<int[], string>[] SubsetData;
            for (int i = 0; i < 122; i++)
            {
                if (UnicodeSubsets[i])
                {
                    SubsetData = NLS.UnicodeSubsetsInfo[i];
                    foreach (Tuple < int[], string> data in SubsetData)
                    {
                        Subsets.Add(new UnicodeSubset(data.Item1, data.Item2));
                    }
                }
            }
            this.UnicodeSubsets = Subsets.ToArray();
            BitArray CodePageBitfield = BuildCodePageBitfield(Structure.DefaultCodePages);
            List<CodePageInfo> CodePagesInfo = new List<CodePageInfo>();
            int CodePageValue;
            for (int i = 0;i < CodePageBitfield.Count; i++)
            {
                if (CodePageBitfield[i])
                {
                    CodePageValue = NLS.CodePageBitFields[i].Item1;
                    CodePagesInfo.Add(NLS.GetCodePageInfo(CodePageValue));
                }
            }
            CodePages = CodePagesInfo.ToArray();
            BitArray SupportedCodePageBitfield = BuildCodePageBitfield(Structure.DefaultCodePages);
            List<CodePageInfo> SupportedCodePagesInfo = new List<CodePageInfo>();
            for (int i = 0; i < CodePages.Length; i++)
            {
                if (SupportedCodePageBitfield[i])
                {
                    CodePageValue = NLS.CodePageBitFields[i].Item1;
                    SupportedCodePagesInfo.Add(NLS.GetCodePageInfo(CodePageValue));
                }
            }
            SupportedCodePages = SupportedCodePagesInfo.ToArray();
        }

        /// <summary>
        /// Crea l'array di bit dei subset Unicode.
        /// </summary>
        /// <param name="SubsetValue">Array originale con i valori.</param>
        /// <returns>Array di bit.</returns>
        private static BitArray BuildUnicodeSubsetArray(uint[] SubsetValue)
        {
            BitArray SubsetBits = new BitArray(128);
            BitArray Value0 = new BitArray(BitConverter.GetBytes(SubsetValue[0]));
            BitArray Value1 = new BitArray(BitConverter.GetBytes(SubsetValue[1]));
            BitArray Value2 = new BitArray(BitConverter.GetBytes(SubsetValue[2]));
            BitArray Value3 = new BitArray(BitConverter.GetBytes(SubsetValue[3]));
            for (int i = 0; i < Value0.Count; i++)
            {
                SubsetBits[i] = Value0[i];
            }
            for (int i = 0; i < Value1.Count; i++)
            {
                SubsetBits[i + 32] = Value1[i];
            }
            for (int i = 0; i < Value2.Count; i++)
            {
                SubsetBits[i + 64] = Value2[i];
            }
            for (int i = 0; i < Value3.Count; i++)
            {
                SubsetBits[i + 96] = Value3[i];
            }
            return SubsetBits;
        }

        /// <summary>
        /// Costruisce il campo di bit per le code page.
        /// </summary>
        /// <param name="Values">Valori che contengono i dati.</param>
        /// <returns>Il campo di bit.</returns>
        private static BitArray BuildCodePageBitfield(uint[] Values)
        {
            BitArray Bitfield = new BitArray(64);
            BitArray Value0 = new BitArray(BitConverter.GetBytes(Values[0]));
            BitArray Value1 = new BitArray(BitConverter.GetBytes(Values[1]));
            for (int i = 0; i < Value0.Count; i++)
            {
                Bitfield[i] = Value0[i];
            }
            for (int i = 0; i < Value1.Count; i++)
            {
                Bitfield[i + 32] = Value1[i];
            }
            return Bitfield;
        }
    }
}
