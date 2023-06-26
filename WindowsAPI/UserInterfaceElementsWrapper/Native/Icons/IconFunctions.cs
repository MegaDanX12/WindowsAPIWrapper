using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Icons
{
    /// <summary>
    /// Funzioni relative alle icone.
    /// </summary>
    internal static class IconFunctions
    {
        /// <summary>
        /// Elimina un'icona e libera la memoria che occupava.
        /// </summary>
        /// <param name="IconHandle">Handle all'icona.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>L'icona da eliminare non deve essere in uso.</remarks>
        [DllImport("User32.dll", EntryPoint = "DestroyIcon", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyIcon(IntPtr IconHandle);
    }
}