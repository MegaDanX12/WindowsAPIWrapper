using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Puntatore a un valore intero con una dimensione di 1, 2, 4 o 8 byte nella memoria non gestita.
    /// </summary>
    internal sealed class SafeValuePointer : SafeMemoryPointer
    {
        /// <summary>
        /// Dimensione, in byte, del valore in memoria.
        /// </summary>
        private int ValueSize;

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeValuePointer"/>, associandola al puntatore specificato.
        /// </summary>
        /// <param name="Pointer">Puntatore a memoria non gestita.</param>
        internal SafeValuePointer(IntPtr Pointer) : base(Pointer)
        {
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeValuePointer"/>, allocando la quantità di memoria indicata.
        /// </summary>
        /// <param name="Size">Dimensione, in byte, della memoria da allocare.</param>
        internal SafeValuePointer(int Size) : base(Size)
        {
            ValueSize = Size;
        }

        /// <summary>
        /// Permette l'assegnamento di un <see cref="IntPtr"/> a un'istanza di <see cref="SafeValuePointer"/>.
        /// </summary>
        /// <param name="Pointer">Puntatore a memoria non gestita.</param>
        public static implicit operator SafeValuePointer(IntPtr Pointer)
        {
            return new SafeValuePointer(Pointer);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeValuePointer"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeValuePointer"/> che punta a memoria non gestita.</param>
        public static implicit operator IntPtr(SafeValuePointer Pointer)
        {
            return Pointer != null ? Pointer.handle : IntPtr.Zero;
        }

        /// <summary>
        /// Scrive un valore nella memoria non gestita.
        /// </summary>
        /// <param name="Value">Valore da scrivere.</param>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        internal void WriteToMemory(object Value, int Size = -1)
        {
            if (Value is null)
            {
                throw new ArgumentNullException(nameof(Value), "No value provided.");
            }
            if (ValueSize is 0)
            {
                if (Size is -1)
                {
                    Type ValueType = Value.GetType();
                    if (ValueType.IsEnum)
                    {
                        ValueType = ValueType.GetEnumUnderlyingType();
                    }
                    ValueSize = Marshal.SizeOf(ValueType);
                }
                else
                {
                    ValueSize = Size;
                }
            }
            switch (ValueSize)
            {
                case 1:
                    Marshal.WriteByte(handle, (byte)Value);
                    break;
                case 2:
                    Marshal.WriteInt16(handle, (short)Value);
                    break;
                case 4:
                    Marshal.WriteInt32(handle, (int)Value);
                    break;
                case 8:
                    Marshal.WriteInt64(handle, (long)Value);
                    break;
                default:
                    throw new NotSupportedException("Unsupported value type.");
            }
        }

        /// <summary>
        /// Legge un valore dalla memoria non gestita.
        /// </summary>
        /// <param name="ValueType">Tipo del valore da leggere dalla memoria non gestita.</param>
        /// <param name="Size">Dimensione, in byte, del valore da leggere.</param>
        /// <returns>Il valore letto.</returns>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        internal T ReadFromMemory<T>(Type ValueType = null, int Size = -1)
        {
            if (ValueSize is 0)
            {
                if (Size is -1)
                {
                    if (ValueType != null)
                    {
                        if (ValueType.IsEnum)
                        {
                            ValueType = ValueType.GetEnumUnderlyingType();
                        }
                        ValueSize = Marshal.SizeOf(ValueType);
                    }
                    else
                    {
                        throw new InvalidOperationException("The size of the value is unknown.");
                    }
                }
                else
                {
                    ValueSize = Size;
                }
            }
            switch (ValueSize)
            {
                case 1:
                    return (T)(object)Marshal.ReadByte(handle);
                case 2:
                    return (T)(object)Marshal.ReadInt16(handle);
                case 4:
                    return (T)(object)Marshal.ReadInt32(handle);
                case 8:
                    return (T)(object)Marshal.ReadInt64(handle);
                default:
                    throw new NotSupportedException("Unsupported value type.");
            }
        }
    }
}