using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs
{
    /// <summary>
    /// Callback relativi ai dialoghi comuni.
    /// </summary>
    internal static class CommonDialogCallbacks
    {
        /// <summary>
        /// Riceve messaggi o notifihce intese per una finestra di dialogo.
        /// </summary>
        /// <param name="DialogHandle">Handle alla finestra di dialogo.</param>
        /// <param name="Message">Messaggio.</param>
        /// <param name="wParam">Dati messaggio.</param>
        /// <param name="lParam">Dati messaggio.</param>
        /// <returns>0 per permettere alla procedura della finestra di dialogo di elaborare il messaggio, diverso da zero per impedire questa operazione.</returns>
        internal delegate IntPtr DialogMessageHandler(IntPtr DialogHandle, uint Message, IntPtr wParam, IntPtr lParam);
    }
}