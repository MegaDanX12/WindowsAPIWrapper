using System;
using System.Runtime.InteropServices;
using System.Text;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSStructures;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSCallbacks;
using static WindowsAPI.General.Native.GeneralStructures;
using static WindowsAPI.ErrorHandlingWrapper.Native.ErrorHandlingFunctions;

namespace WindowsAPI.NationalLanguageSupportWrapper.Native
{
    /// <summary>
    /// Funzioni NLS.
    /// </summary>
    internal static class NLSFunctions
    {
        /// <summary>
        /// Confronta due stringhe Unicode per una località specificata.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="ComparisonOptions">Opzioni di confronto.</param>
        /// <param name="FirstString">Prima stringa da confrontare.</param>
        /// <param name="FirstStringSize">Lunghezza di <paramref name="FirstString"/>, escluso il carattere nullo finale.</param>
        /// <param name="SecondString">Seconda stringa da confrontare.</param>
        /// <param name="SecondStringSize">Lunghezza di <paramref name="SecondString"/>, escluso il carattere nullo finale.</param>
        /// <param name="VersionInfo">Struttura <see cref="NLSVERSIONINFO"/> con le informazioni di versione della funzionalità NLS relativa.</param>
        /// <param name="Reserved">Riservato, deve essere nullo.</param>
        /// <param name="lParam">Riservato, deve essere 0.</param>
        /// <returns>Uno dei valori dell'enumerazione <see cref="ComparisonResult"/>.</returns>
        /// <remarks><paramref name="LocaleName"/> accetta i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// <paramref name="ComparisonOptions"/> è un valore composito costituito dalle seguenti enumerazioni:<br/><br/>
        /// <see cref="ComparisonOptions"/><br/>
        /// <see cref="SortingOptions"/><br/><br/>
        /// <paramref name="ComparisonOptions"/> può essere impostato a 0 per ottenere il comportamento predefinito.<br/><br/>
        /// <paramref name="FirstStringSize"/> e <paramref name="SecondStringSize"/> possono essere impostati a un valore negativo, se <paramref name="FirstString"/> e <paramref name="SecondString"/> terminano con un carattere nullo.<br/><br/>
        /// In caso di errore la funzione può impostare uno dei seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="ComparisonOptions"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// Questa funzione ignora i kashidas arabi durante il confronto.<br/><br/>
        /// Se <paramref name="ComparisonOptions"/> incluse <see cref="ComparisonOptions.NORM_IGNORENONSPACE"/> e <see cref="ComparisonOptions.NORM_IGNORECASE"/>, il confronto potrebbe non essere eseguito correttamente per alcune località che non supportano i caratteri senza spaziatura o la differenza tra maiuscole e minuscole.<br/>
        /// In questo caso utilizzare i valori <see cref="ComparisonOptions.LINGUISTIC_IGNOREDIACRITIC"/> e <see cref="ComparisonOptions.LINGUISTIC_IGNORECASE"/>.<br/><br/>
        /// Questa funzione può recuperare dati da località personalizzate, questi dati non è garantito che siano uguali tra computer o tra esecuzioni dell'applicazione.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "CompareStringEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern ComparisonResult CompareString(string LocaleName, uint ComparisonOptions, string FirstString, int FirstStringSize, string SecondString, int SecondStringSize, ref NLSVERSIONINFO VersionInfo, IntPtr Reserved, IntPtr lParam);

