using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Callbacks;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Enumerations;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIFunctions;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUICallbacks;
using WindowsAPI.MultilingualUserInterfaceWrapper.Native;
using WindowsAPI.MultilingualUserInterfaceWrapper.DataClasses;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIStructures;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIConstants;
using WindowsAPI.NationalLanguageSupportWrapper.Native;
using WindowsAPI.General.Native;
using WindowsAPI.ErrorHandlingWrapper.Native;
using WindowsAPI.SafeHandles;

namespace WindowsAPI.MultilingualUserInterfaceWrapper
{
    /// <summary>
    /// Metodi per interagire con le funzionalità Multilingual User Interface (MUI).
    /// </summary>
    public static class MUI
    {
        /// <summary>
        /// Callback per l'enumerazione delle lingue.
        /// </summary>
        private static UILanguageEnumerationCallback ManagedCallback;

        /// <summary>
        /// Enumera le lingue installate.
        /// </summary>
        /// <param name="Callback">Callback da usare per elaborare le informazioni sulle lingue installate.</param>
        /// <param name="Format">Formato dell'output.</param>
        /// <param name="Object">Oggetto da passare al callback.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void EnumerateLanguages(UILanguageEnumerationCallback Callback, LanguageFormat Format, object Object)
        {
            if (Format == (LanguageFormat.ID | LanguageFormat.Name))
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            UILanguageEnumProcedure Procedure = new UILanguageEnumProcedure(UILanguagesEnumCallback);
            ManagedCallback = Callback ?? throw new ArgumentNullException(nameof(Callback), "The callback cannot be null.");
            if (Object != null)
            {
                GCHandle ObjectHandle = GCHandle.Alloc(Object);
                _ = EnumUILanguages(Procedure, (MUIEnumerations.LanguageFormat)Format, GCHandle.ToIntPtr(ObjectHandle));
                ObjectHandle.Free();
            }
            else
            {
                _ = EnumUILanguages(Procedure, (MUIEnumerations.LanguageFormat)Format, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Callback per l'enumerazione delle lingue installate.
        /// </summary>
        /// <param name="Language">Lingua.</param>
        /// <param name="lParam">Parametro fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        private static bool UILanguagesEnumCallback(string Language, IntPtr lParam)
        {
            if (lParam != IntPtr.Zero)
            {
                GCHandle Handle = GCHandle.FromIntPtr(lParam);
                return ManagedCallback.Invoke(Language, Handle.Target);
            }
            else
            {
                return ManagedCallback.Invoke(Language, null);
            }
        }

        /// <summary>
        /// Recupera informazioni su un file MUI.
        /// </summary>
        /// <param name="Info">Informazioni da recuperare.</param>
        /// <param name="FilePath">Percorso del file.</param>
        /// <returns>Istanza di <see cref="MUIFileInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static MUIFileInfo GetMuiFileInfo(MuiFileInfo Info, string FilePath)
        {
            uint StructureSize = 0;
            _ = GetFileMUIInfo((MUIEnumerations.MUIFileInfo)Info, FilePath, IntPtr.Zero, ref StructureSize);
            StructureSize = (uint)Marshal.SizeOf(typeof(FILEMUIINFO)) + StructureSize;
            using (SafeStructPointer StructureBuffer = new SafeStructPointer((int)StructureSize))
            {
                FILEMUIINFO Structure = new FILEMUIINFO()
                {
                    Size = StructureSize,
                    Version = MUI_FILEINFO_VERSION
                };
                StructureBuffer.WriteToMemory<FILEMUIINFO>(Structure);
                if (!GetFileMUIInfo((MUIEnumerations.MUIFileInfo)Info, FilePath, StructureBuffer, ref StructureSize))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
                else
                {
                    Structure = StructureBuffer.ReadFromMemory<FILEMUIINFO>();
                    MUIFileInfo FileInfo = new MUIFileInfo(Structure, StructureBuffer);
                    return FileInfo;
                }
            }
        }

        /// <summary>
        /// Recupera i percorsi di tutti i file di risorse specifici della lingua associati a un file della lingua neutrale.
        /// </summary>
        /// <param name="LanguageNeutralFilePath">Percorso al file della lingua neutrale.</param>
        /// <param name="SearchFilter">Opzioni di enumerazione.</param>
        /// <param name="FileType">Tipo del file indicato da <paramref name="LanguageNeutralFilePath"/>.</param>
        /// <param name="Format">Formato dell'output.</param>
        /// <param name="Language">Lingua di cui trovare le risorse.</param>
        /// <returns>Un array contenente i percorsi dei file.</returns>
        /// <remarks><paramref name="SearchFilter"/> ha effetto solo se <paramref name="Language"/> è nullo.<br/><br/>
        /// <paramref name="Language"/> deve seguire il formato indicato da <paramref name="Format"/>, se quest'ultimo non è impostato, allora <paramref name="Language"/> deve essere il nome di una località.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string[] GetLanguageSpecificResourceFiles(string LanguageNeutralFilePath, MuiSearchFilter? SearchFilter = null, MuiPEFileType? FileType = null, LanguageFormat? Format = null, string Language = null)
        {
            if (string.IsNullOrWhiteSpace(LanguageNeutralFilePath))
            {
                throw new ArgumentNullException(nameof(LanguageNeutralFilePath), "No path to language neutral file provided.");
            }
            if (SearchFilter.HasValue)
            {
                if (SearchFilter.Value != MuiSearchFilter.AllLanguages && SearchFilter.Value != MuiSearchFilter.UserPreferredUILanguages && SearchFilter.Value != MuiSearchFilter.InstalledLanguages)
                {
                    throw new InvalidEnumArgumentException("Invalid search filter.");
                }
            }
            if (FileType.HasValue)
            {
                if (FileType.Value != MuiPEFileType.NonNeutralFile && FileType.Value != MuiPEFileType.NeutralFile)
                {
                    throw new InvalidEnumArgumentException("Invalid file type.");
                }
            }
            if (Format.HasValue)
            {
                if (Format.Value != LanguageFormat.ID && Format != LanguageFormat.Name)
                {
                    throw new InvalidEnumArgumentException("Invalid format.");
                }
            }
            if (Format.HasValue && Format.Value is LanguageFormat.ID)
            {
                if (!string.IsNullOrWhiteSpace(Language) && (Language.Length > 4 || Language.All(character => char.IsDigit(character))))
                {
                    throw new ArgumentException("Invalid language string format.", nameof(Language));
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(Language) && Language.Length > NLSConstants.LOCALE_NAME_MAX_LENGTH)
                {
                    throw new ArgumentException("Invalid language string", nameof(Language));
                }
            }
            uint PathLength = GeneralConstants.MAX_PATH;
            uint LocaleNameLength = NLSConstants.LOCALE_NAME_MAX_LENGTH;
            ulong Enumerator = 0;
            uint Options = 0;
            if (SearchFilter.HasValue)
            {
                Options |= (uint)SearchFilter.Value;
            }
            if (Format.HasValue)
            {
                Options |= (uint)Format.Value;
            }
            if (FileType.HasValue)
            {
                Options |= (uint)FileType.Value;
            }
            StringBuilder LanguageBuilder = new StringBuilder(NLSConstants.LOCALE_NAME_MAX_LENGTH);
            if (!string.IsNullOrWhiteSpace(Language))
            {
                LanguageBuilder.Append(Language);
            }
            StringBuilder FilePathBuilder = new StringBuilder(GeneralConstants.MAX_PATH);
            List<string> FilePaths = new List<string>();
            bool Result;
            do
            {
                if (string.IsNullOrWhiteSpace(Language))
                {
                    _ = LanguageBuilder.Clear();
                }
                _ = FilePathBuilder.Clear();
                Result = GetFileMUIPath(Options, LanguageNeutralFilePath, LanguageBuilder, ref LocaleNameLength, FilePathBuilder, ref PathLength, ref Enumerator);
                if (Result)
                {
                    FilePaths.Add(FilePathBuilder.ToString());
                }
                else
                {
                    int ErrorCode = Marshal.GetLastWin32Error();
                    if (ErrorCode != Win32ErrorConstants.ERROR_NO_MORE_FILES)
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
            }
            while (Result);
            return FilePaths.ToArray();
        }

        /// <summary>
        /// Recupera la lista di lingue dell'interfaccia utente preferite dal processo.
        /// </summary>
        /// <param name="Format">Formato delle lingue.</param>
        /// <returns>Un array con le lingue preferite dal processo, vuoto se non ne esistono.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetProcessPreferredUILanguages(LanguageFormat Format)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            uint BufferSize = 0;
            _ = MUIFunctions.GetProcessPreferredUILanguages((MUIEnumerations.LanguageFormat)Format, out _, IntPtr.Zero, ref BufferSize);
            using (SafeMultistringPointer Buffer = new SafeMultistringPointer((int)BufferSize * UnicodeEncoding.CharSize))
            {
                if (MUIFunctions.GetProcessPreferredUILanguages((MUIEnumerations.LanguageFormat)Format, out uint LanguagesCount, Buffer, ref BufferSize))
                {
                    return Buffer.ReadStringsFromMemory((int)LanguagesCount);
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Recupera la lista di lingue dell'interfaccia utente preferite dal sistema.
        /// </summary>
        /// <param name="Format">Formato delle lingue.</param>
        /// <param name="CheckForValidLocales">Indica se la lista restituita deve includere solo località valide.</param>
        /// <returns>Un array con le lingue preferite dal sistema.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetSystemPreferredUILanguages(LanguageFormat Format, bool CheckForValidLocales)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            uint BufferSize = 0;
            uint Options = (uint)Format;
            if (CheckForValidLocales)
            {
                Options |= MUI_MACHINE_LANGUAGE_SETTINGS;
            }
            _ = MUIFunctions.GetSystemPreferredUILanguages(Options, out _, IntPtr.Zero, ref BufferSize);
            using (SafeMultistringPointer Buffer = new SafeMultistringPointer((int)BufferSize * UnicodeEncoding.CharSize))
            {
                if (MUIFunctions.GetSystemPreferredUILanguages(Options, out uint LanguagesCount, Buffer, ref BufferSize))
                {
                    return Buffer.ReadStringsFromMemory((int)LanguagesCount);
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Recupera la lista di lingue dell'interfaccia utente preferite dal thread.
        /// </summary>
        /// <param name="Format">Formato delle lingue.</param>
        /// <param name="OnlyThreadLanguages">Indica se la lista restituita deve includere solo le lingue preferite dal thread se esistono.</param>
        /// <param name="IncludeFallbackAndNeutralLanguages">Indica se la lista restituita deve includere anche le lingue di fallback e neutrali oltre a quelle preferite dal thread.</param>
        /// <returns>Un array con le lingue preferite dal thread.</returns>
        /// <remarks>Solo uno tra <paramref name="OnlyThreadLanguages"/> e <paramref name="IncludeFallbackAndNeutralLanguages"/> può essere true.</remarks>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetThreadPreferredUILanguages(LanguageFormat Format, bool OnlyThreadLanguages, bool IncludeFallbackAndNeutralLanguages)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            uint BufferSize = 0;
            uint Options = (uint)Format;
            if (OnlyThreadLanguages && !IncludeFallbackAndNeutralLanguages)
            {
                Options |= (uint)MUIEnumerations.ThreadLanguagesFilter.MUI_THREAD_LANGUAGES;
            }
            else if (!OnlyThreadLanguages && IncludeFallbackAndNeutralLanguages)
            {
                Options |= (uint)MUIEnumerations.ThreadLanguagesFilter.MUI_UI_FALLBACK;
            }
            else if (OnlyThreadLanguages && IncludeFallbackAndNeutralLanguages)
            {
                throw new ArgumentException("Invalid filtering.");
            }
            _ = MUIFunctions.GetThreadPreferredUILanguages(Options, out _, IntPtr.Zero, ref BufferSize);
            using (SafeMultistringPointer Buffer = new SafeMultistringPointer((int)BufferSize * UnicodeEncoding.CharSize))
            {
                if (MUIFunctions.GetThreadPreferredUILanguages(Options, out uint LanguagesCount, Buffer, ref BufferSize))
                {
                    return Buffer.ReadStringsFromMemory((int)LanguagesCount);
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Recupera la lingua dell'interfaccia utente esplicitamente associata con il thread.
        /// </summary>
        /// <returns>L'identificativo della lingua.</returns>
        public static short GetThreadUILanguage()
        {
            return (short)MUIFunctions.GetThreadUILanguage();
        }

        /// <summary>
        /// Recupera informazioni su una lingua.
        /// </summary>
        /// <param name="Format">Formato del parametro <paramref name="Language"/>.</param>
        /// <param name="Language">Lingua.</param>
        /// <returns>Istanza di <see cref="UILanguageInfo"/> con le informazioni.</returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static UILanguageInfo GetLanguageInfo(LanguageFormat Format, string Language)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            uint BufferSize = 0;
            _ = GetUILanguageInfo((MUIEnumerations.LanguageFormat)Format, Language, IntPtr.Zero, ref BufferSize, out _);
            using (SafeMultistringPointer Buffer = new SafeMultistringPointer((int)BufferSize * UnicodeEncoding.CharSize))
            {
                if (GetUILanguageInfo((MUIEnumerations.LanguageFormat)Format, Language, Buffer, ref BufferSize, out MUIEnumerations.LanguageAttributes Attributes))
                {
                    UILanguageInfo LanguageInfo = new UILanguageInfo(Language, Attributes, Buffer);
                    return LanguageInfo;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Recupera la lingua predefinita dell'interfaccia utente dell'utente corrente.
        /// </summary>
        /// <returns>L'identificativo della lingua.</returns>
        public static short GetUserDefaultUILanguage()
        {
            return (short)MUIFunctions.GetUserDefaultUILanguage();
        }

        /// <summary>
        /// Recupera la lista di lingue dell'interfaccia utente preferite dall'utente.
        /// </summary>
        /// <param name="Format">Formato delle lingue.</param>
        /// <returns>Un array con le lingue preferite dall'utente.</returns>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static string[] GetUserPreferredUILanguages(LanguageFormat Format)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            uint BufferSize = 0;
            _ = MUIFunctions.GetUserPreferredUILanguages((MUIEnumerations.LanguageFormat)Format, out _, IntPtr.Zero, ref BufferSize);
            using (SafeMultistringPointer Buffer = new SafeMultistringPointer((int)BufferSize * UnicodeEncoding.CharSize))
            {
                if (MUIFunctions.GetUserPreferredUILanguages((MUIEnumerations.LanguageFormat)Format, out uint LanguagesCount, Buffer, ref BufferSize))
                {
                    return Buffer.ReadStringsFromMemory((int)LanguagesCount);
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Imposta le lingue dell'interfaccia utente preferite dal processo.
        /// </summary>
        /// <param name="Format">Formato della lista.</param>
        /// <param name="Languages">Lista di lingue da aggiungere.</param>
        /// <remarks>Questo metodo imposta solamente le prime 5 lingue indicate nell'array.</remarks>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetProcessPreferredUILanguages(LanguageFormat Format, string[] Languages)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            if (Languages is null || Languages.Length == 0)
            {
                if (!MUIFunctions.SetProcessPreferredUILanguages((MUIEnumerations.LanguageFormat)Format, null, out _))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                StringBuilder LanguageListBuilder = new StringBuilder();
                foreach (string Language in Languages)
                {
                    LanguageListBuilder.Append(Language).Append('\0');
                }
                LanguageListBuilder.Append('\0');
                if (!MUIFunctions.SetProcessPreferredUILanguages((MUIEnumerations.LanguageFormat)Format, LanguageListBuilder.ToString(), out _))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Imposta le lingue dell'interfaccia utente preferite dal thread.
        /// </summary>
        /// <param name="Format">Formato delle lingue nella lista.</param>
        /// <param name="Filter">Filtro da applicare.</param>
        /// <param name="Languages">Lista di lingue.</param>
        /// <remarks>Se <paramref name="Filter"/> è impostato, <paramref name="Languages"/> deve essere nullo o vuoto.</remarks>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetThreadPreferredUILanguages(LanguageFormat Format, ThreadLanguageFilterSet? Filter, string[] Languages)
        {
            if (Format != LanguageFormat.ID && Format != LanguageFormat.Name)
            {
                throw new InvalidEnumArgumentException("Invalid format.");
            }
            if (Filter.HasValue)
            {
                if (Languages != null || Languages.Length > 0)
                {
                    throw new ArgumentException("The parameter must be null or an empty array.", nameof(Languages));
                }
                else
                {
                    if (Filter.Value != ThreadLanguageFilterSet.ReplaceConsoleIncompatibleLanguages && Filter.Value != ThreadLanguageFilterSet.ReplaceComplexScriptsLanguages && Filter.Value != ThreadLanguageFilterSet.ResetFilters)
                    {
                        throw new InvalidEnumArgumentException("Invalid filter.");
                    }
                    else
                    {
                        uint Options = (uint)Format | (uint)Filter.Value;
                        if (!MUIFunctions.SetThreadPreferredUILanguages(Options, null, out _))
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                }
            }
            else
            {
                if (Languages is null || Languages.Length == 0)
                {
                    if (!MUIFunctions.SetThreadPreferredUILanguages((uint)Format, null, out _))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
                else
                {
                    StringBuilder LanguageListBuilder = new StringBuilder();
                    foreach (string Language in Languages)
                    {
                        LanguageListBuilder.Append(Language).Append('\0');
                    }
                    LanguageListBuilder.Append('\0');
                    if (!MUIFunctions.SetThreadPreferredUILanguages((uint)Format, LanguageListBuilder.ToString(), out _))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
            }
        }

        /// <summary>
        /// Imposta la lingua dell'interfaccia utente per il thread.
        /// </summary>
        /// <param name="LangID">Identificatore della lingua.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetThreadUILanguage(short LangID)
        {
            ushort SetLangID = MUIFunctions.SetThreadUILanguage((ushort)LangID);
            if (LangID != 0)
            {
                if (SetLangID == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }
    }
}