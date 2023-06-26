using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapEnumerations;

namespace WindowsAPI.WindowsGDIWrapper.Native.Bitmaps
{
    /// <summary>
    /// Strutture relative ai bitmap.
    /// </summary>
    internal static class BitmapStructures
    {
        /// <summary>
        /// Informazioni sulla dimensione e sui colori di un bitmap indipendente dal dispositivo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BITMAPINFO
        {
            /// <summary>
            /// Informazioni sulle dimensione e sul formato del colore.
            /// </summary>
            public BITMAPINFOHEADER Header;
            /// <summary>
            /// Colori della tabella.
            /// </summary>
            public IntPtr Colors;
        }

        /// <summary>
        /// Informazioni sulla dimensione e sul formato del colore di un bitmap indipendente dal dispositivo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BITMAPINFOHEADER
        {
            /// <summary>
            /// Numero di byte richiesti dalla struttura.
            /// </summary>
            /// <remarks>Questo valore non include la dimensione della tabella dei colori o la dimensione della maschera dei colori, se esistono dopo la struttura.</remarks>
            public uint Size;
            /// <summary>
            /// Larghezza del bitmap, in pixel.
            /// </summary>
            public int Width;
            /// <summary>
            /// Altezza del bitmap, in pixel.
            /// </summary>
            /// <remarks>Per bitmap non compressi, se il valore è positivo, il bitmap è bottom-up con l'origine all'angolo inferiore sinistro.<br/>
            /// Se il valore è negativo, il bitmap è top-down con l'origine all'angolo superiore sinistro.<br/><br/>
            /// Per bitmap YUV, essi sono sempre top-down.<br/><br/>
            /// Per bitmap compressi, il valore deve essere positivo, a prescindere dall'orientamento dell'immagine.</remarks>
            public int Height;
            /// <summary>
            /// Numero di piani per il dispostivo obbiettivo.
            /// </summary>
            /// <remarks>Deve essere 1.</remarks>
            public ushort Planes;
            /// <summary>
            /// Numero di bit per pixel.
            /// </summary>
            /// <remarks>Per formati non compressi, questo valore rappresenta il numero medio di bit per pixel.<br/>
            /// Per formati compressi, questo valore rappresenta la profondita di bit dell'immagine non compressa, dopo la decodifica.</remarks>
            public ushort BitCount;
            /// <summary>
            /// Compressione del bitmap.
            /// </summary>
            public BitmapCompression Compression;
            /// <summary>
            /// Dimensione, in byte, dell'immagine.
            /// </summary>
            /// <remarks>Può essere 0 per bitmap RGB non compressi.</remarks>
            public uint ImageSize;
            /// <summary>
            /// Risoluzione orizzontale, in pixel per metro, del dispositivo obbiettivo per il bitmap.
            /// </summary>
            public int HorizontalResolutionPixelPerMeter;
            /// <summary>
            /// Risoluzione verticale, in pixel per metro, del dispositivo obbiettivo per il bitmap.
            /// </summary>
            public int VerticalResolutionPixelPerMeter;
            /// <summary>
            /// Numero di indici dei colori usati nella tabella dei colori dal bitmap.
            /// </summary>
            public uint UsedColors;
            /// <summary>
            /// Numero di indici dei colori importanti per la visualizzazione del bitmap.
            /// </summary>
            /// <remarks>Se questo valore è 0, tutti i colori sono importanti.</remarks>
            public uint ImportantColors;
        }
    }
}