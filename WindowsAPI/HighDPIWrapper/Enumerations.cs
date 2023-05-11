using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.HighDPIWrapper.Native;
using WindowsAPI.WindowsAndMessagesWrapper.Configuration;
using WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration;
using WindowsAPI.WindowsGDIWrapper.Native.FontAndText;
using static WindowsAPI.HighDPIWrapper.Native.HighDPIEnumerations;

namespace WindowsAPI.HighDPIWrapper
{
    /// <summary>
    /// Enumerazioni relative alle funzionalità associate con i valori DPI alti.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Valori DPI Awareness Context.
        /// </summary>
        public enum DPIAwarenessContext
        {
            /// <summary>
            /// Non tiene conto dei DPI.
            /// </summary>
            /// <remarks>La finestra non viene ridimensionata in caso di cambi nei DPI e si assume sempre un fattore di scala del 100% (96 DPI).</remarks>
            Unaware = -1,
            /// <summary>
            /// Tiene conto dei DPI di sistema.
            /// </summary>
            /// <remarks>La finestra controlla il valore dei DPI quando viene creata e si adatta quando tale valore cambia.<br/>
            /// Questi processi non vengono adattati automaticamente dal sistema.</remarks>
            SystemAware = -2,
            /// <summary>
            /// Tiene conto dei DPI del monitor.
            /// </summary>
            /// <remarks>La finestra controlla il valore dei DPI quando viene creata e si adatta quando tale valore cambia.<br/>
            /// Questi processi non vengono adattati automaticamente dal sistema.</remarks>
            PerMonitorAware = -3,
            /// <summary>
            /// Simile a <see cref="DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE"/>.
            /// </summary>
            /// <remarks>Questa modalità include i seguenti comportamenti:<br/><br/>
            /// Tutto l'albero delle finestre viene notificato di cambiamenti nei DPI<br/>
            /// L'area non client viene automaticamente adattata<br/>
            /// I menù NTUSER vengono automaticamente adattati<br/>
            /// Le finestre di dialogo create risponderanno a cambiamenti nei DPI<br/>
            /// Il comportamento rispetto ai DPI di diversi controlli comctl32 è stato migliorato<br/>
            /// Handle UxTheme lavoreranno in base ai DPI associati alla finestra</remarks>
            PerMonitorAwareV2 = -4,
            /// <summary>
            /// Non tiene conto dei DPI con qualità migliorata del contenuto basato su GDI.
            /// </summary>
            /// <remarks>Comportamento simile a <see cref="Unaware"/> ma permette al sistema di migliorare automaticamente la qualità del rendering di testi e di altri primitivi basata su GDI quando la finestra viene visualizzata su un monitor con un valore DPI alto.</remarks>
            UnawareGDIScaled = -5
        }

        /// <summary>
        /// Impostazioni DPI per un thread, un processo o una finestra.
        /// </summary>
        public enum DPIAwareness
        {
            /// <summary>
            /// Non valido.
            /// </summary>
            Invalid = DPI_AWARENESS.DPI_AWARENESS_INVALID,
            /// <summary>
            /// Non tiene conto dei DPI.
            /// </summary>
            Unaware = DPI_AWARENESS.DPI_AWARENESS_UNAWARE,
            /// <summary>
            /// Tiene conto dei DPI di sistema.
            /// </summary>
            SystemAware = DPI_AWARENESS.DPI_AWARENESS_SYSTEM_AWARE,
            /// <summary>
            /// Tiene conto dei DPI per monitor.
            /// </summary>
            PerMonitorAware = DPI_AWARENESS.DPI_AWARENESS_PER_MONITOR_AWARE
        }

        /// <summary>
        /// Impostazioni DPI per un processo.
        /// </summary>
        public enum ProcessDPIAwareness
        {
            /// <summary>
            /// Non tiene conto dei DPI.
            /// </summary>
            Unaware = PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE,
            /// <summary>
            /// Tiene conto dei DPI di sistema.
            /// </summary>
            SystemAware = PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE,
            /// <summary>
            /// Tiene conto dei DPI per monitor.
            /// </summary>
            PerMonitorAware = PROCESS_DPI_AWARENESS.PROCESS_PER_MONITOR_DPI_AWARE
        }

