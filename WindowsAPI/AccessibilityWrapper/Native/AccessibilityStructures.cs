using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;

namespace WindowsAPI.AccessibilityWrapper.Native
{
    /// <summary>
    /// Strutture accessibilità.
    /// </summary>
    internal static class AccessibilityStructures
    {
        /// <summary>
        /// Informazioni sul timeout associato alle funzionalità di accessibilità.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ACCESSTIMEOUT
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Impostazione timeout.
            /// </summary>
            public TimeoutBehaviour TimeoutBehaviour;
            /// <summary>
            /// Periodo, in millisecondi, del timeout.
            /// </summary>
            public uint TimeoutMilliseconds;
        }

        /// <summary>
        /// Informazioni sulla funzionalità Filtro Tasti.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct FILTERKEYS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà funzionalità.
            /// </summary>
            public FilterKeysProperties Properties;
            /// <summary>
            /// Tempo, in millisecondi, che l'utente deve tenere premuto un tasto prima che sia accettato dal computer.
            /// </summary>
            public uint KeyDownAcceptMilliseconds;
            /// <summary>
            /// Tempo, in millisecondi, che l'utente deve tenere premuto un tasto prima che si ripeta.
            /// </summary>
            public uint KeyDownRepeatMilliseconds;
            /// <summary>
            /// Tempo, in millisecondi, tra ogni ripetizione di un tasto.
            /// </summary>
            public uint KeyRepetitionPeriodMilliseconds;
            /// <summary>
            /// Tempo, in millisecondi, che deve passare dopo il rilascio di un tasto prima che il computer accetti una nuova pressione dello stesso tasto.
            /// </summary>
            public uint KeyReleasePeriodMilliseconds;
        }

        /// <summary>
        /// Informazioni sulla funzionalità Alto Contrasto.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct HIGHCONTRAST
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà della funzionalità.
            /// </summary>
            public HighContrastProperties Properties;
            /// <summary>
            /// Nome del schema colori che viene impostato come schema di default.
            /// </summary>
            public string DefaultScheme;
        }

        /// <summary>
        /// Informazioni sulla funzionalità MouseKeys.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MOUSEKEYS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà della funzionalità.
            /// </summary>
            public MouseKeysProperties Properties;
            /// <summary>
            /// Velocità massima del cursore del mouse quando lo si muove tramite le frecce direzionali.
            /// </summary>
            public uint ArrowKeyMaxCursorSpeed;
            /// <summary>
            /// Tempo, in millisecondi, per il cursore impiega per raggiungere la velocità massima quando una freccia direzionali viene premuta.
            /// </summary>
            /// <remarks>Il valori validi vanno da 1000 a 5000.</remarks>
            public uint TimeToMaxSpeed;
            /// <summary>
            /// Moltiplicatore applicato alla velocità del cursore del mouse quando l'utente tiene premuto il tasto CTRL mentre le frecce direzionali vengono usate per muoverlo.
            /// </summary>
            /// <remarks>Se <see cref="MouseKeysProperties.MKF_MODIFIERS"/> non è impostato, questo valore è ignorato.</remarks>
            public uint CtrlMultiplier;
            /// <summary>
            /// Riservato per uso futuro, deve essere zero.
            /// </summary>
            private uint Reserved1;
            /// <summary>
            /// Riservato per uso futuro, deve essere zero.
            /// </summary>
            private uint Reserved2;
        }

        /// <summary>
        /// Informazioni sulla funzionalità SerialKeys.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct SERIALKEYS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà della funzionalità.
            /// </summary>
            public SerialKeysProperties Properties;
            /// <summary>
            /// Nome della porta seriale che riceve l'input quando la funzionalità è attivata.
            /// </summary>
            /// <remarks>Se nessuna porta viene usata, questo valore è nullo, se la stringa ha valore "Auto", il sistema monitora tutte le porte seriali inutilizzate.</remarks>
            public string ActivePortString;
            /// <summary>
            /// Riservato, deve essere nullo.
            /// </summary>
            private string Port;
            /// <summary>
            /// Velocità di trasmissione.
            /// </summary>
            public BaudRate BaudRate;
            /// <summary>
            /// Stato della porta.
            /// </summary>
            public PortState State;
            /// <summary>
            /// Porta attiva.
            /// </summary>
            public uint ActivePort;
        }

        /// <summary>
        /// Informazioni sulla funzionalità SoundSentry.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct SOUNDSENTRY
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà della funzionalità.
            /// </summary>
            public SoundSentryProperties Properties;
            /// <summary>
            /// Segnale visuale da visualizzare quando un'applicazione testuale genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
            /// </summary>
            public TextAppVisualSignal TextVisualSignal;
            /// <summary>
            /// Durata, in millisecondi, del segnale visuale mostrato da un'applicazione testuale.
            /// </summary>
            public uint TextVisualSignalDuration;
            /// <summary>
            /// Valore RGB del colore da usare quando deve essere visualizzato il segnale visuale da un'applicazione testuale.
            /// </summary>
            public uint TextVisualSignalColor;
            /// <summary>
            /// Segnale visuale da visualizzare quando un'applicazione in modalità grafica genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
            /// </summary>
            public VirtualGraphicsAppVisualSignal GraphicsAppVisualSignal;
            /// <summary>
            /// Durata, in millisecondi, del segnale visuale mostrato da un'applicazione grafica.
            /// </summary>
            public uint GraphicsAppVisualSignalDuration;
            /// <summary>
            /// Valore RGB del colore da usare quando deve essere visualizzato il segnale visuale da un'applicazione grafica.
            /// </summary>
            public uint GraphicsAppVisualSignalColor;
            /// <summary>
            /// Segnale visuale da visualizzare quando un suono viene generato da un'applicazione basata su Windows o da un'applicazione MS-DOS in esecuzione in una finestra.
            /// </summary>
            public WindowAppVisualSignal WindowAppVisualSignal;
            /// <summary>
            /// Durata, in millisecondi, del segnale visuale mostrato da un'applicazione basata su Windows o da un'applicazione MS-DOS in esecuzione in una finestra.
            /// </summary>
            public uint WindowAppVisualSignalDuration;
            /// <summary>
            /// Riservato per uso futuro, dev essere nullo.
            /// </summary>
            private string WindowsEffectDLL;
            /// <summary>
            /// Riservato per uso futuro, deve essere zero.
            /// </summary>
            private uint WindowsEffectOrdinal;
        }

        /// <summary>
        /// Informazioni sulla funzionalità StickyKeys.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct STICKYKEYS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà della funzionalità.
            /// </summary>
            public StickyKeysProperties Properties;
        }

        /// <summary>
        /// Informazioni sulla funzionalità Tasti permanenti.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct TOGGLEKEYS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Proprietà della funzionalità.
            /// </summary>
            public ToggleKeysProperties Properties;
        }

        /// <summary>
        /// Informazioni sulla funzionalità Descrizioni audio.
        /// </summary>
        [StructLayout (LayoutKind.Sequential)]
        internal struct AUDIODESCRIPTION
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Indica se la funzionalità è abilitata.
            /// </summary>
            [MarshalAs(UnmanagedType.Bool)]
            public bool Enabled;
            /// <summary>
            /// Lingua delle descrizioni audio.
            /// </summary>
            public uint LocaleID;
        }
    }
}