using System;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonStructures;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonEnumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonNotifications;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsNotifications;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsEnumerations;
using static WindowsAPI.LegacyUserInteractionWrapper.MouseInput.MouseInputNotifications;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons
{
    /// <summary>
    /// Messaggi dei pulsanti.
    /// </summary>
    internal static class ButtonMessages
    {
        /// <summary>
        /// Primo valore per i messaggi di controllo dei pulsanti.
        /// </summary>
        private const int BCM_FIRST = 5632;

        /// <summary>
        /// Recupera la dimensione del pulsante migliore per il suo testo e immagine, se una lista di immagini è presente.
        /// </summary>
        /// <remarks>wParam: non usato, deve essere 0.<br/>
        /// lParam: puntatore a una struttura <see cref="SIZE"/> che riceve la dimensione desiderata del pulsante, incluso il testo e la lista di immagini, se presente.<br/><br/>
        /// L'applicazione è responsabile per l'allocazione dello spazio per la struttura, i campi <see cref="SIZE.Y"/> e <see cref="SIZE.X"/> devono essere impostati a 0 per fare in modo che i valori vengano restituiti nella struttura.<br/>
        /// Per specificare una larghezza, impostare il campo <see cref="SIZE.X"/> alla larghezza desiderata, il sistema calcola l'altezza ideale e la restituisce nel campo <see cref="SIZE.Y"/>.<br/><br/>
        /// Se il messaggio ha successo, restituisce true, altrimenti false.</remarks>
        internal const int BCM_GETIDEALSIZE = BCM_FIRST + 1;

        /// <summary>
        /// Assegna una lista di immagini a un pulsante.
        /// </summary>
        /// <remarks>wParam: non usato, deve essere 0.<br/>
        /// lParam: puntatore a una struttura <see cref="BUTTON_IMAGELIST"/> che contiene le informazioni sulla lista di immagini.<br/><br/>
        /// Se il messaggio ha successo, restituisce true, altrimenti false.</remarks>
        internal const int BCM_SETIMAGELIST = BCM_FIRST + 2;

        /// <summary>
        /// Recupera le informazioni sulla lista di immagini assegnata a un pulsante.
        /// </summary>
        /// <remarks>wParam: non usato, deve essere 0.<br/>
        /// lParam: puntatore a una struttura <see cref="BUTTON_IMAGELIST"/> che contiene le informazioni sulla lista di immagini.<br/><br/>
        /// Se il messaggio ha successo, restituisce true, altrimenti false.</remarks>
        internal const int BCM_GETIMAGELIST = BCM_FIRST + 3;

        /// <summary>
        /// Imposta i margini per disegnare il testo in un pulsante.
        /// </summary>
        /// <remarks>wParam: non usato, deve essere 0.<br/>
        /// lParam: puntatore a struttura <see cref="RECT"/> che specifica i margini da usare per disegnare il testo.<br/><br/>
        /// Se il messaggio ha successo, restituisce true, false altrimenti.</remarks>
        internal const int BCM_SETTEXTMARGIN = BCM_FIRST + 4;

        /// <summary>
        /// Recupera i margini per disegnare il testo in un pulsante.
        /// </summary>
        /// <remarks>wParam: non usato, deve essere 0.<br/>
        /// lParam: puntatore a struttura <see cref="RECT"/> che specifica i margini da usare per disegnare il testo.<br/><br/>
        /// Se il messaggio ha successo, restituisce true, false altrimenti.</remarks>
        internal const int BCM_GETTEXTMARGIN = BCM_FIRST + 5;

        /// <summary>
        /// Imposta lo stato del dropdown per un pulsante con lo stile <see cref="Native.Toolbars.ToolbarsEnumerations.ToolbarStyles.TBSTYLE_DROPDOWN"/> applicato.
        /// </summary>
        /// <remarks>wParam: true se il dropdown è visibile, false altrimenti.<br/>
        /// lParam: deve essere 0.<br/><br/>
        /// Restituisce true se l'operazione ha successo, false altrimenti.</remarks>
        internal const int BCM_SETDROPDOWNSTATE = BCM_FIRST + 6;

        /// <summary>
        /// Imposta le informazioni per uno split button.
        /// </summary>
        /// <remarks>wParam: deve essere 0.<br/>
        /// lParam: puntatore a struttura <see cref="BUTTON_SPLITINFO"/> che contiene le informazioni su uno split button.<br/><br/>
        /// Restituisce true se l'operazione è riuscita, false altrimenti.</remarks>
        internal const int BCM_SETSPLITINFO = BCM_FIRST + 7;

        /// <summary>
        /// Recupera le informazioni per uno split button.
        /// </summary>
        /// <remarks>wParam: deve essere 0.<br/>
        /// lParam: puntatore a struttura <see cref="BUTTON_SPLITINFO"/> che contiene le informazioni su uno split button.<br/>
        /// L'applicazione è responsabile per l'allocazione dello spazio per la struttura, impostare il campo <see cref="BUTTON_SPLITINFO.ValidMembers"/> per indicare a quali informazioni si è interessati.<br/><br/>
        /// Restituisce true se l'operazione è riuscita, false altrimenti.</remarks>
        internal const int BCM_GETSPLITINFO = BCM_FIRST + 8;

        /// <summary>
        /// Imposta il testo della nota associata a un pulsante command link.
        /// </summary>
        /// <remarks>wParam: deve essere 0.<br/>
        /// lParam: puntatore a stringa a terminazione nulla che contiene la note.<br/><br/>
        /// Restituisce true se l'operazione è riuscita, false altrimenti.</remarks>
        internal const int BCM_SETNOTE = BCM_FIRST + 9;

        /// <summary>
        /// Recupera il testo della nota associata a un pulsante command link.
        /// </summary>
        /// <remarks>wParam: valore a 32 bit che specifica la dimensione del buffer puntato da lParam, in caratteri.<br/>
        /// lParam: puntatore al buffer che riceve il testo, deve essere grande a sufficienza per includere un carattere nullo finale.<br/><br/>
        /// Se il messaggio ha successo, restituisce true, false altrimenti.<br/><br/>
        /// In caso di errore, uno dei seguenti codici di errore può essere impostato:<br/><br/>
        /// <see cref="ERROR_NOT_SUPPORTED"/>: il pulsante non ha lo stile <see cref="ButtonStyles.BS_COMMANDLINK"/> oppure <see cref="ButtonStyles.BS_DEFCOMMANDLINK"/> applicato<br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: il buffer puntato da lParam è troppo piccolo, wParam contiene la dimensione necessaria, in caratteri</remarks>
        internal const int BCM_GETNOTE = BCM_FIRST + 10;

        /// <summary>
        /// Recupera la lunghezza del testo della nota che può essere visualizzata nella descrizione di un pulsante command link.
        /// </summary>
        /// <remarks>wParam e lParam: devono essere 0.<br/><br/>
        /// Restituisce la lunghezza della nota in caratteri, escluso il carattere nullo finale, 0 se la nota è vuota.</remarks>
        internal const int BCM_GETNOTELENGTH = BCM_FIRST + 11;

        /// <summary>
        /// Imposta lo stato di elevazione necessario per un pulsante così da visulizzare l'icona di elevazione (uno scudo).
        /// </summary>
        /// <remarks>wParam: deve essere 0.<br/>
        /// lParam: true per disegnare l'icona, false altrimenti.<br/><br/>
        /// Restituisce 1 se l'operazione ha successo, un codice di errore altrimenti.</remarks>
        internal const int BCM_SETSHIELD = BCM_FIRST + 12;

        /// <summary>
        /// Recupera lo stato di selezione di un radio button o di un checkbox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere 0.<br/><br/>
        /// Restituisce uno dei seguenti valori:<br/><br/>
        /// <see cref="ButtonState.BST_CHECKED"/><br/>
        /// <see cref="ButtonState.BST_INDETERMINATE"/><br/>
        /// <see cref="ButtonState.BST_UNCHECKED"/><br/><br/>
        /// Se il pulsante non ha uno dei seguenti stili applicati, restituisce 0:<br/><br/>
        /// <see cref="ButtonStyles.BS_AUTOCHECKBOX"/><br/>
        /// <see cref="ButtonStyles.BS_AUTORADIOBUTTON"/><br/>
        /// <see cref="ButtonStyles.BS_AUTO3STATE"/><br/>
        /// <see cref="ButtonStyles.BS_CHECKBOX"/><br/>
        /// <see cref="ButtonStyles.BS_RADIOBUTTON"/><br/>
        /// <see cref="ButtonStyles.BS_3STATE"/></remarks>
        internal const int BM_GETCHECK = 240;

        /// <summary>
        /// Imposta lo stato di selezione di un radio button o di un checkbox.
        /// </summary>
        /// <remarks>wParam: uno dei seguenti valori:<br/><br/>
        /// <see cref="ButtonState.BST_CHECKED"/><br/>
        /// <see cref="ButtonState.BST_INDETERMINATE"/><br/>
        /// <see cref="ButtonState.BST_UNCHECKED"/><br/><br/>
        /// lParam: non usato<br/><br/>
        /// Questo messaggio restituisce sempre 0.</remarks>
        internal const int BM_SETCHECK = 241;

        /// <summary>
        /// Recupera lo stato del pulsante o del checkbox.
        /// </summary>
        /// <remarks>wParam e lParam: non usato deve essere 0.<br/><br/>
        /// Restituisce una combinazione dei valori dell'enumerazione <see cref="ButtonState"/>.</remarks>
        internal const int BM_GETSTATE = 242;

        /// <summary>
        /// Imposta lo stato di evidenziazione di un pulsante.
        /// </summary>
        /// <remarks>wParam: true per impostare il pulsante come evidenziato, false rimuove l'evidenziazione.<br/>
        /// lParam: non usato.<br/><br/>
        /// Questo messaggio restituisce sempre 0.</remarks>
        internal const int BM_SETSTATE = 243;

        /// <summary>
        /// Imposta lo stile di un pulsante.
        /// </summary>
        /// <remarks>wParam: stile del pulsante, una combinazione dei valori dell'enumerazione <see cref="ButtonStyles"/>.<br/>
        /// lParam: gli ultimi due byte sono un valore booleano che specifica se il pulsante deve essere ridisegnato (true) o meno (false).<br/><br/>
        /// Questo messaggio restituisce sempre 0.</remarks>
        internal const int BM_SETSTYLE = 244;

        /// <summary>
        /// Simula il click del pulsante.
        /// </summary>
        /// <remarks>Questo messaggio causa la ricezione da parte del pulsante dei messaggi <see cref="WM_LBUTTONDOWN"/> e <see cref="WM_LBUTTONUP"/>, la finestra padre del pulsante riceve una notifica <see cref="BN_CLICKED"/>.<br/><br/>
        /// wParam e lParam: non usati, devono essere 0.<br/><br/>
        /// Questo messaggio non restituisce alcun valore.</remarks>
        internal const int BM_CLICK = 245;

        /// <summary>
        /// Recupera un handle all'immagine associate con un pulsante.
        /// </summary>
        /// <remarks>wParam: tipo di immagine associata con il pulsante, uno dei seguenti valori:<br/><br/>
        /// <see cref="ImageType.IMAGE_BITMAP"/><br/>
        /// <see cref="ImageType.IMAGE_ICON"/><br/><br/>
        /// lParam: non usato<br/><br/>
        /// Il valore restituito è un handle all'immagine, se esistente, altrimenti <see cref="IntPtr.Zero"/>.</remarks>
        internal const int BM_GETIMAGE = 246;

        /// <summary>
        /// Associa una nuova immagine con un pulsante.
        /// </summary>
        /// <remarks>wParam: tipo di immagine associata con il pulsante, uno dei seguenti valori:<br/><br/>
        /// <see cref="ImageType.IMAGE_BITMAP"/><br/>
        /// <see cref="ImageType.IMAGE_ICON"/><br/><br/>
        /// lParam: handle all'immagine da associare.<br/><br/>
        /// Il valore restituito è un handle all'immagine precedentemente associata con il pulsante, se esistente, altrimenti <see cref="IntPtr.Zero"/>.</remarks>
        internal const int BM_SETIMAGE = 247;

        /// <summary>
        /// Imposta un valore in un radio button che controlla la generazione di messaggi <see cref="BN_CLICKED"/> quando il pulsante riceve focus.
        /// </summary>
        /// <remarks>wParam: true per impostare il valore, false per mantenerlo.<br/>
        /// lParam: non usato, deve essere 0.<br/><br/>
        /// Nessun valore restituito.</remarks>
        internal const int BM_SETDONTCLICK = 248;

        /// <summary>
        /// Notifica la finestra padre di un pulsante relativamente a un'operazione di disegno personalizzata su di esso.
        /// </summary>
        /// <remarks>lParam: puntatore a struttura <see cref="NMCUSTOMDRAWINFO"/> che contiene le informazioni sull'operazione di disegno.<br/>
        /// Il campo <see cref="NMCUSTOMDRAWINFO.ItemNumber"/> contiene l'indice dell'oggetto in corso di disegno, il campo <see cref="NMCUSTOMDRAWINFO.AppData"/> contiene il valore lParam dell'oggetto.<br/><br/>
        /// Il valore restituito dipende dal passaggio di disegno in corso, specificato dal campo <see cref=" NMCUSTOMDRAWINFO.DrawingStage"/>.<br/>
        /// L'applicazione deve restituire uno dei seguenti valori:<br/><br/>
        /// <see cref="CustomDrawReturnValue.CDRF_NOTIFYPOSTERASE"/><br/>
        /// <see cref="CustomDrawReturnValue.CDRF_NOTIFYPOSTPAINT"/><br/>
        /// <see cref="CustomDrawReturnValue.CDRF_SKIPDEFAULT"/></remarks>
        internal const uint NM_CUSTOMDRAW = unchecked(NM_FIRST - 12);
    }
}