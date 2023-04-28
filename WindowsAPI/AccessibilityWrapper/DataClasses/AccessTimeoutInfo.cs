using System;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;

namespace WindowsAPI.AccessibilityWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sul timeout delle funzionalità di accessibilità.
    /// </summary>
    public class AccessTimeoutInfo
    {
        /// <summary>
        /// Indica se il timeout è attivo.
        /// </summary>
        public bool IsTimeoutEnabled { get; }

        /// <summary>
        /// Indica se il sistema emette un suono quando il tempo di timeout è scaduto.
        /// </summary>
        public bool IsFeedbackSoundEnabled { get; }

        /// <summary>
        /// Tempo di timeout, in secondi.
        /// </summary>
        public int TimeoutPeriod { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="AccessTimeoutInfo"/>.
        /// </summary>
        /// <param name="TimeoutData">Struttura <see cref="ACCESSTIMEOUT"/> con le informazioni.</param>
        internal AccessTimeoutInfo(ACCESSTIMEOUT TimeoutData) 
        {
            IsTimeoutEnabled = TimeoutData.TimeoutBehaviour.HasFlag(TimeoutBehaviour.ATF_TIMEOUTON);
            IsFeedbackSoundEnabled = TimeoutData.TimeoutBehaviour.HasFlag(TimeoutBehaviour.ATF_ONOFFFEEDBACK);
            TimeoutPeriod = (int)(TimeoutData.TimeoutMilliseconds / 1000);
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="AccessTimeoutInfo"/>.
        /// </summary>
        /// <param name="Enabled">Indica se la funzionalità è abilitata.</param>
        /// <param name="FeedbackEnabled">Indica se il sistema emette un suono quando il tempo di timeout è scaduto.</param>
        /// <param name="Timeout">Tempo di timeout, in secondi.</param>
        /// <exception cref="ArgumentException"></exception>
        public AccessTimeoutInfo(bool Enabled, bool FeedbackEnabled, int Timeout)
        {
            IsTimeoutEnabled = Enabled;
            IsFeedbackSoundEnabled = FeedbackEnabled;
            if (Timeout > 0)
            {
                TimeoutPeriod = Timeout;
            }
            else
            {
                throw new ArgumentException("The timeout value is invalid.", nameof(Timeout));
            }
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="ACCESSTIMEOUT"/>.
        /// </summary>
        /// <returns>Struttura <see cref="ACCESSTIMEOUT"/> risultato della conversione.</returns>
        internal ACCESSTIMEOUT ToStruct()
        {
            TimeoutBehaviour NewBehaviour = 0;
            if (IsTimeoutEnabled)
            {
                NewBehaviour |= TimeoutBehaviour.ATF_TIMEOUTON;
            }
            if (IsFeedbackSoundEnabled)
            {
                NewBehaviour |= TimeoutBehaviour.ATF_ONOFFFEEDBACK;
            }
            ACCESSTIMEOUT Structure = new ACCESSTIMEOUT()
            {
                Size = (uint)Marshal.SizeOf(typeof(ACCESSTIMEOUT)),
                TimeoutBehaviour = NewBehaviour,
                TimeoutMilliseconds = (uint)(TimeoutPeriod * 1000)
            };
            return Structure;
        }
    }
}