using System;

namespace WindowsAPI.HighDPIWrapper.Native
{
    /// <summary>
    /// Enumerazioni relative alle funzionalità High DPI.
    /// </summary>
    internal static class HighDPIEnumerations
    {
        /// <summary>
        /// Impostazioni DPI per un thread, un processo o una finestra.
        /// </summary>
        internal enum DPI_AWARENESS
        {
            /// <summary>
            /// Valore non valido.
            /// </summary>
            DPI_AWARENESS_INVALID = -1,
            /// <summary>
            /// Non tiene conto dei DPI.
            /// </summary>
            DPI_AWARENESS_UNAWARE,
            /// <summary>
            /// Tiene conto dei DPI di sistema.
            /// </summary>
            DPI_AWARENESS_SYSTEM_AWARE,
            /// <summary>
            /// Tiene conto dei DPI per monitor.
            /// </summary>
            DPI_AWARENESS_PER_MONITOR_AWARE
        }

        /// <summary>
        /// Impostazioni DPI di un monitor.
        /// </summary>
        internal enum MONITOR_DPI_TYPE
        {
            /// <summary>
            /// DPI effettivi.
            /// </summary>
            /// <remarks>Questo valore dovrebbe essere utilizzato per determinare il corretto fattore di scala per adattare gli elementi UI.<br/>
            /// Incorpora il fattore di scala impostato dall'utente per il display specifico.</remarks>
            MDT_EFFECTIVE_DPI,
            /// <summary>
            /// DPI angolare.
            /// </summary>
            /// <remarks>Questo valore DPI permette il rendering ad una risoluzione angolare valida dello schermo.<br/>
            /// Non include il fattore di scala impostato dall'utente per il display specifico.</remarks>
            MDT_ANGULAR_DPI,
            /// <summary>
            /// DPI grezzo.
            /// </summary>
            /// <remarks>Questo valore sono i DPI lineari dello schermo come misurato su di esso.<br/>
            /// Questo valore deovrebbe essere utilizzato per leggere la densità dei pixel e non per impostare il fattore di scala raccomandato.<br/>
            /// Non include il fattore di scala impostato dall'utente per il display specifico e non garantito che sia un valore DPI valido.</remarks>
            MDT_RAW_DPI,
            /// <summary>
            /// Impostazione predefinita.
            /// </summary>
            MDT_DEFAULT = MDT_EFFECTIVE_DPI
        }

        /// <summary>
        /// Identifica il modo in cui un processo tiene conto dei DPI
        /// </summary>
        internal enum PROCESS_DPI_AWARENESS
        {
            /// <summary>
            /// Non tiene conto dei DPI.
            /// </summary>
            /// <remarks>L'applicazione non reagisce a cambiamenti nei DPI e ha sempre un fattore di scala di 100% (96 DPI).</remarks>
            PROCESS_DPI_UNAWARE,
            /// <summary>
            /// Tiene conto dei DPI del sistema.
            /// </summary>
            /// <remarks>L'applicazione non reagisce a cambiamenti dei DPI.<br/>
            /// L'applicazione richiede il valore dei DPI una volta e lo usa per tutta la sua durata di esecuzione.<br/>
            /// Il sistema adatterà automaticamente l'applicazione quando il valore DPI cambia da quello di sistema.</remarks>
            PROCESS_SYSTEM_DPI_AWARE,
            /// <summary>
            /// Tiene conto dei DPI per monitor.
            /// </summary>
            /// <remarks>L'applicazione controlla il valore dei DPI quando vine creata e adatta il suo fattore di scala quando esso cambia.<br/>
            /// Queste applicazioni non sono automaticamente adattate dal sistema.</remarks>
            PROCESS_PER_MONITOR_DPI_AWARE
        }

        /// <summary>
        /// Modo in cui le finestre di dialogo reagiscono ai cambiamenti nei DPI.
        /// </summary>
        [Flags]
        internal enum DIALOG_DPI_CHANGE_BEHAVIORS
        {
            /// <summary>
            /// Comportamento predefinito del gestore dialoghi.
            /// </summary>
            /// <remarks>In risposta a un cambiamento nei DPI, il gestore dialoghi ridistribuisce i controlli, aggiorna il font in ognuno di essi, ridimensiona la finestra e aggiorna il font del dialogo.</remarks>
            DDC_DEFAULT,
            /// <summary>
            /// Impedisce al gestore dialoghi di rispondere ai messaggi <see cref="Window.WindowMessages.WM_GETDPISCALEDSIZE"/> e <see cref="Window.WindowMessages.WM_DPICHANGED"/>, disabilitando tutti i comportamenti relativi ai DPI.
            /// </summary>
            DDC_DISABLE_ALL,
            /// <summary>
            /// Impedisce al gestore dialoghi di ridimensionare la finestra in risposta a un cambiamento nei DPI.
            /// </summary>
            DDC_DISABLE_RESIZE,
            /// <summary>
            /// Impedisce al gestore dialoghi ridistribuire tutti i figli diretti del dialogo in risposta a un cambiamenti nei DPI.
            /// </summary>
            DDC_DISABLE_CONTROL_RELAYOUT = 4
        }

        /// <summary>
        /// Modo in cui le finestre figlie di un dialogo reagiscono ai cambiamenti nei DPI.
        /// </summary>
        [Flags]
        internal enum DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS
        {
            /// <summary>
            /// Comportamento predefinito del gestore dialoghi.
            /// </summary>
            /// <remarks>In risposta a un cambiamento dei DPI, il gestore dialoghi aggiornerà il font, la dimensione e la posizione delle finestre figlie.</remarks>
            DCDC_DEFAULT,
            /// <summary>
            /// Impedisce al gestore dialoghi di inviare un font aggiornato alle finestre figlie tramite il messaggio <see cref="Window.WindowMessages.WM_SETFONT"/>.
            /// </summary>
            DCDC_DISABLE_FONT_UPDATE,
            /// <summary>
            /// Impedisce al gestore dialoghi di ridimensionare e riposizionare le finestre figlie.
            /// </summary>
            DCDC_DISABLE_RELAYOUT
        }

        /// <summary>
        /// Identifica il comportamento di hosting DPI per una finestra.
        /// </summary>
        internal enum DPI_HOSTING_BEHAVIOR
        {
            /// <summary>
            /// Comportamento non valido.
            /// </summary>
            DPI_HOSTING_BEHAVIOR_INVALID = -1,
            /// <summary>
            /// Comportamento predefinito.
            /// </summary>
            /// <remarks>La finestra si comporta normalmente, non può creare o cambiare il padre delle finestre figlie con un DPI_AWARENESS_CONTEXT diverso.</remarks>
            DPI_HOSTING_BEHAVIOR_DEFAULT,
            /// <summary>
            /// Comportamento misto.
            /// </summary>
            /// <remarks>Permette la creazione e il cambiamento del padre delle finestre figlie con un DPI_AWARENESS_CONTEXT diverso.</remarks>
            DPI_HOSTING_BEHAVIOR_MIXED
        }

        /// <summary>
        /// Parametri di sistema adattabili in base ai DPI.
        /// </summary>
        internal enum SystemParametersDPI
        {
            /// <summary>
            /// Recupera le metriche associate con le icone.
            /// </summary>
            SPI_GETICONMETRICS = 45,
            /// <summary>
            /// Recupera le informazioni sul font logico attuale per le icone.
            /// </summary>
            SPI_GETICONTITLEFONT = 31,
            /// <summary>
            /// Recupera le metriche associate con l'area non client di finestre non minimizzate.
            /// </summary>
            SPI_GETNONCLIENTMETRICS = 41
        }
    }
}