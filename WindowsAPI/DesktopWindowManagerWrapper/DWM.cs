using WindowsAPI.DesktopWindowManagerWrapper.Native;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WindowsAPI.DesktopWindowManagerWrapper.DataClasses;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMStructures;
using WindowsAPI.DiagnosticsWrapper.Native;
using System.ComponentModel;
using static WindowsAPI.DesktopWindowManagerWrapper.Enumerations;
using WindowsAPI.WindowsAndMessagesWrapper.Window;
using WindowsAPI.DiagnosticsWrapper.ProcessesAndThreads;
using WindowsAPI.WindowsGDIWrapper.DeviceContexts;
using static WindowsAPI.General.Native.GeneralStructures;
using WindowsAPI.General.Native;

namespace WindowsAPI.DesktopWindowManagerWrapper
{
    /// <summary>
    /// Permette l'accesso alle funzionalità di Desktop Window Manager.
    /// </summary>
    public static class DWM
    {
        /// <summary>
        /// Invia un messaggio a DWM.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra che ha ricevuto il messaggio.</param>
        /// <param name="Message">Messaggio.</param>
        /// <param name="MessageData">Dati del messaggio.</param>
        /// <returns>true se il messaggio è stato elaborato, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool SendMessageToDWM(IntPtr WindowHandle, uint Message, object MessageData)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            IntPtr Result;
            bool Processed;
            IntPtr StructurePointer;
            IntPtr wParam;
            switch (Message)
            {
                case DWMMessages.WM_NCHITTEST:
                    Point CursorCoordinates = (Point)MessageData;
                    IntPtr lParam = Macros.MAKEMESSAGEDATA((ushort)CursorCoordinates.X, (ushort)CursorCoordinates.Y);
                    Result = Marshal.AllocHGlobal(4);
                    Processed = DWMFunctions.DwmDefWindowProc(WindowHandle, Message, IntPtr.Zero, lParam, Result);
                    Marshal.FreeHGlobal(Result);
                    return Processed;
                case DWMMessages.WM_NCLBUTTONDBLCLK:
                case DWMMessages.WM_NCLBUTTONDOWN:
                case DWMMessages.WM_NCLBUTTONUP:
                case DWMMessages.WM_NCMBUTTONDBLCLK:
                case DWMMessages.WM_NCMBUTTONDOWN:
                case DWMMessages.WM_NCMBUTTONUP:
                case DWMMessages.WM_NCMOUSEHOVER:
                case DWMMessages.WM_NCMOUSEMOVE:
                case DWMMessages.WM_NCRBUTTONDBLCLK:
                case DWMMessages.WM_NCRBUTTONDOWN:
                case DWMMessages.WM_NCRBUTTONUP:
                    NonClientMouseMessageData Data = (NonClientMouseMessageData)MessageData;
                    wParam = new IntPtr((int)Data.HitTestResult);
                    StructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(POINTS)));
                    Marshal.StructureToPtr(Data.CoordinatesToStructure(), StructurePointer, false);
                    Result = Marshal.AllocHGlobal(4);
                    Processed = DWMFunctions.DwmDefWindowProc(WindowHandle, Message, wParam, StructurePointer, Result);
                    Marshal.FreeHGlobal(Result);
                    return Processed;
                case DWMMessages.WM_NCMOUSELEAVE:
                    Result = Marshal.AllocHGlobal(4);
                    Processed = DWMFunctions.DwmDefWindowProc(WindowHandle, Message, IntPtr.Zero, IntPtr.Zero, Result);
                    Marshal.FreeHGlobal(Result);
                    return Processed;
                case DWMMessages.WM_NCXBUTTONDBLCLK:
                case DWMMessages.WM_NCXBUTTONDOWN:
                case DWMMessages.WM_NCXBUTTONUP:
                    NonClientMouseMessageXButtonData Data2 = (NonClientMouseMessageXButtonData)MessageData;
                    wParam = Macros.MAKEMESSAGEDATA((ushort)Data2.HitTestResult, (ushort)Data2.Button);
                    StructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(POINTS)));
                    Marshal.StructureToPtr(Data2.CoordinatesToStructure(), StructurePointer, false);
                    Result = Marshal.AllocHGlobal(4);
                    Processed = DWMFunctions.DwmDefWindowProc(WindowHandle, Message, wParam, StructurePointer, Result);
                    Marshal.FreeHGlobal(Result);
                    return Processed;
                default: 
                    return false;
            }
        }

        /// <summary>
        /// Notifica DWM di partecipare alla programmazione del servizio MMCSS mentre il processo è in esecuzione.
        /// </summary>
        public static void OptInMMCSSScheduling()
        {
            uint Result = DWMFunctions.EnableMultimediaClassScheduleService(true);
            if (Result !=  HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Notifica DWM di interrompere la partecipazione alla programmazione del servizio MMCSS.
        /// </summary>
        public static void OptOutMMCSSScheduling()
        {
            uint Result = DWMFunctions.EnableMultimediaClassScheduleService(false);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Estende la cornice di una finestra nella sua area client.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="ExtensionMargins">Margini da usare per estendere la cornice.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void ExtendFrameIntoClientArea(IntPtr WindowHandle, WindowMargins ExtensionMargins)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            MARGINS MarginStructure = ExtensionMargins.ToStructure();
            uint Result = DWMFunctions.ExtendFrameIntoClientArea(WindowHandle, ref MarginStructure);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Invia una chiamata di pulizia (flush) che blocca il chiamante fino alla prossima presentazione, quando tutti gli aggiornamenti di superfici Microsoft DirectX da eseguire sono stati effettuati.
        /// </summary>
        public static void Flush()
        {
            uint Result = DWMFunctions.Flush();
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Recupera il colore attualmente utilizzato da DWM per la composizione vetro.
        /// </summary>
        /// <param name="IsOpaque">Indica se il colore è una miscela opaca.</param>
        /// <returns>Colore della composizione vetro.</returns>
        public static Color GetColorizationColor(out bool IsOpaque)
        {
            uint Result = DWMFunctions.GetColorizationColor(out uint ColorizationColor, out bool IsOpaqueBlend);
            if (Result == HRESULTErrorConstants.S_OK)
            {
                IsOpaque = IsOpaqueBlend;
                return Color.FromArgb((int)ColorizationColor);
            }
            else
            {
                IsOpaque = false;
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return Color.Empty;
            }
        }

        /// <summary>
        /// Recupera le tempistiche per la composizione DWM.
        /// </summary>
        /// <returns>Istanza di <see cref="DWMTimingInfo"/> con le informazioni.</returns>
        public static DWMTimingInfo GetCompositionTimingInfo()
        {
            DWM_TIMING_INFO Structure = new DWM_TIMING_INFO()
            {
                Size = (uint)Marshal.SizeOf(typeof(DWM_TIMING_INFO))
            };
            uint Result = DWMFunctions.GetCompositionTimingInfo(IntPtr.Zero, ref Structure);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                return new DWMTimingInfo(Structure);
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return null;
            }
        }

        /// <summary>
        /// Recupera gli attributi di trasporto DWM.
        /// </summary>
        /// <returns>Istanza di <see cref="DWMTransportAttributes"/> con le informazioni.</returns>
        public static DWMTransportAttributes GetTransportAttributes()
        {
            uint Result = DWMFunctions.GetTransportAttributes(out bool IsRemoting, out bool IsConnected, out uint Generation);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return null;
            }
            else
            {
                return new DWMTransportAttributes(IsRemoting, IsConnected, Generation);
            }
        }
        #region Window Attributes Getters
        /// <summary>
        /// Recupera lo stato del rendering dell'area non client di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>true se il rendering dell'area non client è abilitato, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static bool IsNonClientRenderingEnabled(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            uint Result = DWMFunctions.GetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_ENABLED, ValuePointer, 4);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                bool RenderingStatus = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return RenderingStatus;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return false;
            }
        }

        /// <summary>
        /// Recupera i bordi dell'area dei tasti di controllo nello spazio relativo alla finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Struttura <see cref="Rectangle"/> che definisce i bordi dell'area.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Rectangle GetCaptionButtonArea(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            int StructureSize = Marshal.SizeOf(typeof(RECT));
            IntPtr ValuePointer = Marshal.AllocHGlobal(StructureSize);
            uint Result = DWMFunctions.GetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_CAPTION_BUTTON_BOUNDS, ValuePointer, (uint)StructureSize);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                RECT Structure = new RECT();
                Marshal.PtrToStructure(ValuePointer, Structure);
                Marshal.FreeHGlobal(ValuePointer);
                Rectangle Rectangle = Rectangle.FromLTRB(Structure.Left, Structure.Top, Structure.Right, Structure.Bottom);
                return Rectangle;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// Recupera i bordi del rettangolo della cornice estesa.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Struttura <see cref="Rectangle"/> che definisce i bordi della cornice.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static Rectangle GetExtendedFrameBounds(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            int StructureSize = Marshal.SizeOf(typeof(RECT));
            IntPtr ValuePointer = Marshal.AllocHGlobal(StructureSize);
            uint Result = DWMFunctions.GetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_EXTENDED_FRAME_BOUNDS, ValuePointer, (uint)StructureSize);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                RECT Structure = new RECT();
                Marshal.PtrToStructure(ValuePointer, Structure);
                Marshal.FreeHGlobal(ValuePointer);
                Rectangle Rectangle = Rectangle.FromLTRB(Structure.Left, Structure.Top, Structure.Right, Structure.Bottom);
                return Rectangle;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// Recupera il motivo per cui una finestra è nascosta.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Uno dei valori dell'enumerazione <see cref="CloakReason"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static CloakReason GetWindowCloakReason(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            uint Result = DWMFunctions.GetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_CLOAKED, ValuePointer, 4);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                CloakReason CloakReason = (CloakReason)Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return CloakReason;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return CloakReason.Inherited;
            }
        }

        /// <summary>
        /// Recupera la larghezza del bordo esterno disegnato da DWM attorno alla finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>La larghezza del bordo.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetOuterBorderWidth(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            uint Result = DWMFunctions.GetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_VISIBLE_FRAME_BORDER_THICKNESS, ValuePointer, 4);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                int Width = Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return Width;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return -1;
            }
        }

        /// <summary>
        /// Recupera il materiale del fondale di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Uno dei valori dell'enumerazione <see cref="SystemBackdropType"/>.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static SystemBackdropType GetBackdropType(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            uint Result = DWMFunctions.GetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, ValuePointer, 4);
            if (Result is HRESULTErrorConstants.S_OK)
            {
                SystemBackdropType BackdropType = (SystemBackdropType)Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return BackdropType;
            }
            else
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return SystemBackdropType.LetDWMDecide;
            }
        }
        #endregion
        #region Window Attributes Setters
        /// <summary>
        /// Imposta i criteri di rendering dell'area non client di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Policy">Nuovo criterio da impostare.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void SetNonClientRenderingPolicy(IntPtr WindowHandle, NonClientRenderingPolicy Policy)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            if ((int)Policy > 2 || (int)Policy < 0)
            {
                throw new InvalidEnumArgumentException("Invalid policy value.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, (int)Policy);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_NCRENDERING_POLICY, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Abilita le transizioni DWM.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void EnableDWMTransitions(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_TRANSITIONS_FORCEDISABLED, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Disabilita le transizioni DWM.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void DisableDWMTransitions(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_TRANSITIONS_FORCEDISABLED, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Permette al contenuto renderizzato nell'area non client di essere visibile nella cornice disegnata da DWM.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void AllowNCContentVisibleOnFrame(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_ALLOW_NCPAINT, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Fa in modo che il contenuto renderizzato nell'area non client non sia visibile nella cornice disegnata da DWM.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void DisallowNCContentVisibleOnFrame(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_ALLOW_NCPAINT, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il contenuto non client come RTL (right-to-left) mirrored.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetNonClientContentAsRTLMirrored(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_NONCLIENT_RTL_LAYOUT, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il contenuto non client come non RTL (right-to-left) mirrored.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UnsetNonClientContentAsRTLMirrored(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_NONCLIENT_RTL_LAYOUT, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Forza una finestra a visualizzare una propria miniatura o una rappresentazione peek anche se uno snapshot o una rappresentazione in tempo reale è disponibile.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void ForceIconicThumbnailUsage(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_FORCE_ICONIC_REPRESENTATION, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Permette nuovamente a una finestra di utilizzare uno snapshot o una rappresentazione in tempo reale per se stessa se disponibile.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void CancelRequirementOfIconicThumbnailUsage(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_FORCE_ICONIC_REPRESENTATION, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Cambia come Flip3D tratta la finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Policy">Criterio che specifica il trattamento della finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void ChangeFlip3DPolicy(IntPtr WindowHandle, Flip3DWindowPolicy Policy)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            if ((int)Policy > 2 || (int)Policy < 0)
            {
                throw new InvalidEnumArgumentException("Invalid policy value.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, (int)Policy);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_FLIP3D_POLICY, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Notifica DWM se la finestra renderà disponibile una miniatura o una rappresentazione peek di se stessa.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="HasIconicBitmap">Indica se la finestra renderà disponibile una miniatura o una rappresentazione peek.</param>
        /// <param name="ForceIconicRepresentation">Indica se la finestra potrà usare solo la miniatura o la rappresentazione peek per se stessa.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void HasIconicBitmapNotify(IntPtr WindowHandle, bool HasIconicBitmap, bool ForceIconicRepresentation)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            DWMEnumerations.DWMWINDOWATTRIBUTE Attribute = DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_HAS_ICONIC_BITMAP;
            if (ForceIconicRepresentation)
            {
                Attribute |= DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_FORCE_ICONIC_REPRESENTATION;
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(HasIconicBitmap));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, Attribute, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Impedisce la visualizzazione dell'anteprima peek per una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void DisallowPeekPreview(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_DISALLOW_PEEK, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Permette la visualizzazione dell'anteprima peek per una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void AllowPeekPreview(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_DISALLOW_PEEK, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Impedisce a una finestra di diventare una lastra di vetro al momento dell'attivazione di peek.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void ExcludeFromPeek(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_EXCLUDED_FROM_PEEK, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Permette a una finestra di diventare una lastra di vetro al momento dell'attivazione di peek.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void IncludeInPeek(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_EXCLUDED_FROM_PEEK, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Nasconde la finestra all'utente.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void CloakWindow(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_CLOAK, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Rende visibile la finestra all'utente.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UncloakWindow(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_CLOAK, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Blocca l'aggiornamento della miniatura della finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void FreezeThumbnailImage(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_FREEZE_REPRESENTATION, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                if (Result is HRESULTErrorConstants.E_INVALIDARG)
                {
                    throw new ArgumentException("The window must be a UWP window.", nameof(WindowHandle));
                }
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Riprende l'aggiornamento della miniatura della finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UnfreezeThumbnailImage(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_FREEZE_REPRESENTATION, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                if (Result is HRESULTErrorConstants.E_INVALIDARG)
                {
                    throw new ArgumentException("The window must be a UWP window.", nameof(WindowHandle));
                }
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Abilita la modalità passiva per l'aggiornamento della finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void EnablePassiveUpdateMode(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_PASSIVE_UPDATE_MODE, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Disabilita la modalità passiva per l'aggiornamento della finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void DisablePassiveUpdateMode(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_PASSIVE_UPDATE_MODE, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Permette a una finestra non UWP di usare i pennelli per il fondale dell'host.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void AllowUsageOfHostBackdropBrushes(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_USE_HOSTBACKDROPBRUSH, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Impedisce a una finestra non UWP di usare i pennelli per il fondale dell'host.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void DisallowUsageOfHostBackdropBrushes(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_USE_HOSTBACKDROPBRUSH, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Permette alla cornice della finestra di essere disegnata usando i colori della modalità scura quando tale impostazione è attiva.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void EnableImmersiveDarkMode(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(true));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Impedisce alla cornice della finestra di essere disegnata usando i colori della modalità scura.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void DisableImmersiveDarkMode(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Convert.ToInt32(false));
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_USE_IMMERSIVE_DARK_MODE, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta la preferenza per gli angoli di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Setting">Nuova impostazione.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void SetWindowCornerSetting(IntPtr WindowHandle, WindowCornerPreference Setting)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            if ((int)Setting > 3 || (int)Setting < 0)
            {
                throw new InvalidEnumArgumentException("Invalid setting value.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, (int)Setting);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il colore del bordo della finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Color">Nuovo colore del bordo della finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetWindowBorderColor(IntPtr WindowHandle, Color Color)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            int Win32ColorValue = ColorTranslator.ToWin32(Color);
            Marshal.WriteInt32(ValuePointer, Win32ColorValue);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_BORDER_COLOR, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il colore della barra del titolo di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Color">Nuovo colore della barra del titolo.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetTitleBarColor(IntPtr WindowHandle, Color Color)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            int Win32ColorValue = ColorTranslator.ToWin32(Color);
            Marshal.WriteInt32(ValuePointer, Win32ColorValue);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_CAPTION_COLOR, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il colore del testo della barra del titolo.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Color">Nuovo colore del testo.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetTitleBarTextColor(IntPtr WindowHandle, Color Color)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            int Win32ColorValue = ColorTranslator.ToWin32(Color);
            Marshal.WriteInt32(ValuePointer, Win32ColorValue);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_TEXT_COLOR, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il materiale del fondale di una finestra disegnato dal sistema, incluso dietro all'area non client.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="BackdropType">Tipo di fondale.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void SetSystemBackdropMaterial(IntPtr WindowHandle, SystemBackdropType BackdropType)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "Invalid handle.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            if ((int)BackdropType > 4 || (int)BackdropType < 0)
            {
                throw new InvalidEnumArgumentException("Invalid setting value.");
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, (int)BackdropType);
            uint Result = DWMFunctions.SetWindowAttribute(WindowHandle, DWMEnumerations.DWMWINDOWATTRIBUTE.DWMWA_SYSTEMBACKDROP_TYPE, ValuePointer, 4);
            Marshal.FreeHGlobal(ValuePointer);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }
        #endregion
        /// <summary>
        /// Indica che tutte le miniature e le rappresentazioni peek fornite precedentemente non sono più valide.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void InvalidateIconicBitmaps(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!Windows.IsValidWindow(WindowHandle))
            {
                throw new ArgumentException(nameof(WindowHandle), "The handle does not reference a valid window.");
            }
            uint Result = DWMFunctions.InvalidateIconicBitmaps(WindowHandle);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Recupera la dimensione della fonte di una miniatura DWM.
        /// </summary>
        /// <param name="ThumbnailHandle">Handle alla miniatura.</param>
        /// <returns>Struttura <see cref="Size"/> che specifica la dimensione della fonte.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Size GetDWMThumbnailSourceSize(IntPtr ThumbnailHandle)
        {
            if (ThumbnailHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ThumbnailHandle), "No handle provided.");
            }
            uint Result = DWMFunctions.QueryThumbnailSourceSize(ThumbnailHandle, out SIZE Size);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return new Size();
            }
            else
            {
                Size ThumbnailSize = new Size()
                {
                    Width = Size.Width,
                    Height = Size.Height
                };
                return ThumbnailSize;
            }
        }

        /// <summary>
        /// Crea una relazione tra due finestre per la visualizzazione di una miniatura.
        /// </summary>
        /// <param name="DestinationWindow">Handle alla finestra di destinazione.</param>
        /// <param name="SourceWindow">Handle alla finestra di origine.</param>
        /// <param name="ThumbnailProperties">Istanza di <see cref="DWMThumbnailProperties"/> con le proprietà della miniatura.</param>
        /// <returns>Handle alla miniatura.</returns>
        /// <remarks>Se <paramref name="ThumbnailProperties"/> è nullo, è necessario chiamare <see cref="UpdateThumbnailProperties"/> e fornire un'istanza della classe per configurare la miniatura.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static IntPtr RegisterThumbnail(IntPtr DestinationWindow, IntPtr SourceWindow, DWMThumbnailProperties ThumbnailProperties = null)
        {
            if (DestinationWindow == IntPtr.Zero || SourceWindow == IntPtr.Zero)
            {
                throw new ArgumentNullException(string.Empty, "No handle provided.");
            }
            if (!Windows.IsValidWindow(DestinationWindow) || !Windows.IsValidWindow(SourceWindow))
            {
                throw new ArgumentException("The handle does not reference a valid window.");
            }
            IntPtr DesktopWindowHandle = Windows.GetDesktopWindowHandle();
            if (SourceWindow == DesktopWindowHandle)
            {
                throw new ArgumentException("The source window can't be the desktop window.", nameof(SourceWindow));
            }
            else
            {
                if (Windows.GetRootWindowHandle(SourceWindow) != SourceWindow)
                {
                    throw new ArgumentException("The handle must reference a top level window.", nameof(SourceWindow));
                }
            }
            if (DestinationWindow != DesktopWindowHandle)
            {
                if (Windows.GetRootWindowHandle(DestinationWindow) != DestinationWindow)
                {
                    throw new ArgumentException("The handle must reference a top level window.", nameof(SourceWindow));
                }
                if (Windows.GetWindowAssociatedProcessID(DestinationWindow) != ProcessesAndThreads.GetCurrentProcessID())
                {
                    throw new ArgumentException("The destination window must belong to the calling process or be the desktop window.", nameof(DestinationWindow));
                }
            }
            uint Result = DWMFunctions.RegisterThumbnailRelationship(DestinationWindow, SourceWindow, out IntPtr ThumbnailHandle);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return IntPtr.Zero;
            }
            else
            {
                if (ThumbnailProperties != null)
                {
                    UpdateThumbnailProperties(ThumbnailHandle, ThumbnailProperties); 
                }
                return ThumbnailHandle;
            }
        }

        /// <summary>
        /// Aggiorna le proprietà per una miniatura DWM.
        /// </summary>
        /// <param name="ThumbnailHandle">Handle alla miniatura.</param>
        /// <param name="Properties">Proprietà della miniatura.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UpdateThumbnailProperties(IntPtr ThumbnailHandle, DWMThumbnailProperties Properties)
        {
            if (ThumbnailHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ThumbnailHandle), "No handle provided.");
            }
            if (Properties == null)
            {
                throw new ArgumentNullException(nameof(Properties), "No data available to set thumbnail properties.");
            }
            DWM_THUMBNAIL_PROPERTIES PropertiesStructure = Properties.ToStructure();
            uint Result = DWMFunctions.UpdateThumbnailProperties(ThumbnailHandle, ref PropertiesStructure);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                if (Result == HRESULTErrorConstants.E_INVALIDARG)
                {
                    throw new ArgumentException("The handle references a thumbnail that is not owned by the current process or an invalid one.");
                }
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta un bitmap statico come live preview di una finestra o di una scheda.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Bitmap">Bitmap da usare.</param>
        /// <param name="DisplayFrameAroundBitmap">Indica se mostrare una cornice attorno al bitmap.</param>
        /// <param name="ClientRegionOffset">Offset della regione client della scheda dalla alla cornice della finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetIconicLivePreviewBitmap(IntPtr WindowHandle, Bitmap Bitmap, bool DisplayFrameAroundBitmap, Point? ClientRegionOffset = null)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (Bitmap == null)
            {
                throw new ArgumentNullException(nameof(Bitmap), "No bitmap provided.");
            }
            if (Windows.GetWindowAssociatedProcessID(WindowHandle) != ProcessesAndThreads.GetCurrentProcessID())
            {
                throw new ArgumentException("The window must belong to the calling process.", nameof(WindowHandle));
            }
            IntPtr BitmapHandle = Bitmap.GetHbitmap();
            IntPtr StructurePointer = IntPtr.Zero;
            if (ClientRegionOffset.HasValue)
            {
                POINT Offset = new POINT()
                {
                    x = ClientRegionOffset.Value.X,
                    y = ClientRegionOffset.Value.Y,
                };
                StructurePointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(POINT)));
                Marshal.StructureToPtr(Offset, StructurePointer, false);
            }
            uint Result = DWMFunctions.SetIconicLivePreviewBitmap(WindowHandle, BitmapHandle, StructurePointer, DisplayFrameAroundBitmap ? DWMConstants.DWM_SIT_DISPLAYFRAME : 0);
            if (StructurePointer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StructurePointer);
            }
            _ = DeviceContexts.DeleteObject(BitmapHandle);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta un bitmap statico da usare come miniatura di una finestra o di una scheda.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Bitmap">Bitmap da usare.</param>
        /// <param name="DisplayFrameAroundBitmap">Indica se mostrare una cornice attorno al bitmap.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void SetIconicThumbnail(IntPtr WindowHandle, Bitmap Bitmap, bool DisplayFrameAroundBitmap)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (Bitmap == null)
            {
                throw new ArgumentNullException(nameof(Bitmap), "No bitmap provided.");
            }
            if (Windows.GetWindowAssociatedProcessID(WindowHandle) != ProcessesAndThreads.GetCurrentProcessID())
            {
                throw new ArgumentException("The window must belong to the calling process.", nameof(WindowHandle));
            }
            IntPtr BitmapHandle = Bitmap.GetHbitmap();
            uint Result = DWMFunctions.SetIconicThumbnail(WindowHandle, BitmapHandle, DisplayFrameAroundBitmap ? DWMConstants.DWM_SIT_DISPLAYFRAME : 0);
            _ = DeviceContexts.DeleteObject(BitmapHandle);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }

        /// <summary>
        /// Imposta il tipo di feedback visuale da visualizzare in risposta a un particolare tocco o contatto di una penna.
        /// </summary>
        /// <param name="PointerID">ID del contatto.</param>
        /// <param name="Type">Tipo di contatto.</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public static void SetContactFeedback(int PointerID, ContactType Type)
        {
            if ((uint)Type < 16)
            {
                if ((uint)Type != 1)
                {
                    if ((uint)Type % 2 != 0)
                    {
                        throw new InvalidEnumArgumentException("Invalid contact type.");
                    }
                }
            }
            else if ((uint)Type > 16 && Type != ContactType.All)
            {
                throw new InvalidEnumArgumentException("Invalid contact type.");
            }
            uint Result = DWMFunctions.ShowContact((uint)PointerID, (DWMEnumerations.DWM_SHOWCONTACT)Type);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                if (Result is HRESULTErrorConstants.E_INVALIDARG)
                {
                    throw new InvalidOperationException("No contact with the given ID currently on screen.");
                }
                else
                {
                    Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                }
            }
        }

        /// <summary>
        /// Annulla la relazione tra due finestre.
        /// </summary>
        /// <param name="ThumbnailHandle">Handle alla miniatura.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void UnregisterThumbnailRelationshiop(IntPtr ThumbnailHandle)
        {
            if (ThumbnailHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ThumbnailHandle), "No handle provided.");
            }
            uint Result = DWMFunctions.UnregisterThumbnailRelationship(ThumbnailHandle);
            if (Result != HRESULTErrorConstants.S_OK)
            {
                if (Result == HRESULTErrorConstants.E_INVALIDARG)
                {
                    throw new ArgumentException("The handle references a thumbnail that is not owned by the current process or an invalid one.");
                }
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
            }
        }
    }
}