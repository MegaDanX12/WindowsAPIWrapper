using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.HighDPIWrapper.Native;
using WindowsAPI.WindowsAndMessagesWrapper.Native.Window;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.HighDPIWrapper.Enumerations;
using static WindowsAPI.HighDPIWrapper.Native.HighDPIEnumerations;
using static WindowsAPI.WindowsAndMessagesWrapper.Window.Enumerations;
using static WindowsAPI.WindowsAndMessagesWrapper.Window.Windows;
using static WindowsAPI.DiagnosticsWrapper.Native.HRESULTErrorConstants;
using WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration;
using WindowsAPI.WindowsGDIWrapper.FontAndText.DataClasses;
using static WindowsAPI.WindowsGDIWrapper.Native.FontAndText.FontAndTextStructures;
using WindowsAPI.UserInterfaceElementsWrapper.Icons.DataClasses;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Icons.IconStructures;
using WindowsAPI.WindowsAndMessagesWrapper.Configuration.DataClasses;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration.ConfigurationStructures;
using WindowsAPI.DiagnosticsWrapper.Native;
using WindowsAPI.HighDPIWrapper.DataClasses;

namespace WindowsAPI.HighDPIWrapper
{
    /// <summary>
    /// Metodi per gestire le funzionalità relative ai DPI.
    /// </summary>
    public static class HighDPI
    {
        /// <summary>
        /// Calcola la dimensione necessaria del rettangolo di una finestra per ospitare un'area client della dimensione indicata ai DPI indicati.
        /// </summary>
        /// <param name="DesiredClientArea">Dimensione desiderata dell'area client.</param>
        /// <param name="Styles">Stili della finestra.</param>
        /// <param name="HasMenu">Indica se la finestra ha un menù.</param>
        /// <param name="ExtendedStyles">Stili estesi della finestra.</param>
        /// <param name="DPI">DPI da usare per adattare il rettangolo.</param>
        /// <returns>Struttura <see cref="Rectangle"/> che indica la dimensione raccomandata della finestra.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static Rectangle AdjustWindowRectangleForDpi(Rectangle DesiredClientArea, WindowStyles Styles, bool HasMenu, ExtendedWindowStyles ExtendedStyles, int DPI)
        {
            if (DPI < 0)
            {
                throw new ArgumentException("Invalid DPI value.", nameof(DPI));
            }
            if (Styles.HasFlag(WindowStyles.IsOverlappedWindow))
            {
                throw new ArgumentException("Invalid styles.", nameof(Styles));
            }
            RECT ClientArea = new RECT()
            {
                Left = DesiredClientArea.X,
                Top = DesiredClientArea.Y,
                Right = DesiredClientArea.Right,
                Bottom = DesiredClientArea.Bottom
            };
            bool Result = HighDPIFunctions.AdjustWindowRectForDpi(ref ClientArea, (WindowEnumerations.WindowStyles)Styles, HasMenu, (WindowEnumerations.WindowExtendedStyles)ExtendedStyles, (uint)DPI);
            if (Result)
            {
                return Rectangle.FromLTRB(ClientArea.Left, ClientArea.Top, ClientArea.Right, ClientArea.Bottom);
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Abilita l'adattamento delle aree non client di una finestra top level.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <remarks>Questo metodo deve essere chiamato durante l'inizializzazione di una finestra.<br/>
        /// La finestra deve avere un valore <see cref="DPIAwarenessContext"/> di <see cref="DPIAwarenessContext.PerMonitorAware"/>.<br/>
        /// Non è necessario chiamare questo metodo per finestre con una valore <see cref="DPIAwarenessContext"/> di <see cref="DPIAwarenessContext.PerMonitorAwareV2"/>.</remarks>
        public static void EnableNonClientDpiScaling(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            IntPtr DPIAwarenessContext = HighDPIFunctions.GetWindowDpiAwarenessContext(WindowHandle);
            if (!HighDPIFunctions.AreDpiAwarenessContextsEqual(DPIAwarenessContext, HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2))
            {
                if (!HighDPIFunctions.AreDpiAwarenessContextsEqual(DPIAwarenessContext, HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE))
                {
                    throw new InvalidOperationException("Invalid DPI Awareness Context.");
                }
                else
                {
                    if (GetRootWindowHandle(WindowHandle) != WindowHandle)
                    {
                        throw new ArgumentException("The handle must reference a top level window.", nameof(WindowHandle));
                    }
                    else
                    {
                        if (!HighDPIFunctions.EnableNonClientDpiScaling(WindowHandle))
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Recupera il valore DPI di un display.
        /// </summary>
        /// <param name="MonitorHandle">Handle al monitor.</param>
        /// <param name="DPIType">Tipo di DPI da recuperare.</param>
        /// <returns>I DPI del monitor.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static int GetMonitorDPI(IntPtr MonitorHandle, MonitorDPIType DPIType)
        {
            if (MonitorHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(MonitorHandle), "No handle provided.");
            }
            if ((int)DPIType > 2)
            {
                throw new InvalidEnumArgumentException("Invalid DPI Type.");
            }
            uint Result = HighDPIFunctions.GetDpiForMonitor(MonitorHandle, (MONITOR_DPI_TYPE)DPIType, out uint X, out uint Y);
            if (Result != S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                return 0;
            }
            else
            {
                return (int)X;
            }
        }

        /// <summary>
        /// Recupera i DPI di sistema.
        /// </summary>
        /// <returns>I DPI di sistema.</returns>
        public static int GetSystemDPI()
        {
            return (int)HighDPIFunctions.GetDpiForSystem();
        }

        /// <summary>
        /// Recupera i DPI di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Il valore DPI della finestra.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetWindowDPI(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            uint DPI = HighDPIFunctions.GetDpiForWindow(WindowHandle);
            if (DPI is 0)
            {
                throw new ArgumentException("Invalid handle.", nameof(WindowHandle));
            }
            else
            {
                return (int)DPI;
            }
        }

        /// <summary>
        /// Recupera il modo in cui un processo tiene conto dei DPI.
        /// </summary>
        /// <param name="ProcessHandle">Handle al processo.</param>
        /// <returns>Un valore <see cref="DPIAwareness"/> che indica come il processo tiene conto dei DPI.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public static DPIAwareness GetProcessDpiAwareness(IntPtr ProcessHandle)
        {
            uint Result = HighDPIFunctions.GetProcessDpiAwareness(ProcessHandle, out PROCESS_DPI_AWARENESS Value);
            if (Result == S_OK)
            {
                return (DPIAwareness)Value;
            }
            else
            {
                if (Result is E_INVALIDARG)
                {
                    throw new ArgumentException("Invalid handle.", nameof(ProcessHandle));
                }
                else if (Result is E_ACCESSDENIED)
                {
                    throw new UnauthorizedAccessException("Insufficient privileges.");
                }
                else
                {
                    Marshal.ThrowExceptionForHR((int)Result, new IntPtr(-1));
                    return DPIAwareness.Invalid;
                }
            }
        }

        /// <summary>
        /// Recupera una metrica di sistema adattata in base al valore DPI fornito.
        /// </summary>
        /// <param name="Metric">Metrica da recuperare.</param>
        /// <param name="DPI">DPI da usare per adattare il risultato.</param>
        /// <returns>Valore della metrica desiderata.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static int GetSystemMetric(DPIMetric Metric, int DPI)
        {
            if (DPI < 0) 
            {
                throw new ArgumentException("Invalid DPI value.", nameof(DPI));
            }
            int[] EnumValue = (int[])Enum.GetValues(typeof(DPIMetric));
            if (!EnumValue.Contains((int)Metric))
            {
                throw new InvalidEnumArgumentException(nameof(Metric), (int)Metric, typeof(DPIMetric));
            }
            int Value = HighDPIFunctions.GetSystemMetricsForDpi((ConfigurationEnumerations.SystemMetric)Metric, (uint)DPI);
            if (Value is 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                return Value;
            }
        }

        /// <summary>
        /// Recupera il modo in cui il thread chiamante tiene conto dei DPI.
        /// </summary>
        /// <returns>Valore <see cref="DPIAwarenessContext"/> che descrive il modo che tiene conto dei DPI.</returns>
        public static DPIAwarenessContext GetThreadDpiAwarenessContext()
        {
            IntPtr Context = HighDPIFunctions.GetThreadDpiAwarenessContext();
            DPIAwarenessContext ContextValue = 0;
            if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE))
            {
                ContextValue = DPIAwarenessContext.Unaware;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
            {
                ContextValue = DPIAwarenessContext.SystemAware;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE))
            {
                ContextValue = DPIAwarenessContext.PerMonitorAware;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2))
            {
                ContextValue = DPIAwarenessContext.PerMonitorAwareV2;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED))
            {
                ContextValue = DPIAwarenessContext.UnawareGDIScaled;
            }
            return ContextValue;
        }

