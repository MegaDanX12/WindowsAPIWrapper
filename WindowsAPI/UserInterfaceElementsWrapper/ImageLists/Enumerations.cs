using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListEnumerations;

namespace WindowsAPI.UserInterfaceElementsWrapper.ImageLists
{
    /// <summary>
    /// Enumerazioni relative alle liste immagini.
    /// </summary>
    public static class Enumerations
    {
        /// <summary>
        /// Opzioni di creazione di una lista immagini.
        /// </summary>
        [Flags]
        public enum ImageListCreationOptions
        {
            /// <summary>
            /// Usa una maschera.
            /// </summary>
            /// <remarks>La lista di immagini contiene due bitmap, uno dei quali è monocromatico usato come maschera.<br/>
            /// Se questo valore non è incluso, la lista contiene un solo bitmap.</remarks>
            UseMask = ImageListCreationFlags.ILC_MASK,
            /// <summary>
            /// Usa il comportamento predefinito se nessuna delle altre opzioni ILC_COLORx sono specificate.
            /// </summary>
            /// <remarks>Tipicamente il valore predefinito è <see cref="Use4BitDeviceIndipendentBitmap"/>, ma per driver video più vecchi, il valore predefinito è <see cref="UseDeviceDependentBitmap"/>.</remarks>
            UseDefaultBitmap = ImageListCreationFlags.ILC_COLOR,
            /// <summary>
            /// Usa un bitmap dipendente dal dispositivo.
            /// </summary>
            UseDeviceDependentBitmap = ImageListCreationFlags.ILC_COLORDDB,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 4-bit (16 colori) per la lista immagini.
            /// </summary>
            Use4BitDeviceIndipendentBitmap = ImageListCreationFlags.ILC_COLOR4,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 8-bit per la lista immagini.
            /// </summary>
            /// <remarks>I colori usati sono gli stessi della tavolozza mezzitoni.</remarks>
            Use8BitDeviceIndipendentBitmap = ImageListCreationFlags.ILC_COLOR8,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 16-bit (32000/64000 colori) per la lista immagini.
            /// </summary>
            Use16BitDeviceIndipendentBitmap = ImageListCreationFlags.ILC_COLOR16,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 24-bit per la lista immagini.
            /// </summary>
            Use24BitDeviceIndipendentBitmap = ImageListCreationFlags.ILC_COLOR24,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 32-bit per la lista immagini.
            /// </summary>
            Use32BitDeviceIndipendentBitmap = ImageListCreationFlags.ILC_COLOR32,
            /// <summary>
            /// Crea lo speculare delle icone contenute.
            /// </summary>
            MirrorIcons = ImageListCreationFlags.ILC_MIRROR,
            /// <summary>
            /// Crea lo speculare di ogni elemento anzinché di tutta la striscia.
            /// </summary>
            MirrorEveryItem = ImageListCreationFlags.ILC_PERITEMMIRROR,
            /// <summary>
            /// La lista di immagini dovrebbe accettare immagini più piccole del valore previsto e applica la dimensione originale basandosi sull'immagine aggiunta.
            /// </summary>
            UpscaleImages = ImageListCreationFlags.ILC_ORIGINALSIZE
        }
    }
}