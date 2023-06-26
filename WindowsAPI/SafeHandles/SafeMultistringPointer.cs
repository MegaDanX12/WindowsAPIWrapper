using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Puntatore all'inizio di una multistringa.
    /// </summary>
    internal sealed class SafeMultistringPointer : SafeMemoryPointer
    {
        internal SafeMultistringPointer(IntPtr Pointer) : base(Pointer)
        {
        }

        internal SafeMultistringPointer(int Size) : base(Size)
        {
        }

        /// <summary>
        /// Permette l'assegnamento di un <see cref="IntPtr"/> a un'istanza di <see cref="SafeMultistringPointer"/>.
        /// </summary>
        /// <param name="Pointer">Puntatore a memoria non gestita.</param>
        public static implicit operator SafeMultistringPointer(IntPtr Pointer)
        {
            return new SafeMultistringPointer(Pointer);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeMultistringPointer"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeMultistringPointer"/> che punta a memoria non gestita.</param>
        public static implicit operator IntPtr(SafeMultistringPointer Pointer)
        {
            return Pointer is null ? IntPtr.Zero : Pointer.handle;
        }

        /// <summary>
        /// Legge le stringhe presenti in una multistringa.
        /// </summary>
        /// <param name="StringCount">Numero di stringhe nella multistringa.</param>
        /// <returns>Array che contiene le stringhe lette dalla multistringa.</returns>
        internal string[] ReadStringsFromMemory(int StringCount = -1)
        {
            IntPtr StringPointer = handle;
            string[] Strings;
            if (StringCount is 0) 
            {
                return Array.Empty<string>();
            }
            else if (StringCount > 0)
            {
                Strings = new string[StringCount];
                for (int i = 0; i < StringCount; i++)
                {
                    Strings[i] = Marshal.PtrToStringUni(StringPointer);
                    StringPointer += (Strings[i].Length + 1) * UnicodeEncoding.CharSize;
                }
                return Strings;
            }
            else
            {
                List<string> StringsList = new List<string>();
                string ReadString;
                do
                {
                    ReadString = Marshal.PtrToStringUni(StringPointer);
                    StringPointer += (ReadString.Length + 1) * UnicodeEncoding.CharSize;
                    if (!string.IsNullOrWhiteSpace(ReadString))
                    {
                        StringsList.Add(ReadString);
                    }
                }
                while (!string.IsNullOrWhiteSpace(ReadString));
                return StringsList.ToArray();
            }
        }
    }
}