        /// <summary>
        /// Recupera il modo in cui una finestra tiene conto dei DPI.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Valore <see cref="DPIAwarenessContext"/> che descrive il modo che tiene conto dei DPI.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static DPIAwarenessContext GetWindowDpiAwarenessContext(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            IntPtr Context = HighDPIFunctions.GetWindowDpiAwarenessContext(WindowHandle);
            DPIAwarenessContext ContextValue = 0;
            if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE))
            {
                ContextValue = DPIAwarenessContext.Unaware;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE))
            {
                ContextValue = DPIAwarenessContext.SystemAware;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE))
            {
                ContextValue = DPIAwarenessContext.PerMonitorAware;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2))
            {
                ContextValue = DPIAwarenessContext.PerMonitorAwareV2;
            }
            else if (HighDPIFunctions.AreDpiAwarenessContextsEqual(Context, HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED))
            {
                ContextValue = DPIAwarenessContext.UnawareGDIScaled;
            }
            return ContextValue;
        }

        /// <summary>
        /// Determina se il contesto indicato è supportato dal sistema.
        /// </summary>
        /// <param name="Context">Valore da controllare.</param>
        /// <returns>true se il contesto è supportato, false altrimenti.</returns>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static bool IsDpiAwarenessContextSupported(DPIAwarenessContext Context)
        {
            if ((int)Context < -5 || (int)Context > -1)
            {
                throw new InvalidEnumArgumentException(nameof(Context), (int)Context, typeof(DPIAwarenessContext));
            }
            switch (Context)
            {
                case DPIAwarenessContext.Unaware:
                    return HighDPIFunctions.IsValidDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE);
                case DPIAwarenessContext.UnawareGDIScaled:
                    return HighDPIFunctions.IsValidDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED);
                case DPIAwarenessContext.SystemAware:
                    return HighDPIFunctions.IsValidDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE);
                case DPIAwarenessContext.PerMonitorAware:
                    return HighDPIFunctions.IsValidDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE);
                case DPIAwarenessContext.PerMonitorAwareV2:
                    return HighDPIFunctions.IsValidDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Converte un punto in una finestra dalle sue coordinate logiche nelle coordinate fisiche.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="LogicalCoordinates">Coordinate logiche da convertire.</param>
        /// <returns>Coordinate fisiche risultato della conversione.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static Point LogicalToPhysicalCoordinates(IntPtr WindowHandle, Point LogicalCoordinates)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            if (LogicalCoordinates.IsEmpty)
            {
                throw new ArgumentException("Invalid coordinates.", nameof(LogicalCoordinates));
            }
            POINT Coordinates = new POINT()
            {
                x = LogicalCoordinates.X,
                y = LogicalCoordinates.Y
            };
            if (HighDPIFunctions.LogicalToPhysicalPointForPerMonitorDPI(WindowHandle, ref Coordinates))
            {
                return new Point(Coordinates.x, Coordinates.y);
            }
            else
            {
                throw new Win32Exception("Conversion error.");
            }
        }

        /// <summary>
        /// Converte un punto in una finestra dalle sue coordinate fisiche nelle coordinate logiche.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="PhysicalCoordinates">Coordinate fisiche da convertire.</param>
        /// <returns>Coordinate logiche risultato della conversione.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static Point PhysicalToLogicalCoordinates(IntPtr WindowHandle, Point PhysicalCoordinates)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            if (PhysicalCoordinates.IsEmpty)
            {
                throw new ArgumentException("Invalid coordinates.", nameof(PhysicalCoordinates));
            }
            POINT Coordinates = new POINT()
            {
                x = PhysicalCoordinates.X,
                y = PhysicalCoordinates.Y
            };
            if (HighDPIFunctions.PhysicalToLogicalPointForPerMonitorDPI(WindowHandle, ref Coordinates))
            {
                return new Point(Coordinates.x, Coordinates.y);
            }
            else
            {
                throw new Win32Exception("Conversion error.");
            }
        }

        /// <summary>
        /// Imposta il modo in cui un processo tiene conto dei DPI.
        /// </summary>
        /// <param name="Awareness">Valore <see cref="ProcessDPIAwareness"/> che descrive la modalità di trattamento dei DPI.</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void SetProcessDpiAwareness(ProcessDPIAwareness Awareness)
        {
            if ((int)Awareness < 0 || (int)Awareness > 2)
            {
                throw new InvalidEnumArgumentException(nameof(Awareness), (int)Awareness, typeof(ProcessDPIAwareness));
            }
            uint Result = HighDPIFunctions.SetProcessDpiAwareness((PROCESS_DPI_AWARENESS)Awareness);
            if (Result != S_OK)
            {
                Marshal.ThrowExceptionForHR((int)Result);
            }
        }

        /// <summary>
        /// Imposta il modo in cui un thread gestisce cambiamenti nei DPI.
        /// </summary>
        /// <param name="Context">Contesto di gestione.</param>
        /// <param name="FormerContext">Contesto precedente.</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <remarks>In caso di errore il valore di <paramref name="FormerContext"/> non è valido.</remarks>
        public static void SetThreadDpiAwarenessContext(DPIAwarenessContext Context, out DPIAwarenessContext FormerContext)
        {
            if ((int)Context < -5 || (int)Context > -1)
            {
                throw new InvalidEnumArgumentException(nameof(Context), (int)Context, typeof(DPIAwarenessContext));
            }
            FormerContext = 0;
            IntPtr FormerContextHandle = IntPtr.Zero;
            switch (Context)
            {
                case DPIAwarenessContext.Unaware:
                    FormerContextHandle = HighDPIFunctions.SetThreadDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE);
                    break;
                case DPIAwarenessContext.UnawareGDIScaled:
                    FormerContextHandle = HighDPIFunctions.SetThreadDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED);
                    break;
                case DPIAwarenessContext.SystemAware:
                    FormerContextHandle = HighDPIFunctions.SetThreadDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE);
                    break;
                case DPIAwarenessContext.PerMonitorAware:
                    FormerContextHandle = HighDPIFunctions.SetThreadDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE);
                    break;
                case DPIAwarenessContext.PerMonitorAwareV2:
                    FormerContextHandle = HighDPIFunctions.SetThreadDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                    break;
            }
            if (FormerContextHandle != IntPtr.Zero)
            {
                if (FormerContextHandle == HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE)
                {
                    FormerContext = DPIAwarenessContext.Unaware;
                }
                else if (FormerContextHandle == HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED)
                {
                    FormerContext = DPIAwarenessContext.UnawareGDIScaled;
                }
                else if (FormerContextHandle == HighDPIConstants.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE)
                {
                    FormerContext = DPIAwarenessContext.SystemAware;
                }
                else if (FormerContextHandle == HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE)
                {
                    FormerContext = DPIAwarenessContext.PerMonitorAware;
                }
                else if (FormerContextHandle == HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2)
                {
                    FormerContext = DPIAwarenessContext.PerMonitorAwareV2;
                }
            }
            else
            {
                throw new Win32Exception("Invalid context value.");
            }
        }

        /// <summary>
        /// Recupera le informazioni sul font logico usato per le icone.
        /// </summary>
        /// <param name="DPI">DPI da usare per adattare le informazioni.</param>
        /// <returns>Istanza di <see cref="FontAttributes"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static FontAttributes GetIconTitleFont(int DPI)
        {
            LOGFONT Structure = new LOGFONT();
            int StructureSize = Marshal.SizeOf(Structure);
            IntPtr StructurePointer = Marshal.AllocHGlobal(StructureSize);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (HighDPIFunctions.SystemParametersInfoForDpi((uint)SystemParametersDPI.SPI_GETICONTITLEFONT, (uint)StructureSize, StructurePointer, 0, (uint)DPI))
            {
                Structure = (LOGFONT)Marshal.PtrToStructure(StructurePointer, typeof(LOGFONT));
                Marshal.FreeHGlobal(StructurePointer);
                return new FontAttributes(Structure);
            }
            else
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera le metriche relative alle icone.
        /// </summary>
        /// <param name="DPI">DPI da usare per adattare le informazioni.</param>
        /// <returns>Istanza di <see cref="IconMetrics"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static IconMetrics GetIconMetrics(int DPI)
        {
            ICONMETRICS Structure = new ICONMETRICS()
            {
                Size = (uint)Marshal.SizeOf(typeof(ICONMETRICS))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (HighDPIFunctions.SystemParametersInfoForDpi((uint)SystemParametersDPI.SPI_GETICONMETRICS, Structure.Size, StructurePointer, 0, (uint)DPI))
            {
                Structure = (ICONMETRICS)Marshal.PtrToStructure(StructurePointer,typeof(ICONMETRICS));
                Marshal.FreeHGlobal(StructurePointer);
                return new IconMetrics(Structure);
            }
            else
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera le metriche relative all'area non client di finestre non minimizzate.
        /// </summary>
        /// <param name="DPI">DPI da usare per adattare le informazioni.</param>
        /// <returns>Istanza di <see cref="NonClientMetrics"/> con le informazioni.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static NonClientMetrics GetNonClientMetrics(int DPI)
        {
            NONCLIENTMETRICS Structure = new NONCLIENTMETRICS()
            {
                Size = (uint)Marshal.SizeOf(typeof(NONCLIENTMETRICS))
            };
            IntPtr StructurePointer = Marshal.AllocHGlobal((int)Structure.Size);
            Marshal.StructureToPtr(Structure, StructurePointer, false);
            if (HighDPIFunctions.SystemParametersInfoForDpi((uint)SystemParametersDPI.SPI_GETNONCLIENTMETRICS, Structure.Size, StructurePointer, 0, (uint)DPI))
            {
                Structure = (NONCLIENTMETRICS)Marshal.PtrToStructure(StructurePointer, typeof(NONCLIENTMETRICS));
                Marshal.FreeHGlobal(StructurePointer);
                return new NonClientMetrics(Structure);
            }
            else
            {
                Marshal.FreeHGlobal(StructurePointer);
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Imposta il modo in cui un processo gestisce cambiamenti nei DPI.
        /// </summary>
        /// <param name="Context">Contesto di gestione.</param>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <remarks>In caso di errore il valore di <paramref name="FormerContext"/> non è valido.</remarks>
        public static void SetProcessDpiAwarenessContext(DPIAwarenessContext Context)
        {
            if ((int)Context < -5 || (int)Context > -1)
            {
                throw new InvalidEnumArgumentException(nameof(Context), (int)Context, typeof(DPIAwarenessContext));
            }
            bool Result = false;
            switch (Context)
            {
                case DPIAwarenessContext.Unaware:
                    Result = HighDPIFunctions.SetProcessDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE);
                    break;
                case DPIAwarenessContext.UnawareGDIScaled:
                    Result = HighDPIFunctions.SetProcessDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED);
                    break;
                case DPIAwarenessContext.SystemAware:
                    Result = HighDPIFunctions.SetProcessDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_SYSTEM_AWARE);
                    break;
                case DPIAwarenessContext.PerMonitorAware:
                    Result = HighDPIFunctions.SetProcessDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE);
                    break;
                case DPIAwarenessContext.PerMonitorAwareV2:
                    Result = HighDPIFunctions.SetProcessDpiAwarenessContext(HighDPIConstants.DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
                    break;
            }
            if (!Result)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode == Win32ErrorConstants.ERROR_ACCESS_DENIED)
                {
                    throw new UnauthorizedAccessException("DPI awareness mode already set.");
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Cambia il modo in cui le finestre di dialogo reagiscono a un cambiamento nei DPI.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo.</param>
        /// <param name="NewBehavior">Nuovo comportamento.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetDialogDpiChangeBehavior(IntPtr DialogHandle, DialogDPIChangeBehavior NewBehavior)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "No handle provided.");
            }
            if (!IsValidWindow(DialogHandle))
            {
                throw new ArgumentException("The handle does not refer to a valid dialog.");
            }
            DIALOG_DPI_CHANGE_BEHAVIORS Mask = DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DEFAULT | DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_ALL | DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_RESIZE | DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_CONTROL_RELAYOUT;
            DIALOG_DPI_CHANGE_BEHAVIORS Values = NewBehavior.ToEnumValue();
            if (!HighDPIFunctions.SetDialogDpiChangeBehavior(DialogHandle, Mask, Values))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode == Win32ErrorConstants.ERROR_ACCESS_DENIED)
                {
                    throw new UnauthorizedAccessException("The dialog belongs to another process.");
                }
                else if (ErrorCode == Win32ErrorConstants.ERROR_INVALID_PARAMETER)
                {
                    throw new ArgumentException("The handle is invalid.", nameof(DialogHandle));
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Recupera il modo in cui le finestre di dialogo reagiscono a un cambiamento nei DPI.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo.</param>
        /// <returns>Istanza di <see cref="DialogDPIChangeBehavior"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static DialogDPIChangeBehavior GetDialogDpiChangeBehavior(IntPtr DialogHandle)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "No handle provided.");
            }
            if (!IsValidWindow(DialogHandle))
            {
                throw new ArgumentException("The handle does not refer to a valid dialog.");
            }
            DIALOG_DPI_CHANGE_BEHAVIORS Behavior = HighDPIFunctions.GetDialogDpiChangeBehavior(DialogHandle);
            if (Behavior != 0)
            {
                return new DialogDPIChangeBehavior(Behavior);
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != Win32ErrorConstants.ERROR_SUCCESS)
                {
                    if (ErrorCode == Win32ErrorConstants.ERROR_INVALID_HANDLE)
                    {
                        throw new ArgumentException("Invalid dialog handle.", nameof(DialogHandle));
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
                else
                {
                    return new DialogDPIChangeBehavior(Behavior);
                }
            }
        }

        /// <summary>
        /// Cambia il modo in cui le finestre figlie di un dialogo reagiscono a un cambiamento nei DPI.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo.</param>
        /// <param name="NewBehavior">Nuovo comportamento.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetDialogControlDpiChangeBehavior(IntPtr DialogHandle, DialogControlDPIChangeBehavior NewBehavior)
        {
            if (DialogHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(DialogHandle), "No handle provided.");
            }
            if (!IsValidWindow(DialogHandle))
            {
                throw new ArgumentException("The handle does not refer to a valid dialog.");
            }
            DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Mask = DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DEFAULT | DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_FONT_UPDATE | DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_RELAYOUT;
            DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Values = NewBehavior.ToEnumValue();
            if (!HighDPIFunctions.SetDialogControlDpiChangeBehavior(DialogHandle, Mask, Values))
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode == Win32ErrorConstants.ERROR_ACCESS_DENIED)
                {
                    throw new UnauthorizedAccessException("The dialog belongs to another process.");
                }
                else if (ErrorCode == Win32ErrorConstants.ERROR_INVALID_PARAMETER)
                {
                    throw new ArgumentException("The handle is invalid.", nameof(DialogHandle));
                }
                else
                {
                    throw new Win32Exception(ErrorCode);
                }
            }
        }

        /// <summary>
        /// Recupera il modo in cui le finestre figlie di un dialogo reagiscono a un cambiamento nei DPI.
        /// </summary>
        /// <param name="WindowHandle">Handle al dialogo.</param>
        /// <returns>Istanza di <see cref="DialogDPIChangeBehavior"/> con le informazioni.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static DialogControlDPIChangeBehavior GetDialogControlDpiChangeBehavior(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not refer to a valid window.");
            }
            DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Behavior = HighDPIFunctions.GetDialogControlDpiChangeBehavior(WindowHandle);
            if (Behavior != 0)
            {
                return new DialogControlDPIChangeBehavior(Behavior);
            }
            else
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != Win32ErrorConstants.ERROR_SUCCESS)
                {
                    if (ErrorCode == Win32ErrorConstants.ERROR_INVALID_HANDLE)
                    {
                        throw new ArgumentException("Invalid window handle.", nameof(WindowHandle));
                    }
                    else
                    {
                        throw new Win32Exception(ErrorCode);
                    }
                }
                else
                {
                    return new DialogControlDPIChangeBehavior(Behavior);
                }
            }
        }

        /// <summary>
        /// Apre un handle ai dati del tema per una finestra e le classi specificate.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="ClassList">Elenco di classi.</param>
        /// <param name="DPI">DPI da usare per recuperare i dati.</param>
        /// <returns>Handle ai dati del tema, se viene restituito <see cref="IntPtr.Zero"/> allora non esistono dati per le classi specificate nel tema corrente.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static IntPtr GetThemeDataForWindowForDPI(IntPtr WindowHandle, string[] ClassList, int DPI)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not refer to a valid window.");
            }
            if (ClassList == null || ClassList.Length == 0)
            {
                throw new ArgumentNullException(nameof(ClassList), "The class list cannot be empty.");
            }
            if (DPI <= 0)
            {
                throw new ArgumentException("Invalid DPI value.", nameof(DPI));
            }
            StringBuilder ClassListBuilder = new StringBuilder();
            for (int i = 0; i < ClassList.Length; i++)
            {
                if (i != ClassList.Length - 1)
                {
                    ClassListBuilder.Append(ClassList[i]);
                }
                else
                {
                    ClassListBuilder.Append(ClassList[i]).Append(';');
                }
            }
            IntPtr ThemeHandle = HighDPIFunctions.OpenThemeDataForDpi(WindowHandle, ClassListBuilder.ToString(), (uint)DPI);
            if (ThemeHandle == IntPtr.Zero)
            {
                int ErrorCode = Marshal.GetLastWin32Error();
                if (ErrorCode != Win32ErrorConstants.ERROR_SUCCESS)
                {
                    throw new Win32Exception(ErrorCode);
                }
                else
                {
                    return ThemeHandle;
                }
            }
            else
            {
                return ThemeHandle;
            }
        }

        /// <summary>
        /// Recupera il valore DPI associato con un processo.
        /// </summary>
        /// <param name="ProcessHandle">Handle al processo.</param>
        /// <returns>Il valore DPI.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static int GetSystemDpiForProcess(IntPtr ProcessHandle)
        {
            if (ProcessHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ProcessHandle), "No handle provided.");
            }
            return (int)HighDPIFunctions.GetSystemDpiForProcess(ProcessHandle);
        }

        /// <summary>
        /// Recupera il valore DPI associato con il thread chiamante.
        /// </summary>
        /// <returns>Il valore DPI.</returns>
        /// <remarks>Se il <see cref="DPIAwarenessContext"/> del thread è <see cref="DPIAwarenessContext.PerMonitorAware"/> oppure <see cref="DPIAwarenessContext.PerMonitorAwareV2"/> il valore DPI non può essere recuperato.</remarks>
        /// <exception cref="InvalidOperationException"></exception>
        public static int GetCurrentThreadDPI()
        {
            IntPtr Context = HighDPIFunctions.GetThreadDpiAwarenessContext();
            uint DPI = HighDPIFunctions.GetDpiFromContext(Context);
            if (DPI == 0)
            {
                throw new InvalidOperationException("Unable to retrieve the DPI value for the thread.");
            }
            else
            {
                return (int)DPI;
            }
        }

        /// <summary>
        /// Imposta il comportamento di hosting DPI per il thread chiamante.
        /// </summary>
        /// <param name="NewBehavior">Nuovo comportamento.</param>
        /// <param name="FormerBehavior">Comportamento precedente.</param>
        /// <remarks>Se <paramref name="NewBehavior"/> non è valido, il valore restituito è <see cref="DpiHostingBehavior.Invalid"/>.</remarks>
        /// <exception cref="InvalidEnumArgumentException"></exception>
        public static void SetThreadDpiHostingBehavior(DpiHostingBehavior NewBehavior, out DpiHostingBehavior FormerBehavior)
        {
            if ((int)NewBehavior < -1 || (int) NewBehavior > 1)
            {
                throw new InvalidEnumArgumentException(nameof(NewBehavior), (int)NewBehavior, typeof(DpiHostingBehavior));
            }
            FormerBehavior = (DpiHostingBehavior)HighDPIFunctions.SetThreadDpiHostingBehavior((DPI_HOSTING_BEHAVIOR)NewBehavior);
        }

        /// <summary>
        /// Recupera il comportamento di hosting DPI per il thread chiamante.
        /// </summary>
        /// <returns>Un valore <see cref="DpiHostingBehavior"/> che descrive il comportamento.</returns>
        public static DpiHostingBehavior GetThreadDpiHostingBehavior()
        {
            return (DpiHostingBehavior)HighDPIFunctions.GetThreadDpiHostingBehavior();
        }

        /// <summary>
        /// Recupera il comportamento di hosting DPI per una finestra.
        /// </summary>
        /// <returns>Un valore <see cref="DpiHostingBehavior"/> che descrive il comportamento.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static DpiHostingBehavior GetWindowDpiHostingBehavior(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (!IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not refer to a valid window.");
            }
            return (DpiHostingBehavior)HighDPIFunctions.GetWindowDpiHostingBehavior(WindowHandle);
        }
    }
}