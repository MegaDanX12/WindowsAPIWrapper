using System;
using System.Runtime.InteropServices;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;

namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni di versione NLS.
    /// </summary>
    public class NLSVersionInfo
    {
        /// <summary>
        /// Nome località.
        /// </summary>
        public string LocaleName { get; }

        /// <summary>
        /// Versione della funzionalità NLS.
        /// </summary>
        public int Version { get; }


        private readonly int DefinedVersion;


        private readonly int EffectiveID;

        /// <summary>
        /// GUID del comportamento di un'ordinamento personalizzato usato dalla località.
        /// </summary>
        public Guid CustomSortBehaviorGUID { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="NLSVersionInfo"/>.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Info">Struttura <see cref="NLSVERSIONINFO"/> con le informazioni.</param>
        internal NLSVersionInfo(string LocaleName, NLSVERSIONINFO Info)
        {
            this.LocaleName = LocaleName;
            Version = (int)Info.Version;
            DefinedVersion = (int)Info.DefinedVersion;
            EffectiveID = (int)Info.EffectiveID;
            CustomSortBehaviorGUID = Info.CustomVersionGuid;
        }

        /// <summary>
        /// Converte i dati in questa istanza in una struttura <see cref="NLSVERSIONINFO"/>.
        /// </summary>
        /// <returns>Struttura <see cref="NLSVERSIONINFO"/> risultato della conversione.</returns>
        internal NLSVERSIONINFO ToStructure()
        {
            NLSVERSIONINFO Structure = new NLSVERSIONINFO()
            {
                Size = (uint)Marshal.SizeOf(typeof(NLSVERSIONINFO)),
                Version = (uint)Version,
                DefinedVersion = (uint)DefinedVersion,
                EffectiveID = (uint)EffectiveID,
                CustomVersionGuid = CustomSortBehaviorGUID
            };
            return Structure;
        }
    }
}