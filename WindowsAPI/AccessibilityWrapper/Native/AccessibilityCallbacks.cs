using System;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;

namespace WindowsAPI.AccessibilityWrapper.Native
{
    /// <summary>
    /// Callback accessibilità.
    /// </summary>
    internal static class AccessibilityCallbacks
    {
        /// <summary>
        /// Callback che produce un segnale visuale personalizzato quando la funzionalità SoundSentry è attiva e un'applicazione basata su Windows (o in esecuzione in una finestra) genera un suono.
        /// </summary>
        /// <param name="Duration">Durata, in millisecondi, del segnale visuale.</param>
        /// <param name="Effect">Segnale visuale da visualizzare, deve essere <see cref="WindowAppVisualSignal.SSWF_CUSTOM"/>.</param>
        /// <returns>Se il segnale visuale è stato o sarà visulizzato correttamente il valore restituito sarà TRUE, se il segnale è asincrono e lo stato non è disponibile al momento della chiamata dovrebbe restituire TRUE.<br/>
        /// Se si è verificato un errore il valore restituito sarà FALSE.</returns>
        internal delegate IntPtr SoundSentryProc(uint Duration, WindowAppVisualSignal Effect);
    }
}