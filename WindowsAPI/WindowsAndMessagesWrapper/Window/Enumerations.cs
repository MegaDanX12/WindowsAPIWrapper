using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.WindowsAndMessagesWrapper.Native.Window;

namespace WindowsAPI.WindowsAndMessagesWrapper.Window
{
    /// <summary>
    /// Enumerazioni relative alle finestre.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Stili di una finestra.
        /// </summary>
        [Flags]
        public enum WindowStyles : uint
        {
            /// <summary>
            /// La finestra ha un bordo sottile.
            /// </summary>
            HasThinLineBorder = WindowEnumerations.WindowStyles.WS_BORDER,
            /// <summary>
            /// La finestra ha una barra del titolo.
            /// </summary>
            /// <remarks>Lo stile <see cref="HasThinLineBorder"/> è incluso.</remarks>
            HasTitleBar = WindowEnumerations.WindowStyles.WS_CAPTION,
            /// <summary>
            /// La finestra è una finestra figlia.
            /// </summary>
            /// <remarks>Questo stile non può essere usato insieme a <see cref="IsPopupWindow"/>.</remarks>
            IsChildWindow = WindowEnumerations.WindowStyles.WS_CHILD,
            /// <summary>
            /// Esclude l'area occupata dalle finestre figlia al momento del disegno all'interno della finestra padre.
            /// </summary>
            /// <remarks>Questo stile è usato al momento della creazione della finestra padre.</remarks>
            ClipChildren = WindowEnumerations.WindowStyles.WS_CLIPCHILDREN,
            /// <summary>
            /// Nasconde le finestre figlie tra di loro.
            /// </summary>
            /// <remarks>Quando una finestra figlia riceve il messaggio <see cref="Window.WindowMessages.WM_PAINT"/>, questo stile taglia tutte le finestre figlie sovrapposta ad essa fuori dalla regione da aggiornare.</remarks>
            ClipSiblings = WindowEnumerations.WindowStyles.WS_CLIPSIBLINGS,
            /// <summary>
            /// La finestra è inizialmente disabilitata.
            /// </summary>
            IsDisabled = WindowEnumerations.WindowStyles.WS_DISABLED,
            /// <summary>
            /// La finestra ha un bordo tipicamente usato con le finestre di dialogo.
            /// </summary>
            /// <remarks>Una finestra con questo stile non può avere una barra del titolo.</remarks>
            HasDialogStyledBorder = WindowEnumerations.WindowStyles.WS_DLGFRAME,
            /// <summary>
            /// La finestra è il primo controllo di un gruppo di controlli.
            /// </summary>
            /// <remarks>Il gruppo consiste di questo controllo e di tutti quelli definiti dopo fino al prossimo con questo stile.</remarks>
            IsFirstControlOfGroup = WindowEnumerations.WindowStyles.WS_GROUP,
            /// <summary>
            /// La finestra ha una barra di scorrimento orizzontale.
            /// </summary>
            HasHorizontalScrollBar = WindowEnumerations.WindowStyles.WS_HSCROLL,
            /// <summary>
            /// La finestra è inizialmente ridotta a icona.
            /// </summary>
            IsMinimized = WindowEnumerations.WindowStyles.WS_ICONIC,
            /// <summary>
            /// La finestra è inizialmente ingrandita.
            /// </summary>
            IsMaximized = WindowEnumerations.WindowStyles.WS_MAXIMIZE,
            /// <summary>
            /// La finestra ha un pulsante "Ingrandisci".
            /// </summary>
            /// <remarks>Questo stile non può essere combinato con <see cref="WindowExtendedStyles.ContextHelp"/>, lo stile <see cref="HasWindowMenu"/> deve essere specificato.</remarks>
            HasMaximizeButton = WindowEnumerations.WindowStyles.WS_MAXIMIZEBOX,
            /// <summary>
            /// La finestra ha un pulsante "Riduci a icona".
            /// </summary>
            /// <remarks>Questo stile non può essere combinato con <see cref="WindowExtendedStyles.ContextHelp"/>, lo stile <see cref="HasWindowMenu"/> deve essere specificato.</remarks>
            HasMinimizeButton = WindowEnumerations.WindowStyles.WS_MINIMIZEBOX,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            /// <remarks>Una finestra sovrapposta ha una barra del titolo e un bordo.</remarks>
            IsOverlappedWindow = WindowEnumerations.WindowStyles.WS_OVERLAPPED,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            IsOverlappedWindow2 = WindowEnumerations.WindowStyles.WS_OVERLAPPEDWINDOW,
            /// <summary>
            /// La finestra è una finestra popup.
            /// </summary>
            /// <remarks>Questo stile non può essere usato con <see cref="IsChildWindow"/>.</remarks>
            IsPopupWindow = WindowEnumerations.WindowStyles.WS_POPUP,
            /// <summary>
            /// La finestra è una finestra popup.
            /// </summary>
            /// <remarks>Questo stile deve essere combinato con <see cref="HasTitleBar"/> perché il menù sia visibile.</remarks>
            IsPopupWindow2 = WindowEnumerations.WindowStyles.WS_POPUPWINDOW,
            /// <summary>
            /// La finestra ha un bordo di ridimensionamento.
            /// </summary>
            CanBeResized = WindowEnumerations.WindowStyles.WS_SIZEBOX,
            /// <summary>
            /// La finestra ha un menù sulla barra del titolo.
            /// </summary>
            /// <remarks>Deve essere specificato anche lo stile <see cref="HasTitleBar"/>.</remarks>
            HasWindowMenu = WindowEnumerations.WindowStyles.WS_SYSMENU,
            /// <summary>
            /// La finestra è un controllo che riceve il focus della tastiera quando l'utente preme il tasto TAB.
            /// </summary>
            CanReceiveKeyboardFocusOnTab = WindowEnumerations.WindowStyles.WS_TABSTOP,
            /// <summary>
            /// La finestra è inizialmente visibile.
            /// </summary>
            IsVisible = WindowEnumerations.WindowStyles.WS_VISIBLE,
            /// <summary>
            /// La finestra ha una barra di scorrimento verticale.
            /// </summary>
            HasVeticalScrollBar = WindowEnumerations.WindowStyles.WS_VSCROLL
        }