        /// <summary>
        /// Impostazione DPI per un monitor.
        /// </summary>
        public enum MonitorDPIType
        {
            /// <summary>
            /// DPI effettivi.
            /// </summary>
            /// <remarks>Questo valore dovrebbe essere utilizzato per determinare il corretto fattore di scala per adattare gli elementi UI.<br/>
            /// Incorpora il fattore di scala impostato dall'utente per il display specifico.</remarks>
            Effective = MONITOR_DPI_TYPE.MDT_EFFECTIVE_DPI,
            /// <summary>
            /// DPI angolare.
            /// </summary>
            /// <remarks>Questo valore DPI permette il rendering ad una risoluzione angolare valida dello schermo.<br/>
            /// Non include il fattore di scala impostato dall'utente per il display specifico.</remarks>
            Angular = MONITOR_DPI_TYPE.MDT_ANGULAR_DPI,
            /// <summary>
            /// DPI grezzo.
            /// </summary>
            /// <remarks>Questo valore sono i DPI lineari dello schermo come misurato su di esso.<br/>
            /// Questo valore deovrebbe essere utilizzato per leggere la densità dei pixel e non per impostare il fattore di scala raccomandato.<br/>
            /// Non include il fattore di scala impostato dall'utente per il display specifico e non garantito che sia un valore DPI valido.</remarks>
            Raw = MONITOR_DPI_TYPE.MDT_RAW_DPI,
            /// <summary>
            /// Impostazione predefinita.
            /// </summary>
            Default = Effective
        }

