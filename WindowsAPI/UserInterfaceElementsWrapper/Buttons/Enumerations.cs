using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListEnumerations;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Buttons
{
    /// <summary>
    /// Enumerazioni relative ai pulsanti.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Stato del pulsante.
        /// </summary>
        public enum ButtonState
        {
            /// <summary>
            /// Premuto.
            /// </summary>
            Checked = ButtonEnumerations.ButtonState.BST_CHECKED,
            /// <summary>
            /// Indeterminato.
            /// </summary>
            Indeterminate = ButtonEnumerations.ButtonState.BST_INDETERMINATE,
            /// <summary>
            /// Non premuto.
            /// </summary>
            Unchecked = ButtonEnumerations.ButtonState.BST_UNCHECKED,
            /// <summary>
            /// Il pulsante è nello stato "premuto".
            /// </summary>
            Pushed = ButtonEnumerations.ButtonState.BST_PUSHED,
            /// <summary>
            /// Il pulsante ha il focus della tastiera.
            /// </summary>
            HasKeyboardFocus = ButtonEnumerations.ButtonState.BST_FOCUS,
            /// <summary>
            /// Il mouse si trova sopra il pulsante.
            /// </summary>
            Hot = ButtonEnumerations.ButtonState.BST_HOT,
            /// <summary>
            /// Il dropdown del pulsante è visibile.
            /// </summary>
            DropdownPushed = ButtonEnumerations.ButtonState.BST_DROPDOWNPUSHED
        }

        /// <summary>
        /// Allineamento dell'immagine di un pulsante.
        /// </summary>
        public enum ButtonImageAlignment
        {
            /// <summary>
            /// Allinea l'immagine con il margine sinistro.
            /// </summary>
            Left = ButtonEnumerations.ImagelistAlignment.BUTTON_IMAGELIST_ALIGN_LEFT,
            /// <summary>
            /// Allinea l'immagine con il margine destro.
            /// </summary>
            Right = ButtonEnumerations.ImagelistAlignment.BUTTON_IMAGELIST_ALIGN_RIGHT,
            /// <summary>
            /// Allinea l'immagine con il margine superiore.
            /// </summary>
            Top = ButtonEnumerations.ImagelistAlignment.BUTTON_IMAGELIST_ALIGN_TOP,
            /// <summary>
            /// Allinea l'immagine con il margine inferiore.
            /// </summary>
            Bottom = ButtonEnumerations.ImagelistAlignment.BUTTON_IMAGELIST_ALIGN_BOTTOM,
            /// <summary>
            /// Centra l'immagine.
            /// </summary>
            Center = ButtonEnumerations.ImagelistAlignment.BUTTON_IMAGELIST_ALIGN_CENTER
        }

        /// <summary>
        /// Informazioni da richiedere su uno split button.
        /// </summary>
        [Flags]
        public enum SplitButtonRequiredInfo
        {
            /// <summary>
            /// Dimensione del glifo.
            /// </summary>
            GlyphSize = ButtonEnumerations.SplitInfoMembers.BCSIF_SIZE,
            /// <summary>
            /// Stile del pulsante.
            /// </summary>
            Style = ButtonEnumerations.SplitInfoMembers.BCSIF_STYLE
        }

        /// <summary>
        /// Tipo di immagine associata al pulsante.
        /// </summary>
        public enum ButtonImageType
        {
            /// <summary>
            /// Bitmap.
            /// </summary>
            Bitmap = ImageType.IMAGE_BITMAP,
            /// <summary>
            /// Icona.
            /// </summary>
            Icon = ImageType.IMAGE_ICON
        }

        /// <summary>
        /// Stili dei pulsanti.
        /// </summary>
        [Flags]
        public enum ButtonStyles
        {
            /// <summary>
            /// Pulsante a tre stati (selezionato, non selezionato e indeterminato).
            /// </summary>
            ThreeStateCheckbox = ButtonEnumerations.ButtonStyles.BS_3STATE,
            /// <summary>
            /// Pulsante a tre stati (selezionato, non selezionato e indeterminato).
            /// </summary>
            /// <remarks>Simile a <see cref="ThreeStateCheckbox"/> ma il pulsante cambia automaticamente il suo stato quando l'utente clicca su di esso.</remarks>
            ThreeStateAutoCheckbox = ButtonEnumerations.ButtonStyles.BS_AUTO3STATE,
            /// <summary>
            /// Checkbox automatico.
            /// </summary>
            /// <remarks>Simile a <see cref="Checkbox"/> ma il suo stato cambia automaticamente quando l'utente clicca su di esso.</remarks>
            AutoCheckbox = ButtonEnumerations.ButtonStyles.BS_AUTOCHECKBOX,
            /// <summary>
            /// Radio button automatico.
            /// </summary>
            /// <remarks>Simile a <see cref="RadioButton"/> ma il suo stato e quello di tutti gli altri cambia automaticamente quando l'utente clicca su di esso.</remarks>
            AutoRadioButton = ButtonEnumerations.ButtonStyles.BS_AUTORADIOBUTTON,
            /// <summary>
            /// Il pulsante visualizza un bitmap.
            /// </summary>
            DisplaysBitmap = ButtonEnumerations.ButtonStyles.BS_BITMAP,
            /// <summary>
            /// Posiziona il testo sotto al rettangolo del pulsante.
            /// </summary>
            TextAtBottom = ButtonEnumerations.ButtonStyles.BS_BOTTOM,
            /// <summary>
            /// Centra orizzontalmente nel rettangolo del pulsante.
            /// </summary>
            HorizontallyCenterText = ButtonEnumerations.ButtonStyles.BS_CENTER,
            /// <summary>
            /// Crea un checkbox.
            /// </summary>
            /// <remarks>Per impostazione predefinita il testo si trova a destra del box.</remarks>
            Checkbox = ButtonEnumerations.ButtonStyles.BS_CHECKBOX,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="PushButton"/>, ma il pulsante ha una freccia verde a sinistra che punta al testo.
            /// </summary>
            CommandLink = ButtonEnumerations.ButtonStyles.BS_COMMANDLINK,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="PushButton"/>.
            /// </summary>
            /// <remarks>Se il pulsante si trova in un box di dialogo, l'utente può selezionare il pulsante premendo Invio, anche se esso non ha il focus.</remarks>
            DefCommandLink = ButtonEnumerations.ButtonStyles.BS_DEFCOMMANDLINK,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="PushButton"/> ma con un aspetto differente.
            /// </summary>
            /// <remarks>Se il pulsante si trova in un box di dialogo, l'utente può selezionare il pulsante premendo Invio, anche se esso non ha il focus.</remarks>
            DefPushButton = ButtonEnumerations.ButtonStyles.BS_DEFPUSHBUTTON,
            /// <summary>
            /// Crea un pulsante che funziona allo stesso modo di un <see cref="PushButton"/> ma con un aspetto differente.
            /// </summary>
            /// <remarks>Se il pulsante si trova in un box di dialogo, l'utente può selezionare il pulsante premendo Invio, anche se esso non ha il focus.</remarks>
            DefSplitButton = ButtonEnumerations.ButtonStyles.BS_DEFSPLITBUTTON,
            /// <summary>
            /// Crea un rettangolo in cui possono essere inseriti altri controlli.
            /// </summary>
            /// <remarks>Il testo associato con questo stile viene visualizzato nell'angolo superiore sinistro del rettangolo.</remarks>
            Groupbox = ButtonEnumerations.ButtonStyles.BS_GROUPBOX,
            /// <summary>
            /// Il pulsante visualizza un'icona.
            /// </summary>
            DisplaysIcon = ButtonEnumerations.ButtonStyles.BS_ICON,
            /// <summary>
            /// Il pulsante è a due dimensioni.
            /// </summary>
            /// <remarks>Non utilizza lo shading predefinito per craere un'immagine 3D.</remarks>
            TwoDimensionalButton = ButtonEnumerations.ButtonStyles.BS_FLAT,
            /// <summary>
            /// Giustifica a sinistra il testo in un rettangolo di un pulsante.
            /// </summary>
            /// <remarks>Se il pulsante e un checkbox o un radio button che non ha lo stile <see cref="SelectorOnRightSide"/> applicato, il testo è giustificato a sinistra alla destra del pulsante.</remarks>
            LeftJustifiedText = ButtonEnumerations.ButtonStyles.BS_LEFT,
            /// <summary>
            /// Posiziona il testo alla sinistra del radio button o del checkbox se combinato con uno degli stili.
            /// </summary>
            TextOnLeftSideOfButton = ButtonEnumerations.ButtonStyles.BS_LEFTTEXT,
            /// <summary>
            /// Avvolge il testo del pulsante in linee multiple se è troppo lungo per una singola linea nel rettangolo del pulsante.
            /// </summary>
            WrapText = ButtonEnumerations.ButtonStyles.BS_MULTILINE,
            /// <summary>
            /// Permette a un pulsante di inviare le notifiche relative alla perdita e alla ricezione del focus alla finestra padre.
            /// </summary>
            SendFocusNotifications = ButtonEnumerations.ButtonStyles.BS_NOTIFY,
            /// <summary>
            /// Crea un pulsante disegnato dal proprietario.
            /// </summary>
            /// <remarks>Questo stile non può essere combinato con nessun'altro.</remarks>
            OwnerDrawn = ButtonEnumerations.ButtonStyles.BS_OWNERDRAW,
            /// <summary>
            /// Crea un push button che posta un messaggio <see cref="WM_COMMAND"/> alla finestra proprietaria quando l'utente seleziona un pulsante.
            /// </summary>
            PushButton = ButtonEnumerations.ButtonStyles.BS_PUSHBUTTON,
            /// <summary>
            /// Fa in modo che un pulsante sia visualizzato e agisca come un push button.
            /// </summary>
            /// <remarks>Il pulsante sembra sollevato quando non è premuto o selezionato e affondato quando è premuto o selezionato.</remarks>
            PushLikeButton = ButtonEnumerations.ButtonStyles.BS_PUSHLIKE,
            /// <summary>
            /// Crea un piccolo cerchio con testo.
            /// </summary>
            /// <remarks>Per impostazione predefinita il testo è visualizzato alla destra del cerchio.<br/>
            /// Per visualizzare il testo alla sinistra del cerchio, combinare questo valore con <see cref="TextOnLeftSideOfButton"/> oppure con l'equivalente <see cref="SelectorOnRightSide"/>.<br/>
            /// Usare i radio button per gruppi di scelte correlate ma mutualmente esclusive.</remarks>
            RadioButton = ButtonEnumerations.ButtonStyles.BS_RADIOBUTTON,
            /// <summary>
            /// Giustifica a destra il testo nel rettangolo di un pulsante.
            /// </summary>
            /// <remarks>Se il pulsante è un checkbox o un radio button al quale lo stile <see cref="SelectorOnRightSide"/> non è applicato, il testo è giustificato a destra sulla parte destra del checkbox o del radio button.</remarks>
            RightJustifiedText = ButtonEnumerations.ButtonStyles.BS_RIGHT,
            /// <summary>
            /// Posiziona il cerchio del radio button o il quadrato del check box alla parte destra del rettangolo del pulsante.
            /// </summary>
            /// <remarks>Questo stile è equivalente allo stile <see cref="TextOnLeftSideOfButton"/>.</remarks>
            SelectorOnRightSide = ButtonEnumerations.ButtonStyles.BS_RIGHTBUTTON,
            /// <summary>
            /// Crea uno split button.
            /// </summary>
            /// <remarks>Uno split button ha una freccia di dropdown.</remarks>
            SplitButton = ButtonEnumerations.ButtonStyles.BS_SPLITBUTTON,
            /// <summary>
            /// Il pulsante visualizza del testo.
            /// </summary>
            ShowsText = ButtonEnumerations.ButtonStyles.BS_TEXT,
            /// <summary>
            /// Posiziona il testo sulla parte superiore del rettangolo del pulsante.
            /// </summary>
            TextAtTop = ButtonEnumerations.ButtonStyles.BS_TOP,
            /// <summary>
            /// Posiziona il testo al centro (verticalmente) del rettangolo del pulsante.
            /// </summary>
            TextVerticallyCenters = ButtonEnumerations.ButtonStyles.BS_VCENTER
        }
    }
}