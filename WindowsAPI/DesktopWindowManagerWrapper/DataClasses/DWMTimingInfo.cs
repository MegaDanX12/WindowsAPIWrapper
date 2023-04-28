using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMStructures;

namespace WindowsAPI.DesktopWindowManagerWrapper.DataClasses
{
    /// <summary>
    /// Informazioni sulle tempistiche della composizione DWM.
    /// </summary>
    public class DWMTimingInfo
    {
        /// <summary>
        /// Frequenza di aggiornamento del monitor.
        /// </summary>
        public float MonitorRefreshRate { get; }

        /// <summary>
        /// Periodo di aggiornamento del monitor.
        /// </summary>
        public long PCRefreshPeriod { get; }

        /// <summary>
        /// Frequenza di composizione.
        /// </summary>
        public float CompositionRate { get; }

        /// <summary>
        /// Valore del contatore prestazioni prima del vertical blank.
        /// </summary>
        public long PCVerticalBlank { get; }

        /// <summary>
        /// Contatore di aggiornamenti DWM.
        /// </summary>
        public long DWMRefreshCounter { get; }

        /// <summary>
        /// Contatore di aggiornamento DirectX.
        /// </summary>
        public int DirectXRefreshCounter { get; }

        /// <summary>
        /// Valore del contatore prestazioni per una composizione di un frame.
        /// </summary>
        public long PCFrameComposition { get; }

        /// <summary>
        /// Numero del frame composto al momento indicato da <see cref="PCFrameComposition"/>.
        /// </summary>
        public long FrameNumber { get; }

        /// <summary>
        /// Numero di presentazione DirectX usato per identificare i frame in corso di rendering.
        /// </summary>
        public int DirectXPresentNumber { get; }

        /// <summary>
        /// Il contatore di aggiornamento del frame composto al momento indicato da <see cref="PCFrameComposition"/>.
        /// </summary>
        public long FrameRefreshCount { get; }

        /// <summary>
        /// Numero del frame DWM inviato per ultimo.
        /// </summary>
        public long DWMLastSubmittedFrameNumber { get; }

        /// <summary>
        /// Numero di presentazione DirectX inviato per ultimo.
        /// </summary>
        public int DirectXPresentNumberSubmitted { get; }

        /// <summary>
        /// Numero dell'ultimo frame DWM confermato come presentato.
        /// </summary>
        public long DWMConfirmedFrameNumber { get; }

        /// <summary>
        /// Ultimo numero di presentazione DirectX confermato come presentato.
        /// </summary>
        public int DirectXConfirmedPresentNumber { get; }

        /// <summary>
        /// Conteggio aggiornamenti obbiettivo dell'ultimo frame confermato come completato dalla GPU.
        /// </summary>
        public long LastFrameRefreshTargetCount { get; }

        /// <summary>
        /// Contatore di aggiornamenti DirectX quando il frame è stato confermato come presentato.
        /// </summary>
        public int DirectXConfirmedRefreshCount { get; }

        /// <summary>
        /// Numero di frame DWM presentati in ritardo.
        /// </summary>
        public long DWMLateFramesCounter { get; }

        /// <summary>
        /// Numero di frame inviati ma non confermati come completati.
        /// </summary>
        public int OutstandingFramesCounter { get; }

        /// <summary>
        /// Ultimo frame visualizzato.
        /// </summary>
        public long LastDisplayedFrame { get; }

        /// <summary>
        /// Valore del contatore prestazioni del passaggio di composizione quando il frame è stato visualizzato.
        /// </summary>
        public long PCDisplayedFrame { get; }

        /// <summary>
        /// Contatore di aggiornamenti verticali quando il frame dovrebbe essere diventato visibile.
        /// </summary>
        public long VRefreshCountFrameVisible { get; }

        /// <summary>
        /// ID dell'ultimo frame indicato come completato.
        /// </summary>
        public long CompletedFrameID { get; }

        /// <summary>
        /// Valore del contatore prestazioni di quando l'ultimo frame è stato indicato come completato.
        /// </summary>
        public long PCCompletedFrame { get; }

        /// <summary>
        /// ID dell'ultimo frame indicato come "in corso".
        /// </summary>
        public long PendingFrameID { get; }

        /// <summary>
        /// Valore del contatore prestazioni di quando l'ultimo frame è stato indicato come "in corso".
        /// </summary>
        public long PCPendingFrame { get; }

        /// <summary>
        /// Numero di frame unici visualizzati.
        /// </summary>
        public long FramesDisplayed { get; }

        /// <summary>
        /// Numero di nuovi frame completati che sono stati ricevuti.
        /// </summary>
        public long NewCompletedFrames { get; }

        /// <summary>
        /// Numero di nuovi frame inviati a DirectX ma non ancora completati.
        /// </summary>
        public long NewPendingFrames { get; }

        /// <summary>
        /// Numero di frame disponibili ma non visualizzati, usati o ignorati.
        /// </summary>
        public long AvailableFrames { get; }

