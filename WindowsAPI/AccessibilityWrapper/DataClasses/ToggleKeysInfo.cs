using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità ToggleKeys.
    /// </summary>
    public class ToggleKeysInfo
    {
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
        /// Indica se la funzionalità è attiva.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="ToggleKeysInfo"/>.
        /// </summary>
        /// <param name="ToggleKeysData">Struttura <see cref="TOGGLEKEYS"/> con le informazioni.</param>
        internal ToggleKeysInfo(TOGGLEKEYS ToggleKeysData)
        {
            IsAvailable = ToggleKeysData.Properties.HasFlag(ToggleKeysProperties.TKF_AVAILABLE);
            IsConfirmationDialogEnabled = ToggleKeysData.Properties.HasFlag(ToggleKeysProperties.TKF_CONFIRMHOTKEY);
            IsHotkeyActive = ToggleKeysData.Properties.HasFlag(ToggleKeysProperties.TKF_HOTKEYACTIVE);
            IsHotkeyActivationSoundEnabled = ToggleKeysData.Properties.HasFlag(ToggleKeysProperties.TKF_HOTKEYSOUND);
            IsEnabled = ToggleKeysData.Properties.HasFlag(ToggleKeysProperties.TKF_TOGGLEKEYSON);
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="ToggleKeysInfo"/>.
        /// </summary>
        /// <param name="Available">Indica se la funzionalità è disponibile.</param>
        /// <param name="ConfirmationDialogEnabled">Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità viene attivata tramite hotkey.</param>
        /// <param name="HotkeyActive">Indica se l'hotkey è attiva.</param>
        /// <param name="HotkeyActivationSoundEnabled">Indica se il sistema emette un suono quando la funzionalità viene attivata o disattivata tramite hotkey.</param>
        /// <param name="Enabled">Indica se la funzionalità è attiva.</param>
        public ToggleKeysInfo(bool Available, bool ConfirmationDialogEnabled, bool HotkeyActive, bool HotkeyActivationSoundEnabled, bool Enabled)
        {
            IsAvailable = Available;
            IsConfirmationDialogEnabled = ConfirmationDialogEnabled;
            IsHotkeyActive = HotkeyActive;
            IsHotkeyActivationSoundEnabled = HotkeyActivationSoundEnabled;
            IsEnabled = Enabled;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="TOGGLEKEYS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="TOGGLEKEYS"/> risultato della conversione.</returns>
        internal TOGGLEKEYS ToStruct()
        {
            TOGGLEKEYS Structure = new TOGGLEKEYS()
            {
                Size = (uint)Marshal.SizeOf(typeof(TOGGLEKEYS))
            };
            ToggleKeysProperties Properties = 0;
            if (IsAvailable)
            {
                Properties |= ToggleKeysProperties.TKF_AVAILABLE;
            }
            if (IsConfirmationDialogEnabled)
            {
                Properties |= ToggleKeysProperties.TKF_CONFIRMHOTKEY;
            }
            if (IsHotkeyActive) 
            {
                Properties |= ToggleKeysProperties.TKF_HOTKEYACTIVE;
            }
            if (IsHotkeyActivationSoundEnabled)
            {
                Properties |= ToggleKeysProperties.TKF_HOTKEYSOUND;
            }
            if (IsEnabled)
            {
                Properties |= ToggleKeysProperties.TKF_TOGGLEKEYSON;
            }
            Structure.Properties = Properties;
            return Structure;
        }
    }
}