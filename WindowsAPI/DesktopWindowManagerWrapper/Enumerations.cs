using WindowsAPI.DesktopWindowManagerWrapper.Native;
using System;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMEnumerations;

namespace WindowsAPI.DesktopWindowManagerWrapper
{
    /// <summary>
    /// Enumerazioni DWM.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Valori restituiti da un hit test.
        /// </summary>
        public enum HitTestReturn
        {
            /// <summary>
            /// Sul bordo di una finestra che non ha un bordo di ridimensionamento.
            /// </summary>
            WindowBorderNoSizing = HitTestReturnValue.HTBORDER,
            /// <summary>
            /// Sul bordo inferiore orizzontale di una finestra ridimensionabile.
            /// </summary>
            BottomBorder = HitTestReturnValue.HTBOTTOM,
            /// <summary>
            /// Nell'angolo inferiore sinistro di un bordo di una finestra ridimensionabile.
            /// </summary>
            BorderBottomLeftCorner = HitTestReturnValue.HTBOTTOMLEFT,
            /// <summary>
            /// Nell'angolo inferiore destro di un bordo di una finestra ridimensionabile.
            /// </summary>
            BorderBottomRightCorner = HitTestReturnValue.HTBOTTOMRIGHT,
            /// <summary>
            /// Sulla barra del titolo.
            /// </summary>
            TitleBar = HitTestReturnValue.HTCAPTION,
            /// <summary>
            /// Nell'area client.
            /// </summary>
            ClientArea = HitTestReturnValue.HTCLIENT,
            /// <summary>
            /// Sul pulsante Chiudi.
            /// </summary>
            CloseButton = HitTestReturnValue.HTCLOSE,
            /// <summary>
            /// Sullo sfondo dello schermo o su una linea di divisione tra finestre.
            /// </summary>
            OutsideOfWindow = HitTestReturnValue.HTERROR,
            /// <summary>
            /// In un size box.
            /// </summary>
            SizeBox = HitTestReturnValue.HTGROWBOX,
            /// <summary>
            /// Su un pulsante Guida.
            /// </summary>
            HelpButton = HitTestReturnValue.HTHELP,
            /// <summary>
            /// Su una barra di scorrimento orizzontale.
            /// </summary>
            HorizontalScrollBar = HitTestReturnValue.HTHSCROLL,
            /// <summary>
            /// Sul bordo sinistro di una finestra ridimensionabile.
            /// </summary>
            LeftBorder = HitTestReturnValue.HTLEFT,
            /// <summary>
            /// Su un menù.
            /// </summary>
            Menu = HitTestReturnValue.HTMENU,
            /// <summary>
            /// Sul pulsante Ingrandisci.
            /// </summary>
            MaximizeButton = HitTestReturnValue.HTMAXBUTTON,
            /// <summary>
            /// Sul pulsante Riduci a icona.
            /// </summary>
            MinimizeButton = HitTestReturnValue.HTMINBUTTON,
            /// <summary>
            /// Sullo sfondo dello schermo o su una linea di divisione tra finestre.
            /// </summary>
            OutsideOfWindow2 = HitTestReturnValue.HTNOWHERE,
            /// <summary>
            /// Sul pulsante Riduci a icona.
            /// </summary>
            MinimizeButton2 = HitTestReturnValue.HTREDUCE,
            /// <summary>
            /// Sul bordo destro di una finestra ridimensionabile.
            /// </summary>
            RightBorder = HitTestReturnValue.HTRIGHT,
            /// <summary>
            /// In un size box.
            /// </summary>
            SizeBox2 = HitTestReturnValue.HTSIZE,
            /// <summary>
            /// Nel menù di sistema di una finestra o sul pulsante Chiudi in una finestra figlia.
            /// </summary>
            WindowSystemMenu = HitTestReturnValue.HTSYSMENU,
            /// <summary>
            /// Sul bordo superiore orizzontale di una finestra.
            /// </summary>
            TopBorder = HitTestReturnValue.HTTOP,
            /// <summary>
            /// Sull'angolo superiore sinistro del bordo di una finestra.
            /// </summary>
            BorderTopLeftCorner = HitTestReturnValue.HTTOPLEFT,
            /// <summary>
            /// Sull'angolo superiore destro del bordo di una finestra.
            /// </summary>
            BorderTopRightCorner = HitTestReturnValue.HTTOPRIGHT,
            /// <summary>
            /// Su una finestra attualmente coperta da un'altra finestra nello stesso thread.
            /// </summary>
            SameThreadCoveredWindow = HitTestReturnValue.HTTRANSPARENT,
            /// <summary>
            /// Su una barra di scorrimento verticale.
            /// </summary>
            VerticalScrollBar = HitTestReturnValue.HTVSCROLL,
            /// <summary>
            /// Sul pulsante Ingrandisci.
            /// </summary>
            MaximizeButton2 = HitTestReturnValue.HTZOOM
        }

