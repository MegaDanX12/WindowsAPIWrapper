using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls
{
    /// <summary>
    /// Enumerazioni comuni per i controlli Windows.
    /// </summary>
    internal static class CommonControlsEnumerations
    {
        /// <summary>
        /// Informazioni oggetto attivo.
        /// </summary>
        [Flags]
        internal enum HotItemFlags
        {

            HICF_OTHER,

            HICF_MOUSE,

            HICF_ARROWKEYS = 2,

            HICF_ACCELERATOR = 4,

            HICF_DUPACCEL = 8,
            /// <summary>
            /// Il mouse è entrato nel perimetro del pulsante.
            /// </summary>
            HICF_ENTERING = 16,
            /// <summary>
            /// Il mouse è uscito dal perimetro del pulsante.
            /// </summary>
            HICF_LEAVING = 32,

            HICF_RESELECT = 64,

            HICF_LMOUSE = 128,

            HICF_TOGGLEDROPDOWN = 256
        }

        /// <summary>
        /// Passaggio di disegno.
        /// </summary>
        internal enum DrawingStage
        {
            /// <summary>
            /// Dopo il termine del ciclo di cancellazione
            /// </summary>
            CDDS_POSTERASE = 4,
            /// <summary>
            /// Dopo il termine del ciclo di disegno.
            /// </summary>
            CDDS_POSTPAINT = 2,
            /// <summary>
            /// Prima dell'inizio del ciclo di cancellazione.
            /// </summary>
            CDDS_PREERASE = 3,
            /// <summary>
            /// Prima dell'inizio del ciclo di disegno.
            /// </summary>
            CDDS_PREPAINT = 1,
            /// <summary>
            /// Indica che i campi <see cref="NMCUSTOMDRAWINFO.ItemNumber"/>, <see cref="NMCUSTOMDRAWINFO.ItemState"/> e <see cref="NMCUSTOMDRAWINFO.AppData"/> sono validi.
            /// </summary>
            CDDS_ITEM = 65536,
            /// <summary>
            /// Dopo che un oggetto è stato cancellato.
            /// </summary>
            CDDS_ITEMPOSTERASE =
                CDDS_ITEM |
                CDDS_POSTERASE,
            /// <summary>
            /// Dopo che un oggetto è stato disegnato.
            /// </summary>
            CDDS_ITEMPOSTPAINT =
                CDDS_ITEM |
                CDDS_POSTPAINT,
            /// <summary>
            /// Prima che un oggetto sia cancellato.
            /// </summary>
            CDDS_ITEMPREERASE =
                CDDS_ITEM |
                CDDS_PREERASE,
            /// <summary>
            /// Prima che un oggetto sia disegnato.
            /// </summary>
            CDDS_ITEMPREPAINT =
                CDDS_ITEM |
                CDDS_PREPAINT,
            /// <summary>
            /// Valore combinato con <see cref="CDDS_ITEMPREPAINT"/> oppure <see cref="CDDS_ITEMPOSTPAINT"/> se un sottoelemento è in corso di disegno.
            /// </summary>
            /// <remarks>Questo valore viene impostato solo se il valore restituito da <see cref="CDDS_PREPAINT"/> è <see cref="CustomDrawReturnValue.CDRF_NOTIFYITEMDRAW"/>.</remarks>
            CDDS_SUBITEM = 131072
        }

        /// <summary>
        /// Stato dell'oggetto.
        /// </summary>
        [Flags]
        internal enum ItemState
        {
            /// <summary>
            /// L'oggetto è selezionato.
            /// </summary>
            CDIS_CHECKED = 8,
            /// <summary>
            /// L'oggetto è nello stato predefinito.
            /// </summary>
            CDIS_DEFAULT = 32,
            /// <summary>
            /// L'oggetto è disabilitato.
            /// </summary>
            CDIS_DISABLED = 4,
            /// <summary>
            /// L'oggetto è in focus.
            /// </summary>
            CDIS_FOCUS = 16,
            /// <summary>
            /// L'oggetto è disattivato.
            /// </summary>
            CDIS_GRAYED = 2,
            /// <summary>
            /// L'oggetto si trova sotto il puntatore.
            /// </summary>
            CDIS_HOT = 64,
            /// <summary>
            /// L'oggetto si trova in uno stato indeterminato.
            /// </summary>
            CDIS_INDETERMINATE = 256,
            /// <summary>
            /// L'oggetto è contrassegnato.
            /// </summary>
            CDIS_MARKED = 128,
            /// <summary>
            /// L'oggetto è selezionato.
            /// </summary>
            CDIS_SELECTED = 1,
            /// <summary>
            /// L'oggetto visualizza gli spunti tastiera.
            /// </summary>
            CDIS_SHOWKEYBOARDCUES = 512,
            /// <summary>
            /// L'oggetto è parte in un controllo che si trova sotto il puntatore.
            /// </summary>
            CDIS_NEARHOT = 1024,
            /// <summary>
            /// L'oggetto è parte di uno split button che si trova sotto il puntatore.
            /// </summary>
            CDIS_OTHERSIDEHOT = 2048,
            /// <summary>
            /// L'oggetto è attualmente il bersaglio di rilascio di un'operazione di drag-and-drop.
            /// </summary>
            CDIS_DROPTHILITED = 4096
        }

        /// <summary>
        /// Valori restituiti per il messaggio NM_CUSTOMDRAW.
        /// </summary>
        internal enum CustomDrawReturnValue
        {

            CDRF_DEFAULT,

            CDRF_NEWFONT = 2,

            CDRF_DOERASE = 8,

            CDRF_SKIPPOSTPAINT = 256,

            CDRF_NOTIFYITEMDRAW = 32,

            CDRF_NOTIFYSUBITEMDRAW = CDRF_NOTIFYITEMDRAW,
            /// <summary>
            /// Il controllo notificherà il padre dopo la cancellazione di un oggetto.
            /// </summary>
            /// <remarks>Questo valore può essere usato solo il passaggio di disegno in corso è <see cref="DrawingStage.CDDS_PREERASE"/>.</remarks>
            CDRF_NOTIFYPOSTERASE = 64,
            /// <summary>
            /// Il controllo notificherà il padre dopo il disegno di un oggetto.
            /// </summary>
            /// <remarks>Questo valore può essere usato solo il passaggio di disegno in corso è <see cref="DrawingStage.CDDS_PREPAINT"/>.</remarks>
            CDRF_NOTIFYPOSTPAINT = 16,
            /// <summary>
            /// L'applicazione ha disegnato l'oggetto manualmente.
            /// </summary>
            /// <remarks>Il controllo non disegnerà l'oggetto.<br/><br/>
            /// Questo valore può essere usato solo il passaggio di disegno in corso è <see cref="DrawingStage.CDDS_PREPAINT"/> o <see cref="DrawingStage.CDDS_PREERASE"/>.</remarks>
            CDRF_SKIPDEFAULT = 4
        }

        /// <summary>
        /// Tipo di controllo disegnato dal proprietario.
        /// </summary>
        internal enum OwnerDrawnControlType
        {
            /// <summary>
            /// Pulsante.
            /// </summary>
            ODT_BUTTON = 4,
            /// <summary>
            /// ComboBox.
            /// </summary>
            ODT_COMBOBOX = 3,
            /// <summary>
            /// List box.
            /// </summary>
            ODT_LISTBOX = 2,
            /// <summary>
            /// List view.
            /// </summary>
            ODT_LISTVIEW = 102,
            /// <summary>
            /// Menù.
            /// </summary>
            ODT_MENU = 1,
            /// <summary>
            /// Controllo statico.
            /// </summary>
            ODT_STATIC = 5,
            /// <summary>
            /// Scheda.
            /// </summary>
            ODT_TAB = 101,
            /// <summary>
            /// Intestazione.
            /// </summary>
            ODT_HEADER = 100
        }

        /// <summary>
        /// Azione di disegno.
        /// </summary>
        [Flags]
        internal enum DrawingAction
        {
            /// <summary>
            /// Tutto il controllo deve essere disegnato.
            /// </summary>
            ODA_DRAWENTIRE = 1,
            /// <summary>
            /// Lo stato di selezione è cambiato.
            /// </summary>
            ODA_SELECT,
            /// <summary>
            /// Il controllo ha perso o ha ricevuto il focus della tastiera.
            /// </summary>
            ODA_FOCUS = 4
        }

        /// <summary>
        /// Stato visuale dell'oggetto.
        /// </summary>
        [Flags]
        internal enum ItemVisualState
        {
            /// <summary>
            /// L'oggetto del menù deve essere selezionato.
            /// </summary>
            /// <remarks>Questo valore viene usato solo in un menù.</remarks>
            ODS_CHECKED = 8,
            /// <summary>
            /// L'operazione di disegno avviene nel campo di selezione di un combobox disegnato dal proprietario.
            /// </summary>
            ODS_COMBOBOXEDIT = 4096,
            /// <summary>
            /// L'oggetto è quello predefinito.
            /// </summary>
            ODS_DEFAULT = 32,
            /// <summary>
            /// L'oggetto deve essere disegnato come disabilitato.
            /// </summary>
            ODS_DISABLED = 4,
            /// <summary>
            /// L'oggetto ha il focus della tastiera.
            /// </summary>
            ODS_FOCUS = 16,
            /// <summary>
            /// L'oggetto deve essere disattivato.
            /// </summary>
            /// <remarks>Questo valore viene usato solo in un menù.</remarks>
            ODS_GRAYED = 2,
            /// <summary>
            /// L'oggetto verrà evidenziato quando il mouse si trova su di esso.
            /// </summary>
            ODS_HOTLIGHT = 64,
            /// <summary>
            /// L'oggetto è inattivo e la finestra associata al menù è inattiva.
            /// </summary>
            ODS_INACTIVE = 128,
            /// <summary>
            /// Il controllo deve essere disegnato senza gli spunti tastiera.
            /// </summary>
            ODS_NOACCEL = 256,
            /// <summary>
            /// Il controllo deve essere disegnato senza gli spunti indicatori del focus.
            /// </summary>
            ODS_NOFOCUSRECT = 512,
            /// <summary>
            /// L'oggetto del menù è selezionato.
            /// </summary>
            ODS_SELECTED = 1
        }
    }
}