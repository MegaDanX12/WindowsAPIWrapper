using System;
using System.Runtime.InteropServices;
using static WindowsAPI.HighDPIWrapper.Native.HighDPIEnumerations;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Window.WindowEnumerations;
using static WindowsAPI.DiagnosticsWrapper.Native.HRESULTErrorConstants;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration.ConfigurationEnumerations;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.HighDPIWrapper.Native
{
    /// <summary>
    /// Funzioni il cui comportamento cambi in base ai DPI.
    /// </summary>
    internal static class HighDPIFunctions
    {
        /// <summary>
        /// Calcola la dimensione richiesta per il rettangolo della finestra, in base alla dimensione desiderata del rettangolo client e il valore DPI fornito.
        /// </summary>
        /// <param name="RectPointer">Puntatore a struttura <see cref="General.Native.GeneralStructures.RECT"/> che contiene le coordinate degli angoli superiore sinistro e inferiore destro dell'area client desiderata.</param>
        /// <param name="Styles">Gli stili della finestra.</param>
        /// <param name="HasMenu">Indica se la finestra ha un menù.</param>
        /// <param name="ExtendedStyles">Gli stili estesi della finestra.</param>
        /// <param name="DPI">Il valore DPI da usare per il ridimensionamento.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>La struttura <see cref="General.Native.GeneralStructures.RECT"/> puntata da <paramref name="RectPointer"/>, al termine dell'operazione, contiene le coordinate degli angoli superiore sinistro e inferiore destro della finestra necessari per contenere l'area client desiderata.<br/><br/>
        /// <see cref="WindowStyles.WS_OVERLAPPED"/> non può essere specificato per <paramref name="Styles"/>.</remarks>
        [DllImport("User32.dll", EntryPoint = "AdjustWindowRectExForDpi", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AdjustWindowRectForDpi(ref RECT Rect, WindowStyles Styles, [MarshalAs(UnmanagedType.Bool)] bool HasMenu, WindowExtendedStyles ExtendedStyles, uint DPI);

        /// <summary>
        /// Determina se due valori DPI_AWARENESS_CONTEXT sono uguali.
        /// </summary>
        /// <param name="ContextA">Primo valore da confrontare.</param>
        /// <param name="ContextB">Secondo valore da confrontare.</param>
        /// <returns>true se i valori sono uguali, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "AreDpiAwarenessContextsEqual", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool AreDpiAwarenessContextsEqual(IntPtr ContextA, IntPtr ContextB);

        /// <summary>
        /// Abilita l'adattamento automatico dell'area non client di una finestra top level.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Questa funzione deve essere chiamata durante l'inizializzazione della finestra.</remarks>
        [DllImport("User32.dll", EntryPoint = "EnableNonClientDpiScaling", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableNonClientDpiScaling(IntPtr WindowHandle);

        /// <summary>
        /// Recupera il valore <see cref="DPI_AWARENESS"/> da un DPI_AWARENESS_CONTEXT.
        /// </summary>
        /// <param name="Value">Valore da cui recuperare i dati.</param>
        /// <returns>Il valore <see cref="DPI_AWARENESS"/>.</returns>
        /// <remarks>Se <paramref name="Value"/> non è valido, la funzione restituisce <see cref="DPI_AWARENESS.DPI_AWARENESS_INVALID"/>.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetAwarenessFromDpiAwarenessContext", SetLastError = true)]
        internal static extern DPI_AWARENESS GetAwarenessFromDpiAwarenessContext(IntPtr Value);

        /// <summary>
        /// Richiede i DPI del display.
        /// </summary>
        /// <param name="MonitorHandle">Handle al monitor.</param>
        /// <param name="DPIType">Tipo di DPI da recuperare.</param>
        /// <param name="X">Valore DPI sull'asse X.</param>
        /// <param name="Y">Valore DPI sull'asse Y.</param>
        /// <returns>La funzione restituisce uno dei seguenti valori:<br/><br/>
        /// <see cref="S_OK"/>: l'operazione è riuscita<br/>
        /// <see cref="E_INVALIDARG"/>: l'handle, il tipo di DPI o i puntatore passati non sono validi</returns>
        /// <remarks>Questa funzione non tiene conto dei DPI, non dovrebbe essere usata se il thread chiamante tiene conto dei DPI per monitor.<br/><br/>
        /// I valori restituiti da questa funzione dipendono da come l'applicazione chiamante tiene conto dei DPI.<br/>
        /// I seguenti valori sono i risultati della funzione in base al valore <see cref="PROCESS_DPI_AWARENESS"/> dell'applicazione:<br/><br/>
        /// <see cref="PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE"/> -> 96<br/>
        /// <see cref="PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE"/> -> un valore impostato come DPI di sistema<br/>
        /// <see cref="PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE"/> -> effettivo valore DPI impostato dall'utente per il display<br/><br/>
        /// I valori di <paramref name="X"/> e <paramref name="Y"/> sono uguali.</remarks>
        [DllImport("Shcore.dll", EntryPoint = "GetDpiForMonitor", SetLastError = true)]
        internal static extern uint GetDpiForMonitor(IntPtr MonitorHandle, MONITOR_DPI_TYPE DPIType, out uint X, out uint Y);

        /// <summary>
        /// Recupera i DPI di sistema.
        /// </summary>
        /// <returns>Il valore dei DPI di sistema.</returns>
        /// <remarks>Il valore restituito dipende dal contesto chiamante.<br/>
        /// Se <see cref="DPI_AWARENESS"/> è <see cref="DPI_AWARENESS.DPI_AWARENESS_UNAWARE"/>, il valore restituito sarà sempre 96, per qualunque tipo il valore restituito è l'effettivo DPI di sistema.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetDpiForSystem", SetLastError = true)]
        internal static extern uint GetDpiForSystem();

        /// <summary>
        /// Recupera il valore DPI per la finestra specificata.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Valore DPI per la finestra.</returns>
        /// <remarks>Il valore restituito dipende dal valore <see cref="DPI_AWARENESS"/> della finestra, i valori possibili sono i seguenti:<br/><br/>
        /// <see cref="DPI_AWARENESS.DPI_AWARENESS_UNAWARE"/> -> 96<br/>
        /// <see cref="DPI_AWARENESS.DPI_AWARENESS_SYSTEM_AWARE"/> -> i DPI di sistema<br/>
        /// <see cref="DPI_AWARENESS.DPI_AWARENESS_PER_MONITOR_AWARE"/> -> DPI del monitor dove si trova la finestra</remarks>
        [DllImport("User32.dll", EntryPoint = "GetDpiForWindow", SetLastError = true)]
        internal static extern uint GetDpiForWindow(IntPtr WindowHandle);

        /// <summary>
        /// Recupera il modo in cui un processo tiene conto dei DPI.
        /// </summary>
        /// <param name="ProcessHandle">Handle al processo.</param>
        /// <param name="Value">Valore <see cref="PROCESS_DPI_AWARENESS"/> associato al processo.</param>
        /// <returns>Questa funzione restituisce uno dei seguenti valori:<br/><br/>
        /// <see cref="S_OK"/> se l'operazione è riuscita<br/>
        /// <see cref="E_INVALIDARG"/> se l'handle o il puntatore passati non sono validi<br/>
        /// <see cref="E_ACCESSDENIED"/> se l'applicazione non ha privilegi sufficienti</returns>
        [DllImport("Shcore.dll", EntryPoint = "GetProcessDpiAwareness", SetLastError = true)]
        internal static extern uint GetProcessDpiAwareness(IntPtr ProcessHandle, out PROCESS_DPI_AWARENESS Value);

        /// <summary>
        /// Recupera la metrica o l'impostazione di sistema indicata tenendo conto di DPI forniti.
        /// </summary>
        /// <param name="Metric">Metrica o impostazione da recuperare.</param>
        /// <param name="DPI">DPI da usare per adattare la metrica.</param>
        /// <returns>Se l'operazione è riuscita il valore restituito è diverso da 0, 0 altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "GetSystemMetricsForDpi", SetLastError = true)]
        internal static extern int GetSystemMetricsForDpi(SystemMetric Metric, uint DPI);

        /// <summary>
        /// Recupera il valore DPI_AWARENESS_CONTEXT per il thread corrente.
        /// </summary>
        /// <returns>Il valore DPI_AWARENESS_CONTEXT per il thread.</returns>
        [DllImport("User32.dll", EntryPoint = "GetThreadDpiAwarenessContext")]
        internal static extern IntPtr GetThreadDpiAwarenessContext();

        /// <summary>
        /// Recupera il valore DPI_AWARENESS_CONTEXT associato a una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Il valore DPI_AWARENESS_CONTEXT per la finestra.</returns>
        /// <remarks>Se <paramref name="WindowHandle"/> non è valido, il valore restituito è nullo.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetWindowDpiAwarenessContext")]
        internal static extern IntPtr GetWindowDpiAwarenessContext(IntPtr WindowHandle);

        /// <summary>
        /// Determina se un valore DPI_AWARENESS_CONTEXT è valido e supportato dal sistema.
        /// </summary>
        /// <param name="Context">Valore da controllare.</param>
        /// <returns>true se il valore è supportato, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "IsValidDpiAwarenessContext")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsValidDpiAwarenessContext(IntPtr Context);

        /// <summary>
        /// Converte un punto in una finestra da coordinate logiche a coordinate fisiche senza tenere conto dei DPI del chiamante.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Point">Struttura <see cref="POINT"/> che specifica le coordinate logiche da convertire.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="Point"/>, dopo il termine dell'operazione, riceve le coordinate fisiche.</remarks>
        [DllImport("User32.dll", EntryPoint = "LogicalToPhysicalPointForPerMonitorDPI")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool LogicalToPhysicalPointForPerMonitorDPI(IntPtr WindowHandle, ref POINT Point);

        /// <summary>
        /// Converte un punto in una finestra da coordinate fisiche a coordinate logiche senza tenere conto dei DPI del chiamante.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Point">Struttura <see cref="POINT"/> che specifica le coordinate fisiche da convertire.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks><paramref name="Point"/>, dopo il termine dell'operazione, riceve le coordinate logiche.</remarks>
        [DllImport("User32.dll", EntryPoint = "PhysicalToLogicalPointForPerMonitorDPI")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool PhysicalToLogicalPointForPerMonitorDPI(IntPtr WindowHandle, ref POINT Point);

        /// <summary>
        /// Imposta il modo predefinito in cui il processo tiene conto dei DPI.
        /// </summary>
        /// <param name="Awareness">Valore <see cref="PROCESS_DPI_AWARENESS"/> da impostare.</param>
        /// <returns>Questa funzione restituisce uno dei seguenti valori:<br/><br/>
        /// <see cref="S_OK"/>: l'operazione è riuscita<br/>
        /// <see cref="E_INVALIDARG"/>: il valore passato non è valido<br/>
        /// <see cref="E_ACCESSDENIED"/>: il valore per il processo è gia stato impostato</returns>
        [DllImport("Shcore.dll", EntryPoint = "SetProcessDpiAwareness")]
        internal static extern uint SetProcessDpiAwareness(PROCESS_DPI_AWARENESS Awareness);

        /// <summary>
        /// Imposta il modo in cui il thread corrente tiene conto dei DPI.
        /// </summary>
        /// <param name="NewContext">Valore DPI_AWARENESS_CONTEXT da impostare.</param>
        /// <returns>Il valore precedente impostato per il thread.</returns>
        /// <remarks>Se il valore fornito non è valido, il thread non viene aggiornato e il valore restituito è nullo.</remarks>
        [DllImport("User32.dll", EntryPoint = "SetThreadDpiAwarenessContext")]
        internal static extern IntPtr SetThreadDpiAwarenessContext(IntPtr NewContext);

        /// <summary>
        /// Recupera il valore di uno dei parametri di sistema, tenendo conto dei DPI forniti.
        /// </summary>
        /// <param name="Parameter">Parametro da recuperare.</param>
        /// <param name="uiParam">Dati relativi al parametro.</param>
        /// <param name="pvParam">Dati relativi al parametro.</param>
        /// <param name="WinIni">Nessun effetto.</param>
        /// <param name="DPI">Valore DPI da usare.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SystemParametersInfoForDpi", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfoForDpi(uint Parameter, uint uiParam, IntPtr pvParam, uint WinIni, uint DPI);

        /// <summary>
        /// Imposta il DPI_AWARENESS_CONTEXT per il processo corrente.
        /// </summary>
        /// <param name="NewValue">Nuovo valore da impostare.</param>
        /// <returns>true se l'operazione ha avuto successo, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SetProcessDpiAwarenessContext", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetProcessDpiAwarenessContext(IntPtr NewValue);

        /// <summary>
        /// Imposta il modo in cui le finestre di dialogo reagiscono ai cambiamenti nei DPI.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Mask">Maschera che contiene i valori da cambiare.</param>
        /// <param name="Values">Maschera che contiene i nuovi valori.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SetDialogDpiChangeBehavior", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetDialogDpiChangeBehavior(IntPtr WindowHandle, DIALOG_DPI_CHANGE_BEHAVIORS Mask, DIALOG_DPI_CHANGE_BEHAVIORS Values);

        /// <summary>
        /// Recupera il modo in cui le finestre di dialogo reagiscono ai cambiamenti nei DPI.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo.</param>
        /// <returns>Il valore che indica il comportamento della finestra di dialogo.</returns>
        /// <remarks>Se l'handle non è valido, questa funzione restituisce 0.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetDialogDpiChangeBehavior", SetLastError = true)]
        internal static extern DIALOG_DPI_CHANGE_BEHAVIORS GetDialogDpiChangeBehavior(IntPtr DialogHandle);

        /// <summary>
        /// Imposta il modo in cui le finestre figlie di un dialogo reagiscono ai cambiamenti nei DPI.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Mask">Maschera che contiene i valori da cambiare.</param>
        /// <param name="Values">Maschera che contiene i nuovi valori.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SetDialogControlDpiChangeBehavior", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetDialogControlDpiChangeBehavior(IntPtr WindowHandle, DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Mask, DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Values);

        /// <summary>
        /// Recupera il modo in cui le finestre figlie di un dialogo reagiscono ai cambiamenti nei DPI.
        /// </summary>
        /// <param name="DialogHandle">Handle al dialogo.</param>
        /// <returns>Il valore che indica il comportamento della finestre figlie del dialogo.</returns>
        /// <remarks>Se l'handle non è valido, questa funzione restituisce 0.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetDialogControlDpiChangeBehavior", SetLastError = true)]
        internal static extern DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS GetDialogControlDpiChangeBehavior(IntPtr DialogHandle);

        /// <summary>
        /// Apre un handle ai dati del tema per una finestra e la sua classe associata per un certo valore DPI.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="ClassList">Lista di classi.</param>
        /// <param name="DPI">Valore DPI da associare all'handle al tema.</param>
        /// <returns>Se esiste una corrispondenza tra i dati di una delle classi indicate e i dati delle classi nel tema attivo viene restituito un handle al tema, altrimenti il valore restituito è nullo.</returns>
        /// <remarks><paramref name="ClassList"/> contiene una lista di classi separate da un punto e virgola.</remarks>
        [DllImport("User32.dll", EntryPoint = "OpenThemeDataForDpi", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr OpenThemeDataForDpi(IntPtr WindowHandle, string ClassList, uint DPI);

        /// <summary>
        /// Recupera il DPI di sistema associato con un processo.
        /// </summary>
        /// <param name="ProcessHandle">Handle al processo.</param>
        /// <returns>Il valore dei DPI di sistema per il processo.</returns>
        /// <remarks>Se <paramref name="ProcessHandle"/> è nullo, il valore restituito è quello valido per il processo chiamante.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetSystemDpiForProcess", SetLastError = true)]
        internal static extern uint GetSystemDpiForProcess(IntPtr ProcessHandle);

        /// <summary>
        /// Recupera il valore DPI da un DPI_AWARENESS_CONTEXT.
        /// </summary>
        /// <param name="Context">Valore da esaminare.</param>
        /// <returns>Valore DPI associato al DPI_AWARENESS_CONTEXT.</returns>
        [DllImport("User32.dll", EntryPoint = "GetDpiFromDpiAwarenessContext", SetLastError = true)]
        internal static extern uint GetDpiFromContext(IntPtr Context);

        /// <summary>
        /// Imposta il comportamento di hosting DPI per un thread.
        /// </summary>
        /// <param name="NewBehavior">Nuovo comportamento.</param>
        /// <returns>Il valore che definisce il comportamento precedente.</returns>
        /// <remarks>Se <paramref name="NewBehavior"/> non è valido, il thread non viene aggiornato e il valore restituito è <see cref="DPI_HOSTING_BEHAVIOR.DPI_HOSTING_BEHAVIOR_INVALID"/>.</remarks>
        [DllImport("User32.dll", EntryPoint = "SetThreadDpiHostingBehavior", SetLastError = true)]
        internal static extern DPI_HOSTING_BEHAVIOR SetThreadDpiHostingBehavior(DPI_HOSTING_BEHAVIOR NewBehavior);

        /// <summary>
        /// Recupera il comportamento di hosting DPI per un thread.
        /// </summary>
        /// <returns>Il valore <see cref="DPI_HOSTING_BEHAVIOR"/> associato al thread.</returns>
        [DllImport("User32.dll", EntryPoint = "GetThreadDpiHostingBehavior", SetLastError = true)]
        internal static extern DPI_HOSTING_BEHAVIOR GetThreadDpiHostingBehavior();

        /// <summary>
        /// Recupera il comportamento di hosting DPI per una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns>Il valore <see cref="DPI_HOSTING_BEHAVIOR"/> associato alla finestra.</returns>
        [DllImport("User32.dll", EntryPoint = "GetWindowDpiHostingBehavior", SetLastError = true)]
        internal static extern DPI_HOSTING_BEHAVIOR GetWindowDpiHostingBehavior(IntPtr WindowHandle);
    }
}