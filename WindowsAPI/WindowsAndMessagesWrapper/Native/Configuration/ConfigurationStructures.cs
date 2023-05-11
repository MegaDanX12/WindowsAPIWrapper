using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsGDIWrapper.Native.FontAndText.FontAndTextStructures;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration
{
    /// <summary>
    /// Strutture relative alle configurazione del sistema.
    /// </summary>
    internal static class ConfigurationStructures
    {
        /// <summary>
        /// Metriche scalabili associate all'area non client di una finestra non minimizzata.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NONCLIENTMETRICS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Spessore del bordo di ridimensionamento, in pixel.
            /// </summary>
            /// <remarks>Il valore predefinito è di 1 pixel.</remarks>
            public int SizingBorderWidth;
            /// <summary>
            /// Larghezza di una barra di scorrimento verticale standard, in pixel.
            /// </summary>
            public int VScrollBarWidth;
            /// <summary>
            /// Altezza di una barra di scorrimento orizzontale standard, in pixel.
            /// </summary>
            public int HScrollBarHeight;
            /// <summary>
            /// Larghezza dei pulsanti della barra del titolo, in pixel.
            /// </summary>
            public int CaptionButtonWidth;
            /// <summary>
            /// Altezza dei pulsanti della barra del titolo, in pixel.
            /// </summary>
            public int CaptionButtonHeight;
            /// <summary>
            /// Font usato per la barra del titolo.
            /// </summary>
            public LOGFONT CaptionFont;
            /// <summary>
            /// Larghezza dei pulsanti piccoli della barra del titolo, in pixel.
            /// </summary>
            public int SmallCaptionButtonWidth;
            /// <summary>
            /// Altezza dei pulsanti piccoli della barra del titolo, in pixel.
            /// </summary>
            public int SmallCaptionButtonHeight;
            /// <summary>
            /// Font usato per la barra dei titolo piccola.
            /// </summary>
            public LOGFONT SmallCaptionFont;
            /// <summary>
            /// Larghezza dei pulsanti della barra dei menù, in pixel.
            /// </summary>
            public int MenuWidth;
            /// <summary>
            /// Altezza dei pulsanti della barra dei menù, in pixel.
            /// </summary>
            public int MenuHeight;
            /// <summary>
            /// Font usato nelle barre dei menù.
            /// </summary>
            public LOGFONT MenuFont;
            /// <summary>
            /// Font usato nelle barra di stato e nei tooltip.
            /// </summary>
            public LOGFONT StatusBarFont;
            /// <summary>
            /// Font usato nelle finestre di messaggio.
            /// </summary>
            public LOGFONT MessageBoxesFont;
            /// <summary>
            /// Spessore del bordo riempito, in pixel.
            /// </summary>
            /// <remarks>Il valore predefinito è di 4 pixel.</remarks>
            public int PaddedBorderThickness;
        }
    }
}