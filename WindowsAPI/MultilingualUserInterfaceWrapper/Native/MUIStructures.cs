using System.Runtime.InteropServices;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIEnumerations;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.Native
{
    /// <summary>
    /// Strutture relative alle funzionalità MUI.
    /// </summary>
    internal static class MUIStructures
    {
        /// <summary>
        /// Informazioni su un file relative al suo uso in MUI.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct FILEMUIINFO
        {
            /// <summary>
            /// Dimensione, in byte della struttura incluso il buffer.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Versione della struttura.
            /// </summary>
            public uint Version;
            /// <summary>
            /// Tipo di file MUI.
            /// </summary>
            public MUIFileType FileType;
            /// <summary>
            /// Checksum a 128 bit per un file.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] Checksum;
            /// <summary>
            /// Checksum a 128 bit usato per servicing.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] ServiceChecksum;
            /// <summary>
            /// Offset, dall'inizio della struttura, al nome della lingua per un file risorse specifico oppure all'ultima lingua di fallback per un file neutrale.
            /// </summary>
            public uint LanguageNameOffset;
            /// <summary>
            /// Dimensione dell'array all'offset indicato da <see cref="TypeIDMainOffset"/>.
            /// </summary>
            /// <remarks>Questo campo indica anche il numero di stringhe nell'array indicato da <see cref="TypeNameMainOffset"/>.</remarks>
            public uint TypeIDMainSize;
            /// <summary>
            /// Offset, in byte, dall'inizio della struttura a un array di valori senza segno a 32 bit che elenca i tipi di risorse contenute in un file di lingua neutrale.
            /// </summary>
            public uint TypeIDMainOffset;
            /// <summary>
            /// Offset, in byte, dall'inizio della struttura a una serie di stringhe a terminazione nulla che elencano i nomi delle risorse contenute in un file di lingua neutrale.
            /// </summary>
            public uint TypeNameMainOffset;
            /// <summary>
            /// Dimensione dell'array che si trova all'offset indicato da <see cref="TypeIDMUIOffset"/>.
            /// </summary>
            /// <remarks>Questa campo indica anche il numero di stringhe nell'array indicato da <see cref="TypeNameMUIOffset"/>.</remarks>
            public uint TypeIDMUISize;
            /// <summary>
            /// Offset, in byte, dall'inizio della struttura a un array di valori senza segno a 32 bit che elenca i tipi di risorse contenute in un file di lingua neutrale.
            /// </summary>
            public uint TypeIDMUIOffset;
            /// <summary>
            /// Offset, in byte, dall'inizio della struttura a una serie di stringhe a terminazione nulla che elencano i nomi delle risorse contenute in un file di lingua neutrale.
            /// </summary>
            public uint TypeNameMUIOffset;
            /// <summary>
            /// Memoria allocata rimanente per la struttura.
            /// </summary>
            /// <remarks>La dimensione di questo buffer può essere superiore a 8 byte.</remarks>
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] Buffer;
        }
    }
}