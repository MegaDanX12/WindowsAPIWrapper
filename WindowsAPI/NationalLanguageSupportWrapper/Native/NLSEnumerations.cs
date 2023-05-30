using System;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;

namespace WindowsAPI.NationalLanguageSupportWrapper.Native
{
    /// <summary>
    /// Enumerazioni NLS.
    /// </summary>
    internal static class NLSEnumerations
    {
        /// <summary>
        /// Opzioni di confronto.
        /// </summary>
        [Flags]
        internal enum ComparisonOptions
        {
            /// <summary>
            /// Ignora la differenza linguistica tra le maiuscole e le minuscole.
            /// </summary>
            LINGUISTIC_IGNORECASE = 16,
            /// <summary>
            /// Ignora i caratteri senza spaziatura, in modo linguisticamente appropriato.
            /// </summary>
            /// <remarks>Questo valore non produce sempre risultati prevedibii con caratteri decomposti, cioè caratteri il cui carattere base e o uno o più caratteri senza spaziatura ha valori diversi.</remarks>
            LINGUISTIC_IGNOREDIACRITIC = 32,
            /// <summary>
            /// Ignora la differenza linguistica tra le maiuscole e le minuscole.
            /// </summary>
            /// <remarks>Questo valore ignora qualunque distinzione terziaria che il caso sia linguistico o meno.<br/><br/>
            /// La distinzione tra le forme larghe e ristrette dei caratteri CJK compatibili.</remarks>
            NORM_IGNORECASE = 1,
            /// <summary>
            /// Ignora la differenza tra i caratteri hiragana e katakana.
            /// </summary>
            NORM_IGNOREKANATYPE = 65536,
            /// <summary>
            /// Ignora i caratteri senza spaziatura.
            /// </summary>
            /// <remarks>Questo valore ignora qualunque distinzione secondaria che il carattere sia diacritico o meno.<br/><br/>
            /// Questo valore ha effetto solo su località nelle quali i caratteri accentati sono ordinati in un secondo passaggio rispetto ai caratteri principali.</remarks>
            NORM_IGNORENONSPACE = 2,
            /// <summary>
            /// Ignora i simboli e la punteggiatura.
            /// </summary>
            NORM_IGNORESYMBOLS = 4,
            /// <summary>
            /// Ignora la differenza tra caratteri half-width e full-width.
            /// </summary>
            NORM_IGNOREWIDTH = 131072,
            /// <summary>
            /// Usa le regole linguistiche per le lettere maiuscole, al posto delle regole del file system.
            /// </summary>
            NORM_LINGUISTIC_CASING = 134217728
        }

        /// <summary>
        /// Opzioni di ordinamento.
        /// </summary>
        [Flags]
        internal enum SortingOptions
        {
            /// <summary>
            /// Tratta le cifre come numeri durante l'ordinamento.
            /// </summary>
            SORT_DIGITSASNUMBERS = 8,
            /// <summary>
            /// Tratta la punteggiatura allo stesso modo dei simboli.
            /// </summary>
            SORT_STRINGSORT = 4096
        }

        /// <summary>
        /// Risultato del confronto.
        /// </summary>
        internal enum ComparisonResult
        {
            /// <summary>
            /// La prima stringa ha un valore lessicale minore della seconda.
            /// </summary>
            CSTR_LESS_THAN = 1,
            /// <summary>
            /// Le stringhe hanno lo stesso valore lessicale.
            /// </summary>
            /// <remarks>Le stringhe sono equivalenti per scopi di ordinamento, non necessariamente identiche.</remarks>
            CSTR_EQUAL,
            /// <summary>
            /// La prima stringa ha un valore lessicale maggiore della seconda.
            /// </summary>
            CSTR_GREATER_THAN
        }

