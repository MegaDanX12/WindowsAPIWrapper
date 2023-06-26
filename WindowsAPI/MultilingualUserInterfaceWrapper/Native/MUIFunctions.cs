using System;
using System.Runtime.InteropServices;
using System.Text;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUICallbacks;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIEnumerations;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIStructures;
using static WindowsAPI.MultilingualUserInterfaceWrapper.Native.MUIConstants;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;
using static WindowsAPI.ErrorHandlingWrapper.Native.Win32ErrorConstants;

namespace WindowsAPI.MultilingualUserInterfaceWrapper.Native
{
    /// <summary>
    /// Funzioni relative alle funzionalità MUI.
    /// </summary>
    internal static class MUIFunctions
    {
        /// <summary>
        /// Enumera le lingue dell'interfaccia utente disponibili nel sistema operativo e chiama la funzione specificata per ognuna di esse.
        /// </summary>
        /// <param name="EnumerationProcedure">Funzione da chiamare.</param>
        /// <param name="Format">Formato delle lingue restituite.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("Kernel32.dll", EntryPoint = "EnumUILanguagesW", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnumUILanguages(UILanguageEnumProcedure EnumerationProcedure, LanguageFormat Format, IntPtr lParam);

        /// <summary>
        /// Recupera le informazioni relative alle risorse di un file.
        /// </summary>
        /// <param name="Info">Informazioni da recuperare.</param>
        /// <param name="FilePath">Percorso al file.</param>
        /// <param name="MUIInfoPointer">Puntatore a struttura <see cref="FILEMUIINFO"/> e al buffer che la segue.</param>
        /// <param name="MUIInfoSize">Dimensione del buffer, in byte, indicato da <paramref name="MUIInfoPointer"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="MUIInfoPointer"/> è nullo e <paramref name="MUIInfoSize"/> è impostato a 0, la funzione recupera la dimensione necessaria del buffer in <paramref name="MUIInfoSize"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetFileMUIInfo", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetFileMUIInfo(MUIFileInfo Info, string FilePath, IntPtr MUIInfoPointer, ref uint MUIInfoSize);

        /// <summary>
        /// Recupera i percorsi per tutti i file di risorse specifici della lingua associato con un file neutrale fornito.
        /// </summary>
        /// <param name="Flags">Opzioni di ricerca.</param>
        /// <param name="FilePath">Percorso del file.</param>
        /// <param name="Language">Lingua.</param>
        /// <param name="LanguageSize">Dimensione, in caratteri, di <paramref name="Language"/>.</param>
        /// <param name="FileMUIPath">Percorso al file di risorse specifico della lingua.</param>
        /// <param name="MUIPathSize">Dimensione, in caratteri, di <paramref name="FileMUIPath"/>.</param>
        /// <param name="Enumerator">Variabile di enumerazione.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti</returns>
        /// <remarks>Questa funzione deve essere chiamata più volte per trovare tutti i file.<br/><br/>
        /// <paramref name="Flags"/> è un valore singolo formato dall'unione delle seguenti enumerazioni:<br/><br/>
        /// <see cref="LanguageFormat"/><br/>
        /// <see cref="SearchFilter"/><br/>
        /// <see cref="MUIPEFileType"/><br/><br/>
        /// <paramref name="FilePath"/> può specificare qualunque file, sono supportati i percorsi di rete.<br/><br/>
        /// <paramref name="Language"/> può indicare la lingua per la quale cercare i file di risorse.<br/><br/>
        /// <paramref name="Enumerator"/> deve essere inizialmente impostato a 0.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione del buffer è insufficiente o è nullo<br/>
        /// <see cref="ERROR_NO_MORE_FILES"/>: non ci sono più file da elaborare</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetFileMUIPath", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetFileMUIPath(uint Flags, string FilePath, StringBuilder Language, ref uint LanguageSize, StringBuilder FileMUIPath, ref uint MUIPathSize, ref ulong Enumerator);

