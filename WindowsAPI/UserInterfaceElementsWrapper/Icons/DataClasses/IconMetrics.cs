using System;
using WindowsAPI.WindowsGDIWrapper.FontAndText.DataClasses;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Icons.IconStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Icons.DataClasses
{
    /// <summary>
    /// Metriche scalabili relative alle icone.
    /// </summary>
    public class IconMetrics
    {
        /// <summary>
        /// Spazio orizzontale, in pixel, per ogni icona disposta.
        /// </summary>
        public int ArrangedIconHorizontalSpacing { get; }

        /// <summary>
        /// Spazio verticale, in pixel, per ogni icona disposta.
        /// </summary>
        public int ArrangedIconVerticalSpacing { get; }

        /// <summary>
        /// Indica se il ritorno a capo è attivo per i titoli delle icone.
        /// </summary>
        public bool TitleWrapEnabled { get; }

        /// <summary>
        /// Font usato per i titoli delle icone.
        /// </summary>
        public FontAttributes FontData { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="IconMetrics"/>.
        /// </summary>
        /// <param name="IconsData">Struttura <see cref="ICONMETRICS"/> con i dati.</param>
        internal IconMetrics(ICONMETRICS IconsData)
        {
            ArrangedIconHorizontalSpacing = IconsData.HorizontalSpacing;
            ArrangedIconVerticalSpacing = IconsData.VerticalSpacing;
            TitleWrapEnabled = Convert.ToBoolean(IconsData.TitleWrap);
            FontData = new FontAttributes(IconsData.Font);
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="IconMetrics"/>.
        /// </summary>
        /// <param name="HorizontalSpacing">Spazio orizzontale, in pixel, per ogni icona disposta.</param>
        /// <param name="VerticalSpacing">Spazio verticale, in pixel, per ogni icona disposta.</param>
        /// <param name="TitleWrapEnabled">Indica se il ritorno a capo è attivo per i titoli delle icone.</param>
        /// <param name="FontData">Font usato per i titoli delle icone.</param>
        public IconMetrics(int HorizontalSpacing, int VerticalSpacing, bool TitleWrapEnabled, FontAttributes FontData)
        {
            ArrangedIconHorizontalSpacing = HorizontalSpacing;
            ArrangedIconVerticalSpacing = VerticalSpacing;
            this.TitleWrapEnabled = TitleWrapEnabled;
            this.FontData = FontData;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="ICONMETRICS"/>.
        /// </summary>
        /// <returns>Struttura <see cref="ICONMETRICS"/> risultato della conversione.</returns>
        internal ICONMETRICS ToStructure()
        {
            ICONMETRICS Structure = new ICONMETRICS()
            {
                HorizontalSpacing = ArrangedIconHorizontalSpacing,
                VerticalSpacing = ArrangedIconVerticalSpacing,
                TitleWrap = Convert.ToInt32(TitleWrapEnabled),
                Font = FontData.ToStructure()
            };
            return Structure;
        }
    }
}