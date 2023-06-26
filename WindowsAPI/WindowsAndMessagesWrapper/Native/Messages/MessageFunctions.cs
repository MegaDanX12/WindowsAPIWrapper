using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Messages.MessageConstants;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Messages
{
    /// <summary>
    /// Funzioni relative ai messaggi.
    /// </summary>
    internal static class MessageFunctions
    {
        /// <summary>
        /// Invia un messaggio a una finestra o a una serie di finestre.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra che deve ricevere il messaggio.</param>
        /// <param name="Message">Messaggio da inviare.</param>
        /// <param name="wParam">Dati del messaggio.</param>
        /// <param name="lParam">Dati del messaggio.</param>
        /// <returns>Risultato dell'elaborazione del messaggio.</returns>
        /// <remarks>La funzione non restituisce fino a quando la finestra non ha elaborato il messaggio.<br/><br/>
        /// Se <paramref name="WindowHandle"/> è impostato a <see cref="HWND_BROADCAST"/>, il messaggio è inviato a tutte le finestre top-level del sistema, incluse quelle disabilitate o invisibili senza proprietario, finestre sovrapposte e popup.<br/>
        /// Il messaggio non viene inviato alle finestre figlie.<br/><br/>
        /// L'invio del messaggio è soggetto a UIPI, il thread o il processo può inviare messaggi solo a code appartenenti a thread con un livello di integrità minore o equivalente.<br/>
        /// Se l'invio di un messaggio viene bloccato da UIPI, viene impostato il codice di errore <see cref="ERROR_ACCESS_DENIED"/>.</remarks>
        [DllImport("User32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        internal static extern IntPtr SendMessage(IntPtr WindowHandle, uint Message, IntPtr wParam, IntPtr lParam);
    }
}