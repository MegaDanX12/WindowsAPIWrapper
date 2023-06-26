using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsEnumerations;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls
{
    /// <summary>
    /// Strutture comuni per i controlli Windows.
    /// </summary>
    internal static class CommonControlsStructures
    {
        /// <summary>
        /// Informazioni su un messaggi di notifica.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMHDR
        {
            /// <summary>
            /// Handle al controllo che sta inviando il messaggio.
            /// </summary>
            public IntPtr ControlHandle;
            /// <summary>
            /// ID del controllo che sta inviando il messaggio.
            /// </summary>
            public uint SenderControlID;
            /// <summary>
            /// Codice di notifica.
            /// </summary>
            public uint NotificationCode;
        }

        /// <summary>
        /// Informazioni su uno specifico codice di notifica NM_CUSTOMDRAW.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMCUSTOMDRAWINFO
        {
            /// <summary>
            /// Informazioni sul codice di notifica.
            /// </summary>
            public NMHDR NotificationData;
            /// <summary>
            /// Passaggio di disegno.
            /// </summary>
            public DrawingStage DrawingStage;
            /// <summary>
            /// Handle al contesto dispositivo.
            /// </summary>
            public IntPtr DeviceContextHandle;
            /// <summary>
            /// Rettangolo di delimitazione dell'area in corso di disegno.
            /// </summary>
            /// <remarks>Questo campo viene inizializzato dalle notifiche <see cref="DrawingStage.CDDS_ITEMPREPAINT"/> e <see cref="DrawingStage.CDDS_PREPAINT"/>.</remarks>
            public RECT BoundingRectangle;
            /// <summary>
            /// Numero dell'oggetto.
            /// </summary>
            /// <remarks>Il valore di questo campo dipende dal tipo di controllo che sta inviando la notifica.</remarks>
            public IntPtr ItemNumber;
            /// <summary>
            /// Stato dell'oggetto.
            /// </summary>
            public ItemState ItemState;
            /// <summary>
            /// Dati definiti dall'applicazione.
            /// </summary>
            public IntPtr AppData;
        }

        /// <summary>
        /// Informazioni usate dalla finestra proprietaria di un controllo o di un oggetto di un menù disegnato dal proprietario per determinare come disegnarlo.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct DRAWITEMSTRUCT
        {
            /// <summary>
            /// Tipo di controllo.
            /// </summary>
            /// <remarks>Alcuni controlli non impostano questo campo.</remarks>
            public OwnerDrawnControlType ControlType;
            /// <summary>
            /// Identificatore del controllo.
            /// </summary>
            /// <remarks>Questo campo non viene usato per un oggetto di un menù.</remarks>
            public uint ControlID;
            /// <summary>
            /// Identificatore dell'oggetto di menù oppure l'indice dell'oggetto in un list box o in un combobox.
            /// </summary>
            /// <remarks>Se il list box o combobox è vuoto, questo campo può essere impostato a -1.<br/>
            /// Questo permette all'applicazione di disegnare il rettangolo di focus alle coordinate specificate da <see cref="ControlBoundaries"/> nonostante non ci siano oggetti nel controllo.<br/>
            /// Questo indica all'utente se il list box o il combobox ha il focus.<br/>
            /// Il valore di <see cref="DrawingAction"/> determina se il rettangolo deve essere disegnato come se il list box oppure il combobox ha il focus.</remarks>
            public uint ItemID;
            /// <summary>
            /// Azione di disegno richiesta.
            /// </summary>
            public DrawingAction DrawingAction;
            /// <summary>
            /// Lo stato visivo del'oggetto dopo che l'attuale azione di disegno sarà completata.
            /// </summary>
            public ItemVisualState ItemState;
            /// <summary>
            /// Handle al controllo o al menù che contiene l'oggetto.
            /// </summary>
            public IntPtr ControlHandle;
            /// <summary>
            /// Handle al contesto dispositivo.
            /// </summary>
            public IntPtr DeviceContextHandle;
            /// <summary>
            /// Rettangolo che definisce i limiti del controllo da disegnare.
            /// </summary>
            /// <remarks>Il sistema taglia ogni cosa disegnata dalla finestra proprietaria nel contesto dispositivo per i comboboxes, list boxes e pulsanti ma non per gli oggetti dei menù.<br/>
            /// Quando disegna gli oggetti di menù, la finestra proprietaria non deve disegnare al di fuori dei limiti del rettangolo.</remarks>
            public RECT ControlBoundaries;
            /// <summary>
            /// Valore fornito dall'applicazione associato con l'oggetto di menù.
            /// </summary>
            /// <remarks>Se <see cref="ControlType"/> ha valore <see cref="OwnerDrawnControlType.ODT_BUTTON"/> oppure <see cref="OwnerDrawnControlType.ODT_STATIC"/>, questo campo è impostato a <see cref="IntPtr.Zero"/>.</remarks>
            public IntPtr AppData;
        }

        /// <summary>
        /// Fornisce gli identificatori e i dati associati ad essi per due elementi in un ComboBox o Listbox ordinato disegnato dal proprietario.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct COMPAREITEMSTRUCT
        {
            /// <summary>
            /// Tipo di controllo.
            /// </summary>
            public OwnerDrawnControlType ControlType;
            /// <summary>
            /// Identificatore del controllo.
            /// </summary>
            public uint ControlID;
            /// <summary>
            /// Handle al controllo.
            /// </summary>
            public IntPtr ItemHandle;
            /// <summary>
            /// Indice del primo elemento da confrontare.
            /// </summary>
            /// <remarks>Questo campo è impostato a -1 se l'oggetto non è stato inserito o è in corso una ricerca per un eventuale oggetto che sarà inserito.</remarks>
            public uint ItemIndex1;
            /// <summary>
            /// Dati forniti dall'applicazione relativi al primo elemento.
            /// </summary>
            public IntPtr ItemData1;
            /// <summary>
            /// Indice del secondo elemento da confrontare.
            /// </summary>
            public uint ItemIndex2;
            /// <summary>
            /// Dati forniti dall'applicazione relativi al secondo elemento.
            /// </summary>
            /// <remarks>Questo campo è impostato a -1 se l'oggetto non è stato inserito o è in corso una ricerca per un eventuale oggetto che sarà inserito.</remarks>
            public IntPtr ItemData2;
            /// <summary>
            /// Identificatore località.
            /// </summary>
            public uint LocaleID;
        }

        /// <summary>
        /// Informa il sistema delle dimensione di un oggetto di menù o di un controllo disegnato dal proprietario.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MEASUREITEMSTRUCT
        {
            /// <summary>
            /// Tipo di controllo.
            /// </summary>
            public OwnerDrawnControlType ControlType;
            /// <summary>
            /// Identificatore del Listbox o Combobox.
            /// </summary>
            /// <remarks>Questo campo non è utilizzato per i menù.</remarks>
            public uint ControlID;
            /// <summary>
            /// Identificatore dell'oggetto di menù o posizione dell'oggetto in un Listbox o Combobox.
            /// </summary>
            public uint ItemID;
            /// <summary>
            /// Larghezza, in pixel. di un'oggetto di menù.
            /// </summary>
            /// <remarks>Se il controllo è disegnato dal proprietario, questo campo deve essere impostato da quest'ultimo.</remarks>
            public uint ItemWidth;
            /// <summary>
            /// Altezza, in pixel, dell'oggetto di menù o dell'oggetto specifico in un Listbox.
            /// </summary>
            /// <remarks>Se il controllo è disegnato dal proprietario, questo campo deve essere impostato da quest'ultimo.</remarks>
            public uint ItemHeight;
            /// <summary>
            /// Valore definito dall'applicazione associato all'oggetto di menù.
            /// </summary>
            public IntPtr ItemData;
        }
    }
}