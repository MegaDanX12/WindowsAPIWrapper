using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsGDIWrapper.Native.FontAndText.FontAndTextStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Icons
{
    /// <summary>
    /// Strutture relative alle icone.
    /// </summary>
    internal static class IconStructures
    {
        /// <summary>
        /// Metriche scalabili associate alle icone.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct ICONMETRICS
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Spazio orizzontale, in pixel, per ogni icona disposta.
            /// </summary>
            public int HorizontalSpacing;
            /// <summary>
            /// Spazio verticale, in pixel, per ogni icona disposta.
            /// </summary>
            public int VerticalSpacing;
            /// <summary>
            /// Indica se il titolo dell'icona può andare a capo (diverso da 0) o meno (0).
            /// </summary>
            public int TitleWrap;
            /// <summary>
            /// Font da usare per il titolo dell'icona.
            /// </summary>
            public LOGFONT Font;
        }
    }
}