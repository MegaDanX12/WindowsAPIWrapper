using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.WindowsGDIWrapper.Native.FontAndText;

namespace WindowsAPI.WindowsGDIWrapper.FontAndText
{
    /// <summary>
    /// Enumerazioni relative ai font e al testo.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Spessore del font.
        /// </summary>
        public enum FontWeight
        {
            NotImportant = FontAndTextEnumerations.FontWeight.FW_DONTCARE,
            Thin = FontAndTextEnumerations.FontWeight.FW_THIN,
            ExtraLight = FontAndTextEnumerations.FontWeight.FW_EXTRALIGHT,
            Light = FontAndTextEnumerations.FontWeight.FW_LIGHT,
            Normal = FontAndTextEnumerations.FontWeight.FW_NORMAL,
            Medium = FontAndTextEnumerations.FontWeight.FW_MEDIUM,
            SemiBold = FontAndTextEnumerations.FontWeight.FW_SEMIBOLD,
            Bold = FontAndTextEnumerations.FontWeight.FW_BOLD,
            ExtraBold = FontAndTextEnumerations.FontWeight.FW_EXTRABOLD,
            Heavy = FontAndTextEnumerations.FontWeight.FW_HEAVY
        }

        /// <summary>
        /// Set di caratteri.
        /// </summary>
        public enum Charset
        {
            Ansi = FontAndTextEnumerations.Charset.ANSI_CHARSET,
            Baltic = FontAndTextEnumerations.Charset.BALTIC_CHARSET,
            ChineseBig5 = FontAndTextEnumerations.Charset.CHINESEBIG5_CHARSET,
            Default = FontAndTextEnumerations.Charset.DEFAULT_CHARSET,
            EastEurope = FontAndTextEnumerations.Charset.EASTEUROPE_CHARSET,
            GB2312 = FontAndTextEnumerations.Charset.GB2312_CHARSET,
            Greek = FontAndTextEnumerations.Charset.GREEK_CHARSET,
            Hangul = FontAndTextEnumerations.Charset.HANGUL_CHARSET,
            Mac = FontAndTextEnumerations.Charset.MAC_CHARSET,
            OEM = FontAndTextEnumerations.Charset.OEM_CHARSET,
            Russian = FontAndTextEnumerations.Charset.RUSSIAN_CHARSET,
            ShiftJis = FontAndTextEnumerations.Charset.SHIFTJIS_CHARSET,
            Symbol = FontAndTextEnumerations.Charset.SYMBOL_CHARSET,
            Turkish = FontAndTextEnumerations.Charset.TURKISH_CHARSET,
            Vietnamese = FontAndTextEnumerations.Charset.VIETNAMESE_CHARSET,
            Johab = FontAndTextEnumerations.Charset.JOHAB_CHARSET,
            Arabic = FontAndTextEnumerations.Charset.ARABIC_CHARSET,
            Hebrew = FontAndTextEnumerations.Charset.HEBREW_CHARSET,
            Thai = FontAndTextEnumerations.Charset.THAI_CHARSET
        }

        /// <summary>
        /// Precisione output del font mapper.
        /// </summary>
        public enum FontOutputPrecision
        {
            /// <summary>
            /// Non usato.
            /// </summary>
            Character = FontAndTextEnumerations.OutputPrecision.OUT_CHARACTER_PRECIS,
            /// <summary>
            /// Comportamento predefinito.
            /// </summary>
            Default = FontAndTextEnumerations.OutputPrecision.OUT_DEFAULT_PRECIS,
            /// <summary>
            /// Scegliere font dispositivo quando ne esistono diversi con lo stesso nome.
            /// </summary>
            Device = FontAndTextEnumerations.OutputPrecision.OUT_DEVICE_PRECIS,
            /// <summary>
            /// Scegliere font TrueType e altri basati sul contorno.
            /// </summary>
            Outline = FontAndTextEnumerations.OutputPrecision.OUT_OUTLINE_PRECIS,
            /// <summary>
            /// Scegliere solo font PostScript, se non ne esistono usare il comportamento predefinito.
            /// </summary>
            PostScriptOnly = FontAndTextEnumerations.OutputPrecision.OUT_PS_ONLY_PRECIS,
            /// <summary>
            /// Scegliere font raster quando ne esistono diversi con lo stesso nome.
            /// </summary>
            Raster = FontAndTextEnumerations.OutputPrecision.OUT_RASTER_PRECIS,
            /// <summary>
            /// Non usato dal font mapper.
            /// </summary>
            String = FontAndTextEnumerations.OutputPrecision.OUT_STRING_PRECIS,
            /// <summary>
            /// Non usato dal font mapper.
            /// </summary>
            Stroke = FontAndTextEnumerations.OutputPrecision.OUT_STROKE_PRECIS,
            /// <summary>
            /// Scegliere solo font TrueType, se non ne esistono usare il comportamento predefinito.
            /// </summary>
            TrueTypeOnly = FontAndTextEnumerations.OutputPrecision.OUT_TT_ONLY_PRECIS,
            /// <summary>
            /// Scegliere font TrueType se ne esistono diversi con lo stesso nome.
            /// </summary>
            AllowTrueType = FontAndTextEnumerations.OutputPrecision.OUT_TT_PRECIS
        }

