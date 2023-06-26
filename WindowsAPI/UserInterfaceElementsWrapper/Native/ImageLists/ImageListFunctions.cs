using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists
{
    /// <summary>
    /// Funzioni relative alle liste di immagini.
    /// </summary>
    internal static class ImageListFunctions
    {
        /// <summary>
        /// Crea una nuova lista di immagini.
        /// </summary>
        /// <param name="Width">Larghezza, in pixel, di ogni immagine.</param>
        /// <param name="Height">Altezza, in pixel, di ogni immagine.</param>
        /// <param name="CreationOptions">Opzioni di creazione.</param>
        /// <param name="InitialImageCount">Numero iniziale di immagini presenti nella lista.</param>
        /// <param name="GrowSize">Numero di nuove immagini che la lista può contenere dopo che il sistema l'ha ridimensionata.</param>
        /// <returns>Handle alla lista di immagini se l'operazione ha successo, <see cref="IntPtr.Zero"/> altrimenti.</returns>
        [DllImport("Comctl32.dll", EntryPoint = "ImageList_Create", SetLastError = true)]
        internal static extern IntPtr CreateImageList(int Width, int Height, ImageListCreationFlags CreationOptions, int InitialImageCount, int GrowSize);

        /// <summary>
        /// Elimina una lista di immagini.
        /// </summary>
        /// <param name="ImageListHandle">Handle alla lista di immagini.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("Comctl32.dll", EntryPoint = "ImageList_Destroy", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyImageList(IntPtr ImageListHandle);

        /// <summary>
        /// Aggiunge una o multiple immagini a una lista di immagini.
        /// </summary>
        /// <param name="ImageListHandle">Handle alla lista di immagini.</param>
        /// <param name="BitmapHandle">Handle al bitmap che include l'immagine o le immagini.</param>
        /// <param name="BitmapMask">Handle al bitmap che contiene la maschera.</param>
        /// <returns>Indice della prima nuova immagine se l'operazione ha successo, -1 altrimenti.</returns>
        [DllImport("Comctl32.dll", EntryPoint = "ImageList_Add", SetLastError = true)]
        internal static extern int AddImage(IntPtr ImageListHandle, IntPtr BitmapHandle, IntPtr BitmapMask);
    }
}