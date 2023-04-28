using System;

namespace WindowsAPI.DesktopWindowManagerWrapper.Native
{
    /// <summary>
    /// Enumerazioni DWM.
    /// </summary>
    internal static class DWMEnumerations
    {
        #region DWM Window Attribute Enumerations
        /// <summary>
        /// Opzioni per l'impostazione o il recupero degli attributi delle finestre DWM.
        /// </summary>
        [Flags]
        internal enum DWMWINDOWATTRIBUTE
        {
            /// <summary>
            /// Recupera lo stato del rendering dell'area non client.
            /// </summary>
            /// <remarks>Il valore recuperato è di tipo BOOL.</remarks>
            DWMWA_NCRENDERING_ENABLED = 1,
            /// <summary>
            /// Imposta i criteri di rendering dell'area non client.
            /// </summary>
            /// <remarks>Deve essere utilizzato uno dei valori di <see cref="DWMNCRENDERINGPOLICY"/>.</remarks>
            DWMWA_NCRENDERING_POLICY,
            /// <summary>
            /// Abilita o disabilita forzatamente le transizioni DWM.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_TRANSITIONS_FORCEDISABLED,
            /// <summary>
            /// Abilita o disabilita la visibilita del contenuto renderizzato nell'area non client sulla cornice disegnata da DWM.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_ALLOW_NCPAINT,
            /// <summary>
            /// Recupera i bordi dell'area dei tasti di controllo nello spazio relativo alla finestra.
            /// </summary>
            /// <remarks>Il valore recuperato è di tipo <see cref="DWMStructures.RECT"/>.<br/><br/>
            /// Se la finestra è minimizzata o comunque non visibile all'utente, i valori nella struttura non sono definiti.</remarks>
            DWMWA_CAPTION_BUTTON_BOUNDS,
            /// <summary>
            /// Attiva o disattiva il mirroring RTL (Right-to-left) del contenuto dell'area non client.
            /// </summary>
            /// <remarks>Il valore di utilizzare è di tipo BOOL.</remarks>
            DWMWA_NONCLIENT_RTL_LAYOUT,
            /// <summary>
            /// Forza la finestra a visualizzare una miniatura o una rappresentazione peek (bitmap statico) anche se uno snapshot della finestra è disponibile.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_FORCE_ICONIC_REPRESENTATION,
            /// <summary>
            /// Imposta il modo in cui Flip3D tratta la finestra.
            /// </summary>
            /// <remarks>Il valore da utilizzare è uno dei valori dell'enumerazione <see cref="DWMFLIP3DWINDOWPOLICY"/>.</remarks>
            DWMWA_FLIP3D_POLICY,
            /// <summary>
            /// Recupera i bordi del rettangolo della cornice estesa.
            /// </summary>
            /// <remarks>Il valore recuperato è una struttura <see cref="RECT"/>.</remarks>
            DWMWA_EXTENDED_FRAME_BOUNDS,
            /// <summary>
            /// Imposta la necessità da parte della finestra di fornire un bitmap che DWM userà come miniatura o rappresentazione peek.
            /// </summary>
            /// <remarks>Questo valore può essere usato insieme a <see cref="DWMA_FORCE_ICONIC_REPRESENTATION"/>.<br/><br/>
            /// Il valore da usare è di tipo BOOL.</remarks>
            DWMWA_HAS_ICONIC_BITMAP,
            /// <summary>
            /// Disattiva l'anteprima peek per la finestra.
            /// </summary>
            /// <remarks>Il valore da usare è di tipo BOOL.</remarks>
            DWMWA_DISALLOW_PEEK,
            /// <summary>
            /// Impedisce a una finestra di trasformarsi in una lastra di vetro quando viene attivato peek.
            /// </summary>
            /// <remarks>Il valore da usare è di tipo BOOL.</remarks>
            DWMWA_EXCLUDED_FROM_PEEK,
            /// <summary>
            /// Nasconde la finestra in modo che non sia visibile all'utente, DWM continua la composizione.
            /// </summary>
            DWMWA_CLOAK,
            /// <summary>
            /// Determina chi ha nascosto la finestra.
            /// </summary>
            /// <remarks>Questo valore restituisce uno dei valori dell'enumerazione <see cref="CloakReason"/>.</remarks>
            DWMWA_CLOAKED,
            /// <summary>
            /// Blocca l'aggiornamento dell'immagine della miniatura.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_FREEZE_REPRESENTATION,
            /// <summary>
            /// Aggiorna la finestra solo quando la composizione del desktop avviene per altre ragioni.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_PASSIVE_UPDATE_MODE,
            /// <summary>
            /// Permette o meno a una finestra non-UWP di usare i pennelli backdrop.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_USE_HOSTBACKDROPBRUSH,
            /// <summary>
            /// Permette alla cornice della finestra di essere disegnata nei colori della modalità scura quando quest'ultima è abilitata.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo BOOL.</remarks>
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            /// <summary>
            /// Specifica le impostazioni per gli angoli arrotondati della finestra.
            /// </summary>
            /// <remarks>Il valore da utilizzare è uno dei valori dell'enumerazione <see cref="DWM_WINDOW_CORNER_PREFERENCE"/>.</remarks>
            DWMWA_WINDOW_CORNER_PREFERENCE = 33,
            /// <summary>
            /// Imposta il colore del bordo della finestra.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo COLORREF (colore RGB nel formato 0x00bbggrr).<br/><br/>
            /// L'applicazione è responsabile del cambio del colore del bordo quando richiesto.</remarks>
            DWMWA_BORDER_COLOR,
            /// <summary>
            /// Specifica il colore della barra del titolo.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo COLORREF (colore RGB nel formato 0x00bbggrr).</remarks>
            DWMWA_CAPTION_COLOR,
            /// <summary>
            /// Specifica il colore del testo della barra del titolo.
            /// </summary>
            /// <remarks>Il valore da utilizzare è di tipo COLORREF (colore RGB nel formato 0x00bbggrr).</remarks>
            DWMWA_TEXT_COLOR,
            /// <summary>
            /// Recupera la larghezza del bordo esterno disegnato da DWM attordo alla finestra.
            /// </summary>
            /// <remarks>Il valore recuperato è di tipo UINT.</remarks>
            DWMWA_VISIBLE_FRAME_BORDER_THICKNESS,
            /// <summary>
            /// Recupera o imposta il materiale backdrop di una finestra disegnato dal sistema anche dietro l'area non client.
            /// </summary>
            /// <remarks>Il valore da utilizzare o recuperato è uno dei valori dell'enumerazione <see cref="DWM_SYSTEMBACKDROP_TYPE"/>.</remarks>
            DWMWA_SYSTEMBACKDROP_TYPE,
            /// <summary>
            /// Valore massimo dell'enumerazione.
            /// </summary>
            DWMWA_LAST
        }

