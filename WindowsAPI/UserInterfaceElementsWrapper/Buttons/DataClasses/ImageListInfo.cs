using System;
using System.Drawing;
using WindowsAPI.SafeHandles;
using static WindowsAPI.UserInterfaceElementsWrapper.Buttons.Enumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Buttons.ButtonStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Buttons.DataClasses
{
    /// <summary>
    /// Informazioni su una lista di immagini.
    /// </summary>
    public sealed class ImageListInfo : IDisposable
    {
        /// <summary>
        /// Handle alla lista di immagini.
        /// </summary>
        internal SafeImageListHandle Handle { get; }

        /// <summary>
        /// Margine attorno all'icona.
        /// </summary>
        public Rectangle Margin { get; }

        /// <summary>
        /// Allineamento dell'immagine.
        /// </summary>
        public ButtonImageAlignment Alignment { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="ImageListInfo"/>.
        /// </summary>
        /// <param name="Data">Struttura <see cref="BUTTON_IMAGELIST"/> con le informazioni.</param>
        internal ImageListInfo(BUTTON_IMAGELIST Data)
        {
            Handle = new SafeImageListHandle(Data.ImagelistHandle, false);
            Margin = Rectangle.FromLTRB(Data.Margin.Left, Data.Margin.Top, Data.Margin.Right, Data.Margin.Bottom);
            Alignment = (ButtonImageAlignment)Data.Alignment;
        }

        public void Dispose()
        {
            Handle.Dispose();
        }
    }
}