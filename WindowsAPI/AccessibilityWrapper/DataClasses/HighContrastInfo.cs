using System;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità Alto contrasto.
    /// </summary>
    public class HighContrastInfo
    {
        /// <summary>
        /// Indica se la funzionalità è disponibile.
        /// </summary>
        public bool IsAvailable { get; }

        /// <summary>
        /// Indica se la funzionalità è abilitata.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Indica se l'hotkey è attiva.
        /// </summary>
        public bool IsHotkeyActive { get; }

        /// <summary>
        /// Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità viene attivata tramite hotkey.
        /// </summary>
        public bool IsConfirmationDialogEnabled { get; }

        /// <summary>
        /// Indica se il computer emette un suono quando la funzionalità viene attivata tramite hotkey.
        /// </summary>
        public bool IsHotkeyActivationSoundEnabled { get; }

        /// <summary>
        /// Indica se un indicatore visuale viene visualizzato quando la funzionalità è attiva.
        /// </summary>
        public bool IsVisualIndicatorEnabled { get; }

        /// <summary>
        /// Indica se l'hotkey è disponibile.
        /// </summary>
        public bool IsHotkeyAvailable { get; }

        /// <summary>
        /// Nome dello schema colori predefinito che verrà impostato.
        /// </summary>
        public string DefaultColorSchemeName { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="HighContrastInfo"/>.
        /// </summary>
        /// <param name="HighContrastData">Struttura <see cref="HIGHCONTRAST"/> con le informazioni.</param>
        internal HighContrastInfo(HIGHCONTRAST HighContrastData)
        {
            IsAvailable = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_AVAILABLE);
            IsEnabled = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_HIGHCONTRASTON);
            IsHotkeyActive = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_HOTKEYACTIVE);
            IsConfirmationDialogEnabled = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_CONFIRMHOTKEY);
            IsHotkeyActivationSoundEnabled = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_HOTKEYSOUND);
            IsVisualIndicatorEnabled = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_INDICATOR);
            IsHotkeyAvailable = HighContrastData.Properties.HasFlag(HighContrastProperties.HCF_HOTKEYAVAILABLE);
            DefaultColorSchemeName = HighContrastData.DefaultScheme;
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="HighContrastInfo"/>.
        /// </summary>
        /// <param name="Available">Indica se la funzionalità è disponibile.</param>
        /// <param name="Enabled">Indica se la funzionalità è attiva.</param>
        /// <param name="HotkeyActive">Indica se l'hotkey è attiva.</param>
        /// <param name="ConfirmationDialogEnabled">Indica se viene visualizzata una finestra di dialogo di conferma quando la funzionalità viene attivata tramite hotkey.</param>
        /// <param name="HotkeyActivationSoundEnabled">Indica se il computer emette un suono quando la funzionalità viene attivata tramite hotkey.</param>
        /// <param name="VisualIndicatorEnabled">Indica se un indicatore visuale viene visualizzato quando la funzionalità è attiva.</param>
        /// <param name="HotkeyAvailable">Indica se l'hotkey è disponibile.</param>
        /// <param name="DefaultColorScheme">Nome dello schema colori predefinito che verrà impostato.</param>
        public HighContrastInfo(bool Available, bool Enabled, bool HotkeyActive, bool ConfirmationDialogEnabled, bool HotkeyActivationSoundEnabled, bool VisualIndicatorEnabled, bool HotkeyAvailable, string DefaultColorScheme)
        {
            IsAvailable = Available;
            IsEnabled = Enabled;
            IsHotkeyActive = HotkeyActive;
            IsConfirmationDialogEnabled = ConfirmationDialogEnabled;
            IsHotkeyActivationSoundEnabled = HotkeyActivationSoundEnabled;
            IsVisualIndicatorEnabled = VisualIndicatorEnabled;
            IsHotkeyAvailable = HotkeyAvailable;
            DefaultColorSchemeName = DefaultColorScheme ?? throw new ArgumentNullException(nameof(DefaultColorScheme), "No color scheme.");
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="HIGHCONTRAST"/>.
        /// </summary>
        /// <param name="NoThemeChangeEffects">Indica se il cambio delle impostazioni deve causare effetti di cambio tema.</param>
        /// <returns>Struttura <see cref="HIGHCONTRAST"/> risultato della conversione.</returns>
        internal HIGHCONTRAST ToStruct(bool NoThemeChangeEffects)
        {
            HIGHCONTRAST Structure = new HIGHCONTRAST()
            {
                Size = (uint)Marshal.SizeOf(typeof(HIGHCONTRAST)),
                DefaultScheme = DefaultColorSchemeName
            };
            HighContrastProperties Properties = 0;
            if (IsEnabled)
            {
                Properties |= HighContrastProperties.HCF_HIGHCONTRASTON;
            }
            if (IsAvailable)
            {
                Properties |= HighContrastProperties.HCF_AVAILABLE;
            }
            if (IsHotkeyActive)
            {
                Properties |= HighContrastProperties.HCF_HOTKEYACTIVE;
            }
            if (IsConfirmationDialogEnabled)
            {
                Properties |= HighContrastProperties.HCF_CONFIRMHOTKEY;
            }
            if (IsHotkeyActivationSoundEnabled)
            {
                Properties |= HighContrastProperties.HCF_HOTKEYSOUND;
            }
            if (IsVisualIndicatorEnabled)
            {
                Properties |= HighContrastProperties.HCF_INDICATOR;
            }
            if (IsHotkeyAvailable)
            {
                Properties |= HighContrastProperties.HCF_HOTKEYAVAILABLE;
            }
            if (NoThemeChangeEffects)
            {
                Properties |= HighContrastProperties.HCF_OPTION_NOTHEMECHANGE;
            }
            Structure.Properties = Properties;
            return Structure;
        }
    }
}