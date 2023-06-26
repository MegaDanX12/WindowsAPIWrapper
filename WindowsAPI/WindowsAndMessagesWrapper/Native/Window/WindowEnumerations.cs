using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Window
{
    /// <summary>
    /// Enumerazioni relative alle finestre.
    /// </summary>
    internal static class WindowEnumerations
    {
        /// <summary>
        /// Tipo di finestra associata.
        /// </summary>
        internal enum WindowAncestor : uint
        {
            /// <summary>
            /// Finestra padre.
            /// </summary>
            /// <remarks>Non include la proprietaria.</remarks>
            GA_PARENT = 1,
            /// <summary>
            /// Finestra root.
            /// </summary>
            GA_ROOT,
            /// <summary>
            /// Finestra root di cui si è il proprietario.
            /// </summary>
            GA_ROOTOWNER
        }

        /// <summary>
        /// Stili della finestra.
        /// </summary>
        [Flags]
        internal enum WindowStyles : uint
        {
            /// <summary>
            /// La finestra ha un bordo sottile.
            /// </summary>
            WS_BORDER = 8388608,
            /// <summary>
            /// La finestra ha una barra del titolo.
            /// </summary>
            /// <remarks>Lo stile <see cref="WS_BORDER"/> è incluso.</remarks>
            WS_CAPTION = 12582912,
            /// <summary>
            /// La finestra è una finestra figlia.
            /// </summary>
            /// <remarks>Questo stile non può essere usato insieme a <see cref="WS_POPUP"/>.</remarks>
            WS_CHILD = 1073741824,
            /// <summary>
            /// La finestra è una finestra figlia.
            /// </summary>
            WS_CHILDWINDOW = WS_CHILD,
            /// <summary>
            /// Esclude l'area occupata dalle finestre figlia al momento del disegno all'interno della finestra padre.
            /// </summary>
            /// <remarks>Questo stile è usato al momento della creazione della finestra padre.</remarks>
            WS_CLIPCHILDREN = 33554432,
            /// <summary>
            /// Nasconde le finestre figlie tra di loro.
            /// </summary>
            /// <remarks>Quando una finestra figlia riceve il messaggio <see cref="Window.WindowMessages.WM_PAINT"/>, questo stile taglia tutte le finestre figlie sovrapposta ad essa fuori dalla regione da aggiornare.</remarks>
            WS_CLIPSIBLINGS = 67108864,
            /// <summary>
            /// La finestra è inizialmente disabilitata.
            /// </summary>
            WS_DISABLED = 134217728,
            /// <summary>
            /// La finestra ha un bordo tipicamente usato con le finestre di dialogo.
            /// </summary>
            /// <remarks>Una finestra con questo stile non può avere una barra del titolo.</remarks>
            WS_DLGFRAME = 4194304,
            /// <summary>
            /// La finestra è il primo controllo di un gruppo di controlli.
            /// </summary>
            /// <remarks>Il gruppo consiste di questo controllo e di tutti quelli definiti dopo fino al prossimo con questo stile.</remarks>
            WS_GROUP = 131072,
            /// <summary>
            /// La finestra ha una barra di scorrimento orizzontale.
            /// </summary>
            WS_HSCROLL = 1048576,
            /// <summary>
            /// La finestra è inizialmente ridotta a icona.
            /// </summary>
            WS_ICONIC = 536870912,
            /// <summary>
            /// La finestra è inizialmente ingrandita.
            /// </summary>
            WS_MAXIMIZE = 16777216,
            /// <summary>
            /// La finestra ha un pulsante "Ingrandisci".
            /// </summary>
            /// <remarks>Questo stile non può essere combinato con <see cref="WindowExtendedStyles.WS_EX_CONTEXTHELP"/>, lo stile <see cref="WS_SYSMENU"/> deve essere specificato.</remarks>
            WS_MAXIMIZEBOX = 65536,
            /// <summary>
            /// La finestra è inizialmente ridotta a icona.
            /// </summary>
            WS_MINIMIZE = WS_ICONIC,
            /// <summary>
            /// La finestra ha un pulsante "Riduci a icona".
            /// </summary>
            /// <remarks>Questo stile non può essere combinato con <see cref="WindowExtendedStyles.WS_EX_CONTEXTHELP"/>, lo stile <see cref="WS_SYSMENU"/> deve essere specificato.</remarks>
            WS_MINIMIZEBOX = 131072,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            /// <remarks>Una finestra sovrapposta ha una barra del titolo e un bordo.</remarks>
            WS_OVERLAPPED = 0,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            WS_OVERLAPPEDWINDOW =
                WS_OVERLAPPED |
                WS_CAPTION |
                WS_SYSMENU |
                WS_THICKFRAME |
                WS_MINIMIZEBOX |
                WS_MAXIMIZEBOX,
            /// <summary>
            /// La finestra è una finestra popup.
            /// </summary>
            /// <remarks>Questo stile non può essere usato con <see cref="WS_CHILD"/>.</remarks>
            WS_POPUP = 2147483648,
            /// <summary>
            /// La finestra è una finestra popup.
            /// </summary>
            /// <remarks>Questo stile deve essere combinato con <see cref="WS_CAPTION"/> perché il menù sia visibile.</remarks>
            WS_POPUPWINDOW =
                WS_POPUP |
                WS_BORDER |
                WS_SYSMENU,
            /// <summary>
            /// La finestra ha un bordo di ridimensionamento.
            /// </summary>
            WS_SIZEBOX = 262144,
            /// <summary>
            /// La finestra ha un menù sulla barra del titolo.
            /// </summary>
            /// <remarks>Deve essere specificato anche lo stile <see cref="WS_CAPTION"/>.</remarks>
            WS_SYSMENU = 524288,
            /// <summary>
            /// La finestra è un controllo che riceve il focus della tastiera quando l'utente preme il tasto TAB.
            /// </summary>
            WS_TABSTOP = 65536,
            /// <summary>
            /// La finestra ha un bordo di ridimensionamento.
            /// </summary>
            WS_THICKFRAME = WS_SIZEBOX,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            /// <remarks>Una finestra sovrapposta ha una barra del titolo e un bordo.</remarks>
            WS_TILED = WS_OVERLAPPED,
            /// <summary>
            /// La finestra ha una finestra sovrapposta.
            /// </summary>
            WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW,
            /// <summary>
            /// La finestra è inizialmente visibile.
            /// </summary>
            WS_VISIBLE = 268435456,
            /// <summary>
            /// La finestra ha una barra di scorrimento verticale.
            /// </summary>
            WS_VSCROLL = 2097152
        }

        /// <summary>
        /// Stili estesi della finestra.
        /// </summary>
        [Flags]
        internal enum WindowExtendedStyles : uint
        {
            /// <summary>
            /// La finestra accetta file tramite drag-drop.
            /// </summary>
            WS_EX_ACCEPTFILES = 16,
            /// <summary>
            /// Forza una finestra top-level sulla barra delle applicazioni quando la finestra è visibile.
            /// </summary>
            WS_EX_APPWINDOW = 262144,
            /// <summary>
            /// La finestra ha un bordo incavato.
            /// </summary>
            WS_EX_CLIENTEDGE = 512,
            /// <summary>
            /// Disegna tutti i discendenti di una finestra dal basso verso l'alto usando il doppio buffering.
            /// </summary>
            /// <remarks>Questo modo di disegno permette alle finestre discendenti di avere trasparenza ed effetti di trasparenza ma solo se lo stile <see cref="WS_EX_TRANSPARENT"/> è applicato.<br/>
            /// Il doppio buffering permette alla finestra e ai suoi discendenti di essere disegnati senza sfarfallio.<br/>
            /// Questo stile non può essere usato quando la classe della finestra ha uno stile <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_OWNDC"/> oppure <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_CLASSDC"/>.</remarks>
            WS_EX_COMPOSITED = 33554432,
            /// <summary>
            /// La barra del titolo della finestra include un punto di domanda.
            /// </summary>
            /// <remarks>Quando l'utente clicca il punto di domanda, il cursore si trasforma in un punto di domanda con un puntatore.<br/>
            /// Se l'utente clicca una finestra figlia, essa riceve una messaggio <see cref="Window.WindowMessages.WM_HELP"/> che la finestra figlia passa alla procedura del padre.<br/><br/>
            /// Questo stile non può essere usato insieme a <see cref="WindowStyles.WS_MAXIMIZEBOX"/> oppure <see cref="WindowStyles.WS_MINIMIZEBOX"/>.</remarks>
            WS_EX_CONTEXTHELP = 1024,
            /// <summary>
            /// La finestra contiene finestre figlie che dovrebbero prendere parte nella navigazione del box di dialogo.
            /// </summary>
            /// <remarks>Se questo stile è specificato, il gestore della finestra di dialogo ricerca tra i figli della finestra quando esegue operazioni di navigazione come la gestione del tasto TAB, di una freccia direzionale o di un mnemonico da tastiera.</remarks>
            WS_EX_CONTROLPARENT = 65536,
            /// <summary>
            /// La finestra ha un bordo doppio.
            /// </summary>
            /// <remarks>La finestra può essere creata con una barra del titolo specificando <see cref="WindowStyles.WS_CAPTION"/>.</remarks>
            WS_EX_DLGMODALFRAME = 1,
            /// <summary>
            /// La finestra è a strati.
            /// </summary>
            /// <remarks>Questo stile non può essere usato quando la classe della finestra ha uno stile <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_OWNDC"/> oppure <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_CLASSDC"/>.</remarks>
            WS_EX_LAYERED = 524288,
            /// <summary>
            /// Se la lingua della shell supporta l'allineamento dell'ordine di lettura, l'origine orizzontale della finestra si trova sul bordo destro.
            /// </summary>
            WS_EX_LAYOUTRTL = 4194304,
            /// <summary>
            /// La finestra ha proprietà di allineamento verso sinistra generiche.
            /// </summary>
            /// <remarks>Questo è il valore predefinito.</remarks>
            WS_EX_LEFT = 0,
            /// <summary>
            /// Se la lingua della shell supporta l'allineamento dell'ordine di lettura, la barra di scorrimento verticale (se presente) si trova alla sinistra dell'area client.
            /// </summary>
            WS_EX_LEFTSCROLLBAR = 16384,
            /// <summary>
            /// Il testo della finestra viene visualizzato usando le proprietà dell'ordine di lettura da sinistra verso destra.
            /// </summary>
            /// <remarks>Questo è il valore predefinito.</remarks>
            WS_EX_LTRREADING = 0,
            /// <summary>
            /// La finestra è una finestra figlia MDI.
            /// </summary>
            WS_EX_MDICHILD = 64,
            /// <summary>
            /// Una finestra top-level con questo stile non diventa la finestra in primo piano quando l'utente clicca su di essa, ne quando essa viene ridotta a icona o se l'attuale finestra in primo piano viene chiusa.
            /// </summary>
            /// <remarks>La finestra non dovrebbe essere attivata tramite programmazione oppure tramite la navigazione da tastiera usando tecnologia di accessibilità.<br/><br/>
            /// La finestra non appare sulla barra delle applicazioni per impostazione predefinita, è possibile forzare la sua presenza usando lo stile <see cref="WS_EX_APPWINDOW"/>.</remarks>
            WS_EX_NOACTIVATE = 134217728,
            /// <summary>
            /// La finestra non passa il suo layout alle sue figlie.
            /// </summary>
            WS_EX_NOINHERITLAYOUT = 1048576,
            /// <summary>
            /// La finestra figlia creata con questo stile non invia il messaggio <see cref="Window.WindowMessages.WM_PARENTNOTIFY"/> alla finestra padre quando viene creata o distrutta.
            /// </summary>
            WS_EX_NOPARENTNOTIFY = 4,
            /// <summary>
            /// La finestra non viene renderizzata su una superficie di ridirezionamento.
            /// </summary>
            /// <remarks>Questo stile è previsto per finestre che non hanno contenuto visibile o che usano un meccanismo diverso dalle superfici per visualizzare il proprio contenuto.</remarks>
            WS_EX_NOREDIRECTIONBITMAP = 2097152,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            WS_EX_OVERLAPPEDWINDOW =
                WS_EX_WINDOWEDGE |
                WS_EX_CLIENTEDGE,
            /// <summary>
            /// La finestra è una finestra di tavolozza.
            /// </summary>
            /// <remarks>Le finestre di tavolozza sono box di dialogo che presentano una serie di comandi.</remarks>
            WS_EX_PALETTEWINDOW =
                WS_EX_WINDOWEDGE |
                WS_EX_TOOLWINDOW |
                WS_EX_TOPMOST,
            /// <summary>
            /// La finestra ha proprietà di allineamento a destra generiche.
            /// </summary>
            /// <remarks>Il comportamento della finestra dipende dalla sua classe, lo stile ha effetto solo la lingua della shell supporta l'allineamento dell'ordine di lettura.</remarks>
            WS_EX_RIGHT = 4096,
            /// <summary>
            /// La barra di scorrimento verticale (se presente) si trova alla destra dell'area client.
            /// </summary>
            /// <remarks>Questo è il valore predefinito.</remarks>
            WS_EX_RIGHTSCROLLBAR = 0,
            /// <summary>
            /// se la lingua della shell supporta l'allineamento dell'ordine di lettura, il testo della finestra viene visualizzato usando proprietà dell'ordine di lettura da destra verso sinistra.
            /// </summary>
            WS_EX_RTLREADING = 8192,
            /// <summary>
            /// I bordi della finestra hanno uno stile a tre dimensioni inteso per oggetti che non accettano input dall'utente.
            /// </summary>
            WS_EX_STATICEDGE = 131072,
            /// <summary>
            /// La finestra è intesa per essere usata come barra degli strumenti fluttuante.
            /// </summary>
            WS_EX_TOOLWINDOW = 128,
            /// <summary>
            /// La finesta dovrebbe essere posizionata sopra tutte le finestre non-topmost e dovrebbe restare in posizione anche quando viene disattivata.
            /// </summary>
            WS_EX_TOPMOST = 8,
            /// <summary>
            /// La finestra non dovrebbe essere disegnata fino a quando gli oggetti al di sotto di essa (creati dallo stesso thread) sono stati disegnati.
            /// </summary>
            WS_EX_TRANSPARENT = 32,
            /// <summary>
            /// La finestra ha un bordo rialzato.
            /// </summary>
            WS_EX_WINDOWEDGE = 256
        }

        /// <summary>
        /// Colori predefiniti di sistema.
        /// </summary>
        internal enum SystemColors
        {

            COLOR_SCROLLBAR,

            COLOR_BACKGROUND,

            COLOR_ACTIVECAPTION,

            COLOR_INACTIVECAPTION,

            COLOR_MENU,

            COLOR_WINDOW,

            COLOR_WINDOWFRAME,

            COLOR_MENUTEXT,

            COLOR_WINDOWTEXT,

            COLOR_CAPTIONTEXT,

            COLOR_ACTIVEBORDER,

            COLOR_INACTIVEBORDER,

            COLOR_APPWORKSPACE,

            COLOR_HIGHLIGHT,

            COLOR_HIGHLIGHTTEXT,

            COLOR_BTNFACE,

            COLOR_BTNSHADOW,

            COLOR_GRAYTEXT,

            COLOR_BTNTEXT,

            COLOR_INACTIVECAPTIONTEXT,

            COLOR_BTNHIGHLIGHT,

            COLOR_3DDKSHADOW,

            COLOR_3DLIGHT,

            COLOR_INFOTEXT,

            COLOR_INFOBK,

            COLOR_HOTLIGHT = 26,

            COLOR_GRADIENTACTIVECAPTION,

            COLOR_GRADIENTINACTIVECAPTION,

            COLOR_MENUHILIGHT,

            COLOR_MENUBAR,

            COLOR_DESKTOP = COLOR_BACKGROUND,

            COLOR_3DFACE = COLOR_BTNFACE,

            COLOR_3DSHADOW = COLOR_BTNSHADOW,

            COLOR_3DHIGHLIGHT = COLOR_BTNHIGHLIGHT,

            COLOR_3DHILIGHT = COLOR_BTNHIGHLIGHT,

            COLOR_BTNHILIGHT = COLOR_BTNHIGHLIGHT
        }
    }
}