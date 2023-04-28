using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Callbacks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSCallbacks;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses
{
    /// <summary>
    /// Opzioni per il riconoscimento del testo.
    /// </summary>
    public class TextRecognitionOptions
    {
        /// <summary>
        /// Lingua di input accettata dal servizio.
        /// </summary>
        /// <remarks>Questo campo segue le convenzioni IETF.</remarks>
        public string InputLanguage { get; }

        /// <summary>
        /// Lingua di output da usare per il risultato.
        /// </summary>
        /// <remarks>Questo campo segue le convenzioni IETF.</remarks>
        public string OutputLanguage { get; }

        /// <summary>
        /// Nome script Unicode accettato dal servizio.
        /// </summary>
        /// <remarks>Questo campo può essere nullo per permettere al servizio di decidere come gestire l'input.</remarks>
        public string InputScript { get; }

        /// <summary>
        /// Nome script Unicode che il servizio dovrà usare per il risultato.
        /// </summary>
        /// <remarks>Questo campo può essere nullo per permettere al servizio di decidere lo script di output.</remarks>
        public string OutputScript { get; }

        /// <summary>
        /// Tipo di contenuto MIME interpretabile dal servizio.
        /// </summary>
        public string InputContentType { get; }

        /// <summary>
        /// Tipo di contenuto MIME che il servizio deve usare per formattare il risultato.
        /// </summary>
        /// <remarks>Questo campo può essere nullo per permettere al servizio di decidere il formato dell'output.</remarks>
        public string OutputContentType { get; }

        /// <summary>
        /// Callback che riceve i risultati dell'elaborazione del testo.
        /// </summary>
        /// <remarks>Questo campo può essere nullo se si vuole eseguire il riconoscimento in modo sincrono.</remarks>
        public RecognitionResultCallback RecognizeCallback { get; }

        /// <summary>
        /// Dati privati dell'applicazione.
        /// </summary>
        public byte[] CallerData { get; }

        /// <summary>
        /// Valore usato per influenzare il comportamento del servizio.
        /// </summary>
        public int? ServiceFlag { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="TextRecognitionOptions"/>.
        /// </summary>
        /// <param name="InputLanguage">Lingua di input accettata dal servizio.</param>
        /// <param name="OutputLanguage">Lingua di output da usare per il risultato.</param>
        /// <param name="InputScript">Nome script Unicode accettato dal servizio.</param>
        /// <param name="OutputScript">Nome script Unicode che il servizio dovrà usare per il risultato.</param>
        /// <param name="InputContentType">Tipo di contenuto MIME interpretabile dal servizio.</param>
        /// <param name="OutputContentType">Tipo di contenuto MIME che il servizio deve usare per formattare il risultato.</param>
        /// <param name="Callback">Callback che riceve i risultati dell'elaborazione del testo.</param>
        /// <param name="CallerData">Dati privati dell'applicazione.</param>
        /// <param name="ServiceFlag">Valore usato per influenzare il comportamento del servizio.</param>
        public TextRecognitionOptions(string InputLanguage = null, string OutputLanguage = null, string InputScript = null, string OutputScript = null, string InputContentType = null, string OutputContentType = null, RecognitionResultCallback Callback = null, byte[] CallerData = null, int? ServiceFlag = null)
        {
            this.InputLanguage = InputLanguage;
            this.OutputLanguage = OutputLanguage;
            this.InputScript = InputScript;
            this.OutputScript = OutputScript;
            this.InputContentType = InputContentType;
            this.OutputContentType = OutputContentType;
            RecognizeCallback = Callback;
            this.CallerData = CallerData;
            this.ServiceFlag = ServiceFlag;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="MAPPING_OPTIONS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="MAPPING_OPTIONS"/> risultato della conversione.</returns>
        internal MAPPING_OPTIONS ToStructure()
        {
            MAPPING_OPTIONS Structure = new MAPPING_OPTIONS()
            {
                InputLanguage = InputLanguage,
                OutputLanguage = OutputLanguage,
                InputScript = InputScript,
                OutputScript = OutputScript,
                InputContentType = InputContentType,
                OutputContentType = OutputContentType
            };
            if (RecognizeCallback != null)
            {
                ELS.TextRecognitionManagedCallback = RecognizeCallback;
                Structure.RecognizeCallback = ELS.TextRecognitionCallback;
            }
            else
            {
                Structure.RecognizeCallback = null;
            }
            if (CallerData != null && CallerData.Length > 0)
            {
                IntPtr DataPointer = Marshal.AllocHGlobal(CallerData.Length);
                Marshal.Copy(CallerData, 0, DataPointer, CallerData.Length);
                Structure.RecognizeCallerData = DataPointer;
                Structure.RecognizeCallerDataSize = (uint)CallerData.Length;
            }
            else
            {
                Structure.RecognizeCallerData = IntPtr.Zero;
                Structure.RecognizeCallerDataSize = 0;
            }
            if (ServiceFlag.HasValue)
            {
                Structure.ServiceFlag = (uint)ServiceFlag.Value;
            }
            else
            {
                Structure.ServiceFlag = 0;
            }
            return Structure;
        }
    }
}