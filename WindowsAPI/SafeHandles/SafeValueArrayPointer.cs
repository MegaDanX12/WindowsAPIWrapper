using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Puntatore a un array di tipi di base.
    /// </summary>
    internal sealed class SafeValueArrayPointer : SafeMemoryPointer
    {
        /// <summary>
        /// Numero di elementi nell'array.
        /// </summary>
        private int ArrayLength;

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeValueArrayPointer"/> assegnandola al puntatore indicato.
        /// </summary>
        /// <param name="Pointer">Puntatore da assegnare all'istanza.</param>
        private SafeValueArrayPointer(IntPtr Pointer) : base(Pointer)
        {
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeValueArrayPointer"/>, allocando la memoria necessaria per contenere il numero di elementi indicato della dimensione indicata.
        /// </summary>
        /// <param name="ElementsSize">Dimensione, in byte, di ogni elemento.</param>
        /// <param name="ArraySize">Numero di elementi nell'array.</param>
        internal SafeValueArrayPointer(int ElementsSize, int ArraySize) : base(ElementsSize * ArraySize)
        {
        }

        /// <summary>
        /// Scrive gli elementi di un'array in memoria.
        /// </summary>
        /// <param name="ArrayToCopy">Array i cui elementi vanno copiati in memoria.</param>
        internal void WriteToMemory(byte[] ArrayToCopy)
        {
            Marshal.Copy(ArrayToCopy, 0, handle, ArrayToCopy.Length);
        }

        /// <summary>
        /// Scrive gli elementi di un'array in memoria.
        /// </summary>
        /// <param name="ArrayToCopy">Array i cui elementi vanno copiati in memoria.</param>
        internal void WriteToMemory(int[] ArrayToCopy)
        {
            Marshal.Copy(ArrayToCopy, 0, handle, ArrayToCopy.Length);
        }

        /// <summary>
        /// Scrive gli elementi di un'array in memoria.
        /// </summary>
        /// <param name="ArrayToCopy">Array i cui elementi vanno copiati in memoria.</param>
        internal void WriteToMemory(short[] ArrayToCopy)
        {
            Marshal.Copy(ArrayToCopy, 0, handle, ArrayToCopy.Length);
        }

        /// <summary>
        /// Scrive gli elementi di un'array in memoria.
        /// </summary>
        /// <param name="ArrayToCopy">Array i cui elementi vanno copiati in memoria.</param>
        internal void WriteToMemory(long[] ArrayToCopy)
        {
            Marshal.Copy(ArrayToCopy, 0, handle, ArrayToCopy.Length);
        }

        /// <summary>
        /// Scrive gli elementi di un'array in memoria.
        /// </summary>
        /// <param name="ArrayToCopy">Array i cui elementi vanno copiati in memoria.</param>
        internal void WriteToMemory(float[] ArrayToCopy)
        {
            Marshal.Copy(ArrayToCopy, 0, handle, ArrayToCopy.Length);
        }

        /// <summary>
        /// Scrive gli elementi di un'array in memoria.
        /// </summary>
        /// <param name="ArrayToCopy">Array i cui elementi vanno copiati in memoria.</param>
        internal void WriteToMemory(double[] ArrayToCopy)
        {
            Marshal.Copy(ArrayToCopy, 0, handle, ArrayToCopy.Length);
        }

        /// <summary>
        /// Legge gli elementi di un array dalla memoria.
        /// </summary>
        /// <returns>Array di byte letto dalla memoria non gestita.</returns>
        internal byte[] ReadFromMemoryByte(int ArrayLength = -1)
        {
            if (this.ArrayLength is 0 && ArrayLength > 0)
            {
                this.ArrayLength = ArrayLength;
            }
            byte[] ReadArray = new byte[ArrayLength];
            Marshal.Copy(handle, ReadArray, 0, ArrayLength);
            return ReadArray;
        }

        /// <summary>
        /// Legge gli elementi di un array dalla memoria.
        /// </summary>
        /// <returns>Array di interi a 32 bit letto dalla memoria non gestita.</returns>
        internal int[] ReadFromMemoryInt32(int ArrayLength = -1)
        {
            if (this.ArrayLength is 0 && ArrayLength > 0)
            {
                this.ArrayLength = ArrayLength;
            }
            int[] ReadArray = new int[ArrayLength];
            Marshal.Copy(handle, ReadArray, 0, ArrayLength);
            return ReadArray;
        }

        /// <summary>
        /// Legge gli elementi di un array dalla memoria.
        /// </summary>
        /// <returns>Array di interi a 16 bit letto dalla memoria non gestita.</returns>
        internal short[] ReadFromMemoryInt16(int ArrayLength = -1)
        {
            if (this.ArrayLength is 0 && ArrayLength > 0)
            {
                this.ArrayLength = ArrayLength;
            }
            short[] ReadArray = new short[ArrayLength];
            Marshal.Copy(handle, ReadArray, 0, ArrayLength);
            return ReadArray;
        }

        /// <summary>
        /// Legge gli elementi di un array dalla memoria.
        /// </summary>
        /// <returns>Array di interi a 64 bit letto dalla memoria non gestita.</returns>
        internal long[] ReadFromMemoryInt64(int ArrayLength = -1)
        {
            if (this.ArrayLength is 0 && ArrayLength > 0)
            {
                this.ArrayLength = ArrayLength;
            }
            long[] ReadArray = new long[ArrayLength];
            Marshal.Copy(handle, ReadArray, 0, ArrayLength);
            return ReadArray;
        }

        /// <summary>
        /// Legge gli elementi di un array dalla memoria.
        /// </summary>
        /// <returns>Array di numeri a virgola mobile a precisione singola letto dalla memoria non gestita.</returns>
        internal float[] ReadFromMemoryFloat(int ArrayLength = -1)
        {
            if (this.ArrayLength is 0 && ArrayLength > 0)
            {
                this.ArrayLength = ArrayLength;
            }
            float[] ReadArray = new float[ArrayLength];
            Marshal.Copy(handle, ReadArray, 0, ArrayLength);
            return ReadArray;
        }

        /// <summary>
        /// Legge gli elementi di un array dalla memoria.
        /// </summary>
        /// <returns>Array di numeri a virgola mobile a precisione doppia letto dalla memoria non gestita.</returns>
        internal double[] ReadFromMemoryDouble(int ArrayLength = -1)
        {
            if (this.ArrayLength is 0 && ArrayLength > 0)
            {
                this.ArrayLength = ArrayLength;
            }
            double[] ReadArray = new double[ArrayLength];
            Marshal.Copy(handle, ReadArray, 0, ArrayLength);
            return ReadArray;
        }

        /// <summary>
        /// Permette l'assegnamento di un <see cref="IntPtr"/> a un'istanza di <see cref="SafeValueArrayPointer"/>.
        /// </summary>
        /// <param name="Pointer">Puntatore all'inizio dell'array.</param>
        public static implicit operator SafeValueArrayPointer(IntPtr Pointer)
        {
            return new SafeValueArrayPointer(Pointer);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeValueArrayPointer"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeValueArrayPointer"/> che punta all'inizio dell'array.</param>
        public static implicit operator IntPtr(SafeValueArrayPointer Pointer)
        {
            return Pointer != null ? Pointer.handle : IntPtr.Zero;
        }
    }
}