        /// <summary>
        /// Recupera le lingue preferite del processo.
        /// </summary>
        /// <param name="Format">Formato della lingua.</param>
        /// <param name="LanguagesCount">Numero di lingue recuperate.</param>
        /// <param name="LanguagesBuffer">Puntatore a una lista di stringhe a terminazione nulla.</param>
        /// <param name="LanguagesBufferSize">Dimensione, in caratteri, di <paramref name="LanguagesBuffer"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="LanguagesBuffer"/> è nullo e <paramref name="LanguagesBufferSize"/> è impostato a 0, la funzione imposta <paramref name="LanguagesBufferSize"/> alla dimensione necessaria di <paramref name="LanguagesBuffer"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione del buffer è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido.<br/><br/>
        /// Se la lista di lingue preferite dal processo è vuota o se le lingue specificate non sono valide, <paramref name="LanguagesBuffer"/> è una stringa vuota e <paramref name="LanguagesBufferSize"/> è impostato a 2.<br/><br/>
        /// Se <paramref name="Format"/> ha valore <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, la funzione recuperare gli identificatore esadecimali senza prefisso 0x e una lunghezza di 4 caratteri.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetProcessPreferredUILanguages", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetProcessPreferredUILanguages(LanguageFormat Format, out uint LanguagesCount, IntPtr LanguagesBuffer, ref uint LanguagesBufferSize);

        /// <summary>
        /// Recupera le lingue preferite del sistema.
        /// </summary>
        /// <param name="Options">Opzioni di enumerazione.</param>
        /// <param name="LanguagesCount">Numero di lingue recuperate.</param>
        /// <param name="LanguagesBuffer">Puntatore a una lista di stringhe a terminazione nulla.</param>
        /// <param name="LanguagesBufferSize">Dimensione, in caratteri, di <paramref name="LanguagesBuffer"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="Options"/> può includere uno dei valori dell'enumerazione <see cref="LanguageFormat"/> e la costante <see cref="MUI_MACHINE_LANGUAGE_SETTINGS"/>.<br/><br/>
        /// Se <paramref name="LanguagesBuffer"/> è nullo e <paramref name="LanguagesBufferSize"/> è impostato a 0, la funzione imposta <paramref name="LanguagesBufferSize"/> alla dimensione necessaria di <paramref name="LanguagesBuffer"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione del buffer è insufficiente o è nullo<br/><br/>
        /// Se l'operazione non riesce per qualunque altro motivo, <paramref name="LanguagesCount"/> e <paramref name="LanguagesBuffer"/> non sono definiti.<br/><br/>
        /// Se <paramref name="Options"/> include <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, la funzione recuperare gli identificatore esadecimali senza prefisso 0x e una lunghezza di 4 caratteri.<br/><br/>
        /// La lista di lingue preferite dell'interfaccia utente non può includere più di una lingua proveniente da un Language Interface Pack che corrisponde a una località supplementare, se tale lingua è inclusa e <paramref name="Options"/> include <see cref="LanguageFormat.MUI_LANGUAGE_ID"/> il buffer contiene "1400" per tale lingua, cioè il valore di <see cref="LOCALE_CUSTOM_UI_DEFAULT"/>.<br/><br/>
        /// Se <paramref name="Options"/> include <see cref="MUI_MACHINE_LANGUAGE_SETTINGS"/>, la funzione controlla che ogni lingua della lista corrisponda a una località NLS valida, la lista recuperata può contenere i seguenti elementi:<br/><br/>
        /// Lingue non installate nel sistema<br/>
        /// Voci duplicate<br/>
        /// Una stringa vuota<br/><br/>
        /// Se <paramref name="Options"/> include <see cref="MUI_MACHINE_LANGUAGE_SETTINGS"/> e la lista di lingue preferite dal sistema è vuota, la funzione imposta <paramref name="LanguagesBuffer"/> a una stringa vuota, 0 per <paramref name="LanguagesCount"/> e 2 per <paramref name="LanguagesBufferSize"/>.<br/><br/>
        /// Se <paramref name="Options"/> non include <see cref="MUI_MACHINE_LANGUAGE_SETTINGS"/>, la lista recuperata ha le seguenti caratteristiche:<br/><br/>
        /// Ogni lingua rappresenta una valida località NLS<br/>
        /// Ogni lingua è installata nel sistema<br/>
        /// Non esistono voci duplicate</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetSystemPreferredUILanguages", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetSystemPreferredUILanguages(uint Options, out uint LanguagesCount, IntPtr LanguagesBuffer, ref uint LanguagesBufferSize);

