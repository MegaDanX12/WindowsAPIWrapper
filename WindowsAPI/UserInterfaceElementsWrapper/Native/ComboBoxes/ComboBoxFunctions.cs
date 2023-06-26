using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxEnumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes
{
    /// <summary>
    /// Funzioni relative ai Combobox.
    /// </summary>
    internal static class ComboBoxFunctions
    {
        /// <summary>
        /// Sostituisce i contenuti di un Combobox con i nomi dei file e delle sottodirectory presenti nel percorso indicato.
        /// </summary>
        /// <param name="DialogHandle">Handle alla finestra di dialogo che contiene il controllo.</param>
        /// <param name="BasePath">Percorso base.</param>
        /// <param name="ControlID">ID del Combobox.</param>
        /// <param name="StaticControlID">ID di un controllo statico.</param>
        /// <param name="Options">Tipi di file da includere.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="BasePath"/> può essere un percorso assoluto, relativo, un nome di file o un percorso di rete.<br/>
        /// La stringa viene divisa in una directory è in un nome di file, la funzione cerca la directory per i file con il nome indicato, se non viene specificata una directory, la ricerca avviene in quella corrente.<br/>
        /// Se è incluso un nome di file, esso deve contenere una wildcard, se non è incluso un nome di file, la funzione si comporta come se fosse stato specificato un asterisco al suo posto.<br/>
        /// Tutti i file con il nome indicato e con gli attributi specificati da <paramref name="Options"/> sono inseriti nel Combobox.<br/><br/>
        /// Se <paramref name="ControlID"/> è impostato a 0, la funzione non tenta di riempire un Combobox.<br/><br/>
        /// Se <paramref name="StaticControlID"/> ha un valore diverso da 0, il testo del controllo viene impostato al disco e alla directory corrente.<br/><br/>
        /// Se <paramref name="Options"/> include <see cref="FileType.DDL_DIRECTORY"/> e <paramref name="BasePath"/> specifica una directory di primo livello, il Combobox conterrà sempre una voce ".." per la directory padre.<br/>
        /// Questo succede in ogni caso, anche se la directory padre è nascosta o è di sistema e <paramref name="Options"/> non include <see cref="FileType.DDL_HIDDEN"/> o <see cref="FileType.DDL_SYSTEM"/>.<br/><br/>
        /// La lista include nomi di file lunghi, se esistono.</remarks>
        [DllImport("User32.dll", EntryPoint = "DlgDirListComboBoxW", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int DirListCombobox(IntPtr DialogHandle, string BasePath, int ControlID, int StaticControlID, FileType Options);

        /// <summary>
        /// Recupera le selezione corrente di un Combobox riempito tramite <see cref="DirListCombobox"/>.
        /// </summary>
        /// <param name="DialogHandle">Handle alla finestra di dialogo che contiene il controllo.</param>
        /// <param name="Selection">Selezione.</param>
        /// <param name="SelectionLength">Lunghezza, in caratteri, di <paramref name="Selection"/>.</param>
        /// <param name="ComboboxID">ID del Combobox.</param>
        /// <returns>Diverso da zero se la selezione è un nome di directory, 0 altrimenti.</returns>
        /// <remarks>Se la selezione corrente specifica una directory o una lettera di unità, la funzioni rimuove qualunque carattare extra così che la stringa possa essere usata in un nuovo percorso.<br/><br/>
        /// Questa funzione non permette la restituzione di più di un nome file dal Combobox.<br/><br/>
        /// Se <paramref name="Selection"/> ha una dimensione pari o inferiore alla selezione, esso contiene la stringa troncata con un carattere nullo finale.</remarks> 
        [DllImport("User32.dll", EntryPoint = "DlgDirSelectComboBoxExW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DirSelectComboBox(IntPtr DialogHandle, StringBuilder Selection, int SelectionLength, int ComboboxID);

        /// <summary>
        /// Recupera informazioni su un Combobox.
        /// </summary>
        /// <param name="ComboBoxHandle">Handle al ComboBox.</param>
        /// <param name="Info">Struttura <see cref="COMBOBOXINFO"/> con le informazioni.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "GetComboBoxInfo", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetComboBoxInfo(IntPtr ComboBoxHandle, ref COMBOBOXINFO Info);
    }
}