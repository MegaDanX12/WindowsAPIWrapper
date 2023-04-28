using System;

namespace WindowsAPI.AccessibilityWrapper.Native
{
    /// <summary>
    /// Enumerazioni accessibilità.
    /// </summary>
    internal static class AccessibilityEnumerations
    {
        /// <summary>
        /// Tipo di puntatore.
        /// </summary>
        internal enum POINTER_INPUT_TYPE
        {
            /// <summary>
            /// Puntatore generico.
            /// </summary>
            PT_POINTER = 1,
            /// <summary>
            /// Puntatore touch.
            /// </summary>
            PT_TOUCH,
            /// <summary>
            /// Puntatore penna.
            /// </summary>
            PT_PEN,
            /// <summary>
            /// Puntatore mouse.
            /// </summary>
            PT_MOUSE,
            /// <summary>
            /// PUntatore touchpad.
            /// </summary>
            PT_TOUCHPAD
        }

        /// <summary>
        /// Comportamento del timeout delle funzionalità di accessibilità.
        /// </summary>
        [Flags]
        internal enum TimeoutBehaviour : uint
        {
            /// <summary>
            /// Viene riprodotto un suono quando il periodo di timeout è scaduto e le funzionalità di accessibilità sono disattivate.
            /// </summary>
            ATF_ONOFFFEEDBACK = 2,
            /// <summary>
            /// Il periodo di timeout è abilitato.
            /// </summary>
            ATF_TIMEOUTON = 1
        }

        /// <summary>
        /// Proprietà di Filtro tasti.
        /// </summary>
        [Flags]
        internal enum FilterKeysProperties : uint
        {
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            FKF_AVAILABLE = 2,
            /// <summary>
            /// Viene riprodotto un suono quando un tasto viene premuto e accettato.<br/>
            /// Se SlowKeys è attivo, il suono viene riprodotto quando il tasto viene premuto e quando viene accettato.
            /// </summary>
            FKF_CLICKON = 64,
            /// <summary>
            /// Una finestra di dialogo di conferma viene visualizzata quando la funzionalità viene attivata tramite hotkey.
            /// </summary>
            FKF_CONFIRMHOTKEY = 8,
            /// <summary>
            /// La funzionalità è attiva.
            /// </summary>
            FKF_FILTERKEYSON = 1,
            /// <summary>
            /// L'utente può attivare o disattivare la funzionalità tenendo premuto lo shift destro per otto secondi.
            /// </summary>
            FKF_HOTKEYACTIVE = 4,
            /// <summary>
            /// Il computer riproduce un suono quando la funzionalità viene attivata o disattivata tramite l'hotkey.
            /// </summary>
            FKF_HOTKEYSOUND = 16,
            /// <summary>
            /// Un indicatore visuale viene visualizzato quando la funzioanlità è attiva.
            /// </summary>
            FKF_INDICATOR = 32
        }

        /// <summary>
        /// Proprietà di Alto Contrasto.
        /// </summary>
        [Flags]
        internal enum HighContrastProperties : uint
        {
            /// <summary>
            /// La funzionalità è attiva.
            /// </summary>
            HCF_HIGHCONTRASTON = 1,
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            HCF_AVAILABLE,
            /// <summary>
            /// L'utente può attivare o disattivare la funzionalità premendo simultaneamente ALT sinistro, SHIFT sinistro e PRINT SCREEN.
            /// </summary>
            HCF_HOTKEYACTIVE = 4,
            /// <summary>
            /// Una finestra di dialogo di conferma appare quando la funzionalità viene attivata usando l'hotkey.
            /// </summary>
            HCF_CONFIRMHOTKEY = 8,
            /// <summary>
            /// Viene riprodotto un suono quando la funzionalità viene attivata o disattiva tramite l'hotkey.
            /// </summary>
            HCF_HOTKEYSOUND = 16,
            /// <summary>
            /// Un indicatore visuale viene visualizato quando la funzionalità è attiva.
            /// </summary>
            /// <remarks>Questo valore non è attualmente utilizzato.</remarks>
            HCF_INDICATOR = 32,
            /// <summary>
            /// L'hotkey può essere abilitata.
            /// </summary>
            /// <remarks>Questo valore non può essere impostato.</remarks>
            HCF_HOTKEYAVAILABLE = 64,
            /// <summary>
            /// Questo valore impedisce il verificarsi di effetti relativi al cambio tema se esso non viene cambiato.
            /// </summary>
            /// <remarks>Questa opzione non dovrebbe essere utilizzata con <see cref="HCF_HIGHCONTRASTON"/>.</remarks>
            HCF_OPTION_NOTHEMECHANGE = 4096
        }

