using System;
using System.Runtime.InteropServices;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonEnumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonConstants;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonNotifications;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls.CommonControlsEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons
{
    /// <summary>
    /// Strutture relative ai pulsanti.
    /// </summary>
    internal static class ButtonStructures
    {
        /// <summary>
        /// Informazioni su una lista di immagini usata con un pulsante.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BUTTON_IMAGELIST
        {
            /// <summary>
            /// Handle alla lista di immagini.
            /// </summary>
            /// <remarks>Il fornitore mantiene la proprietà della lista di immagini ed è responsabile per la sua eliminazione.<br/>
            /// Questo campo può essere impostato a <see cref="BCCL_NOGLYPH"/> per indicare di non visulizzare nessun glifo.</remarks>
            public IntPtr ImagelistHandle;
            /// <summary>
            /// Margine attorno all'icona.
            /// </summary>
            public RECT Margin;
            /// <summary>
            /// Allineamento dell'immagine.
            /// </summary>
            public ImagelistAlignment Alignment;
        }

        /// <summary>
        /// Informazioni su uno split button.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct BUTTON_SPLITINFO
        {
            /// <summary>
            /// Membri validi della struttura.
            /// </summary>
            public SplitInfoMembers ValidMembers;
            /// <summary>
            /// Handle alla lista di immmagini.
            /// </summary>
            public IntPtr ImagelistHandle;
            /// <summary>
            /// Stile dello split button.
            /// </summary>
            public SplitStyle SplitStyle;
            /// <summary>
            /// Dimensione del glifo.
            /// </summary>
            public SIZE GlyphSize;
        }

        /// <summary>
        /// Informazioni sul movimento del mouse su un pulsante.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMBCHOTITEM
        {
            /// <summary>
            /// Struttura <see cref="NMHDR"/> con le informazioni sulla notifica.
            /// </summary>
            public NMHDR NotificationData;
            /// <summary>
            /// Azione del mouse.
            /// </summary>
            /// <remarks>Questo campo può avere solo uno dei seguenti valori combinato con <see cref="HotItemFlags.HICF_MOUSE"/>:<br/><br/>
            /// <see cref="HotItemFlags.HICF_ENTERING"/><br/>
            /// <see cref="HotItemFlags.HICF_LEAVING"/></remarks>
            public HotItemFlags MouseAction;
        }

        /// <summary>
        /// Informazioni su una notifica <see cref="BCN_DROPDOWN"/>.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct NMBCDROPDOWN
        {
            /// <summary>
            /// Informazioni sulla notifica.
            /// </summary>
            public NMHDR NotificationData;
            /// <summary>
            /// Area client del pulsante.
            /// </summary>
            public RECT ButtonClientArea;
        }
    }
}