using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.NationalLanguageSupportWrapper.Native;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;

namespace WindowsAPI.NationalLanguageSupportWrapper
{
    /// <summary>
    /// Enumerazioni NLS.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Opzioni di confronto.
        /// </summary>
        [Flags]
        public enum ComparisonOptions
        {
            /// <summary>
            /// Ignora la differenza linguistica tra lettere maiuscole e minuscole.
            /// </summary>
            IgnoreCaseLinguistic = NLSEnumerations.ComparisonOptions.LINGUISTIC_IGNORECASE,
            /// <summary>
            /// Ignora i caratteri senza spaziatura, in modo linguisticamente appropriato.
            /// </summary>
            /// <remarks>Questo valore non produce sempre risultati prevedibii con caratteri decomposti, cioè caratteri il cui carattere base e o uno o più caratteri senza spaziatura ha valori diversi.</remarks>
            IgnoreNonSpacingLinguistic = NLSEnumerations.ComparisonOptions.LINGUISTIC_IGNOREDIACRITIC,
            /// <summary>
            /// Ignora la differenza linguistica tra le maiuscole e le minuscole.
            /// </summary>
            /// <remarks>Questo valore ignora qualunque distinzione terziaria che il caso sia linguistico o meno.<br/><br/>
            /// La distinzione tra le forme larghe e ristrette dei caratteri CJK compatibili.</remarks>
            IgnoreCase = NLSEnumerations.ComparisonOptions.NORM_IGNORECASE,
            /// <summary>
            /// Ignora la differenza tra i caratteri hiragana e katakana.
            /// </summary>
            IgnoreKanatype = NLSEnumerations.ComparisonOptions.NORM_IGNOREKANATYPE,
            /// <summary>
            /// Ignora i caratteri senza spaziatura.
            /// </summary>
            /// <remarks>Questo valore ignora qualunque distinzione secondaria che il carattere sia diacritico o meno.<br/><br/>
            /// Questo valore ha effetto solo su località nelle quali i caratteri accentati sono ordinati in un secondo passaggio rispetto ai caratteri principali.</remarks>
            IgnoreNonSpacing = NLSEnumerations.ComparisonOptions.NORM_IGNORENONSPACE,
            /// <summary>
            /// Ignora i simboli e la punteggiatura.
            /// </summary>
            IgnoreSymbols = NLSEnumerations.ComparisonOptions.NORM_IGNORESYMBOLS,
            /// <summary>
            /// Ignora la differenza tra caratteri half-width e full-width.
            /// </summary>
            IgnoreWidth =  NLSEnumerations.ComparisonOptions.NORM_IGNOREWIDTH,
            /// <summary>
            /// Usa le regole linguistiche per le lettere maiuscole, al posto delle regole del file system.
            /// </summary>
            UseLinguisticCasing = NLSEnumerations.ComparisonOptions.NORM_LINGUISTIC_CASING
        }

        /// <summary>
        /// Opzioni di ordinamento.
        /// </summary>
        [Flags]
        public enum SortingOptions
        {
            /// <summary>
            /// Tratta le cifre come numeri durante l'ordinamento.
            /// </summary>
            DigitsAsNumbers = NLSEnumerations.SortingOptions.SORT_DIGITSASNUMBERS,
            /// <summary>
            /// Tratta la punteggiatura allo stesso modo dei simboli.
            /// </summary>
            PunctuationAsSymbols = NLSEnumerations.SortingOptions.SORT_STRINGSORT
        }

        /// <summary>
        /// Calendario.
        /// </summary>
        public enum Calendar : uint
        {
            /// <summary>
            /// Gregoriano (localizzato).
            /// </summary>
            Gregorian = CalendarID.CAL_GREGORIAN,
            /// <summary>
            /// Gregoriano (sempre in inglese).
            /// </summary>
            GregorianEnglish = CalendarID.CAL_GREGORIAN_US,
            /// <summary>
            /// Era imperatori giapponesi.
            /// </summary>
            JapaneseEmperorEra = CalendarID.CAL_JAPAN,
            /// <summary>
            /// Calendario Taiwan.
            /// </summary>
            Taiwan = CalendarID.CAL_TAIWAN,
            /// <summary>
            /// Era Tangun coreana.
            /// </summary>
            KoreaTangunEra = CalendarID.CAL_KOREA,
            /// <summary>
            /// Calendario Hijiri (arabo lunare).
            /// </summary>
            Hijri = CalendarID.CAL_HIJRI,
            /// <summary>
            /// Calendario Thai.
            /// </summary>
            Thai = CalendarID.CAL_THAI,
            /// <summary>
            /// Calendario ebreo (lunare).
            /// </summary>
            LunarHebrew = CalendarID.CAL_HEBREW,
            /// <summary>
            /// Gregoriano francese del medio oriente.
            /// </summary>
            GregorianMiddleEastFrench = CalendarID.CAL_GREGORIAN_ME_FRENCH,
            /// <summary>
            /// Gregoriano arabo.
            /// </summary>
            GregorianArabic = CalendarID.CAL_GREGORIAN_ARABIC,
            /// <summary>
            /// Gregoriano inglese transliterato.
            /// </summary>
            TransliteratedGregorianEnglish = CalendarID.CAL_GREGORIAN_XLIT_ENGLISH,
            /// <summary>
            /// Gregoriano francese transliterato.
            /// </summary>
            TransliteratedGregorianFrench = CalendarID.CAL_GREGORIAN_XLIT_FRENCH,
            /// <summary>
            /// Calendario Um Al Qura (lunare arabo).
            /// </summary>
            UmAlQura = CalendarID.CAL_UMALQURA,
            /// <summary>
            /// Tutti i calendari validi per la località.
            /// </summary>
            All = CalendarID.ENUM_ALL_CALENDARS
        }

