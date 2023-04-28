using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Window.WindowEnumerations;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Window
{
    /// <summary>
    /// Funzioni relative alle finestre.
    /// </summary>
    internal static class WindowFunctions
    {
        /// <summary>
        /// Recupera l'identificatore del thread che ha creato la finestra specificata e, eventualmente, l'identificatore del processo.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="ProcessID">Puntatore a variabile che riceve l'identificatore del processo.</param>
        /// <returns>Se l'operazione è riuscita, il valore restituito è l'identificatore del thread che ha creato la finestra.<br/>
        /// Se l'handle non è valido, il valore restituito è 0.</returns>
        [DllImport("User32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
        internal static extern uint GetWindowAssociatedThreadProcessId(IntPtr WindowHandle, out uint ProcessID);

        /// <summary>
        /// Determina se l'handle si riferisce a una finestra esistente.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>true se l'handle si riferisce a una finestra esistente, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "IsWindow", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr WindowHandle);

        /// <summary>
        /// Recupera l'handle alla finestra associata a un'altra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="AncestorType">Tipo di finestra associata da recuperare.</param>
        /// <returns>Handle alla finestra associata.</returns>
        /// <remarks>Se <paramref name="WindowHandle"/> si riferisce al desktop, questa funzione restituisce <see cref="IntPtr.Zero"/>.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetAncestor", SetLastError = true)]
        internal static extern IntPtr GetAncestor(IntPtr WindowHandle, WindowAncestor AncestorType);

        /// <summary>
        /// Recupera un handle al desktop.
        /// </summary>
        /// <returns>Handle al desktop.</returns>
        [DllImport("User32.dll", EntryPoint = "GetDesktopWindow", SetLastError = true)]
        internal static extern IntPtr GetDesktopWindow();
    }
}