        /// <summary>
        /// ID calendario.
        /// </summary>
        internal enum CalendarID : uint
        {
            /// <summary>
            /// Gregoriano (localizzato).
            /// </summary>
            CAL_GREGORIAN = 1,
            /// <summary>
            /// Gregoriano (sempre in inglese).
            /// </summary>
            CAL_GREGORIAN_US,
            /// <summary>
            /// Era imperatori giapponesi.
            /// </summary>
            CAL_JAPAN,
            /// <summary>
            /// Calendario Taiwan.
            /// </summary>
            CAL_TAIWAN,
            /// <summary>
            /// Era Tangun coreana.
            /// </summary>
            CAL_KOREA,
            /// <summary>
            /// Calendario Hijiri (arabo lunare).
            /// </summary>
            CAL_HIJRI,
            /// <summary>
            /// Calendario Thai.
            /// </summary>
            CAL_THAI,
            /// <summary>
            /// Calendario ebreo (lunare).
            /// </summary>
            CAL_HEBREW,
            /// <summary>
            /// Gregoriano francese del medio oriente.
            /// </summary>
            CAL_GREGORIAN_ME_FRENCH,
            /// <summary>
            /// Gregoriano arabo.
            /// </summary>
            CAL_GREGORIAN_ARABIC,
            /// <summary>
            /// Gregoriano inglese transliterato.
            /// </summary>
            CAL_GREGORIAN_XLIT_ENGLISH,
            /// <summary>
            /// Gregoriano francese transliterato.
            /// </summary>
            CAL_GREGORIAN_XLIT_FRENCH,
            /// <summary>
            /// Calendario Um Al Qura (lunare arabo).
            /// </summary>
            CAL_UMALQURA = 23,
            /// <summary>
            /// Tutti i calendari validi per la località.
            /// </summary>
            ENUM_ALL_CALENDARS = uint.MaxValue
        }

        /// <summary>
        /// Opzioni.
        /// </summary>
        [Flags]
        internal enum CalendarOptions : uint
        {
            /// <summary>
            /// Usa le impostazioni predefinite di sistema al posto di quelle dell'utente.
            /// </summary>
            CAL_NOUSEROVERRIDE = NLSConstants.LOCALE_NOUSEROVERRIDE,
            /// <summary>
            /// Restituisce le forme genitive dei nomi dei mesi.
            /// </summary>
            CAL_RETURN_GENITIVE_NAMES = NLSConstants.LOCALE_RETURN_GENITIVE_NAMES,
            /// <summary>
            /// Restituisce il risultato come un numero al posto che come una stringa.
            /// </summary>
            CAL_RETURN_NUMBER = NLSConstants.LOCALE_RETURN_NUMBER,
            /// <summary>
            /// Usa la code page ANSI di sistema al posto di quella locale.
            /// </summary>
            CAL_USE_CP_ACP = NLSConstants.LOCALE_USE_CP_ACP
        }

