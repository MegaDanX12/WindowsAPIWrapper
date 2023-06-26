using System;
using System.Drawing;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonStructures;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonEnumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListFunctions;
using WindowsAPI.SafeHandles;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Buttons.DataClasses
{
    /// <summary>
    /// Informazioni su uno slit button.
    /// </summary>
    public sealed class SplitButtonInfo : IDisposable
    {
        /// <summary>
        /// Handle alla lista di immagini.
        /// </summary>
        private readonly SafeImageListHandle Handle;

        /// <summary>
        /// Indica se l'immagine o il glifo è allineato orizzontalemente con il margine sinistro.
        /// </summary>
        public bool? IsAlignedLeft { get; }

        /// <summary>
        /// Indica se utilizza un'icona come glifo.
        /// </summary>
        public bool? HasImageAsGlyph { get; }

        /// <summary>
        /// Indica se il pulsante non ha una divisione.
        /// </summary>
        public bool? NoSplit { get; }

        /// <summary>
        /// Indica se il glifo è allungato cercando di mantenere il rapporto d'aspetto.
        /// </summary>
        public bool? IsGlyphStreched { get; }

        /// <summary>
        /// Dimensione del glifo.
        /// </summary>
        public Size? GlyphSize { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SplitButtonInfo"/>.
        /// </summary>
        /// <param name="Data">Struttura <see cref="BUTTON_SPLITINFO"/> con le informazioni.</param>
        internal SplitButtonInfo(BUTTON_SPLITINFO Data)
        {
            Handle = Data.ValidMembers.HasFlag(SplitInfoMembers.BCSIF_GLYPH) ? new SafeImageListHandle(Data.ImagelistHandle, false) : null;
            if (Data.ValidMembers.HasFlag(SplitInfoMembers.BCSIF_STYLE))
            {
                if (Data.SplitStyle.HasFlag(SplitStyle.BCSS_ALIGNLEFT))
                {
                    IsAlignedLeft = true;
                }
                if (Data.SplitStyle.HasFlag(SplitStyle.BCSS_IMAGE))
                {
                    HasImageAsGlyph = true;
                }
                if (Data.SplitStyle.HasFlag(SplitStyle.BCSS_NOSPLIT))
                {
                    NoSplit = true;
                }
                if (Data.SplitStyle.HasFlag(SplitStyle.BCSS_STRETCH))
                {
                    IsGlyphStreched = true;
                }
            }
            if (Data.ValidMembers.HasFlag(SplitInfoMembers.BCSIF_SIZE))
            {
                GlyphSize = new Size(Data.GlyphSize.X, Data.GlyphSize.Y);
            }
        }

        /// <summary>
        /// Converte i dati di questa istanza in una struttura <see cref="BUTTON_SPLITINFO"/>.
        /// </summary>
        /// <returns>Struttura <see cref="BUTTON_SPLITINFO"/> risultato della conversione.</returns>
        internal BUTTON_SPLITINFO ToStructure()
        {
            SplitInfoMembers ValidMembers = 0;
            if (!Handle.IsInvalid)
            {
                if (HasImageAsGlyph.HasValue)
                {
                    if (HasImageAsGlyph.Value)
                    {
                        ValidMembers |= SplitInfoMembers.BCSIF_IMAGE;
                    }
                    else
                    {
                        ValidMembers |= SplitInfoMembers.BCSIF_GLYPH;
                    }
                }
                else
                {
                    ValidMembers |= SplitInfoMembers.BCSIF_GLYPH;
                }
            }
            if (GlyphSize.HasValue)
            {
                ValidMembers |= SplitInfoMembers.BCSIF_SIZE;
            }
            SplitStyle Style = 0;
            if (IsAlignedLeft.HasValue && IsAlignedLeft.Value)
            {
                Style |= SplitStyle.BCSS_ALIGNLEFT;
            }
            if (HasImageAsGlyph.HasValue && HasImageAsGlyph.Value)
            {
                Style |= SplitStyle.BCSS_IMAGE;
            }
            if (NoSplit.HasValue && NoSplit.Value)
            {
                Style |= SplitStyle.BCSS_NOSPLIT;
            }
            if (IsGlyphStreched.HasValue && IsGlyphStreched.Value)
            {
                Style |= SplitStyle.BCSS_STRETCH;
            }
            if (Style != 0)
            {
                ValidMembers |= SplitInfoMembers.BCSIF_STYLE;
            }
            BUTTON_SPLITINFO Structure = new BUTTON_SPLITINFO()
            {
                ValidMembers = ValidMembers,
                ImagelistHandle = Handle,
                SplitStyle = Style
            };
            if (GlyphSize.HasValue)
            {
                SIZE GlyphSizeStructure = new SIZE()
                {
                    X = GlyphSize.Value.Width,
                    Y = GlyphSize.Value.Height
                };
                Structure.GlyphSize = GlyphSizeStructure;
            }
            return Structure;
        }

        public void Dispose()
        {
            DestroyImageList(Handle);
        }
    }
}