        /// <summary>
        /// Proprietà di MouseKeys.
        /// </summary>
        [Flags]
        internal enum MouseKeysProperties : uint
        {
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            MKF_AVAILABLE = 2,
            /// <summary>
            /// Una finestra di dialogo di conferma appare quando la funzionalità viene attivata usando l'hotkey.
            /// </summary>
            MKF_CONFIRMHOTKEY = 8,
            /// <summary>
            /// L'utente può attivare e disattivare la funzionalità usando l'hotkey (ALT sinistro + SHIFT sinistro + NUM LOCK).
            /// </summary>
            MKF_HOTKEYACTIVE = 4,
            /// <summary>
            /// Il sistema riproduce un suono quando l'utente attiva o disattiva la funzionalità usando l'hotkey.
            /// </summary>
            MKF_HOTKEYSOUND = 16,
            /// <summary>
            /// Un indicatore visuale viene visualizzato quando la funzionalità è attiva.
            /// </summary>
            MKF_INDICATOR = 32,
            /// <summary>
            /// Il tasto sinistro è in stato "premuto".
            /// </summary>
            MKF_LEFTBUTTONDOWN = 16777216,
            /// <summary>
            /// L'utente ha selezionato il tasto sinistro per le azioni del mouse.
            /// </summary>
            MKF_LEFTBUTTONSEL = 268435456,
            /// <summary>
            /// Il tasto CTRL aumenta la velocità del cursore, il tasto SHIFT causa un breve ritardo dopo il movimento del mouse di un singolo pixel.
            /// </summary>
            MKF_MODIFIERS = 64,
            /// <summary>
            /// La funzionalità è attivata.
            /// </summary>
            MKF_MOUSEKEYSON = 1,
            /// <summary>
            /// Il sistema elabora l'input del tastierino numerico come comandi del mouse.
            /// </summary>
            MKF_MOUSEMODE = 2147483648,
            /// <summary>
            /// Il tastierino numerico muove il mouse quando NUM LOCK è attivo.
            /// </summary>
            MKF_REPLACENUMBERS = 128,
            /// <summary>
            /// Il tasto destro è in stato "premuto".
            /// </summary>
            MKF_RIGHTBUTTONDOWN = 33554432,
            /// <summary>
            /// L'utente ha selezionato il tasto destro per le azioni del mouse.
            /// </summary>
            MKF_RIGHTBUTTONSEL = 536870912
        }
        #region SerialKeys Feature
        /// <summary>
        /// Proprietà della funzionalità SerialKeys.
        /// </summary>
        [Flags]
        internal enum SerialKeysProperties : uint
        {
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            SERKF_AVAILABLE = 2,
            /// <summary>
            /// Un indicatore visuale viene visualizzato quando la funzionalità è attiva.
            /// </summary>
            /// <remarks>Questo valore non è attualmente usato.</remarks>
            SERKF_INDICATOR = 4,
            /// <summary>
            /// La funzionalità è attiva.
            /// </summary>
            SERKF_SERIALKEYSON = 1
        }