        /// <summary>
        /// Stili estesi di una finestra.
        /// </summary>
        [Flags]
        public enum ExtendedWindowStyles : uint
        {
            /// <summary>
            /// La finestra accetta file tramite drag-drop.
            /// </summary>
            AcceptsDragDropFiles = WindowEnumerations.WindowExtendedStyles.WS_EX_ACCEPTFILES,
            /// <summary>
            /// Forza una finestra top-level sulla barra delle applicazioni quando la finestra è visibile.
            /// </summary>
            ForceOnTaskbarWhenVisible = WindowEnumerations.WindowExtendedStyles.WS_EX_APPWINDOW,
            /// <summary>
            /// La finestra ha un bordo incavato.
            /// </summary>
            HasBorderWithSunkenEdge = WindowEnumerations.WindowExtendedStyles.WS_EX_CLIENTEDGE,
            /// <summary>
            /// Disegna tutti i discendenti di una finestra dal basso verso l'alto usando il doppio buffering.
            /// </summary>
            /// <remarks>Questo modo di disegno permette alle finestre discendenti di avere trasparenza ed effetti di trasparenza ma solo se lo stile <see cref="PaintAfterSiblings"/> è applicato.<br/>
            /// Il doppio buffering permette alla finestra e ai suoi discendenti di essere disegnati senza sfarfallio.<br/>
            /// Questo stile non può essere usato quando la classe della finestra ha uno stile <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_OWNDC"/> oppure <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_CLASSDC"/>.</remarks>
            Composited = WindowEnumerations.WindowExtendedStyles.WS_EX_COMPOSITED,
            /// <summary>
            /// La barra del titolo della finestra include un punto di domanda.
            /// </summary>
            /// <remarks>Quando l'utente clicca il punto di domanda, il cursore si trasforma in un punto di domanda con un puntatore.<br/>
            /// Se l'utente clicca una finestra figlia, essa riceve una messaggio <see cref="Window.WindowMessages.WM_HELP"/> che la finestra figlia passa alla procedura del padre.<br/><br/>
            /// Questo stile non può essere usato insieme a <see cref="WindowStyles.HasMaximizeButton"/> oppure <see cref="WindowStyles.HasMinimizeButton"/>.</remarks>
            ContextHelp = WindowEnumerations.WindowExtendedStyles.WS_EX_CONTEXTHELP,
            /// <summary>
            /// La finestra contiene finestre figlie che dovrebbero prendere parte nella navigazione del box di dialogo.
            /// </summary>
            /// <remarks>Se questo stile è specificato, il gestore della finestra di dialogo ricerca tra i figli della finestra quando esegue operazioni di navigazione come la gestione del tasto TAB, di una freccia direzionale o di un mnemonico da tastiera.</remarks>
            IsDialogNavParent = WindowEnumerations.WindowExtendedStyles.WS_EX_CONTROLPARENT,
            /// <summary>
            /// La finestra ha un bordo doppio.
            /// </summary>
            /// <remarks>La finestra può essere creata con una barra del titolo specificando <see cref="WindowStyles.HasTitleBar"/>.</remarks>
            HasDoubleBorder = WindowEnumerations.WindowExtendedStyles.WS_EX_DLGMODALFRAME,
            /// <summary>
            /// La finestra è a strati.
            /// </summary>
            /// <remarks>Questo stile non può essere usato quando la classe della finestra ha uno stile <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_OWNDC"/> oppure <see cref="WindowClasses.WindowClassesEnumerations.ClassStyles.CS_CLASSDC"/>.</remarks>
            IsLayeredWindow = WindowEnumerations.WindowExtendedStyles.WS_EX_LAYERED,
            /// <summary>
            /// Se la lingua della shell supporta l'allineamento dell'ordine di lettura, l'origine orizzontale della finestra si trova sul bordo destro.
            /// </summary>
            HasRtlLayout = WindowEnumerations.WindowExtendedStyles.WS_EX_LAYOUTRTL,
            /// <summary>
            /// La finestra ha proprietà di allineamento verso sinistra generiche.
            /// </summary>
            /// <remarks>Questo è il valore predefinito.</remarks>
            IsAlignedLeft = WindowEnumerations.WindowExtendedStyles.WS_EX_LEFT,
            /// <summary>
            /// Se la lingua della shell supporta l'allineamento dell'ordine di lettura, la barra di scorrimento verticale (se presente) si trova alla sinistra dell'area client.
            /// </summary>
            VerticalScrollBarOnLeftSide = WindowEnumerations.WindowExtendedStyles.WS_EX_LEFTSCROLLBAR,
            /// <summary>
            /// Il testo della finestra viene visualizzato usando le proprietà dell'ordine di lettura da sinistra verso destra.
            /// </summary>
            /// <remarks>Questo è il valore predefinito.</remarks>
            UsesLtrReadingOrder = WindowEnumerations.WindowExtendedStyles.WS_EX_LTRREADING,
            /// <summary>
            /// La finestra è una finestra figlia MDI.
            /// </summary>
            ISMDIChildWindow = WindowEnumerations.WindowExtendedStyles.WS_EX_MDICHILD,
            /// <summary>
            /// Una finestra top-level con questo stile non diventa la finestra in primo piano quando l'utente clicca su di essa, ne quando essa viene ridotta a icona o se l'attuale finestra in primo piano viene chiusa.
            /// </summary>
            /// <remarks>La finestra non dovrebbe essere attivata tramite programmazione oppure tramite la navigazione da tastiera usando tecnologia di accessibilità.<br/><br/>
            /// La finestra non appare sulla barra delle applicazioni per impostazione predefinita, è possibile forzare la sua presenza usando lo stile <see cref="ForceOnTaskbarWhenVisible"/>.</remarks>
            PreventWindowActivation = WindowEnumerations.WindowExtendedStyles.WS_EX_NOACTIVATE,
            /// <summary>
            /// La finestra non passa il suo layout alle sue figlie.
            /// </summary>
            DoesNotPassLayoutInfoToChildren = WindowEnumerations.WindowExtendedStyles.WS_EX_NOINHERITLAYOUT,
            /// <summary>
            /// La finestra figlia creata con questo stile non invia il messaggio <see cref="Window.WindowMessages.WM_PARENTNOTIFY"/> alla finestra padre quando viene creata o distrutta.
            /// </summary>
            DoNotNotifyParent = WindowEnumerations.WindowExtendedStyles.WS_EX_NOPARENTNOTIFY,
            /// <summary>
            /// La finestra non viene renderizzata su una superficie di ridirezionamento.
            /// </summary>
            /// <remarks>Questo stile è previsto per finestre che non hanno contenuto visibile o che usano un meccanismo diverso dalle superfici per visualizzare il proprio contenuto.</remarks>
            DoesNotRenderOnRedirectionSurface = WindowEnumerations.WindowExtendedStyles.WS_EX_NOREDIRECTIONBITMAP,
            /// <summary>
            /// La finestra è una finestra sovrapposta.
            /// </summary>
            IsOverlappedWindow = WindowEnumerations.WindowExtendedStyles.WS_EX_OVERLAPPEDWINDOW,
            /// <summary>
            /// La finestra è una finestra di tavolozza.
            /// </summary>
            /// <remarks>Le finestre di tavolozza sono box di dialogo che presentano una serie di comandi.</remarks>
            IsPaletteWindow = WindowEnumerations.WindowExtendedStyles.WS_EX_PALETTEWINDOW,
            /// <summary>
            /// La finestra ha proprietà di allineamento a destra generiche.
            /// </summary>
            /// <remarks>Il comportamento della finestra dipende dalla sua classe, lo stile ha effetto solo la lingua della shell supporta l'allineamento dell'ordine di lettura.</remarks>
            IsAlignedRight = WindowEnumerations.WindowExtendedStyles.WS_EX_RIGHT,
            /// <summary>
            /// La barra di scorrimento verticale (se presente) si trova alla destra dell'area client.
            /// </summary>
            /// <remarks>Questo è il valore predefinito.</remarks>
            VerticalScrollBarOnRightSide = WindowEnumerations.WindowExtendedStyles.WS_EX_RIGHTSCROLLBAR,
            /// <summary>
            /// se la lingua della shell supporta l'allineamento dell'ordine di lettura, il testo della finestra viene visualizzato usando proprietà dell'ordine di lettura da destra verso sinistra.
            /// </summary>
            UsesRtlReadingOrder = WindowEnumerations.WindowExtendedStyles.WS_EX_RTLREADING,
            /// <summary>
            /// I bordi della finestra hanno uno stile a tre dimensioni inteso per oggetti che non accettano input dall'utente.
            /// </summary>
            Has3DBorder = WindowEnumerations.WindowExtendedStyles.WS_EX_STATICEDGE,
            /// <summary>
            /// La finestra è intesa per essere usata come barra degli strumenti fluttuante.
            /// </summary>
            IsToolWindow = WindowEnumerations.WindowExtendedStyles.WS_EX_TOOLWINDOW,
            /// <summary>
            /// La finesta dovrebbe essere posizionata sopra tutte le finestre non-topmost e dovrebbe restare in posizione anche quando viene disattivata.
            /// </summary>
            IsTopMost = WindowEnumerations.WindowExtendedStyles.WS_EX_TOPMOST,
            /// <summary>
            /// La finestra non dovrebbe essere disegnata fino a quando gli oggetti al di sotto di essa (creati dallo stesso thread) sono stati disegnati.
            /// </summary>
            PaintAfterSiblings = WindowEnumerations.WindowExtendedStyles.WS_EX_TRANSPARENT,
            /// <summary>
            /// La finestra ha un bordo rialzato.
            /// </summary>
            HasRaisedEdge = WindowEnumerations.WindowExtendedStyles.WS_EX_WINDOWEDGE
        }
    }
}