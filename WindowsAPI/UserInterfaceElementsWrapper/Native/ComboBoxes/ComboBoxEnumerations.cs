using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ComboBoxes
{
    /// <summary>
    /// Enumerazioni relative ai Combobox.
    /// </summary>
    internal static class ComboBoxEnumerations
    {
        /// <summary>
        /// Tipi di file da includere in un combobox.
        /// </summary>
        [Flags]
        internal enum FileType
        {
            /// <summary>
            /// Include archivi.
            /// </summary>
            DDL_ARCHIVE = 32,
            /// <summary>
            /// Include le sottodirectory.
            /// </summary>
            /// <remarks>Le sottocartelle sono tra parentesi quadre.</remarks>
            DDL_DIRECTORY = 16,
            /// <summary>
            /// Tutti i dischi mappati sono inclusi.
            /// </summary>
            /// <remarks>I dischi sono visualizzati nella seguente forma: -x-, dove x è la lettera d'unità.</remarks>
            DDL_DRIVES = 16384,
            /// <summary>
            /// Solo file con gli attributi specificati.
            /// </summary>
            /// <remarks>Per impostazioni predefinita, i file in lettura/scrittura sono inclusi anche se <see cref="DDL_READWRITE"/> non è incluso.</remarks>
            DDL_EXCLUSIVE = 32768,
            /// <summary>
            /// Includi file nascosti.
            /// </summary>
            DDL_HIDDEN = 2,
            /// <summary>
            /// Includi file in sola lettura.
            /// </summary>
            DDL_READONLY = 1,
            /// <summary>
            /// Include file in lettura/scrittura senza attributi speciali.
            /// </summary>
            DDL_READWRITE = 0,
            /// <summary>
            /// Include file di sistema.
            /// </summary>
            DDL_SYSTEM = 4,
            /// <summary>
            /// Indica di usare <see cref="WindowsAndMessagesWrapper.Native.Messages.MessageFunctions.PostMessage"/> per inviare il messaggio.
            /// </summary>
            DDL_POSTMSGS = 8192
        }

        /// <summary>
        /// Stato del pulsante di un ComboBox.
        /// </summary>
        [Flags]
        internal enum ComboBoxButtonState
        {
            /// <summary>
            /// Il pulsante esiste è non è premuto.
            /// </summary>
            ExistsNotPressed,
            /// <summary>
            /// Il pulsante non esiste.
            /// </summary>
            STATE_SYSTEM_INVISIBLE = 32768,
            /// <summary>
            /// Il pulsante e premuto.
            /// </summary>
            STATE_SYSTEM_PRESSED = 8
        }

        /// <summary>
        /// Valori restituiti da un messaggio di un ComboBox.
        /// </summary>
        internal enum ComboBoxMessageReturnValue
        {
            /// <summary>
            /// Operazione riuscita.
            /// </summary>
            CB_OKAY,
            /// <summary>
            /// Si è verificato un errore.
            /// </summary>
            CB_ERR = -1,
            /// <summary>
            /// Spazio insufficiente.
            /// </summary>
            CB_ERRSPACE = -2
        }

        /// <summary>
        /// Stili di un ComboBox.
        /// </summary>
        [Flags]
        internal enum ComboBoxStyles
        {
            /// <summary>
            /// Visualizza sempre il list box.
            /// </summary>
            /// <remarks>La selezione corrente viene visualizza nel controllo di modifica.</remarks>
            CBS_SIMPLE = 1,
            /// <summary>
            /// Simile a <see cref="CBS_SIMPLE"/> con la differenza che il dropdown è visibile solo dopo che viene premuta un'icona di fianco al controllo di modifica.
            /// </summary>
            CBS_DROPDOWN,
            /// <summary>
            /// Simile a <see cref="CBS_DROPDOWNLIST"/> con la differenza che il controllo di modifica è sostituito da un oggetto statico che visualizza la selezione corrente.
            /// </summary>
            CBS_DROPDOWNLIST = 4,
            /// <summary>
            /// Il proprietario del controllo si occupa di disegnare i suoi contenuti e gli oggetti sono tutti della stessa altezza.
            /// </summary>
            CBD_OWNERDRAWFIXED = 16,
            /// <summary>
            /// Il proprietario del controllo si occupa di disegnare i suoi contenuti e gli oggetti hanno tutti altezze diverse.
            /// </summary>
            CBS_OWNERDRAWVARIABLE = 32,
            /// <summary>
            /// Scorre automaticamente a destra il testo in un controllo di modifica quando l'utente scrive un carattere alla fine della linea.
            /// </summary>
            /// <remarks>Se questo stile non è impostato, il testo non può superare il bordo del rettangolo.</remarks>
            CBS_AUTOHSCROLL = 64,
            /// <summary>
            /// Converte i caratteri inseriti nel controllo di modifica dal set di caratteri di Windows al set di caratteri OEM e poi nuovamente nel set di caratteri di Windows.
            /// </summary>
            /// <remarks>Questo stile è utile se il ComboBox contiene nomi di file è può essere usato solo con <see cref="CBS_SIMPLE"/> o <see cref="CBS_DROPDOWN"/>.</remarks>
            CBS_OEMCONVERT = 128,
            /// <summary>
            /// Le stringhe vengono automaticamente ordinate.
            /// </summary>
            CBS_SORT = 256,
            /// <summary>
            /// Il Combobox disegnato dal proprietario contiene delle stringhe.
            /// </summary>
            CBS_HASSTRINGS = 512,
            /// <summary>
            /// La dimensione del Combobox è esattamente quella indicata dalla creazione al momento della creazione del controllo.
            /// </summary>
            /// <remarks>Normalmente il sistema ridimensiona il controllo per evitare di visualizzare gli oggetti parzialmente.</remarks>
            CBS_NOINTEGRALHEIGHT = 1024,
            /// <summary>
            /// Visualizza una barra di scorrimento verticale disabiitata se non ci sono abbastanza elementi da necessitare lo scorrimento.
            /// </summary>
            /// <remarks>Se questo stile non è impostato, la barra di scorrimento è nascosta fino a quando non serve.</remarks>
            CBS_DISABLENOSCROLL = 2048,
            /// <summary>
            /// Tutti i caratteri nel controllo di modifica vengono convertiti in caratteri maiuscoli.
            /// </summary>
            CBS_UPPERCASE = 8192,
            /// <summary>
            /// Tutti i caratteri nel controllo di modifica vengono convertiti in caratteri minuscoli.
            /// </summary>
            CBS_LOWERCASE = 16384
        }
    }
}