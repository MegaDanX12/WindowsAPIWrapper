using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsAndMessagesWrapper.Window.Windows;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Messages.MessageConstants;
using WindowsAPI.WindowsAndMessagesWrapper.Native.Messages;
using System.Runtime.InteropServices;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;

namespace WindowsAPI.WindowsAndMessagesWrapper.Messages
{
    /// <summary>
    /// Metodì per gestire i messaggi ricevuti e inviati.
    /// </summary>
    public static class Messages
    {
        /// <summary>
        /// Invia un messaggio alla coda di una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra a cui inviare il messaggio.</param>
        /// <param name="Message">Messaggio da inviare.</param>
        /// <param name="MessageData1">Dati del messaggio.</param>
        /// <param name="MessageData2">Dati del messaggio.</param>
        /// <param name="SendToAll">Indica se inviare il messaggio a tutte le finestre top-level.</param>
        /// <returns>Valore restituito dal messaggio.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        public static IntPtr SendMessage(IntPtr WindowHandle, uint Message, IntPtr MessageData1, IntPtr MessageData2, bool SendToAll = false)
        {
            if (SendToAll)
            {
                WindowHandle = HWND_BROADCAST;
            }
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No handle provided.");
            }
            if (WindowHandle != HWND_BROADCAST && !IsValidWindow(WindowHandle))
            {
                throw new ArgumentException("The handle does not reference a valid window.", nameof(WindowHandle));
            }
            if (Message < 0)
            {
                throw new ArgumentException("Invalid message ID.", nameof(Message));
            }
            IntPtr Result = MessageFunctions.SendMessage(WindowHandle, Message, MessageData1, MessageData2);
            int ErrorCode = Marshal.GetLastWin32Error();
            if (ErrorCode is ERROR_ACCESS_DENIED)
            {
                throw new UnauthorizedAccessException("Unable to send message, blocked by UIPI.");
            }
            return Result;
        }
    }
}