        /// <summary>
        /// Recupera le lingue dell'interfaccia utente preferite del thread corrente.
        /// </summary>
        /// <param name="Options">Opzioni di enumerazione.</param>
        /// <param name="LanguagesCount">Numero di lingue recuperate.</param>
        /// <param name="LanguagesBuffer">Puntatore a una lista di stringhe a terminazione nulla.</param>
        /// <param name="LanguagesBufferSize">Dimensione, in caratteri, di <paramref name="LanguagesBuffer"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="Options"/> può includere i valori dalle seguenti enumerazioni:<br/><br/>
        /// <see cref="LanguageFormat"/> (singolo)<br/>
        /// <see cref="ThreadLanguagesFilter"/> (singolo o composito)<br/><br/>
        /// Se <paramref name="LanguagesBuffer"/> è nullo e <paramref name="LanguagesBufferSize"/> è impostato a 0, la funzione imposta <paramref name="LanguagesBufferSize"/> alla dimensione necessaria di <paramref name="LanguagesBuffer"/>.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione del buffer è insufficiente o è nullo<br/><br/>
        /// Se l'operazione non riesce per qualunque altro motivo, <paramref name="LanguagesCount"/> e <paramref name="LanguagesBuffer"/> non sono definiti.<br/><br/>
        /// Se <paramref name="Options"/> include <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, la funzione recuperare gli identificatore esadecimali senza prefisso 0x e una lunghezza di 4 caratteri.<br/><br/>
        /// La lista di lingue preferite dal thread può includerne una o più corrispondenti a località supplementari, può esserci solo una lingua di tale tipo.<br/>
        /// La stringa "1400", che corrisponde al valore di <see cref="LOCALE_CUSTOM_UI_DEFAULT"/>, è presente nel buffer per ogni lingua corrispondente a una località supplementare.<br/>
        /// Se l'operazione è riuscita, il buffer contiene "1000", che corrisponde al valore di <see cref="LOCALE_CUSTOM_UNSPECIFIED"/>, per ogni altra lingua che corrisponde a una località supplementare, questo valore non è utile come input in nessuna funzione.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetThreadPreferredUILanguages", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetThreadPreferredUILanguages(uint Options, out uint LanguagesCount, IntPtr LanguagesBuffer, ref uint LanguagesBufferSize);

        /// <summary>
        /// Recupera l'identificatore della prima lingua dell'interfaccia utente per il thread corrente.
        /// </summary>
        /// <returns>Identificatore di una lingua esplicitamente associata al thread dalle funzioni <see cref="SetThreadUILanguage"/> oppure <see cref="SetThreadPreferredUILanguages"/>.</returns>
        /// <remarks>Se non è stata associata esplicitamente una lingua con il thread corrente, viene restituito l'identificatore di una lingua dell'interfaccia utente del sistema o dell'utente.<br/><br/>
        /// Il valore restituito da questa funzione non fornisce informazioni utili per una lingua LIP se corrisponde a una località supplementare, in questo caso viene restituito <see cref="LOCALE_CUSTOM_UI_DEFAULT"/> se essa è specificata nelle lingue preferite dell'utente, altrimenti viene restituito <see cref="LOCALE_CUSTOM_UNSPECIFIED"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetThreadUILanguage", SetLastError = true)]
        internal static extern ushort GetThreadUILanguage();