        /// <summary>
        /// Numero di frame renderizzati che non sono mai stati visualizzati perché la composizione è avvenuta troppo tardi.
        /// </summary>
        public long DroppedFrames { get; }

        /// <summary>
        /// Numero di volte che un vecchio frame è stato composto quando un nuovo frame doveva essere usato ma non era disponibile.
        /// </summary>
        public long MissedFrames { get; }

        /// <summary>
        /// Conteggio frame al quale il prossimo frame deve essere visualizzato.
        /// </summary>
        public long NextFrameCount { get; }

        /// <summary>
        /// Conteggio frame al quale la prossima presentazione DirectX deve essere visualizzata.
        /// </summary>
        public long DirectXPresentFrameCount { get; }

        /// <summary>
        /// Numero totale di aggiornamenti visualizzati per l'applicazione dall'ultima impostazione dei parametri di presentazione.
        /// </summary>
        public long TotalDisplayedRefreshes { get; }

        /// <summary>
        /// Numero totale di aggiornamenti visualizzati per l'applicazione dall'ultima impostazione dei parametri di presentazione.
        /// </summary>
        public long TotalPresentedRefreshes;

        /// <summary>
        /// Il numero di aggiornamenti di quando la visualizzazione del contenuto della finestra è iniziato.
        /// </summary>
        public long StartingRefreshNumber;

        /// <summary>
        /// Numero totale di pixel DirectX ridirezionati a DWM.
        /// </summary>
        public long TotalPixelDirectXToDWM;

        /// <summary>
        /// Numero di pixel disegnati.
        /// </summary>
        public long DrawnPixels;

        /// <summary>
        /// Numero di buffer vuoti nella flip chain.
        /// </summary>
        public long FlipChainEmptyBuffers;

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DWMTimingInfo"/>.
        /// </summary>
        /// <param name="Data">Struttura <see cref="DWM_TIMING_INFO"/> con i dati.</param>
        internal DWMTimingInfo(DWM_TIMING_INFO Data)
        {
            MonitorRefreshRate = Data.RefreshRate.Numerator / Data.RefreshRate.Denominator;
            PCRefreshPeriod = (long)Data.PerformanceCounterRefreshPeriod;
            CompositionRate = Data.CompositionRate.Numerator / Data.CompositionRate.Denominator;
            PCVerticalBlank = (long)Data.PerformanceCounterVerticalBlank;
            DWMRefreshCounter = (long)Data.DWMRefreshCounter;
            DirectXRefreshCounter = (int)Data.DirectXRefreshCounter;
            PCFrameComposition = (long)Data.PerformanceCounterCompositionFrame;
            FrameNumber = (long)Data.FrameNumber;
            DirectXPresentNumber = (int)Data.DirectxPresentNumber;
            FrameRefreshCount = (long)Data.FrameRefreshCount;
            DWMLastSubmittedFrameNumber = (long)Data.DWMLastSubmittedFrameNumber;
            DirectXPresentNumberSubmitted = (int)Data.DirectXPresentNumberSubmitted;
            DWMConfirmedFrameNumber = (long)Data.DWMConfirmedFrameNumber;
            DirectXConfirmedPresentNumber = (int)Data.DirectXConfirmedPresentNumber;
            LastFrameRefreshTargetCount = (long)Data.LastFrameRefreshTargetCount;
            DirectXConfirmedRefreshCount = (int)Data.DirectXConfirmedRefreshCount;
            DWMLateFramesCounter = (long)Data.DWMLateFramesCounter;
            OutstandingFramesCounter = (int)Data.OutstandingFramesCounter;
            LastDisplayedFrame = (long)Data.LastDisplayedFrame;
            PCDisplayedFrame = (long)Data.PerformanceCounterDisplayedFrame;
            VRefreshCountFrameVisible = (long)Data.VRefreshCountFrameVisible;
            CompletedFrameID = (long)Data.CompletedFrameID;
            PCCompletedFrame = (long)Data.PerformanceCounterCompletedFrame;
            PendingFrameID = (long)Data.PendingFrameID;
            PCPendingFrame = (long)Data.PerformanceCounterPendingFrame;
            FramesDisplayed = (long)Data.FramesDisplayed;
            NewCompletedFrames = (long)Data.NewCompletedFrames;
            NewPendingFrames = (long)Data.NewPendingFrames;
            AvailableFrames = (long)Data.AvailableFrames;
            DroppedFrames = (long)Data.DroppedFrames;
            MissedFrames = (long)Data.MissedFrames;
            NextFrameCount = (long)Data.NextFrameCount;
            DirectXPresentFrameCount = (long)Data.DirectXPresentFrameCount;
            TotalDisplayedRefreshes = (long)Data.TotalDisplayedRefreshes;
            TotalPresentedRefreshes = (long)Data.TotalPresentedRefreshes;
            StartingRefreshNumber = (long)Data.StartingRefreshNumber;
            TotalPixelDirectXToDWM = (long)Data.TotalPixelDirectXToDWM;
            DrawnPixels = (long)Data.DrawnPixels;
            FlipChainEmptyBuffers = (long)Data.FlipChainEmptyBuffers;
        }
    }
}