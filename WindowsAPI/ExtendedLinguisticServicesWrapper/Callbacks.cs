using WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses;

namespace WindowsAPI.ExtendedLinguisticServicesWrapper
{
    /// <summary>
    /// Delegati usati dai servizi ELS.
    /// </summary>
    public static class Callbacks
    {
        /// <summary>
        /// Delegato che elabora in modo asincrono i dati prodotti da <see cref="ELS.RecognizeText"/>.
        /// </summary>
        /// <param name="Properties">Istanza di <see cref="TextRecognitionProperties"/> contenente i risultati dell'elaborazione del testo.</param>
        /// <param name="ApplicationData">Dati privati dell'applicazione.</param>
        /// <param name="Success">Indica se l'operazione ha avuto successo.</param>
        public delegate void RecognitionResultCallback(TextRecognitionProperties Properties, byte[] ApplicationData, bool Success);
    }
}