        /// <summary>
        /// Confronta l'equivalenza binaria di due stringhe Unicode.
        /// </summary>
        /// <param name="FirstString">Prima stringa da confrontare.</param>
        /// <param name="FirstStringSize">Dimensione di <paramref name="FirstString"/>.</param>
        /// <param name="SecondString">Seconda stringa da confrontare.</param>
        /// <param name="SecondStringSize">Dimensione di <paramref name="SecondString"/>.</param>
        /// <param name="IgnoreCase">Indica se eseguire un confronto ignorando la differenza tra maiuscole e minuscole, usando la tabella delle maiuscole del sistema operativo.</param>
        /// <returns>Uno dei valori dell'enumerazione <see cref="ComparisonResult"/>.</returns>
        /// <remarks>In caso di errore la funzione può impostare uno dei seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/></remarks>
        [DllImport("Kernel32.dll", EntryPoint = "CompareStringOrdinal", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern ComparisonResult CompareStringOrdinal(string FirstString, int FirstStringSize, string SecondString, int SecondStringSize, [MarshalAs(UnmanagedType.Bool)] bool IgnoreCase);

        /// <summary>
        /// Enumera le informazioni su un calendario per una località.
        /// </summary>
        /// <param name="Callback">Callback che elabora le informazioni.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Calendar">Calendario le cui informazioni vanno recuperate.</param>
        /// <param name="Reserved">Riservato, deve essere nullo.</param>
        /// <param name="CalData">Informazione da recuperare.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="CalData"/> può includere i valori dalle seguenti enumerazioni:<br/><br/>
        /// <see cref="CalendarOptions"/><br/>
        /// <see cref="CalendarData"/> (singolo)<br/><br/>
        /// <paramref name="LocaleName"/> accetta anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="CalData"/> o <paramref name="Calendar"/> non sono validi<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "EnumCalendarInfoExEx", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumCalendarInfo(CalendarInfoEnumProcedure Callback, string LocaleName, CalendarID Calendar, string Reserved, uint CalData, IntPtr lParam);

        /// <summary>
        /// Enumera i formati data disponibili per una località.
        /// </summary>
        /// <param name="Callback">Callback che elabora le informazioni.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Format">Formati data da enumerare.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleName"/> accetta anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// <paramref name="DateFormat"/> include uno dei valori dell'enumerazione <see cref="DateFormat"/> e può includere anche <see cref="LOCALE_USE_CP_ACP"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="CalData"/> o <paramref name="Calendar"/> non sono validi<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_BADDB"/>: la funzione non ha potuto accedere ai dati<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "EnumDateFormatsExEx", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumDateFormats(DateFormatEnumProcedure Callback, string LocaleName, uint DateFormat, IntPtr lParam);

        /// <summary>
        /// Enumera le code page installare o supportate dal sistema operativo.
        /// </summary>
        /// <param name="Callback">Callback che elabora le informazioni.</param>
        /// <param name="Type">Tipo di code page.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Type"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_BADDB"/>: la funzione non ha potuto accedere ai dati</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "EnumSystemCodePagesW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumSystemCodePages(CodePagesEnumProcedure Callback, CodePageType Type);

        /// <summary>
        /// Enumera le località installate o supportate dal sistema operativo.
        /// </summary>
        /// <param name="Callback">Callback che elabora le informazioni.</param>
        /// <param name="LocaleInfo">Valore che identifica le località da enumerare.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <param name="Reserved">Riservato, deve essere <see cref="IntPtr.Zero"/>.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleInfo"/> può usare i seguenti valori in modo singolo o combinati:<br/><br/>
        /// <see cref="LOCALE_ALL"/><br/>
        /// <see cref="LOCALE_ALTERNATE_SORTS"/><br/>
        /// <see cref="LOCALE_NEUTRALDATA"/><br/>
        /// <see cref="LOCALE_SUPPLEMENTAL"/><br/>
        /// <see cref="LOCALE_WINDOWS"/><br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="LocaleInfo"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_BADDB"/>: la funzione non ha potuto accedere ai dati</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "EnumSystemLocalesEx", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumSystemLocales(LocaleEnumProcedure Callback, uint LocaleInfo, IntPtr lParam, IntPtr Reserved);

        /// <summary>
        /// Enumera i formati ora per la località.
        /// </summary>
        /// <param name="Callback">Callback che elabora le informazioni.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Formato ora.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleName"/> accetta anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="LocaleInfo"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido</remarks>
        [DllImport("Kernel32,dll", EntryPoint = "EnumTimeFormatsEx", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumTimeFormats(TimeFormatEnumProcedure Callback, string LocaleName, TimeFormat Flags, IntPtr lParam);

        /// <summary>
        /// Enumera i codici a 2 lettere ISO 3166-1 o i codici numerici UN M.49 per le posizioni geografiche disponibili nel sistema operativo.
        /// </summary>
        /// <param name="Class">Classe della posizione geografica.</param>
        /// <param name="Callback">Callback che elabora le informazioni.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Class"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "EnumSystemGeoNames", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumSystemGeoNames(GEOCLASS Class, GeoNameEnumProcedure Callback, IntPtr lParam);

        /// <summary>
        /// Trova una stringa Unicode o il suo equivalente in un'altra stringa Unicode per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Opzioni di ricerca e filtraggio.</param>
        /// <param name="SourceString">Stringa di origine.</param>
        /// <param name="SourceSize">Dimensione di <paramref name="SourceString"/>, escluso carattere nullo finale.</param>
        /// <param name="StringToFind">Stringa da trovare.</param>
        /// <param name="StringToFindSize">Dimensione di <paramref name="StringToFind"/>, escluso carattere nullo finale.</param>
        /// <param name="FoundStringLength">Lunghezza della stringa trovata.</param>
        /// <param name="VersionInformation">Riservato, deve essere <see cref="IntPtr.Zero"/>.</param>
        /// <param name="Reserved">Riservato, deve essere <see cref=" IntPtr.Zero"/>.</param>
        /// <param name="SortHandle">Riservato, deve essere 0.</param>
        /// <returns>Indice basato su 0 nella stringa di origine se l'operazione è riuscita, -1 altrimenti.</returns>
        /// <remarks>I valori validi per <paramref name="Flags"/> includono i valori nelle seguenti enumerazioni:<br/><br/>
        /// <see cref="SearchOptions"/> (singolo)<br/>
        /// <see cref="ComparisonOptions"/> (singolo)<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_SUCCESS"/>: l'operazione è riuscita ma non ha prodotto risultati.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "FindNLSStringEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int FindNLSString(string LocaleName, uint Flags, string SourceString, int SourceSize, string StringToFind, int StringToFindSize, out int FoundStringLength, IntPtr VersionInformation, IntPtr Reserved, IntPtr SortHandle);

        /// <summary>
        /// Trova una stringa Unicode in un'altra stringa Unicode per un confronto non linguistico.
        /// </summary>
        /// <param name="Options">Opzioni di ricerca.</param>
        /// <param name="SourceString">Stringa di origine.</param>
        /// <param name="SourceStringSize">Dimensione di <paramref name="SourceString"/>, escluso il carattere nullo finale.</param>
        /// <param name="StringToFind">Stringa da trovare.</param>
        /// <param name="StringToFindSize">Dimensione di <paramref name="StringToFindSize"/>, escluso il carattere nullo finale.</param>
        /// <param name="IgnoreCase">Indica se eseguire un confronto ignorando la differenza tra maiuscole e minuscole.</param>
        /// <returns>Indice basato su 0 nella stringa di origine se l'operazione è riuscita, -1 altrimenti.</returns>
        /// <remarks>Questa funzione esegue un confronto binario, non restituisce risultati linguisticamente appropriati.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_SUCCESS"/>: l'operazione è riuscita ma non ha prodotto risultati.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "FindStringOrdinal", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int FindStringOrdinal(SearchOptions Options, string SourceString, int SourceStringSize, string StringToFind, int StringToFindSize, [MarshalAs(UnmanagedType.Bool)] bool IgnoreCase);

        /// <summary>
        /// Mappa una stringa Unicode a un'altra, eseguendo la trasformazione specificata.
        /// </summary>
        /// <param name="Options">Opzioni di trasformazione.</param>
        /// <param name="SourceString">Stringa di origine.</param>
        /// <param name="SourceStringSize">Dimensione di <paramref name="SourceString"/>, escluso il carattere nullo finale.</param>
        /// <param name="DestinationString">Stringa risulato della trasformazione.</param>
        /// <param name="DestinationStringSize">Dimensione di <paramref name="DestinationString"/>.</param>
        /// <returns>Numero di carattere nella stringa risultato della trasformazione, incluso il carattere nullo finale, se l'operazione ha successo, 0 se l'operazione è fallita.</returns>
        /// <remarks><paramref name="SourceStringSize"/> può essere impostato a un valore negativo per specificare che la stringa termina con un carattere nullo, in questo caso la funzione calcola la lunghezza della stringa automaticamente e termina <paramref name="DestinationString"/> con un carattere nullo.<br/><br/>
        /// Se <paramref name="SourceStringSize"/> include lo spazio per il carattere nullo finale, anche <paramref name="DestinationStringSize"/> deve includere lo spazio per esso.<br/>
        /// <paramref name="DestinationStringSize"/> può essere impostato a 0, in questo caso la funzione non usa <paramref name="DestinationString"/> e restituisce la dimensione necessaria del buffer per la stringa.<br/>
        /// Se <paramref name="Options"/> include <see cref="TrasformationOptions.MAP_FOLDDIGITS"/>, il valore restituito è la dimensione massima richiesta anche se l'effettivo numero di caratteri è minore.<br/>
        /// Se <paramref name="DestinationStringSize"/> non viene impostato a tale valore, l'operazione fallisce e viene impostato il codice di errore <see cref="ERROR_INSUFFICIENT_BUFFER"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: dimensione di <paramref name="DestinationString"/> insufficiente oppure è nullo<br/>
        /// <see cref="ERROR_INVALID_DATA"/>: dati non validi<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_MOD_NOT_FOUND"/>: il module non è stato trovato<br/>
        /// <see cref="ERROR_OUTOFMEMORY"/>: memoria insufficiente per completare l'operazione<br/>
        /// <see cref="ERROR_PROC_NOT_FOUND"/>: la procedura richiesta non è stata trovata<br/><br/>
        /// <paramref name="SourceString"/> e <paramref name="DestinationString"/> non posso puntare alla stessa stringa, in questo caso l'operazione fallisce e viene impostato il codice di errore <see cref="ERROR_INVALID_PARAMETER"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "FoldStringW", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int FoldString(TransformationOptions Options, string SourceString, int SourceStringSize, StringBuilder DestinationString, int DestinationStringSize);

        /// <summary>
        /// Recupera l'identificatore della code page ANSI per il sistema operativo.
        /// </summary>
        /// <returns>L'identificatore della code page.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetACP", SetLastError = true)]
        internal static extern uint GetACP();

        /// <summary>
        /// Recupera l'informazione specificata su un calendario per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="CalID">ID del calendario.</param>
        /// <param name="Reserved">Riservato, deve essere nullo.</param>
        /// <param name="CalData">Informazione da recuperare.</param>
        /// <param name="CalDataString">Informazione recuperata come stringa.</param>
        /// <param name="CalDataStringSize">Dimensione, in caratteri, di <paramref name="CalDataString"/>.</param>
        /// <param name="Value">Puntatore all'informazione recuperata come numero intero.</param>
        /// <returns>Numero di caratteri in <paramref name="CalDataString"/> se l'operazione ha successo, 0 in caso contrario.</returns>
        /// <remarks><paramref name="CalData"/> può includere i valori dalle seguenti enumerazioni:<br/><br/>
        /// <see cref="CalendarOptions"/><br/>
        /// <see cref="CalendarData"/> (singolo)<br/><br/>
        /// <paramref name="LocaleName"/> accetta anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// Se <paramref name="CalData"/> include <see cref="CalendarOptions.CAL_RETURN_NUMBER"/>, <paramref name="CalDataString"/> deve essere nullo.<br/><br/>
        /// <paramref name="CalDataStringSize"/> può essere impostato a 0, in questo caso la funzione restituisce la dimensione necessaria di <paramref name="CalDataString"/>.<br/>
        /// Se <paramref name="CalData"/> include <see cref="CalendarOptions.CAL_RETURN_NUMBER"/>, <paramref name="CalDataStringSize"/> deve essere 0.<br/><br/>
        /// Se <paramref name="CalData"/> include <see cref="CalendarOptions.CAL_RETURN_NUMBER"/>, <paramref name="Value"/> non deve essere nullo, se tale valore non è incluso, <paramref name="Value"/> deve essere nullo.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: dimensione di <paramref name="DestinationString"/> insufficiente oppure è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="CalID"/> oppure <paramref name="CalData"/> non sono validi<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// Se <paramref name="CalDataStringSize"/> è impostato a 0 e <paramref name="CalData"/> include <see cref="CalendarOptions.CAL_RETURN_NUMBER"/>, il valore restituito è la dimensione di <paramref name="Value"/>, che ha sempre valore 2.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetCalendarInfoEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetCalendarInfo(string LocaleName, CalendarID CalID, string Reserved, uint CalData, StringBuilder CalDataString, int CalDataStringSize, IntPtr Value);

        /// <summary>
        /// Recupera informazioni su ogni code page installata o disponibile.
        /// </summary>
        /// <param name="CodePage">Code page.</param>
        /// <param name="Flags">Riservato, deve essere 0.</param>
        /// <param name="Info">Struttura <see cref="CPINFO"/> che riceve le informazioni su una code page.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="CodePage"/> può essere impostato a uno dei valori dell'enumerazione <see cref="CodePageDefaultValue"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetCPInfoExW", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCPInfo(uint CodePage, uint Flags, out CPINFO Info);

        /// <summary>
        /// Formatta una stringa numerica come una stringa di valuta per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Opzioni.</param>
        /// <param name="Value">Stringa da formattare.</param>
        /// <param name="Format">Informazioni di formattazione.</param>
        /// <param name="CurrencyString">Stringa formattata.</param>
        /// <param name="CurrencyStringSize">Dimensione, in caratteri, di <paramref name="CurrencyString"/>.</param>
        /// <returns>Numero di caratteri presenti in <paramref name="CurrencyString"/> se l'operazione ha successo, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="Format"/> non è nullo, <paramref name="Flags"/> deve essere 0, in caso contrario, il parametro può includere <see cref="LOCALE_NOUSEROVERRIDE"/>.<br/><br/>
        /// I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// Se <paramref name="CurrencyStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="CurrencyString"/>, incluso il carattere nullo finale.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="CurrencyString"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetCurrencyFormatEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetCurrencyFormat(string LocaleName, uint Flags, string Value, IntPtr Format, StringBuilder CurrencyString, int CurrencyStringSize);

        /// <summary>
        /// Formatta una stringa come data per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Options">Opzioni.</param>
        /// <param name="Date">Puntatore a struttura <see cref="SYSTEMTIME"/> che contiene le informazioni da formattare.</param>
        /// <param name="FormatString">Stringa di formato da usare per formattare la data.</param>
        /// <param name="DateString">Stringa formattata.</param>
        /// <param name="DateStringSize">Dimensione, in caratteri, di <paramref name="DateString"/>.</param>
        /// <param name="Calendar">Riservato, deve essere nullo.</param>
        /// <returns>Numero di caratteri presenti in <paramref name="DateString"/> se l'operazione ha successo, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="FormatString"/> è nullo, <paramref name="Options"/> può includere <see cref="LOCALE_NOUSEROVERRIDE"/> insieme ai valori dell'enumerazione <see cref="DateFormat"/>.<br/><br/>
        /// I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// Se <paramref name="DateStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="DateString"/>, incluso il carattere nullo finale.<br/><br/>
        /// <paramref name="Date"/> può essere nullo per fare in modo che la funzione usi la data locale di sistema.<br/><br/>
        /// <paramref name="FormatString"/> può essere nullo per formattare la stringa in base alla località fornita.<br/>
        /// Se la stringa di formato non è corretta, la funzione forma la stringa formattata migliore possibile al posto di restituire un errore.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="DateString"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetDateFormatEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetDateFormat(string LocaleName, uint Options, IntPtr Date, string FormatString, StringBuilder DateString, int DateStringSize, string Calendar);

        /// <summary>
        /// Formatta una stringa come durata.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Options">Opzioni.</param>
        /// <param name="Duration">Puntatore a struttura <see cref="SYSTEMTIME"/> che contiene le informazioni di durata da formattare.</param>
        /// <param name="DurationValue">Numero di intervalli di 100 nanosecondi nella durata.</param>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="DurationString">Stringa formattata.</param>
        /// <param name="DurationStringSize">Dimensione, in caratteri, di <paramref name="DurationString"/>.</param>
        /// <returns>Numero di caratteri presenti in <paramref name="DurationString"/> se l'operazione ha successo, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="FormatString"/> è nullo, <paramref name="Options"/> può includere il valore <see cref="LOCALE_NOUSEROVERRIDE"/>.<br/><br/>
        /// I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// Se <paramref name="DurationStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="DurationString"/>, incluso il carattere nullo finale.<br/><br/>
        /// <paramref name="Duration"/> può essere nullo per fare in modo che la funzione usi <paramref name="DurationValue"/>.<br/>
        /// Se sia <paramref name="Duration"/> che <paramref name="DurationValue"/> sono impostati,<paramref name="Duration"/> prende priorità.<br/><br/>
        /// <paramref name="FormatString"/> può essere nullo per formattare la stringa in base alla località fornita.<br/>
        /// Se la stringa di formato non è corretta, la funzione forma la stringa formattata migliore possibile al posto di restituire un errore.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="DurationString"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetDurationFormatEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetDurationFormat(string LocaleName, uint Options, IntPtr Duration, ulong DurationValue, string FormatString, StringBuilder DurationString, int DurationStringSize);

        /// <summary>
        /// Recupera informazioni su una località geografica specificata.
        /// </summary>
        /// <param name="Location">Codice a 2 lettere ISO 3166-1 o il codice numerico UN M.49 per la locazione geografica.</param>
        /// <param name="Info">Informazione da recuperare.</param>
        /// <param name="DataString">Informazione recuperata.</param>
        /// <param name="DataStringSize">Dimensione, in caratteri, di <paramref name="DataString"/>.</param>
        /// <returns>Numero di byte dell'informazione che la funzione ha scritto in <paramref name="DataString"/> se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="DataStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="DataString"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="DataString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Info"/> non è valido</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetGeoInfoEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetGeoInfo(string Location, SYSGEOTYPE Info, IntPtr DataString, int DataStringSize);

        /// <summary>
        /// Recupera informazioni su una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Info">Informazione da recuperare.</param>
        /// <param name="DataString">Informazione recuperata.</param>
        /// <param name="DataStringSize">Dimensione, in caratteri, di <paramref name="DataString"/>.</param>
        /// <returns>Numero di caratteri di <paramref name="DataString"/> se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// Se <paramref name="DataStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="DataString"/>, incluso il carattere nullo finale.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="DataString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Info"/> non è valido<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetLocaleInfoEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetLocaleInfo(string LocaleName, uint Info, IntPtr DataString, int DataStringSize);

        /// <summary>
        /// Recupera informazioni sulla versione corrente di una funzionalità NLS per una località.
        /// </summary>
        /// <param name="Function">Funzionalità.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="VersionInfo">Struttura <see cref="NLSVERSIONINFO"/> che conterrà le informazioni.</param>
        /// <returns>true se <paramref name="VersionInfo"/> è valido, false altrimenti.</returns>
        /// <remarks>I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="VersionInfo"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Function"/> non è valido<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetNLSVersionEx", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetNLSVersion(SYSNLS_FUNCTION Function, string LocaleName, ref NLSVERSIONINFO VersionInfo);

        /// <summary>
        /// Formatta una stringa come stringa numerica per una località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Opzioni.</param>
        /// <param name="StringToFormat">Stringa da formattare.</param>
        /// <param name="Format">Puntatore a strutture <see cref="NUMBERFMT"/> che contiene le informazioni di formattazione.</param>
        /// <param name="NumberString">Stringa formattata.</param>
        /// <param name="NumberStringSize">Dimensione, in caratteri, di <paramref name="NumberString"/>.</param>
        /// <returns>Numero di caratteri presenti in <paramref name="NumberString"/> se l'operazione ha successo, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="FormatString"/> è nullo, <paramref name="Flags"/> può includere il valore <see cref="LOCALE_NOUSEROVERRIDE"/>.<br/><br/>
        /// I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// <paramref name="FormatString"/> può contenere solo i seguenti caratteri:<br/><br/>
        /// Caratteri da "0" a "9"<br/>
        /// Un punto decimale se il numero è un valore floating point<br/>
        /// Segno meno nella prima posizione se il numero è negativo<br/><br/>
        /// Se <paramref name="NumberStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="NumberString"/>, incluso il carattere nullo finale.<br/><br/>
        /// <paramref name="Format"/> può essere nullo per formattare la stringa in base alla località fornita.<br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="NumberString"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_OUTOFMEMORY"/>: memoria insufficiente per completare l'operazione<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetNumberFormatEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetNumberFormat(string LocaleName, uint Flags, string StringToFormat, IntPtr Format, StringBuilder NumberString, int NumberStringSize);

        /// <summary>
        /// Recupera l'identificatore della code page OEM per il sistema operativo.
        /// </summary>
        /// <returns>L'identificatore della code page.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetOEMCP", SetLastError = true)]
        internal static extern uint GetOEMCP();

        /// <summary>
        /// Lista gli script usati in una specifica stringa Unicode.
        /// </summary>
        /// <param name="Flags">Opzioni.</param>
        /// <param name="StringToAnalyze">Stringa da analizzare.</param>
        /// <param name="StringToAnalyzeSize">Dimensione, in caratteri, di <paramref name="StringToAnalyze"/>.</param>
        /// <param name="Scripts">Lista di script.</param>
        /// <param name="ScriptsSize">Dimensione, in caratteri, di <paramref name="Scripts"/>.</param>
        /// <returns>Numero di caratteri in <paramref name="Scripts"/>, incluso il carattere nullo finale, se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="StringToAnalyze"/> termina con una carattere nullo, <paramref name="StringToAnalyzeSize"/> può essere impostato a -1.<br/>
        /// Se <paramref name="StringToAnalyzeSize"/> viene impostato a 0, <paramref name="Scripts"/> sarà una stringa vuota e restituisce 1.<br/><br/>
        /// La lista di script in <paramref name="Scripts"/> usa la notaziona ISO 15924 a 4 caratteri.<br/>
        /// Ogni nome di script consiste di 4 caratteri latini, sono in ordine alfabetico e sono tutti seguiti, compreso l'ultimo, da un punto e virgola.<br/><br/>
        /// Se <paramref name="Scripts"/> è nullo e <paramref name="ScriptsSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="Scripts"/>, incluso il carattere nullo finale.<br/><br/>
        /// La funzione restituisce 1 per indicare che non è stato trovato nessuno script.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="Scripts"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_BADDB"/>: la funzione non ha potuto accedere ai dati</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetStringScripts", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetStringScripts(uint Flags, string StringToAnalyze, int StringToAnalyzeSize, StringBuilder Scripts, int ScriptsSize);

        /// <summary>
        /// Recupera informazioni sul tipo di carattere per i caratteri in una stringa.
        /// </summary>
        /// <param name="Locale">Identificatore località.</param>
        /// <param name="Info">Informazione da recuperare.</param>
        /// <param name="SourceString">Stringa da analizzare.</param>
        /// <param name="SourceStringSize">Dimensione, in caratteri, di <paramref name="SourceString"/>.</param>
        /// <param name="CharTypeData">Puntatore a un array di valori a 16 bit.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="Locale"/> non è utilizzato.<br/><br/>
        /// Se <paramref name="SourceStringSize"/> è un numero negativo, la funzione assume che <paramref name="SourceString"/> termina con un carattere nullo e la sua dimensione viene calcolata automaticamente includendo il carattere nullo finale.<br/><br/>
        /// Il numero di elementi dell'array puntato da <paramref name="CharTypeData"/> deve essere pari al valore di <paramref name="SourceStringSize"/> se esso non è negativo, in tal caso, il numero di elementi deve essere pari al numero di caratteri in <paramref name="SourceString"/> + 1.<br/>
        /// Al termine dell'operazione, questo array contiene un valore a 16 bit per ogni carattere in <paramref name="SourceString"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Info"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetStringTypeExW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetStringType(uint Locale, CharTypeInfo Info, string SourceString, int SourceStringSize, IntPtr CharTypeData);

        /// <summary>
        /// Recupera l'identificatore lingua per la località del sistema.
        /// </summary>
        /// <returns>L'identificatore lingua.</returns>
        /// <remarks>La lingua è quella usata per programmi che non supportano Unicode.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetSystemDefaultLangID", SetLastError = true)]
        internal static extern ushort GetSystemDefaultLangID();

        /// <summary>
        /// Recupera il nome della località predefinita di sistema.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="LocaleNameSize">Dimensione, in caratteri, di <paramref name="LocaleName"/>.</param>
        /// <returns>Maggiore di 0 che indica la lunghezza del nome della località, incluso il carattere nullo finale, se l'operazione ha successo, 0 in caso contrario.</returns>
        /// <remarks>La lunghezza massima di <paramref name="LocaleName"/> è <see cref="LOCALE_NAME_MAX_LENGTH"/>, questa è la dimensione raccomandata.<br/><br/>
        /// In caso di errore la funzione può impostare il codice di errore <see cref="ERROR_INSUFFICIENT_BUFFER"/> se la dimensione di <paramref name="LocaleName"/> è insufficiente o è nullo.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetSystemDefaultLocaleName", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetSystemDefaultLocaleName(StringBuilder LocaleName, int LocaleNameSize);

        /// <summary>
        /// Recupera l'identificatore della località corrente per il thread chiamante.
        /// </summary>
        /// <returns>L'identificatore località.</returns>
        /// <remarks>Se la località del thread corrente è personalizzata, la funzione restituisce <see cref="LOCALE_CUSTOM_DEFAULT"/>.<br/>
        /// Se la località è una località personalizzata supplementare, la funzione restituisce <see cref="LOCALE_CUSTOM_UNSPECIFIED"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetThreadLocale", SetLastError = true)]
        internal static extern uint GetThreadLocale();

        /// <summary>
        /// Formatta una stringa come un'ora.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Opzioni.</param>
        /// <param name="Time">Puntatore a struttura <see cref="SYSTEMTIME"/> che contiene le informazioni da formattare.</param>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="TimeString">Stringa formattata.</param>
        /// <param name="TimeStringSize">Dimensione, in caratteri di <paramref name="TimeString"/>.</param>
        /// <returns>Numero di caratteri in <paramref name="TimeString"/> se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>I seguenti valori sono validi per <paramref name="LocaleName"/>:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// I valori dell'enumerazione <see cref="TimeFormat"/> possono essere usati per <paramref name="Flags"/>.<br/>
        /// E' possibile combinare tali valori con <see cref="LOCALE_USE_CP_ACP"/> oppure <see cref="LOCALE_NOUSEROVERRIDE"/>.<br/><br/>
        /// Se <paramref name="Time"/> è nullo, la funzione usa l'ora corrente di sistema.<br/><br/>
        /// Se <paramref name="FormatString"/> è nullo, la funzione formatta la stringa usando le impostazioni della località.<br/><br/>
        /// Se <paramref name="TimeStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="TimeString"/>, incluso il carattere nullo finale, e non usa tale parametro.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="TimeString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_OUTOFMEMORY"/>: memoria insufficiente per completare l'operazione<br/><br/>
        /// Se esiste un segnatempo e <paramref name="Flags"/> non include <see cref="TimeFormat.TIME_NOTIMEMARKER"/>, la funzione localizza il segnatempo in base alla località.<br/><br/>
        /// I valori della struttura puntata da <paramref name="Time"/> devono essere validi, la funzione controlla la loro validità, se uno o più di essi non è valido, l'operazione fallisce e viene impostato il codice di errore <see cref="ERROR_INVALID_PARAMETER"/>.<br/><br/>
        /// Se <paramref name="Flags"/> include <see cref="TimeFormat.TIME_NOMINUTESORSECONDS"/> oppure <see cref="TimeFormat.TIME_NOSECONDS"/>, la funzione rimuove i separatori che seguono i minuti e/o i secondi.<br/><br/>
        /// Se <paramref name="Flags"/> include <see cref="TimeFormat.TIME_NOTIMEMARKER"/>, la funzione rimuove i separatori che precedono e che seguono il segnatempo.<br/><br/>
        /// Se <paramref name="Flags"/> include <see cref="TimeFormat.TIME_FORCE24HOURFORMAT"/>, la funzione include nella stringa il segnatempo a meno che <paramref name="Flags"/> non includa anche <see cref="TimeFormat.TIME_NOTIMEMARKER"/>.<br/><br/>
        /// I millisecondi non sono inclusi nella stringa formattata.<br/><br/>
        /// Se <paramref name="FormatString"/> non è corretto, la funzione forma la migliore stringa formattata possibile al posto di restituire un errore.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetTimeFormatEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetTimeFormat(string LocaleName, uint Flags, IntPtr Time, string FormatString, StringBuilder TimeString, int TimeStringSize);

        /// <summary>
        /// Recupera il codice ISO 3166-1 a due lettere o il codice numerico UN M.49 per la località geografica predefinita dell'utente.
        /// </summary>
        /// <param name="GeoName">Codice ISO 3166-1 o il codice numerico UN M.49 per la località geografica.</param>
        /// <param name="GeoNameSize">Dimensione, in caratteri, di <paramref name="GeoName"/>.</param>
        /// <returns>Numero di caratteri copiati in <paramref name="GeoName"/> se l'operazione è riuscita, 0 in caso contrario.</returns>
        /// <remarks>Se <paramref name="GeoNameSize"/> è impostato a 0, la funzione restituisce il numero di caratteri che sarebbero stati copiati in <paramref name="GeoName"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_BADDB"/>: la funzione non ha potuto leggere l'informazione dal registro<br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="GeoName"/> è insufficiente.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetUserDefaultGeoName", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetUserDefaultGeoName(StringBuilder GeoName, int GeoNameSize);

        /// <summary>
        /// Recupera l'identificatore lingua delle impostazioni regionali per l'utente corrente.
        /// </summary>
        /// <returns>L'identificatore della lingua.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "GetUserDefaultLangID", SetLastError = true)]
        internal static extern ushort GetUserDefaultLangID();

        /// <summary>
        /// Recupera il nome della località predefinita dell'utente.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="LocaleNameSize">Dimensione, in caratteri, di <paramref name="LocaleName"/>.</param>
        /// <returns>La dimensione di <paramref name="LocaleName"/>, incluso il carattere nullo finale, se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>La dimensione massima di <paramref name="LocaleName"/> è di <see cref="LOCALE_NAME_MAX_LENGTH"/>, tale dimensione è raccomandata.<br/><br/>
        /// In caso di errore la funzione può impostare il codice di errore <see cref="ERROR_INSUFFICIENT_BUFFER"/> se la dimensione di <paramref name="LocaleName"/> non è sufficiente o è nullo.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetUserDefaultLocaleName", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int GetUserDefaultLocaleName(StringBuilder LocaleName, int LocaleNameSize);

        /// <summary>
        /// Converte un nome di dominio internazionalizzato (IDN) o un'altra etichetta internazionalizzata a una rappresentazione Unicode della stringa ASCII che rappresenta il nome nella sintassi di trasferimento codifica Punycode.
        /// </summary>
        /// <param name="Options">Opzioni di conversione.</param>
        /// <param name="IdnUnicodeString">Stringa Unicode rappresentante un IDN o un'altra etichetta internazionalizzata.</param>
        /// <param name="IdnUnicodeStringSize">Numero di caratteri in <paramref name="IdnUnicodeString"/>.</param>
        /// <param name="IdnAsciiString">Stringa Unicode che consiste solo di caratteri compresi nel set di caratteri ASCII.</param>
        /// <param name="IdnAsciiStringSize">Dimensione di <paramref name="IdnAsciiString"/>.</param>
        /// <returns>Numero di caratteri in <paramref name="IdnAsciiString"/> se l'operazione ha successo, 0 altrimenti.</returns>
        /// <remarks>Al termine dell'operazione <paramref name="IdnAsciiString"/> contiene l'equivalente ASCII di <paramref name="IdnUnicodeString"/> sotto Punycode.<br/><br/>
        /// Se <paramref name="IdnAsciiString"/> è nullo e <paramref name="IdnAsciiStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="IdnAsciiString"/>, incluso il carattere nullo finale se esiste in <paramref name="IdnUnicodeString"/>.<br/><br/>
        /// <paramref name="IdnAsciiString"/> termina con un carattere nullo solo se <paramref name="IdnUnicodeString"/> termina anch'essa con un carattere nullo.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="IdnAsciiString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_NAME"/>: un nome non valido è stato fornito alla funzione, utilizzato per tutti gli errori di sintassi<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>:uno dei parametri non è valido <br/>
        /// <see cref="ERROR_NO_UNICODE_TRANSLATION"/>: Unicode non valido trovato nella stringa<br/><br/>
        /// <paramref name="IdnAsciiString"/> non termina con un carattere nullo se <paramref name="IdnUnicodeStringSize"/> non ne specifica la presenza.<br/>
        /// Per aggiungere un carattere nullo finale a <paramref name="IdnAsciiString"/>, <paramref name="IdnUnicodeStringSize"/> dovrebbe essere impostato a -1 o il carattere nullo finale deve essere incluso nel conteggio.<br/><br/>
        /// L'operazione non riesce se <paramref name="IdnUnicodeString"/> contiene i caratteri Unicode di controllo, il carattere "cancella" o il carattere nullo in una posizione diversa da quella finale.</remarks>
        [DllImport("Normaliz.dll", EntryPoint = "IdnToAscii", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int IdnToAscii(IdnConversionOptions Options, string IdnUnicodeString, int IdnUnicodeStringSize, StringBuilder IdnAsciiString, int IdnAsciiStringSize);

        /// <summary>
        /// Converte un nome di dominio internazionalizzato (IDN) o un'altra etichetta internazionalizzata alla forma Nameprep specificata dal Network Working Group RFC 3491 senza effettuare la conversione a Punycode.
        /// </summary>
        /// <param name="Options">Opzioni di conversione.</param>
        /// <param name="IdnUnicodeString">Stringa Unicode rappresentante un IDN o un'altra etichetta internazionalizzata.</param>
        /// <param name="IdnUnicodeStringSize">Numero di caratteri in <paramref name="IdnUnicodeString"/>.</param>
        /// <param name="NameprepString">Stringa Unicode risultato della conversione tramite Nameprep.</param>
        /// <param name="NameprepStringSize">Dimensione, in caratteri, di <paramref name="NameprepString"/>.</param>
        /// <returns>Numero di caratteri in <paramref name="NameprepString"/> se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>Se <paramref name="NameprepString"/> è nullo e <paramref name="NameprepStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="NameprepString"/>, incluso il carattere nullo finale se esiste in <paramref name="IdnUnicodeString"/>.<br/><br/>
        /// <paramref name="NameprepString"/> termina con un carattere nullo solo se <paramref name="IdnUnicodeString"/> termina anch'essa con un carattere nullo.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="NameprepString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_NAME"/>: un nome non valido è stato fornito alla funzione, utilizzato per tutti gli errori di sintassi<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>:uno dei parametri non è valido <br/>
        /// <see cref="ERROR_NO_UNICODE_TRANSLATION"/>: Unicode non valido trovato nella stringa<br/><br/>
        /// <paramref name="NameprepString"/> non termina con un carattere nullo se <paramref name="IdnUnicodeStringSize"/> non ne specifica la presenza.<br/>
        /// Per aggiungere un carattere nullo finale a <paramref name="NameprepString"/>, <paramref name="IdnUnicodeStringSize"/> dovrebbe essere impostato a -1 o il carattere nullo finale deve essere incluso nel conteggio.<br/><br/>
        /// L'operazione non riesce se <paramref name="IdnUnicodeString"/> contiene i caratteri Unicode di controllo, il carattere "cancella" o il carattere nullo in una posizione diversa da quella finale.</remarks>
        [DllImport("Normaliz.dll", EntryPoint = "IdnToNameprepUnicode", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int IdnToNameprepUnicode(IdnConversionOptions Options, string IdnUnicodeString, int IdnUnicodeStringSize, StringBuilder NameprepString, int NameprepStringSize);

        /// <summary>
        /// Converte la forma Punycode di un nome di dominio internazionalizzato (IDN) o un'altra etichetta internazionalizzata a una normale stringa Unicode UTF-16.
        /// </summary>
        /// <param name="Options">Opzioni di conversione.</param>
        /// <param name="ASCIIPunycodeString">Stringa in codifica Punycode di un IDN o di un'altra etichetta internazionalizzata.</param>
        /// <param name="ASCIIPunycodeStringSize">Numero di caratteri in <paramref name="ASCIIPunycodeString"/>.</param>
        /// <param name="UnicodeString">Stringa Unicode equivalente a <paramref name="ASCIIPunycodeString"/>.</param>
        /// <param name="UnicodeStringSize">Dimensione, in caratteri, di <paramref name="UnicodeString"/>.</param>
        /// <returns>Numero di caratteri in <paramref name="UnicodeString"/> se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="ASCIIPunycodeString"/> deve consistere solo di caratteri ASCII, può includere Unicode codificato tramite Punycode.<br/><br/>
        /// Se <paramref name="UnicodeString"/> è nullo e <paramref name="UnicodeStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria di <paramref name="UnicodeString"/>, incluso il carattere nullo finale se esiste in <paramref name="ASCIIPunycodeString"/>.<br/><br/>
        /// <paramref name="UnicodeString"/> termina con un carattere nullo solo se <paramref name="ASCIIPunycodeString"/> termina anch'essa con un carattere nullo.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="UnicodeString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Options"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_NAME"/>: un nome non valido è stato fornito alla funzione, utilizzato per tutti gli errori di sintassi<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>:uno dei parametri non è valido <br/>
        /// <see cref="ERROR_NO_UNICODE_TRANSLATION"/>: Unicode non valido trovato nella stringa<br/><br/>
        /// <paramref name="UnicodeString"/> non termina con un carattere nullo se <paramref name="UnicodeStringSize"/> non ne specifica la presenza.<br/>
        /// Per aggiungere un carattere nullo finale a <paramref name="UnicodeString"/>, <paramref name="ASCIIPunycodeString"/> dovrebbe essere impostato a -1 o il carattere nullo finale deve essere incluso nel conteggio.<br/><br/>
        /// L'operazione non riesce se <paramref name="ASCIIPunycodeString"/> contiene i caratteri Unicode di controllo, il carattere "cancella" o il carattere nullo in una posizione diversa da quella finale.</remarks>
        [DllImport("Normaliz.dll", EntryPoint = "IdnToUnicode", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int IdnToUnicode(IdnConversionOptions Options, string ASCIIPunycodeString, int ASCIIPunycodeStringSize, StringBuilder UnicodeString, int UnicodeStringSize);

        /// <summary>
        /// Determina se ogni carattere in una stringa ha un risultato definito per una specificata funzionalità NLS.
        /// </summary>
        /// <param name="Function">Funzionalità.</param>
        /// <param name="Flags">Opzioni, deve essere 0.</param>
        /// <param name="VersionInformation">Puntatore a struttura <see cref="NLSVERSIONINFO"/> che contiene informazioni sulla versione.</param>
        /// <param name="StringToVerify">Stringa da esaminare.</param>
        /// <param name="StringToVerifySize">Numero di caratteri in <paramref name="StringToVerify"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="VersionInformation"/> può essere nullo per usare la versione corrente.<br/><br/>
        /// <paramref name="StringToVerifySize"/> può includere il carattere nullo finale, la sua inclusione non influenza il controllo perché è sempre definito.<br/>
        /// Impostarlo a -1 indica che la stringa termina con un carattere nullo, in questo caso, la funzione calcola la lunghezza della stringa.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: dimensione del buffer insufficiente o nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "IsNLSDefinedString", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsNLSDefinedString(SYSNLS_FUNCTION Function, uint Flags, IntPtr VersionInformation, string StringToVerify, int StringToVerifySize);

        /// <summary>
        /// Verifica che una stringa è normalizzata secondo Unicode 4.0 TR#15.
        /// </summary>
        /// <param name="NormalizationForm">Forma di normalizzazione.</param>
        /// <param name="StringToTest">Stringa da controllare.</param>
        /// <param name="StringToTestSize">Lunghezza, in caratteri, di <paramref name="StringToTest"/>, incluso il carattere nullo finale.</param>
        /// <returns>true se <paramref name="StringToTest"/> è già normalizzata nella forma appropriata, false altrimenti.</returns>
        /// <remarks>Se <paramref name="StringToTestSize"/> ha valore -1, la funzione assume che la stringa termina con un carattere nullo e ne calcola la lunghezza automaticamente.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_NO_UNICODE_TRANSLATION"/>: Unicode non valido in <paramref name="StringToTest"/><br/>
        /// <see cref="ERROR_SUCCESS"/>: l'operazione è completata ma non ha prodotto risultati<br/><br/>
        /// Per determinare in modo affidabile una condizione di errore, utilizzare <see cref="SetLastError"/> con <see cref="ERROR_SUCCESS"/> come codice di errore prima di chiamare questa funzione.</remarks>
        [DllImport("Normaliz.dll", EntryPoint = "IsNormalizedString", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsNormalizedString(NORM_FORM NormalizationForm, string StringToTest, int StringToTestSize);

        /// <summary>
        /// Determina se una code page è valida.
        /// </summary>
        /// <param name="CodePage">Code page da controllare.</param>
        /// <returns>Diverso da 0 se la code page è valida, 0 altrimenti.</returns>
        /// <remarks>Una code page è considerata valida sono se installata nel sistema operativo.<br/>
        /// Unicode è consigliato.<br/>
        /// A partire da Windows Vista tutte le code page che possono essere installate sono caricate per impostazione predefinita.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "IsValidCodePage", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsValidCodePage(uint CodePage);

        /// <summary>
        /// Determina se uno specifico nome località è valida per una località installata o supportata nel sistema operativo.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <returns>Diverso da 0 se il nome località è valida, 0 altrimenti.</returns>
        /// <remarks>A partire da Windows Vista tutte le località supportate dovrebbero essere installate su tutti i sistemi operativi.<br/><br/>
        /// Questa funzione può gestire il nome di una località personalizzata.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "IsValidLocaleName", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsValidLocaleName(string LocaleName);

        /// <summary>
        /// Determina se la versione NLS è valida per una determinata funzionalità.
        /// </summary>
        /// <param name="Function">Funzionalità.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="VersionInformation">Struttura <see cref="NLSVERSIONINFO"/> con le informazioni di versione.</param>
        /// <returns>Diverso da 0 è la versione NLS è valida, 0 altrimenti.</returns>
        /// <remarks>Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "IsValidNLSVersion", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint IsValidNLSVersion(SYSNLS_FUNCTION Function, string LocaleName, ref NLSVERSIONINFO VersionInformation);

        /// <summary>
        /// Converte un identificatore località in un nome località.
        /// </summary>
        /// <param name="LocaleID">Identificatore località.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="LocaleNameSize">Dimensione, in caratteri, di <paramref name="LocaleName"/>.</param>
        /// <param name="Flags">Opzioni.</param>
        /// <returns>Numero di caratteri, incluso il carattere nullo finale, di <paramref name="LocaleName"/> se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleID"/> può accettare anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_INVARIANT"/><br/>
        /// <see cref="LOCALE_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_USER_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UI_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UNSPECIFIED"/><br/><br/>
        /// <paramref name="LocaleName"/> può avere i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// La dimensione massima di <paramref name="LocaleName"/> è <see cref="LOCALE_NAME_MAX_LENGTH"/>, questo valore è quello raccomandato.<br/><br/>
        /// Se <paramref name="LocaleNameSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria, incluso il carattere nullo finale, per <paramref name="LocaleName"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="LocaleName"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "LCIDToLocaleName", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LCIDToLocaleName(uint LocaleID, StringBuilder LocaleName, int LocaleNameSize, uint Flags);

        /// <summary>
        /// Mappa i caratteri di una stringa in input usando la trasformazione specificata, o genera una chiave di ordinamento.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Options">Opzioni.</param>
        /// <param name="SourceString">Stringa originale.</param>
        /// <param name="SourceStringSize">Dimensione, in caratteri, di <paramref name="SourceString"/>.</param>
        /// <param name="DestinationString">Stringa risultato della mappatura.</param>
        /// <param name="DestinationStringSize">Dimensione, in caratteri, di <paramref name="DestinationString"/>.</param>
        /// <param name="VersionInformation">Struttura <see cref="NLSVERSIONINFO"/> che contiene le informazioni di versione.</param>
        /// <param name="Reserved">Riservato, deve essere nullo.</param>
        /// <param name="SortHandle">Riservato, deve essere 0.</param>
        /// <returns>Il numero di caratteri in <paramref name="DestinationString"/> oppure il numero di byte nella chiave di ordinamento, se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleName"/> può avere anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// <paramref name="Options"/> può includere i valori dalle seguenti enumerazioni:<br/><br/>
        /// <see cref="MappingOptions"/><br/>
        /// <see cref="ComparisonOptions"/><br/>
        /// <see cref="SortingOptions"/><br/><br/>
        /// <paramref name="SourceString"/> non può essere una stringa vuota.<br/><br/>
        /// <paramref name="SourceStringSize"/> può includere il carattere nullo finale ma non è necessario, se è incluso il comportamento della funzione non ne è influenzato.<br/>
        /// Questo parametro può essere impostato a un numero negativo per indicare che <paramref name="SourceString"/> termina con un carattere nullo, in questo caso, se la funzione viene utilizzata per la mappatura di <paramref name="SourceString"/>, la dimensione viene calcolata automaticamente e <paramref name="DestinationString"/> termina con un carattere nullo.<br/>
        /// Questo parametro non può essere impostato a 0.<br/><br/>
        /// Se la funzione viene usata per generare una chiave di ordinamento:<br/><br/>
        /// <paramref name="DestinationString"/> deve essere trattato come un generico array di byte<br/>
        /// <paramref name="DestinationString"/> può avere un numero di byte dispari<br/><br/>
        /// Se il chiamante richiede una parte di <paramref name="SourceString"/>, <paramref name="DestinationString"/> non include il carattere nullo finale a meno che non sia incluso in <paramref name="DestinationStringSize"/>.<br/><br/>
        /// In caso di errore, <paramref name="DestinationString"/> può contenere risultati parziali o anche nessun risultato, il suo contenuto deve essere considerato non valido.<br/><br/>
        /// Se la funzione viene usata per la mappatura stringa, <paramref name="DestinationStringSize"/> indica il numero di caratteri.<br/>
        /// Se <paramref name="SourceStringSize"/> include lo spazio per un carattere nullo finale anche <paramref name="DestinationStringSize"/> deve includerlo.<br/><br/>
        /// Se la funzione viene usata per generare una chiave di ordinamento, <paramref name="DestinationStringSize"/> indica il numero di byte, questo conteggio deve includere il byte 0 finale.<br/><br/>
        /// <paramref name="DestinationStringSize"/> può essere impostato a 0, in tal caso la funzione non utilizza <paramref name="DestinationString"/> e restituisce la dimensione necessaria di tale parametro.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="DestinationString"/> è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="MappingOptions"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "LCMapStringEx", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int LCMapString(string LocaleName, uint Options, string SourceString, int SourceStringSize, IntPtr DestinationString, int DestinationStringSize, ref NLSVERSIONINFO VersionInformation, IntPtr Reserved, IntPtr SortHandle);

        /// <summary>
        /// Converte un nome località in un identificatore località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="Flags">Opzioni.</param>
        /// <returns>L'identificatore località corrispondente se l'operazione è riuscita, 0 in caso contrario.</returns>
        /// <remarks><paramref name="LocaleName"/> può avere anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_NAME_INVARIANT"/><br/>
        /// <see cref="LOCALE_NAME_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_NAME_USER_DEFAULT"/><br/><br/>
        /// <paramref name="Flags"/> può essere impostato a <see cref="LOCALE_ALLOW_NEUTRAL_NAMES"/> per permettere la restituzione di un LCID neutrale.<br/><br/>
        /// Se <paramref name="LocaleName"/> corrisponde a una località personalizzata che è predefinita per l'utente, la funzione restituisce <see cref="LOCALE_CUSTOM_DEFAULT"/>.<br/><br/>
        /// Se <paramref name="LocaleName"/> corrisponde a una località personalizzata che non è predefinita per l'utente, la funzione restituisce <see cref="LOCALE_CUSTOM_UNSPECIFIED"/>.<br/><br/>
        /// Se l'applicazione passa tag lingua a questa funzione dallo spazio dei nomi Windows.Globalization, questi devono prima essere convertiti usando <see cref="ResolveLocaleName"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "LocaleNameToLCID", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint LocaleNameToLCID(string LocaleName, uint Flags);

        /// <summary>
        /// Normalizza i caratteri di una stringa secondo Unicode 4.0 TR#15.
        /// </summary>
        /// <param name="NormalizationForm">Forma di normalizzazione.</param>
        /// <param name="SourceString">Stringa originale.</param>
        /// <param name="SourceStringSize">Dimensione, in caratteri, di <paramref name="SourceString"/>.</param>
        /// <param name="DestinationString">Stringa normalizzata.</param>
        /// <param name="DestinationStringSize">Dimensione, in caratteri, di <paramref name="DestinationString"/>.</param>
        /// <returns>Lunghezza della stringa normalizzata in <paramref name="DestinationString"/>, se l'operazione è riuscita, minore o uguale a 0 in caso contrario.</returns>
        /// <remarks><paramref name="SourceStringSize"/> può essere impostato a -1 per fare in modo che la funzione assuma che <paramref name="SourceString"/> termini con un carattere nullo e ne calcoli la lunghezza automaticamente.<br/><br/>
        /// <paramref name="DestinationString"/> non termina con un carattere nullo se <paramref name="SourceStringSize"/> non ne specifica la presenza in <paramref name="SourceString"/>.<br/>
        /// Impostare a -1 <paramref name="SourceStringSize"/> o includere il caratter nullo finale nel conteggio per fare in modo che <paramref name="DestinationString"/> termini con un carattere nullo.<br/><br/>
        /// Se <paramref name="DestinationString"/> è nullo e <paramref name="DestinationStringSize"/> è impostato a 0, la funzione restituisce la dimensione necessaria per <paramref name="DestinationString"/>.<br/><br/>
        /// In caso di errore la funzione può restituire i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione di <paramref name="DestinationString"/> non è sufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_NO_UNICODE_TRANSLATION"/>: Unicode non valido trovato in <paramref name="SourceString"/>, la funzione restituisce il valore negativo dell'indice dove si trova l'errore<br/>
        /// <see cref="ERROR_SUCCESS"/>: l'operazione è stata completata ma non ha prodotto risultati</remarks>
        [DllImport("Normaliz.dll", EntryPoint = "NormalizeString", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int NormalizeString(NORM_FORM NormalizationForm, string SourceString, int SourceStringSize, StringBuilder DestinationString, int DestinationStringSize);

        /// <summary>
        /// Trova un possibile nome località per un nome fornito.
        /// </summary>
        /// <param name="NameToResolve">Nome da risolvere.</param>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="LocaleNameSize">Dimensione, in caratteri, di <paramref name="LocaleName"/>.</param>
        /// <returns>Dimensione di <paramref name="LocaleName"/>, incluso il carattere nullo finale se l'operazione è riuscita, 0 in caso contrario.</returns>
        /// <remarks>La dimensione massima di <paramref name="LocaleName"/> è <see cref="LOCALE_NAME_MAX_LENGTH"/>, questo valore è la dimensione raccomandata.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "ResolveLocaleName", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int ResolveLocaleName(string NameToResolve, StringBuilder LocaleName, int LocaleNameSize);

        /// <summary>
        /// Imposta un informazione per un calendario.
        /// </summary>
        /// <param name="LocaleID">ID località.</param>
        /// <param name="Calendar">Identificatore calendario.</param>
        /// <param name="Data">Informazione da impostare.</param>
        /// <param name="Value">Valore dell'informazione.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleID"/> può avere anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_INVARIANT"/><br/>
        /// <see cref="LOCALE_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_USER_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UI_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UNSPECIFIED"/><br/><br/>
        /// L'unico valore valido per <paramref name="Data"/> è <see cref="CalendarData.CAL_ITWODIGITYEARMAX"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INTERNAL_ERROR"/>: si è verificato un errore inaspettato<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Data"/> o <paramref name="Calendar"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// La funzione influenza solo le impostazioni utente, non imposta le impostazioni predefinite di sistema.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetCalendarInfoW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetCalendarInfo(uint LocaleID, CalendarID Calendar, CalendarData Data, string Value);

        /// <summary>
        /// Imposta un informazione sulla località.
        /// </summary>
        /// <param name="LocaleID">ID località.</param>
        /// <param name="LocaleData">Informazione da impostare.</param>
        /// <param name="DataValue">Valore informazione.</param>
        /// <returns>Diverso da 0 se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks><paramref name="LocaleID"/> può avere anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_INVARIANT"/><br/>
        /// <see cref="LOCALE_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_USER_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UI_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UNSPECIFIED"/><br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_ACCESS_DISABLED_BY_POLICY"/>: criteri di gruppo impedisce l'esecuzione di questa operazione<br/>
        /// <see cref="ERROR_INVALID_ACCESS"/>: codice di accesso non valido<br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="LocaleData"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/><br/>
        /// La funzione influenza solo le impostazioni utente, non imposta le impostazioni predefinite di sistema.<br/><br/>
        /// Questa funzione influenza il comportamento delle altre applicazioni.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetLocaleInfoW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetLocaleInfo(uint LocaleID, uint LocaleData, string DataValue);

        /// <summary>
        /// Imposta la località del thread corrente
        /// </summary>
        /// <param name="LocaleID">Identificatore località.</param>
        /// <returns>L'identificatore località precedente, se l'operazione è riuscita.</returns>
        /// <remarks><paramref name="LocaleID"/> può avere anche i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_INVARIANT"/><br/>
        /// <see cref="LOCALE_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_USER_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UI_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UNSPECIFIED"/></remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetThreadLocale", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetThreadLocale(uint LocaleID);

        /// <summary>
        /// Imposta la posizione geografica dell'utente corrente al codice ISO 3166-1 a due lettere o al codice numerico UN M.49.
        /// </summary>
        /// <param name="GeoName">Codice ISO 3166-1 o il codice numerico UN M.49 per la posizione geografica dell'utente corrente.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_ACCESS_DISABLED_BY_POLICY"/>: criteri di gruppo impedisce l'esecuzione di questa operazione<br/>
        /// <see cref="ERROR_INTERNAL_ERROR"/>: si è verificato un errore inaspettato<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: <paramref name="GeoName"/> non è valido<br/><br/>
        /// La funzione influenza solo le impostazioni utente, non imposta le impostazioni predefinite di sistema.<br/><br/>
        /// Questa funzione influenza il comportamento delle altre applicazioni.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetUserGeoName", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetUserGeoName(string GeoName);

        /// <summary>
        /// Confronta due lista enumerate di script.
        /// </summary>
        /// <param name="Flags">Opzioni di verifica.</param>
        /// <param name="LocaleScripts">Lista enumerata di script per una località.</param>
        /// <param name="LocaleScriptsSize">Dimensione, in caratteri, di <paramref name="LocaleScripts"/>.</param>
        /// <param name="TestScripts">Lista di script enumerata da testare.</param>
        /// <param name="TestScriptsSize">Dimensione, in caratteri, di <paramref name="TestScripts"/>.</param>
        /// <returns>true se <paramref name="TestScripts"/> non è vuoto e tutti i suoi elementi sono inclusi in <paramref name="LocaleScripts"/>, false in altri casi.</returns>
        /// <remarks><paramref name="LocaleScriptsSize"/> e <paramref name="TestScriptsSize"/> possono avere valore -1 se le relative stringhe terminano con un carattere nullo.<br/>
        /// Nessuno dei due parametri può essere impostato a 0.<br/><br/>
        /// La funzione restituisce true anche se <paramref name="TestScripts"/> contiene più script di <paramref name="LocaleScripts"/>, sempre che tutti gli script di test siano contenuti in <paramref name="LocaleScripts"/>.<br/><br/>
        /// Se <paramref name="Flags"/> è impostato a <see cref="VS_ALLOW_LATIN"/>, la funzione si comporta come se "Latn;" si trova in <paramref name="TestScripts"/>.<br/><br/>
        /// La funzione restituisce false anche in caso di errore, per verificare la riuscita dell'operazione recuperare l'ultimo codice di errore che può essere anche uno dei seguenti:<br/><br/>
        /// <see cref="ERROR_INVALID_FLAGS"/>: <paramref name="Flags"/> non è valido<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_SUCCESS"/>: l'operazione è stata completata ma non ha prodotto risultati.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "VerifyScripts", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool VerifyScripts(uint Flags, string LocaleScripts, int LocaleScriptsSize, string TestScripts, int TestScriptsSize);
    }
}