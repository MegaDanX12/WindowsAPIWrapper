using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Puntatore a struttura personalizzata nella memoria non gestita.
    /// </summary>
    internal sealed class SafeStructPointer : SafeMemoryPointer
    {
        private SafeStructPointer(IntPtr Pointer) : base(Pointer)
        {
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeStructPointer"/>, allocando la quantità di memoria indicata.
        /// </summary>
        /// <param name="Size">Dimensione, in byte, del blocco di memoria non gestita da allocare.</param>
        internal SafeStructPointer(int Size) : base(Size)
        {
        }

        /// <summary>
        /// Permette l'assegnamento di un <see cref="IntPtr"/> a un'istanza di <see cref="SafeStructPointer"/>.
        /// </summary>
        /// <param name="Pointer">Puntatore a memoria non gestita.</param>
        public static implicit operator SafeStructPointer(IntPtr Pointer)
        {
            return new SafeStructPointer(Pointer);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeStructPointer"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeStructPointer"/> che punta a memoria non gestita.</param>
        public static implicit operator IntPtr(SafeStructPointer Pointer)
        {
            return Pointer is null ? IntPtr.Zero : Pointer.handle;
        }

        /// <summary>
        /// Copia una struttura nella memoria non gestita..
        /// </summary>
        /// <param name="Structure">Struttura da copiare in memoria.</param>
        /// <typeparam name="T">Tipo della struttura.</typeparam>
        internal void WriteToMemory<T>(object Structure)
        {
            Marshal.StructureToPtr((T)Structure, handle, false);
        }

        /// <summary>
        /// Legge una struttura dalla memoria non gestita.
        /// </summary>
        /// <typeparam name="T">Tipo della strutttura.</typeparam>
        /// <returns>Struttura del tipo specificato, letta dalla memoria non gestita.</returns>
        internal T ReadFromMemory<T>() where T : new()
        {
            return (T)Marshal.PtrToStructure(handle, typeof(T));
        }
    }
}