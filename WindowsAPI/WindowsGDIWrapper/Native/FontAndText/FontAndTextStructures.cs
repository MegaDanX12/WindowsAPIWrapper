using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsGDIWrapper.Native.FontAndText.FontAndTextEnumerations;
using static WindowsAPI.WindowsGDIWrapper.Native.FontAndText.FontAndTextConstants;

namespace WindowsAPI.WindowsGDIWrapper.Native.FontAndText
{
    /// <summary>
    /// Struttura relative ai font e al testo.
    /// </summary>
    internal static class FontAndTextStructures
    {
        /// <summary>
        /// Attributi di un font.
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct LOGFONT
        {
            /// <summary>
            /// Altezza, in unità logiche, della cella del carattere del font.
            /// </summary>
            /// <remarks>Se questo campo ha valore maggiore di 0, il font mapper trasforma questo valore in unità dispositivo e lo confronta con l'altezza della cella dei font disponibili.<br/>
            /// Se questo campo ha valore uguale a 0, il font mapper usa un valore predefinito per l'altezza quando cerca una corrispondenza.<br/>
            /// Se questo campo ha valore minore di 0, il font mapper trasforma questo valore in unità dispositivo e confronta il suo valore assoluto con l'altezza dei caratteri dei font disponibili.</remarks>
            public int Height;
            /// <summary>
            /// Larghezza media, in unità logiche, dei caratteri nel font.
            /// </summary>
            /// <remarks>Se questo campo ha valore 0, le proporzioni del dispositivo sono confrontate con le proporzioni della digitalizzazione dei font disponibili per trova la corrispondenza più vicina, determinata dal valore assoluto della differenza.</remarks>
            public int Width;
            /// <summary>
            /// Angolo, in decimi di grado, tra il vettore di scappamento e l'asse x del dispositivo.
            /// </summary>
            public int Escapement;
            /// <summary>
            /// Angolo, in decimi di grado, tra la linea base di ogni carattere e l'asse x del dispositivo.
            /// </summary>
            public int Orientation;
            /// <summary>
            /// Spessore del font.
            /// </summary>
            public FontWeight Weight;
            /// <summary>
            /// Indica se il font è corsivo.
            /// </summary>
            [MarshalAs(UnmanagedType.U1)]
            public bool Italic;
            /// <summary>
            /// Indica se il font è sottolineato.
            /// </summary>
            [MarshalAs(UnmanagedType.U1)]
            public bool Underline;
            /// <summary>
            /// Indica se il font è sbarrato.
            /// </summary>
            [MarshalAs(UnmanagedType.U1)]
            public bool StrikeOut;
            /// <summary>
            /// Set di caratteri.
            /// </summary>
            public Charset Charset;
            /// <summary>
            /// Precisione dell'output.
            /// </summary>
            public OutputPrecision OutputPrecision;
            /// <summary>
            /// Precisione di taglio.
            /// </summary>
            public ClippingPrecision ClippingPrecision;
            /// <summary>
            /// Qualità dell'output.
            /// </summary>
            public OutputQuality OutputQuality;
            /// <summary>
            /// Inclinazione e famiglia del font.
            /// </summary>
            public FontPitchAndFamily PitchAndFamily;
            /// <summary>
            /// Nome del font.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = LF_FACESIZE)]
            public string FaceName;
        }
    }
}