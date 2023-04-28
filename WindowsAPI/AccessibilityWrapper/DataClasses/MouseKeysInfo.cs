using System;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità MouseKeys.
    /// </summary>
    public class MouseKeysInfo
    {
        /// <summary>
        /// Indica se la funzionalità è disponibile.
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        /// Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità è attivata tramite hotkey.
        /// </summary>
        public bool IsConfirmationDialogEnabled { get; }

        /// <summary>
        /// Indica se l'hotkey è attiva.
        /// </summary>
        public bool IsHotkeyActive { get; }

        /// <summary>
        /// Indica se il computer deve emettere un suono quando la funzionalità viene attivata o disattivata tramite hotkey.
        /// </summary>
        public bool IsHotkeyActivationSoundEnabled { get; }

        /// <summary>
        /// Indica se viene visualizzato un indicatore visuale quando la funzionalità è attiva.
        /// </summary>
        public bool IsVisualIndicatorEnabled { get; }

        /// <summary>
        /// Il pulsante sinistro del mouse è in stato "premuto".
        /// </summary>
        public bool LeftButtonDown { get; }

        /// <summary>
        /// Indica se il pulsante sinistro è selezionato per le azioni del mouse.
        /// </summary>
        public bool IsLeftButtonSelected { get; }

        /// <summary>
        /// Indica se i tasti CTRL e SHIFT possono alterare la velocità del mouse.
        /// </summary>
        /// <remarks>Il tasto CTRL aumenta la velocità del cursore mentre SHIFT introduce un piccolo ritardo nel movimento dopo ogni singolo pixel.</remarks>
        public bool ModifiersEnabled { get; }

        /// <summary>
        /// Indica se la funzionalità è attiva.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Indica se l'input del tastierino numerico viene elaborato come comandi del mouse.
        /// </summary>
        public bool NumericKeypadAsMouseCommands { get; }

        /// <summary>
        /// Indica se il NUM LOCK deve essere attivo perché il tastierino numerico possa muovere il mouse.
        /// </summary>
        public bool NumLock { get; }

        /// <summary>
        /// Indica se il tasto destro del mouse è in stato "premuto".
        /// </summary>
        public bool RightButtonDown { get; }

        /// <summary>
        /// Indica se il tasto destro è stato selezionato per le azioni del mouse.
        /// </summary>
        public bool IsRightButtonSelected { get; }

        /// <summary>
        /// Velocità massima del cursore quando una freccia direzionale viene tenuta premuta.
        /// </summary>
        public int CursorMaxSpeed { get; }

        /// <summary>
        /// Tempo, in secondi, richiesto al cursore del mouse per raggiungere la velocità massima.
        /// </summary>
        public int CursorTimeToMaxSpeed { get; }

        /// <summary>
        /// Moltiplicatore applicato alla velocità del mouse quando l'utente tiene premuto il tasto CTRL.
        /// </summary>
        public int CtrlMultiplier { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="MouseKeysInfo"/>.
        /// </summary>
        /// <param name="MouseKeysData">Struttura <see cref="MOUSEKEYS"/> con le informazioni.</param>
        internal MouseKeysInfo(MOUSEKEYS MouseKeysData)
        {
            IsAvailable = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_AVAILABLE);
            IsConfirmationDialogEnabled = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_CONFIRMHOTKEY);
            IsHotkeyActive = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_HOTKEYACTIVE);
            IsHotkeyActivationSoundEnabled = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_HOTKEYSOUND);
            IsVisualIndicatorEnabled = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_INDICATOR);
            LeftButtonDown = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_LEFTBUTTONDOWN);
            IsLeftButtonSelected = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_LEFTBUTTONSEL);
            ModifiersEnabled = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_MODIFIERS);
            IsEnabled = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_MOUSEKEYSON);
            NumericKeypadAsMouseCommands = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_MOUSEMODE);
            NumLock = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_REPLACENUMBERS);
            RightButtonDown = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_RIGHTBUTTONDOWN);
            IsRightButtonSelected = MouseKeysData.Properties.HasFlag(MouseKeysProperties.MKF_RIGHTBUTTONSEL);
            CursorMaxSpeed = (int)MouseKeysData.ArrowKeyMaxCursorSpeed;
            CursorTimeToMaxSpeed = (int)MouseKeysData.TimeToMaxSpeed;
            CtrlMultiplier = (int)MouseKeysData.CtrlMultiplier;
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="MouseKeysInfo"/>.
        /// </summary>
        /// <param name="Available">Indica se la funzionalità è disponibile.</param>
        /// <param name="ConfirmationDialogEnabled">Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità è attivata tramite hotkey.</param>
        /// <param name="HotkeyActive">Indica se l'hotkey è attiva.</param>
        /// <param name="HotkeyActivationSoundEnabled">Indica se il computer deve emettere un suono quando la funzionalità viene attivata o disattivata tramite hotkey.</param>
        /// <param name="VisualIndicatorEnabled">Indica se viene visualizzato un indicatore visuale quando la funzionalità è attiva.</param>
        /// <param name="ModifiersEnabled">Indica se i tasti CTRL e SHIFT possono alterare la velocità del mouse.</param>
        /// <param name="Enabled">Indica se la funzionalità è attiva.</param>
        /// <param name="NumLock">Indica se il NUM LOCK deve essere attivo perché il tastierino numerico possa muovere il mouse.</param>
        /// <param name="MaxSpeed">Velocità massima del cursore quando una freccia direzionale viene tenuta premuta.</param>
        /// <param name="TimeToMaxSpeed">Tempo, in secondi, richiesto al cursore del mouse per raggiungere la velocità massima.</param>
        /// <param name="CtrlSpeed">Moltiplicatore applicato alla velocità del mouse quando l'utente tiene premuto il tasto CTRL.</param>
        public MouseKeysInfo(bool Available, bool ConfirmationDialogEnabled, bool HotkeyActive, bool HotkeyActivationSoundEnabled, bool VisualIndicatorEnabled, bool ModifiersEnabled, bool Enabled, bool NumLock, int MaxSpeed, int TimeToMaxSpeed, int CtrlSpeed)
        {
            IsAvailable = Available;
            IsConfirmationDialogEnabled = ConfirmationDialogEnabled;
            IsHotkeyActive = HotkeyActive;
            IsHotkeyActivationSoundEnabled = HotkeyActivationSoundEnabled;
            IsVisualIndicatorEnabled = VisualIndicatorEnabled;
            this.ModifiersEnabled = ModifiersEnabled;
            IsEnabled = Enabled;
            this.NumLock = NumLock;
            CursorMaxSpeed = MaxSpeed;
            if (TimeToMaxSpeed >= 10 && TimeToMaxSpeed <= 50)
            {
                CursorTimeToMaxSpeed = TimeToMaxSpeed;
            }
            else
            {
                throw new ArgumentException("The time to max speed must be between 10 and 50 included.");
            }
            CtrlMultiplier = CtrlSpeed;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="MOUSEKEYS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="MOUSEKEYS"/> risultato della conversione.</returns>
        internal MOUSEKEYS ToStruct()
        {
            MOUSEKEYS Structure = new MOUSEKEYS()
            {
                Size = (uint)(Marshal.SizeOf(typeof(MOUSEKEYS))),
                ArrowKeyMaxCursorSpeed = (uint)CursorMaxSpeed,
                TimeToMaxSpeed = (uint)CursorTimeToMaxSpeed,
                CtrlMultiplier = (uint)this.CtrlMultiplier
            };
            MouseKeysProperties Properties = 0;
            if (IsAvailable)
            {
                Properties |= MouseKeysProperties.MKF_AVAILABLE;
            }
            if (IsConfirmationDialogEnabled)
            {
                Properties |= MouseKeysProperties.MKF_CONFIRMHOTKEY;
            }
            if (IsHotkeyActive)
            {
                Properties |= MouseKeysProperties.MKF_HOTKEYACTIVE;
            }
            if (IsHotkeyActivationSoundEnabled)
            {
                Properties |= MouseKeysProperties.MKF_HOTKEYSOUND;
            }
            if (IsVisualIndicatorEnabled)
            {
                Properties |= MouseKeysProperties.MKF_INDICATOR;
            }
            if (ModifiersEnabled)
            {
                Properties |= MouseKeysProperties.MKF_MODIFIERS;
            }
            if (IsEnabled)
            {
                Properties |= MouseKeysProperties.MKF_MOUSEKEYSON;
            }
            if (NumLock)
            {
                Properties |= MouseKeysProperties.MKF_REPLACENUMBERS;
            }
            Structure.Properties = Properties;
            return Structure;
        }
    }
}