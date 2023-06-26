using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Buttons.DataClasses
{
    /// <summary>
    /// Informazioni sullo stato di un pulsante.
    /// </summary>
    public class ButtonStateInfo
    {
        /// <summary>
        /// Indica se il pulsante è selezionato.
        /// </summary>
        public bool IsChecked { get; }

        /// <summary>
        /// Indica se il dropdown è visibile.
        /// </summary>
        public bool IsDropdownVisible { get; }

        /// <summary>
        /// Indica se il pulsante ha il focus della tastiera.
        /// </summary>
        public bool HasKeyboardFocus { get; }

        /// <summary>
        /// Indica se il puntatore del mouse si trova sopra il pulsante.
        /// </summary>
        public bool IsHot { get; }

        /// <summary>
        /// Indica se lo stato del pulsante non è determinato.
        /// </summary>
        public bool IsIndeterminate { get; }

        /// <summary>
        /// Indica se il pulsante è premuto.
        /// </summary>
        public bool IsPushed { get; }

        /// <summary>
        /// Inizializza un'istanza di <see cref="ButtonStateInfo"/>.
        /// </summary>
        /// <param name="State">Valore composito che indica lo stato del pulsante.</param>
        internal ButtonStateInfo(ButtonState State)
        {
            IsChecked = State.HasFlag(ButtonState.BST_CHECKED);
            IsDropdownVisible = State.HasFlag(ButtonState.BST_DROPDOWNPUSHED);
            HasKeyboardFocus = State.HasFlag(ButtonState.BST_FOCUS);
            IsIndeterminate = State.HasFlag(ButtonState.BST_INDETERMINATE);
            IsPushed = State.HasFlag(ButtonState.BST_PUSHED);
        }
    }
}