        /// <summary>
        /// Criteri di rendering dell'area non client.
        /// </summary>
        internal enum DWMNCRENDERINGPOLICY
        {
            /// <summary>
            /// Il rendering dell'area non client viene eseguita in base allo stile della finestra.
            /// </summary>
            DWMNCRP_USEWINDOWSTYLE,
            /// <summary>
            /// Il rendering dell'area non client è disattivato, lo stile della finestra viene ignorato.
            /// </summary>
            DWMNCRP_DISABLED,
            /// <summary>
            /// Il rendering dell'area non client è attivato, lo stile della finestra viene ignorato.
            /// </summary>
            DWMNCRP_ENABLED,
            /// <summary>
            /// Valore massimo dell'enumerazione.
            /// </summary>
            DWMNCRP_LAST
        }

        /// <summary>
        /// Specifica i criteri Flip3D di una finestra.
        /// </summary>
        internal enum DWMFLIP3DWINDOWPOLICY
        {
            /// <summary>
            /// Usa gli stili della finestra e le impostazioni di visibilità per determinare se nasconderla o meno nel rendering Flip3D.
            /// </summary>
            DWMFLIP3D_DEFAULT,
            /// <summary>
            /// Esclude la finestra da Flip3D e la visualizza sotto il rendering.
            /// </summary>
            DWMFLIP3D_EXCLUDEBELOW,
            /// <summary>
            /// Esclude la finestra da Flip3D e la visualizza sopra il rendering.
            /// </summary>
            DWMFLIP3D_EXCLUDEABOVE,
            /// <summary>
            /// Valore massimo dell'enumerazione.
            /// </summary>
            DWMFLIP3D_LAST
        }

        /// <summary>
        /// Motivazione per cui una finestra è nascosta.
        /// </summary>
        internal enum CloakReason
        {
            /// <summary>
            /// La finestra è stata nascosta dalla sua applicazione proprietaria.
            /// </summary>
            DWM_CLOAKED_APP = 1,
            /// <summary>
            /// La finestra è stata nascosta dalla Shell.
            /// </summary>
            DWM_CLOAKED_SHELL,
            /// <summary>
            /// Lo stato è stato ereditato dalla sua finestra proprietaria.
            /// </summary>
            DWM_CLOAKED_INHERITED = 4
        }