        /// <summary>
        /// Recupera una serie di informazioni su una lingua dell'interfaccia utente installata.
        /// </summary>
        /// <param name="Format">Formato della lingua.</param>
        /// <param name="Language">Lista di lingue ordinata e delimitata da caratteri nulli di identificatori o di nomi.</param>
        /// <param name="FallbackLanguages">Puntatore a un buffer in cui la funzione recupera una lista ordinata e delimitata da caratteri nulli formattata come indicato da <paramref name="Format"/>.</param>
        /// <param name="FallbackLanguagesBufferSize">Dimensione, in caratteri, del buffer indicato da <paramref name="FallbackLanguages"/>.</param>
        /// <param name="Attributes">Attributi dell'ultima lingua indicata in <paramref name="Language"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="FallbackLanguages"/> è nullo e <paramref name="FallbackLanguagesBufferSize"/> è impostato a 0, la funzione imposta <paramref name="FallbackLanguagesBufferSize"/> alla dimensione necessaria del buffer.<br/><br/>
        /// In caso di errore la funzione può impostare i seguenti codici di errore:<br/><br/>
        /// <see cref="ERROR_INSUFFICIENT_BUFFER"/>: la dimensione del buffer è insufficiente o è nullo<br/>
        /// <see cref="ERROR_INVALID_PARAMETER"/>: uno dei parametri non è valido<br/>
        /// <see cref="ERROR_OBJECT_NAME_NOT_FOUND"/>: l'oggetto specificato non esiste o non è valido oppure la prima lingua nella lista non è installata<br/><br/>
        /// Se la funzione imposta un qualunque altro codice di errore, <paramref name="FallbackLanguagesBufferSize"/> e <paramref name="Attributes"/> non sono definiti.<br/><br/>
        /// Se <paramref name="Format"/> è impostato a <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, la funzione recuperare gli identificatore esadecimali senza prefisso 0x e una lunghezza di 4 caratteri.<br/><br/>
        /// Se la lista di lingue preferite dall'utente include una lingua corrispondente a una località supplementare, il valore "1400" può essere usato per identificarla, può esserci solo una lingua di tale tipo.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetUILanguageInfo", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetUILanguageInfo(LanguageFormat Format, string Language, IntPtr FallbackLanguages, ref uint FallbackLanguagesBufferSize, out LanguageAttributes Attributes);

        /// <summary>
        /// Recupera l'identificatore della lingua dell'interfaccia utente per l'utente corrente.
        /// </summary>
        /// <returns>Identificatore della lingua.</returns>
        /// <remarks>Se l'utente non ha impostato una lingua, la funzione restituisce la lingua preferita impostata per il sistema, se tale lingue non è impostata, viene restituita la lingua predefinita di sistema.<br/><br/>
        /// Se la lingua dell'utente è parte di un Language Interface Pack e corrisponde a una località supplementare, questa funzione restituisce <see cref="LOCALE_CUSTOM_UI_DEFAULT"/>.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetUserDefaultUILanguage", SetLastError = true)]
        internal static extern ushort GetUserDefaultUILanguage();

        /// <summary>
        /// Recupera informazioni sulle impostazioni della lingua di visualizzazione.
        /// </summary>
        /// <param name="Format">Formato dell'output.</param>
        /// <param name="LanguagesCount">Numero di lingue.</param>
        /// <param name="LanguagesBuffer">Puntatore al buffer che contiene la lista di lingue.</param>
        /// <param name="LanguagesBufferSize">Dimensione, in caratteri, di <paramref name="LanguagesBuffer"/>.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="LanguagesBuffer"/> è nullo e <paramref name="LanguagesBufferSize"/> è impostato a 0, la funzione reimposta quest'ultimo valore alla dimensione necessaria del buffer.<br/><br/>
        /// In caso di errore la funzione può impostare il codice di errore <see cref="ERROR_INSUFFICIENT_BUFFER"/> se la dimensione del buffer è insufficiente o è nullo.<br/>
        /// Per qualunque altro tipo di errore, <paramref name="LanguagesCount"/> e <paramref name="LanguagesBuffer"/> non sono definiti.<br/><br/>
        /// Se <paramref name="Format"/> è impostato a <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, le stringhe presenti nel buffer sono degli identificatori esadecimali che non includono il prefisso 0x e sono lunghi 4 caratteri.<br/><br/>
        /// La lingua di visualizzazione non può includere più di una lingua inclusa in un LIP corrispondente a una località supplementare.<br/>
        /// Se la lista include più di una di tali lingue e l'applicazione specifica <see cref="LanguageFormat.MUI_LANGUAGE_ID"/> in <paramref name="Format"/>, il buffer contiene la stringa "1400" che corrisponde al valore di <see cref="LOCALE_CUSTOM_UI_DEFAULT"/>.<br/><br/>
        /// La lista di lingue recuperata da questa funzione ha le seguenti caratteristiche:<br/><br/>
        /// ogni lingua rappresenta una località NLS valida<br/>
        /// ogni lingua è installata nel sistema operativo<br/>
        /// non ci sono voci duplicate<br/><br/>
        /// Se la lista è vuota o se la convalida non ha successo, viene restituita la lista di lingue preferite dal sistema al suo posto.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "GetUserPreferredUILanguages", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetUserPreferredUILanguages(LanguageFormat Format, out uint LanguagesCount, IntPtr LanguagesBuffer, ref uint LanguagesBufferSize);

