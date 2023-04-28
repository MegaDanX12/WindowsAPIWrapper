using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità Tasti permanenti.
    /// </summary>
    public class StickyKeysInfo
    {
        /// <summary>
        /// Il sistema emette un suono quando l'utente aggancia, blocca o rilascia i tasti modificatori usando la funzionalità.
        /// </summary>
        public bool IsAudioFeedbackEnabled { get; }

        /// <summary>
        /// Indica se la funzionalità è disponibile.
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        /// Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità viene attivata tramite hotkey.
        /// </summary>
        public bool IsConfirmationDialogEnabled { get; }

        /// <summary>
        /// Indica se l'hotkey è attiva.
        /// </summary>
        public bool IsHotkeyActive { get; }

        /// <summary>
        /// Indica se il sistema emette un suono quando la funzionalità viene attivata o disattivata tramite hotkey.
        /// </summary>
        public bool IsHotkeyActivationSoundEnabled { get; }

        /// <summary>
        /// Indica se viene visualizzato un indicatore visuale quando la funzionalità è attiva.
        /// </summary>
        public bool IsVisualIndicatorEnabled { get; }

        /// <summary>
        /// Indica se la funzionalità è attiva.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Indica se premere un tasto modificatore due volte di fila blocca il tasto fino a una terza pressione.
        /// </summary>
        public bool ModifierDoublePressLock { get; }

        /// <summary>
        /// Indica se rilasciare un tasto modificatore, premuto in combinazione con qualunque altro tasto, disattiva la funzionalità.
        /// </summary>
        public bool ModifierReleaseDisable { get; }
        #region Key Status Properties
        /// <summary>
        /// Alt sinistro agganciato.
        /// </summary>
        public bool LAltLatched { get; }

        /// <summary>
        /// Ctrl sinistro aggancicato.
        /// </summary>
        public bool LCtrlLatched { get; }

        /// <summary>
        /// Shift sinistro agganciato.
        /// </summary>
        public bool LShiftLatched { get; }

        /// <summary>
        /// Alt destro agganciato.
        /// </summary>
        public bool RAltLatched { get; }

        /// <summary>
        /// Ctrl destro agganciato.
        /// </summary>
        public bool RCtrlLatched { get; }

        /// <summary>
        /// Shift destro agganciato.
        /// </summary>
        public bool RShiftLatched { get; }

        /// <summary>
        /// Alt sinistro bloccato.
        /// </summary>
        public bool LAltLocked { get; }

        /// <summary>
        /// Ctrl sinistro bloccato.
        /// </summary>
        public bool LCtrlLocked { get; }

        /// <summary>
        /// Shift sinistro bloccato.
        /// </summary>
        public bool LShiftLocked { get; }

        /// <summary>
        /// Alt destro bloccato.
        /// </summary>
        public bool RAltLocked { get; }

        /// <summary>
        /// Ctrl destro bloccato.
        /// </summary>
        public bool RCtrlLocked { get; }

        /// <summary>
        /// Shift destro bloccato.
        /// </summary>
        public bool RShiftLocked { get; }

        /// <summary>
        /// Tasto Windows sinistro agganciato.
        /// </summary>
        public bool LWinLatched { get; }

        /// <summary>
        /// Tasto Windows destro agganciato.
        /// </summary>
        public bool RWinLatched { get; }

        /// <summary>
        /// Tasto Windows sinistro bloccato.
        /// </summary>
        public bool LWinLocked { get; }

        /// <summary>
        /// Tasto Windows destro bloccato.
        /// </summary>
        public bool RWinLocked { get; }
        #endregion

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="StickyKeysInfo"/>.
        /// </summary>
        /// <param name="StickyKeysData">Struttura <see cref="STICKYKEYS"/> con le informazioni.</param>
        internal StickyKeysInfo(STICKYKEYS StickyKeysData)
        {
            IsAudioFeedbackEnabled = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_AUDIBLEFEEDBACK);
            IsAvailable = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_AVAILABLE);
            IsConfirmationDialogEnabled = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_CONFIRMHOTKEY);
            IsHotkeyActive = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_HOTKEYACTIVE);
            IsHotkeyActivationSoundEnabled = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_HOTKEYSOUND);
            IsVisualIndicatorEnabled = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_INDICATOR);
            IsEnabled = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_STICKYKEYSON);
            ModifierDoublePressLock = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_TRISTATE);
            ModifierReleaseDisable = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_TWOKEYSOFF);
            LAltLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LALTLATCHED);
            LCtrlLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LCTLLATCHED);
            LShiftLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LSHIFTLATCHED);
            RAltLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RALTLATCHED);
            RCtrlLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RCTLLATCHED);
            RShiftLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RSHIFTLATCHED);
            LAltLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LALTLOCKED);
            LCtrlLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LCTLLOCKED);
            LShiftLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LSHIFTLOCKED);
            RAltLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RALTLOCKED);
            RCtrlLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RCTLLOCKED);
            RShiftLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RSHIFTLOCKED);
            LWinLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LWINLATCHED);
            RWinLatched = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RWINLATCHED);
            LWinLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_LWINLOCKED);
            RWinLocked = StickyKeysData.Properties.HasFlag(StickyKeysProperties.SKF_RWINLOCKED);
        }

        /// <summary>
        /// Inizializza una nuova istanza  di <see cref="StickyKeysInfo"/>.
        /// </summary>
        /// <param name="AudioFeedbackEnabled">Il sistema emette un suono quando l'utente aggancia, blocca o rilascia i tasti modificatori usando la funzionalità.</param>
        /// <param name="Available">Indica se la funzionalità è disponibile.</param>
        /// <param name="ConfirmationDialogEnabled">Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità viene attivata tramite hotkey.</param>
        /// <param name="HotkeyActive">Indica se l'hotkey è attiva.</param>
        /// <param name="HotkeyActivationSound">Indica se il sistema emette un suono quando la funzionalità viene attivata o disattivata tramite hotkey.</param>
        /// <param name="VisualIndicatorEnabled">Indica se viene visualizzato un indicatore visuale quando la funzionalità è attiva.</param>
        /// <param name="Enabled">Indica se la funzionalità è attiva.</param>
        /// <param name="ModifierDoublePressLock">Indica se premere un tasto modificatore due volte di fila blocca il tasto fino a una ternza pressione.</param>
        /// <param name="ModifierReleaseDisable">Indica se rilasciare un tasto modificatore, premuto in combinazione con qualunque altro tasto, disattiva la funzionalità.</param>
        public StickyKeysInfo(bool AudioFeedbackEnabled, bool Available, bool ConfirmationDialogEnabled, bool HotkeyActive, bool HotkeyActivationSound, bool VisualIndicatorEnabled, bool Enabled, bool ModifierDoublePressLock, bool ModifierReleaseDisable)
        {
            IsAudioFeedbackEnabled = AudioFeedbackEnabled;
            IsAvailable = Available;
            IsConfirmationDialogEnabled = ConfirmationDialogEnabled;
            IsHotkeyActive = HotkeyActive;
            IsHotkeyActivationSoundEnabled = HotkeyActivationSound;
            IsVisualIndicatorEnabled = VisualIndicatorEnabled;
            IsEnabled = Enabled;
            this.ModifierDoublePressLock = ModifierDoublePressLock;
            this.ModifierReleaseDisable = ModifierReleaseDisable;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="STICKYKEYS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="STICKYKEYS"/> risultato della conversione.</returns>
        internal STICKYKEYS ToStruct()
        {
            STICKYKEYS Structure = new STICKYKEYS()
            {
                Size = (uint)Marshal.SizeOf(typeof(STICKYKEYS))
            };
            StickyKeysProperties Properties = 0;
            if (IsAudioFeedbackEnabled)
            {
                Properties |= StickyKeysProperties.SKF_AUDIBLEFEEDBACK;
            }
            if (IsAvailable)
            {
                Properties |= StickyKeysProperties.SKF_AVAILABLE;
            }
            if (IsConfirmationDialogEnabled)
            {
                Properties |= StickyKeysProperties.SKF_CONFIRMHOTKEY;
            }
            if (IsHotkeyActive)
            {
                Properties |= StickyKeysProperties.SKF_HOTKEYACTIVE;
            }
            if (IsHotkeyActivationSoundEnabled)
            {
                Properties |= StickyKeysProperties.SKF_HOTKEYSOUND;
            }
            if (IsVisualIndicatorEnabled)
            {
                Properties |= StickyKeysProperties.SKF_INDICATOR;
            }
            if (IsEnabled)
            {
                Properties |= StickyKeysProperties.SKF_STICKYKEYSON;
            }
            if (ModifierDoublePressLock)
            {
                Properties |= StickyKeysProperties.SKF_TRISTATE;
            }
            if (ModifierReleaseDisable)
            {
                Properties |= StickyKeysProperties.SKF_TWOKEYSOFF;
            }
            Structure.Properties = Properties;
            return Structure;
        }
    }
}