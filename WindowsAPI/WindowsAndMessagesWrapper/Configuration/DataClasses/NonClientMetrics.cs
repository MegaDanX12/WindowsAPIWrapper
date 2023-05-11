using WindowsAPI.WindowsGDIWrapper.FontAndText.DataClasses;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration.ConfigurationStructures;

namespace WindowsAPI.WindowsAndMessagesWrapper.Configuration.DataClasses
{
    /// <summary>
    /// Metriche scalabili associate con l'area non client di una finestra non minimizzata.
    /// </summary>
    public class NonClientMetrics
    {
        /// <summary>
        /// Spessore del bordo di ridimensionamento, in pixel.
        /// </summary>
        public int SizingBorderThickness { get; }

        /// <summary>
        /// Larghezza di una barra di scorrimento verticale standard, in pixel.
        /// </summary>
        public int VScrollBarWidth { get; }

        /// <summary>
        /// Altezza di una barra di scorrimento orizzontale standard, in pixel.
        /// </summary>
        public int HScrollBarHeight { get; }

        /// <summary>
        /// Larghezza dei pulsanti della barra del titolo, in pixel.
        /// </summary>
        public int CaptionButtonsWidth { get; }

        /// <summary>
        /// Altezza dei pulsanti della barra del titolo, in pixel.
        /// </summary>
        public int CaptionButtonsHeight { get; }

        /// <summary>
        /// Font usato per la barra del titolo.
        /// </summary>
        public FontAttributes CaptionFont { get; }

        /// <summary>
        /// Larghezza dei pulsanti piccoli della barra del titolo, in pixel.
        /// </summary>
        public int SmallCaptionButtonsWidth { get; }

        /// <summary>
        /// Altezza dei pulsanti piccoli della barra del titolo, in pixel.
        /// </summary>
        public int SmallCaptionButtonsHeight { get; }

        /// <summary>
        /// Font usata per la barra del titolo piccola.
        /// </summary>
        public FontAttributes SmallCaptionFont { get; }

        /// <summary>
        /// Larghezza dei pulsanti della barra dei menù, in pixel.
        /// </summary>
        public int MenuButtonsWidth { get; }

        /// <summary>
        /// Altezza dei pulsanti della barra dei menù, in pixel.
        /// </summary>
        public int MenuButtonsHeight { get; }

        /// <summary>
        /// Font usato nelle barre dei menù.
        /// </summary>
        public FontAttributes MenuFont { get; }

        /// <summary>
        /// Font usato nelle barre di stato e nei tooltip.
        /// </summary>
        public FontAttributes StatusBarFont { get; }

        /// <summary>
        /// Font usato nelle finestre di messaggio.
        /// </summary>
        public FontAttributes MessageBoxesFont { get; }

