using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Puntatore a una stringa nella memoria non gestita.
    /// </summary>
    internal sealed class SafeStringPointer : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Numero di caratteri nella stringa, incluso il carattere nullo se esiste.
        /// </summary>
        private readonly int CharCount;

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeStringPointer"/>, allocando abbastanza memoria per contenere una stringa della dimensione indicata.
        /// </summary>
        /// <param name="StringSize">Numero di caratteri nella stringa.</param>
        internal SafeStringPointer(int StringSize) : base(true)
        {
            CharCount = StringSize;
            SetHandle(Marshal.AllocHGlobal(StringSize * UnicodeEncoding.CharSize));
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeStringPointer"/>, copiando la stringa fornita in memoria.
        /// </summary>
        /// <param name="String">Stringa da copiare.</param>
        internal SafeStringPointer(string String) : base(true)
        {
            CharCount = String.Length;
            SetHandle(Marshal.StringToHGlobalUni(String));
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeStringPointer"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeStringPointer"/> che punta a memoria non gestita.</param>
        public static implicit operator IntPtr(SafeStringPointer Pointer)
        {
            return Pointer != null ? Pointer.handle : IntPtr.Zero;
        }

        /// <summary>
        /// Legge una stringa dalla memoria non gestita.
        /// </summary>
        /// <returns>Stringa letta dalla memoria non gestita.</returns>
        internal string ReadFromMemory(int CharCount = -1)
        {
            if (this.CharCount is 0)
            {
                if (CharCount > 0)
                {
                    return Marshal.PtrToStringUni(handle, CharCount);
                }
                else
                {
                    return Marshal.PtrToStringUni(handle);
                }
            }
            else
            {
                return Marshal.PtrToStringUni(handle, CharCount);
            }
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}