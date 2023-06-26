using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.MemoryManagementWrapper.Native
{
    /// <summary>
    /// Funzioni relative alla gestione della memoria.
    /// </summary>
    internal static class MemoryManagementFunctions
    {
        /// <summary>
        /// Copia un blocco di memoria da un punto a un altro.
        /// </summary>
        /// <param name="Destination">Destinazione dove copiare la memoria.</param>
        /// <param name="Source">Origine della copia.</param>
        /// <param name="Size">Quantità, in byte, della memoria da copiare.</param>
        [DllImport("Kernel32.dll", EntryPoint = "CopyMemory", SetLastError = true)]
        internal static extern void CopyMemory(IntPtr Destination, IntPtr Source, IntPtr Size);
    }
}