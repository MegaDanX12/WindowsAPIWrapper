using System.Drawing;
using static WindowsAPI.DesktopWindowManagerWrapper.Enumerations;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMEnumerations;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.DesktopWindowManagerWrapper.DataClasses
{
    /// <summary>
    /// Dati per un messaggio relativo a un'azione del mouse verificatasi nell'area non client di una finestra.
    /// </summary>
    public class NonClientMouseMessageData
    {
        /// <summary>
        /// Risultato dell'hit test.
        /// </summary>
        public HitTestReturn HitTestResult { get; }

        /// <summary>
        /// Coordinate del cursore.
        /// </summary>
        public Point CursorCoordinates { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="NonClientMouseMessageData"/>.
        /// </summary>
        /// <param name="HitTestResult">Risultato dell'hit test.</param>
        /// <param name="CursorCoordinates">Coordinate del cursore.</param>
        internal NonClientMouseMessageData(HitTestReturnValue HitTestResult, POINTS CursorCoordinates)
        {
            this.HitTestResult = (HitTestReturn)HitTestResult;
            this.CursorCoordinates = new Point(CursorCoordinates.X, CursorCoordinates.Y);
        }

        /// <summary>
        /// Converte le coordinate del cursore in una struttura <see cref="POINTS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="POINTS"/> risultato della conversione.</returns>
        internal POINTS CoordinatesToStructure()
        {
            POINTS Structure = new POINTS()
            {
                X = (short)CursorCoordinates.X,
                Y = (short)CursorCoordinates.Y
            };
            return Structure;
        }
    }
}