        /// <summary>
        /// Dati del calendario.
        /// </summary>
        internal enum CalendarData
        {
            /// <summary>
            /// Tipo del calendario alternativo.
            /// </summary>
            CAL_ICALINTVALUE = 1,
            /// <summary>
            /// Nome nativo del calendario alternativo.
            /// </summary>
            CAL_SCALNAME,
            /// <summary>
            /// Offset degli anni per ogni era.
            /// </summary>
            /// <remarks>Questo valore comprende più stringhe a terminazione nulla, la lista termina con un carattere nullo.</remarks>
            CAL_IYEAROFFSETRANGE,
            /// <summary>
            /// Ere associate con i valori di <see cref="CAL_IYEAROFFSETRANGE"/>.
            /// </summary>
            /// <remarks>Questo valore comprende più stringhe a terminazione nulla, la lista termina con un carattere nullo.</remarks>
            CAL_SERASTRING,
            /// <summary>
            /// Formato data corta.
            /// </summary>
            CAL_SSHORTDATE,
            /// <summary>
            /// Formato data lunga.
            /// </summary>
            CAL_SLONGDATE,
            /// <summary>
            /// Nome nativo del primo giorno della settimana.
            /// </summary>
            CAL_SDAYNAME1,
            /// <summary>
            /// Nome nativo del secondo giorno della settimana.
            /// </summary>
            CAL_SDAYNAME2,
            /// <summary>
            /// Nome nativo del terzo giorno della settimana.
            /// </summary>
            CAL_SDAYNAME3,
            /// <summary>
            /// Nome nativo del quarto giorno della settimana.
            /// </summary>
            CAL_SDAYNAME4,
            /// <summary>
            /// Nome nativo del quinto giorno della settimana.
            /// </summary>
            CAL_SDAYNAME5,
            /// <summary>
            /// Nome nativo del sesto giorno della settimana.
            /// </summary>
            CAL_SDAYNAME6,
            /// <summary>
            /// Nome nativo del settimo giorno della settimana.
            /// </summary>
            CAL_SDAYNAME7,
            /// <summary>
            /// Nome nativo abbreviato del primo giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME1,
            /// <summary>
            /// Nome nativo abbreviato del secondo giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME2,
            /// <summary>
            /// Nome nativo abbreviato del terzo giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME3,
            /// <summary>
            /// Nome nativo abbreviato del quarto giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME4,
            /// <summary>
            /// Nome nativo abbreviato del quinto giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME5,
            /// <summary>
            /// Nome nativo abbreviato del sesto giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME6,
            /// <summary>
            /// Nome nativo abbreviato del settimo giorno della settimana.
            /// </summary>
            CAL_SABBREVDAYNAME7,
            /// <summary>
            /// Nome nativo del primo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME1,
            /// <summary>
            /// Nome nativo del secondo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME2,
            /// <summary>
            /// Nome nativo del terzo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME3,
            /// <summary>
            /// Nome nativo del quarto mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME4,
            /// <summary>
            /// Nome nativo del quinto mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME5,
            /// <summary>
            /// Nome nativo del sesto mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME6,
            /// <summary>
            /// Nome nativo del settimo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME7,
            /// <summary>
            /// Nome nativo dell'ottavo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME8,
            /// <summary>
            /// Nome nativo del nono mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME9,
            /// <summary>
            /// Nome nativo del decimo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME10,
            /// <summary>
            /// Nome nativo dell'undicesimo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME11,
            /// <summary>
            /// Nome nativo del dodicesimo mese dell'anno.
            /// </summary>
            CAL_SMONTHNAME12,
            /// <summary>
            /// Nome nativo del tredicesimo mese dell'anno, se esiste.
            /// </summary>
            CAL_SMONTHNAME13,
            /// <summary>
            /// Nome nativo abbreviato del primo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME1,
            /// <summary>
            /// Nome nativo abbreviato del secondo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME2,
            /// <summary>
            /// Nome nativo abbreviato del terzo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME3,
            /// <summary>
            /// Nome nativo abbreviato del quarto mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME4,
            /// <summary>
            /// Nome nativo abbreviato del quinto mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME5,
            /// <summary>
            /// Nome nativo abbreviato del sesto mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME6,
            /// <summary>
            /// Nome nativo abbreviato del settimo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME7,
            /// <summary>
            /// Nome nativo abbreviato dell'ottavo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME8,
            /// <summary>
            /// Nome nativo abbreviato del nono mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME9,
            /// <summary>
            /// Nome nativo abbreviato del decimo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME10,
            /// <summary>
            /// Nome nativo abbreviato del undicesimo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME11,
            /// <summary>
            /// Nome nativo abbreviato del dodicesimo mese dell'anno.
            /// </summary>
            CAL_SABBREVMONTHNAME12,
            /// <summary>
            /// Nome nativo abbreviato del tredicesimo mese dell'anno, se esiste.
            /// </summary>
            CAL_SABBREVMONTHNAME13,
            /// <summary>
            /// Formato anno/mese.
            /// </summary>
            CAL_SYEARMONTH,
            /// <summary>
            /// Numero intero che indica il valore massimo dell'anno a due cifre.
            /// </summary>
            CAL_ITWODIGITYEARMAX,
            /// <summary>
            /// Nome nativo corto del primo giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME1,
            /// <summary>
            /// Nome nativo corto del secondo giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME2,
            /// <summary>
            /// Nome nativo corto del terzo giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME3,
            /// <summary>
            /// Nome nativo corto del quarto giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME4,
            /// <summary>
            /// Nome nativo corto del quinto giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME5,
            /// <summary>
            /// Nome nativo corto del sesto giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME6,
            /// <summary>
            /// Nome nativo corto del settimo giorno della settimana.
            /// </summary>
            CAL_S_SHORTESTDAYNAME7,
            /// <summary>
            /// Formato del mese e del giorno.
            /// </summary>
            CAL_SMONTHDAY,
            /// <summary>
            /// Nome nativo abbreviato dell'era.
            /// </summary>
            CAL_SABBREVERASTRING,
            /// <summary>
            /// Data lunga senza anno, giorno della settimana, mese, data.
            /// </summary>
            CAL_SRELATIVELONGDATE,
            /// <summary>
            /// Nomi inglesi delle ere.
            /// </summary>
            CAL_SENGLISHERANAME,
            /// <summary>
            /// Nomi inglesi abbreviati delle ere.
            /// </summary>
            CAL_SENGLISHABBREVERANAME,
            /// <summary>
            /// Primo anno ichinen o gannen.
            /// </summary>
            CAL_SJAPANESEERAFIRSTYEAR
        }

