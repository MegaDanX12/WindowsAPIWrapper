using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsGDIWrapper.Native.DeviceContexts
{
    /// <summary>
    /// Funzioni relativi ai contesti dispositivo.
    /// </summary>
    internal static class DeviceContextsFunctions
    {
        /// <summary>
        /// Elimina un oggetto GDI e libera tutte le risorse di sistema associate ad esso.
        /// </summary>
        /// <param name="GdiObjectHandle">Handle all'oggetto GDI.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("Gdi32.dll", EntryPoint = "DeleteObject", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr GdiObjectHandle);
    }
}