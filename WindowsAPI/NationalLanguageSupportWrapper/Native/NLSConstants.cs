using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.NationalLanguageSupportWrapper.Native
{
    /// <summary>
    /// Costanti NLS.
    /// </summary>
    internal static class NLSConstants
    {
        /// <summary>
        /// Località personalizzata predefinita.
        /// </summary>
        /// <remarks>Questa costante viene restituita quando una funzione deve fornire un identificatore per una località supplementare per l'utente corrente al posto di <see cref="LOCALE_USER_DEFAULT"/>.</remarks>
        internal const int LOCALE_CUSTOM_DEFAULT = 3072;

        /// <summary>
        /// Località personalizzata predefinita per MUI.
        /// </summary>
        /// <remarks>Le liste di lingue preferite dall'utente e dal sistema possono contenere al massimo una lingua implementate tramite LIP e per la quale l'identificatore corrisponde a una località supplementare.<br/>
        /// Se esiste tale lingua nella lista, questa costante viene usata per riferirsi ad essa in certi contesti.</remarks>
        internal const int LOCALE_CUSTOM_UI_DEFAULT = 5120;

        /// <summary>
        /// Località personalizzata non specificata.
        /// </summary>
        /// <remarks>Questa costante è utilizzata per identificare tutte le località supplementari eccetto quelle per l'utente corrente.<br/>
        /// Alcune funzioni NLS restituiscono questo valore per indicare che non possono fornire un identificatore utile per una specifica località.</remarks>
        internal const int LOCALE_CUSTOM_UNSPECIFIED = 4096;

        /// <summary>
        /// Località predefinita per l'utente o il processo.
        /// </summary>
        internal const int LOCALE_USER_DEFAULT = 1024;

        /// <summary>
        /// Località predefinita per il sistema operativo.
        /// </summary>
        internal const int LOCALE_SYSTEM_DEFAULT = 2048;

        /// <summary>
        /// Località preferita da usare per la console.
        /// </summary>
        /// <remarks>Massima lunghezza della stringa è di 86 caratteri, incluso il carattere nullo.</remarks>
        internal const int LOCALE_SCONSOLEFALLBACKNAME = 110;

        /// <summary>
        /// Località invariante.
        /// </summary>
        internal const string LOCALE_NAME_INVARIANT = "";

        /// <summary>
        /// Lunghezza massima del nome di una località.
        /// </summary>
        internal const int LOCALE_NAME_MAX_LENGTH = 85;

        /// <summary>
        /// Nome della località del sistema operativo.
        /// </summary>
        internal const string LOCALE_NAME_SYSTEM_DEFAULT = "!x-sys-default-locale";

        /// <summary>
        /// Nome della località dell'utente corrente.
        /// </summary>
        internal const string LOCALE_NAME_USER_DEFAULT = null;
    }
}