        /// <summary>
        /// Formato data.
        /// </summary>
        [Flags]
        internal enum DateFormat : uint
        {
            /// <summary>
            /// Formato data corta.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_MONTHDAY"/>, <see cref="DATE_LONGDATE"/> e <see cref="DATE_YEARMONTH"/>.</remarks>
            DATE_SHORTDATE = 1,
            /// <summary>
            /// Formato data lunga.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_MONTHDAY"/>, <see cref="DATE_SHORTDATE"/> e <see cref="DATE_YEARMONTH"/>.</remarks>
            DATE_LONGDATE,
            /// <summary>
            /// Usa il calendario alternativo (se esiste).
            /// </summary>
            DATE_USE_ALT_CALENDAR = 4,
            /// <summary>
            /// Formato anno/mese.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_MONTHDAY"/>, <see cref="DATE_SHORTDATE"/> e <see cref="DATE_LONGDATE"/>.</remarks>
            DATE_YEARMONTH = 8,
            /// <summary>
            /// Aggiunge segni per la lettura da sinistra a destra.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_RTLREADING"/>.</remarks>
            DATE_LTRREADING = 16,
            /// <summary>
            /// Aggiunge segni per la lettura da destra a sinistra.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_LTRREADING"/>.</remarks>
            DATE_RTLREADING = 32,
            /// <summary>
            /// Determina automaticamente quali segni per l'ordine di lettura inserire.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_LTRREADING"/> e <see cref="DATE_RTLREADING"/>.</remarks>
            DATE_AUTOLAYOUT = 64,
            /// <summary>
            /// Combinazione dei formati mese e giorno.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="DATE_YEARMONTH"/>, <see cref="DATE_SHORTDATE"/> e <see cref="DATE_LONGDATE"/>.</remarks>
            DATE_MONTHDAY = 128
        }

        /// <summary>
        /// Stato della code page.
        /// </summary>
        internal enum CodePageType
        {
            /// <summary>
            /// Enumera code pages installate.
            /// </summary>
            CP_INSTALLED = 1,
            /// <summary>
            /// Enumera code pages supportate.
            /// </summary>
            CP_SUPPORTED
        }

        /// <summary>
        /// Formato ora.
        /// </summary>
        internal enum TimeFormat
        {
            /// <summary>
            /// Non usare minuti o secondi.
            /// </summary>
            TIME_NOMINUTESORSECONDS = 1,
            /// <summary>
            /// Non usare i secondi.
            /// </summary>
            TIME_NOSECONDS,
            /// <summary>
            /// Non usare i segni.
            /// </summary>
            TIME_NOTIMEMARKER = 4,
            /// <summary>
            /// Usa sempre il formato 24 ore.
            /// </summary>
            TIME_FORCE24HOURFORMAT = 8
        }

        /// <summary>
        /// Opzioni di ricerca.
        /// </summary>
        internal enum SearchOptions
        {
            /// <summary>
            /// Cerca dall'inizio della stringa.
            /// </summary>
            FIND_FROMSTART = 4194304,
            /// <summary>
            /// Cerca dalla fine della stringa, in ordine inverso.
            /// </summary>
            FIND_FROMEND = 8388608,
            /// <summary>
            /// Controlla se la stringa inizia con un certo valore.
            /// </summary>
            FIND_STARTSWITH = 1048576,
            /// <summary>
            /// Controlla se la stringa finisce con un certo valore.
            /// </summary>
            FIND_ENDSWITH = 2097152
        }

        /// <summary>
        /// Opzioni di trasformazione.
        /// </summary>
        [Flags]
        internal enum TransformationOptions
        {
            /// <summary>
            /// Associa caratteri accentati a caratteri decomposti.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MAP_PRECOMPOSED"/>.</remarks>
            MAP_COMPOSITE = 64,
            /// <summary>
            /// Espande tutti i caratteri di ligatura così che siano rappresentati dai due caratteri equivalenti.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MAP_PRECOMPOSED"/> e <see cref="MAP_COMPOSITE"/>.</remarks>
            MAP_EXPAND_LIGATURES = 8192,
            /// <summary>
            /// Trasforma i caratteri nella zona di compatibilità negli equivalenti Unicode.
            /// </summary>
            MAP_FOLDCZONE = 16,
            /// <summary>
            /// Associa tutte le cifre ai caratteri Unicode da 0 a 9.
            /// </summary>
            MAP_FOLDDIGITS = 128,
            /// <summary>
            /// Associa caratteri accentati a caratteri precomposti.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MAP_COMPOSITE"/>.</remarks>
            MAP_PRECOMPOSED = 32
        }