        /// <summary>
        /// Dati del calendario.
        /// </summary>
        public enum CalendarData
        {
            /// <summary>
            /// Tipo del calendario alternativo.
            /// </summary>
            AlternateCalendarType = NLSEnumerations.CalendarData.CAL_ICALINTVALUE,
            /// <summary>
            /// Limite massimo del valore per gli anni a due cifre.
            /// </summary>
            TwoDigitYearMaxValue = NLSEnumerations.CalendarData.CAL_ITWODIGITYEARMAX,
            /// <summary>
            /// Offset per ogni era.
            /// </summary>
            EraOffsets = NLSEnumerations.CalendarData.CAL_IYEAROFFSETRANGE,
            /// <summary>
            /// Nome primo giorno della settimana nativo abbreviato.
            /// </summary>
            WeekFirstDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME1,
            /// <summary>
            /// Nome secondo giorno della settimana nativo abbreviato.
            /// </summary>
            WeekSecondDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME2,
            /// <summary>
            /// Nome terzo giorno della settimana nativo abbreviato.
            /// </summary>
            WeekThirdDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME3,
            /// <summary>
            /// Nome quarto giorno della settimana nativo abbreviato.
            /// </summary>
            WeekFourthDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME4,
            /// <summary>
            /// Nome quinto giorno della settimana nativo abbreviato.
            /// </summary>
            WeekFifthDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME5,
            /// <summary>
            /// Nome sesto giorno della settimana nativo abbreviato.
            /// </summary>
            WeekSixthDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME6,
            /// <summary>
            /// Nome settimo giorno della settimana nativo abbreviato.
            /// </summary>
            WeekSeventhDayAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVDAYNAME7,
            /// <summary>
            /// Nome abbreviato dell'era.
            /// </summary>
            AbbrevEraName = NLSEnumerations.CalendarData.CAL_SABBREVERASTRING,
            /// <summary>
            /// Nome primo mese nativo abbreviato.
            /// </summary>
            FirstMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME1,
            /// <summary>
            /// Nome secondo mese nativo abbreviato.
            /// </summary>
            SecondMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME2,
            /// <summary>
            /// Nome terzo mese nativo abbreviato.
            /// </summary>
            ThirdMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME3,
            /// <summary>
            /// Nome quarto mese nativo abbreviato.
            /// </summary>
            FourthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME4,
            /// <summary>
            /// Nome quinto mese nativo abbreviato.
            /// </summary>
            FifthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME5,
            /// <summary>
            /// Nome sesto mese nativo abbreviato.
            /// </summary>
            SixthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME6,
            /// <summary>
            /// Nome settimo mese nativo abbreviato.
            /// </summary>
            SeventhMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME7,
            /// <summary>
            /// Nome ottavo mese nativo abbreviato.
            /// </summary>
            EighthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME8,
            /// <summary>
            /// Nome nono mese nativo abbreviato.
            /// </summary>
            NinthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME9,
            /// <summary>
            /// Nome decimo mese nativo abbreviato.
            /// </summary>
            TenthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME10,
            /// <summary>
            /// Nome undicesimo mese nativo abbreviato.
            /// </summary>
            EleventhMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME11,
            /// <summary>
            /// Nome dodicesimo mese nativo abbreviato.
            /// </summary>
            TwelfthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME12,
            /// <summary>
            /// Nome tredicesimo mese nativo abbreviato, se esiste.
            /// </summary>
            ThirteenthMonthAbbrevNativeName = NLSEnumerations.CalendarData.CAL_SABBREVMONTHNAME13,
            /// <summary>
            /// Nome nativo del calendario alternativo.
            /// </summary>
            NativeAlternateCalendarName = NLSEnumerations.CalendarData.CAL_SCALNAME,
            /// <summary>
            /// Nome primo giorno della settimana nativo.
            /// </summary>
            WeekFirstDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME1,
            /// <summary>
            /// Nome secondo giorno della settimana nativo.
            /// </summary>
            WeekSecondDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME2,
            /// <summary>
            /// Nome terzo giorno della settimana nativo.
            /// </summary>
            WeekThirdDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME3,
            /// <summary>
            /// Nome quarto giorno della settimana nativo.
            /// </summary>
            WeekFourthDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME4,
            /// <summary>
            /// Nome quinto giorno della settimana nativo.
            /// </summary>
            WeekFifthDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME5,
            /// <summary>
            /// Nome sesto giorno della settimana nativo.
            /// </summary>
            WeekSixthDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME6,
            /// <summary>
            /// Nome settimo giorno della settimana nativo.
            /// </summary>
            WeekSeventhDayNativeName = NLSEnumerations.CalendarData.CAL_SDAYNAME7,
            /// <summary>
            /// Nome dell'era.
            /// </summary>
            EraName = NLSEnumerations.CalendarData.CAL_SERASTRING,
            /// <summary>
            /// Formati data lunga.
            /// </summary>
            LongDateFormats = NLSEnumerations.CalendarData.CAL_SLONGDATE,
            /// <summary>
            /// Formati mese/giorno.
            /// </summary>
            MonthAndDayFormat = NLSEnumerations.CalendarData.CAL_SMONTHDAY,
            /// <summary>
            /// Nome primo mese nativo.
            /// </summary>
            FirstMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME1,
            /// <summary>
            /// Nome secondo mese nativo.
            /// </summary>
            SecondMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME2,
            /// <summary>
            /// Nome terzo mese nativo.
            /// </summary>
            ThirdMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME3,
            /// <summary>
            /// Nome quarto mese nativo.
            /// </summary>
            FourthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME4,
            /// <summary>
            /// Nome quinto mese nativo.
            /// </summary>
            FifthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME5,
            /// <summary>
            /// Nome sesto mese nativo.
            /// </summary>
            SixthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME6,
            /// <summary>
            /// Nome settimo mese nativo.
            /// </summary>
            SeventhMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME7,
            /// <summary>
            /// Nome ottavo mese nativo.
            /// </summary>
            EighthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME8,
            /// <summary>
            /// Nome nono mese nativo.
            /// </summary>
            NinthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME9,
            /// <summary>
            /// Nome decimo mese nativo.
            /// </summary>
            TenthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME10,
            /// <summary>
            /// Nome undicesimo mese nativo.
            /// </summary>
            EleventhMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME11,
            /// <summary>
            /// None dodicesimo mese nativo.
            /// </summary>
            TwelfthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME12,
            /// <summary>
            /// Nome tredicesimo mese nativo, se esiste.
            /// </summary>
            ThirteenthMonthNativeName = NLSEnumerations.CalendarData.CAL_SMONTHNAME13,
            /// <summary>
            /// Formati data corta.
            /// </summary>
            ShortDateFormats = NLSEnumerations.CalendarData.CAL_SSHORTDATE,
            /// <summary>
            /// Nome primo giorno settimana più corto.
            /// </summary>
            WeekShortestFirstDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME1,
            /// <summary>
            /// Nome secondo giorno settimana più corto.
            /// </summary>
            WeekShortestSecondDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME2,
            /// <summary>
            /// Nome terzo giorno settimana più corto.
            /// </summary>
            WeekShortestThirdDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME3,
            /// <summary>
            /// Nome quarto giorno settimana più corto.
            /// </summary>
            WeekShortestFourthDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME4,
            /// <summary>
            /// Nome quinto giorno settimana più corto.
            /// </summary>
            WeekShortestFifthDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME5,
            /// <summary>
            /// Nome sesto giorno settimana più corto.
            /// </summary>
            WeekShortestSixthDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME6,
            /// <summary>
            /// Nome settimo giorno settimana più corto.
            /// </summary>
            WeekShortestSeventhDayNativeName = NLSEnumerations.CalendarData.CAL_S_SHORTESTDAYNAME7,
            /// <summary>
            /// Formati anno/mese.
            /// </summary>
            YearMonthFormats = NLSEnumerations.CalendarData.CAL_SYEARMONTH
        }