        /// <summary>
        /// Velocità di trasmissione.
        /// </summary>
        internal enum BaudRate : uint
        {
            NotUsed,
            CBR_110 = 110,
            CBR_300 = 300,
            CBR_600 = 600,
            CBR_1200 = 1200,
            CBR_2400 = 2400,
            CBR_4800 = 4800,
            CBR_9600 = 9600,
            CBR_14400 = 14400,
            CBR_19200 = 19200,
            CBR_38400 = 38400,
            CBR_56000 = 56000,
            CBR_57600 = 57600,
            CBR_115200 = 115200,
            CBR_128000 = 128000,
            CBR_256000 = 256000
        }

        /// <summary>
        /// Stato della porta.
        /// </summary>
        internal enum PortState : uint
        {
            /// <summary>
            /// Tutto l'input è ignorato.
            /// </summary>
            Ignored,
            /// <summary>
            /// L'input viene monitorato per le sequenze di attivazione di SerialKeys.
            /// </summary>
            InputWatchedForActivationSeq,
            /// <summary>
            /// Tutto l'input viene monitorato.
            /// </summary>
            InputWatched
        }
        #endregion
        #region SoundSentry Feature
        /// <summary>
        /// Proprietà della funzionalità SoundSentry.
        /// </summary>
        [Flags]
        internal enum SoundSentryProperties : uint
        {
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            SSF_AVAILABLE = 2,
            /// <summary>
            /// Non implementato.
            /// </summary>
            SSF_INDICATOR = 4,
            /// <summary>
            /// La funzionalità è attiva.
            /// </summary>
            SSF_SOUNDSENTRYON = 1
        }

        /// <summary>
        /// Segnale visuale da presentare quando un'applicazione in modalità testuale genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        internal enum TextAppVisualSignal : uint
        {
            /// <summary>
            /// Il bordo dello schermo (area overscan) lampeggia.
            /// </summary>
            /// <remarks>Quest'area non è disponibile in alcuni display.</remarks>
            SSTF_BORDER = 2,
            /// <summary>
            /// I caratteri posizionati a bordo schermo lampeggiano.
            /// </summary>
            SSTF_CHARS = 1,
            /// <summary>
            /// Il display lampeggia.
            /// </summary>
            SSTF_DISPLAY = 3,
            /// <summary>
            /// Nessun segnale visuale.
            /// </summary>
            SSTF_NONE = 0
        }

        /// <summary>
        /// Segnale visuale da presentare quando un'applicazione in modalità grafica genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        internal enum VirtualGraphicsAppVisualSignal : uint
        {
            /// <summary>
            /// Il display lampeggia.
            /// </summary>
            SSGF_DISPLAY = 3,
            /// <summary>
            /// Nessn segnale visuale.
            /// </summary>
            SSGF_NONE = 0
        }

