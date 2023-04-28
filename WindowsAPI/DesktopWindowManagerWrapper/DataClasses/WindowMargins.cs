using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.DesktopWindowManagerWrapper.DataClasses
{
    /// <summary>
    /// Definisce i margini di una finestra.
    /// </summary>
    public class WindowMargins
    {
        /// <summary>
        /// Larghezza del bordo sinistro.
        /// </summary>
        public int LeftBorderWidth { get; }

        /// <summary>
        /// Altezza del bordo superiore.
        /// </summary>
        public int TopBorderHeight { get;}

        /// <summary>
        /// Larghezza del bordo destro.
        /// </summary>
        public int RightBorderWidth { get; }

        /// <summary>
        /// Altezza del bordo inferiore.
        /// </summary>
        public int BottomBorderHeight { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="WindowMargins"/>.
        /// </summary>
        /// <param name="LeftBorderWidth">Larghezza del bordo sinistro.</param>
        /// <param name="TopBorderHeight">Altezza del bordo superiore.</param>
        /// <param name="RightBorderWidth">Larghezza del bordo destro.</param>
        /// <param name="BottomBorderHeight">Altezza del bordo inferiore.</param>
        public WindowMargins(int LeftBorderWidth, int TopBorderHeight, int RightBorderWidth, int BottomBorderHeight)
        {
            this.LeftBorderWidth = LeftBorderWidth;
            this.TopBorderHeight = TopBorderHeight;
            this.RightBorderWidth = RightBorderWidth;
            this.BottomBorderHeight = BottomBorderHeight;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="MARGINS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="MARGINS"/> risultato della conversione.</returns>
        internal MARGINS ToStructure()
        {
            MARGINS Structure = new MARGINS()
            {
                LeftBorderWidth = LeftBorderWidth,
                TopBorderHeight = TopBorderHeight,
                RightBorderWidth = RightBorderWidth,
                BottomBorderHeight = BottomBorderHeight
            };
            return Structure;
        }
    }
}