        /// <summary>
        /// Formato data.
        /// </summary>
        [Flags]
        public enum DateFormat : uint
        {
            /// <summary>
            /// Formato data corta.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="MonthDay"/>, <see cref="Long"/> e <see cref="YearMonth"/>.</remarks>
            Short = NLSEnumerations.DateFormat.DATE_SHORTDATE,
            /// <summary>
            /// Formato data lunga.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="MonthDay"/>, <see cref="Short"/> e <see cref="YearMonth"/>.</remarks>
            Long = NLSEnumerations.DateFormat.DATE_LONGDATE,
            /// <summary>
            /// Formato anno/mese.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="MonthDay"/>, <see cref="Short"/> e <see cref="Long"/>.</remarks>
            YearMonth = NLSEnumerations.DateFormat.DATE_YEARMONTH,
            /// <summary>
            /// Combinazione dei formati mese e giorno.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="YearMonth"/>, <see cref="Short"/> e <see cref="Long"/>.</remarks>
            MonthDay = NLSEnumerations.DateFormat.DATE_MONTHDAY,
            /// <summary>
            /// Usa il calendario alternativo (se esiste).
            /// </summary>
            UseAlternateCalendar = NLSEnumerations.DateFormat.DATE_USE_ALT_CALENDAR,
            /// <summary>
            /// Determina automaticamente quali segni per l'ordine di lettura inserire.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="LtrReading"/> e <see cref="RtlReading"/>.</remarks>
            AutoLayout = NLSEnumerations.DateFormat.DATE_AUTOLAYOUT,
            /// <summary>
            /// Aggiunge segni per la lettura da sinistra a destra.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="RtlReading"/>.</remarks>
            LtrReading = NLSEnumerations.DateFormat.DATE_LTRREADING,
            /// <summary>
            /// Aggiunge segni per la lettura da destra a sinistra.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="LtrReading"/>.</remarks>
            RtlReading = NLSEnumerations.DateFormat.DATE_RTLREADING
        }

