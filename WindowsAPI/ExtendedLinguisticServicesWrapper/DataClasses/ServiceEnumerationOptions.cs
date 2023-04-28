using System;
using System.Runtime.InteropServices;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses
{
    /// <summary>
    /// Opzioni di enumerazione servizi.
    /// </summary>
    public class ServiceEnumerationOptions
    {
        /// <summary>
        /// Categoria del servizio.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// Lingua di input supportata dal servizio.
        /// </summary>
        /// <remarks>Il nome segue le convenzioni IETF.</remarks>
        public string InputLanguage { get; }

        /// <summary>
        /// Lingua di output nella quale il servizio produce risultati.
        /// </summary>
        /// <remarks>Il nome segue le convezioni IETF.</remarks>
        public string OutputLanguage { get; }

        /// <summary>
        /// Nome Unicode di script accettati dal servizio.
        /// </summary>
        public string InputScript { get; }

        /// <summary>
        /// Nome Unicode di script usati da servizio.
        /// </summary>
        public string OutputScript { get; }

        /// <summary>
        /// Tipo di contenuto MIME supportato dal servizio.
        /// </summary>
        public string InputContentType { get; }

        /// <summary>
        /// Tipo di contenuto MIME nel quale il servizio recupera dati.
        /// </summary>
        public string OutputContentType { get; }

        /// <summary>
        /// GUID del servizio.
        /// </summary>
        public Guid? GUID { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="ServiceEnumerationOptions"/>.
        /// </summary>
        /// <param name="ServiceCategory">Categoria del servizio.</param>
        /// <param name="ServiceInputLanguage">Lingua di input supportata dal servizio.</param>
        /// <param name="ServiceOutputLanguage">Lingua di output nella quale il servizio produce risultati.</param>
        /// <param name="ServiceInputScript">Nome Unicode di script accettati dal servizio.</param>
        /// <param name="ServiceOutputScript">Nome Unicode di script usati da servizio.</param>
        /// <param name="ServiceInputContentType">Tipo di contenuto MIME supportato dal servizio.</param>
        /// <param name="ServiceOutputContentType">Tipo di contenuto MIME nel quale il servizio recupera dati.</param>
        /// <param name="ServiceGUID">GUID del servizio.</param>
        public ServiceEnumerationOptions(string ServiceCategory = null, string ServiceInputLanguage = null, string ServiceOutputLanguage = null, string ServiceInputScript = null, string ServiceOutputScript = null, string ServiceInputContentType = null, string ServiceOutputContentType = null, Guid? ServiceGUID = null)
        {
            Category = ServiceCategory;
            InputLanguage = ServiceInputLanguage;
            OutputLanguage = ServiceOutputLanguage;
            InputScript = ServiceInputScript;
            OutputScript = ServiceOutputScript;
            InputContentType = ServiceInputContentType;
            OutputContentType = ServiceOutputContentType;
            GUID = ServiceGUID;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="MAPPING_ENUM_OPTIONS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="MAPPING_ENUM_OPTIONS"/> risultato della conversione.</returns>
        internal MAPPING_ENUM_OPTIONS ToStructure()
        {
            MAPPING_ENUM_OPTIONS Structure = new MAPPING_ENUM_OPTIONS
            {
                Size = new IntPtr(Marshal.SizeOf(typeof(MAPPING_ENUM_OPTIONS))),
                Category = Category,
                InputLanguage = InputLanguage,
                OutputLanguage = OutputLanguage,
                InputScript = InputScript,
                OutputScript = OutputScript,
                InputContentType = InputContentType,
                OutputContentType = OutputContentType
            };
            if (GUID.HasValue) 
            {
                IntPtr StructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Guid)));
                Marshal.StructureToPtr(GUID, StructurePointer, false);
                Structure.GUID = StructurePointer;
            }
            else
            {
                Structure.GUID = IntPtr.Zero;
            }
            return Structure;
        }
    }
}