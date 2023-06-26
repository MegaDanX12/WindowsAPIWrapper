using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.DialogBoxes
{
    internal static class DialogBoxMessages
    {
        /// <summary>
        /// Inviato alla procedura della finestra di dialogo immediatamente prima che sia visualizzata.
        /// </summary>
        /// <remarks>wParam: handle al controllo che riceve il focus per impostazione predefinita<br/>
        /// lParam: altri dati di inizializzazione<br/><br/>
        /// Deve restituire true per indicare al sistema di impostare il focus sul controllo il cui handle è indicato da wParam, false per impedire questa operazione.</remarks>
        internal const int WM_INITDIALOG = 272;
    }
}