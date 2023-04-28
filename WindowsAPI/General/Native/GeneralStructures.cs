using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.General.Native
{
    /// <summary>
    /// Struttura generali usati in diverse API.
    /// </summary>
    internal static class GeneralStructures
    {
        /// <summary>
        /// Definisce i margini di una finestra con gli stili visuali applicati ad essa.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MARGINS
        {
            /// <summary>
            /// Larghezza del bordo sinistro che mantiene la sua dimensione.
            /// </summary>
            public int LeftBorderWidth;
            /// <summary>
            /// Larghezza del bordo destro che mantiene la sua dimensione.
            /// </summary>
            public int RightBorderWidth;
            /// <summary>
            /// Altezza del bordo superiore che mantiene la sua dimensione.
            /// </summary>
            public int TopBorderHeight;
            /// <summary>
            /// Altezza del bordo inferiore che mantiene la sua dimensione.
            /// </summary>
            public int BottomBorderHeight;
        }

        /// <summary>
        /// Definisce la larghezza e l'altezza di un rettangolo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct SIZE
        {
            /// <summary>
            /// Larghezza del rettangolo.
            /// </summary>
            public int Width;
            /// <summary>
            /// Altezza del rettangolo.
            /// </summary>
            public int Height;
        }

        /// <summary>
        /// Definisce le coordinate x e y di un punto.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINT
        {
            /// <summary>
            /// Coordinata x del punto.
            /// </summary>
            public int x;
            /// <summary>
            /// Coordinata y del punto.
            /// </summary>
            public int y;
        }

        /// <summary>
        /// Definisce un rettangolo attraverso le coordinate dei suoi angoli superiore sinistro e inferiore destro.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            /// <summary>
            /// Coordinata x dell'angolo superiore sinistro del rettangolo.
            /// </summary>
            public int Left;
            /// <summary>
            /// Coordinata y dell'angolo superiore sinistro del rettangolo.
            /// </summary>
            public int Top;
            /// <summary>
            /// Coordinata x dell'angolo inferiore destro del rettangolo.
            /// </summary>
            public int Right;
            /// <summary>
            /// Coordinata y dell'angolo inferiore destro del rettangolo.
            /// </summary>
            public int Bottom;
        }

        /// <summary>
        /// Definisce le coordinate x e y di un punto.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINTS
        {
            /// <summary>
            /// Coordinata x.
            /// </summary>
            public short X;
            /// <summary>
            /// Coordinata y.
            /// </summary>
            public short Y;
        }

        /// <summary>
        /// Identificatore univoco.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct GUID
        {
            /// <summary>
            /// Le prime 8 cifre esadecimali.
            /// </summary>
            public int Data1;
            /// <summary>
            /// Il primo gruppo di 4 cifre esadecimali.
            /// </summary>
            public short Data2;
            /// <summary>
            /// Il secondo gruppo di 4 cifre esadecimali.
            /// </summary>
            public short Data3;
            /// <summary>
            /// Array di 8 byte.
            /// </summary>
            /// <remarks>I primi 2 byte contengono il terzo gruppo di 4 cifre esadecimali, il resto contine il gruppo finale di 12 cifre esadecimali.</remarks>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Data4;
        }
    }
}