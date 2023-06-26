using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes.ComboBoxEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.ComboBoxes
{
    /// <summary>
    /// Enumerazioni relative ai Combobox.
    /// </summary>
    public static class Enumerations
    {

        [Flags]
        public enum FileAttributes
        {
            /// <summary>
            /// Include archivi.
            /// </summary>
            IncludeArchivedFiles = FileType.DDL_ARCHIVE,
            /// <summary>
            /// Include le sottodirectory.
            /// </summary>
            /// <remarks>Le sottocartelle sono tra parentesi quadre.</remarks>
            IncludeSubdirectories = FileType.DDL_DIRECTORY,
            /// <summary>
            /// Tutti i dischi mappati sono inclusi.
            /// </summary>
            /// <remarks>I dischi sono visualizzati nella seguente forma: -x-, dove x è la lettera d'unità.</remarks>
            IncludeMappedDrivers = FileType.DDL_DRIVES,
            /// <summary>
            /// Solo file con gli attributi specificati.
            /// </summary>
            /// <remarks>Per impostazioni predefinita, i file in lettura/scrittura sono inclusi anche se <see cref="IncludeReadWriteFiles"/> non è incluso.</remarks>
            OnlyFilesWithSpecifiedAttributes = FileType.DDL_EXCLUSIVE,
            /// <summary>
            /// Includi file nascosti.
            /// </summary>
            IncludeHiddenFiles = FileType.DDL_HIDDEN,
            /// <summary>
            /// Includi file in sola lettura.
            /// </summary>
            IncludeReadOnlyFiles = FileType.DDL_READONLY,
            /// <summary>
            /// Include file in lettura/scrittura senza attributi speciali.
            /// </summary>
            IncludeReadWriteFiles = FileType.DDL_READWRITE,
            /// <summary>
            /// Include file di sistema.
            /// </summary>
            IncludeSystemFiles = FileType.DDL_SYSTEM,
            /// <summary>
            /// Indica di usare un metodo alternativo per inviare il messaggio.
            /// </summary>
            UsePostMessage = FileType.DDL_POSTMSGS
        }

        /// <summary>
        /// Componenti di un Combobox.
        /// </summary>
        public enum ComboboxComponent
        {
            /// <summary>
            /// Controllo di modifica.
            /// </summary>
            SelectionField = -1,
            /// <summary>
            /// Oggetti della lista
            /// </summary>
            ListItems
        }

        /// <summary>
        /// Stili di un Combobox.
        /// </summary>
        [Flags]
        public enum ComboboxStyles
        {
            /// <summary>
            /// Scorre automaticamente a destra il testo in un controllo di modifica quando l'utente scrive un carattere alla fine della linea.
            /// </summary>
            /// <remarks>Se questo stile non è impostato, il testo non può superare il bordo del rettangolo.</remarks>
            ScrollsAutomatically = ComboBoxStyles.CBS_AUTOHSCROLL,
            /// <summary>
            /// Visualizza una barra di scorrimento verticale disabiitata se non ci sono abbastanza elementi da necessitare lo scorrimento.
            /// </summary>
            /// <remarks>Se questo stile non è impostato, la barra di scorrimento è nascosta fino a quando non serve.</remarks>
            ShowDisabledVScrollBar = ComboBoxStyles.CBS_DISABLENOSCROLL,
            /// <summary>
            /// Il dropdown è visibile solo dopo che viene premuta un'icona di fianco al controllo di modifica.
            /// </summary>
            ShowListOnClick = ComboBoxStyles.CBS_DROPDOWN,
            /// <summary>
            /// Simile a <see cref="ShowListOnClick"/> con la differenza che il controllo di modifica è sostituito da un oggetto statico che visualizza la selezione corrente.
            /// </summary>
            ShowCurrentSelection = ComboBoxStyles.CBS_DROPDOWNLIST,
            /// <summary>
            /// Il Combobox disegnato dal proprietario contiene delle stringhe.
            /// </summary>
            ContainsStrings = ComboBoxStyles.CBS_HASSTRINGS,
            /// <summary>
            /// Tutti i caratteri nel controllo di modifica vengono convertiti in caratteri minuscoli.
            /// </summary>
            ConvertTextToLowercase = ComboBoxStyles.CBS_LOWERCASE,
            /// <summary>
            /// La dimensione del Combobox è esattamente quella indicata dalla creazione al momento della creazione del controllo.
            /// </summary>
            /// <remarks>Normalmente il sistema ridimensiona il controllo per evitare di visualizzare gli oggetti parzialmente.</remarks>
            ExactSize = ComboBoxStyles.CBS_NOINTEGRALHEIGHT,
            /// <summary>
            /// Converte i caratteri inseriti nel controllo di modifica dal set di caratteri di Windows al set di caratteri OEM e poi nuovamente nel set di caratteri di Windows.
            /// </summary>
            /// <remarks>Questo stile è utile se il ComboBox contiene nomi di file è può essere usato solo con <see cref="AlwaysDisplayListbox"/> o <see cref="ShowListOnClick"/>.</remarks>
            ConvertCharsToOEMCharset = ComboBoxStyles.CBS_OEMCONVERT,
            /// <summary>
            /// Il proprietario del controllo si occupa di disegnare i suoi contenuti e gli oggetti sono tutti della stessa altezza.
            /// </summary>
            AllItemsHaveSameHeight = ComboBoxStyles.CBD_OWNERDRAWFIXED,
            /// <summary>
            /// Il proprietario del controllo si occupa di disegnare i suoi contenuti e gli oggetti hanno tutti altezze diverse.
            /// </summary>
            AllItemsHaveDifferentHeight = ComboBoxStyles.CBS_OWNERDRAWVARIABLE,
            /// <summary>
            /// Visualizza sempre il list box.
            /// </summary>
            /// <remarks>La selezione corrente viene visualizza nel controllo di modifica.</remarks>
            AlwaysDisplayListbox = ComboBoxStyles.CBS_SIMPLE,
            /// <summary>
            /// Tutti i caratteri nel controllo di modifica vengono convertiti in caratteri maiuscoli.
            /// </summary>
            ConvertTextToUppercase = ComboBoxStyles.CBS_UPPERCASE,
            /// <summary>
            /// Le stringhe vengono automaticamente ordinate.
            /// </summary>
            SortStrings = ComboBoxStyles.CBS_SORT
        }

    }
}