        /// <summary>
        /// Metriche di sistema adattabili ai DPI.
        /// </summary>
        public enum DPIMetric
        {
            /// <summary>
            /// Larghezza del bordo di una finestra, in pixel.
            /// </summary>
            WindowBorderWidth = ConfigurationEnumerations.SystemMetric.SM_CXBORDER,
            /// <summary>
            /// Larghezza, di un bordo 3D, in pixel.
            /// </summary>
            Border3DWidth = ConfigurationEnumerations.SystemMetric.SM_CXEDGE,
            /// <summary>
            /// Spessore della cornice attorno al perimetro di una finestra che ha una barra del titolo ma non è ridimensionabile, in pixel.
            /// </summary>
            /// <remarks>Questo valore rappresenta l'altezza del bordo orizzontale.</remarks>
            WindowFrameThicknessHorizontalHeight = ConfigurationEnumerations.SystemMetric.SM_CXFIXEDFRAME,
            /// <summary>
            /// Larghezza dei pulsanti della barra dei menù, in pixel.
            /// </summary>
            MenuBarButtonWidth = ConfigurationEnumerations.SystemMetric.SM_CXMENUSIZE,
            /// <summary>
            /// Larghezza minima di una finestra, in pixel.
            /// </summary>
            WindowMinimumWidth = ConfigurationEnumerations.SystemMetric.SM_CXMIN,
            /// <summary>
            /// Quantità di padding per finestre con una barra del titolo, in pixel.
            /// </summary>
            PaddedBorderAmount = ConfigurationEnumerations.SystemMetric.SM_CXPADDEDBORDER,
            /// <summary>
            /// Larghezza di un pulsante nella barra del titolo, in pixel.
            /// </summary>
            TitleBarButtonWidth = ConfigurationEnumerations.SystemMetric.SM_CXSIZE,
            /// <summary>
            /// Lo spessore di un bordo di ridimensionamento attorno al perimetro di una finestra che può essere ridimensionata, in pixel.
            /// </summary>
            /// <remarks>Questo valore è la larghezza del bordo orizzontale.</remarks>
            SizingBorderThicknessHorizontalWidth = ConfigurationEnumerations.SystemMetric.SM_CXSIZEFRAME,
            /// <summary>
            /// Larghezza dei pulsanti piccoli della barra del titolo, in pixel.
            /// </summary>
            SmallCaptionButtonWidth = ConfigurationEnumerations.SystemMetric.SM_CXSMSIZE,
            /// <summary>
            /// Larghezza di una barra di scorrimento verticale, in pixel.
            /// </summary>
            VerticalScrollBarWidth = ConfigurationEnumerations.SystemMetric.SM_CXVSCROLL,
            /// <summary>
            /// Altezza del bordo di una finestra, in pixel.
            /// </summary>
            WindowBorderHeight = ConfigurationEnumerations.SystemMetric.SM_CYBORDER,
            /// <summary>
            /// Altezza della barra del titolo, in pixel.
            /// </summary>
            CaptionAreaHeight = ConfigurationEnumerations.SystemMetric.SM_CYCAPTION,
            /// <summary>
            /// Altezza del bordo 3D.
            /// </summary>
            Border3DHeight = ConfigurationEnumerations.SystemMetric.SM_CYEDGE,
            /// <summary>
            /// Spessore della cornice attorno al perimetro di una finestra che ha una barra del titolo ma non è ridimensionabile, in pixel.
            /// </summary>
            /// <remarks>Questo valore è la larghezza del bordo verticale.</remarks>
            WindowFrameThicknessVerticalWidth = ConfigurationEnumerations.SystemMetric.SM_CYFIXEDFRAME,
            /// <summary>
            /// Altezza di una barra di scorrimento orizzontale, in pixel.
            /// </summary>
            HorizontalScrollBarHeight = ConfigurationEnumerations.SystemMetric.SM_CYHSCROLL,
            /// <summary>
            /// Altezza della finestra Kanji nella parte inferiore dello schermo, in pixel.
            /// </summary>
            KanjiWindowHeight = ConfigurationEnumerations.SystemMetric.SM_CYKANJIWINDOW,
            /// <summary>
            /// Altezza di una barra dei menù a singola linea, in pixel.
            /// </summary>
            SingleLineMenuBarHeight = ConfigurationEnumerations.SystemMetric.SM_CYMENU,
            /// <summary>
            /// Altezza dei pulsanti della barra dei menù.
            /// </summary>
            MenuBarButtonsHeight = ConfigurationEnumerations.SystemMetric.SM_CYMENUSIZE,
            /// <summary>
            /// Altezza minima di una finestra, in pixel.
            /// </summary>
            WindowMinimumHeight = ConfigurationEnumerations.SystemMetric.SM_CYMIN,
            /// <summary>
            /// Altezza del pulsante nella barra del titolo di una finestra, in pixel.
            /// </summary>
            TitleBarButtonHeight = ConfigurationEnumerations.SystemMetric.SM_CYSIZE,
            /// <summary>
            /// Spessore del bordo di ridimensionamento attorno al perimetro di una finestra che può essere ridimensionata, in pixel.
            /// </summary>
            /// <remarks>Questa metrica rappresenta l'altezza del bordo verticale.</remarks>
            SizingBorderThicknessVerticalHeight = ConfigurationEnumerations.SystemMetric.SM_CYSIZEFRAME,
            /// <summary>
            /// Altezza della barra del titolo piccola, in pixel.
            /// </summary>
            SmallCaptionHeight = ConfigurationEnumerations.SystemMetric.SM_CYSMCAPTION,
            /// <summary>
            /// Altezza di un pulsante di una barra del titolo piccola, in pixel.
            /// </summary>
            SmallCaptionButtonHeight = ConfigurationEnumerations.SystemMetric.SM_CYSMSIZE,
            /// <summary>
            /// Altezza di un thumb box in una barra di scorrimento verticale, in pixel.
            /// </summary>
            VScrollBarThumbBoxHeight = ConfigurationEnumerations.SystemMetric.SM_CYVTHUMB,
            /// <summary>
            /// Larghezza del thumb box nella barra di scorrimento orizzontale, in pixel.
            /// </summary>
            HScrollBarThumbBoxWidth = ConfigurationEnumerations.SystemMetric.SM_CXHTHUMB
        }

        /// <summary>
        /// Comportamento di hosting DPI di una finestra.
        /// </summary>
        public enum DpiHostingBehavior
        {
            /// <summary>
            /// Non valido.
            /// </summary>
            Invalid = DPI_HOSTING_BEHAVIOR.DPI_HOSTING_BEHAVIOR_INVALID,
            /// <summary>
            /// Comportamento predefinito.
            /// </summary>
            /// <remarks>La finestra si comporta normalmente, non può creare o cambiare il padre delle finestre figlie con un <see cref="DPIAwarenessContext"/> diverso.</remarks>
            Default = DPI_HOSTING_BEHAVIOR.DPI_HOSTING_BEHAVIOR_DEFAULT,
            /// <summary>
            /// Comportamento misto.
            /// </summary>
            /// <remarks>Permette la creazione e il cambiamento del padre delle finestre figlie con un <see cref="DPIAwarenessContext"/> diverso.</remarks>
            Mixed = DPI_HOSTING_BEHAVIOR.DPI_HOSTING_BEHAVIOR_MIXED
        }
    }
}