        /// <summary>
        /// Preferenza per gli angoli arrotondati di una finestra.
        /// </summary>
        internal enum DWM_WINDOW_CORNER_PREFERENCE
        {
            /// <summary>
            /// Lascia decidere al sistema se usare o meno gli angoli arrotondati.
            /// </summary>
            DWMWCP_DEFAULT,
            /// <summary>
            /// Non usare angoli arrotondati.
            /// </summary>
            DWMWCP_DONOTROUND,
            /// <summary>
            /// Arrotonda gli angoli, se appropriato.
            /// </summary>
            DWMCP_ROUND,
            /// <summary>
            /// Arrotonda gli angoli con un raggio piccolo, se appropriato.
            /// </summary>
            DWMWCP_ROUNDSMALL
        }

        /// <summary>
        /// Specifica il materiale del fondale della finestra disegnato dal sistema anche dietro all'area non client.
        /// </summary>
        internal enum DWM_SYSTEMBACKDROP_TYPE
        {
            /// <summary>
            /// Lascia decidere a DWM il materiale.
            /// </summary>
            DWMSBT_AUTO,
            /// <summary>
            /// Non disegnare nessun fondale.
            /// </summary>
            DWMSBT_NONE,
            /// <summary>
            /// Disegna il materiale del fondale di una finestra presente da lungo tempo.
            /// </summary>
            DWMSBT_MAINWINDOW,
            /// <summary>
            /// Disegna il materiale del fondale corrispondente a una finestra transitoria.
            /// </summary>
            DWMSBT_TRANSIENTWINDOW,
            /// <summary>
            /// Disegna il materiale del fondale corrispondente a una finestra con una barra del titolo tabbata.
            /// </summary>
            DWMSBT_TABBEDWINDOW
        }
        #endregion
        /// <summary>
        /// Tipo di gesto.
        /// </summary>
        internal enum GESTURE_TYPE
        {
            /// <summary>
            /// Tocco di una penna.
            /// </summary>
            GT_PEN_TAP,
            /// <summary>
            /// Doppio tocco di una penna.
            /// </summary>
            GT_PEN_DOUBLETAP,
            /// <summary>
            /// Tocco destro di una penna.
            /// </summary>
            GT_PEN_RIGHTTAP,
            /// <summary>
            /// Tieni premuto (penna).
            /// </summary>
            GT_PEN_PRESSANDHOLD,
            /// <summary>
            /// Annullamento del gesto "Tieni premuto (penna)".
            /// </summary>
            GT_PEN_PRESSANDHOLDABORT,
            /// <summary>
            /// Tocco.
            /// </summary>
            GT_TOUCH_TAP,
            /// <summary>
            /// Doppio tocco.
            /// </summary>
            GT_TOUCH_DOUBLETAP,
            /// <summary>
            /// Tocco destro.
            /// </summary>
            GT_TOUCH_RIGHTTAP,
            /// <summary>
            /// Tieni premuto (tocco).
            /// </summary>
            GT_TOUCH_PRESSANDHOLD,
            /// <summary>
            /// Annullamento del gesto "Tieni premuto (tocco)".
            /// </summary>
            GT_TOUCH_PRESSANDHOLDABORT,
            /// <summary>
            /// Premi e tocca.
            /// </summary>
            GT_TOUCH_PRESSANDTAP
        }

        /// <summary>
        /// Tipo di contatto da mostrare.
        /// </summary>
        [Flags]
        internal enum DWM_SHOWCONTACT : uint
        {

            DWMSC_DOWN = 1,

            DWMSC_UP = 2,

            DWMSC_DRAG = 4,

            DWMSC_HOLD = 8,

            DWMSC_PENBARREL = 16,

            DWMSC_NONE = 0,

            DWMSC_ALL = uint.MaxValue
        }

        /// <summary>
        /// Identifica l'obbiettivo.
        /// </summary>
        internal enum DWMTRANSITION_OWNEDWINDOW_TARGET
        {
            /// <summary>
            /// Nessuna animazione.
            /// </summary>
            DWMTRANSITION_OWNEDWINDOW_NULL = -1,
            /// <summary>
            /// La finestra viene riposizionata.
            /// </summary>
            DWMTRANSITION_OWNEDWINDOW_REPOSITION
        }