        /// <summary>
        /// Spessore del bordo riempito, in pixel.
        /// </summary>
        public int PaddedBorderThickness { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="NonClientMetrics"/>.
        /// </summary>
        /// <param name="Data">Struttura <see cref="NONCLIENTMETRICS"/> con le informazioni.</param>
        internal NonClientMetrics(NONCLIENTMETRICS Data)
        {
            SizingBorderThickness = Data.SizingBorderWidth;
            VScrollBarWidth = Data.VScrollBarWidth;
            HScrollBarHeight = Data.HScrollBarHeight;
            CaptionButtonsHeight = Data.CaptionButtonHeight;
            CaptionButtonsWidth = Data.CaptionButtonWidth;
            CaptionFont = new FontAttributes(Data.CaptionFont);
            SmallCaptionButtonsWidth = Data.SmallCaptionButtonWidth;
            SmallCaptionButtonsHeight = Data.SmallCaptionButtonHeight;
            SmallCaptionFont = new FontAttributes(Data.SmallCaptionFont);
            MenuButtonsWidth = Data.MenuWidth;
            MenuButtonsHeight = Data.MenuHeight;
            MenuFont = new FontAttributes(Data.MenuFont);
            StatusBarFont = new FontAttributes(Data.StatusBarFont);
            MessageBoxesFont = new FontAttributes(Data.MessageBoxesFont);
            PaddedBorderThickness = Data.PaddedBorderThickness;
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="NonClientMetrics/>.
        /// </summary>
        /// <param name="sizingBorderThickness">Spessore del bordo di ridimensionamento, in pixel.</param>
        /// <param name="vScrollBarWidth">Larghezza di una barra di scorrimento verticale standard, in pixel.</param>
        /// <param name="hScrollBarHeight">Altezza di una barra di scorrimento orizzontale standard, in pixel.</param>
        /// <param name="captionButtonsWidth">Larghezza dei pulsanti della barra del titolo, in pixel.</param>
        /// <param name="captionButtonsHeight">Altezza dei pulsanti della barra del titolo, in pixel.</param>
        /// <param name="captionFont">Font usato per la barra del titolo.</param>
        /// <param name="smallCaptionButtonsWidth">Larghezza dei pulsanti piccoli della barra del titolo, in pixel.</param>
        /// <param name="smallCaptionButtonsHeight">Altezza dei pulsanti piccoli della barra del titolo, in pixel.</param>
        /// <param name="smallCaptionFont">Font usata per la barra del titolo piccola.</param>
        /// <param name="menuButtonsWidth">Larghezza dei pulsanti della barra dei menù, in pixel.</param>
        /// <param name="menuButtonsHeight">Altezza dei pulsanti della barra dei menù, in pixel.</param>
        /// <param name="menuFont">Font usato nelle barre dei menù.</param>
        /// <param name="statusBarFont">Font usato nelle barre di stato e nei tooltip.</param>
        /// <param name="messageBoxesFont">Font usato nelle finestre di messaggio.</param>
        /// <param name="paddedBorderThickness">Spessore del bordo riempito, in pixel.</param>
        public NonClientMetrics(int sizingBorderThickness, int vScrollBarWidth, int hScrollBarHeight, int captionButtonsWidth, int captionButtonsHeight, FontAttributes captionFont, int smallCaptionButtonsWidth, int smallCaptionButtonsHeight, FontAttributes smallCaptionFont, int menuButtonsWidth, int menuButtonsHeight, FontAttributes menuFont, FontAttributes statusBarFont, FontAttributes messageBoxesFont, int paddedBorderThickness)
        {
            SizingBorderThickness = sizingBorderThickness;
            VScrollBarWidth = vScrollBarWidth;
            HScrollBarHeight = hScrollBarHeight;
            CaptionButtonsWidth = captionButtonsWidth;
            CaptionButtonsHeight = captionButtonsHeight;
            CaptionFont = captionFont;
            SmallCaptionButtonsWidth = smallCaptionButtonsWidth;
            SmallCaptionButtonsHeight = smallCaptionButtonsHeight;
            SmallCaptionFont = smallCaptionFont;
            MenuButtonsWidth = menuButtonsWidth;
            MenuButtonsHeight = menuButtonsHeight;
            MenuFont = menuFont;
            StatusBarFont = statusBarFont;
            MessageBoxesFont = messageBoxesFont;
            PaddedBorderThickness = paddedBorderThickness;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="NONCLIENTMETRICS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="NONCLIENTMETRICS"/> risultato della conversione.</returns>
        internal NONCLIENTMETRICS ToStructure()
        {
            NONCLIENTMETRICS Structure = new NONCLIENTMETRICS()
            {
                SizingBorderWidth = SizingBorderThickness,
                VScrollBarWidth = VScrollBarWidth,
                HScrollBarHeight = HScrollBarHeight,
                CaptionButtonWidth = CaptionButtonsWidth,
                CaptionButtonHeight = CaptionButtonsHeight,
                CaptionFont = CaptionFont.ToStructure(),
                SmallCaptionButtonWidth = SmallCaptionButtonsWidth,
                SmallCaptionButtonHeight = SmallCaptionButtonsHeight,
                SmallCaptionFont = SmallCaptionFont.ToStructure(),
                MenuWidth = MenuButtonsWidth,
                MenuHeight = MenuButtonsHeight,
                MenuFont = MenuFont.ToStructure(),
                StatusBarFont = StatusBarFont.ToStructure(),
                MessageBoxesFont = MessageBoxesFont.ToStructure(),
                PaddedBorderThickness = PaddedBorderThickness
            };
            return Structure;
        }
    }
}