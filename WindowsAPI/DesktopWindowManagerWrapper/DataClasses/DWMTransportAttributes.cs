namespace WindowsAPI.DesktopWindowManagerWrapper.DataClasses
{
    /// <summary>
    /// Attributi di trasporto DWM.
    /// </summary>
    public class DWMTransportAttributes
    {
        /// <summary>
        /// Indica se il remoting è supportato.
        /// </summary>
        public bool SupportsRemoting { get; }

        /// <summary>
        /// Indica se il trasporto è connesso.
        /// </summary>
        public bool IsConnected { get; }

        /// <summary>
        /// Generazione del trasporto.
        /// </summary>
        public int Generation { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DWMTransportAttributes"/>.
        /// </summary>
        /// <param name="SupportsRemoting">Indica se il remoting è supportato.</param>
        /// <param name="IsConnected">Indica se il trasporto è connesso.</param>
        /// <param name="Generation">Generazione del trasporto.</param>
        internal DWMTransportAttributes(bool SupportsRemoting, bool IsConnected, uint Generation)
        {
            this.SupportsRemoting = SupportsRemoting;
            this.IsConnected = IsConnected;
            this.Generation = (int)Generation;
        }
    }
}