        /// <summary>
        /// Segnale visuale da visualizzare quando un'applicazione basata su Windows o MS-DOS in esecuzione in una finestra genera un suono.
        /// </summary>
        internal enum WindowAppVisualSignal : uint
        {
            /// <summary>
            /// Segnale personalizzato.
            /// </summary>
            SSWF_CUSTOM = 4,
            /// <summary>
            /// Il display lampeggia.
            /// </summary>
            SSWF_DISPLAY = 3,
            /// <summary>
            /// Nessun segnale visuale.
            /// </summary>
            SSWF_NONE = 0,
            /// <summary>
            /// La barra del titolo della finestra attiva lampeggia.
            /// </summary>
            SSWF_TITLE = 1,
            /// <summary>
            /// La finestra attiva lampeggia.
            /// </summary>
            SSWF_WINDOW = 2
        }
        #endregion
        /// <summary>
        /// Proprietà della funzionalità StickyKeys.
        /// </summary>
        [Flags]
        internal enum StickyKeysProperties : uint
        {
            /// <summary>
            /// Il sistema genera un suono quando l'utente aggancia, blocca o rilascia i tasti modificatori usando la funzionalità.
            /// </summary>
            SKF_AUDIBLEFEEDBACK = 64,
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            SKF_AVAILABLE = 2,
            /// <summary>
            /// Una finestra di dialogo di conferma viene visualizzata quando la funzionalità è attivata tramite hotkey.
            /// </summary>
            SKF_CONFIRMHOTKEY = 8,
            /// <summary>
            /// La funzionalità può essere attivata o disattivata premendo SHIFT cinque volte.
            /// </summary>
            SKF_HOTKEYACTIVE = 4,
            /// <summary>
            /// Il sistema riproduce un suono quando l'utente attiva o disattiva la funzionalità usando la hotkey.
            /// </summary>
            SKF_HOTKEYSOUND = 16,
            /// <summary>
            /// Un indicatore visuale viene visualizzato quando la funzionalità è attiva.
            /// </summary>
            SKF_INDICATOR = 32,
            /// <summary>
            /// La funzionalità è attiva.
            /// </summary>
            SKF_STICKYKEYSON = 1,
            /// <summary>
            /// Premere un tasto modificatore due volte di file blocca il tasto fino a quando non viene premuto una terza volta.
            /// </summary>
            SKF_TRISTATE = 128,
            /// <summary>
            /// Rilasciare un tasto modificatore che è stato premuto in combinazione con qualunque altro tasto disattiva la funzionalità.
            /// </summary>
            SKF_TWOKEYSOFF = 256,
            /// <summary>
            /// ALT sinistro agganciato.
            /// </summary>
            SKF_LALTLATCHED = 268435456,
            /// <summary>
            /// CTRL sinistro agganciato.
            /// </summary>
            SKF_LCTLLATCHED = 67108864,
            /// <summary>
            /// SHIFT sinistro agganciato.
            /// </summary>
            SKF_LSHIFTLATCHED = 16777216,
            /// <summary>
            /// ALT destro agganciato.
            /// </summary>
            SKF_RALTLATCHED = 536870912,
            /// <summary>
            /// CTRL destro agganciato.
            /// </summary>
            SKF_RCTLLATCHED = 134217728,
            /// <summary>
            /// SHIFT destro agganciato.
            /// </summary>
            SKF_RSHIFTLATCHED = 33554432,
            /// <summary>
            /// ALT sinistro bloccato.
            /// </summary>
            SKF_LALTLOCKED = 1048576,
            /// <summary>
            /// CTRL sinistro bloccato.
            /// </summary>
            SKF_LCTLLOCKED = 262144,
            /// <summary>
            /// SHIFT sinistro bloccato.
            /// </summary>
            SKF_LSHIFTLOCKED = 65536,
            /// <summary>
            /// ALT destro bloccato.
            /// </summary>
            SKF_RALTLOCKED = 2097152,
            /// <summary>
            /// CTRL destro bloccato.
            /// </summary>
            SKF_RCTLLOCKED = 524288,
            /// <summary>
            /// SHIFT destro bloccato.
            /// </summary>
            SKF_RSHIFTLOCKED = 131072,
            /// <summary>
            /// Tasto Windows sinistro agganciato.
            /// </summary>
            SKF_LWINLATCHED = 1073741824,
            /// <summary>
            /// Tasto Windows destro agganciato.
            /// </summary>
            SKF_RWINLATCHED = 2147483648,
            /// <summary>
            /// Tasto Windows sinistro bloccato.
            /// </summary>
            SKF_LWINLOCKED = 4194304,
            /// <summary>
            /// Tasto Windows destro bloccato.
            /// </summary>
            SKF_RWINLOCKED = 8388608
        }