        /// <summary>
        /// Valore predefiniti per le code page.
        /// </summary>
        internal enum CodePageDefaultValue
        {
            /// <summary>
            /// Code page ANSI Windows predefinita di sistema.
            /// </summary>
            CP_ACP,
            /// <summary>
            /// Code page Macintosh predefinita di sistema.
            /// </summary>
            CP_MACCP = 2,
            /// <summary>
            /// Code page OEM predefinita di sistema.
            /// </summary>
            CP_OEMCP = 1,
            /// <summary>
            /// Code page ANSI del thread corrente.
            /// </summary>
            CP_THREAD_ACP = 3,
            /// <summary>
            /// Traduzione simboli.
            /// </summary>
            CP_SYMBOL = 42,
            /// <summary>
            /// Traduzione UTF-7.
            /// </summary>
            CP_UTF7 = 65000,
            /// <summary>
            /// Traduzione UTF-8.
            /// </summary>
            CP_UTF8 = 65001
        }

        /// <summary>
        /// Posizione del segno negativo in un valore monetario.
        /// </summary>
        internal enum NegativeSignPosition
        {
            /// <summary>
            /// Tra parentesi.
            /// </summary>
            ParenthesisSurrounded,
            /// <summary>
            /// Precede il numero.
            /// </summary>
            PrecededNumber,
            /// <summary>
            /// Segue il numero.
            /// </summary>
            FollowsNumber,
            /// <summary>
            /// Precede il simbolo di valuta.
            /// </summary>
            PrecedesSymbol,
            /// <summary>
            /// Segue il simbolo di valuta.
            /// </summary>
            FollowsSymbol
        }

        /// <summary>
        /// Posizione del simbolo della valuta in un valore positivo.
        /// </summary>
        internal enum PositiveCurrencyMonetarySymbolPosition
        {
            /// <summary>
            /// $1.1
            /// </summary>
            PrefixNoSeparation,
            /// <summary>
            /// 1.1$
            /// </summary>
            SuffixNoSeparation,
            /// <summary>
            /// $ 1.1
            /// </summary>
            PrefixSingleCharSeparation,
            /// <summary>
            /// 1.1 $
            /// </summary>
            SuffixSingleCharSeparation
        }

        /// <summary>
        /// Informazione geografica.
        /// </summary>
        internal enum SYSGEOTYPE
        {
            /// <summary>
            /// Latitudine.
            /// </summary>
            /// <remarks>Il valore viene restituito come numero floating point.</remarks>
            GEO_LATITUDE = 2,
            /// <summary>
            /// Longitudine.
            /// </summary>
            /// <remarks>Il valore viene restituito come numero floating point.</remarks>
            GEO_LONGITUDE,
            /// <summary>
            /// Codice a 2 lettere del paese/della regione.
            /// </summary>
            /// <remarks>Il valore viene restituito come stringa.</remarks>
            GEO_ISO2,
            /// <summary>
            /// Codice a 3 lettere del paese/della regione.
            /// </summary>
            /// <remarks>Il valore viene restituito come stringa.</remarks>
            GEO_ISO3,
            /// <summary>
            /// Nome comune della nazione.
            /// </summary>
            /// <remarks>Il valore viene restituito come stringa.</remarks>
            GEO_FRIENDLYNAME = 8,
            /// <summary>
            /// Nome ufficiale della nazione.
            /// </summary>
            /// <remarks>Il valore viene restituito come stringa.</remarks>
            GEO_OFFICIALNAME,
            /// <summary>
            /// Non implementato.
            /// </summary>
            GEO_TIMEZONES,
            /// <summary>
            /// Non implementato.
            /// </summary>
            GEO_OFFICIALLANGUAGES,
            /// <summary>
            /// Codice a 3 lettere del paese/della regione.
            /// </summary>
            /// <remarks>Il valore viene restituito come stringa.</remarks>
            GEO_ISO_UN_NUMBER,
            /// <summary>
            /// Identificatore della posizione geografica della regione padre del paese/della regione.
            /// </summary>
            /// <remarks>Il valore viene restituito come stringa.</remarks>
            GEO_PARENT,
            /// <summary>
            /// Il prefisso telefonico da usare con i numeri di telefono nella località geografica.
            /// </summary>
            GEO_DIALINGCODE,
            /// <summary>
            /// Codice a tre lettere per la valuta che la località geografica usa.
            /// </summary>
            GEO_CURRENCYCODE,
            /// <summary>
            /// Simbolo della valuta che la località geografica usa.
            /// </summary>
            GEO_CURRENCYSYMBOL,
            /// <summary>
            /// Codice ISO a 2 lettere o il codice numerico UN M.49 per la regione.
            /// </summary>
            GEO_NAME
        }