        /// <summary>
        /// Indica quali membri della struttura <see cref="DWMStructures.DWM_THUMBNAIL_PROPERTIES"/> sono validi.
        /// </summary>
        [Flags]
        internal enum DWM_TNP
        {
            /// <summary>
            /// <see cref="DWMStructures.DWM_THUMBNAIL_PROPERTIES.DestinationRectangle"/> è valido.
            /// </summary>
            DWM_TNP_RECTDESTINATION = 1,
            /// <summary>
            /// <see cref="DWMStructures.DWM_THUMBNAIL_PROPERTIES.SourceRectangle"/> è valido.
            /// </summary>
            DWM_TNP_RECTSOURCE,
            /// <summary>
            /// <see cref="DWMStructures.DWM_THUMBNAIL_PROPERTIES.Opacity"/> è valido.
            /// </summary>
            DWM_TNP_OPACITY = 4,
            /// <summary>
            /// <see cref="DWMStructures.DWM_THUMBNAIL_PROPERTIES.Visible"/> è valido.
            /// </summary>
            DWM_TNP_VISIBLE = 8,
            /// <summary>
            /// <see cref="DWMStructures.DWM_THUMBNAIL_PROPERTIES.OnlyClientArea"/> è valido.
            /// </summary>
            DWM_TNP_SOURCECLIENTAREAONLY = 16
        }

        /// <summary>
        /// Valori restituiti per il messaggio <see cref="DWMMessages.WM_NCHITTEST"/>.
        /// </summary>
        internal enum HitTestReturnValue
        {
            /// <summary>
            /// Sul bordo di una finestra che non ha un bordo di ridimensionamento.
            /// </summary>
            HTBORDER = 18,
            /// <summary>
            /// Sul bordo inferiore orizzontale di una finestra ridimensionabile.
            /// </summary>
            HTBOTTOM = 15,
            /// <summary>
            /// Nell'angolo inferiore sinistro di un bordo di una finestra ridimensionabile.
            /// </summary>
            HTBOTTOMLEFT,
            /// <summary>
            /// Nell'angolo inferiore destro di un bordo di una finestra ridimensionabile.
            /// </summary>
            HTBOTTOMRIGHT,
            /// <summary>
            /// Sulla barra del titolo.
            /// </summary>
            HTCAPTION = 2,
            /// <summary>
            /// Nell'area client.
            /// </summary>
            HTCLIENT = 1,
            /// <summary>
            /// Sul pulsante Chiudi.
            /// </summary>
            HTCLOSE = 20,
            /// <summary>
            /// Sullo sfondo dello schermo o su una linea di divisione tra finestre.
            /// </summary>
            HTERROR = -2,
            /// <summary>
            /// In un size box.
            /// </summary>
            HTGROWBOX = 4,
            /// <summary>
            /// Su un pulsante Guida.
            /// </summary>
            HTHELP = 21,
            /// <summary>
            /// Su una barra di scorrimento orizzontale.
            /// </summary>
            HTHSCROLL = 6,
            /// <summary>
            /// Sul bordo sinistro di una finestra ridimensionabile.
            /// </summary>
            HTLEFT = 10,
            /// <summary>
            /// Su un menù.
            /// </summary>
            HTMENU = 5,
            /// <summary>
            /// Sul pulsante Ingrandisci.
            /// </summary>
            HTMAXBUTTON = 9,
            /// <summary>
            /// Sul pulsante Riduci a icona.
            /// </summary>
            HTMINBUTTON = 8,
            /// <summary>
            /// Sullo sfondo dello schermo o su una linea di divisione tra finestre.
            /// </summary>
            HTNOWHERE = 0,
            /// <summary>
            /// Sul pulsante Riduci a icona.
            /// </summary>
            HTREDUCE = 8,
            /// <summary>
            /// Sul bordo destro di una finestra ridimensionabile.
            /// </summary>
            HTRIGHT = 11,
            /// <summary>
            /// In un size box.
            /// </summary>
            HTSIZE = 4,
            /// <summary>
            /// Nel menù di sistema di una finestra o sul pulsante Chiudi in una finestra figlia.
            /// </summary>
            HTSYSMENU = 3,
            /// <summary>
            /// Sul bordo superiore orizzontale di una finestra.
            /// </summary>
            HTTOP = 12,
            /// <summary>
            /// Sull'angolo superiore sinistro del bordo di una finestra.
            /// </summary>
            HTTOPLEFT = 13,
            /// <summary>
            /// Sull'angolo superiore destro del bordo di una finestra.
            /// </summary>
            HTTOPRIGHT = 14,
            /// <summary>
            /// Su una finestra attualmente coperta da un'altra finestra nello stesso thread.
            /// </summary>
            /// <remarks>Il messaggio continuerà ad essere inviato alle finestre sotto di essa fino a quando una non restituisce un codice diverso.</remarks>
            HTTRANSPARENT = -1,
            /// <summary>
            /// Su una barra di scorrimento verticale.
            /// </summary>
            HTVSCROLL = 7,
            /// <summary>
            /// Sul pulsante Ingrandisci.
            /// </summary>
            HTZOOM = 9
        }
    }
}