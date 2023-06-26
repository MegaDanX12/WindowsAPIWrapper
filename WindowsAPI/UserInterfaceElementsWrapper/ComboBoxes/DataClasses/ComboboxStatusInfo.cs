using System;
using System.Drawing;
using WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.ComboBoxes.DataClasses
{
    /// <summary>
    /// Informazioni di stato di un Combobox.
    /// </summary>
    public class ComboboxStatusInfo
    {
        /// <summary>
        /// Coordinate del controllo di modifica.
        /// </summary>
        public Rectangle EditBoxCoordinates { get; }

        /// <summary>
        /// Coordinate del pulsante di dropdown.
        /// </summary>
        public Rectangle DropdownButtonCoordinates { get; }

        /// <summary>
        /// Indica se il pulsante del ComboBox è visibile.
        /// </summary>
        public bool Exists { get; }

        /// <summary>
        /// Indica se il pulsante del ComboBox è premuto.
        /// </summary>
        public bool IsPressed { get; }

        /// <summary>
        /// Handle al ComboBox.
        /// </summary>
        public IntPtr ControlHandle { get; }

        /// <summary>
        /// Handle al controllo di modifica.
        /// </summary>
        public IntPtr EditBoxHandle { get; }

        /// <summary>
        /// Handle al dropdown.
        /// </summary>
        public IntPtr DropdownListHandle { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="ComboboxStatusInfo"/>.
        /// </summary>
        /// <param name="Info">Struttura <see cref="COMBOBOXINFO"/> con i dati.</param>
        internal ComboboxStatusInfo(COMBOBOXINFO Info)
        {
            EditBoxCoordinates = Rectangle.FromLTRB(Info.EditBoxCoordinates.Left, Info.EditBoxCoordinates.Top, Info.EditBoxCoordinates.Right, Info.EditBoxCoordinates.Bottom);
            DropdownButtonCoordinates = Rectangle.FromLTRB(Info.DropdownArrowCoordinates.Left, Info.DropdownArrowCoordinates.Top, Info.DropdownArrowCoordinates.Right, Info.DropdownArrowCoordinates.Bottom);
            Exists = !Info.State.HasFlag(ComboBoxEnumerations.ComboBoxButtonState.STATE_SYSTEM_INVISIBLE);
            IsPressed = Info.State.HasFlag(ComboBoxEnumerations.ComboBoxButtonState.STATE_SYSTEM_PRESSED);
            ControlHandle = Info.ComboboxHandle;
            EditBoxHandle = Info.EditBoxHandle;
            DropdownListHandle = Info.DropdownListHandle;
        }
    }
}