        /// <summary>
        /// Classe geografica.
        /// </summary>
        internal enum GEOCLASS
        {
            GEOCLASS_NATION = 16,
            GEOCLASS_ALL = 0
        }

        /// <summary>
        /// Primo giorno della settimana.
        /// </summary>
        internal enum FirstDayOfWeek
        {
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME1"/>.
            /// </summary>
            Monday,
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME2"/>.
            /// </summary>
            Tuesday,
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME3"/>.
            /// </summary>
            Wednesday,
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME4"/>.
            /// </summary>
            Thursday,
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME5"/>.
            /// </summary>
            Friday,
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME6"/>.
            /// </summary>
            Saturday,
            /// <summary>
            /// Associato a <see cref="LOCALE_SDAYNAME7"/>.
            /// </summary>
            Sunday
        }

        /// <summary>
        /// Prima settimana dell'anno.
        /// </summary>
        internal enum FirstWeekOfYear
        {
            /// <summary>
            /// Settimana contenente la data 1/1.
            /// </summary>
            /// <remarks>Può essere un singolo giorno.</remarks>
            FirstJanuaryWeek,
            /// <summary>
            /// Prima intera settimana dopo 1/1.
            /// </summary>
            FirstFullWeekAfterFirstJanuary,
            /// <summary>
            /// Prima settimana con almeno 4 giorni.
            /// </summary>
            /// <remarks>Compatibile con ISO 8601.</remarks>
            FirstWeekWith4Days
        }

        /// <summary>
        /// Ordine di lettura.
        /// </summary>
        internal enum ReadingLayout
        {
            /// <summary>
            /// Da sinistra a destra.
            /// </summary>
            LeftToRight,
            /// <summary>
            /// Da destra a sinistra.
            /// </summary>
            RightToLeft,
            /// <summary>
            /// Verticalmente dall'alto verso il basso con colonne da destra a sinistra oppure letto in righe orizzontali da sinistra a destra.
            /// </summary>
            VerticallyTopBottomRTLHorizontal,
            /// <summary>
            /// Verticalmente dall'alto verso il basso con colonne da sinistra a destra.
            /// </summary>
            VerticallyTopBottomLTRHorizontal,
        }

        /// <summary>
        /// Funzionalità NLS.
        /// </summary>
        internal enum SYSNLS_FUNCTION
        {
            /// <summary>
            /// Confronto di due stringhe.
            /// </summary>
            COMPARE_STRING = 1
        }

        /// <summary>
        /// Informazioni sul tipo di caratteri.
        /// </summary>
        internal enum CharTypeInfo : uint
        {
            /// <summary>
            /// Tipo di carattere.
            /// </summary>
            CT_CTYPE1 = 1,
            /// <summary>
            /// Layout bidirezionale.
            /// </summary>
            CT_CTYPE2,
            /// <summary>
            /// Elaborazione testo.
            /// </summary>
            CT_CTYPE3 = 4
        }

        /// <summary>
        /// Tipo di caratteri.
        /// </summary>
        [Flags]
        internal enum CharacterTypes : ushort
        {
            /// <summary>
            /// Maiuscoli.
            /// </summary>
            C1_UPPER = 1,
            /// <summary>
            /// Minuscoli.
            /// </summary>
            C1_LOWER,
            /// <summary>
            /// Cifre decimali.
            /// </summary>
            C1_DIGIT = 4,
            /// <summary>
            /// Spazi.
            /// </summary>
            C1_SPACE = 8,
            /// <summary>
            /// Punteggiatura.
            /// </summary>
            C1_PUNCT = 16,
            /// <summary>
            /// Caratteri di controllo.
            /// </summary>
            C1_CNTRL = 32,
            /// <summary>
            /// Caratteri vuoti.
            /// </summary>
            C1_BLANK = 64,
            /// <summary>
            /// Cifre esadecimali.
            /// </summary>
            C1_XDIGIT = 128,
            /// <summary>
            /// Caratteri linguistici (alfabetico, sillabario, ideografico).
            /// </summary>
            C1_ALPHA = 256,
            /// <summary>
            /// Carattere definito, ma non parte degli altri tipi.
            /// </summary>
            C1_DEFINED = 512
        }