        /// <summary>
        /// Opzioni di ricerca.
        /// </summary>
        public enum SearchOptions
        {
            /// <summary>
            /// Cerca dall'inizio della stringa.
            /// </summary>
            FromStart = NLSEnumerations.SearchOptions.FIND_FROMSTART,
            /// <summary>
            /// Cerca dalla fine della stringa, in ordine inverso.
            /// </summary>
            FromEnd = NLSEnumerations.SearchOptions.FIND_FROMEND,
            /// <summary>
            /// Controlla se la stringa inizia con un certo valore.
            /// </summary>
            StartsWith = NLSEnumerations.SearchOptions.FIND_STARTSWITH,
            /// <summary>
            /// Controlla se la stringa finisce con un certo valore.
            /// </summary>
            EndsWith = NLSEnumerations.SearchOptions.FIND_ENDSWITH
        }

        /// <summary>
        /// Opzioni di trasformazione.
        /// </summary>
        [Flags]
        public enum TransformationOptions
        {
            /// <summary>
            /// Associa caratteri accentati a caratteri decomposti.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="AccentedCharsToPrecomposedChars"/>.</remarks>
            AccentedCharsToDecomposedChars = NLSEnumerations.TransformationOptions.MAP_COMPOSITE,
            /// <summary>
            /// Espande tutti i caratteri di ligatura così che siano rappresentati dai due caratteri equivalenti.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="AccentedCharsToPrecomposedChars"/> e <see cref="AccentedCharsToDecomposedChars"/>.</remarks>
            LigatureCharsToTwoCharEquivalent = NLSEnumerations.TransformationOptions.MAP_EXPAND_LIGATURES,
            /// <summary>
            /// Trasforma i caratteri nella zona di compatibilità negli equivalenti Unicode.
            /// </summary>
            CZoneToUnicodeEquivalent = NLSEnumerations.TransformationOptions.MAP_FOLDCZONE,
            /// <summary>
            /// Associa tutte le cifre ai caratteri Unicode da 0 a 9.
            /// </summary>
            DigitsToUnicodeEquivalent = NLSEnumerations.TransformationOptions.MAP_FOLDDIGITS,
            /// <summary>
            /// Associa caratteri accentati a caratteri precomposti.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="AccentedCharsToDecomposedChars"/>.</remarks>
            AccentedCharsToPrecomposedChars = NLSEnumerations.TransformationOptions.MAP_PRECOMPOSED
        }

        /// <summary>
        /// Classe geografica.
        /// </summary>
        public enum GeoClass
        {
            All = GEOCLASS.GEOCLASS_ALL,
            Nation = GEOCLASS.GEOCLASS_NATION
        }

