using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Puntatore a memoria non gestita.
    /// </summary>
    internal abstract class SafeMemoryPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeMemoryPointer"/>, associandola al puntatore specificato.
        /// </summary>
        /// <param name="Pointer">Puntatore a memoria non gestita.</param>
        protected SafeMemoryPointer(IntPtr Pointer) : base(true)
        {
            SetHandle(Pointer);
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeMemoryPointer"/>, allocando la quantità di memoria indicata.
        /// </summary>
        /// <param name="Size">Dimensione, in byte, della memoria da allocare.</param>
        /// <exception cref="OutOfMemoryException"></exception>
        protected SafeMemoryPointer(int Size) : base(true)
        {
            SetHandle(Marshal.AllocHGlobal(Size));
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}