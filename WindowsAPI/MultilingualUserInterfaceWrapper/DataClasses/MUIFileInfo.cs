using System;
using System.Runtime.InteropServices;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Enumerations;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIStructures;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.DataClasses
{
    /// <summary>
    /// Informazioni su un file MUI.
    /// </summary>
    public class MUIFileInfo
    {
        /// <summary>
        /// Tipo di file.
        /// </summary>
        public MuiFileType FileType { get; }

        /// <summary>
        /// Checksum.
        /// </summary>
        public byte[] Checksum { get; }

        /// <summary>
        /// Checksum usato per la manutenzione.
        /// </summary>
        public byte[] ServiceChecksum { get; }

        /// <summary>
        /// Nome della lingua.
        /// </summary>
        public string LanguageName { get; }

        /// <summary>
        /// Risorse.
        /// </summary>
        public Tuple<int, string>[] Resources { get; }

        /// <summary>
        /// Risorse MUI.
        /// </summary>
        public Tuple<int, string>[] MUIResources { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="MUIFileInfo"/>.
        /// </summary>
        /// <param name="Data">Struttura <see cref="FILEMUIINFO"/> contenente i dati.</param>
        /// <param name="StructureBase">Puntatore alla base della struttura <see cref="FILEMUIINFO"/>.</param>
        internal MUIFileInfo(FILEMUIINFO Data, IntPtr StructureBase)
        {
            FileType = (MuiFileType)Data.FileType;
            Checksum = Data.Checksum;
            ServiceChecksum = Data.ServiceChecksum;
            LanguageName = Marshal.PtrToStringUni(StructureBase + (int)Data.LanguageNameOffset);
            uint[] ResourceIDs = UtilityMethods.ReadUnmanagedArray<uint>(StructureBase + (int)Data.TypeIDMainOffset, (int)Data.TypeIDMainSize);
            string[] ResourceNames = UtilityMethods.ReadUnmanagedArray<string>(StructureBase + (int)Data.TypeNameMainOffset, (int)Data.TypeIDMainSize);
            Resources = new Tuple<int, string>[Data.TypeIDMainSize];
            for (int i = 0; i < Data.TypeIDMainSize; i++)
            {
                Resources[i] = new Tuple<int, string>((int)ResourceIDs[i], ResourceNames[i]);
            }
            uint[] MUIResourceIDs = UtilityMethods.ReadUnmanagedArray<uint>(StructureBase + (int)Data.TypeIDMUIOffset, (int)Data.TypeIDMUISize);
            string[] MUIResourceNames = UtilityMethods.ReadUnmanagedArray<string>(StructureBase + (int)Data.TypeNameMUIOffset, (int)Data.TypeIDMUISize);
            MUIResources = new Tuple<int, string>[Data.TypeIDMUISize];
            for (int i = 0; i < Data.TypeIDMainSize; i++)
            {
                MUIResources[i] = new Tuple<int, string>((int)MUIResourceIDs[i], MUIResourceNames[i]);
            }
        }
    }
}