        /// <summary>
        /// Imposta le lingue dell'interfaccia utente preferite del processo.
        /// </summary>
        /// <param name="Format">Formato dell'input.</param>
        /// <param name="LanguageList">Lista ordinata, in ordine discendente di preferenza, delimitata da caratteri nulli.</param>
        /// <param name="LanguagesCount">Numero di lingue impostate.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="BufferPointer"/> è nullo, la lista di lingue preferite del processo viene svuotata.<br/><br/>
        /// Possono essere impostato al massimo 5 lingue.<br/><br/>
        /// In caso di errore questa funzione può impostare il codice di errore <see cref="ERROR_INVALID_PARAMETER"/>.<br/>
        /// Se la lista è vuota o se le lingue specificate non sono valide, l'operazione è completata correttamente e <paramref name="LanguagesCount"/> è impostato a 0.<br/><br/>
        /// Se <paramref name="Format"/> è impostato a <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, le stringhe di input devono essere identificatori esadecimali senza il prefisso 0x e di 4 caratteri di lunghezza.</remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetProcessPreferredUILanguages", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetProcessPreferredUILanguages(LanguageFormat Format, string LanguageList, out uint LanguagesCount);

        /// <summary>
        /// Imposta le lingue dell'interfaccia utente preferite per il thread corrente.
        /// </summary>
        /// <param name="Options">Opzioni di impostazione.</param>
        /// <param name="LanguageList">Lista ordinata, in ordine discendente di preferenza, delimitata da caratteri nulli.</param>
        /// <param name="LanguagesCount">Numero di lingue impostate.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="Options"/> può includere i valori delle seguenti enumerazioni:<br/><br/>
        /// <see cref="LanguageFormat"/><br/>
        /// <see cref="ThreadLanguageSetFilter"/><br/><br/>
        /// Per svuotare la lista di lingue, impostare <paramref name="LanguageList"/> impostare a una stringa vuota con doppio terminatore nullo, <paramref name="Options"/> deve essere 0 o uno dei valori di <see cref="LanguageFormat"/>.<br/>
        /// Se <paramref name="Options"/> include uno dei valori dell'enumerazione <see cref="ThreadLanguageSetFilter"/>, <paramref name="LanguageList"/> e <paramref name="LanguagesCount"/> devono essere nulli.<br/><br/>
        /// Possono essere impostato al massimo 5 lingue.<br/><br/>
        /// Se <paramref name="Options"/> include <see cref="LanguageFormat.MUI_LANGUAGE_ID"/>, le stringhe di input devono essere identificatori esadecimali senza il prefisso 0x e di 4 caratteri di lunghezza, non possono indicare i seguenti valori:<br/><br/>
        /// <see cref="LOCALE_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_USER_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UI_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UNSPECIFIED"/></remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetThreadPreferredUILanguages", SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetThreadPreferredUILanguages(uint Options, string LanguageList, out uint LanguagesCount);

        /// <summary>
        /// Imposta la lingua dell'interfaccia utente per il thread corrente.
        /// </summary>
        /// <param name="LangID">Identificatore della lingua.</param>
        /// <returns>L'identificare fornito se l'operazione è riuscita, un valore diverso in caso contrario.</returns>
        /// <remarks><paramref name="LangID"/> può avere valore 0, in tal caso, l'operazione ha sempre successo e la funzione restituisce l'identificatore della lingua che supporta meglio la console.<br/><br/>
        /// Impostare <paramref name="LangID"/> a 0 ha lo stesso effetto di una chiamata a <see cref="SetThreadPreferredUILanguages"/> con il valore <see cref="ThreadLanguageSetFilter.MUI_CONSOLE_FILTER"/> per il parametro Options impostato.<br/><br/>
        /// Dopo aver specificato 0 per <paramref name="LangID"/> non è più possibile usare le seguenti costanti per identificare una lingua:<br/><br/>
        /// <see cref="LOCALE_SYSTEM_DEFAULT"/><br/>
        /// <see cref="LOCALE_USER_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UI_DEFAULT"/><br/>
        /// <see cref="LOCALE_CUSTOM_UNSPECIFIED"/></remarks>
        [DllImport("Kernel32.dll", EntryPoint = "SetThreadUILanguage", SetLastError = true)]
        internal static extern ushort SetThreadUILanguage(ushort LangID);
    }
}