        /// <summary>
        /// Layout del testo.
        /// </summary>
        internal enum TextLayout
        {
            /// <summary>
            /// Da sinistra a destra.
            /// </summary>
            C2_LEFTTORIGHT = 1,
            /// <summary>
            /// Da destra a sinistra.
            /// </summary>
            C2_RIGHTTOLEFT,
            /// <summary>
            /// Numero o cifra europea.
            /// </summary>
            C2_EUROPENUMBER,
            /// <summary>
            /// Separatore numerico europeo.
            /// </summary>
            C2_EUROPESEPARATOR,
            /// <summary>
            /// Terminatore numerico europeo.
            /// </summary>
            C2_EUROPETERMINATOR,
            /// <summary>
            /// Numero arabo.
            /// </summary>
            C2_ARABICNUMBER,
            /// <summary>
            /// Separatore numerico comune.
            /// </summary>
            C2_COMMONSEPARATOR,
            /// <summary>
            /// Separatore di blocco.
            /// </summary>
            C2_BLOCKSEPARATOR,
            /// <summary>
            /// Separatore di segmento.
            /// </summary>
            C2_SEGMENTSEPARATOR,
            /// <summary>
            /// Spazio vuoto.
            /// </summary>
            C2_WHITESPACE,
            /// <summary>
            /// Altri neutrali.
            /// </summary>
            C2_OTHERNEUTRAL,
            /// <summary>
            /// Nessuna direzione implicita.
            /// </summary>
            C2_NOTAPPLICABLE = 0
        }

        /// <summary>
        /// Elaborazione testo.
        /// </summary>
        [Flags]
        internal enum TextProcessing
        {
            /// <summary>
            /// Segno senza spaziatura.
            /// </summary>
            C3_NONSPACING = 1,
            /// <summary>
            /// Segno diacritico senza spaziatura.
            /// </summary>
            C3_DIACRITIC,
            /// <summary>
            /// Segno di una vocale senza spaziatura.
            /// </summary>
            C3_VOWELMARK = 4,
            /// <summary>
            /// Simbolo.
            /// </summary>
            C3_SYMBOL =  8,
            /// <summary>
            /// Carattere Katakana.
            /// </summary>
            C3_KATAKANA = 16,
            /// <summary>
            /// Carattere Hiragana.
            /// </summary>
            C3_HIRAGANA = 32,
            /// <summary>
            /// Carattere half-width.
            /// </summary>
            C3_HALFWIDTH = 64,
            /// <summary>
            /// Carattere full-width.
            /// </summary>
            C3_FULLWIDTH = 128,
            /// <summary>
            /// Carattere ideografico.
            /// </summary>
            C3_IDEOGRAPH = 256,
            /// <summary>
            /// Carattere kashida arabo.
            /// </summary>
            C3_KASHIDA = 512,
            /// <summary>
            /// Punteggiatura considerata parte della parola.
            /// </summary>
            C3_LEXICAL = 1024,
            /// <summary>
            /// Tutti i caratteri linguistici (alfabetici, sillabari, e ideografici).
            /// </summary>
            C3_ALPHA = 32768,
            /// <summary>
            /// Unità di codice high surrogate.
            /// </summary>
            C3_HIGHSURROGATE = 2048,
            /// <summary>
            /// Unità di codice low surrogate.
            /// </summary>
            C3_LOWSURROGATE = 4096,
            /// <summary>
            /// Non applicabile.
            /// </summary>
            C3_NOTAPPLICABLE = 0
        }

        /// <summary>
        /// Opzioni di conversione IDN.
        /// </summary>
        [Flags]
        internal enum IdnConversionOptions
        {
            /// <summary>
            /// Permetti l'inclusione di code points non assegnati nella stringa di input.
            /// </summary>
            /// <remarks>Questo valore permette l'elaborazione di caratteri attualmente non legali in IDN ma che potrebbero diventarlo in versioni successive dello standard IDNA.<br/><br/>
            /// Questo valore non dovrebbe essere usato per una stringa che deve essere memorizzata.</remarks>
            IDN_ALLOW_UNASSIGNED = 1,
            /// <summary>
            /// Filtra i caratteri ASCII non permessi in nomi STD3.
            /// </summary>
            /// <remarks>Gli unici caratteri ASCII permessi sono lettere, cifre e il trattino-meno, la stringa non può né iniziare né finire con tale carattere.<br/><br/>
            /// I caratteri Unicode di controllo e il carattere "cancella" non sono permessi.</remarks>
            IDN_USE_STD3_ASCII_RULES,
            /// <summary>
            /// Abilita il fallback algoritmico EAI per le parti locali di un indirizzo email.
            /// </summary>
            IDN_EMAIL_ADDRESS = 4,
            /// <summary>
            /// Disabilita la convalida e la mappatura di Punycode.
            /// </summary>
            IDM_RAW_PUNYCODE = 8
        }

