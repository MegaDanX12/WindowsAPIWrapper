using WindowsAPI.AccessibilityWrapper.DataClasses;
using WindowsAPI.AccessibilityWrapper.Native;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static WindowsAPI.AccessibilityWrapper.Enumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityStructures;
using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityFunctions;
using static WindowsAPI.DiagnosticsWrapper.Native.Win32ErrorConstants;

namespace WindowsAPI.AccessibilityWrapper
{
    /// <summary>
    /// Funzionalità accessibilità.
    /// </summary>
    public static class Accessibility
    {
        /// <summary>
        /// Registra una finestra come bersaglio per puntatori del tipo specificato.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="PointerType">Tipo di puntatore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        public static bool RegisterPointerInputTarget(IntPtr WindowHandle, PointerType PointerType)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentException("The handle is not valid.", nameof(WindowHandle));
            }
            if (PointerType is PointerType.All || PointerType is PointerType.Mouse)
            {
                throw new ArgumentException("Invalid pointer type");
            }
            if (!AccessibilityFunctions.RegisterPointerInputTarget(WindowHandle, (POINTER_INPUT_TYPE)PointerType))
            {
                if (Marshal.GetLastWin32Error() is ERROR_ACCESS_DENIED)
                {
                    throw new UnauthorizedAccessException("The calling thread does not have UI access privilege, does not own the window or it is already registered as a target for the specified input.");
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Annulla la registrazione di una finestra come bersaglio per puntatori del tipo specificato.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="PointerType">Tipo di puntatore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        public static bool UnregisterPointerInputTarget(IntPtr WindowHandle, PointerType PointerType)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentException("The handle is not valid.", nameof(WindowHandle));
            }
            if (PointerType is PointerType.All || PointerType is PointerType.Mouse)
            {
                throw new ArgumentException("Invalid pointer type");
            }
            if (!AccessibilityFunctions.UnregisterPointerInputTarget(WindowHandle, (POINTER_INPUT_TYPE)PointerType))
            {
                if (Marshal.GetLastWin32Error() is ERROR_ACCESS_DENIED)
                {
                    throw new UnauthorizedAccessException("The calling thread does not have UI access privilege or it does not own the window.");
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                return true;
            }           
        }

        /// <summary>
        /// Recupera le informazioni sul timeout delle funzionalità di accessibilità.
        /// </summary>
        /// <returns>Un'istanza di <see cref="AccessTimeoutInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static AccessTimeoutInfo GetAccessibilityTimeoutInfo()
        {
            ACCESSTIMEOUT Structure = new ACCESSTIMEOUT()
            {
                Size = (uint)Marshal.SizeOf(typeof(ACCESSTIMEOUT))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETACCESSTIMEOUT, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new AccessTimeoutInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta il timeout delle funzionalità di accessibilità.
        /// </summary>
        /// <param name="TimeoutInfo">Nuove impostazioni del timeout.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetAccessibilityTimeoutParameters(AccessTimeoutInfo TimeoutInfo, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            ACCESSTIMEOUT Structure = TimeoutInfo.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETACCESSTIMEOUT, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sulle descrizioni audio.
        /// </summary>
        /// <returns>Un'istanza di <see cref="AudioDescriptionInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static AudioDescriptionInfo GetAudioDescriptionInfo()
        {
            AUDIODESCRIPTION Structure = new AUDIODESCRIPTION()
            {
                Size = (uint)Marshal.SizeOf(typeof(AUDIODESCRIPTION))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETAUDIODESCRIPTION, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new AudioDescriptionInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta le descrizioni audio.
        /// </summary>
        /// <param name="Data">Nuovi parametri delle descrizioni audio.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetAudioDescriptionParameters(AudioDescriptionInfo Data, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            AUDIODESCRIPTION Structure = Data.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETAUDIODESCRIPTION, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera lo stato delle animazioni dell'area client.
        /// </summary>
        /// <returns>true se le animazioni sono attive, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsClientAreaAnimationEnabled()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETCLIENTAREAANIMATION, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Abilita le animazioni dell'area client.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableClientAreaAnimations(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(1);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETCLIENTAREAANIMATION, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Disabilita le animazioni dell'area client.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableClientAreaAnimations(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETCLIENTAREAANIMATION, 0, IntPtr.Zero, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera lo stato del contenuto sovrapposto.
        /// </summary>
        /// <returns>true se il contenuto sovrapposto è abilitato, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsOverlappedContentEnabled()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETDISABLEOVERLAPPEDCONTENT, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Abilita il contenuto sovrapposto.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableOverlappedContent(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(0);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETDISABLEOVERLAPPEDCONTENT, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Disabilita il contenuto sovrapposto.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableOverlappedContent(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(1);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETDISABLEOVERLAPPEDCONTENT, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sulla funzionalità Filtro tasti.
        /// </summary>
        /// <returns>Un'istanza di <see cref="FilterKeysInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static FilterKeysInfo GetFilterKeysInfo()
        {
            FILTERKEYS Structure = new FILTERKEYS()
            {
                Size = (uint)Marshal.SizeOf(typeof(FILTERKEYS))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETFILTERKEYS, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new FilterKeysInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta i nuovi parametri per la funzionalità Filtro tasti.
        /// </summary>
        /// <param name="FilterKeysInfo">Istanza di <see cref="FilterKeysInfo"/> con i nuovi parametri.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetFilterKeysParameters(FilterKeysInfo FilterKeysInfo, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            FILTERKEYS Structure = FilterKeysInfo.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETFILTERKEYS, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera l'altezza del rettangolo di focus.
        /// </summary>
        /// <returns>Altezza, in pixel, del rettangolo di focus.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static int GetFocusBorderHeight()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETFOCUSBORDERHEIGHT, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                int Value = Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Recupera la larghezza del rettangolo di focus.
        /// </summary>
        /// <returns>Larghezza, in pixel, del rettangolo di focus.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static int GetFocusBorderWidth()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETFOCUSBORDERWIDTH, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                int Value = Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Imposta l'altezza e la larghezza dei bordi del rettangolo di focus.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <param name="Height">Altezza del rettangolo.</param>
        /// <param name="Width">Larghezza del rettangolo.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetFocusRectangleBorders(bool UpdateUserProfile, bool NotifyUpdate, int? Height = null, int? Width = null)
        {
            if (!Height.HasValue && !Width.HasValue)
            {
                throw new ArgumentNullException(string.Empty, "At least one of the parameters must have a valid value.");
            }
            if ((Height.HasValue && Height < 0) || (Width.HasValue && Width < 0))
            {
                throw new ArgumentOutOfRangeException(string.Empty, "The values of the parameters can't be less than zero.");
            }
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr BorderHeight = Height.HasValue && Height > 0 ? new IntPtr(Height.Value) : IntPtr.Zero;
            IntPtr BorderWidth = Width.HasValue && Width > 0 ? new IntPtr(Width.Value) : IntPtr.Zero;
            if (BorderHeight != IntPtr.Zero)
            {
                if (!SystemParametersInfo(AccessibilityParameter.SPI_SETFOCUSBORDERHEIGHT, 0, BorderHeight, Option))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            if (BorderWidth != IntPtr.Zero)
            {
                if (!SystemParametersInfo(AccessibilityParameter.SPI_SETFOCUSBORDERWIDTH, 0, BorderWidth, Option))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Recupera informazioni sulla funzionalità Alto constrasto.
        /// </summary>
        /// <returns>Istanza di <see cref="HighContrastInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static HighContrastInfo GetHighContrastInfo()
        {
            HIGHCONTRAST Structure = new HIGHCONTRAST()
            {
                Size = (uint)Marshal.SizeOf(typeof(HIGHCONTRAST))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETHIGHCONTRAST, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new HighContrastInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta i parametri della funzionalità Alto constasto.
        /// </summary>
        /// <param name="Info">Istanza di <see cref="HighContrastInfo"/> con i nuovi parametri.</param>
        /// <param name="NoThemeChange">Indica se il cambio delle impostazioni deve causare effetti di cambio tema.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetHighContrastInfo(HighContrastInfo Info, bool NoThemeChange, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            if (NoThemeChange && Info.IsEnabled)
            {
                throw new ArgumentException("Feature must be disabled to prevent theme change effects.", nameof(NoThemeChange));
            }
            HIGHCONTRAST Structure = Info.ToStruct(NoThemeChange);
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETHIGHCONTRAST, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera la durata, in secondi, delle notifiche popup.
        /// </summary>
        /// <returns>La durata delle notifiche.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static int GetPopupDuration()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETMESSAGEDURATION, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                int Value = Marshal.ReadInt32(ValuePointer);
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Imposta la durata, in secondi, dei popup.
        /// </summary>
        /// <param name="Duration">Durata dei popup.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetPopupDuration(int Duration, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(Duration);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMESSAGEDURATION, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera lo stato della funzionalità di blocco del tasto del mouse.
        /// </summary>
        /// <returns>true se attiva, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsMouseClickLockEnabled()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETMOUSECLICKLOCK, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Abilita la funzionalità di blocco del tasto del mouse.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableMouseClickLock(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(1);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSECLICKLOCK, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Disabilita la funzionalità di blocco del tasto del mouse.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableMouseClickLock(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = IntPtr.Zero;
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSECLICKLOCK, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il tempo prima che il tasto primario del mouse venga bloccato.
        /// </summary>
        /// <returns>Il tempo di blocco, in secondi.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static int GetMouseClickLockTime()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETMOUSECLICKLOCKTIME, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                int Value = Marshal.ReadInt32(ValuePointer) / 1000;
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Imposta il tempo prima che il tasto primario del mouse venga bloccato.
        /// </summary>
        /// <param name="Time">Tempo prima del blocco, in secondi.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetMouseClickLockTime(int Time, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            Marshal.WriteInt32(ValuePointer, Time * 1000);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSECLICKLOCKTIME, 0, ValuePointer, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera le informazioni sulla funzionalità MouseKeys.
        /// </summary>
        /// <returns>Istanza di <see cref="MouseKeysInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static MouseKeysInfo GetMouseKeysInfo()
        {
            MOUSEKEYS Structure = new MOUSEKEYS()
            {
                Size = (uint)Marshal.SizeOf(typeof(MOUSEKEYS))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETMOUSEKEYS, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new MouseKeysInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta i parametri della funzionalità MouseKeys.
        /// </summary>
        /// <param name="Info">Istanza di <see cref="MouseKeysInfo"/> con i nuovi parametri.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetMouseKeysParameters(MouseKeysInfo Info, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            MOUSEKEYS Structure = Info.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSEKEYS, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera lo stato della funzionalità Mouse Sonar.
        /// </summary>
        /// <returns>true se la funzionalità è attiva, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsMouseSonarEnabled()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETMOUSESONAR, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Abilita la funzionalità Mouse Sonar.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableMouseSonar(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(1);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSESONAR, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }


        /// <summary>
        /// Disabilita la funzionalità Mouse Sonar.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableMouseSonar(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = IntPtr.Zero;
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSESONAR, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera lo stato della funzionalità Mouse Vanish.
        /// </summary>
        /// <returns>true se la funzionalità è attiva, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsMouseVanishEnabled()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETMOUSEVANISH, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Abilita la funzionalità Mouse Vanish.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableMouseVanish(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = new IntPtr(1);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSEVANISH, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Disabilita la funzionalità Mouse Vanish.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableMouseVanish(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            IntPtr Value = IntPtr.Zero;
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETMOUSEVANISH, 0, Value, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Determina se è in esecuzione un'applicazione di lettura schermo.
        /// </summary>
        /// <returns>true se un'applicazione di lettura schermo è in esecuzione, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsScreenReviewerAppRunning()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETSCREENREADER, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Imposta la flag che indica se un'utilità di lettura schermo è in esecuzione.
        /// </summary>
        /// <param name="IsRunning">Indica se un'utilità di lettura schermo è in esecuzione.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetScreenReaderFlag(bool IsRunning, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETSCREENREADER, Convert.ToUInt32(IsRunning), IntPtr.Zero, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Determina se la funzionalità ShowSounds è abilitata.
        /// </summary>
        /// <returns>true se la funzionalità è abilitata, false altrimenti.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static bool IsShowSoundsEnabled()
        {
            IntPtr ValuePointer = Marshal.AllocHGlobal(4);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETSHOWSOUNDS, 0, ValuePointer, 0))
            {
                Marshal.FreeHGlobal(ValuePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                bool Value = Convert.ToBoolean(Marshal.ReadInt32(ValuePointer));
                Marshal.FreeHGlobal(ValuePointer);
                return Value;
            }
        }

        /// <summary>
        /// Abilita la funzionalità ShowSounds.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void EnableShowSounds(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETSHOWSOUNDS, 1, IntPtr.Zero, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Disabilita la funzionalità ShowSounds.
        /// </summary>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void DisableShowSounds(bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETSHOWSOUNDS, 0, IntPtr.Zero, Option))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sulla funzionalità SoundSentry.
        /// </summary>
        /// <returns>Istanza di <see cref="SoundSentryInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static SoundSentryInfo GetSoundSentryInfo()
        {
            SOUNDSENTRY Structure = new SOUNDSENTRY()
            {
                Size = (uint)Marshal.SizeOf(typeof(SOUNDSENTRY))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETSOUNDSENTRY, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new SoundSentryInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta i nuovi parametri della funzionalità SoundSentry.
        /// </summary>
        /// <param name="Info">Istanza di <see cref="SoundSentryInfo"/> con i nuovi parametri.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetSoundSentryInfo(SoundSentryInfo Info, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            SOUNDSENTRY Structure = Info.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETSOUNDSENTRY, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sulla funzionalità Tasti permanenti.
        /// </summary>
        /// <returns>Istanza di <see cref="StickyKeysInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static StickyKeysInfo GetStickyKeysInfo()
        {
            STICKYKEYS Structure = new STICKYKEYS()
            {
                Size = (uint)Marshal.SizeOf(typeof(STICKYKEYS))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETSTICKYKEYS, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new StickyKeysInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta i parametri della funzionalità Tasti permanenti.
        /// </summary>
        /// <param name="Info">Istanza di <see cref="StickyKeysInfo"/> con i nuovi parametri.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetStickyKeysParameters(StickyKeysInfo Info, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            STICKYKEYS Structure = Info.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETSTICKYKEYS, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera informazioni sulla funzionalità ToggleKeys.
        /// </summary>
        /// <returns>Istanza di <see cref="ToggleKeysInfo"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static ToggleKeysInfo GetToggleKeysInfo()
        {
            TOGGLEKEYS Structure = new TOGGLEKEYS()
            {
                Size = (uint)Marshal.SizeOf(typeof(TOGGLEKEYS))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_GETTOGGLEKEYS, Structure.Size, StructurePointer, 0))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                Marshal.PtrToStructure(StructurePointer, Structure);
                Marshal.FreeHGlobal(StructurePointer);
                return new ToggleKeysInfo(Structure);
            }
        }

        /// <summary>
        /// Imposta i parametri della funzionalità ToggleKeys.
        /// </summary>
        /// <param name="Info">Istanza di <see cref="ToggleKeysInfo"/> con i nuovi parametri.</param>
        /// <param name="UpdateUserProfile">Indica se il profilo utente deve essere aggiornato.</param>
        /// <param name="NotifyUpdate">Indica se l'aggiornamento delle impostazioni deve essere notificato alle applicazioni.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetToggleKeysParameters(ToggleKeysInfo Info, bool UpdateUserProfile, bool NotifyUpdate)
        {
            UserProfileUpdateOptions Option = 0;
            if (UpdateUserProfile && !NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_UPDATEINIFILE;
            }
            else if (UpdateUserProfile && NotifyUpdate)
            {
                Option = UserProfileUpdateOptions.SPIF_SENDWININICHANGE;
            }
            TOGGLEKEYS Structure = Info.ToStruct();
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (!SystemParametersInfo(AccessibilityParameter.SPI_SETTOGGLEKEYS, Structure.Size, StructurePointer, Option))
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}