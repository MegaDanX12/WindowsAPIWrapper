using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.WindowsAndMessagesWrapper.Native.Window;

namespace WindowsAPI.WindowsAndMessagesWrapper.Window
{
    /// <summary>
    /// Metodi per la gestione delle finestre.
    /// </summary>
    public static class Windows
    {
        /// <summary>
        /// Recupera l'ID del thread che ha creato una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>L'ID del thread.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetWindowAssociatedThreadID(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (WindowFunctions.IsWindow(WindowHandle))
            {
                uint ThreadID = WindowFunctions.GetWindowAssociatedThreadProcessId(WindowHandle, out _);
                if (ThreadID != 0)
                {
                    return (int)ThreadID;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                throw new ArgumentException("The handle does not reference an existing window.", nameof(WindowHandle));
            }
        }

        /// <summary>
        /// Recupera l'ID del processo che ha creato una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>L'ID del processo.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static int GetWindowAssociatedProcessID(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (WindowFunctions.IsWindow(WindowHandle))
            {
                uint ThreadID = WindowFunctions.GetWindowAssociatedThreadProcessId(WindowHandle, out uint PID);
                if (ThreadID != 0)
                {
                    return (int)PID;
                }
                else
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                throw new ArgumentException("The handle does not reference an existing window.", nameof(WindowHandle));
            }
        }

        /// <summary>
        /// Determina se un handle si riferisce a una finestra esistente.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>true se l'handle si riferisce a una finestra esistente, false altrimenti.</returns>
        public static bool IsValidWindow(IntPtr WindowHandle)
        {
            return WindowFunctions.IsWindow(WindowHandle);
        }

        /// <summary>
        /// Recupera l'handle alla finestra del desktop.
        /// </summary>
        /// <returns>Handle alla finestra del desktop.</returns>
        public static IntPtr GetDesktopWindowHandle()
        {
            return WindowFunctions.GetDesktopWindow();
        }

        /// <summary>
        /// Recupera l'handle alla finestra padre.
        /// </summary>
        /// <param name="ChildWindowHandle">Handle alla finestra figlia.</param>
        /// <returns>Handle alla finestra padre.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IntPtr GetParentWindowHandle(IntPtr ChildWindowHandle)
        {
            if (ChildWindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ChildWindowHandle), "No handle provided.");
            }
            if (WindowFunctions.IsWindow(ChildWindowHandle))
            {
                throw new ArgumentException("The handle does not reference an existing window.", nameof(ChildWindowHandle));
            }
            if (GetDesktopWindowHandle() == ChildWindowHandle)
            {
                throw new ArgumentException("The desktop window handle is not a valid parameter for this method.", nameof(ChildWindowHandle));
            }
            return WindowFunctions.GetAncestor(ChildWindowHandle, WindowEnumerations.WindowAncestor.GA_PARENT);
        }

        /// <summary>
        /// Recupera l'handle alla finestra radice.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Handle alla finestra radice.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IntPtr GetRootWindowHandle(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (WindowFunctions.IsWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference an existing window.", nameof(WindowHandle));
            }
            if (GetDesktopWindowHandle() == WindowHandle)
            {
                throw new ArgumentException("The desktop window handle is not a valid parameter for this method.", nameof(WindowHandle));
            }
            return WindowFunctions.GetAncestor(WindowHandle, WindowEnumerations.WindowAncestor.GA_ROOT);
        }

        /// <summary>
        /// Recupera l'handle alla finestra radice di cui si è il proprietario.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Handle alla finestra radice.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IntPtr GetOwnedRootWindowHandle(IntPtr WindowHandle)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (WindowFunctions.IsWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference an existing window.", nameof(WindowHandle));
            }
            if (GetDesktopWindowHandle() == WindowHandle)
            {
                throw new ArgumentException("The desktop window handle is not a valid parameter for this method.", nameof(WindowHandle));
            }
            return WindowFunctions.GetAncestor(WindowHandle, WindowEnumerations.WindowAncestor.GA_ROOTOWNER);
        }
    }
}