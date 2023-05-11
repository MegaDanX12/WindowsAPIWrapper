using System;

namespace WindowsAPI.HighDPIWrapper.Native
{
    /// <summary>
    /// Costanti usati per contesti a valori DPI alti.
    /// </summary>
    internal static class HighDPIConstants
    {
        /// <summary>
        /// Non tiene conto dei DPI.
        /// </summary>
        /// <remarks>La finestra non viene ridimensionata in caso di cambi nei DPI e si assume sempre un fattore di scala del 100% (96 DPI).</remarks>
        internal static readonly IntPtr DPI_AWARENESS_CONTEXT_UNAWARE = new IntPtr(-1);

        /// <summary>
        /// Tiene conto dei DPI di sistema.
        /// </summary>
        /// <remarks>La finestra controlla il valore dei DPI quando viene creata e si adatta quando tale valore cambia.<br/>
        /// Questi processi non vengono adattati automaticamente dal sistema.</remarks>
        internal static readonly IntPtr DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = new IntPtr(-2);

        /// <summary>
        /// Tiene conto dei DPI del monitor.
        /// </summary>
        /// <remarks>La finestra controlla il valore dei DPI quando viene creata e si adatta quando tale valore cambia.<br/>
        /// Questi processi non vengono adattati automaticamente dal sistema.</remarks>
        internal static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = new IntPtr(-3);

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
        internal static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);

        /// <summary>
        /// Non tiene conto dei DPI con qualità migliorata del contenuto basato su GDI.
        /// </summary>
        /// <remarks>Comportamento simile a <see cref="DPI_AWARENESS_CONTEXT_UNAWARE"/> ma permette al sistema di migliorare automaticamente la qualità del rendering di testi e di altri primitivi basata su GDI quando la finestra viene visualizzata su un monitor con un valore DPI alto.</remarks>
        internal static readonly IntPtr DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED = new IntPtr(-5);
    }
}