        /// <summary>
        /// Valori predefiniti per le code page.
        /// </summary>
        public enum CodePageDefaultValue
        {
            /// <summary>
            /// Code page ANSI Windows predefinita di sistema.
            /// </summary>
            WindowsANSICodePage = NLSEnumerations.CodePageDefaultValue.CP_ACP,
            /// <summary>
            /// Code page Macintosh predefinita di sistema.
            /// </summary>
            MacintoshCodePage = NLSEnumerations.CodePageDefaultValue.CP_MACCP,
            /// <summary>
            /// Code page OEM predefinita di sistema.
            /// </summary>
            OEMCodePage = NLSEnumerations.CodePageDefaultValue.CP_OEMCP,
            /// <summary>
            /// Code page ANSI del thread corrente.
            /// </summary>
            CurrentThreadANSICodePage = NLSEnumerations.CodePageDefaultValue.CP_THREAD_ACP,
            /// <summary>
            /// Traduzione simboli.
            /// </summary>
            Symbol = NLSEnumerations.CodePageDefaultValue.CP_SYMBOL,
            /// <summary>
            /// Traduzione UTF-7.
            /// </summary>
            UTF7 = NLSEnumerations.CodePageDefaultValue.CP_UTF7,
            /// <summary>
            /// Traduzione UTF-8.
            /// </summary>
            UTF8 = NLSEnumerations.CodePageDefaultValue.CP_UTF8
        }

        /// <summary>
        /// Formato di un valore monetario negativo.
        /// </summary>
        public enum NegativeCurrencyFormat
        {
            /// <summary>
            /// ($1.1)
            /// </summary>
            SymbolNumberInParenthesis,
            /// <summary>
            /// -$1.1
            /// </summary>
            SignSymbolNumber,
            /// <summary>
            /// $-1.1
            /// </summary>
            SymbolSignNumber,
            /// <summary>
            /// $1.1-
            /// </summary>
            SymbolNumberSign,
            /// <summary>
            /// (1.1$)
            /// </summary>
            NumberSymbolInParenthesis,
            /// <summary>
            /// -1.1$
            /// </summary>
            SignNumberSymbol,
            /// <summary>
            /// 1.1-$
            /// </summary>
            NumberSignSymbol,
            /// <summary>
            /// 1.1$-
            /// </summary>
            NumberSymbolSign,
            /// <summary>
            /// -1-1 $
            /// </summary>
            SignNumberSpaceSymbol,
            /// <summary>
            /// -$ 1.1
            /// </summary>
            SignSymbolSpaceNumber,
            /// <summary>
            /// 1.1 $-
            /// </summary>
            NumberSpaceSymbolSign,
            /// <summary>
            /// $ 1.1-
            /// </summary>
            SymbolSpaceNumberSign,
            /// <summary>
            /// $ -1.1
            /// </summary>
            SymbolSpaceSignNumber,
            /// <summary>
            /// 1.1- $
            /// </summary>
            NumberSignSpaceSymbol,
            /// <summary>
            /// ($ 1.1)
            /// </summary>
            SymbolSpaceNumberInParenthesis,
            /// <summary>
            /// (1.1 $)
            /// </summary>
            NumberSpaceSymbolInParenthesis
        }

