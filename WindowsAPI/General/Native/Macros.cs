using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.General.Native
{
    /// <summary>
    /// Macro Win32.
    /// </summary>
    internal static class Macros
    {
        /// <summary>
        /// Recupera gli ultimi 16 bit dal un valore a 32 bit fornito.
        /// </summary>
        /// <param name="Value">Valore a 32 bit.</param>
        /// <returns>Il valore degli ultimi 16 bit.</returns>
        internal static ushort LOWORD(uint Value)
        {
            if (IntPtr.Size == 4)
            {
                return (ushort)(Value & 0xffff);
            }
            else
            {
                return (ushort)(((long)Value) & 0xffff);
            }
        }

        /// <summary>
        /// Recupera i primi 16 bit dal un valore a 32 bit fornito.
        /// </summary>
        /// <param name="Value">Valore a 32 bit.</param>
        /// <returns>Il valore dei primi 16 bit.</returns>
        internal static ushort HIWORD(uint Value)
        {
            if (IntPtr.Size == 4)
            {
                return (ushort)((Value >> 16) & 0xffff);
            }
            else
            {
                return (ushort)((((long)Value) >> 16) & 0xffff);
            }
        }

        /// <summary>
        /// Recupera la coordinata x da un valore LPARAM.
        /// </summary>
        /// <param name="lParam">Valore da cui recuperare la coordinata.</param>
        /// <returns>La coordinata x.</returns>
        internal static int GET_X_LPARAM(IntPtr lParam)
        {
            return (short)LOWORD((uint)lParam.ToInt32());
        }

        /// <summary>
        /// Recupera la coordinata y da un valore LPARAM.
        /// </summary>
        /// <param name="lParam">Valore da cui recuperare la coordinata.</param>
        /// <returns>La coordinata y.</returns>
        internal static int GET_Y_LPARAM(IntPtr lParam)
        {
            return (short)HIWORD((uint)lParam.ToInt32());
        }

        /// <summary>
        /// Crea un intero a 32 bit concatendo i valori specificati.
        /// </summary>
        /// <param name="Value1">Primo valore.</param>
        /// <param name="Value2">Secondo valore.</param>
        /// <returns>Risultato della concatenazione.</returns>
        internal static int MAKELONG(ushort Value1, ushort Value2)
        {
            if (IntPtr.Size is 4)
            {
                return (int)(((ushort)(((uint)Value1) & 0xffff)) | (((uint)(ushort)(((uint)Value2) & 0xffff)) << 16));
            }
            else
            {
                return (int)(((ushort)(((ulong)Value1) & 0xffff)) | (((uint)(ushort)(((ulong)Value2) & 0xffff)) << 16));
            }
        }

        /// <summary>
        /// Crea un valore utilizzabile come dati in un messaggio.
        /// </summary>
        /// <param name="LowWord">I primi 16 bit del nuovo valore.</param>
        /// <param name="HighWord">Gli ultimi 16 bit del nuovo valore.</param>
        /// <returns>Il valore risultato della concatenazione.</returns>
        internal static IntPtr MAKEMESSAGEDATA(ushort LowWord, ushort HighWord)
        {
            return new IntPtr((uint)MAKELONG(LowWord, HighWord));
        }

        /// <summary>
        /// Recupera il valore dell'hit test dal un valore wParam.
        /// </summary>
        /// <param name="wParam">Valore wParam.</param>
        /// <returns>Valore dell'hit test.</returns>
        internal static short GET_NCHITTEST_WPARAM(IntPtr wParam)
        {
            return (short)LOWORD((uint)wParam.ToInt32());
        }

        /// <summary>
        /// Recupera lo stato di certi pulsanti dal valore wParam.
        /// </summary>
        /// <param name="wParam">Valore wParam origine dei dati.</param>
        /// <returns>Stato dei pulsanti.</returns>
        internal static ushort GET_XBUTTON_WPARAM(IntPtr wParam)
        {
            return HIWORD((uint)wParam.ToInt32());
        }
    }
}