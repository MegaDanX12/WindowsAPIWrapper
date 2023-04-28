using System;
using System.Runtime.InteropServices;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMEnumerations;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMStructures;
using static WindowsAPI.DiagnosticsWrapper.Native.HRESULTErrorConstants;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.DesktopWindowManagerWrapper.Native
{
    /// <summary>
    /// Funzioni DWM.
    /// </summary>
    internal static class DWMFunctions
    {
        /// <summary>
        /// Procedura finestra di default per Desktop Window Manager per l'hit testing nell'area non client.
        /// </summary>
        /// <param name="WindowHandle">Handle alla procedura della finestra che ha ricevuto il messaggio.</param>
        /// <param name="Message">Messaggio.</param>
        /// <param name="wParam">Informazioni addizionali messaggio.</param>
        /// <param name="lParam">Informazioni addizionali messaggio.</param>
        /// <param name="Result">Risultato dell'hit test.</param>
        /// <returns>true se il messaggio è stato elaborato, false altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmDefWindowProc", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DwmDefWindowProc(IntPtr WindowHandle, uint Message, IntPtr wParam, IntPtr lParam, IntPtr Result);

        /// <summary>
        /// Notifica DWM di partecipare o di interrompere la sua partecipazione alla programmazione del servizio MMCSS mentre il processo è in esecuzione.
        /// </summary>
        /// <param name="EnableMMCSS">Indica a Dwm se partecipare (true) o meno (false) alla programmazione del servizio MMCSS.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT in caso contrario.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmEnableMMCSS", SetLastError = true)]
        internal static extern uint EnableMultimediaClassScheduleService([MarshalAs(UnmanagedType.Bool)] bool EnableMMCSS);

        /// <summary>
        /// Estende la cornice della finestra nell'area client.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Margins">Struttura <see cref="MARGINS"/> che descrive i margini da usare per estendere la cornice.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT in caso contrario.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmExtendFrameIntoClientArea", SetLastError = true)]
        internal static extern uint ExtendFrameIntoClientArea(IntPtr WindowHandle, ref MARGINS Margins);

        /// <summary>
        /// Invia una chiamata di pulizia (flush) che blocca il chiamante fino alla prossima presentazione, quando tutti gli aggiornamenti di superfici Microsoft DirectX da eseguire sono stati effettuati.
        /// </summary>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmFlush", SetLastError = true)]
        internal static extern uint Flush();

        /// <summary>
        /// Recupera il colore attualmente usato dalla composizione vetro DWM.
        /// </summary>
        /// <param name="Colorization">Colore usato per la composizione (0xAARRGGBB).</param>
        /// <param name="IsOpaqueBlend">Indica se il colore è una miscela opaca (true) o no (false).</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmGetColorizationColor", SetLastError = true)]
        internal static extern uint GetColorizationColor(out uint Colorization, [MarshalAs(UnmanagedType.Bool)] out bool IsOpaqueBlend);

        /// <summary>
        /// Recupera le attuali informazioni sulle tempistiche della composizione per la finestra specificata.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra, deve essere <see cref="IntPtr.Zero"/>.</param>
        /// <param name="TimingInfo">Puntatore a struttura <see cref="DWM_TIMING_INFO"/> che riceve le informazioni.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmGetCompositionTimingInfo", SetLastError = true)]
        internal static extern uint GetCompositionTimingInfo(IntPtr WindowHandle, ref DWM_TIMING_INFO TimingInfo);

        /// <summary>
        /// Recupera gli attributi di transporto.
        /// </summary>
        /// <param name="IsRemoting">Indica se il trasporto supporta il remoting.</param>
        /// <param name="IsConnected">Indica se il trasporto è connesso.</param>
        /// <param name="Generation">Generazione del trasporto.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmGetTransportAttributes", SetLastError = true)]
        internal static extern uint GetTransportAttributes([MarshalAs(UnmanagedType.Bool)] out bool IsRemoting, [MarshalAs(UnmanagedType.Bool)] out bool IsConnected, out uint Generation);

        /// <summary>
        /// Recupera il valore di un attributo DWM applicato a una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Attribute">Attributo da recuperare.</param>
        /// <param name="AttributeValue">Valore dell'attributo.</param>
        /// <param name="AttributeSize">Dimensione, in byte, del valore restituito.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmGetWindowAttribute", SetLastError = true)]
        internal static extern uint GetWindowAttribute(IntPtr WindowHandle, DWMWINDOWATTRIBUTE Attribute, IntPtr AttributeValue, uint AttributeSize);

        /// <summary>
        /// Indica a DWM che tutte le miniature e rappresentazioni peek precedentemente fornite per una finestra non sono più valide e devono essere aggiornate.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        /// <remarks><paramref name="WindowHandle"/> deve riferirsi a una finestra che appartiene al processo chiamante.</remarks>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmInvalidateIconicBitmaps", SetLastError = true)]
        internal static extern uint InvalidateIconicBitmaps(IntPtr WindowHandle);

        /// <summary>
        /// Recupera la dimensione della fonte di una miniatura DWM.
        /// </summary>
        /// <param name="ThumbnailHandle">Handle alla miniatura.</param>
        /// <param name="Size">Struttura <see cref="SIZE"/> che descrive la dimensione della miniatura.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmQueryThumbnailSourceSize", SetLastError = true)]
        internal static extern uint QueryThumbnailSourceSize(IntPtr ThumbnailHandle, out SIZE Size);

        /// <summary>
        /// Crea una relazione tra due finestre per la visualizzazione della miniatura.
        /// </summary>
        /// <param name="DestinationWindow">Handle alla finestra dove verrà visualizzata la miniatura.</param>
        /// <param name="SourceWindow">Handle alla finestra da usare come fonte della miniatura.</param>
        /// <param name="ThumbnailHandle">Handle alla relazione.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmRegisterThumbnail", SetLastError = true)]
        internal static extern uint RegisterThumbnailRelationship(IntPtr DestinationWindow, IntPtr SourceWindow, out IntPtr ThumbnailHandle);

        /// <summary>
        /// Notifica a DWM che un contatto è stato riconosciuto come un gesto e che dovrebbe essere disegnato il feedback per tale gesto.
        /// </summary>
        /// <param name="Type">Tipo di gesto.</param>
        /// <param name="Contacts">Numero di punti di contatto.</param>
        /// <param name="PointerID">ID del puntatore.</param>
        /// <param name="Points">Struttura <see cref="POINT"/>che definisce il punto.</param>
        /// <returns>Questa funzione non restituisce nulla.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmRenderGesture", SetLastError = true)]
        internal static extern uint RenderGesture(GESTURE_TYPE Type, uint Contacts, ref uint PointerID, ref POINT Points);

        /// <summary>
        /// Imposta un bitmap statico da visualizzare come live preview di una finestra o di una scheda.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="BitmapHandle">Handle al bitmap.</param>
        /// <param name="ClientOffset">Offset della regione client di una scheda dalla cornice della finestra che la ospita.</param>
        /// <param name="DisplayOptions">Opzioni di visualizzazione.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore altrimenti.</returns>
        /// <remarks>Il bitmap non viene memorizzato, se l'anteprima della finestra non è visibile al momento della chiamata, la funzione restituisce <see cref="S_OK"/> ma il bitmap viene scartato senza essere usato.<br/><br/>
        /// <paramref name="WindowHandle"/> deve riferirsi a una finestra appartenente al processo chiamante.</remarks>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmSetIconicLivePreviewBitmap", SetLastError = true)]
        internal static extern uint SetIconicLivePreviewBitmap(IntPtr WindowHandle, IntPtr BitmapHandle, IntPtr ClientOffset, uint DisplayOptions);

        /// <summary>
        /// Imposta un bitmap statico di una finestra o scheda da usare come miniatura.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="BitmapHandle">Handle al bitmap.</param>
        /// <param name="DisplayOptions">Opzioni di visualizzazione.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        /// <remarks><paramref name="WindowHandle"/> deve riferirsi a una finestra appartenente al processo chiamante.</remarks>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmSetIconicThumbnail", SetLastError = true)]
        internal static extern uint SetIconicThumbnail(IntPtr WindowHandle, IntPtr BitmapHandle, uint DisplayOptions);

        /// <summary>
        /// Imposta uno degli attributi DWM relativi al rendering dell'area non client per una finestra.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Attribute">Attributo da modificare.</param>
        /// <param name="ValuePointer">Puntatore a un oggetto che contiene il valore da impostare.</param>
        /// <param name="ValueSize">Dimensione, in byte, del valore dell'attributo.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmSetWindowAttribute", SetLastError = true)]
        internal static extern uint SetWindowAttribute(IntPtr WindowHandle, DWMWINDOWATTRIBUTE Attribute, IntPtr ValuePointer, uint ValueSize);

        /// <summary>
        /// Chiamata da un app o da un framework per specificare il tipo di feedback visuale da disegnare in risposta a un particolare contatto di penna o tocco.
        /// </summary>
        /// <param name="PointerID">ID del puntatore.</param>
        /// <param name="ContactType">Tipo di contatto.</param>
        /// <returns>Se <paramref name="PointerID"/> non corrisponde a nessun contatto attualmente presente sullo schermo, la funzione restituisce <see cref="E_INVALIDARG"/> altrimenti restituisce <see cref="S_OK"/>.</returns>
        /// <remarks>Questa funziona può essere chiamata dal thread UI.</remarks>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmShowContact", SetLastError = true)]
        internal static extern uint ShowContact(uint PointerID, DWM_SHOWCONTACT ContactType);

        /// <summary>
        /// Abilita il feedback grafico di interazioni "tocca e trascina" all'utente.
        /// </summary>
        /// <param name="PointerID">ID del puntatore.</param>
        /// <param name="Enable">Indica se il contatto è abilitato.</param>
        /// <param name="Tether">Il punto di connessione.</param>
        /// <returns>Questa funzione non restituisce nulla.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmTetherContact", SetLastError = true)]
        internal static extern uint TetherContact(uint PointerID, [MarshalAs(UnmanagedType.Bool)] bool Enable, POINT Tether);

        /// <summary>
        /// Coordina le animazioni tra le finestre di utilità e DWM.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Target">Obbiettivo.</param>
        /// <returns>Questa funziona non restituisce nulla.</returns>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmTransitionOwnedWindow", SetLastError = true)]
        internal static extern uint TransitionOwnedWindow(IntPtr WindowHandle, DWMTRANSITION_OWNEDWINDOW_TARGET Target);

        /// <summary>
        /// Annulla una relazione creata tramite <see cref="RegisterThumbnailRelationship"/>.
        /// </summary>
        /// <param name="ThumbnailHandle">Handle alla relazione.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        /// <remarks>Se <paramref name="ThumbnailHandle"/> è nullo o si riferisce a una relazione non esistente, la funzione restituisce <see cref="E_INVALIDARG"/>.<br/><br/>
        /// Annullare la registrazione di una relazione può essere fatto solo dal processo che l'ha registrata.</remarks>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmRegisterThumbnail", SetLastError = true)]
        internal static extern uint UnregisterThumbnailRelationship(IntPtr ThumbnailHandle);

        /// <summary>
        /// Aggiorna le proprietà per una miniatura DWM.
        /// </summary>
        /// <param name="ThumbnailHandle">Handle alla miniatura.</param>
        /// <param name="Properties">Struttura <see cref="DWM_THUMBNAIL_PROPERTIES"/> che descrive le proprietà della miniatura.</param>
        /// <returns><see cref="S_OK"/> se l'operazione è riuscita, un codice di errore HRESULT altrimenti.</returns>
        /// <remarks>Le relazioni create tramite <see cref="RegisterThumbnailRelationship"/> non verranno renderizzate fino alla chiamata a questa funzione.</remarks>
        [DllImport("Dwmapi.dll", EntryPoint = "DwmUpdateThumbnailProperties", SetLastError = true)]
        internal static extern uint UpdateThumbnailProperties(IntPtr ThumbnailHandle, ref DWM_THUMBNAIL_PROPERTIES Properties);
    }
}