        /// <summary>
        /// Posizione del simbolo in un valore monetario positivo.
        /// </summary>
        public enum PositiveCurrencySymbolPosition
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
            PrefixSingleCharacterSeparation,
            /// <summary>
            /// 1.1 $
            /// </summary>
            SuffixSingleCharacterSeparation
        }

        /// <summary>
        /// Posizione del segno negativo in un valore monetario negativo.
        /// </summary>
        public enum NegativeCurrencySignPosition
        {
            /// <summary>
            /// Tra parentesi.
            /// </summary>
            ParenthesisSurrounded = NegativeSignPosition.ParenthesisSurrounded,
            /// <summary>
            /// Precede il numero.
            /// </summary>
            PrecededNumber = NegativeSignPosition.PrecededNumber,
            /// <summary>
            /// Segue il numero.
            /// </summary>
            FollowsNumber = NegativeSignPosition.FollowsNumber,
            /// <summary>
            /// Precede il simbolo di valuta.
            /// </summary>
            PrecedesSymbol = NegativeSignPosition.PrecedesSymbol,
            /// <summary>
            /// Segue il simbolo di valuta.
            /// </summary>
            FollowsSymbol = NegativeSignPosition.FollowsSymbol
        }

        /// <summary>
        /// Formato di un valore percentuale positivo.
        /// </summary>
        public enum PositivePercentageFormat
        {
            /// <summary>
            /// # %
            /// </summary>
            NumberSpacePercent,
            /// <summary>
            /// #%
            /// </summary>
            NumberPercent,
            /// <summary>
            /// %#
            /// </summary>
            PercentNumber,
            /// <summary>
            /// % #
            /// </summary>
            PercentSpaceNumber
        }

        /// <summary>
        /// Formato numero negativo.
        /// </summary>
        public enum NegativeNumberFormat
        {
            /// <summary>
            /// (1.1)
            /// </summary>
            NumberInParenthesis,
            /// <summary>
            /// -1.1
            /// </summary>
            SignNumber,
            /// <summary>
            /// - 1.1
            /// </summary>
            SignSpaceNumber,
            /// <summary>
            /// 1.1-
            /// </summary>
            NumberSign,
            /// <summary>
            /// 1.1 -
            /// </summary>
            NumberSpaceSign
        }

        /// <summary>
        /// Layout del testo.
        /// </summary>
        public enum CharacterLayout
        {
            /// <summary>
            /// Da sinistra a destra.
            /// </summary>
            LeftToRight = TextLayout.C2_LEFTTORIGHT,
            /// <summary>
            /// Da destra a sinistra.
            /// </summary>
            RightToLeft = TextLayout.C2_RIGHTTOLEFT,
            /// <summary>
            /// Numero o cifra europea.
            /// </summary>
            EuropeNumber = TextLayout.C2_EUROPENUMBER,
            /// <summary>
            /// Separatore numerico europeo.
            /// </summary>
            EuropeSeparator = TextLayout.C2_EUROPESEPARATOR,
            /// <summary>
            /// Terminatore numerico europeo.
            /// </summary>
            EuropeTerminator = TextLayout.C2_EUROPETERMINATOR,
            /// <summary>
            /// Numero arabo.
            /// </summary>
            ArabicNumber = TextLayout.C2_ARABICNUMBER,
            /// <summary>
            /// Separatore numerico comune.
            /// </summary>
            CommonSeparator = TextLayout.C2_COMMONSEPARATOR,
            /// <summary>
            /// Separatore di blocco.
            /// </summary>
            BlockSeparator = TextLayout.C2_BLOCKSEPARATOR,
            /// <summary>
            /// Separatore di segmento.
            /// </summary>
            SegmentSeparator = TextLayout.C2_SEGMENTSEPARATOR,
            /// <summary>
            /// Spazio vuoto.
            /// </summary>
            Whitespace = TextLayout.C2_WHITESPACE,
            /// <summary>
            /// Altri neutrali.
            /// </summary>
            OtherNeutral = TextLayout.C2_OTHERNEUTRAL,
            /// <summary>
            /// Nessuna direzione implicita.
            /// </summary>
            NotApplicable = TextLayout.C2_NOTAPPLICABLE
        }

        /// <summary>
        /// Formato ora.
        /// </summary>
        public enum TimeFormat
        {
            /// <summary>
            /// Non usare minuti o secondi.
            /// </summary>
            NoMinuteOrSeconds = NLSEnumerations.TimeFormat.TIME_NOMINUTESORSECONDS,
            /// <summary>
            /// Non usare i secondi.
            /// </summary>
            NoSeconds = NLSEnumerations.TimeFormat.TIME_NOSECONDS,
            /// <summary>
            /// Non usare i segni.
            /// </summary>
            NoTimeMarker = NLSEnumerations.TimeFormat.TIME_NOTIMEMARKER,
            /// <summary>
            /// Usa sempre il formato 24 ore.
            /// </summary>
            Force24HourFormat = NLSEnumerations.TimeFormat.TIME_FORCE24HOURFORMAT
        }

        /// <summary>
        /// Opzioni di conversione IDN.
        /// </summary>
        [Flags]
        public enum IDNConversionOptions
        {
            /// <summary>
            /// Permetti l'inclusione di code points non assegnati nella stringa di input.
            /// </summary>
            /// <remarks>Questo valore permette l'elaborazione di caratteri attualmente non legali in IDN ma che potrebbero diventarlo in versioni successive dello standard IDNA.<br/><br/>
            /// Questo valore non dovrebbe essere usato per una stringa che deve essere memorizzata.</remarks>
            AllowUnassignedCodePoints = IdnConversionOptions.IDN_ALLOW_UNASSIGNED,
            /// <summary>
            /// Filtra i caratteri ASCII non permessi in nomi STD3.
            /// </summary>
            /// <remarks>Gli unici caratteri ASCII permessi sono lettere, cifre e il trattino-meno, la stringa non può né iniziare né finire con tale carattere.<br/><br/>
            /// I caratteri Unicode di controllo e il carattere "cancella" non sono permessi.</remarks>
            UseSTD3AsciiRules = IdnConversionOptions.IDN_USE_STD3_ASCII_RULES,
            /// <summary>
            /// Abilita il fallback algoritmico EAI per le parti locali di un indirizzo email.
            /// </summary>
            EnableEAIFallback = IdnConversionOptions.IDN_EMAIL_ADDRESS,
            /// <summary>
            /// Disabilita la convalida e la mappatura di Punycode.
            /// </summary>
            DisablePunycodeValidationAndMapping = IdnConversionOptions.IDM_RAW_PUNYCODE
        }

        /// <summary>
        /// Forma di normalizzazione.
        /// </summary>
        public enum NormalizationForm
        {
            /// <summary>
            /// Non supportato.
            /// </summary>
            Other = NORM_FORM.NormalizationOther,
            /// <summary>
            /// Forma di normalizzazione Unicode C, composizione canonica.
            /// </summary>
            /// <remarks>Trasforma i gruppi decomposti negli equivalenti precomposti canonici.</remarks>
            C = NORM_FORM.NormalizationC,
            /// <summary>
            /// Forma di normalizzazione Unicode D, decomposizione canonica.
            /// </summary>
            /// <remarks>Trasforma ogni carattere precomposto nel suo equivalente canonico decomposto.</remarks>
            D = NORM_FORM.NormalizationD,
            /// <summary>
            /// Forma di normalizzazione Unicode KC, composizione di compatibilità.
            /// </summary>
            /// <remarks>Trasforma ogni carattere base insieme al carattere di combinazione nei caratteri canonici precomposti equivalenti e tutti i caratteri di compatibilità nei loro equivalenti.</remarks>
            KC = NORM_FORM.NormalizationKC,
            /// <summary>
            /// Forma di normalizzazione Unicode KD, decomposizione di compatibilità.
            /// </summary>
            /// <remarks>Trasforma ogni carattere precomposto nel suo decomposto canonico equivalente e tutti i caratteri di compatibilità nei loro equivalenti.</remarks>
            KD = NORM_FORM.NormalizationKD
        }

        /// <summary>
        /// Opzioni di mappatura.
        /// </summary>
        [Flags]
        public enum MappingOptions
        {
            /// <summary>
            /// Nessuna opzione.
            /// </summary>
            None = 0,
            /// <summary>
            /// Inverti i byte.
            /// </summary>
            UseByteReversal = NLSEnumerations.MappingOptions.LCMAP_BYTEREV,
            /// <summary>
            /// Usa caratteri fullwidth dove applicabile.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="UseNarrowChars"/>.</remarks>
            UseWideChars = NLSEnumerations.MappingOptions.LCMAP_FULLWIDTH,
            /// <summary>
            /// Usa caratteri halfiwidth dove applicabile.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="UseWideChars"/>.</remarks>
            UseNarrowChars = NLSEnumerations.MappingOptions.LCMAP_HALFWIDTH,
            /// <summary>
            /// Mappa tutti i caratteri katakana a caratteri hiragana.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MapHiraganaToKatakana"/>.</remarks>
            MapKatakanaToHiragana = NLSEnumerations.MappingOptions.LCMAP_HIRAGANA,
            /// <summary>
            /// Mappa tutti i caratteri hiragana a caratteri katakana.
            /// </summary>
            /// <remarks>Questo valore non può essere usato insieme a <see cref="MapKatakanaToHiragana"/>.</remarks>
            MapHiraganaToKatakana = NLSEnumerations.MappingOptions.LCMAP_KATAKANA,
            /// <summary>
            /// Usa regole linguistiche per le maiuscole al posto di quelle del file system.
            /// </summary>
            /// <remarks>Questo valore può essere usato solamente insieme a <see cref="MapToUppercase"/> oppure <see cref="MapToLowercase"/>.</remarks>
            UseLinguisticCasing = NLSEnumerations.MappingOptions.LCMAP_LINGUISTIC_CASING,
            /// <summary>
            /// Mappa tutti i caratteri minuscoli a caratteri maiuscoli.
            /// </summary>
            MapToLowercase = NLSEnumerations.MappingOptions.LCMAP_LOWERCASE,
            /// <summary>
            /// Restituisce un hash dei pesi grezzi di una stringa.
            /// </summary>
            /// <remarks>Il buffer deve avere una dimensione di 32 bit.</remarks>
            GenerateHash = NLSEnumerations.MappingOptions.LCMAP_HASH,
            /// <summary>
            /// Mappa caratteri cinesi tradizionali a caratteri cinesi semplificati.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="MapSimplifiedChineseToTraditionalChinese"/>.</remarks>
            MapTraditionalChineseToSimplifiedChinese = NLSEnumerations.MappingOptions.LCMAP_SIMPLIFIED_CHINESE,
            /// <summary>
            /// Produce una chiave di ordinamento normalizzata.
            /// </summary>
            /// <remarks>Se questo valore non è specificato, viene eseguito la mappatura della stringa.</remarks>
            GenerateSortKey = NLSEnumerations.MappingOptions.LCMAP_SORTKEY,
            /// <summary>
            /// Mappa tutte le prime lettere di ogni parola al loro equivalente maiuscolo.
            /// </summary>
            MapToTitleCase = NLSEnumerations.MappingOptions.LCMAP_TITLECASE,
            /// <summary>
            /// Mappa caratteri cinesi semplificati a caratteri cinesi tradizionali.
            /// </summary>
            /// <remarks>Questo valore non può essere usato con <see cref="MapTraditionalChineseToSimplifiedChinese"/>.</remarks>
            MapSimplifiedChineseToTraditionalChinese = NLSEnumerations.MappingOptions.LCMAP_TRADITIONAL_CHINESE,
            /// <summary>
            /// Mappa tutti i caratteri maiuscoli a caratteri minuscoli.
            /// </summary>
            MapToUppercase = NLSEnumerations.MappingOptions.LCMAP_UPPERCASE
        }

        /// <summary>
        /// Sostituzione cifre.
        /// </summary>
        public enum DigitSubstitution
        {
            /// <summary>
            /// Basata sul contesto.
            /// </summary>
            ContextBased,
            /// <summary>
            /// Nessuna sostituzione.
            /// </summary>
            None,
            /// <summary>
            /// Sostituzione nativa delle cifre.
            /// </summary>
            NativeDigit
        }

        /// <summary>
        /// Primo giorno della settimana.
        /// </summary>
        public enum FirstDayOfWeek
        {
            /// <summary>
            /// Lunedì
            /// </summary>
            Monday = NLSEnumerations.FirstDayOfWeek.Monday,
            /// <summary>
            /// Martedì.
            /// </summary>
            Tuesday = NLSEnumerations.FirstDayOfWeek.Tuesday,
            /// <summary>
            /// Mercoledì.
            /// </summary>
            Wednesday = NLSEnumerations.FirstDayOfWeek.Wednesday,
            /// <summary>
            /// Giovedì.
            /// </summary>
            Thursday = NLSEnumerations.FirstDayOfWeek.Thursday,
            /// <summary>
            /// Venerdi.
            /// </summary>
            Friday = NLSEnumerations.FirstDayOfWeek.Friday,
            /// <summary>
            /// Sabato.
            /// </summary>
            Saturday = NLSEnumerations.FirstDayOfWeek.Saturday,
            /// <summary>
            /// Domenica.
            /// </summary>
            Sunday = NLSEnumerations.FirstDayOfWeek.Sunday
        }

        /// <summary>
        /// Prima settimana dell'anno.
        /// </summary>
        public enum FirstWeekOfYear
        {
            /// <summary>
            /// Settimana contenente il primo gennaio.
            /// </summary>
            WeekContainingFirstJanuary = NLSEnumerations.FirstWeekOfYear.FirstJanuaryWeek,
            /// <summary>
            /// Prima settimana completa dopo il primo gennaio.
            /// </summary>
            FirstFullWeekAfterFirstJanuary = NLSEnumerations.FirstWeekOfYear.FirstFullWeekAfterFirstJanuary,
            /// <summary>
            /// Prima settimana con almeno 4 giorni.
            /// </summary>
            FirstWeekWithFourDays = NLSEnumerations.FirstWeekOfYear.FirstWeekWith4Days
        }

        /// <summary>
        /// Sistema di misura.
        /// </summary>
        public enum MeasurementSystem
        {
            /// <summary>
            /// Sistema metrico decimale.
            /// </summary>
            Metric,
            /// <summary>
            /// Sistema usato dagli Stati Uniti.
            /// </summary>
            Imperial
        }

        /// <summary>
        /// Dimensione della carta.
        /// </summary>
        public enum PaperSize
        {
            USLetter = 1,
            USLegal = 5,
            A3 = 8,
            A4
        }

        /// <summary>
        /// Layout di lettura.
        /// </summary>
        public enum ReadingLayout
        {
            /// <summary>
            /// Da sinistra a destra.
            /// </summary>
            LeftToRight = NLSEnumerations.ReadingLayout.LeftToRight,
            /// <summary>
            /// Da destra a sinistra.
            /// </summary>
            RightToLeft = NLSEnumerations.ReadingLayout.RightToLeft,
            /// <summary>
            /// Verticalmente dall'alto verso il basso con colonne da destra a sinistra oppure letto in righe orizzontali da sinistra a destra.
            /// </summary>
            VerticallyTopBottomRTLHorizontal = NLSEnumerations.ReadingLayout.VerticallyTopBottomRTLHorizontal,
            /// <summary>
            /// Verticalmente dall'alto verso il basso con colonne da sinistra a destra.
            /// </summary>
            VerticallyTopBottomLTRHorizontal = NLSEnumerations.ReadingLayout.VerticallyTopBottomLTRHorizontal,
        }
    }
}