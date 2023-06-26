using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs.CommonDialogStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs
{
    /// <summary>
    /// Funzioni relative alle finestre di dialogo comuni.
    /// </summary>
    internal static class CommonDialogFunctions
    {
        /// <summary>
        /// Visualizza la finestra di dialogo Colori
        /// </summary>
        /// <param name="InitializazionData">Struttura <see cref="CHOOSECOLOR"/> che contiene i dati per inizializzare la finestra di dialogo.</param>
        /// <returns>true se l'utente ha premuto il pulsante OK, false altrimenti.</returns>
        /// <remarks>Se l'utente preme il pulsante OK, <see cref="CHOOSECOLOR.SelectedColor"/> contiene il colore selezionato dall'utente.<br/><br/>
        /// Se l'utente chiude la finestra di dialogo o in caso di errore, può essere impostato uno dei seguenti codici di errore:<br/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_DIALOGFAILURE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_FINDRESFAILURE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_MEMLOCKFAILURE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_INITIALIZATION"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_NOHINSTANCE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_NOHOOK"/><br/>
        /// <see cref=" CommonDialogEnumerations.CommonDialogErrorCode.CDERR_LOADRESFAILURE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_NOTEMPLATE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_LOADSTRFAILURE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_STRUCTSIZE"/><br/>
        /// <see cref="CommonDialogEnumerations.CommonDialogErrorCode.CDERR_MEMALLOCFAILURE"/><br/><br/>
        /// La finestra di dialogo non supporta le tavolozze, i colori selezionabili sono limitati ai colori di sistema e le loro versioni retinate.<br/><br/>
        /// Può essere fornita una procedura di hook per gestire i messaggi per la finestra di dialogo.<br/>
        /// Per abilitare la procedura di hook, includere nel campo <see cref="CHOOSECOLOR.Flags"/> il valore <see cref="CommonDialogEnumerations.ColorDialogInitializationFlags.CC_ENABLEHOOK"/> e impostare <see cref="CHOOSECOLOR.MessageHandler"/> a un'istanza del delegato.</remarks>
        [DllImport("Comdlg32.dll", EntryPoint = "ChooseColorW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ChooseColor(ref CHOOSECOLOR InitializazionData);
    }
}