using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Menus
{
    /// <summary>
    /// Notifiche relative ai menù.
    /// </summary>
    internal static class MenuNotifications
    {
        /// <summary>
        /// Inviato quando l'utente seleziona un'oggetto di comando in un menù, quando un controllo invia una notifica alla sua finestra padre oppure quando un tasto accelleratore viene tradotto.
        /// </summary>
        /// <remarks>I valori di wParam e lParam possono eseere i seguenti:<br/><br/>
        /// Se la fonte del messaggio è un menù:<br/><br/>
        /// wParam: 0 (primi due byte), identificatore menù (ultimi due byte)<br/>
        /// lParam: 0<br/><br/>
        /// Se la fonte del messaggio è un accelleratore:<br/><br/>
        /// wParam: 1 (primi due byte), identificatore accelleratore (ultimi due byte)<br/>
        /// lParam: 0<br/><br/>
        /// Se la fonte del messaggio è un controllo:<br/><br/>
        /// wParam: codice di notifica (primi due byte), identificatore controllo (ultimi due byte)<br/>
        /// lParam: handle alla finestra del controllo<br/><br/>
        /// Se l'applicazione elabora il messaggio, dovrebbe restituire 0.</remarks>
        internal const int WM_COMMAND = 273;
    }
}