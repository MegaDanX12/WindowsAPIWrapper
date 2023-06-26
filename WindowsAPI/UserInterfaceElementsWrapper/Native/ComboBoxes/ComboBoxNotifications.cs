using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes
{
    /// <summary>
    /// Notifiche relative ai ComboBox.
    /// </summary>
    internal static class ComboBoxNotifications
    {
        /// <summary>
        /// Inviato alla finestra padre di un pulsante, combobox, list box oppure menu disegnato dal proprietario quando un aspetto visuale del controllo è cambiato.
        /// </summary>
        /// <remarks>wParam: identificatore del controlle che ha inviato il messaggio, se il mittente è un menù questo parametro è impostato a 0.<br/>
        /// lParam: puntatore a una stuttura <see cref="DRAWITEMSTRUCT"/> che contiene informazioni sull'oggetto che sta per essere disegnato e il tipo di operazione di disegno richiesta.<br/><br/>
        /// Se l'applicazione elabora questo messaggio, essa dovrebbe restituire true.</remarks>
        internal const int WM_DRAWITEM = 43;

        /// <summary>
        /// Inviato quando il ComboBox non riesce ad allocare abbastanza memoria per completare una richiesta.
        /// </summary>
        /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_ERRSPACE = -1;

        /// <summary>
        /// Inviato quando l'utente cambia la selezione corrente nel list box di un ComboBox.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_SELCHANGE = 1;

        /// <summary>
        /// Inviato quando l'utente fa doppio click su una stringa nel list box di un ComboBox.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_DBLCLK = 2;

        /// <summary>
        /// Inviato quando il ComboBox riceve il focus della tastiera.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_SETFOCUS = 3;

        /// <summary>
        /// Inviato quando il ComboBox ha perso il focus della tastiera.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_KILLFOCUS = 4;

        /// <summary>
        /// Inviato dopo che l'utente ha modificato il testo nel controllo di modifica di un ComboBox e dopo che il sistema ha aggiornato lo schermo.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_EDITCHANGE = 5;

        /// <summary>
        /// Inviato quando il controllo di modifica di un ComboBox sta per visualizzare il testo alterato, il testo è stato già formattato.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_EDITUPDATE = 6;

        /// <summary>
        /// Inviato quando il list box di un ComboBox sta per diventare visibile.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_DROPDOWN = 7;

        /// <summary>
        /// Inviato quando il list box di un ComboBox è stato chiuso.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_CLOSEUP = 8;

        /// <summary>
        /// Inviato quando l'utente seleziona un oggetto della lista o seleziona un oggetto e chiude la lista, indica che la selezione deve essere elaborata.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_SELENDOK = 9;

        /// <summary>
        /// Inviato per determinare la posizione relative di un nuovo oggetto in un ComboBox o Listbox disegnato dal proprietario.
        /// </summary>
        /// <remarks>wParam: ID del controllo che ha inviato il messaggio<br/>
        /// lParam: puntatore a struttura <see cref="COMPAREITEMSTRUCT"/> con i dati degli elementi da controllare<br/><br/>
        /// Ill valore restituito è uno dei seguenti:<br/><br/>
        /// -1: il primo oggetto precede il secondo nell'ordine<br/>
        /// 0: gli oggetti sono equivalenti<br/>
        /// 1: il primo oggetto segue il secondo nell'ordine<br/><br/>
        /// Il sistema invia questo messaggio più volte finché non ha determinato la posizione esatta del nuovo elemento.</remarks>
        internal const int WM_COMPAREITEM = 57;

        /// <summary>
        /// Inviato alla finestra proprietaria di un controllo da essa disegnato, al momento della creazione dello stesso.
        /// </summary>
        /// <remarks>wParam: il valore del campo <see cref="MEASUREITEMSTRUCT.ControlID"/>, questo identifica il controllo che ha inviato il messaggio<br/>
        /// lParam: puntatore a struttura <see cref="MEASUREITEMSTRUCT"/> che contiene le dimensioni del controllo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, deve restituire true.<br/><br/>
        /// Se wParam è impostato a 0, il messaggio è stato inviato da un menù.<br/><br/>
        /// Se wParam è impostato a 0 o a un valore diverso da 0 e <see cref=" MEASUREITEMSTRUCT.ControlType"/> non è impostato a <see cref="CommonControls.CommonControlsEnumerations.OwnerDrawnControlType.ODT_MENU"/>, il messaggio è stato inviato da un Combobox o da un Listbox.<br/><br/>
        /// Se wParam è impostato a un valore diverso da 0 e <see cref="MEASUREITEMSTRUCT.ItemID"/> è impostato a 1, il messaggio è stato inviato dal controllo di modifica di un Combobox.</remarks>
        internal const int WM_MEASUREITEM = 44;

        /// <summary>
        /// Inviato quando l'utente seleziona un oggetto ma poi seleziona un altro controllo o chiude la finestra di dialogo, indica che la selezione deve essere ignorata.
        /// </summary>
        /// /// <remarks>wParam: i primi due byte contengono l'ID del ComboBox, i prossimi due byte contengono il codice di notifica<br/>
        /// lParam: handle al ComboBox.</remarks>
        internal const int CBN_SELENDCANCEL = 10;
    }
}