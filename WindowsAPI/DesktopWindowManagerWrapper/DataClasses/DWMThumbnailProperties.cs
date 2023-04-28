using System;
using System.Drawing;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMEnumerations;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMStructures;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.DesktopWindowManagerWrapper.DataClasses
{
    /// <summary>
    /// Proprietà di una miniatura DWM.
    /// </summary>
    public class DWMThumbnailProperties
    {
        /// <summary>
        /// Area nella finestra di destinazione dove renderizzare la miniatura.
        /// </summary>
        public Rectangle? DestinationWindowArea { get; }

        /// <summary>
        /// Area della finestra di origine da usare come miniatura.
        /// </summary>
        public Rectangle? SourceWindowArea { get; }

        /// <summary>
        /// Opacità della miniatura.
        /// </summary>
        public byte? Opacity { get; }

        /// <summary>
        /// Indica se la miniatura è visibile.
        /// </summary>
        public bool? IsVisible { get; }

        /// <summary>
        /// Indica se usare solo l'area client della finestra di origine.
        /// </summary>
        public bool? UseOnlyClientArea { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DWMThumbnailProperties"/>.
        /// </summary>
        /// <param name="DestinationArea">Area della finestra di destinazione dove renderizzare la miniatura.</param>
        /// <param name="SourceArea">Area della finestra di origine da usare come miniatura.</param>
        /// <param name="Opacity">Opacità della miniatura.</param>
        /// <param name="Visible">Indica se la miniatura è visibile.</param>
        /// <param name="UseOnlyClientArea">Indica se usare solo l'area client della finestra di origine.</param>
        public DWMThumbnailProperties(Rectangle? DestinationArea = null, Rectangle? SourceArea = null, byte? Opacity = null, bool? Visible = null, bool? UseOnlyClientArea = null)
        {
            DestinationWindowArea = DestinationArea;
            SourceWindowArea = SourceArea;
            this.Opacity = Opacity;
            IsVisible = Visible;
            this.UseOnlyClientArea = UseOnlyClientArea;
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="DWM_THUMBNAIL_PROPERTIES"/>.
        /// </summary>
        /// <returns>Struttura <see cref="DWM_THUMBNAIL_PROPERTIES"/> risultato della conversione.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        internal DWM_THUMBNAIL_PROPERTIES ToStructure()
        {
            DWM_TNP ValidMembers = 0;
            DWM_THUMBNAIL_PROPERTIES Structure = new DWM_THUMBNAIL_PROPERTIES();
            if (DestinationWindowArea.HasValue)
            {
                ValidMembers |= DWM_TNP.DWM_TNP_RECTDESTINATION;
                Structure.DestinationRectangle = new RECT()
                {
                    Left = DestinationWindowArea.Value.X,
                    Top = DestinationWindowArea.Value.Y,
                    Right = DestinationWindowArea.Value.Right,
                    Bottom = DestinationWindowArea.Value.Bottom
                };
            }
            if (SourceWindowArea.HasValue)
            {
                ValidMembers |= DWM_TNP.DWM_TNP_RECTSOURCE;
                Structure.SourceRectangle = new RECT()
                {
                    Left = SourceWindowArea.Value.X,
                    Top = SourceWindowArea.Value.Y,
                    Right = SourceWindowArea.Value.Right,
                    Bottom = SourceWindowArea.Value.Bottom
                };
            }
            if (Opacity.HasValue)
            {
                ValidMembers |= DWM_TNP.DWM_TNP_OPACITY;
                Structure.Opacity = Opacity.Value;
            }
            if (UseOnlyClientArea.HasValue)
            {
                ValidMembers |= DWM_TNP.DWM_TNP_SOURCECLIENTAREAONLY;
                Structure.OnlyClientArea = UseOnlyClientArea.Value;
            }
            if (IsVisible.HasValue)
            {
                ValidMembers |= DWM_TNP.DWM_TNP_VISIBLE;
                Structure.Visible = IsVisible.Value;
            }
            if (ValidMembers is 0)
            {
                throw new InvalidOperationException("The instance does not contain valid data.");
            }
            return Structure;
        }
    }
}