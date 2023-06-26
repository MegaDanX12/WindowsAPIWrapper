using System;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonNotifications;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxNotifications;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Menus.MenuNotifications;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons
{
    /// <summary>
    /// Enumerazioni relative ai pulsanti.
    /// </summary>
    internal static class ButtonEnumerations
    {
        /// <summary>
        /// Stato del pulsante.
        /// </summary>
        [Flags]
        internal enum ButtonState
        {
            /// <summary>
            /// Premuto.
            /// </summary>
            BST_CHECKED = 1,
            /// <summary>
            /// Indeterminato.
            /// </summary>
            BST_INDETERMINATE,
            /// <summary>
            /// Non premuto.
            /// </summary>
            BST_UNCHECKED = 0,
            /// <summary>
            /// Il pulsante è nello stato "premuto".
            /// </summary>
            BST_PUSHED = 4,
            /// <summary>
            /// Il pulsante ha il focus della tastiera.
            /// </summary>
            BST_FOCUS = 8,
            /// <summary>
            /// Il mouse si trova sopra il pulsante.
            /// </summary>
            BST_HOT = 512,
            /// <summary>
            /// Il dropdown del pulsante è visibile.
            /// </summary>
            BST_DROPDOWNPUSHED = 1024
        }

        /// <summary>
        /// Stili dei pulsanti.
        /// </summary>
        [Flags]
        internal enum ButtonStyles
        {
            /// <summary>
            /// Pulsante a tre stati (selezionato, non selezionato e indeterminato).
            /// </summary>
            BS_3STATE = 5,
            /// <summary>
            /// Pulsante a tre stati (selezionato, non selezionato e indeterminato).
            /// </summary>
            /// <remarks>Simile a <see cref="BS_3STATE"/> ma il pulsante cambia automaticamente il suo stato quando l'utente clicca su di esso.</remarks>
            BS_AUTO3STATE,
            /// <summary>
            /// Checkbox automatico.
            /// </summary>
            /// <remarks>Simile a <see cref="BS_CHECKBOX"/> ma il suo stato cambia automaticamente quando l'utente clicca su di esso.</remarks>
            BS_AUTOCHECKBOX = 3,
            /// <summary>
            /// Radio button automatico.
            /// </summary>
            /// <remarks>Simile a <see cref="BS_RADIOBUTTON"/> ma il suo stato e quello di tutti gli altri cambia automaticamente quando l'utente clicca su di esso.</remarks>
            BS_AUTORADIOBUTTON = 9,
            /// <summary>
            /// Il pulsante visualizza un bitmap.
            /// </summary>
            BS_BITMAP = 128,
            /// <summary>
            /// Posiziona il testo sotto al rettangolo del pulsante.
            /// </summary>
            BS_BOTTOM = 2048,
            /// <summary>
            /// Centra orizzontalmente nel rettangolo del pulsante.
            /// </summary>
            BS_CENTER = 768,
            /// <summary>
            /// Crea un checkbox.
            /// </summary>
            /// <remarks>Per impostazione predefinita il testo si trova a destra del box.</remarks>
            BS_CHECKBOX = 2,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="BS_PUSHBUTTON"/>, ma il pulsante ha una freccia verde a sinistra che punta al testo.
            /// </summary>
            BS_COMMANDLINK = 14,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="BS_PUSHBUTTON"/>.
            /// </summary>
            /// <remarks>Se il pulsante si trova in un box di dialogo, l'utente può selezionare il pulsante premendo Invio, anche se esso non ha il focus.</remarks>
            BS_DEFCOMMANDLINK,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="BS_PUSHBUTTON"/> ma con un aspetto differente.
            /// </summary>
            /// <remarks>Se il pulsante si trova in un box di dialogo, l'utente può selezionare il pulsante premendo Invio, anche se esso non ha il focus.</remarks>
            BS_DEFPUSHBUTTON = 1,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="BS_PUSHBUTTON"/> ma con un aspetto differente.
            /// </summary>
            /// <remarks>Se il pulsante si trova in un box di dialogo, l'utente può selezionare il pulsante premendo Invio, anche se esso non ha il focus.</remarks>
            BS_DEFSPLITBUTTON = 13,
            /// <summary>
            /// Crea un rettangolo in cui possono essere inseriti altri controlli.
            /// </summary>
            /// <remarks>Il testo associato con questo stile viene visualizzato nell'angolo superiore sinistro del rettangolo.</remarks>
            BS_GROUPBOX = 7,
            /// <summary>
            /// Il pulsante visualizza un'icona.
            /// </summary>
            BS_ICON = 64,
            /// <summary>
            /// Il pulsante è a due dimensioni.
            /// </summary>
            /// <remarks>Non utilizza lo shading predefinito per craere un'immagine 3D.</remarks>
            BS_FLAT = 32768,
            /// <summary>
            /// Giustifica a sinistra il testo in un rettangolo di un pulsante.
            /// </summary>
            /// <remarks>Se il pulsante e un checkbox o un radio button che non ha lo stile <see cref="BS_RIGHTBUTTON"/> applicato, il testo è giustificato a sinistra alla destra del pulsante.</remarks>
            BS_LEFT = 256,
            /// <summary>
            /// Posiziona il testo alla sinistra del radio button o del checkbox se combinato con uno degli stili.
            /// </summary>
            BS_LEFTTEXT = 32,
            /// <summary>
            /// Avvolge il testo del pulsante in linee multiple se è troppo lungo per una singola linea nel rettangolo del pulsante.
            /// </summary>
            BS_MULTILINE = 8192,
            /// <summary>
            /// Permette a un pulsante di inviare le notifiche <see cref="BN_KILLFOCUS"/> e <see cref="BN_SETFOCUS"/> alla finestra padre.
            /// </summary>
            /// <remarks>I pulsanti inviano la notifica <see cref="BN_CLICKED"/> anche se non hanno questo stile applicato, per ricevere la notifica <see cref="BN_DBLCLK"/>, il pulsante deve avere lo stile <see cref="BS_OWNERDRAW"/> oppure <see cref="BS_RADIOBUTTON"/> applicato.</remarks>
            BS_NOTIFY = 16384,
            /// <summary>
            /// Crea un pulsante disegnato dal proprietario.
            /// </summary>
            /// <remarks>La finestra proprietaria riceve un messaggio <see cref="WM_DRAWITEM"/> quando un'aspetto visuale del pulsante è cambiato.<br/><br/>
            /// Questo stile non può essere combinato con nessun'altro.</remarks>
            BS_OWNERDRAW = 11,
            /// <summary>
            /// Crea un push button che posta un messaggio <see cref="WM_COMMAND"/> alla finestra proprietaria quando l'utente seleziona un pulsante.
            /// </summary>
            BS_PUSHBUTTON = 0,
            /// <summary>
            /// Fa in modo che un pulsante sia visualizzato e agisca come un push button.
            /// </summary>
            /// <remarks>Il pulsante sembra sollevato quando non è premuto o selezionato e affondato quando è premuto o selezionato.</remarks>
            BS_PUSHLIKE = 4096,
            /// <summary>
            /// Crea un piccolo cerchio con testo.
            /// </summary>
            /// <remarks>Per impostazione predefinita il testo è visualizzato alla destra del cerchio.<br/>
            /// Per visualizzare il testo alla sinistra del cerchio, combinare questo valore con <see cref="BS_LEFTTEXT"/> oppure con l'equivalente <see cref="BS_RIGHTBUTTON"/>.<br/>
            /// Usare i radio button per gruppi di scelte correlate ma mutualmente esclusive.</remarks>
            BS_RADIOBUTTON = 4,
            /// <summary>
            /// Giustifica a destra il testo nel rettangolo di un pulsante.
            /// </summary>
            /// <remarks>Se il pulsante è un checkbox o un radio button al quale lo stile <see cref="BS_RIGHTBUTTON"/> non è applicato, il testo è giustificato a destra sulla parte destra del checkbox o del radio button.</remarks>
            BS_RIGHT = 512,
            /// <summary>
            /// Posiziona il cerchio del radio button o il quadrato del check box alla parte destra del rettangolo del pulsante.
            /// </summary>
            /// <remarks>Questo stile è equivalente allo stile <see cref="BS_LEFTTEXT"/>.</remarks>
            BS_RIGHTBUTTON = BS_LEFTTEXT,
            /// <summary>
            /// Crea uno split button.
            /// </summary>
            /// <remarks>Uno split button ha una freccia di dropdown.</remarks>
            BS_SPLITBUTTON = 12,
            /// <summary>
            /// Il pulsante visualizza del testo.
            /// </summary>
            BS_TEXT = 0,
            /// <summary>
            /// Posiziona il testo sulla parte superiore del rettangolo del pulsante.
            /// </summary>
            BS_TOP = 1024,
            /// <summary>
            /// Posiziona il testo al centro (verticalmente) del rettangolo del pulsante.
            /// </summary>
            BS_VCENTER = 3072
        }

        /// <summary>
        /// Allineamento della lista di immagini di un pulsante.
        /// </summary>
        internal enum ImagelistAlignment
        {
            /// <summary>
            /// Allinea l'immagine con il margine sinistro.
            /// </summary>
            BUTTON_IMAGELIST_ALIGN_LEFT,
            /// <summary>
            /// Allinea l'immagine con il margine destro.
            /// </summary>
            BUTTON_IMAGELIST_ALIGN_RIGHT,
            /// <summary>
            /// Centra l'immagine.
            /// </summary>
            BUTTON_IMAGELIST_ALIGN_CENTER = 4,
            /// <summary>
            /// Allinea l'immagine con il margine superiore.
            /// </summary>
            BUTTON_IMAGELIST_ALIGN_TOP = 2,
            /// <summary>
            /// Allinea l'immagine con il margine inferiore.
            /// </summary>
            BUTTON_IMAGELIST_ALIGN_BOTTOM
        }

        /// <summary>
        /// Campi validi della struttura <see cref="BUTTON_SPLITINFO"/>.
        /// </summary>
        [Flags]
        internal enum SplitInfoMembers
        {
            /// <summary>
            /// Il campo <see cref="BUTTON_SPLITINFO.ImagelistHandle"/> è valido.
            /// </summary>
            BCSIF_GLYPH = 1,
            /// <summary>
            /// Il campo <see cref="BUTTON_SPLITINFO.ImagelistHandle"/> è valido.
            /// </summary>
            /// <remarks>Da usare quando <see cref="BUTTON_SPLITINFO.SplitStyle"/> è impostato a <see cref="SplitStyle.BCSS_IMAGE"/>.</remarks>
            BCSIF_IMAGE,
            /// <summary>
            /// Il campo <see cref="BUTTON_SPLITINFO.GlyphSize"/> è valido.
            /// </summary>
            BCSIF_SIZE = 8,
            /// <summary>
            /// Il campo <see cref="BUTTON_SPLITINFO.SplitStyle"/> è valido.
            /// </summary>
            BCSIF_STYLE = 4
        }

        /// <summary>
        /// Stile della divisione.
        /// </summary>
        [Flags]
        internal enum SplitStyle
        {
            /// <summary>
            /// Allinea l'immagine o il glifo orizzontalmente al margine sinistro.
            /// </summary>
            BCSS_ALIGNLEFT = 4,
            /// <summary>
            /// Disegna l'immagine di un'icona come glifo.
            /// </summary>
            BCSS_IMAGE = 8,
            /// <summary>
            /// Nessuna divisione.
            /// </summary>
            BCSS_NOSPLIT = 1,
            /// <summary>
            /// Allunga il glifo, ma cerca di mantenere il rapporto d'aspetto.
            /// </summary>
            BCSS_STRETCH
        }
    }
}