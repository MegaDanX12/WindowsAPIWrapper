using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons;
using static WindowsAPI.UserInterfaceElementsWrapper.Buttons.Enumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonFunctions;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonMessages;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonConstants;
using static WindowsAPI.WindowsAndMessagesWrapper.Messages.Messages;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;
using WindowsAPI.UserInterfaceElementsWrapper.Buttons.DataClasses;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses.WindowClassesFunctions;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses.WindowClassesEnumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.ImageLists.Enumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListFunctions;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListEnumerations;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapEnumerations;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapFunctions;
using static WindowsAPI.WindowsGDIWrapper.Native.DeviceContexts.DeviceContextsFunctions;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Icons.IconFunctions;
using WindowsAPI.SafeHandles;
using WindowsAPI.General.Native;

namespace WindowsAPI.UserInterfaceElementsWrapper.Buttons
{
    /// <summary>
    /// Metodi per l'interazione con i pulsanti.
    /// </summary>
    public static class Buttons
    {
        /// <summary>
        /// Cambia lo stato di selezione di un pulsante.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il pulsante.</param>
        /// <param name="ButtonID">Identificatore del pulsante da modificare.</param>
        /// <param name="State">Stato del pulsante.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void CheckDialogButton(IntPtr DialogHandle, int ButtonID, ButtonState State)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "The handle cannot be null.");
            }
            if (State != ButtonState.Checked && State != ButtonState.Unchecked && State != ButtonState.Indeterminate)
            {
                throw new InvalidEnumArgumentException("Invalid state.");
            }
            if (!ButtonFunctions.CheckDialogButton(DialogHandle, ButtonID, (ButtonEnumerations.ButtonState)State))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Seleziona un radio button in un gruppo e deseleziona tutti gli altri pulsanti appartenenti allo stesso gruppo.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il pulsante.</param>
        /// <param name="GroupFirstButtonID">ID del primo pulsante del gruppo.</param>
        /// <param name="GroupLastButtonID">ID dell'ultimo pulsante del gruppo.</param>
        /// <param name="ButtonID">ID del pulsante da selezionare.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void CheckRadioButton(IntPtr DialogHandle, int GroupFirstButtonID, int GroupLastButtonID, int ButtonID)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "The handle cannot be null.");
            }
            if (!ButtonFunctions.CheckRadioButton(DialogHandle, GroupFirstButtonID, GroupLastButtonID, ButtonID))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Determina lo stato di un pulsante.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il pulsante.</param>
        /// <param name="ButtonID">ID del pulsante.</param>
        /// <returns>Uno dei seguenti valori dell'enumerazione <see cref="ButtonState"/>:<br/><br/>
        /// <see cref="ButtonState.Checked"/><br/>
        /// <see cref="ButtonState.Indeterminate"/><br/>
        /// <see cref="ButtonState.Unchecked"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ButtonState GetButtonState(IntPtr DialogHandle, int ButtonID)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "The handle cannot be null.");
            }
            return (ButtonState)IsDialogButtonChecked(DialogHandle, ButtonID);
        }

        /// <summary>
        /// Recupera la dimensione ideale di un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="DesiredWidth">Larghezza desiderata del pulsante.</param>
        /// <returns>Struttura <see cref="Size"/> che indica la dimensione ideale del pulsante.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static Size GetButtonIdealSize(IntPtr ButtonHandle, int DesiredWidth = 0)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            if (DesiredWidth < 0)
            {
                throw new ArgumentException("Invalid width.", nameof(DesiredWidth));
            }
            SIZE IdealSize = new SIZE()
            {
                X = DesiredWidth
            };
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(IdealSize)))
            {
                StructurePointer.WriteToMemory<SIZE>(IdealSize);
                IntPtr Result = SendMessage(ButtonHandle, BCM_GETIDEALSIZE, IntPtr.Zero, StructurePointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not get button ideal size");
                }
                else
                {
                    IdealSize = StructurePointer.ReadFromMemory<SIZE>();
                    return new Size(IdealSize.X, IdealSize.Y);
                }
            }
        }

        /// <summary>
        /// Recupera informazioni sulla lista immagini associata a un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <returns>Istanza di <see cref="ImageListInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static ImageListInfo GetAssociatedImageList(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            BUTTON_IMAGELIST ImageListInfoStructure = new BUTTON_IMAGELIST();
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(ImageListInfoStructure)))
            {
                StructurePointer.WriteToMemory<BUTTON_IMAGELIST>(ImageListInfoStructure);
                IntPtr Result = SendMessage(ButtonHandle, BCM_GETIMAGELIST, IntPtr.Zero, StructurePointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not get image list");
                }
                else
                {
                    ImageListInfoStructure = StructurePointer.ReadFromMemory<BUTTON_IMAGELIST>();
                    return new ImageListInfo(ImageListInfoStructure);
                }
            }
        }

        /// <summary>
        /// Recupera la nota associata a un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <returns>Il testo della nota, <see cref="string.Empty"/> se non esiste una nota.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static string GetAssociatedNote(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr WindowStyles = GetWindowLongPtr(ButtonHandle, (int)WindowInfo.GWL_STYLE);
            if (WindowStyles != IntPtr.Zero)
            {
                ButtonEnumerations.ButtonStyles Styles = (ButtonEnumerations.ButtonStyles)WindowStyles.ToInt32();
                if (!Styles.HasFlag(ButtonEnumerations.ButtonStyles.BS_COMMANDLINK) && !Styles.HasFlag(ButtonEnumerations.ButtonStyles.BS_DEFCOMMANDLINK))
                {
                    throw new InvalidOperationException("The button does not have the required style.");
                }
            }
            IntPtr Result = SendMessage(ButtonHandle, BCM_GETNOTELENGTH, IntPtr.Zero, IntPtr.Zero);
            int NoteLength = Result.ToInt32();
            if (NoteLength == 0)
            {
                return string.Empty;
            }
            else
            {
                using (SafeStringPointer Buffer = new SafeStringPointer(NoteLength + 1))
                {
                    Result = SendMessage(ButtonHandle, BCM_GETNOTE, new IntPtr(NoteLength + 1), Buffer);
                    if (Convert.ToBoolean(Result.ToInt32()))
                    {
                        string Note = Buffer.ReadFromMemory();
                        return Note;
                    }
                    else
                    {
                        throw new Win32Exception("Could not get the note text.");
                    }
                }
            }
        }

        /// <summary>
        /// Recupera informazioni su uno split button.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="RequiredInfo">Informazioni richieste.</param>
        /// <returns>Istanza di <see cref="SplitButtonInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static SplitButtonInfo GetSplitButtonInfo(IntPtr ButtonHandle, SplitButtonRequiredInfo RequiredInfo)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr WindowStyles = GetWindowLongPtr(ButtonHandle, (int)WindowInfo.GWL_STYLE);
            if (WindowStyles != IntPtr.Zero)
            {
                ButtonEnumerations.ButtonStyles Styles = (ButtonEnumerations.ButtonStyles)WindowStyles.ToInt32();
                if (!Styles.HasFlag(ButtonEnumerations.ButtonStyles.BS_SPLITBUTTON) && !Styles.HasFlag(ButtonEnumerations.ButtonStyles.BS_DEFSPLITBUTTON))
                {
                    throw new InvalidOperationException("The button does not have the required style.");
                }
            }
            BUTTON_SPLITINFO SplitButtonInfoStructure = new BUTTON_SPLITINFO()
            {
                ValidMembers = ButtonEnumerations.SplitInfoMembers.BCSIF_GLYPH | ButtonEnumerations.SplitInfoMembers.BCSIF_IMAGE
            };
            if (RequiredInfo.HasFlag(SplitButtonRequiredInfo.GlyphSize))
            {
                SplitButtonInfoStructure.ValidMembers |= ButtonEnumerations.SplitInfoMembers.BCSIF_SIZE;
            }
            if (RequiredInfo.HasFlag(SplitButtonRequiredInfo.Style))
            {
                SplitButtonInfoStructure.ValidMembers |= ButtonEnumerations.SplitInfoMembers.BCSIF_STYLE;
            }
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(SplitButtonInfoStructure)))
            {
                StructurePointer.WriteToMemory<BUTTON_SPLITINFO>(SplitButtonInfoStructure);
                IntPtr Result = SendMessage(ButtonHandle, BCM_GETSPLITINFO, IntPtr.Zero, StructurePointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not get information on the split button");
                }
                else
                {
                    SplitButtonInfoStructure = StructurePointer.ReadFromMemory<BUTTON_SPLITINFO>();
                    return new SplitButtonInfo(SplitButtonInfoStructure);
                }
            }
        }

        /// <summary>
        /// Recupera i margini usati per disegnare il testo in un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <returns>Struttura <see cref="Rectangle"/> che indica i margini.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static Rectangle GetButtonTextMargins(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            RECT TextMargins = new RECT();
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(TextMargins)))
            {
                StructurePointer.WriteToMemory<RECT>(TextMargins);
                IntPtr Result = SendMessage(ButtonHandle, BCM_GETTEXTMARGIN, IntPtr.Zero, StructurePointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not get text margins");
                }
                else
                {
                    TextMargins = StructurePointer.ReadFromMemory<RECT>();
                    return Rectangle.FromLTRB(TextMargins.Left, TextMargins.Top, TextMargins.Right, TextMargins.Bottom);
                }
            }
        }

        /// <summary>
        /// Imposta lo stato del dropdown di un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="IsOpen">Indica se il dropdown è aperto o meno.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetDropdownState(IntPtr ButtonHandle, bool IsOpen)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr Result = SendMessage(ButtonHandle, BCM_SETDROPDOWNSTATE, new IntPtr(Convert.ToInt32(IsOpen)), IntPtr.Zero);
            if (!Convert.ToBoolean(Result.ToInt32()))
            {
                throw new Win32Exception("Could not set the dropdown state.");
            }
        }

        /// <summary>
        /// Imposta la lista di immagini per un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="Width">Larghezza, in pixel, di ogni immagine.</param>
        /// <param name="Height">Altezza, in pixel, di ogni immagine.</param>
        /// <param name="Options">Opzioni di creazione della lista.</param>
        /// <param name="InitialImagesFile">Percorso di un bitmap che contiene tutte le immagini iniziali da inserire.</param>
        /// <param name="MaskFile">Percorso di un bitmap da usare come maschera.</param>
        /// <param name="InitialImagesCount">Numero di immagini inizialmente presenti nella lista.</param>
        /// <param name="GrowSize">Numero di immagini che possono essere aggiunte alla lista.</param>
        /// <param name="IconMargin">Margine attorno all'icona.</param>
        /// <param name="Alignment">Allineamento delle immagini.</param>
        /// <remarks><paramref name="InitialImagesFile"/> può essere nullo, in questo caso, tutti gli altri parametri, esclusi <paramref name="ButtonHandle"/>, <paramref name="IconMargin"/> e <paramref name="Alignment"/>, non sono utilizzati.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetImageList(IntPtr ButtonHandle, int Width, int Height, ImageListCreationOptions? Options, string InitialImagesFile, string MaskFile, int InitialImagesCount, int GrowSize, Rectangle IconMargin, ButtonImageAlignment Alignment)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            if (Width <= 0 || Height <= 0)
            {
                throw new ArgumentException("Width and Height cannot be less than or equal to 0.");
            }
            if (GrowSize < 0)
            {
                throw new ArgumentException("The grow size cannot be less than 0.", nameof(GrowSize));
            }
            if (string.IsNullOrWhiteSpace(InitialImagesFile))
            {
                BUTTON_IMAGELIST ImageListInfo = new BUTTON_IMAGELIST()
                {
                    ImagelistHandle = BCCL_NOGLYPH,
                    Margin = new RECT()
                    {
                        Left = IconMargin.X,
                        Top = IconMargin.Y,
                        Right = IconMargin.Right,
                        Bottom = IconMargin.Bottom
                    },
                    Alignment = (ButtonEnumerations.ImagelistAlignment)Alignment
                };
                using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(ImageListInfo)))
                {
                    StructurePointer.WriteToMemory<BUTTON_IMAGELIST>(ImageListInfo);
                    IntPtr Result = SendMessage(ButtonHandle, BCM_SETIMAGELIST, IntPtr.Zero, StructurePointer);
                    if (!Convert.ToBoolean(Result.ToInt32()))
                    {
                        throw new Win32Exception("Could not associate an image list with the button");
                    }
                }
            }
            else
            {
                if (!Options.HasValue)
                {
                    throw new ArgumentNullException(nameof(Options), "The parameter must have a value if InitialImagesFile is not null or empty.");
                }
                else
                {
                    SafeImageListHandle ImageListHandle = new SafeImageListHandle(CreateImageList(Width, Height, (ImageListCreationFlags)Options, InitialImagesCount, GrowSize), false);
                    if (!ImageListHandle.IsInvalid)
                    {
                        using (SafeBitmapHandle BitmapHandle = new SafeBitmapHandle(LoadImage(IntPtr.Zero, InitialImagesFile, ImageType.IMAGE_BITMAP, 0, 0, LoadingOptions.LR_LOADFROMFILE), true))
                        {
                            if (BitmapHandle.IsInvalid)
                            {
                                throw new Win32Exception("Could not load initial images.");
                            }
                            else
                            {
                                SafeBitmapHandle MaskHandle = null;
                                if (Options.Value.HasFlag(ImageListCreationOptions.UseMask))
                                {
                                    if (string.IsNullOrWhiteSpace(MaskFile))
                                    {
                                        throw new ArgumentNullException(nameof(MaskFile), "No mask image specified.");
                                    }
                                    else
                                    {
                                        MaskHandle = new SafeBitmapHandle(LoadImage(IntPtr.Zero, MaskFile, ImageType.IMAGE_BITMAP, 0, 0, LoadingOptions.LR_LOADFROMFILE | LoadingOptions.LR_MONOCHROME), true);
                                        if (MaskHandle.IsInvalid)
                                        {
                                            throw new Win32Exception("Could not load mask.");
                                        }
                                    }
                                }
                                int NewImageIndex = AddImage(ImageListHandle, BitmapHandle, MaskHandle);
                                if (NewImageIndex != -1)
                                {
                                    BUTTON_IMAGELIST ImageListInfo = new BUTTON_IMAGELIST()
                                    {
                                        ImagelistHandle = ImageListHandle,
                                        Margin = new RECT()
                                        {
                                            Left = IconMargin.X,
                                            Top = IconMargin.Y,
                                            Right = IconMargin.Right,
                                            Bottom = IconMargin.Bottom
                                        },
                                        Alignment = (ButtonEnumerations.ImagelistAlignment)Alignment
                                    };
                                    using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(ImageListInfo)))
                                    {
                                        StructurePointer.WriteToMemory<BUTTON_IMAGELIST>(ImageListInfo);
                                        IntPtr Result = SendMessage(ButtonHandle, BCM_SETIMAGELIST, IntPtr.Zero, StructurePointer);
                                        if (!MaskHandle.IsInvalid)
                                        {
                                            MaskHandle.Dispose();
                                        }
                                        if (!Convert.ToBoolean(Result.ToInt32()))
                                        {
                                            throw new Win32Exception("Could not associate an image list with the button");
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Win32Exception("Could not add images to image list.");
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Win32Exception("Could not create image list.");
                    }
                }
            }
        }

        /// <summary>
        /// Imposta la nota per un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="Note">Nota da impostare.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetNote(IntPtr ButtonHandle, string Note)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            using (SafeStringPointer StringPointer = new SafeStringPointer(Note))
            {
                IntPtr Result = SendMessage(ButtonHandle, BCM_SETNOTE, IntPtr.Zero, StringPointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not set the note text");
                }
            }
        }

        /// <summary>
        /// Indica che l'operazione associata al pulsante richiede diritti amministrativi.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableRequireElevationStatus(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr Result = SendMessage(ButtonHandle, BCM_SETSHIELD, IntPtr.Zero, new IntPtr(1));
            if (Result.ToInt32() != 1)
            {
                throw new Win32Exception(Result.ToInt32());
            }
        }

        /// <summary>
        /// Indica che l'operazione associata al pulsante non richiede diritti amministrativi.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableRequireElevationStatus(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr Result = SendMessage(ButtonHandle, BCM_SETSHIELD, IntPtr.Zero, IntPtr.Zero);
            if (Result.ToInt32() != 1)
            {
                throw new Win32Exception(Result.ToInt32());
            }
        }

        /// <summary>
        /// Imposta i dati di uno split button.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="ButtonInfo">Istanza di <see cref="SplitButtonInfo"/> con le informazioni.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetSplitButtonInfo(IntPtr ButtonHandle, SplitButtonInfo ButtonInfo)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr WindowStyles = GetWindowLongPtr(ButtonHandle, (int)WindowInfo.GWL_STYLE);
            if (WindowStyles != IntPtr.Zero)
            {
                ButtonEnumerations.ButtonStyles Styles = (ButtonEnumerations.ButtonStyles)WindowStyles.ToInt32();
                if (!Styles.HasFlag(ButtonEnumerations.ButtonStyles.BS_SPLITBUTTON) && !Styles.HasFlag(ButtonEnumerations.ButtonStyles.BS_DEFSPLITBUTTON))
                {
                    throw new InvalidOperationException("The button does not have the required style.");
                }
            }
            BUTTON_SPLITINFO SplitInfoStructure = ButtonInfo.ToStructure();
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(SplitInfoStructure)))
            {
                StructurePointer.WriteToMemory<BUTTON_SPLITINFO>(SplitInfoStructure);
                IntPtr Result = SendMessage(ButtonHandle, BCM_SETSPLITINFO, IntPtr.Zero, StructurePointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not set split button data");
                }
            }
        }

        /// <summary>
        /// Imposta i margini di disegno del testo.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="Margins">Margini.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetTextMargins(IntPtr ButtonHandle, Rectangle Margins)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            RECT TextMargins = new RECT()
            {
                Left = Margins.X,
                Top = Margins.Y,
                Right = Margins.Right,
                Bottom = Margins.Bottom
            };
            using (SafeStructPointer StructurePointer = new SafeStructPointer(Marshal.SizeOf(TextMargins)))
            {
                StructurePointer.WriteToMemory<RECT>(TextMargins);
                IntPtr Result = SendMessage(ButtonHandle, BCM_SETTEXTMARGIN, IntPtr.Zero, StructurePointer);
                if (!Convert.ToBoolean(Result.ToInt32()))
                {
                    throw new Win32Exception("Could not set text margins");
                }
            }
        }

        /// <summary>
        /// Simula il click del pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        public static void SimulateClick(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            _ = SendMessage(ButtonHandle, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Recupera l'immagine associata al pulsante come bitmap.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <returns>Istanza di <see cref="Bitmap"/> che contiene l'immagine del pulsante.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Bitmap GetButtonBitmap(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            using (SafeBitmapHandle BitmapHandle = new SafeBitmapHandle(SendMessage(ButtonHandle, BM_GETIMAGE, new IntPtr((int)ImageType.IMAGE_BITMAP), IntPtr.Zero), true))
            {
                if (!BitmapHandle.IsInvalid)
                {
                    return BitmapHandle.GetBitmap();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Recupera l'immagine associata al pulsante come icona.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <returns>Istanza di <see cref="Icon"/> che contiene l'immagine del pulsante.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Icon GetButtonIcon(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            using (SafeIconHandle IconHandle = new SafeIconHandle(SendMessage(ButtonHandle, BM_GETIMAGE, new IntPtr((int)ImageType.IMAGE_ICON), IntPtr.Zero), true))
            {
                if (!IconHandle.IsInvalid)
                {
                    return IconHandle.GetIcon();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Recupera lo stato di un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <returns>Istanza di <see cref="ButtonStateInfo"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static ButtonStateInfo GetButtonState(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr Result = SendMessage(ButtonHandle, BM_GETSTATE, IntPtr.Zero, IntPtr.Zero);
            return new ButtonStateInfo((ButtonEnumerations.ButtonState)Result.ToInt32());
        }

        /// <summary>
        /// Previene la generazione dell'evento Click per un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void PreventClickedMessageSend(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            _ = SendMessage(ButtonHandle, BM_SETDONTCLICK, new IntPtr(1), IntPtr.Zero);
        }

        /// <summary>
        /// Permette la generazione dell'evento Click per un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AllowClickedMessageSend(IntPtr ButtonHandle)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            _ = SendMessage(ButtonHandle, BM_SETDONTCLICK, new IntPtr(0), IntPtr.Zero);
        }

        /// <summary>
        /// Imposta l'immagine associata a un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="Image">Immagine da impostare.</param>
        /// <param name="Type">Tipo di immagine.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetButtonImage(IntPtr ButtonHandle, object Image, ButtonImageType Type)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            IntPtr Result;
            switch (Type)
            {
                case ButtonImageType.Bitmap:
                    SafeBitmapHandle BitmapHandle = new SafeBitmapHandle(((Bitmap)Image).GetHbitmap(), false);
                    Result = SendMessage(ButtonHandle, BM_SETIMAGE, new IntPtr((int)ImageType.IMAGE_BITMAP), BitmapHandle);
                    if (Result != IntPtr.Zero)
                    {
                        _ = DeleteObject(Result);
                    }
                    break;
                case ButtonImageType.Icon:
                    SafeIconHandle IconHandle = new SafeIconHandle(((Icon)Image).Handle, false);
                    Result = SendMessage(ButtonHandle, BM_SETIMAGE, new IntPtr((int)ImageType.IMAGE_ICON), IconHandle);
                    if (Result != IntPtr.Zero)
                    {
                        _ = DestroyIcon(Result);
                    }
                    break;
            }
        }

        /// <summary>
        /// Imposta lo stato di evidenziazione di un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="Highlight">Indica se il pulsante deve essere evidenziato.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetHighlightState(IntPtr ButtonHandle, bool Highlight)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            _ = SendMessage(ButtonHandle, BM_SETSTATE, new IntPtr(Convert.ToInt32(Highlight)), IntPtr.Zero);
        }

        /// <summary>
        /// Imposta gli stili di un pulsante.
        /// </summary>
        /// <param name="ButtonHandle">Handle al pulsante.</param>
        /// <param name="Style">Stile del pulsante.</param>
        /// <param name="RedrawButton">Indica se ridisegnare il pulsante.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetButtonStyles(IntPtr ButtonHandle, ButtonStyles Style, bool RedrawButton)
        {
            if (ButtonHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ButtonHandle), "The handle cannot be null.");
            }
            _ = SendMessage(ButtonHandle, BM_SETSTYLE, new IntPtr(Convert.ToInt32(Style)), Macros.MAKEMESSAGEDATA(Convert.ToUInt16(RedrawButton), 0));
        }
    }
}