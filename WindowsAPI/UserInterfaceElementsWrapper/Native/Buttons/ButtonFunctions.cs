using System;
using System.Runtime.InteropServices;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons
{
    /// <summary>
    /// Funzioni relative ai pulsanti.
    /// </summary>
    internal static class ButtonFunctions
    {
        /// <summary>
        /// Cambia lo stato di selezione di un pulsante.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il pulsante.</param>
        /// <param name="ButtonID">Identificatore del pulsante.</param>
        /// <param name="CheckState">Nuovo stato del pulsante.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>I seguenti valori dell'enumerazione <see cref="ButtonState"/> non sono validi per <paramref name="CheckState"/>:<br/><br/>
        /// <see cref="ButtonState.BST_PUSHED"/><br/>
        /// <see cref="ButtonState.BST_FOCUS"/></remarks>
        [DllImport("User32.dll", EntryPoint = "CheckDlgButton", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CheckDialogButton(IntPtr DialogHandle, int ButtonID, ButtonState CheckState);

        /// <summary>
        /// Aggiunge una spunta a un radio button in un gruppo e rimuove la spunta dagli altri pulsanti nello stesso gruppo.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il pulsante.</param>
        /// <param name="FirstButtonID">ID del primo pulsante nel gruppo.</param>
        /// <param name="LastButtonID">ID dell'ultimo pulsante nel gruppo.</param>
        /// <param name="RadioButtonID">ID del pulsante da selezionare.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "CheckRadioButton", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CheckRadioButton(IntPtr DialogHandle, int FirstButtonID, int LastButtonID, int RadioButtonID);

        /// <summary>
        /// Determina se un pulsante è selezionato o se un pulsante a tre stati è selezionato, non selezionato oppure si trova in uno stato indeterminato.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo che contiene il pulsante.</param>
        /// <param name="ButtonID">ID del pulsante.</param>
        /// <returns></returns>
        [DllImport("User32.dll", EntryPoint = "ISDlgButtonChecked", SetLastError = true)]
        internal static extern ButtonState IsDialogButtonChecked(IntPtr DialogHandle, int ButtonID);
    }
}