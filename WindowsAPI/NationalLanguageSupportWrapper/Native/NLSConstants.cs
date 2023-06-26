using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;

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

        /// <summary>
        /// Località usata per funzione che richiedono risultati indipendenti dalla località e consistenti.
        /// </summary>
        internal const int LOCALE_INVARIANT = 127;

        /// <summary>
        /// Non utilizzare le impostazioni dell'utente.
        /// </summary>
        internal const uint LOCALE_NOUSEROVERRIDE = 2147483648;

        /// <summary>
        /// Usa la code page di sistema.
        /// </summary>
        internal const int LOCALE_USE_CP_ACP = 1073741824;

        /// <summary>
        /// Restituire un numero al posto di una stringa.
        /// </summary>
        internal const int LOCALE_RETURN_NUMBER = 536870912;

        /// <summary>
        /// Restituire le forme genitive dei nomi di mesi.
        /// </summary>
        internal const int LOCALE_RETURN_GENITIVE_NAMES = 268435456;

        /// <summary>
        /// Tutte le località incluse nel sistema operativo, incluse le località sostitutive, esclude quelle con regole di ordinamento alternative.
        /// </summary>
        internal const int LOCALE_WINDOWS = 1;

        /// <summary>
        /// Località supplementari.
        /// </summary>
        internal const int LOCALE_SUPPLEMENTAL = 2;

        /// <summary>
        /// Località con regole di ordinamento alternative.
        /// </summary>
        internal const int LOCALE_ALTERNATE_SORTS = 4;

        /// <summary>
        /// Località sostitutive.
        /// </summary>
        internal const int LOCALE_REPLACEMENT = 8;

        /// <summary>
        /// Dati neutrali località.
        /// </summary>
        internal const int LOCALE_NEUTRALDATA = 16;

        /// <summary>
        /// Dati località specifici.
        /// </summary>
        internal const int LOCALE_SPECIFICDATA = 32;

        /// <summary>
        /// Tutte le località.
        /// </summary>
        internal const int LOCALE_ALL = 0;

        /// <summary>
        /// Numero massimo di lead byte per una code page.
        /// </summary>
        internal const int MAX_LEADBYTES = 12;

        /// <summary>
        /// Dimensione massima del carattere predefinito.
        /// </summary>
        internal const int MAX_DEFAULTCHAR = 2;

        /// <summary>
        /// Numero di cifre decimali per il formato monetario locale.
        /// </summary>
        /// <remarks>Questa stringa può avere massimo 2 caratteri, inclusi i valore da 0 a 9 e un carattere nullo finale.</remarks>
        internal const int LOCALE_ICURRDIGITS = 25;

        /// <summary>
        /// Presenza di zero iniziali in campi decimali.
        /// </summary>
        /// <remarks>0: non ci sono zero iniziali<br/>
        /// 1: ci sono zero iniziali</remarks>
        internal const int LOCALE_ILZERO = 12;

        /// <summary>
        /// Formato valuta negativa.
        /// </summary>
        /// <remarks>Valori validi:<br/><br/>
        /// 0: ($1.1)<br/>
        /// 1: -$1.1<br/>
        /// 2: $-1.1<br/>
        /// 3: $1.1-<br/>
        /// 4: (1.1$)<br/>
        /// 5: -1.1$<br/>
        /// 6: 1.1-$<br/>
        /// 7: 1.1$-<br/>
        /// 8: -1.1 $<br/>
        /// 9: -$ 1.1<br/>
        /// 10: 1.1 $-<br/>
        /// 11: $ 1.1-<br/>
        /// 12: $ -1.1<br/>
        /// 13: 1.1- $<br/>
        /// 14: ($ 1.1)<br/>
        /// 15: (1.1 $)</remarks>
        internal const int LOCALE_INEGCURR = 28;

        /// <summary>
        /// Formati numeri negativi.
        /// </summary>
        /// <remarks>0: (1.1)<br/>
        /// 1: -1.1<br/>
        /// 2: - 1.1<br/>
        /// 3: 1.1-<br/>
        /// 4: 1.1 -</remarks>
        internal const int LOCALE_INEGNUMBER = 4112;

        /// <summary>
        /// Separazione del segno negativo in un valore monetario negativo.
        /// </summary>
        /// <remarks>0: non esiste separazione<br/>
        /// 1: il segno è separato dal valore</remarks>
        internal const int LOCALE_INEGSEPBYSPACE = 87;

        /// <summary>
        /// Posizione del segno negativo in valori di valuta.
        /// </summary>
        /// <remarks>I valori validi sono quelli dell'enumerazione <see cref="NegativeSignPosition"/>.</remarks>
        internal const int LOCALE_INEGSIGNPOSN = 83;

        /// <summary>
        /// Posizione del simbolo della valuta in un valore monetario negativo.
        /// </summary>
        /// <remarks>0: il simbolo segue il valore<br/>
        /// 1: il simbolo precede il valore</remarks>
        internal const int LOCALE_INEGSYMPRECEDES = 86;

        /// <summary>
        /// Posizione del simbolo della valuta per valori positivi.
        /// </summary>
        /// <remarks>I valori validi sono quelli dell'enumerazione <see cref="PositiveCurrencyMonetarySymbolPosition"/>.</remarks>
        internal const int LOCALE_ICURRENCY = 27;

        /// <summary>
        /// Permette la restituzione di nomi neutrali durante la conversione tra nomi località e identificatori località.
        /// </summary>
        internal const int LOCALE_ALLOW_NEUTRAL_NAMES = 134217728;

        /// <summary>
        /// Tipo corrente di calendario.
        /// </summary>
        /// <remarks>I possibili valori sono definiti nell'enumerazione <see cref="CalendarData"/>.</remarks>
        internal const int LOCALE_ICALENDARTYPE = 4105;

        /// <summary>
        /// Code page ANSI usata dalla località per applicazione che non supportato Unicode.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 6 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IDEFAULTANSICODEPAGE = 4100;

        /// <summary>
        /// Code page OEM associata con il paese/la regione.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 6 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IDEFAULTCODEPAGE = 11;

        /// <summary>
        /// Code page Macintosh predefinita associata con la località.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 6 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IDEFAULTMACCODEPAGE = 4113;

        /// <summary>
        /// Code page EBCDIC associata alla località.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 6 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IDEFAULTEBCDICCODEPAGE = 4114;

        /// <summary>
        /// Codice del paese/della regione.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 6 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IDIALINGCODE = 5;

        /// <summary>
        /// Numero di cifre decimali dopo il separatore decimale.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 2 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IDIGITS = 17;

        /// <summary>
        /// Nome nativo lungo per Lunedì.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME1 = 42;

        /// <summary>
        /// Nome nativo lungo per Martedì.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME2 = 43;

        /// <summary>
        /// Nome nativo lungo per Mercoledì.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME3 = 44;

        /// <summary>
        /// Nome nativo lungo per Giovedì.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME4 = 45;

        /// <summary>
        /// Nome nativo lungo per Venerdì.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME5 = 46;

        /// <summary>
        /// Nome nativo lungo per Sabato.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME6 = 47;

        /// <summary>
        /// Nome nativo lungo per Domenica.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza massima di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDAYNAME7 = 48;

        /// <summary>
        /// Primo giorno della settimana.
        /// </summary>
        /// <remarks>I valori restituiti da questa costante sono presenti nell'enumerazione <see cref="FirstDayOfWeek"/>.</remarks>
        internal const int LOCALE_IFIRSTDAYOFWEEK = 4108;

        /// <summary>
        /// Prima settimana dell'anno.
        /// </summary>
        /// <remarks>I valori restituiti da questa costante sono presenti nell'enumerazione <see cref="FirstWeekOfYear"/>.</remarks>
        internal const int LOCALE_IFIRSTWEEKOFYEAR = 4109;

        /// <summary>
        /// Sistema di misura.
        /// </summary>
        /// <remarks>0: sistema metrico decimale<br/>
        /// 1: sistema usato dagli Stati Uniti<br/><br/>
        /// Questa string può avere una lunghezza massima di 2 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_IMEASURE = 13;

        /// <summary>
        /// Formato di un valore percentuale negativo.
        /// </summary>
        /// <remarks>0: -# %<br/>
        /// 1: -#%<br/>
        /// 2: -%#<br/>
        /// 3: %-#<br/>
        /// 4: %#-<br/>
        /// 5: #-%<br/>
        /// 6: #%-<br/>
        /// 7: -% #<br/>
        /// 8: # %-<br/>
        /// 9: % #-<br/>
        /// 10: % -#<br/>
        /// 11: #- %</remarks>
        internal const int LOCALE_INEGATIVEPERCENT = 116;

        /// <summary>
        /// Tipo di località.
        /// </summary>
        /// <remarks>0: località specifica<br/>
        /// 1: località neutrale</remarks>
        internal const int LOCALE_INEUTRAL = 113;

        /// <summary>
        /// Tipo di calendario opzionale disponibile per una località.
        /// </summary>
        internal const int LOCALE_IOPTIONALCALENDAR = 4107;

        /// <summary>
        /// Formato di un valore percentuale positivo.
        /// </summary>
        /// <remarks>0: # %<br/>
        /// 1: #%<br/>
        /// 2: %#<br/>
        /// 3: % #</remarks>
        internal const int LOCALE_IPOSITIVEPERCENT = 117;

        /// <summary>
        /// Separazione del simbolo della valuta in un valore monetario positivo.
        /// </summary>
        /// <remarks>0: il simbolo della valuta non è separato da uno spazio dal valore<br/>
        /// 1: il simbolo della valuta separato da uno spazio dal valore</remarks>
        internal const int LOCALE_IPOSSESPBYSPACE = 85;

        /// <summary>
        /// Posizione del segno in un valore monetario positivo.
        /// </summary>
        /// <remarks>I valore restituiti da questa costante sono quelli dell'enumerazione <see cref="NegativeSignPosition"/> (escluso <see cref="NegativeSignPosition.ParenthesisSurrounded"/>).</remarks>
        internal const int LOCALE_IPOSSIGNPOSN = 82;

        /// <summary>
        /// Posizione del simbolo della valuta in un valore monetario positivo.
        /// </summary>
        /// <remarks>0: il simbolo segue il valore<br/>
        /// 1: il simbolo precede il valore</remarks>
        internal const int LOCALE_IPOSSYMPRECEDES = 84;

        /// <summary>
        /// Layout di lettura per il testo.
        /// </summary>
        /// <remarks>I valore restituiti da questa costante sono quelli dell'enumerazione <see cref="ReadingLayout"/>.</remarks>
        internal const int LOCALE_IREADINGLAYOUT = 112;

        /// <summary>
        /// Nome abbreviato di un paese/una regione.
        /// </summary>
        /// <remarks>La stringa è principalmente basta sullo standard ISO 3166, la lunghezza massima è di 9 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVCTRYNAME = 7;

        /// <summary>
        /// Nome nativo abbreviato per Lunedì.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME1 = 49;

        /// <summary>
        /// Nome nativo abbreviato per Martedì.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME2 = 50;

        /// <summary>
        /// Nome nativo abbreviato per Mercoledì.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME3 = 51;

        /// <summary>
        /// Nome nativo abbreviato per Giovedì.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME4 = 52;

        /// <summary>
        /// Nome nativo abbreviato per Venerdì.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME5 = 53;

        /// <summary>
        /// Nome nativo abbreviato per Sabato.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME6 = 54;

        /// <summary>
        /// Nome nativo abbreviato per Domenica.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVDAYNAME7 = 55;

        /// <summary>
        /// Nome abbreviato di una lingua.
        /// </summary>
        internal const int LOCALE_SABBREVLANGNAME = 3;

        /// <summary>
        /// Nome nativo abbreviato per Gennaio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME1 = 68;

        /// <summary>
        /// Nome nativo abbreviato per Febbraio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME2 = 69;

        /// <summary>
        /// Nome nativo abbreviato per Marzo.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME3 = 70;

        /// <summary>
        /// Nome nativo abbreviato per Aprile.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME4 = 71;

        /// <summary>
        /// Nome nativo abbreviato per Maggio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME5 = 72;

        /// <summary>
        /// Nome nativo abbreviato per Giugno.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME6 = 73;

        /// <summary>
        /// Nome nativo abbreviato per Luglio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME7 = 74;

        /// <summary>
        /// Nome nativo abbreviato per Agosto.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME8 = 75;

        /// <summary>
        /// Nome nativo abbreviato per Settembre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME9 = 76;

        /// <summary>
        /// Nome nativo abbreviato per Ottobre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME10 = 77;

        /// <summary>
        /// Nome nativo abbreviato per Novembre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa string è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME11 = 78;

        /// <summary>
        /// Nome nativo abbreviato per Dicembre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME12 = 79;

        /// <summary>
        /// Nome nativo abbreviato per il tredicesimo mese, se esiste.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SABBREVMONTHNAME13 = 4111;

        /// <summary>
        /// Stringa per il designatore AM.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 15 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SAM = 40;

        /// <summary>
        /// Simbolo della valuta locale.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 13 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SCURRENCY = 20;

        /// <summary>
        /// Carattere usato come separatore decimale.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 4 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SDECIMAL = 14;

        /// <summary>
        /// Formato di una stringa di durata.
        /// </summary>
        internal const int LOCALE_SDURATION = 93;

        /// <summary>
        /// Nome inglese di un paese/una regione.
        /// </summary>
        /// <remarks>Il nome non può usare caratteri non inclusi nel subset a 127 caratteri ASCII.</remarks>
        internal const int LOCALE_SENGLISHCOUNTRYNAME = 4098;

        /// <summary>
        /// Nome comune della località in inglese.
        /// </summary>
        internal const int LOCALE_SENGLISHDISPLAYNAME = 114;

        /// <summary>
        /// Nome di una lingua in inglese.
        /// </summary>
        /// <remarks>Il nome non può usare caratteri non inclusi nel subset a 127 caratteri ASCII.</remarks>
        internal const int LOCALE_SENGLISHLANGUAGENAME = 4097;

        /// <summary>
        /// Dimensioni di ogni gruppo di cifre alla sinistra del decimale.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 10 caratteri, incluso il carattere nullo finale.<br/><br/>
        /// Deve essere esplicitamente indicata la dimensione di ogni gruppo e i valori devono essere separati da punti e virgola.<br/>
        /// Se l'ultimo valore è 0, il valore precedente viene ripetuto.</remarks>
        internal const int LOCALE_SGROUPING = 16;

        /// <summary>
        /// I tre caratteri del simbolo di valuta internazionale specificato nello standard ISO 4217, seguito dal carattere che lo separa dal valore numerico.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 9 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SINTLSYMBOL = 21;

        /// <summary>
        /// Nome regione a tre lettere ISO 3166.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 9 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SISO3166CTRYNAME2 = 104;

        /// <summary>
        /// Nome lingua a tre lettere ISO 639-2.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 9 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SISO639LANGNAME2 = 103;

        /// <summary>
        /// Carattere usato per separare gli oggetti di una lista.
        /// </summary>
        /// <remarks>la lunghezza massima di questa stringa è di 4 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SLIST = 12;

        /// <summary>
        /// Nome completo localizzato di un paese/una regione.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SLOCALIZEDCOUNTRYNAME = 6;

        /// <summary>
        /// Nome completo localizzato della località della lingua dell'interfaccia utente.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa non ha limiti.</remarks>
        internal const int LOCALE_SLOCALIZEDDISPLAYNAME = 2;

        /// <summary>
        /// Formato data lunga per la località.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SLONGDATE = 32;

        /// <summary>
        /// Caratteri usati come separatori decimale per la valuta.
        /// </summary>
        /// <remarks>la lunghezza massima di questa stringa è di 4 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONDECIMALSEP = 22;

        /// <summary>
        /// Dimensioni per ogni gruppo di cifre monetarie alla sinistra del decimale.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 10 caratteri, incluso il carattere nullo finale.<br/><br/>
        /// Deve essere esplicitamente indicata la dimensione di ogni gruppo e i valori devono essere separati da punti e virgola.<br/>
        /// Se l'ultimo valore è 0, il valore precedente viene ripetuto.</remarks>
        internal const int LOCALE_SMONGROUPING = 24;

        /// <summary>
        /// Caratteri usati come separatori delle gruppi di cifre monetarie alla sinistra del decimale.
        /// </summary>
        /// <remarks>la lunghezza massima di questa stringa è di 4 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHOUSANDSEP = 23;

        /// <summary>
        /// Stringa di formato per visualizzare solo mese e giorno.
        /// </summary>
        internal const int LOCALE_SMONTHDAY = 120;

        /// <summary>
        /// Nome nativo lungo per Gennaio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME1 = 56;

        /// <summary>
        /// Nome nativo lungo per Febbraio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME2 = 57;

        /// <summary>
        /// Nome nativo lungo per Marzo.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME3 = 58;

        /// <summary>
        /// Nome nativo lungo per Aprile.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME4 = 59;

        /// <summary>
        /// Nome nativo lungo per Maggio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME5 = 60;

        /// <summary>
        /// Nome nativo lungo per Giugno.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME6 = 61;

        /// <summary>
        /// Nome nativo lungo per Luglio.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME7 = 62;

        /// <summary>
        /// Nome nativo lungo per Agosto.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME8 = 63;

        /// <summary>
        /// Nome nativo lungo per Settembre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME9 = 64;

        /// <summary>
        /// Nome nativo lungo per Ottobre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME10 = 65;

        /// <summary>
        /// Nome nativo lungo per Novembre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME11 = 66;

        /// <summary>
        /// Nome nativo lungo per Dicembre.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME12 = 67;

        /// <summary>
        /// Nome nativo lungo per il tredicesimo mese.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SMONTHNAME13 = 4110;

        /// <summary>
        /// Nome località.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di <see cref="LOCALE_NAME_MAX_LENGTH"/>, incluso il carattere nullo finale.<br/><br/>
        /// La stringa è basata sulla convenzioni indicate in IETF BCP 47.</remarks>
        internal const int LOCALE_SNAME = 92;

        /// <summary>
        /// Stringa per "Not a number".
        /// </summary>
        /// <remarks>La lunghezza di questa stringa non ha limiti.</remarks>
        internal const int LOCALE_SNAN = 105;

        /// <summary>
        /// Nome nativo del paese/della regione.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SNATIVECOUNTRYNAME = 8;

        /// <summary>
        /// Equivalenti nativi dei caratteri ASCII da 0 a 9.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 11 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SNATIVEDIGITS = 19;

        /// <summary>
        /// Nome comune della località nella lingua nativa.
        /// </summary>
        internal const int LOCALE_SNATIVEDISPLAYNAME = 115;

        /// <summary>
        /// Nome nativo della lingua.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SNATIVELANGUAGENAME = 4;

        /// <summary>
        /// Segno negativo.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 5 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SNEGATIVESIGN = 81;

        /// <summary>
        /// Infinito negativo.
        /// </summary>
        /// <remarks>La lunghezza di questa stringa non ha limiti.</remarks>
        internal const int LOCALE_SNEGINFINITY = 107;

        /// <summary>
        /// Tag lingua OpenType usata per recuperare le caratteristiche topografiche appropriate da un font.
        /// </summary>
        /// <remarks>Questa costante restituisce un valore <see cref="uint"/>.</remarks>
        internal const int LOCALE_SOPENTYPELANGUAGETAG = 122;

        /// <summary>
        /// Nome della località da usare per l'ordinamento e la gestione della maiuscole.
        /// </summary>
        internal const int LOCALE_SSORTLOCALE = 123;

        /// <summary>
        /// Località di fallback, usata dal caricatore delle risorse.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 85 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SPARENT = 109;

        /// <summary>
        /// Simbolo usato per indicare la percentuale.
        /// </summary>
        /// <remarks>Questa stringa può avere una lunghezza da 0 a 3 caratteri.</remarks>
        internal const int LOCALE_SPERCENT = 118;

        /// <summary>
        /// Simbolo usato per indicare il permille.
        /// </summary>
        internal const int LOCALE_SPERMILLE = 119;

        /// <summary>
        /// Stringa per il designatore PM.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 15 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SPM = 41;

        /// <summary>
        /// Infinito positivo.
        /// </summary>
        /// <remarks>Questa stringa non ha limite di lunghezza.</remarks>
        internal const int LOCALE_SPOSINFINITY = 106;

        /// <summary>
        /// Stringa localizzata per il segno positivo della località.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 5 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SPOSITIVESIGN = 80;

        /// <summary>
        /// Stringa rappresentante una lista di script che usa la notazione a 4 caratteri ISO 15924.
        /// </summary>
        /// <remarks>Ogni nome di script consiste di 4 caratteri latini, la lista è disposta in ordine alfabetico, ogni nome, incluso l'ultimo, è seguito da un punto e virgola. </remarks>
        internal const int LOCALE_SSCRIPTS = 108;

        /// <summary>
        /// Formato data corta per la località.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SSHORTDATE = 31;

        /// <summary>
        /// Indicatore AM più corto possibile per la località.
        /// </summary>
        internal const int LOCALE_SSHORTESTAM = 126;

        /// <summary>
        /// Nome nativo corto del primo giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME1 = 96;

        /// <summary>
        /// Nome nativo corto del secondo giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME2 = 97;

        /// <summary>
        /// Nome nativo corto del terzo giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME3 = 98;

        /// <summary>
        /// Nome nativo corto del quarto giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME4 = 99;

        /// <summary>
        /// Nome nativo corto del quinto giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME5 = 100;

        /// <summary>
        /// Nome nativo corto del sesto giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME6 = 101;

        /// <summary>
        /// Nome nativo corto del settimo giorno della settimana.
        /// </summary>
        internal const int LOCALE_SSHORTESTDAYNAME7 = 102;

        /// <summary>
        /// Indicatore PM più corto possibile per la località.
        /// </summary>
        internal const int LOCALE_SSHORTESTPM = 127;

        /// <summary>
        /// Formato ora corta per la località.
        /// </summary>
        internal const int LOCALE_SSHORTTIME = 121;

        /// <summary>
        /// Carattere usato per separare gruppi di cifre alla sinistra di un decimale.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 4 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_STHOUSAND = 15;

        /// <summary>
        /// Stringa di formato per l'ora per la località.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_STIMEFORMAT = 4099;

        /// <summary>
        /// Stringa di formato anno-mese per la località.
        /// </summary>
        /// <remarks>La lunghezza massima di questa stringa è di 80 caratteri, incluso il carattere nullo finale.</remarks>
        internal const int LOCALE_SYEARMONTH = 4102;

        /// <summary>
        /// Nome nativo della valuta associata con la località.
        /// </summary>
        /// <remarks>la lunghezza di questa stringa non ha limiti.</remarks>
        internal const int LOCALE_SNATIVECURRNAME = 4104;

        /// <summary>
        /// Firma font.
        /// </summary>
        internal const int LOCALE_FONTSIGNATURE = 88;

        /// <summary>
        /// Nome inglese della valuta associata con la località.
        /// </summary>
        internal const int LOCALE_SENGCURRNAME = 4103;

        /// <summary>
        /// Nome del paese/della regione, basato sullo standard ISO 3166.
        /// </summary>
        internal const int LOCALE_SISO3166CTRYNAME = 90;

        /// <summary>
        /// Nome abbreviato della lingua basato sullo standard ISO 639, minuscolo.
        /// </summary>
        internal const int LOCALE_SISO639LANGNAME = 89;

        /// <summary>
        /// Nome completo localizzato dell'ordinamento per la località.
        /// </summary>
        internal const int LOCALE_SSORTNAME = 4115;

        /// <summary>
        /// Forma delle cifre.
        /// </summary>
        internal const int LOCALE_IDIGITSUBSTITUTION = 4116;

        /// <summary>
        /// Dimensione predefinita della carta.
        /// </summary>
        internal const int LOCALE_IPAPERSIZE = 4106;

        /// <summary>
        /// Includi script ereditati e comuni nella lista.
        /// </summary>
        internal const int GSS_ALLOW_INHERITED_COMMON = 1;

        /// <summary>
        /// Permette la presenza dello script "Latn".
        /// </summary>
        internal const int VS_ALLOW_LATIN = 1;
    }
}