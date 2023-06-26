using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses
{
    /// <summary>
    /// Enumerazioni relative alle classi delle finestre.
    /// </summary>
    internal static class WindowClassesEnumerations
    {
        /// <summary>
        /// Informazioni su una finestra.
        /// </summary>
        internal enum WindowInfo
        {
            /// <summary>
            /// Stili estesi della finestra.
            /// </summary>
            GWL_EXSTYLE = -20,
            /// <summary>
            /// Handle all'istanza dell'applicazione.
            /// </summary>
            GWLP_HINSTANCE = -6,
            /// <summary>
            /// Handle alla finestra padre, se esiste.
            /// </summary>
            GWLP_HWNDPARENT = -8,
            /// <summary>
            /// Identificatore della finestra.
            /// </summary>
            GWLP_ID = -12,
            /// <summary>
            /// Stili della finestra.
            /// </summary>
            GWL_STYLE = -16,
            /// <summary>
            /// Dati utente associati alla finestra.
            /// </summary>
            GWLP_USERDATA = -21,
            /// <summary>
            /// Puntatore, o handle che lo rappresenta, alla procedura della finestra.
            /// </summary>
            GWLP_WNDPROC = -4
        }
    }
}