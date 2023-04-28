using System;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità Filtro tasti.
    /// </summary>
    public class FilterKeysInfo
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
        /// Indica se il computer genera un suono quando un tasto viene premuto o quando la pressione viene accettata.
        /// </summary>
        public bool PressedAcceptedClickSound { get; }

        /// <summary>
        /// Indica se una finestra di dialogo viene visualizzata quando la funzionalità viene attivata tramite hotkey.
        /// </summary>
        public bool ConfirmationOnHotkey { get; }

        /// <summary>
        /// Indica se l'hotkey è attiva.
        /// </summary>
        public bool IsHotkeyActive { get; }

        /// <summary>
        /// Indica se viene generato un suono quando la funzionalità viene attivata o disattivata tramite hotkey.
        /// </summary>
        public bool IsHotkeyActivationSoundEnabled { get; }

        /// <summary>
        /// Indica se viene visualizzato un indicatore visuale quando la funzionalità è attiva.
        /// </summary>
        public bool IsVisualIndicatorEnabled { get; }

        /// <summary>
        /// Tempo, in secondi, che l'utente deve tenere premuto un tasto perché il computer ne accetti la pressione.
        /// </summary>
        public int KeyDownTime { get; }

        /// <summary>
        /// Tempo, in secondi, che l'utente deve tenere premuto un tasto prima che si ripeta.
        /// </summary>
        public int RepeatDelay { get; }

        /// <summary>
        /// Tempo, in secondi, tra le ripetizioni di un tasto.
        /// </summary>
        public int RepeatTime { get; }

        /// <summary>
        /// Tempo, in secondi, che deve trascorrere dopo il rilascio di un tasto prima che il computer ne accetti un'altra pressione.
        /// </summary>
        public int AcceptDelay { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="FilterKeysInfo"/>.
        /// </summary>
        /// <param name="FilterKeysData">Struttura <see cref="FILTERKEYS"/> con le informazioni.</param>
        internal FilterKeysInfo(FILTERKEYS FilterKeysData)
        {
            IsAvailable = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_AVAILABLE);
            IsEnabled = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_FILTERKEYSON);
            PressedAcceptedClickSound = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_CLICKON);
            ConfirmationOnHotkey = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_CONFIRMHOTKEY);
            IsHotkeyActive = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_HOTKEYACTIVE);
            IsHotkeyActivationSoundEnabled = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_HOTKEYSOUND);
            IsVisualIndicatorEnabled = FilterKeysData.Properties.HasFlag(FilterKeysProperties.FKF_INDICATOR);
            KeyDownTime = (int)(FilterKeysData.KeyDownAcceptMilliseconds / 1000);
            RepeatDelay = (int)(FilterKeysData.KeyDownRepeatMilliseconds / 1000);
            RepeatTime = (int)(FilterKeysData.KeyRepetitionPeriodMilliseconds / 1000);
            AcceptDelay = (int)(FilterKeysData.KeyReleasePeriodMilliseconds / 1000);
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="FilterKeysInfo"/>.
        /// </summary>
        /// <param name="Available">Indica se la funzionalità è disponibile.</param>
        /// <param name="Enabled">Indica se la funzionalità è abilitata.</param>
        /// <param name="ClickSound">Indica se il computer genera un suono quando un tasto viene premuto o quando la pressione viene accettata.</param>
        /// <param name="ConfirmationDialogEnabled">Indica se una finestra di dialogo viene visualizzata quando la funzionalità viene attivata tramite hotkey.</param>
        /// <param name="HotkeyActive">Indica se l'hotkey è attiva.</param>
        /// <param name="HotkeyActivationSound">Indica se viene generato un suono quando la funzionalità viene attivata o disattivata tramite hotkey.</param>
        /// <param name="VisualIndicatorEnabled">Indica se viene visualizzato un indicatore visuale quando la funzionalità è attiva.</param>
        /// <param name="KeyDownTime">Tempo, in secondi, che l'utente deve tenere premuto un tasto perché il computer ne accetti la pressione.</param>
        /// <param name="RepeatDelay">Tempo, in secondi, che l'utente deve tenere premuto un tasto prima che si ripeta.</param>
        /// <param name="RepeatTime">Tempo, in secondi, tra le ripetizioni di un tasto.</param>
        /// <param name="AcceptDelay">Tempo, in secondi, che deve trascorrere dopo il rilascio di un tasto prima che il computer ne accetti un'altra pressione.</param>
        /// <remarks>Se <paramref name="AcceptDelay"/> è maggiore di zero, <paramref name="RepeatDelay"/>, <paramref name="RepeatTime"/> e <paramref name="KeyDownTime"/> devono essere 0.<br/><br/>
        /// Se <paramref name="AcceptDelay"/> è zero, <paramref name="RepeatDelay"/>, <paramref name="RepeatTime"/> e <paramref name="KeyDownTime"/> devono essere maggiori di 0.<br/><br/>
        /// Il valore massimo per <paramref name="AcceptDelay"/>, <paramref name="KeyDownTime"/>, <paramref name="RepeatDelay"/> e <paramref name="RepeatTime"/> è 20 secondi.</remarks>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public FilterKeysInfo(bool Available, bool Enabled, bool ClickSound, bool ConfirmationDialogEnabled, bool HotkeyActive, bool HotkeyActivationSound, bool VisualIndicatorEnabled, int KeyDownTime, int RepeatDelay, int RepeatTime, int AcceptDelay)
        {
            IsAvailable = Available;
            IsEnabled = Enabled;
            PressedAcceptedClickSound = ClickSound;
            ConfirmationOnHotkey = ConfirmationDialogEnabled;
            IsHotkeyActive = HotkeyActive;
            IsHotkeyActivationSoundEnabled = HotkeyActivationSound;
            IsVisualIndicatorEnabled = VisualIndicatorEnabled;
            if (AcceptDelay > 0 && (RepeatDelay > 0 || RepeatTime > 0 || KeyDownTime > 0))
            {
                throw new ArgumentException("Invalid values.");
            }
            if (AcceptDelay is 0 && (RepeatDelay is 0 || RepeatTime is 0 || KeyDownTime is 0))
            {
                throw new ArgumentException("Invalid values.");
            }
            if (AcceptDelay > 20 || RepeatDelay > 20 || RepeatTime > 20 || KeyDownTime > 20)
            {
                throw new ArgumentOutOfRangeException(string.Empty, "Invalid values.");
            }
            this.KeyDownTime = KeyDownTime;
            this.RepeatDelay = RepeatDelay;
            this.RepeatTime = RepeatTime;
            this.AcceptDelay = AcceptDelay;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="FILTERKEYS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="FILTERKEYS"/> risultato della conversione.</returns>
        internal FILTERKEYS ToStruct()
        {
            FILTERKEYS Structure = new FILTERKEYS
            {
                Size = (uint)Marshal.SizeOf(typeof(FILTERKEYS))
            };
            FilterKeysProperties Properties = 0;
            if (IsAvailable)
            {
                Properties |= FilterKeysProperties.FKF_AVAILABLE;
            }
            if (PressedAcceptedClickSound)
            {
                Properties |= FilterKeysProperties.FKF_CLICKON;
            }
            if (ConfirmationOnHotkey)
            {
                Properties |= FilterKeysProperties.FKF_CONFIRMHOTKEY;
            }
            if (IsEnabled)
            {
                Properties |= FilterKeysProperties.FKF_FILTERKEYSON;
            }
            if (IsHotkeyActive)
            {
                Properties |= FilterKeysProperties.FKF_HOTKEYACTIVE;
            }
            if (IsHotkeyActivationSoundEnabled)
            {
                Properties |= FilterKeysProperties.FKF_HOTKEYSOUND;
            }
            if (IsVisualIndicatorEnabled)
            {
                Properties |= FilterKeysProperties.FKF_INDICATOR;
            }
            Structure.Properties = Properties;
            Structure.KeyDownAcceptMilliseconds = (uint)(KeyDownTime * 1000);
            Structure.KeyDownRepeatMilliseconds = (uint)(RepeatDelay * 1000);
            Structure.KeyRepetitionPeriodMilliseconds = (uint)(RepeatTime * 1000);
            Structure.KeyReleasePeriodMilliseconds = (uint)(AcceptDelay * 1000);
            return Structure;
        }
    }
}