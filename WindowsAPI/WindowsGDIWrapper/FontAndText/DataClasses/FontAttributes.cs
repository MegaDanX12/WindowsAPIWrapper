using WindowsAPI.WindowsGDIWrapper.Native.FontAndText;
using static WindowsAPI.WindowsGDIWrapper.FontAndText.Enumerations;
using static WindowsAPI.WindowsGDIWrapper.Native.FontAndText.FontAndTextStructures;

namespace WindowsAPI.WindowsGDIWrapper.FontAndText.DataClasses
{
    /// <summary>
    /// Attributi di un font.
    /// </summary>
    public class FontAttributes
    {
        /// <summary>
        /// Dimensioni dei caratteri del font.
        /// </summary>
        public System.Drawing.Size FontSize { get; }

        /// <summary>
        /// Angolo, in decimi di grado, tra il vettore di scappamento e l'asse x del dispositivo.
        /// </summary>
        public int Escapement { get; }

        /// <summary>
        /// Angolo, in decimi di grado, tra la linea base di ogni carattere e l'asse x del dispositivo.
        /// </summary>
        public int Orientation { get; }

        /// <summary>
        /// Spessore del font.
        /// </summary>
        public FontWeight Weight { get; }

        /// <summary>
        /// Indica se il font è corsivo.
        /// </summary>
        public bool IsItalic { get; }

        /// <summary>
        /// Indica se il font è sottolineato.
        /// </summary>
        public bool IsUnderlined { get; }

        /// <summary>
        /// Indica se il font è sbarratp.
        /// </summary>
        public bool IsStrikeout { get; }

        /// <summary>
        /// Set di caratteri.
        /// </summary>
        public Charset Charset { get; }

        /// <summary>
        /// Precisione dell'output.
        /// </summary>
        public FontOutputPrecision OutputPrecision { get; }

        /// <summary>
        /// Precisione di taglio.
        /// </summary>
        public ClippingPrecision ClippingPrecision { get; }

        /// <summary>
        /// Qualità dell'output.
        /// </summary>
        public OutputQuality OutputQuality { get; }

        /// <summary>
        /// Inclinazione del font.
        /// </summary>
        public FontPitch FontPitch { get; }

        /// <summary>
        /// Famiglia del font.
        /// </summary>
        public FontFamily FontFamily { get; }

        /// <summary>
        /// Nome del font.
        /// </summary>
        public string FaceName { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="FontAttributes"/>.
        /// </summary>
        /// <param name="FontData">Struttura <see cref="LOGFONT"/> con i dati.</param>
        internal FontAttributes(LOGFONT FontData)
        {
            FontSize = new System.Drawing.Size(FontData.Width, FontData.Height);
            Escapement = FontData.Escapement;
            Orientation = FontData.Orientation;
            Weight = (FontWeight)FontData.Weight;
            IsItalic = FontData.Italic;
            IsUnderlined = FontData.Underline;
            IsStrikeout = FontData.StrikeOut;
            Charset = (Charset)FontData.Charset;
            OutputPrecision = (FontOutputPrecision)FontData.OutputPrecision;
            ClippingPrecision = (ClippingPrecision)FontData.ClippingPrecision;
            OutputQuality = (OutputQuality)FontData.OutputQuality;
            FontPitch = (FontPitch)((byte)FontData.PitchAndFamily & 3);
            FontFamily = (FontFamily)((byte)FontData.PitchAndFamily >> 4);
            FaceName = FontData.FaceName;
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="FontAttributes"/>.
        /// </summary>
        /// <param name="FontSize">Dimensioni dei caratteri del font.</param>
        /// <param name="Escapement">Angolo, in decimi di grado, tra il vettore di scappamento e l'asse x del dispositivo.</param>
        /// <param name="Orientation">Angolo, in decimi di grado, tra la linea base di ogni carattere e l'asse x del dispositivo.</param>
        /// <param name="Weight">Spessore del font.</param>
        /// <param name="Italic">Indica se il font è corsivo.</param>
        /// <param name="Underlined">Indica se il font è sottolineato.</param>
        /// <param name="Strikeout">Indica se il font è sbarrato.</param>
        /// <param name="Charset">Set di caratteri.</param>
        /// <param name="OutputPrecision">Precisione dell'output.</param>
        /// <param name="ClippingPrecision">Precisione di taglio.</param>
        /// <param name="Quality">Qualità dell'output.</param>
        /// <param name="Pitch">Inclinazione del font.</param>
        /// <param name="Family">Famiglia del font.</param>
        /// <param name="FaceName">Nome del font.</param>
        public FontAttributes(System.Drawing.Size FontSize, int Escapement, int Orientation, FontWeight Weight, bool Italic, bool Underlined, bool Strikeout, Charset Charset, FontOutputPrecision OutputPrecision, ClippingPrecision ClippingPrecision, OutputQuality Quality, FontPitch Pitch, FontFamily Family, string FaceName)
        {
            this.FontSize = FontSize;
            this.Escapement = Escapement;
            this.Orientation = Orientation;
            this.Weight = Weight;
            IsItalic = Italic;
            IsUnderlined = Underlined;
            IsStrikeout = Strikeout;
            this.Charset = Charset;
            this.OutputPrecision = OutputPrecision;
            this.ClippingPrecision = ClippingPrecision;
            OutputQuality = Quality;
            FontPitch = Pitch;
            FontFamily = Family;
            this.FaceName = FaceName;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="LOGFONT"/>.
        /// </summary>
        /// <returns>Struttura <see cref="LOGFONT"/> risultato della conversione.</returns>
        internal LOGFONT ToStructure()
        {
            LOGFONT Structure = new LOGFONT()
            {
                Height = FontSize.Height,
                Width = FontSize.Width,
                Escapement = Escapement,
                Orientation = Orientation,
                Weight = (FontAndTextEnumerations.FontWeight)Weight,
                Italic = IsItalic,
                Underline = IsUnderlined,
                StrikeOut = IsStrikeout,
                Charset = (FontAndTextEnumerations.Charset)Charset,
                OutputPrecision = (FontAndTextEnumerations.OutputPrecision)OutputPrecision,
                ClippingPrecision = (FontAndTextEnumerations.ClippingPrecision)ClippingPrecision,
                OutputQuality = (FontAndTextEnumerations.OutputQuality)OutputQuality,
                FaceName = FaceName
            };
            return Structure;
        }
    }
}