        /// <summary>
        /// Proprietà della funzionalità Tasti permanenti.
        /// </summary>
        [Flags]
        internal enum ToggleKeysProperties : uint
        {
            /// <summary>
            /// La funzionalità è disponibile.
            /// </summary>
            TKF_AVAILABLE = 2,
            /// <summary>
            /// Una finestra di dialogo di conferma viene visualizzata quando la funzionalità viene attivata tramite hotkey.
            /// </summary>
            TKF_CONFIRMHOTKEY = 8,
            /// <summary>
            /// L'utente può abilitare o disabilitare la funzionalità tenendo premuto NUM LOCK per otto secondi.
            /// </summary>
            TKF_HOTKEYACTIVE = 4,
            /// <summary>
            /// Il sistema riproduce un suono quando l'utente attiva o disattiva la funzionalità usando l'hotkey.
            /// </summary>
            TKF_HOTKEYSOUND = 16,
            /// <summary>
            /// Non implementato.
            /// </summary>
            TKF_INDICATOR = 32,
            /// <summary>
            /// La funzionalità è attiva.
            /// </summary>
            TKF_TOGGLEKEYSON = 1
        }

        /// <summary>
        /// Parametri accessibilità.
        /// </summary>
        internal enum AccessibilityParameter : uint
        {
            #region Get values
            /// <summary>
            /// Informazioni sul periodo di timeout delle funzionalità di accessibilità.
            /// </summary>
            SPI_GETACCESSTIMEOUT = 60,
            /// <summary>
            /// Indica se le descrizioni audio sono attive.
            /// </summary>
            SPI_GETAUDIODESCRIPTION = 116,
            /// <summary>
            /// Indica se le animazioni sono attive.
            /// </summary>
            SPI_GETCLIENTAREAANIMATION = 4162,
            /// <summary>
            /// Indica se il contenuto sovrapposto è attivo.
            /// </summary>
            SPI_GETDISABLEOVERLAPPEDCONTENT = 4160,
            /// <summary>
            /// Informazioni sulla funzionalità Filtro tasti.
            /// </summary>
            SPI_GETFILTERKEYS = 50,
            /// <summary>
            /// Altezza, in pixel, dei bordi superiori e inferiori del rettangolo di focus.
            /// </summary>
            SPI_GETFOCUSBORDERHEIGHT = 8208,
            /// <summary>
            /// Larghezza, in pixel, dei bordi sinistro e destro del rettangolo di focus.
            /// </summary>
            SPI_GETFOCUSBORDERWIDTH = 8206,
            /// <summary>
            /// Informazioni sulla funzionalità Alto contrasto.
            /// </summary>
            SPI_GETHIGHCONTRAST = 66,
            /// <summary>
            /// Tempo di visualizzazione delle notifica popup, in secondi.
            /// </summary>
            SPI_GETMESSAGEDURATION = 8214,
            /// <summary>
            /// Stato della funzionalità Blocco del tasto del mouse.
            /// </summary>
            SPI_GETMOUSECLICKLOCK = 4126,
            /// <summary>
            /// Tempo, in millisecondi, prima del blocco del tasto del mouse.
            /// </summary>
            SPI_GETMOUSECLICKLOCKTIME = 8200,
            /// <summary>
            /// Informazioni sulla funzionalità MouseKeys.
            /// </summary>
            SPI_GETMOUSEKEYS = 54,
            /// <summary>
            /// Stato della funzionalità Mouse Sonar.
            /// </summary>
            SPI_GETMOUSESONAR = 4124,
            /// <summary>
            /// Stato della funzionalità Mouse Vanish.
            /// </summary>
            SPI_GETMOUSEVANISH = 4128,
            /// <summary>
            /// Indica se un'applicazione di lettura schermo è in esecuzione.
            /// </summary>
            SPI_GETSCREENREADER = 70,
            /// <summary>
            /// Indica se la funzionalità Mostra suoni è attiva.
            /// </summary>
            SPI_GETSHOWSOUNDS = 56,
            /// <summary>
            /// Informazioni sulla funzionalità SoundSentry.
            /// </summary>
            SPI_GETSOUNDSENTRY = 64,
            /// <summary>
            /// Informazioni sulla funzionalità Tasti permanenti.
            /// </summary>
            SPI_GETSTICKYKEYS = 58,
            /// <summary>
            /// Informazioni sulla funzionalità ToggleKeys.
            /// </summary>
            SPI_GETTOGGLEKEYS = 52,
            #endregion
            #region Set values
            /// <summary>
            /// Imposta il periodo di timeout delle funzionalità di accessibilità.
            /// </summary>
            SPI_SETACCESSTIMEOUT = 61,
            /// <summary>
            /// Attiva o disattiva le descrizioni audio.
            /// </summary>
            SPI_SETAUDIODESCRIPTION = 117,
            /// <summary>
            /// Attiva o disattiva le animazioni dell'area client.
            /// </summary>
            SPI_SETCLIENTAREAANIMATION = 4163,
            /// <summary>
            /// Attiva o disattiva il contenuto sovrapposto.
            /// </summary>
            SPI_SETDISABLEOVERLAPPEDCONTENT = 4161,
            /// <summary>
            /// Imposta i parametri della funzionalità Filtro tasti.
            /// </summary>
            SPI_SETFILTERKEYS = 51,
            /// <summary>
            /// Imposta l'altezza dei bordi superiore e inferiore del rettangolo di focus.
            /// </summary>
            SPI_SETFOCUSBORDERHEIGHT = 8209,
            /// <summary>
            /// Imposta l'altezza dei bordi sinistro e destro del rettangolo di focus.
            /// </summary>
            SPI_SETFOCUSBORDERWIDTH = 8207,
            /// <summary>
            /// Imposta i parametri della funzionalità Alto contrasto.
            /// </summary>
            SPI_SETHIGHCONTRAST = 67,
            /// <summary>
            /// Imposta il tempo di visualizzazione, in secondi, delle notifiche popup.
            /// </summary>
            SPI_SETMESSAGEDURATION = 8215,
            /// <summary>
            /// Attiva o disattiva la funzionalità di Blocco del tasto del mouse.
            /// </summary>
            SPI_SETMOUSECLICKLOCK = 4127,
            /// <summary>
            /// Imposta il tempo, in millisecondi, prima del blocco del tasto del mouse.
            /// </summary>
            SPI_SETMOUSECLICKLOCKTIME = 8201,
            /// <summary>
            /// Imposta i parametri della funzionalità MouseKeys.
            /// </summary>
            SPI_SETMOUSEKEYS = 55,
            /// <summary>
            /// Attiva o disattiva la funzionalità Mouse Sonar.
            /// </summary>
            SPI_SETMOUSESONAR = 4125,
            /// <summary>
            /// Attiva o disattiva la funzionalità Mouse Vanish.
            /// </summary>
            SPI_SETMOUSEVANISH = 4129,
            /// <summary>
            /// Indica se un'applicazione di lettura schermo è in esecuzione.
            /// </summary>
            SPI_SETSCREENREADER = 71,
            /// <summary>
            /// Attiva o disattiva la funzionalità ShowSounds.
            /// </summary>
            SPI_SETSHOWSOUNDS = 57,
            /// <summary>
            /// Imposta i parametri della funzionalità SoundSentry.
            /// </summary>
            SPI_SETSOUNDSENTRY = 65,
            /// <summary>
            /// Imposta i parametri della funzionalità Tasti permanenti.
            /// </summary>
            SPI_SETSTICKYKEYS = 59,
            /// <summary>
            /// Imposta i parametri della funzionalità ToggleKeys.
            /// </summary>
            SPI_SETTOGGLEKEYS = 53
            #endregion
        }

        /// <summary>
        /// Opzioni di aggiornamento profilo utente.
        /// </summary>
        [Flags]
        internal enum UserProfileUpdateOptions : uint
        {
            /// <summary>
            /// Scrive la nuova impostazione nel profilo utente.
            /// </summary>
            SPIF_UPDATEINIFILE = 1,
            /// <summary>
            /// Invia il messaggio WM_SETTINGCHANGE dopo aver aggiornato il profilo utente.
            /// </summary>
            SPIF_SENDWININICHANGE = 2
        }
    }
}