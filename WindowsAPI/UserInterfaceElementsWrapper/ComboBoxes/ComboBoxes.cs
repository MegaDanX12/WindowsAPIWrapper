using System;
using System.IO;
using System.Linq;
using System.Text;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxFunctions;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxEnumerations;
using static WindowsAPI.General.Native.GeneralConstants;
using System.ComponentModel;
using System.Runtime.InteropServices;
using WindowsAPI.UserInterfaceElementsWrapper.ComboBoxes.DataClasses;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxMessages;
using static WindowsAPI.WindowsAndMessagesWrapper.Messages.Messages;
using WindowsAPI.SafeHandles;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses.WindowClassesEnumerations;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses.WindowClassesFunctions;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;
using System.Drawing;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.General.Native.Macros;
using static WindowsAPI.UserInterfaceElementsWrapper.ComboBoxes.Enumerations;
using System.Xml.XPath;
using System.Reflection;

namespace WindowsAPI.UserInterfaceElementsWrapper.ComboBoxes
{
    /// <summary>
    /// Metodi per l'interazione con i ComboBox.
    /// </summary>
    public static class ComboBoxes
    {
        /// <summary>
        /// Sostituisce il contenuto di un Combobox con una lista di nomi di file e cartelle.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il controllo.</param>
        /// <param name="BasePath">Percorso base da cui iniziare l'enumerazione dei file e delle cartelle.</param>
        /// <param name="ComboboxID">ID del ComboBox.</param>
        /// <param name="Filter">Opzioni di filtraggio della lista.</param>
        /// <param name="StaticControlID">ID di un controllo statico.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void AddFileListToCombobox(IntPtr DialogHandle, string BasePath, int ComboboxID, Enumerations.FileAttributes Filter = Enumerations.FileAttributes.IncludeReadWriteFiles, int StaticControlID = 0)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "No handle provided.");
            }
            if (string.IsNullOrWhiteSpace(BasePath))
            {
                throw new ArgumentNullException(nameof(BasePath), "Base path not provided");
            }
            if (ComboboxID <= 0)
            {
                throw new ArgumentException("Invalid ID.", nameof(ComboboxID));
            }
            if (StaticControlID < 0)
            {
                throw new ArgumentException("Invalid ID.", nameof(StaticControlID));
            }
            string BasePathFileName = Path.GetFileName(BasePath);
            if (!string.IsNullOrWhiteSpace(BasePathFileName))
            {
                if (!BasePathFileName.Contains('*') && !BasePathFileName.Contains("?"))
                {
                    throw new FormatException("The filename in BasePath must contain a wildcard (* or ?).");
                }
            }
            int Result = DirListCombobox(DialogHandle, BasePath, ComboboxID, StaticControlID, (FileType)Filter);
            if (Result == 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la selezione corrente di un Combobo riempito tramite <see cref="ComboBoxes.AddFileListToCombobox"/>.
        /// </summary>
        /// <param name="DialogHandle">Handle alla finestra di dialogo che contiene il controllo.</param>
        /// <param name="ComboboxID">ID del controllo.</param>
        /// <returns>La selezione corrente.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string GetSelectedPath(IntPtr DialogHandle, int ComboboxID)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "No handle provided.");
            }
            if (ComboboxID <= 0)
            {
                throw new ArgumentException("Invalid ID.", nameof(ComboboxID));
            }
            StringBuilder Selection = new StringBuilder(MAX_PATH);
            if (DirSelectComboBox(DialogHandle, Selection, MAX_PATH, ComboboxID))
            {
                return Selection.ToString();
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sullo stato di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>Istanza di <see cref="ComboboxStatusInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static ComboboxStatusInfo GetComboboxStatus(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            COMBOBOXINFO InfoStructure = new COMBOBOXINFO()
            {
                Size = (uint)Marshal.SizeOf(typeof(COMBOBOXINFO))
            };
            if (GetComboBoxInfo(ComboboxHandle, ref InfoStructure))
            {
                return new ComboboxStatusInfo(InfoStructure);
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il valore di un elemento dalla lista di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Index">Indice dell'elemento.</param>
        /// <returns>una stringa se il controllo contiene delle stringhe, oppure i dati associati all'elemento.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public static object GetValueFromList(IntPtr ComboboxHandle, int Index)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr WindowStyles = GetWindowLongPtr(ComboboxHandle, (int)WindowInfo.GWL_STYLE);
            bool HasStrings = true;
            if (WindowStyles != IntPtr.Zero)
            {
                ComboBoxStyles Styles = (ComboBoxStyles)WindowStyles.ToInt32();
                if (!Styles.HasFlag(ComboBoxStyles.CBS_HASSTRINGS))
                {
                    HasStrings = false;
                }
            }
            IntPtr StringIndexValue = new IntPtr(Index);
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETLBTEXTLEN, StringIndexValue, IntPtr.Zero);
            int StringLength;
            if (Result.ToInt32() == (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not get string length");
            }
            else
            {
                StringLength = Result.ToInt32();
                if (HasStrings)
                {
                    using (SafeStringPointer StringPointer = new SafeStringPointer(StringLength + 1))
                    {
                        Result = SendMessage(ComboboxHandle, CB_GETLBTEXT, StringIndexValue, StringPointer);
                        if (Result.ToInt32() == (int)ComboBoxMessageReturnValue.CB_ERR)
                        {
                            throw new Win32Exception("Could not get the string");
                        }
                        else
                        {
                            return StringPointer.ReadFromMemory();
                        }
                    }
                }
                else
                {
                    return Result;
                }
            }
        }

        /// <summary>
        /// Aggiunge una stringa a un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="String">Stringa da aggiungere.</param>
        /// <returns>L'indice a cui è stata aggiunta la stringa.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InsufficientMemoryException"></exception>
        public static int AddString(IntPtr ComboboxHandle, string String)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(String))
            {
                IntPtr Result = SendMessage(ComboboxHandle, CB_ADDSTRING, IntPtr.Zero, StringPointer);
                int ResultInteger = Result.ToInt32();
                if (ResultInteger is (int)ComboBoxMessageReturnValue.CB_ERR || ResultInteger is (int)ComboBoxMessageReturnValue.CB_ERRSPACE)
                {
                    if (ResultInteger is (int)ComboBoxMessageReturnValue.CB_ERR)
                    {
                        throw new Win32Exception("Could not add a string to the list.");
                    }
                    else
                    {
                        throw new InsufficientMemoryException("Insufficient memory to add the string.");
                    }
                }
                else
                {
                    return ResultInteger;
                }
            }
        }

        /// <summary>
        /// Rimuove un elemento da un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Index">Indice dell'elemento da eliminare.</param>
        /// <returns>Il numero di elementi rimanenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public static int RemoveString(IntPtr ComboboxHandle, int Index)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_DELETESTRING, new IntPtr(Index), IntPtr.Zero);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            else
            {
                return Result.ToInt32();
            }
        }

        /// <summary>
        /// Trova in un Combobox l'elemento che inizia con i caratteri presenti nella stringa.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle a un Combobox.</param>
        /// <param name="String">Stringa che contiene i caratteri da cercare.</param>
        /// <param name="StartingIndex">Indica da cui iniziare la ricerca.</param>
        /// <returns>Indice dell'elemento trovato.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int IndexOf(IntPtr ComboBoxHandle, string String, int StartingIndex = -1)
        {
            if (ComboBoxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            if (StartingIndex < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(StartingIndex));
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(String))
            {
                IntPtr Result = SendMessage(ComboBoxHandle, CB_FINDSTRING, new IntPtr(StartingIndex), StringPointer);
                if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
                {
                    throw new Win32Exception("No match.");
                }
                else
                {
                    return Result.ToInt32();
                }
            }
        }

        /// <summary>
        /// Trova in un Combobox l'elemento corrispondente alla stringa.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle a un Combobox.</param>
        /// <param name="String">Stringa da cercare.</param>
        /// <param name="StartingIndex">Indica da cui iniziare la ricerca.</param>
        /// <returns>Indice dell'elemento trovato.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int IndexOfExact(IntPtr ComboBoxHandle, string String, int StartingIndex = -1)
        {
            if (ComboBoxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            if (StartingIndex < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(StartingIndex));
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(String))
            {
                IntPtr Result = SendMessage(ComboBoxHandle, CB_FINDSTRINGEXACT, new IntPtr(StartingIndex), StringPointer);
                if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
                {
                    throw new Win32Exception("No match.");
                }
                else
                {
                    return Result.ToInt32();
                }
            }
        }

        /// <summary>
        /// Recupera il numero di elementi in un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>Il numero di elementi nel Combobox.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int GetItemCount(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not get the number of items.");
            }
            else
            {
                return Result.ToInt32();
            }
        }

        /// <summary>
        /// Recupera il suggerimento di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>Il suggerimento.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string GetCueBanner(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result;
            int StringSize = 25;
            string CueBanner = string.Empty;
            do
            {
                using (SafeStringPointer StringPointer = new SafeStringPointer(StringSize))
                {
                    Result = SendMessage(ComboboxHandle, CB_GETCUEBANNER, IntPtr.Zero, IntPtr.Zero);
                    if (Result.ToInt32() != 0 && Result.ToInt32() != 1)
                    {
                        throw new Win32Exception("Could not get the cue banner.");
                    }
                    else if (Result.ToInt32() is 1)
                    {
                        CueBanner = StringPointer.ReadFromMemory();
                    }
                }  
            }
            while (Result.ToInt32() != 1 || Result.ToInt32() != 0);
            return CueBanner;
        }

        /// <summary>
        /// Recupera le coordinate di un Combobox con il dropdown visibile.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>Coordinate del Combobox.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static Rectangle GetDroppedDownCoordinates(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            RECT Coordinates = new RECT();
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(Coordinates)))
            {
                StructurePointer.WriteToMemory<RECT>(Coordinates);
                IntPtr Result = SendMessage(ComboboxHandle, CB_GETDROPPEDCONTROLRECT, IntPtr.Zero, StructurePointer);
                if (Result.ToInt32() is 0)
                {
                    throw new Win32Exception("Could not get the dropped down coordinates.");
                }
                else
                {
                    Coordinates = StructurePointer.ReadFromMemory<RECT>();
                    return Rectangle.FromLTRB(Coordinates.Left, Coordinates.Top, Coordinates.Right, Coordinates.Bottom);
                }
            }
        }

        /// <summary>
        /// Recupera lo stato del listbox di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>true se il listbox è visibile, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsListboxVisible(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETDROPPEDSTATE, IntPtr.Zero, IntPtr.Zero);
            return Convert.ToBoolean(Result.ToInt32());
        }

        /// <summary>
        /// Recupera la larghezza minima permessa del listbox di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>Larghezza minima permessa, in pixel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int GetListBoxMinimumAllowedWidth(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETDROPPEDWIDTH, IntPtr.Zero, IntPtr.Zero);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not get the number of items.");
            }
            else
            {
                return Result.ToInt32();
            }
        }

        /// <summary>
        /// Recupera gli indici della selezione attuale.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="StartingPosition">Posizione iniziale della selezione.</param>
        /// <param name="EndingPosition">Posizione finale della selezione.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GetSelectionIndexes(IntPtr ComboboxHandle, out int StartingPosition, out int EndingPosition)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETEDITSEL, IntPtr.Zero, IntPtr.Zero);
            EndingPosition = HIWORD((uint)Result.ToInt32());
            StartingPosition = LOWORD((uint)Result.ToInt32());
        }

        /// <summary>
        /// Determina se l'interfaccia utente estesa di un Combobox è abilitata
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>true se l'interfaccia utente estesa è abilitata, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool IsExtendedUIEnabled(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETEXTENDEDUI, IntPtr.Zero, IntPtr.Zero);
            return Convert.ToBoolean(Result.ToInt32());
        }

        /// <summary>
        /// Recupera la larghezza di scorrimento del listbox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>La larghezza di scorrimento, in pixel.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetListboxScrollableWidth(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETHORIZONTALEXTENT, IntPtr.Zero, IntPtr.Zero);
            return Result.ToInt32();
        }

        /// <summary>
        /// Recupera i dati associati a un oggetto presente nel Combobox.
        /// </summary>
        /// <typeparam name="T">Tipo dell'oggetto.</typeparam>
        /// <param name="ComboboxHandle">Handle al ComboBox.</param>
        /// <param name="Index">Indice dell'oggetto.</param>
        /// <param name="IsPointer">Indica se il valore recuperato è un puntatore.</param>
        /// <param name="CharCount">Numero di caratteri nella stringa.</param>
        /// <returns>I dati associati.</returns>
        /// <remarks>Questo metodo recupera il valore associato a un oggetto nel Combobox, questo valore può essere uno dei tipi primitivi, un enumerazione, una stringa o un puntatore a un oggetto gestito.<br/><br/>
        /// Se <paramref name="IsPointer"/> è true, i dati recuperati dall'oggetto verranno interpretati come puntatore, il metodo restituisce il valore presente all'indirizzo indicato da esso, se si vuole recuperare solo il valore del puntatore, impostare <typeparamref name="T"/> come <see cref="IntPtr"/> e <paramref name="IsPointer"/> a false.<br/><br/>
        /// Se <typeparamref name="T"/> è impostato a <see cref="string"/>, i dati recuperati dall'oggetto sono sempre interpretati come puntatore, il metodo restituisce la stringa con una dimensione pari a <paramref name="CharCount"/> oppure fino al primo carattere nullo.<br/><br/>
        /// Se <typeparamref name="T"/> è impostato a <see cref="IntPtr"/> e <paramref name="IsPointer"/> è true, i dati recuperati dall'oggetto verranno interpretati come indirizzo di un puntatore e verrà restituito il valore del puntatore all'indirizzo recuperato.<br/><br/>
        /// Se <typeparamref name="T"/> non è un tipo primitivo, un enumerazione o una stringa, i dati recuperati dall'oggetto verranno interpretati come un puntatore a un oggetto gestito fornito da un'istanza di <see cref="GCHandle"/>, il metodo restituisce l'istanza dell'oggetto gestito.<br/><br/>
        /// Il metodo non può recuperare direttamente istanze di strutture personalizzate, se i dati recuperati dall'oggetto sono l'indirizzo di una struttura, recuperare il puntatore come già indicato e poi ricostruire la struttura manualmente.<br/><br/>
        /// I puntatori grezzi non sono supportati.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static T GetItemData<T>(IntPtr ComboboxHandle, int Index, bool IsPointer, int CharCount = -1)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETITEMDATA, new IntPtr(Index), IntPtr.Zero);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not get item data.");
            }
            else
            {
                if (Result != IntPtr.Zero)
                {
                    Type ObjectType = typeof(T);
                    if (ObjectType.IsEnum)
                    {
                        ObjectType = ObjectType.GetEnumUnderlyingType();
                    }
                    if (ObjectType.IsPrimitive)
                    {
                        switch (Type.GetTypeCode(ObjectType))
                        {
                            case TypeCode.Boolean:
                            case TypeCode.Byte:
                            case TypeCode.SByte:
                                if (!IsPointer)
                                {
                                    return (T)(object)Result;
                                }
                                else
                                {
                                    return (T)(object)Marshal.ReadByte(Result);
                                }
                            case TypeCode.Int16:
                            case TypeCode.UInt16:
                            case TypeCode.Char:
                                if (!IsPointer)
                                {
                                    return (T)(object)Convert.ToInt16(Result.ToInt32());
                                }
                                else
                                {
                                    return (T)(object)Marshal.ReadInt16(Result);
                                }
                            case TypeCode.Int32:
                            case TypeCode.UInt32:
                            case TypeCode.Single:
                                if (!IsPointer)
                                {
                                    return (T)(object)Result.ToInt32();
                                }
                                else
                                {
                                    return (T)(object)Marshal.ReadInt32(Result);
                                }
                            case TypeCode.Int64:
                            case TypeCode.UInt64:
                            case TypeCode.Double:
                                if (!IsPointer)
                                {
                                    return (T)(object)Result.ToInt64();
                                }
                                else
                                {
                                    return (T)(object)Marshal.ReadInt64(Result);
                                }
                            default: 
                                throw new NotSupportedException("Unsupported primitive type.");
                        }
                    }
                    else if (ObjectType.IsPointer)
                    {
                        throw new NotSupportedException("Pointer types are not supported");
                    }
                    else if (ObjectType == typeof(IntPtr))
                    {
                        if (!IsPointer)
                        {
                            return (T)(object)Result;
                        }
                        else
                        {
                            return (T)(object)Marshal.ReadIntPtr(Result);
                        }
                    }
                    else if (Type.GetTypeCode(ObjectType) is TypeCode.String)
                    {
                        if (CharCount is -1)
                        {
                            return (T)(object)Marshal.PtrToStringUni(Result);
                        }
                        else
                        {
                            return (T)(object)Marshal.PtrToStringUni(Result, CharCount);
                        }
                    }
                    else
                    {
                        GCHandle ObjectHandle = GCHandle.FromIntPtr(Result);
                        return (T)ObjectHandle.Target;
                    }
                }
                else
                {
                    return default;
                }
            }
        }

        /// <summary>
        /// Recupera l'altezza di un componente di un ComboBox.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al Combobox.</param>
        /// <param name="Component">Componente di cui recuperare l'altezza.</param>
        /// <param name="Index">Indice dell'elemento.</param>
        /// <returns>Altezza, in pixel, del componente.</returns>
        /// <remarks>Se gli elementi della lista hanno altezza variabile, <paramref name="Index"/>, deve essere impostato all'indice dell'oggetto di cui recuperare l'altezza.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int GetComponentHeight(IntPtr ComboBoxHandle, ComboboxComponent Component, int Index = -1)
        {
            if (ComboBoxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            IntPtr WindowStyles = GetWindowLongPtr(ComboBoxHandle, (int)WindowInfo.GWL_STYLE);
            bool ItemsHaveVariableHeight = ((ComboBoxStyles)WindowStyles.ToInt32()).HasFlag(ComboBoxStyles.CBS_OWNERDRAWVARIABLE);
            if (Component is ComboboxComponent.ListItems && ItemsHaveVariableHeight && Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr Result = SendMessage(ComboBoxHandle, CB_GETITEMHEIGHT, new IntPtr((int)Component), IntPtr.Zero);
            if (Result.ToInt32() != (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                return Result.ToInt32();
            }
            else
            {
                throw new Win32Exception("Could not retrieve component height.");
            }
        }

        /// <summary>
        /// Recupera la località di un ComboBox.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al Combobox.</param>
        /// <param name="CountryCode">Codice paese/regione.</param>
        /// <param name="LangID">ID lingua.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void GetLocale(IntPtr ComboBoxHandle, out short CountryCode, out short LangID)
        {
            if (ComboBoxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboBoxHandle, CB_GETLOCALE, IntPtr.Zero, IntPtr.Zero);
            CountryCode = (short)HIWORD((uint)Result.ToInt32());
            LangID = (short)LOWORD((uint)Result.ToInt32());
        }

        /// <summary>
        /// Recupera il numero minimi di elementi visibile nel listbox di un Combobox.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al Combobox.</param>
        /// <returns>Numero minimo di elementi.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static int GetDropdownMinimumVisibleItems(IntPtr ComboBoxHandle)
        {
            if (ComboBoxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            IntPtr WindowStyles = GetWindowLongPtr(ComboBoxHandle, (int)WindowInfo.GWL_STYLE);
            bool NoIntegralHeight = ((ComboBoxStyles)WindowStyles.ToInt32()).HasFlag(ComboBoxStyles.CBS_NOINTEGRALHEIGHT);
            if (NoIntegralHeight)
            {
                throw new InvalidOperationException("Invalid styles.");
            }
            else
            {
                IntPtr Result = SendMessage(ComboBoxHandle, CB_GETMINVISIBLE, IntPtr.Zero, IntPtr.Zero);
                return Result.ToInt32();
            }
        }

        /// <summary>
        /// Recupera l'indice del primo elemento visibile del listbox di un Combobox.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al ComboBox.</param>
        /// <returns>L'indice dell'elemento.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int GetFirstVisibleItemIndex(IntPtr ComboBoxHandle)
        {
            if (ComboBoxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboBoxHandle, CB_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);
            if (Result.ToInt32() != (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                return Result.ToInt32();
            }
            else
            {
                throw new Win32Exception("Could not get index of first visible item.");
            }
        }

        /// <summary>
        /// Alloca la memoria prima di aggiungere una serie di stringhe.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al Combobox.</param>
        /// <param name="Strings">Stringhe da aggiungere.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InsufficientMemoryException"></exception>
        public static void PreallocateAddString(IntPtr ComboBoxHandle, string[] Strings)
        {
            if (ComboBoxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            if (Strings is null || Strings.Length == 0)
            {
                throw new ArgumentNullException(nameof(Strings), "No strings were given");
            }
            int StringsSize = 0;
            foreach (string str in Strings)
            {
                StringsSize += (UnicodeEncoding.CharSize * str.Length) + 1;
            }
            IntPtr Result = SendMessage(ComboBoxHandle, CB_INITSTORAGE, new IntPtr(Strings.Length), new IntPtr(StringsSize));
            if (Result.ToInt32() != (int)ComboBoxMessageReturnValue.CB_ERRSPACE)
            {
                foreach (string str in Strings)
                {
                    using (SafeStringPointer StringPointer = new SafeStringPointer(str))
                    {
                        Result = SendMessage(ComboBoxHandle, CB_ADDSTRING, IntPtr.Zero, StringPointer);
                        if (Result.ToInt32() < 0)
                        {
                            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
                            {
                                continue;
                            }
                            else
                            {
                                throw new InsufficientMemoryException("Not enough space.");
                            }
                        }
                    }
                }
            }
            else
            {
                throw new InsufficientMemoryException("Not enough space.");
            }
        }

        /// <summary>
        /// Inserisce una stringa alla posizione indicata.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Index">Indice dove inserire la stringa.</param>
        /// <param name="String">Stringa da inserire.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InsufficientMemoryException"></exception>
        public static void InsertString(IntPtr ComboboxHandle, int Index, string String)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            if (string.IsNullOrWhiteSpace(String))
            {
                throw new ArgumentNullException(nameof(String), "The string is empty or null.");
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(String))
            {
                IntPtr Result = SendMessage(ComboboxHandle, CB_INSERTSTRING, new IntPtr(Index), StringPointer);
                if (Result.ToInt32() < 0)
                {
                    if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
                    {
                        throw new Win32Exception("Could not insert the string.");
                    }
                    else
                    {
                        throw new InsufficientMemoryException("Not enough space.");
                    }
                }
            }
        }

        /// <summary>
        /// Imposta il limite di caratteri inseribili dall'utente nel controllo di modifica di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al ComboBox.</param>
        /// <param name="LengthLimit">Limite di caratteri.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetTextLengthLimit(IntPtr ComboboxHandle, int LengthLimit)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (LengthLimit < 0)
            {
                throw new ArgumentException("Invalid length.", nameof(LengthLimit));
            }
            _ = SendMessage(ComboboxHandle, CB_LIMITTEXT, new IntPtr(LengthLimit), IntPtr.Zero);
        }

        /// <summary>
        /// Ripulisce il contenuto di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Clear(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            _ = SendMessage(ComboboxHandle, CB_RESETCONTENT, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Cerca una stringa in un Combobo che inizia con i caratteri presenti nella stringa indicata.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="StartingIndex">Indice di partenza.</param>
        /// <param name="Item">Stringa che contiene i caratteri da cercare.</param>
        /// <returns>Indice dell'elemento</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int SelectItem(IntPtr ComboboxHandle, int StartingIndex, string Item)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (StartingIndex < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(StartingIndex));
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(Item))
            {
                IntPtr Result = SendMessage(ComboboxHandle, CB_SELECTSTRING, new IntPtr(StartingIndex), StringPointer);
                if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
                {
                    throw new Win32Exception("Could not find the item.");
                }
                else
                {
                    return Result.ToInt32();
                }
            }
        }

        /// <summary>
        /// Imposta il suggerimento per un ComboBox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="CueBanner">Suggerimento.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetCueBanner(IntPtr ComboboxHandle, string CueBanner)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(CueBanner))
            {
                IntPtr Result = SendMessage(ComboboxHandle, CB_SETCUEBANNER, IntPtr.Zero, StringPointer);
                if (Result.ToInt32() != 1)
                {
                    throw new Win32Exception("Could notset the cue banner.");
                }
            }
        }

        /// <summary>
        /// Seleziona una stringa in un Combobox all'indice indicato.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Index">Indice da selezionare.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SelectIndex(IntPtr ComboboxHandle, int Index)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETCURSEL, new IntPtr(Index), IntPtr.Zero);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not select the index.");
            }
        }

        /// <summary>
        /// Imposta la larghezza minima del listbox di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Width">Larghezza minima.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetListboxMiminumAllowableWidth(IntPtr ComboboxHandle, int Width)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Width < 0)
            {
                throw new ArgumentException("Invalid width.", nameof(Width));
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETDROPPEDWIDTH, new IntPtr(Width), IntPtr.Zero);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not set mimimum width for the list box.");
            }
        }

        /// <summary>
        /// Seleziona i caratteri nel controllo di modifica di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="StartingIndex">Indice del primo carattere da selezionare.</param>
        /// <param name="EndingIndex">Indice dell'ultimo carattere da selezionare.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SelectCharacters(IntPtr ComboboxHandle, int StartingIndex, int EndingIndex)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (StartingIndex < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(StartingIndex));
            }
            if (EndingIndex < -1)
            {
                throw new ArgumentException("Invalid index.", nameof(EndingIndex));
            }
            IntPtr WindowStyles = GetWindowLongPtr(ComboboxHandle, (int)WindowInfo.GWL_STYLE);
            if (!((ComboBoxStyles)WindowStyles).HasFlag(ComboBoxStyles.CBS_DROPDOWNLIST))
            {
                throw new InvalidOperationException("Invalid style");
            }
            IntPtr Data = MAKEMESSAGEDATA((ushort)StartingIndex, (ushort)EndingIndex);
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETEDITSEL, IntPtr.Zero, Data);
            if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not select characters.");
            }
        }

        /// <summary>
        /// Abilita l'UI estesa per un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableExtendedUI(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETEXTENDEDUI, new IntPtr(1), IntPtr.Zero);
            if (Result.ToInt32() == (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not enable extended UI.");
            }
        }

        /// <summary>
        /// Disabilita l'UI estesa per un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableExtendedUI(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETEXTENDEDUI, IntPtr.Zero, IntPtr.Zero);
            if (Result.ToInt32() == (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not disable extended UI.");
            }
        }

        /// <summary>
        /// Imposta la larghezza di scorrimento del listbox di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Width">Larghezza scorrevole, in pixel.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetListboxScrollableWidth(IntPtr ComboboxHandle, int Width)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Width < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Width));
            }
            _ = SendMessage(ComboboxHandle, CB_SETHORIZONTALEXTENT, new IntPtr(Width), IntPtr.Zero);
        }

        /// <summary>
        /// Imposta i dati associati a un oggetto presente nel Combobox.
        /// </summary>
        /// <typeparam name="T">Tipo dell'oggetto.</typeparam>
        /// <param name="ComboboxHandle">Handle al ComboBox.</param>
        /// <param name="Index">Indice dell'oggetto.</param>
        /// <param name="AsPointer">Indica se il valore da impostare deve essere un puntatore a <paramref name="Data"/>.
        /// <param name="Data"/>Dati da associare.</param>
        /// <remarks>Questo metodo imposta il valore associato a un oggetto nel Combobox, questo valore può essere uno dei tipi primitivi, un enumerazione, una stringa o un puntatore a un oggetto gestito.<br/><br/>
        /// Se <paramref name="AsPointer"/> è true, i dati associati all'oggetto saranno un puntatore a <paramref name="Data"/>.<br/><br/>
        /// Se <typeparamref name="T"/> non è un tipo primitivo, un enumerazione o una stringa, <paramref name="Data"/> verrà considerato un oggetto gestito, i dati associati all'oggetto saranno un puntatore all'oggetto ottenuto tramite <see cref="GCHandle.ToIntPtr"/>.<br/><br/>
        /// I puntatori grezzi non sono supportati.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static void SetItemData<T>(IntPtr ComboboxHandle, int Index, T Data, bool AsPointer)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr NewData;
            if (Data != null)
            {
                Type ObjectType = typeof(T);
                if (ObjectType.IsEnum)
                {
                    ObjectType = ObjectType.GetEnumUnderlyingType();
                }
                if (ObjectType.IsPrimitive)
                {
                    switch (Type.GetTypeCode(ObjectType))
                    {
                        case TypeCode.Boolean:
                        case TypeCode.Byte:
                        case TypeCode.SByte:
                            if (!AsPointer)
                            {
                                NewData = new IntPtr((byte)(object)Data);
                            }
                            else
                            {
                                NewData = Marshal.AllocHGlobal(1);
                                Marshal.WriteByte(NewData, (byte)(object)Data);
                            }
                            break;
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Char:
                            if (!AsPointer)
                            {
                                NewData = new IntPtr((short)(object)Data);
                            }
                            else
                            {
                                NewData = Marshal.AllocHGlobal(2);
                                Marshal.WriteInt16(NewData, (short)(object)Data);
                            }
                            break;
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Single:
                            if (!AsPointer)
                            {
                                NewData = new IntPtr((int)(object)Data);
                            }
                            else
                            {
                                NewData = Marshal.AllocHGlobal(4);
                                Marshal.WriteInt32(NewData, (int)(object)Data);
                            }
                            break;
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Double:
                            if (!AsPointer)
                            {
                                NewData = new IntPtr((long)(object)Data);
                            }
                            else
                            {
                                NewData = Marshal.AllocHGlobal(8);
                                Marshal.WriteInt64(NewData, (long)(object)Data);
                            }
                            break;
                        default:
                            throw new NotSupportedException("Unsupported primitive type.");
                    }
                }
                else if (ObjectType.IsPointer)
                {
                    throw new NotSupportedException("Pointer types are not supported");
                }
                else if (ObjectType == typeof(IntPtr))
                {
                    if (!AsPointer)
                    {
                        NewData = (IntPtr)(object)Data;
                    }
                    else
                    {
                        NewData = Marshal.AllocHGlobal(IntPtr.Size);
                        Marshal.WriteIntPtr(NewData, (IntPtr)(object)Data);
                    }
                }
                else if (Type.GetTypeCode(ObjectType) is TypeCode.String)
                {
                    NewData = Marshal.StringToHGlobalUni((string)(object)Data);
                }
                else
                {
                    GCHandle ObjectHandle = GCHandle.Alloc(Data, GCHandleType.Normal);
                    NewData = GCHandle.ToIntPtr(ObjectHandle);
                }
                IntPtr Result = SendMessage(ComboboxHandle, CB_SETITEMDATA, new IntPtr(Index), NewData);
                if (Result.ToInt32() is (int)ComboBoxMessageReturnValue.CB_ERR)
                {
                    throw new Win32Exception("Could not set item data.");
                }
            }
        }


        public static void SetComponentHeight(IntPtr ComboboxHandle, ComboboxComponent Component, int Height, int Index = -1)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Height < 0)
            {
                throw new ArgumentException("Invalid height.", nameof(Height));
            }
            IntPtr WindowStyles = GetWindowLongPtr(ComboboxHandle, (int)WindowInfo.GWL_STYLE);
            bool ItemsHaveVariableHeight = ((ComboBoxStyles)WindowStyles.ToInt32()).HasFlag(ComboBoxStyles.CBS_OWNERDRAWVARIABLE);
            if (Component is ComboboxComponent.ListItems && ItemsHaveVariableHeight && Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            if (Component is ComboboxComponent.SelectionField)
            {
                Component = (ComboboxComponent)1;
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETITEMHEIGHT, new IntPtr((int)Component), new IntPtr(Height));
            if (Result.ToInt32() == (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not set component height.");
            }
        }

        /// <summary>
        /// Imposta la località di un ComboBox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="LocaleID">ID località.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetLocale(IntPtr ComboboxHandle, int LocaleID)
        {
            if (ComboboxHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_SETLOCALE, new IntPtr(LocaleID), IntPtr.Zero);
            if (Result.ToInt32() == (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                throw new Win32Exception("Could not set locale.");
            }
        }

        /// <summary>
        /// Imposta il numero di elementi minimo visibili nel listbox di un Combobox.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al Combobox.</param>
        /// <param name="MinimumVisibleItems">Numero minimo di elementi visibili.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static void SetDropdownMinimumVisibleItems(IntPtr ComboBoxHandle, int MinimumVisibleItems)
        {
            if (ComboBoxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboBoxHandle), "No handle provided.");
            }
            if (MinimumVisibleItems <= 0)
            {
                throw new ArgumentException("Invalid value.", nameof(MinimumVisibleItems));
            }
            IntPtr WindowStyles = GetWindowLongPtr(ComboBoxHandle, (int)WindowInfo.GWL_STYLE);
            bool NoIntegralHeight = ((ComboBoxStyles)WindowStyles.ToInt32()).HasFlag(ComboBoxStyles.CBS_NOINTEGRALHEIGHT);
            if (NoIntegralHeight)
            {
                throw new InvalidOperationException("Invalid styles.");
            }
            else
            {
                IntPtr Result = SendMessage(ComboBoxHandle, CB_SETMINVISIBLE, new IntPtr(MinimumVisibleItems), IntPtr.Zero);
            }
        }

        /// <summary>
        /// Rende visibile l'oggetto al indice indicato presente nel Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <param name="Index">Indice dell'oggetto.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void ShowItem(IntPtr ComboboxHandle, int Index)
        {
            if (ComboboxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            if (Index < 0)
            {
                throw new ArgumentException("Invalid index.", nameof(Index));
            }
            IntPtr Result = SendMessage(ComboboxHandle,CB_SETTOPINDEX, new IntPtr(Index), IntPtr.Zero);
            if (Result != IntPtr.Zero)
            {
                throw new Win32Exception("Could not show item.");
            }
        }

        /// <summary>
        /// Visualizza il dropdown di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al ComboBox.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ShowDropdown(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            _ = SendMessage(ComboboxHandle, CB_SHOWDROPDOWN, new IntPtr(1), IntPtr.Zero);
        }


        /// <summary>
        /// Nasconde il dropdown di un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al ComboBox.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void HideDropdown(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            _ = SendMessage(ComboboxHandle, CB_SHOWDROPDOWN, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Recupera l'indice del oggetto selezionato, se esiste, in un Combobox.
        /// </summary>
        /// <param name="ComboboxHandle">Handle al Combobox.</param>
        /// <returns>Indice dell'oggetto selezionato, -1 se nessun oggetto è selezionato.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetSelectedItemIndex(IntPtr ComboboxHandle)
        {
            if (ComboboxHandle != IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ComboboxHandle), "No handle provided.");
            }
            IntPtr Result = SendMessage(ComboboxHandle, CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
            if (Result.ToInt32() != (int)ComboBoxMessageReturnValue.CB_ERR)
            {
                return Result.ToInt32();
            }
            else
            {
                return -1;
            }
        }
    }
}