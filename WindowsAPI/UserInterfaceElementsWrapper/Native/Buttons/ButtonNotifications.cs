using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons
{
    /// <summary>
    /// Notifiche relative ai pulsanti.
    /// </summary>
    internal static class ButtonNotifications
    {
        /// <summary>
        /// Primo valore per le notifiche dei pulsanti.
        /// </summary>
        private const uint BCN_FIRST = unchecked((uint)0 - 1250);

        /// <summary>
        /// Ultimo valore per le notifiche dei pulsanti.
        /// </summary>
        private const uint BCN_LAST = unchecked((uint)0 - 1350);

        /// <summary>
        /// Notifica il proprietario del pulsante che il mouse sta entrando o sta lasciando l'area client del controllo.
        /// </summary>
        /// <remarks>lParam: puntatore a una struttura <see cref="NMBCHOTITEM"/>.<br/><br/>
        /// Questo messaggio non restituisce nessun valore.</remarks>
        internal const uint BCN_HOTITEMCHANGE = BCN_FIRST + 1;

        /// <summary>
        /// Inviato quando l'utente clicca sulla freccia del dropdown su un pulsante.
        /// </summary>
        /// <remarks>lParam: puntatore a struttura <see cref="NMBCDROPDOWN"/>, il campo <see cref="NMBCDROPDOWN.ButtonClientArea"/> descrive l'area del dropdown.<br/><br/>
        /// Nessun valore restituito.</remarks>
        internal const uint BCN_DROPDOWN = BCN_FIRST + 2;

        /// <summary>
        /// Inviato quando l'utente clicca su un pulsante.
        /// </summary>
        /// <remarks>wParam: gli ultimi due byte sono l'ID del controllo, i primi due byte specificano il codice di notifica.<br/>
        /// lParam: handle al pulsante.</remarks>
        internal const int BN_CLICKED = 0;

        /// <summary>
        /// Inviato quando l'utente fa doppio click su un pulsante.
        /// </summary>
        /// <remarks>Questo codice di notifica viene inviato automaticamente per pulsanti con i seguenti stili:<br/><br/>
        /// <see cref="ButtonStyles.BS_RADIOBUTTON"/><br/>
        /// <see cref="ButtonStyles.BS_OWNERDRAW"/><br/><br/>
        /// Gli altri tipi di pulsanti inviano questa notifica solo se hanno lo stile <see cref="ButtonStyles.BS_NOTIFY"/> applicato.<br/><br/>
        /// wParam: gli ultimi due byte contengono l'ID del pulsante, i primi due byte specifica il codice di notifica.<br/>
        /// lParam: handle al pulsante.</remarks>
        internal const int BN_DOUBLECLICKED = 5;

        /// <summary>
        /// Inviato quando l'utente fa doppio click su un pulsante.
        /// </summary>
        /// <remarks>Questo codice di notifica viene inviato automaticamente per pulsanti con i seguenti stili:<br/><br/>
        /// <see cref="ButtonStyles.BS_RADIOBUTTON"/><br/>
        /// <see cref="ButtonStyles.BS_OWNERDRAW"/><br/><br/>
        /// Gli altri tipi di pulsanti inviano questa notifica solo se hanno lo stile <see cref="ButtonStyles.BS_NOTIFY"/> applicato.<br/><br/>
        /// wParam: gli ultimi due byte contengono l'ID del pulsante, i primi due byte specifica il codice di notifica.<br/>
        /// lParam: handle al pulsante.</remarks>
        internal const int BN_DBLCLK = BN_DOUBLECLICKED;

        /// <summary>
        /// Inviato quando un pulsante riceve il focus della tastiera.
        /// </summary>
        /// <remarks>Il pulsante deve avere lo stile <see cref="ButtonStyles.BS_NOTIFY"/> applicato per inviare questa notifica.<br/><br/>
        /// wParam: gli ultimi due byte contengono l'ID del controllo, i primi due byte specificano il codice di notifica.<br/>
        /// lParam: handle al pulsante.</remarks>
        internal const int BN_SETFOCUS = 6;

        /// <summary>
        /// Inviato quando un pulsante perde il focus della tastiera.
        /// </summary>
        /// <remarks>Il pulsante deve avere lo stile <see cref="ButtonStyles.BS_NOTIFY"/> applicato per inviare questa notifica.<br/><br/>
        /// wParam: gli ultimi due byte contengono l'ID del controllo, i primi due byte specificano il codice di notifica.<br/>
        /// lParam: handle al pulsante.</remarks>
        internal const int BN_KILLFOCUS = 7;

        /// <summary>
        /// Inviato alla finestra padre di un pulsante prima che esso venga disegnato.
        /// </summary>
        /// <remarks>La finestra padre può cambiare il testo e il colore dello sfondo del pulsante.<br/>
        /// Solo pulsanti disegnati dal proprietario rispondono alla finestra padre che elabora questo messaggio.<br/><br/>
        /// wParam: handle al contesto dispositivo per il pulsante.<br/>
        /// lParam: handle al pulsante.<br/><br/>
        /// Se un'applicazione elabora questo messaggio, deve restituire un handle a un pennello, il sistema usare il pennello per disegnare lo sfondo del pulsante.<br/><br/>
        /// Se l'applicazione restituisce un pennello che ha creato, essa deve eliminarlo, se ha restituito un pennello di sistema, non è necessario eliminarlo.</remarks>
        internal const int WM_CTLCOLORBTN = 309;
    }
}