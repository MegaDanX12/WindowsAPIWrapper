using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.ExtendedLinguisticServicesWrapper.DataClasses;
using static WindowsAPI.ExtendedLinguisticServicesWrapper.Native.ELSStructures;

namespace WindowsAPI
{
    /// <summary>
    /// Metodi di utilità.
    /// </summary>
    internal static class UtilityMethods
    {
        /// <summary>
        /// Restituisce un array con la lunghezza indicata e con elementi letti da un puntatore.
        /// </summary>
        /// <typeparam name="T">Tipo di elementi nell'array.</typeparam>
        /// <param name="ArrayPointer">Puntatore al primo elemento dell'array.</param>
        /// <param name="ArrayLength">Numero di elementi.</param>
        /// <returns>Array della lunghezza indicata con gli elementi letti dal puntatore.</returns>
        /// <exception cref="NotSupportedException"></exception>
        internal static T[] ReadUnmanagedArray<T>(IntPtr ArrayPointer, int ArrayLength)
        {
            Type ElementType = typeof(T);
            Array ReturnArray = Array.CreateInstance(ElementType, ArrayLength);
            if (ElementType == typeof(string))
            {
                IntPtr StringPointer;
                for (int i = 0; i < ArrayLength; i++)
                {
                    StringPointer = Marshal.ReadIntPtr(ArrayPointer);
                    ((string[])ReturnArray)[i] = Marshal.PtrToStringUni(StringPointer);
                    ArrayPointer += IntPtr.Size;
                }
            }
            if (ElementType == typeof(uint))
            {
                for (int i = 0; i < ArrayLength; i++)
                {
                    ((uint[])ReturnArray)[i] = (uint)Marshal.ReadInt32(ArrayPointer);
                    ArrayPointer += 4;
                }
            }
            else if (ElementType == typeof(MAPPING_SERVICE_INFO))
            {
                MAPPING_SERVICE_INFO ServiceInfo;
                for (int i = 0; i < ArrayLength; i++)
                {
                    ServiceInfo = (MAPPING_SERVICE_INFO)Marshal.PtrToStructure(ArrayPointer, typeof(MAPPING_SERVICE_INFO));
                    ArrayPointer += ServiceInfo.Size.ToInt32();
                    ((MAPPING_SERVICE_INFO[])ReturnArray)[i] = ServiceInfo;
                }
            }
            else if (ElementType == typeof(MAPPING_DATA_RANGE))
            {
                MAPPING_DATA_RANGE DataRange;
                int StructureSize = Marshal.SizeOf(typeof(MAPPING_DATA_RANGE));
                for (int i = 0; i < ArrayLength; i++)
                {
                    DataRange = (MAPPING_DATA_RANGE)Marshal.PtrToStructure(ArrayPointer, typeof(MAPPING_DATA_RANGE));
                    ArrayPointer += StructureSize;
                    ((MAPPING_DATA_RANGE[])ReturnArray)[i] = DataRange;
                }
            }
            else
            {
                throw new NotSupportedException("Unsupported array type.");
            }
            return (T[])ReturnArray;
        }
    }
}