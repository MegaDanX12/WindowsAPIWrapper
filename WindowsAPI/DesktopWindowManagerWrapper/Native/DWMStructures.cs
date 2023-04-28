using System.Runtime.InteropServices;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMEnumerations;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.DesktopWindowManagerWrapper.Native
{
    /// <summary>
    /// Strutture DWM.
    /// </summary>
    internal static class DWMStructures
    {
        /// <summary>
        /// Rappresenta una generica frazione.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct UNSIGNED_RATIO
        {
            /// <summary>
            /// Numeratore della frazione.
            /// </summary>
            public uint Numerator;
            /// <summary>
            /// Denominatore della frazione.
            /// </summary>
            public uint Denominator;
        }

        /// <summary>
        /// Informazioni sulle tempistiche della composizione DWM.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct DWM_TIMING_INFO
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Frequenza di aggiornamento del monitor.
            /// </summary>
            public UNSIGNED_RATIO RefreshRate;
            /// <summary>
            /// Periodo di aggiornamento del monitor.
            /// </summary>
            public ulong PerformanceCounterRefreshPeriod;
            /// <summary>
            /// Frequenza di composizione.
            /// </summary>
            public UNSIGNED_RATIO CompositionRate;
            /// <summary>
            /// Valore del contatore prestazioni prima del vertical blank.
            /// </summary>
            public ulong PerformanceCounterVerticalBlank;
            /// <summary>
            /// Contatore di aggiornamenti DWM.
            /// </summary>
            public ulong DWMRefreshCounter;
            /// <summary>
            /// Contatore di aggiornamento DirectX.
            /// </summary>
            public uint DirectXRefreshCounter;
            /// <summary>
            /// Valore del contatore prestazioni per una composizione di un frame.
            /// </summary>
            public ulong PerformanceCounterCompositionFrame;
            /// <summary>
            /// Numero del frame composto al momento indicato da <see cref="PerformanceCounterCompositionFrame"/>.
            /// </summary>
            public ulong FrameNumber;
            /// <summary>
            /// Numero di presentazione DirectX usato per identificare i frame in corso di rendering.
            /// </summary>
            public uint DirectxPresentNumber;
            /// <summary>
            /// Il contatore di aggiornamento del frame composto al momento indicato da <see cref="PerformanceCounterCompositionFrame"/>.
            /// </summary>
            public ulong FrameRefreshCount;
            /// <summary>
            /// Numero del frame DWM inviato per ultimo.
            /// </summary>
            public ulong DWMLastSubmittedFrameNumber;
            /// <summary>
            /// Numero di presentazione DirectX inviato per ultimo.
            /// </summary>
            public uint DirectXPresentNumberSubmitted;
            /// <summary>
            /// Numero dell'ultimo frame DWM confermato come presentato.
            /// </summary>
            public ulong DWMConfirmedFrameNumber;
            /// <summary>
            /// Ultimo numero di presentazione DirectX confermato come presentato.
            /// </summary>
            public uint DirectXConfirmedPresentNumber;
            /// <summary>
            /// Conteggio aggiornamenti obbiettivo dell'ultimo frame confermato come completato dalla GPU.
            /// </summary>
            public ulong LastFrameRefreshTargetCount;
            /// <summary>
            /// Contatore di aggiornamenti DirectX quando il frame è stato confermato come presentato.
            /// </summary>
            public uint DirectXConfirmedRefreshCount;
            /// <summary>
            /// Numero di frame DWM presentati in ritardo.
            /// </summary>
            public ulong DWMLateFramesCounter;
            /// <summary>
            /// Numero di frame inviato ma non confermati come completati.
            /// </summary>
            public uint OutstandingFramesCounter;
            /// <summary>
            /// Ultimo frame visualizzato.
            /// </summary>
            public ulong LastDisplayedFrame;
            /// <summary>
            /// Valore del contatore prestazioni del passaggio di composizione quando il frame è stato visualizzato.
            /// </summary>
            public ulong PerformanceCounterDisplayedFrame;
            /// <summary>
            /// Contatore di aggiornamenti verticali quando il frame dovrebbe essere diventato visibile.
            /// </summary>
            public ulong VRefreshCountFrameVisible;
            /// <summary>
            /// ID dell'ultimo frame indicato come completato.
            /// </summary>
            public ulong CompletedFrameID;
            /// <summary>
            /// Valore del contatore prestazioni di quando l'ultimo frame è stato indicato come completato.
            /// </summary>
            public ulong PerformanceCounterCompletedFrame;
            /// <summary>
            /// ID dell'ultimo frame indicato come "in corso".
            /// </summary>
            public ulong PendingFrameID;
            /// <summary>
            /// Valore del contatore prestazioni di quando l'ultimo frame è stato indicato come "in corso".
            /// </summary>
            public ulong PerformanceCounterPendingFrame;
            /// <summary>
            /// Numero di frame unici visualizzati.
            /// </summary>
            public ulong FramesDisplayed;
            /// <summary>
            /// Numero di nuovi frame completati che sono stati ricevuti.
            /// </summary>
            public ulong NewCompletedFrames;
            /// <summary>
            /// Numero di nuovi frame inviati a DirectX ma non ancora completati.
            /// </summary>
            public ulong NewPendingFrames;
            /// <summary>
            /// Numero di frame disponibili ma non visualizzati, usati o ignorati.
            /// </summary>
            public ulong AvailableFrames;
            /// <summary>
            /// Numero di frame renderizzati che non sono mai stati visualizzati perché la composizione è avvenuta troppo tardi.
            /// </summary>
            public ulong DroppedFrames;
            /// <summary>
            /// Numero di volte che un vecchio frame è stato composto quando un nuovo frame doveva essere usato ma non era disponibile.
            /// </summary>
            public ulong MissedFrames;
            /// <summary>
            /// Conteggio frame al quale il prossimo frame deve essere visualizzato.
            /// </summary>
            public ulong NextFrameCount;
            /// <summary>
            /// Conteggio frame al quale la prossima presentazione DirectX deve essere visualizzata.
            /// </summary>
            public ulong DirectXPresentFrameCount;
            /// <summary>
            /// Numero totale di aggiornamenti visualizzati per l'applicazione dall'ultima impostazione dei parametri di presentazione.
            /// </summary>
            public ulong TotalDisplayedRefreshes;
            /// <summary>
            /// Numero totale di aggiornamenti visualizzati per l'applicazione dall'ultima impostazione dei parametri di presentazione.
            /// </summary>
            public ulong TotalPresentedRefreshes;
            /// <summary>
            /// Il numero di aggiornamenti di quando la visualizzazione del contenuto della finestra è iniziato.
            /// </summary>
            public ulong StartingRefreshNumber;
            /// <summary>
            /// Numero totale di pixel DirectX ridirezionati a DWM.
            /// </summary>
            public ulong TotalPixelDirectXToDWM;
            /// <summary>
            /// Numero di pixel disegnati.
            /// </summary>
            public ulong DrawnPixels;
            /// <summary>
            /// Numero di buffer vuoti nella flip chain.
            /// </summary>
            public ulong FlipChainEmptyBuffers;
        }

        /// <summary>
        /// Proprietà della miniatura DWM.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct DWM_THUMBNAIL_PROPERTIES
        {
            /// <summary>
            /// Membri della struttura con valori validi.
            /// </summary>
            public DWM_TNP ValidFields;
            /// <summary>
            /// Area nella finestra di destinazione dove la miniatura verrà visualizzata.
            /// </summary>
            public RECT DestinationRectangle;
            /// <summary>
            /// Area della finestra di origine da usare come miniature.
            /// </summary>
            /// <remarks>Per impostazione predefinita, l'intera finestra viene utilizzata.</remarks>
            public RECT SourceRectangle;
            /// <summary>
            /// Opacità da usare per il rendering della miniatura.
            /// </summary>
            /// <remarks>0 significa completamente trasparente, 255 significa completamente opaco, quest'ultimo è il valore predefinito.</remarks>
            public byte Opacity;
            /// <summary>
            /// Indica se la miniatura è visibile.
            /// </summary>
            /// <remarks>false è il valore predefinito.</remarks>
            [MarshalAs(UnmanagedType.Bool)]
            public bool Visible;
            /// <summary>
            /// Indica se usare solamente l'area client della finestra di origine.
            /// </summary>
            /// <remarks>false è il valore predefinito.</remarks>
            [MarshalAs(UnmanagedType.Bool)]
            public bool OnlyClientArea;
        }
    }
}