        /// <summary>
        /// Precisione di taglio del font mapper.
        /// </summary>
        public enum ClippingPrecision
        {
            /// <summary>
            /// Non usato.
            /// </summary>
            Character = FontAndTextEnumerations.ClippingPrecision.CLIP_CHARACTER_PRECIS,
            /// <summary>
            /// Comportamento predefinito.
            /// </summary>
            Default = FontAndTextEnumerations.ClippingPrecision.CLIP_DEFAULT_PRECIS,
            /// <summary>
            /// Disattiva associazione per il font.
            /// </summary>
            DisableFontAssociation = FontAndTextEnumerations.ClippingPrecision.CLIP_DFA_DISABLE,
            /// <summary>
            /// Deve essere specificato per poter usare un font di sola lettura incorporato.
            /// </summary>
            Embedded = FontAndTextEnumerations.ClippingPrecision.CLIP_EMBEDDED,
            /// <summary>
            /// La rotazione dei font dipende dall'orientamento del sistema di coordinate.
            /// </summary>
            /// <remarks>Se questo valore non è usato, i font dispositivo vengono sempre ruotati in senso antiorario, mentre per gli altri dipende dall'orientamento del sistema di coordinate.</remarks>
            LeftHandedCoordinates = FontAndTextEnumerations.ClippingPrecision.CLIP_LH_ANGLES,
            /// <summary>
            /// Non usato.
            /// </summary>
            Mask = FontAndTextEnumerations.ClippingPrecision.CLIP_MASK,
            /// <summary>
            /// Non usato dal font mapper.
            /// </summary>
            Stroke = FontAndTextEnumerations.ClippingPrecision.CLIP_STROKE_PRECIS,
            /// <summary>
            /// Non usato.
            /// </summary>
            AlwaysTrueType = FontAndTextEnumerations.ClippingPrecision.CLIP_TT_ALWAYS
        }

        /// <summary>
        /// Qualità dell'output.
        /// </summary>
        public enum OutputQuality
        {
            /// <summary>
            /// Al font è sempre applicato l'antialiasing se esso lo supporta e la sua dimnesione è corretta.
            /// </summary>
            Antialiased = FontAndTextEnumerations.OutputQuality.ANTIALIASED_QUALITY,
            /// <summary>
            /// Il testo viene renderizzato usando l'antialiasing ClearType se possibile.
            /// </summary>
            ClearType = FontAndTextEnumerations.OutputQuality.CLEARTYPE_QUALITY,
            /// <summary>
            /// L'aspetto del font non ha importanza.
            /// </summary>
            Default = FontAndTextEnumerations.OutputQuality.DEFAULT_QUALITY,
            /// <summary>
            /// L'aspetto del font è meno importante rispetto a <see cref="PROOF_QUALITY"/>.
            /// </summary>
            /// <remarks>Il ridimensionamento dei font raster GDI è abilitato.<br/>
            /// Font grassetto, corsivo, sottolineato e sbarrato sono sintetizzato se necessario.</remarks>
            Draft = FontAndTextEnumerations.OutputQuality.DRAFT_QUALITY,
            /// <summary>
            /// Al font non è mai applicato l'antialiasing.
            /// </summary>
            NonAntialiased = FontAndTextEnumerations.OutputQuality.NONANTIALIASED_QUALITY,
            /// <summary>
            /// La qualità dei caratteri è più importante dell'esatta corrispondenza degli attributi del font logico.
            /// </summary>
            /// <remarks>Il ridimensionamento dei font raster GDI è disabilitato e viene scelto quello più vicino in grandezza.<br/>
            /// Font grassetto, corsivo, sottolineato e sbarrato sono sintetizzato se necessario.</remarks>
            Proof = FontAndTextEnumerations.OutputQuality.PROOF_QUALITY
        }

        /// <summary>
        /// Inclinazione del font.
        /// </summary>
        public enum FontPitch
        {
            Default = FontAndTextEnumerations.FontPitchAndFamily.DEFAULT_PITCH,
            Fixed = FontAndTextEnumerations.FontPitchAndFamily.FIXED_PITCH,
            Variable = FontAndTextEnumerations.FontPitchAndFamily.VARIABLE_PITCH
        }

        /// <summary>
        /// Famiglia del font.
        /// </summary>
        public enum FontFamily
        {
            /// <summary>
            /// Font decorativi (Inglese antico come esempio).
            /// </summary>
            Decorative = FontAndTextEnumerations.FontPitchAndFamily.FF_DECORATIVE,
            /// <summary>
            /// Font predefinito.
            /// </summary>
            NotImportant = FontAndTextEnumerations.FontPitchAndFamily.FF_DONTCARE,
            /// <summary>
            /// Font con larghezza del tratto costante, con o senza serif.
            /// </summary>
            Modern = FontAndTextEnumerations.FontPitchAndFamily.FF_MODERN,
            /// <summary>
            /// Font con larghezza del tratto variabile con serif.
            /// </summary>
            Roman = FontAndTextEnumerations.FontPitchAndFamily.FF_ROMAN,
            /// <summary>
            /// Font somiglianti alla scrittura a mano.
            /// </summary>
            Script = FontAndTextEnumerations.FontPitchAndFamily.FF_SCRIPT,
            /// <summary>
            /// Font con larghezza del tratto variabile senza serif.
            /// </summary>
            Swiss = FontAndTextEnumerations.FontPitchAndFamily.FF_SWISS
        }
    }
}