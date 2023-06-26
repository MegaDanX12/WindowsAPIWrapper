using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxEnumerations;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes
{
    /// <summary>
    /// Messaggi dei Combobox.
    /// </summary>
    internal static class ComboBoxMessages
    {
        /// <summary>
        /// Primo valore per i messaggi ComboBox.
        /// </summary>
        private const int CBM_FIRST = 5888;

        /// <summary>
        /// Recupera la posizione iniziale e finale della selezione corrente nel controllo di modifica di un Combobox.
        /// </summary>
        /// <remarks>wParam: puntatore a valore a 32 bit che riceve la posizione iniziale, può essere nullo.<br/>
        /// lParam: puntatore a valore a 32 bit che riceve la posizione finale, può essere nullo.<br/><br/>
        /// Restituisce un valore a 32 bit i cui primi duè byte sono la posizione iniziale è i cui ultimi due byte sono la posizione finale.</remarks>
        internal const int CB_GETEDITSEL = 320;

        /// <summary>
        /// Limita la lunghezza del testo inseribile dall'utente nel controllo di modifica di un Combobox.
        /// </summary>
        /// <remarks>wParam: numero di caratteri che l'utente può inserire, escluso il carattere nullo finale.<br/>
        /// lParam: non usato<br/><br/>
        /// Restituisce sempre true.<br/><br/>
        /// Se wParam è 0, il limite viene impostato a 2.147.483.646.<br/><br/>
        /// Questo messaggio limita solamente la lunghezza del testo inserito dall'utente, la lunghezza massima predefinta è di 30.000 caratteri.</remarks>
        internal const int CB_LIMITTEXT = 321;

        /// <summary>
        /// Seleziona caratteri nel controllo di modifica di un Combobox.
        /// </summary>
        /// <remarks>wParam: non usato<br/>
        /// lParam: i primi due byte indicano la posizione iniziale, i prossimi due byte indicano la posizione finale.<br/><br/>
        /// Se l'operazione ha successo, il messaggio restituisce true, se il controllo non ha lo stile <see cref="ComboBoxStyles.CBS_DROPDOWNLIST"/>, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_SETEDITSEL = 322;

        /// <summary>
        /// Aggiunge una stringa al Combobox.
        /// </summary>
        /// <remarks>wParam: non usato<br/>
        /// lParam: puntatore alla stringa a terminazione nulla da aggiungere.<br/><br/>
        /// Viene restituito l'indice in base 0 della posizione della nuova stringa se l'operazione è riuscita, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> altrimenti.<br/>
        /// Se non c'è spazio sufficiente per la stringa, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERRSPACE"/>.<br/><br/>
        /// Se il controllo non ha lo stile <see cref="ComboBoxStyles.CBS_SORT"/> applicato, la stringa viene aggiunta alla fine della lista, in caso contrario viene aggiunta la stringa e la lista viene ordinata.</remarks>
        internal const int CB_ADDSTRING = 323;

        /// <summary>
        /// Elimina una stringa dal un ComboBox.
        /// </summary>
        /// <remarks>wParam: indice in base zero della stringa da eliminare.<br/>
        /// lParam: non usato<br/><br/>
        /// Viene restituito il numero di elementi rimanenti nella lista se l'operazione è riuscita, se l'indice supera la dimensione della lista viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_DELETESTRING = 324;

        /// <summary>
        /// Recupera il numero di oggetti nel list box di un ComboBox;
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Restituisce il numero di elementi nel list box, in caso di errore restituisce <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_GETCOUNT = 326;

        /// <summary>
        /// Recupera l'indice dell'oggetto selezionato, se esistente, nel list box di un Combobox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Restituisce l'indice in base zero dell'oggetto selezionato, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> in caso di errore.</remarks>
        internal const int CB_GETCURSEL = 327;

        /// <summary>
        /// Recupera una stringa dalla lista di un Combobox.
        /// </summary>
        /// <remarks>wParam: indice della stringa<br/>
        /// lParam: puntatore a un buffer che riceve la stringa.<br/><br/>
        /// Restituisce la lunghezza della stringa, escluso il carattere nullo finale se l'operazione ha successo, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> se l'indice non è valido.<br/><br/>
        /// Il buffer deve avere spazio sufficiente per contenere la stringa più il carattere nullo finale.<br/><br/>
        /// Se il ComboBox è stato creato come controllo disegnato dal proprietario ma senza specificare lo stile <see cref="ComboBoxStyles.CBS_HASSTRINGS"/>, il buffer riceve i dati associati all'oggetto.</remarks>
        internal const int CB_GETLBTEXT = 328;

        /// <summary>
        /// Recupera la lunghezza, in caratteri, di una stringa nella lista di un ComboBox.
        /// </summary>
        /// <remarks>wParam: indice della stringa<br/>
        /// lParam: non usato<br/><br/>
        /// Restituisce la lunghezza della stringa, in caratteri, escluso il carattere nullo finale, se l'indice non è valido, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_GETLBTEXTLEN = 329;

        /// <summary>
        /// Inserisce una stringa o i dati di un oggetto nella lista di un ComboBox.
        /// </summary>
        /// <remarks>wParam: indice a cui inserire la stringa, se -1 viene inserita alla fine della lista.<br/>
        /// lParam: puntatore alla stringa da inserire, se il ComboBox non ha lo stile <see cref="ComboBoxStyles.CBS_HASSTRINGS"/> applicato, viene memorizzato il valore del parametro piuttosto che la stringa a cui punta.<br/><br/>
        /// Restituisce l'indice a cui la stringa è stata inserita se l'operazione è riuscita, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> altrimenti, se non c'è spazio sufficiente per memorizzare la stringa, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERRSPACE"/>.</remarks>
        internal const int CB_INSERTSTRING = 330;

        /// <summary>
        /// Rimuove tutti gli elementi dalla lista e dal controllo di modifica di un ComboBox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.</remarks>
        internal const int CB_RESETCONTENT = 331;

        /// <summary>
        /// Cerca il list box di un ComboBox per un oggetto che inizia con i caratteri presenti in una stringa.
        /// </summary>
        /// <remarks>wParam: indice di inizio della ricerca, -1 per cercare in tutto il list box.<br/>
        /// lParam: puntatore alla stringa a terminazione nulla contenente i caratteri da cercare.<br/><br/>
        /// Restituisce l'indice basato su zero dell'oggetto trovato, <see cref="ComboBoxMessageReturnValue.CB_ERR"/>, se la ricerca non ha prodotto risultati.<br/><br/>
        /// La ricerca inizia dall'indice indicato da wParam e continua, anche ripartendo dall'inizio della lista, fino a quando non lo incontra di nuovo, la ricerca non fa distinzione tra maiuscole e minuscole.</remarks>
        internal const int CB_FINDSTRING = 332;

        /// <summary>
        /// Cerca un oggetto che inizia con in caratteri nella stringa specificata, se trovato, viene copiato nel controllo di modifica.
        /// </summary>
        /// <remarks>wParam: indice di inizio della ricerca, -1 per cercare in tutto il list box.<br/>
        /// lParam: puntatore alla stringa a terminazione nulla contenente i caratteri da cercare.<br/><br/>
        /// Restituisce l'indice basato su zero dell'oggetto trovato, <see cref="ComboBoxMessageReturnValue.CB_ERR"/>, se la ricerca non ha prodotto risultati.<br/><br/>
        /// La ricerca inizia dall'indice indicato da wParam e continua, anche ripartendo dall'inizio della lista, fino a quando non lo incontra di nuovo, la ricerca non fa distinzione tra maiuscole e minuscole.</remarks>
        internal const int CB_SELECTSTRING = 333;

        /// <summary>
        /// Seleziona una stringa nel list box di un ComboBox, se necessario, la lista scorre per renderla visibile, il testo nel controllo di modifica cambia per riflettere la nuova selezione.
        /// </summary>
        /// <remarks>wParam: indice in base zero della stringa da selezionare, -1 per annullare la selezione attuale.<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'operazione ha successo, l'indice dell'oggetto trovato, se wParam è maggiore del numero di elementi nella lista o -1, la selezione viene annullata e viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_SETCURSEL = 334;

        /// <summary>
        /// Visualizza o nasconde il list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam: true per visualizzare il dropdown, false per nasconderlo.<br/><br/>
        /// Questo messaggio restituisce sempre true.</remarks>
        internal const int CB_SHOWDROPDOWN = 335;

        /// <summary>
        /// Recupera i dati associato a un oggetto nel ComboBox.
        /// </summary>
        /// <remarks>wParam: indice basato su zero dell'oggetto.<br/>
        /// lParam: non usato.<br/><br/>
        /// Restituisce i dati associati all'oggetto se l'operazione è riuscita, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> altrimenti.</remarks>
        internal const int CB_GETITEMDATA = 336;

        /// <summary>
        /// Associata dei dati a un oggetto nel ComboBox.
        /// </summary>
        /// <remarks>wParam: indice basato su zero dell'oggetto.<br/>
        /// lParam: dati da associare.<br/><br/>
        /// <see cref="ComboBoxMessageReturnValue.CB_ERR"/> in caso di errore.</remarks>
        internal const int CB_SETITEMDATA = 337;

        /// <summary>
        /// Recupera le coordinate schermo di un ComboBox con il dropdown visibile.
        /// </summary>
        /// <remarks>wParam: non usato.<br/>
        /// lParam: puntatore a struttura <see cref="RECT"/> che riceve le coordinate.<br/><br/>
        /// Restituisce un valore diverso da 0 se l'operazione è riuscita, 0 altrimenti.</remarks>
        internal const int CB_GETDROPPEDCONTROLRECT = 338;

        /// <summary>
        /// Imposta l'altezza degli oggetti nella lista o del campo di selezione di un ComboBox.
        /// </summary>
        /// <remarks>wParam: 1 per impostare l'altezza del campo di selezione, 0 per impostare l'altezza degli elementi della lista oppure l'indice in base zero dell'oggetto di cui deve essere impostata l'altezza.<br/>
        /// lParam: altezza, in pixel, dell'oggetto specificato in wParam.<br/><br/>
        /// Se l'indice o l'altezza non sono validi, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_SETITEMHEIGHT = 339;

        /// <summary>
        /// Recupera l'altezza degli oggetti nella lista o del campo di selezione di un ComboBox.
        /// </summary>
        /// <remarks>wParam: -1 per impostare l'altezza del campo di selezione, 0 per impostare l'altezza degli elementi della lista oppure l'indice in base zero dell'oggetto di cui deve essere impostata l'altezza.<br/>
        /// lParam: non usato.<br/><br/>
        /// L'altezza, in pixel, del componente del ComboBox, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/> in caso di errore.<br/><br/>
        /// Se il ComboBox include lo stile <see cref="ComboBoxStyles.CBS_OWNERDRAWVARIABLE"/>, wParam deve essere l'indice dell'oggetto di cui recuperare l'altezza.</remarks>
        internal const int CB_GETITEMHEIGHT = 340;

        /// <summary>
        /// Attiva o disattiva UI estesa per un ComboBox.
        /// </summary>
        /// <remarks>wParam: true per usare l'UI estesa, false per usare l'UI predefinita.<br/>
        /// lParam: non usato.<br/><br/>
        /// Se l'operazione ha successo viene restituito <see cref="ComboBoxMessageReturnValue.CB_OKAY"/>, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> altrimenti.</remarks>
        internal const int CB_SETEXTENDEDUI = 341;

        /// <summary>
        /// Determina se un ComboBox usa l'UI predefinita o l'UI estesa.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Viene restituito true se il controllo usa l'UI estesa, false se usa l'UI predefinita.</remarks>
        internal const int CB_GETEXTENDEDUI = 342;

        /// <summary>
        /// Determina se il dropdown del ComboBox è visibile.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Viene restituito true se il dropdown è visibile, false altrimenti.</remarks>
        internal const int CB_GETDROPPEDSTATE = 343;

        /// <summary>
        /// Cerca il list box di un ComboBox per una stringa uguale a una fornita.
        /// </summary>
        /// <remarks>wParam: indice di inizio della ricerca, -1 per cercare in tutto il list box.<br/>
        /// lParam: puntatore alla stringa a terminazione nulla da cercare.<br/><br/>
        /// Restituisce l'indice basato su zero dell'oggetto trovato, <see cref="ComboBoxMessageReturnValue.CB_ERR"/>, se la ricerca non ha prodotto risultati.<br/><br/>
        /// La ricerca inizia dall'indice indicato da wParam e continua, anche ripartendo dall'inizio della lista, fino a quando non lo incontra di nuovo, la ricerca non fa distinzione tra maiuscole e minuscole.</remarks>
        internal const int CB_FINDSTRINGEXACT = 344;

        /// <summary>
        /// Imposta la località corrente di un ComboBox.
        /// </summary>
        /// <remarks>wParam: ID della località che il ComboBox deve usare per ordinare le stringhe.<br/>
        /// lParam: non usato.<br/><br/>
        /// Viene restituito l'ID della località precedente, se l'ID fornito non è valido, viene restituito <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_SETLOCALE = 345;

        /// <summary>
        /// Recupera la località attuale di un ComboBox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Viene restituito un valore composto nel seguente modo:<br/><br/>
        /// i primi due byte contengono l'ID della lingua<br/>
        /// i prossimi due byte contengono il codice del paese/regione.</remarks>
        internal const int CB_GETLOCALE = 346;

        /// <summary>
        /// Recupera l'indice basato su zero del primo elemento visibile del list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Viene restituito l'indice del primo elemento visibile se l'operazione ha successo, <see cref="ComboBoxMessageReturnValue.CB_ERR"/> altrimenti.</remarks>
        internal const int CB_GETTOPINDEX = 347;

        /// <summary>
        /// Rende un oggetto a uno specifico indice nel list box di un ComboBox visibile.
        /// </summary>
        /// <remarks>wParam: indice in base zero dell'elemento<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'operazione ha successo, viene restituito 0.</remarks>
        internal const int CB_SETTOPINDEX = 348;

        /// <summary>
        /// Recupera la larghezza di scorrimento del list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Restituisce la larghezza di scorrimento, in pixel.</remarks>
        internal const int CB_GETHORIZONTALEXTENT = 349;

        /// <summary>
        /// Imposta la larghezza di scorrimento del list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam: larghezza di scorrimento, in pixel.<br/>
        /// lParam: non usato</remarks>
        internal const int CB_SETHORIZONTALEXTENT = 350;

        /// <summary>
        /// Recupera la larghezza minima permessa del list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Se l'operazione è riuscita, viene restituita la largezza minima permessa, altrimenti <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_GETDROPPEDWIDTH = 351;

        /// <summary>
        /// Imposta la larghezza minima permessa del list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam: larghezza minima permessa, in pixel.<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'operazione è riuscita, viene restituità la nuova larghezza del list box, altrimenti <see cref="ComboBoxMessageReturnValue.CB_ERR"/>.</remarks>
        internal const int CB_SETDROPPEDWIDTH = 352;

        /// <summary>
        /// Alloca memoria per inserire un gran numero di oggetti nel list box.
        /// </summary>
        /// <remarks>wParam: numero di oggetti da aggiungere<br/>
        /// lParam: memoria da allocare, in byte.<br/><br/>
        /// Se l'operazione è riuscita, viene restituito il numero totale di oggetti per cui è stat preallocata memoria, <see cref="ComboBoxMessageReturnValue.CB_ERRSPACE"/> altrimenti.</remarks>
        internal const int CB_INITSTORAGE = 353;

        /// <summary>
        /// 
        /// </summary>
        internal const int CB_MULTIPLEADDSTRING = 355;

        /// <summary>
        /// Imposta il numero minimo di elementi visibile nel list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam: numero minimo di elementi<br/>
        /// lParam: non usato, deve essere <see cref="IntPtr.Zero"/><br/><br/>
        /// Se l'operazione è riuscita, viene restituito true, false altrimenti.</remarks>
        internal const int CB_SETMINVISIBLE = CBM_FIRST + 1;

        /// <summary>
        /// Recupera il numero minimo di elementi visibile nel list box di un ComboBox.
        /// </summary>
        /// <remarks>wParam e lParam: non usati, devono essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Viene restituito il numero minimo di elementi visibile.</remarks>
        internal const int CB_GETMINVISIBLE = CBM_FIRST + 2;

        /// <summary>
        /// Imposta il suggerimento visualizzato per il controllo di modifica di un ComboBox.
        /// </summary>
        /// <remarks>wParam: deve essere <see cref="IntPtr.Zero"/><br/>
        /// lParam: puntatore alla stringa Unicode a terminazione nulla<br/><br/>
        /// Viene restituito 1 se l'operazione è riuscita, un codice di errore altrimenti.</remarks>
        internal const int CB_SETCUEBANNER = CBM_FIRST + 3;

        /// <summary>
        /// Imposta il suggerimento visualizzato per il controllo di modifica di un ComboBox.
        /// </summary>
        /// <remarks>wParam: Puntatore a un buffer che riceve il suggerimento<br/>
        /// lParam: lunghezza, in caratteri, del buffer<br/><br/>
        /// Viene restituito 1 se l'operazione è riuscita, 0 se non esiste testo da recuperare, se il buffer non è stato allocato o è stato modificato prima di inviare il messaggio, il comportamento non è definito.<br/><br/>
        /// L'applicazione è responsabile per l'allocazione del buffer, esso deve essere in grado di contenere il suggerimento più il carattere nullo finale.</remarks>
        internal const int CB_GETCUEBANNER = CBM_FIRST + 4;
    }
}