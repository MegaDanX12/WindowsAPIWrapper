using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses
{
    /// <summary>
    /// Costanti relative alle classi delle finestre.
    /// </summary>
    internal static class WindowClassesConstants
    {
        /// <summary>
        /// Valore restituito da un messaggio elaborato dalla procedura della finestra di dialogo.
        /// </summary>
        internal const int DWLP_MSGRESULT = 0;

        /// <summary>
        /// Puntatore, o un handle che lo rappresenta, alla procedura della finestra di dialogo.
        /// </summary>
        internal static readonly int DWLP_DLGPROC = DWLP_MSGRESULT + IntPtr.Size;

        /// <summary>
        /// Informazioni aggiuntive private dell'applicazione.
        /// </summary>
        internal static readonly int DWLP_USER = DWLP_DLGPROC + IntPtr.Size;
    }
}