using System.Globalization;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulla funzionalità Descrizioni audio.
    /// </summary>
    public class AudioDescriptionInfo
    {
        /// <summary>
        /// Indica se la funzionalità è abilitata.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// Lingua delle descrizioni audio.
        /// </summary>
        /// <remarks>Questa proprietà è nulla se non è stato possibile trovare un oggetto <see cref="CultureInfo"/> che descrive la lingua.</remarks>
        public CultureInfo Locale { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="AudioDescriptionInfo"/>.
        /// </summary>
        /// <param name="AudioDescriptionData">Struttura <see cref="AUDIODESCRIPTION"/> con le informazioni.</param>
        internal AudioDescriptionInfo(AUDIODESCRIPTION AudioDescriptionData)
        {
            IsEnabled = AudioDescriptionData.Enabled;
            try
            {
                Locale = CultureInfo.GetCultureInfo((int)AudioDescriptionData.LocaleID);
            }
            catch (CultureNotFoundException)
            {
                Locale = null;
            }
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="AudioDescriptionInfo"/>.
        /// </summary>
        /// <param name="Enabled">Indica se la funzionalità è abilitata.</param>
        /// <param name="Locale">Oggetto <see cref="CultureInfo"/> che descrive la lingua delle descrizioni audio.</param>
        public AudioDescriptionInfo(bool Enabled, CultureInfo Locale)
        {
            IsEnabled = Enabled;
            this.Locale = Locale;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="AUDIODESCRIPTION"/>.
        /// </summary>
        /// <returns>Struttura <see cref="AUDIODESCRIPTION"/> risultato della conversione.</returns>
        internal AUDIODESCRIPTION ToStruct()
        {
            AUDIODESCRIPTION Structure = new AUDIODESCRIPTION()
            {
                Size = (uint)Marshal.SizeOf(typeof(AUDIODESCRIPTION)),
                Enabled = IsEnabled,
                LocaleID = Locale != null ? (uint)Locale.LCID : (uint)CultureInfo.CurrentCulture.LCID
            };
            return Structure;
        }
    }
}