using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsGDIWrapper.Native.FontAndText
{
    /// <summary>
    /// Enumerazioni relative ai font e al testo.
    /// </summary>
    internal static class FontAndTextEnumerations
    {
        /// <summary>
        /// Spessore del font.
        /// </summary>
        internal enum FontWeight
        {
            FW_DONTCARE,
            FW_THIN = 100,
            FW_EXTRALIGHT = 200,
            FW_ULTRALIGHT = FW_EXTRALIGHT,
            FW_LIGHT = 300,
            FW_NORMAL = 400,
            FW_REGULAR = FW_NORMAL,
            FW_MEDIUM = 500,
            FW_SEMIBOLD = 600,
            FW_DEMIBOLD = FW_SEMIBOLD,
            FW_BOLD = 700,
            FW_EXTRABOLD = 800,
            FW_ULTRABOLD = FW_EXTRABOLD,
            FW_HEAVY = 900,
            FW_BLACK = FW_HEAVY
        }

        /// <summary>
        /// Set di caratteri.
        /// </summary>
        internal enum Charset : byte
        {
            ANSI_CHARSET,
            DEFAULT_CHARSET,
            SYMBOL_CHARSET,
            SHIFTJIS_CHARSET = 128,
            HANGEUL_CHARSET = 129,
            HANGUL_CHARSET = HANGEUL_CHARSET,
            GB2312_CHARSET = 134,
            CHINESEBIG5_CHARSET = 136,
            OEM_CHARSET = 255,
            JOHAB_CHARSET = 130,
            HEBREW_CHARSET = 177,
            ARABIC_CHARSET = 178,
            GREEK_CHARSET = 161,
            TURKISH_CHARSET = 162,
            VIETNAMESE_CHARSET = 163,
            THAI_CHARSET = 222,
            EASTEUROPE_CHARSET = 238,
            RUSSIAN_CHARSET = 204,
            MAC_CHARSET = 77,
            BALTIC_CHARSET = 186
        }

        /// <summary>
        /// Precisione output.
        /// </summary>
        internal enum OutputPrecision : byte
        {
            /// <summary>
            /// Comportamento predefinito.
            /// </summary>
            OUT_DEFAULT_PRECIS,
            /// <summary>
            /// Non usato dal font mapper.
            /// </summary>
            OUT_STRING_PRECIS,
            /// <summary>
            /// Non usato.
            /// </summary>
            OUT_CHARACTER_PRECIS,
            /// <summary>
            /// Non usato dal font mapper.
            /// </summary>
            OUT_STROKE_PRECIS,
            /// <summary>
            /// Scegliere font TrueType se ne esistono diversi con lo stesso nome.
            /// </summary>
            OUT_TT_PRECIS,
            /// <summary>
            /// Scegliere font dispositivo quando ne esistono diversi con lo stesso nome.
            /// </summary>
            OUT_DEVICE_PRECIS,
            /// <summary>
            /// Scegliere font raster quando ne esistono diversi con lo stesso nome.
            /// </summary>
            OUT_RASTER_PRECIS,
            /// <summary>
            /// Scegliere solo font TrueType, se non ne esistono usare il comportamento predefinito.
            /// </summary>
            OUT_TT_ONLY_PRECIS,
            /// <summary>
            /// Scegliere font TrueType e altri basati sul contorno.
            /// </summary>
            OUT_OUTLINE_PRECIS,
            /// <summary>
            /// 
            /// </summary>
            OUT_SCREEN_OUTLINE_PRECIS,
            /// <summary>
            /// Scegliere solo font PostScript, se non ne esistono usare il comportamento predefinito.
            /// </summary>
            OUT_PS_ONLY_PRECIS
        }

        /// <summary>
        /// Precisione di taglio.
        /// </summary>
        internal enum ClippingPrecision : byte
        {
            /// <summary>
            /// Comportamento predefinito.
            /// </summary>
            CLIP_DEFAULT_PRECIS,
            /// <summary>
            /// Non usato.
            /// </summary>
            CLIP_CHARACTER_PRECIS,
            /// <summary>
            /// Non usato dal font mapper.
            /// </summary>
            CLIP_STROKE_PRECIS,
            /// <summary>
            /// Non usato.
            /// </summary>
            CLIP_MASK = 15,
            /// <summary>
            /// La rotazione dei font dipende dall'orientamento del sistema di coordinate.
            /// </summary>
            /// <remarks>Se questo valore non è usato, i font dispositivo vengono sempre ruotati in senso antiorario, mentre per gli altri dipende dall'orientamento del sistema di coordinate.</remarks>
            CLIP_LH_ANGLES = 1 << 4,
            /// <summary>
            /// Non usato.
            /// </summary>
            CLIP_TT_ALWAYS = 2 << 4,
            /// <summary>
            /// Disattiva associazione per il font.
            /// </summary>
            CLIP_DFA_DISABLE = 4 << 4,
            /// <summary>
            /// Deve essere specificato per poter usare un font di sola lettura incorporato.
            /// </summary>
            CLIP_EMBEDDED = 8 << 4
        }

        /// <summary>
        /// Qualità dell'output.
        /// </summary>
        internal enum OutputQuality : byte
        {
            /// <summary>
            /// L'aspetto del font non ha importanza.
            /// </summary>
            DEFAULT_QUALITY,
            /// <summary>
            /// L'aspetto del font è meno importante rispetto a <see cref="PROOF_QUALITY"/>.
            /// </summary>
            /// <remarks>Il ridimensionamento dei font raster GDI è abilitato.<br/>
            /// Font grassetto, corsivo, sottolineato e sbarrato sono sintetizzato se necessario.</remarks>
            DRAFT_QUALITY,
            /// <summary>
            /// La qualità dei caratteri è più importante dell'esatta corrispondenza degli attributi del font logico.
            /// </summary>
            /// <remarks>Il ridimensionamento dei font raster GDI è disabilitato e viene scelto quello più vicino in grandezza.<br/>
            /// Font grassetto, corsivo, sottolineato e sbarrato sono sintetizzato se necessario.</remarks>
            PROOF_QUALITY,
            /// <summary>
            /// Al font non è mai applicato l'antialiasing.
            /// </summary>
            NONANTIALIASED_QUALITY,
            /// <summary>
            /// Al font è sempre applicato l'antialiasing se esso lo supporta e la sua dimnesione è corretta.
            /// </summary>
            ANTIALIASED_QUALITY,
            /// <summary>
            /// Il testo viene renderizzato usando l'antialiasing ClearType se possibile.
            /// </summary>
            CLEARTYPE_QUALITY,
            /// <summary>
            /// 
            /// </summary>
            CLEARTYPE_NATURAL_QUALITY
        }

        /// <summary>
        /// Inclinazione e famiglia del font.
        /// </summary>
        [Flags]
        internal enum FontPitchAndFamily : byte
        {
            DEFAULT_PITCH,
            FIXED_PITCH,
            VARIABLE_PITCH,
            /// <summary>
            /// Font predefinito.
            /// </summary>
            FF_DONTCARE = 0 << 4,
            /// <summary>
            /// Font con larghezza del tratto variabile con serif.
            /// </summary>
            FF_ROMAN = 1 << 4,
            /// <summary>
            /// Font con larghezza del tratto variabile senza serif.
            /// </summary>
            FF_SWISS = 2 << 4,
            /// <summary>
            /// Font con larghezza del tratto costante, con o senza serif.
            /// </summary>
            FF_MODERN = 3 << 4,
            /// <summary>
            /// Font somiglianti alla scrittura a mano.
            /// </summary>
            FF_SCRIPT = 4 << 4,
            /// <summary>
            /// Font decorativi (Inglese antico come esempio).
            /// </summary>
            FF_DECORATIVE = 5 << 4
        }
    }
}