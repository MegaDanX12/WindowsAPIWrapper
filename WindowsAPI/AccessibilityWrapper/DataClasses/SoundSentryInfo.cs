using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Enumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità SoundSentry.
    /// </summary>
    public class SoundSentryInfo
    {
        /// <summary>
        /// Indica se la funzionalità è disponibile.
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        /// Indica se la funzionalità è attiva.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Segnale visuale presentato quando un'applicazione in modalità testo genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public TextApplicationVisualSignal TextAppVisualSignal { get; }

        /// <summary>
        /// Durata, in secondi, del segnale visuale presentato quando un'applicazione in modalità testo genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public int TextAppVisualSignalDuration { get; }

        /// <summary>
        /// Colore del segnale visuale presentato quando un'applicazione in modalità testo genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public Color TextAppVisualSignalColor { get; }

        /// <summary>
        /// Segnale visuale presentato quando un'applicazione in modalità grafica genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public GraphicsApplicationVisualSignal GraphicsAppVisualSignal { get; }

        /// <summary>
        /// Durata, in secondi, del segnale visuale presentato quando un'applicazione in modalità grafica genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public int GraphicsAppVisualSignalDuration { get; }

        /// <summary>
        /// Colore del segnale visuale presentato quando un'applicazione in modalità grafica genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public Color GraphicsAppVisualSignalColor { get; }

        /// <summary>
        /// Segnale visuale presentato quando un'applicazione basata su Windows oppure un'applicazione MS-DOS in esecuzione in una finestra.
        /// </summary>
        public WindowApplicationVisualSignal WindowAppVisualSignal { get; }

        /// <summary>
        /// Durata, in secondi, del segnale visuale presentato quando un'applicazione basata su Windows oppure un'applicazione MS-DOS in esecuzione in una finestra.
        /// </summary>
        public int WindowAppVisualSignalDuration { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SoundSentryInfo"/>.
        /// </summary>
        /// <param name="SoundSentryData">Struttura <see cref="SOUNDSENTRY"/> con le informazioni.</param>
        internal SoundSentryInfo(SOUNDSENTRY SoundSentryData)
        {
            IsAvailable = SoundSentryData.Properties.HasFlag(SoundSentryProperties.SSF_AVAILABLE);
            IsEnabled = SoundSentryData.Properties.HasFlag(SoundSentryProperties.SSF_SOUNDSENTRYON);
            TextAppVisualSignal = (TextApplicationVisualSignal)SoundSentryData.TextVisualSignal;
            TextAppVisualSignalDuration = (int)(SoundSentryData.TextVisualSignalDuration / 1000);
            TextAppVisualSignalColor = ColorTranslator.FromWin32((int)SoundSentryData.TextVisualSignalColor);
            GraphicsAppVisualSignal = (GraphicsApplicationVisualSignal)SoundSentryData.GraphicsAppVisualSignal;
            GraphicsAppVisualSignalDuration = (int)(SoundSentryData.GraphicsAppVisualSignalDuration / 1000);
            GraphicsAppVisualSignalColor = ColorTranslator.FromWin32((int)SoundSentryData.GraphicsAppVisualSignalColor);
            WindowAppVisualSignal = (WindowApplicationVisualSignal)SoundSentryData.WindowAppVisualSignal;
            WindowAppVisualSignalDuration = (int)WindowAppVisualSignalDuration;
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SoundSentryInfo"/>.
        /// </summary>
        /// <param name="Available">Indica se la funzionalità è disponibile.</param>
        /// <param name="Enabled">Indica se la funzionalità è attiva.</param>
        /// <param name="WindowAppVisualSignal">Segnale visuale presentato quando un'applicazione basata su Windows oppure un'applicazione MS-DOS in esecuzione in una finestra.</param>
        /// <param name="WindowAppVisualSignalDuration">Durata, in secondi, del segnale visuale presentato quando un'applicazione basata su Windows oppure un'applicazione MS-DOS in esecuzione in una finestra.</param>
        public SoundSentryInfo(bool Available, bool Enabled, WindowApplicationVisualSignal WindowAppVisualSignal, int WindowAppVisualSignalDuration = 0)
        {
            IsAvailable = Available;
            IsEnabled = Enabled;
            this.WindowAppVisualSignal = WindowAppVisualSignal;
            this.WindowAppVisualSignalDuration = WindowAppVisualSignalDuration;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="SOUNDSENTRY"/>.
        /// </summary>
        /// <returns>Struttura <see cref="SOUNDSENTRY"/> risultato della conversione.</returns>
        internal SOUNDSENTRY ToStruct()
        {
            SOUNDSENTRY Structure = new SOUNDSENTRY()
            {
                Size = (uint)Marshal.SizeOf(typeof(SOUNDSENTRY)),
                WindowAppVisualSignal = (WindowAppVisualSignal)WindowAppVisualSignal,
                WindowAppVisualSignalDuration = (uint)WindowAppVisualSignalDuration
            };
            SoundSentryProperties Properties = 0;
            if (IsAvailable)
            {
                Properties |= SoundSentryProperties.SSF_AVAILABLE;
            }
            if (IsEnabled)
            {
                Properties |= SoundSentryProperties.SSF_SOUNDSENTRYON;
            }
            Structure.Properties = Properties;
            return Structure;
        }
    }
}