        /// <summary>
        /// Forma di normalizzazione.
        /// </summary>
        internal enum NORM_FORM
        {
            /// <summary>
            /// Non supportato.
            /// </summary>
            NormalizationOther,
            /// <summary>
            /// Forma di normalizzazione Unicode C, composizione canonica.
            /// </summary>
            /// <remarks>Trasforma i gruppi decomposti negli equivalenti precomposti canonici.</remarks>
            NormalizationC,
            /// <summary>
            /// Forma di normalizzazione Unicode D, decomposizione canonica.
            /// </summary>
            /// <remarks>Trasforma ogni carattere precomposto nel suo equivalente canonico decomposto.</remarks>
            NormalizationD,
            /// <summary>
            /// Forma di normalizzazione Unicode KC, composizione di compatibilità.
            /// </summary>
            /// <remarks>Trasforma ogni carattere base insieme al carattere di combinazione nei caratteri canonici precomposti equivalenti e tutti i caratteri di compatibilità nei loro equivalenti.</remarks>
            NormalizationKC = 5,
            /// <summary>
            /// Forma di normalizzazione Unicode KD, decomposizione di compatibilità.
            /// </summary>
            /// <remarks>Trasforma ogni carattere precomposto nel suo decomposto canonico equivalente e tutti i caratteri di compatibilità nei loro equivalenti.</remarks>
            NormalizationKD
        }

        /// <summary>
        /// Opzioni di mappatura.
        /// </summary>
        [Flags]
        internal enum MappingOptions
        {
            /// <summary>
            /// Inverti i byte.
            /// </summary>
            LCMAP_BYTEREV = 2048,
            /// <summary>
            /// Usa caratteri fullwidth dove applicabile.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="LCMAP_HALFWIDTH"/>.</remarks>
            LCMAP_FULLWIDTH = 8388608,
            /// <summary>
            /// Usa caratteri halfiwidth dove applicabile.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="LCMAP_FULLWIDTH"/>.</remarks>
            LCMAP_HALFWIDTH = 4194304,
            /// <summary>
            /// Mappa tutti i caratteri katakana a caratteri hiragana.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="LCMAP_KATAKANA"/>.</remarks>
            LCMAP_HIRAGANA = 1048576,
            /// <summary>
            /// Mappa tutti i caratteri hiragana a caratteri katakana.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="LCMAP_HIRAGANA"/>.</remarks>
            LCMAP_KATAKANA = 2097152,
            /// <summary>
            /// Usa regole linguistiche per le maiuscole al posto di quelle del file system.
            /// </summary>
            /// <remarks>Questo valore può essere usato solamente insieme a <see cref="LCMAP_UPPERCASE"/> oppure <see cref="LCMAP_LOWERCASE"/>.</remarks>
            LCMAP_LINGUISTIC_CASING = 16777216,
            /// <summary>
            /// Mappa tutti i caratteri minuscoli a caratteri maiuscoli.
            /// </summary>
            LCMAP_LOWERCASE = 256,
            /// <summary>
            /// Restituisce un hash dei pesi grezzi di una stringa.
            /// </summary>
            /// <remarks>Il buffer deve avere una dimensione di 32 bit.</remarks>
            LCMAP_HASH = 262144,
            /// <summary>
            /// Mappa caratteri cinesi tradizionali a caratteri cinesi semplificati.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="LCMAP_TRADITIONAL_CHINESE"/>.</remarks>
            LCMAP_SIMPLIFIED_CHINESE = 33554432,
            /// <summary>
            /// Restituisce un token rappresentante i parametri risolti di ordinamento per la località.
            /// </summary>
            /// <remarks>Il buffer deve avere una dimensione del tipo LPARAM.</remarks>
            LCMAP_SORTHANDLE = 536870912,
            /// <summary>
            /// Produce una chiave di ordinamento normalizzata.
            /// </summary>
            /// <remarks>Se questo valore non è specificato, viene eseguito la mappatura della stringa.</remarks>
            LCMAP_SORTKEY = 1024,
            /// <summary>
            /// Mappa tutte le prime lettere di ogni parola al loro equivalente maiuscolo.
            /// </summary>
            LCMAP_TITLECASE = 768,
            /// <summary>
            /// Mappa caratteri cinesi semplificati a caratteri cinesi tradizionali.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="LCMAP_SIMPLIFIED_CHINESE"/>.</remarks>
            LCMAP_TRADITIONAL_CHINESE = 67108864,
            /// <summary>
            /// Mappa tutti i caratteri maiuscoli a caratteri minuscoli.
            /// </summary>
            LCMAP_UPPERCASE = 512
        }
    }
}