        /// <summary>
        /// Pulsante X coinvolto in un evento del mouse.
        /// </summary>
        public enum XButton
        {
            /// <summary>
            /// Primo pulsante X.
            /// </summary>
            FirstButton = 1,
            /// <summary>
            /// Secondo pulsante X.
            /// </summary>
            SecondButton
        }

        /// <summary>
        /// Motivo per cui una finestra è nascosta.
        /// </summary>
        public enum CloakReason
        {
            /// <summary>
            /// La finestra è stata nascosta dalla sua applicazione proprietaria.
            /// </summary>
            OwnerApp = DWMEnumerations.CloakReason.DWM_CLOAKED_APP,
            /// <summary>
            /// La finestra è stata nascosta dalla shell.
            /// </summary>
            Shell = DWMEnumerations.CloakReason.DWM_CLOAKED_SHELL,
            /// <summary>
            /// Lo stato della finestra è stato ereditato dalla sua finestra proprietaria.
            /// </summary>
            Inherited = DWMEnumerations.CloakReason.DWM_CLOAKED_INHERITED
        }

        /// <summary>
        /// Materiale del fondale di una finestra.
        /// </summary>
        public enum SystemBackdropType
        {
            /// <summary>
            /// Deciso da DWM.
            /// </summary>
            LetDWMDecide = DWM_SYSTEMBACKDROP_TYPE.DWMSBT_AUTO,
            /// <summary>
            /// Nessuno.
            /// </summary>
            None = DWM_SYSTEMBACKDROP_TYPE.DWMSBT_NONE,
            /// <summary>
            /// Materiale del fondale di una finestra presente da lungo tempo.
            /// </summary>
            LongLivedWindow = DWM_SYSTEMBACKDROP_TYPE.DWMSBT_MAINWINDOW,
            /// <summary>
            /// Materiale del fondale di una finestra transitoria.
            /// </summary>
            TransientWindow = DWM_SYSTEMBACKDROP_TYPE.DWMSBT_TRANSIENTWINDOW,
            /// <summary>
            /// Materiale del fondale di una finestra con una barra del titolo tabbata.
            /// </summary>
            TabbedTitleBarWindow = DWM_SYSTEMBACKDROP_TYPE.DWMSBT_TABBEDWINDOW
        }

        /// <summary>
        /// Criteri di rendering dell'area non client.
        /// </summary>
        public enum NonClientRenderingPolicy
        {
            /// <summary>
            /// Use lo stile della finestra.
            /// </summary>
            UseWindowStyle = DWMNCRENDERINGPOLICY.DWMNCRP_USEWINDOWSTYLE,
            /// <summary>
            /// Il rendering è disabilitato.
            /// </summary>
            Disabled = DWMNCRENDERINGPOLICY.DWMNCRP_DISABLED,
            /// <summary>
            /// Il rendering è abilitato.
            /// </summary>
            Enabled = DWMNCRENDERINGPOLICY.DWMNCRP_ENABLED
        }

