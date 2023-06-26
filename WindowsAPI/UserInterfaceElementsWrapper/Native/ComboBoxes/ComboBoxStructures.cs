using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes
{
    /// <summary>
    /// Strutture relative ai Combobox.
    /// </summary>
    internal static class ComboBoxStructures
    {
        /// <summary>
        /// Informazioni sullo stato di un Combobox.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct COMBOBOXINFO
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Coordinate del controllo di modifica.
            /// </summary>
            public RECT EditBoxCoordinates;
            /// <summary>
            /// Coordinate della freccia del dropdown.
            /// </summary>
            public RECT DropdownArrowCoordinates;
            /// <summary>
            /// Stato del pulsante del Combobox.
            /// </summary>
            public ComboBoxButtonState State;
            /// <summary>
            /// Handle al Combobox.
            /// </summary>
            public IntPtr ComboboxHandle;
            /// <summary>
            /// Handle al controllo di modifica.
            /// </summary>
            public IntPtr EditBoxHandle;
            /// <summary>
            /// Handle alla lista del dropdown.
            /// </summary>
            public IntPtr DropdownListHandle;
        }
    }
}