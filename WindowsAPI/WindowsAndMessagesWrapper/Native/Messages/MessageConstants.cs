using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Messages
{
    /// <summary>
    /// Costanti relative ai messaggi.
    /// </summary>
    internal static class MessageConstants
    {
        /// <summary>
        /// Valore speciale che indica di inviare un messaggio a tutte le finestre top-level nel sistema escluse le finestre figlie.
        /// </summary>
        internal static readonly IntPtr HWND_BROADCAST = new IntPtr(65535);
    }
}