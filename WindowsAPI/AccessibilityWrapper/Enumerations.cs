using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;

namespace WindowsAPI.AccessibilityWrapper
{
    /// <summary>
    /// Enumerazioni accessibilità.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Tipo puntatore.
        /// </summary>
        public enum PointerType
        {
            /// <summary>
            /// Tutti i puntatori.
            /// </summary>
            All = POINTER_INPUT_TYPE.PT_POINTER,
            /// <summary>
            /// Tocco.
            /// </summary>
            Touch = POINTER_INPUT_TYPE.PT_TOUCH,
            /// <summary>
            /// Penna.
            /// </summary>
            Pen = POINTER_INPUT_TYPE.PT_PEN,
            /// <summary>
            /// Mouse.
            /// </summary>
            Mouse = POINTER_INPUT_TYPE.PT_MOUSE,
            /// <summary>
            /// Touchpad.
            /// </summary>
            Touchpad = POINTER_INPUT_TYPE.PT_TOUCHPAD
        }

        /// <summary>
        /// Segnale visuale presentato quando un'applicazione in modalità testo genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public enum TextApplicationVisualSignal : uint
        {
            /// <summary>
            /// Il bordo dello schermo lampeggia.
            /// </summary>
            FlashBorder = TextAppVisualSignal.SSTF_BORDER,
            /// <summary>
            /// I caratteri a bordo schermo lampeggiano.
            /// </summary>
            FlashChars = TextAppVisualSignal.SSTF_CHARS,
            /// <summary>
            /// Il display lampeggia.
            /// </summary>
            FlashDisplay = TextAppVisualSignal.SSTF_DISPLAY,
            /// <summary>
            /// Nessun segnale visuale.
            /// </summary>
            None = TextAppVisualSignal.SSTF_NONE
        }

        /// <summary>
        /// Segnale visuale presentato quando un'applicazione in modalità grafica genera un suono mentre è in esecuzione in una macchina virtuale a schermo intero.
        /// </summary>
        public enum GraphicsApplicationVisualSignal : uint
        {
            /// <summary>
            /// Il display lampeggia.
            /// </summary>
            FlashDisplay = VirtualGraphicsAppVisualSignal.SSGF_DISPLAY,
            /// <summary>
            /// Nessun segnale visuale.
            /// </summary>
            None = VirtualGraphicsAppVisualSignal.SSGF_NONE
        }

        /// <summary>
        /// Segnale visuale presentato quando un'applicazione basata su Windows oppure un'applicazione MS-DOS in esecuzione in una finestra.
        /// </summary>
        public enum WindowApplicationVisualSignal : uint
        {
            /// <summary>
            /// Segnale personalizzato.
            /// </summary>
            Custom = WindowAppVisualSignal.SSWF_CUSTOM,
            /// <summary>
            /// Il display lampeggia.
            /// </summary>
            FlashDisplay = WindowAppVisualSignal.SSWF_DISPLAY,
            /// <summary>
            /// Nessun segnale visuale.
            /// </summary>
            None = WindowAppVisualSignal.SSWF_NONE,
            /// <summary>
            /// La barra del titolo della finestra attiva lampeggia.
            /// </summary>
            FlashTitle = WindowAppVisualSignal.SSWF_TITLE,
            /// <summary>
            /// La finestra attiva lampeggia.
            /// </summary>
            FlashActiveWindow = WindowAppVisualSignal.SSWF_WINDOW
        }
    }
}