        /// <summary>
        /// Criteri Flip3D per una finestra.
        /// </summary>
        public enum Flip3DWindowPolicy
        {
            /// <summary>
            /// Usa lo stile della finestra e le impostazioni di visibilità.
            /// </summary>
            UseWindowSettings = DWMFLIP3DWINDOWPOLICY.DWMFLIP3D_DEFAULT,
            /// <summary>
            /// Escludi la finestra da Flip3D e visualizzala sotto il rendering.
            /// </summary>
            ExcludeDisplayBelow = DWMFLIP3DWINDOWPOLICY.DWMFLIP3D_EXCLUDEBELOW,
            /// <summary>
            /// Escludi la finestra da Flip3D e visualizzala sopra il rendering.
            /// </summary>
            ExcludeDisplayAbove = DWMFLIP3DWINDOWPOLICY.DWMFLIP3D_EXCLUDEABOVE
        }

        /// <summary>
        /// Preferenza per gli angoli arrotondati di una finestra.
        /// </summary>
        public enum WindowCornerPreference
        {
            /// <summary>
            /// Lascia decidere al sistema.
            /// </summary>
            LetSystemDecide = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DEFAULT,
            /// <summary>
            /// Non arrotondare mai gli angoli.
            /// </summary>
            NeverRound = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_DONOTROUND,
            /// <summary>
            /// Arrotonda gli angoli, se appropriato.
            /// </summary>
            RoundIfAppropriate = DWM_WINDOW_CORNER_PREFERENCE.DWMCP_ROUND,
            /// <summary>
            /// Arrotonda gli angoli con un raggio piccolo, se appropriato.
            /// </summary>
            RoundIfAppropriateSmall = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUNDSMALL
        }

        /// <summary>
        /// Tipo di gesto.
        /// </summary>
        public enum GestureType
        {
            /// <summary>
            /// Tocco di una penna.
            /// </summary>
            PenTap = GESTURE_TYPE.GT_PEN_TAP,
            /// <summary>
            /// Doppio tocco di una penna.
            /// </summary>
            PenDoubleTap = GESTURE_TYPE.GT_PEN_DOUBLETAP,
            /// <summary>
            /// Tocco destro di una penna.
            /// </summary>
            PenRightTap = GESTURE_TYPE.GT_PEN_RIGHTTAP,
            /// <summary>
            /// Tieni premuto (penna).
            /// </summary>
            PenPressAndHold = GESTURE_TYPE.GT_PEN_PRESSANDHOLD,
            /// <summary>
            /// Annullamento del gesto "Tieni premuto (penna)".
            /// </summary>
            PenAbortPressAndHold = GESTURE_TYPE.GT_PEN_PRESSANDHOLDABORT,
            /// <summary>
            /// Tocco.
            /// </summary>
            TouchTap = GESTURE_TYPE.GT_TOUCH_TAP,
            /// <summary>
            /// Doppio tocco.
            /// </summary>
            TouchDoubleTap = GESTURE_TYPE.GT_TOUCH_DOUBLETAP,
            /// <summary>
            /// Tocco destro.
            /// </summary>
            TouchRightTab = GESTURE_TYPE.GT_TOUCH_RIGHTTAP,
            /// <summary>
            /// Tieni premuto (tocco).
            /// </summary>
            TouchPressAndHold = GESTURE_TYPE.GT_TOUCH_PRESSANDHOLD,
            /// <summary>
            /// Annullamento del gesto "Tieni premuto (tocco)".
            /// </summary>
            TouchAbortPressAndHold = GESTURE_TYPE.GT_TOUCH_PRESSANDHOLDABORT,
            /// <summary>
            /// Premi e tocca.
            /// </summary>
            TouchPressAndTap = GESTURE_TYPE.GT_TOUCH_PRESSANDTAP
        }

        /// <summary>
        /// Tipo di contatto.
        /// </summary>
        [Flags]
        public enum ContactType : uint
        {
            Down = DWM_SHOWCONTACT.DWMSC_DOWN,

            Up = DWM_SHOWCONTACT.DWMSC_UP,

            Drag = DWM_SHOWCONTACT.DWMSC_DRAG,

            Hold = DWM_SHOWCONTACT.DWMSC_HOLD,

            PenBarrel = DWM_SHOWCONTACT.DWMSC_PENBARREL,

            None = DWM_SHOWCONTACT.DWMSC_NONE,

            All = DWM_SHOWCONTACT.DWMSC_ALL
        }
    }
}