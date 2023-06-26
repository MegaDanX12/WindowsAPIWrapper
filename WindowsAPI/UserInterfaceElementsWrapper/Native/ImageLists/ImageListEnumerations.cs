using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists
{
    /// <summary>
    /// Enumerazioni relative alle liste immagini.
    /// </summary>
    internal static class ImageListEnumerations
    {
        /// <summary>
        /// Opzioni di creazione di una lista immagini.
        /// </summary>
        [Flags]
        internal enum ImageListCreationFlags
        {
            /// <summary>
            /// Usa una maschera.
            /// </summary>
            /// <remarks>La lista di immagini contiene due bitmap, uno dei quali è monocromatico usato come maschera.<br/>
            /// Se questo valore non è incluso, la lista contiene un solo bitmap.</remarks>
            ILC_MASK = 1,
            /// <summary>
            /// Usa il comportamento predefinito se nessuna delle altre opzioni ILC_COLORx sono specificate.
            /// </summary>
            /// <remarks>Tipicamente il valore predefinito è <see cref="ILC_COLOR4"/>, ma per driver video più vecchi, il valore predefinito è <see cref="ILC_COLORDDB"/>.</remarks>
            ILC_COLOR = 0,
            /// <summary>
            /// Usa un bitmap dipendente dal dispositivo.
            /// </summary>
            ILC_COLORDDB = 254,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 4-bit (16 colori) per la lista immagini.
            /// </summary>
            ILC_COLOR4 = 4,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 8-bit per la lista immagini.
            /// </summary>
            /// <remarks>I colori usati sono gli stessi della tavolozza mezzitoni.</remarks>
            ILC_COLOR8 = 8,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 16-bit (32000/64000 colori) per la lista immagini.
            /// </summary>
            ILC_COLOR16 = 16,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 24-bit per la lista immagini.
            /// </summary>
            ILC_COLOR24 = 24,
            /// <summary>
            /// Usa una sezione bitmap indipendente dal dispositivo a 32-bit per la lista immagini.
            /// </summary>
            ILC_COLOR32 = 32,
            /// <summary>
            /// Non implementato.
            /// </summary>
            ILC_PALETTE = 2048,
            /// <summary>
            /// Crea lo speculare delle icone contenute.
            /// </summary>
            ILC_MIRROR = 8192,
            /// <summary>
            /// Crea lo speculare di ogni elemento anzinché di tutta la striscia.
            /// </summary>
            ILC_PERITEMMIRROR = 32768,
            /// <summary>
            /// La lista di immagini dovrebbe accettare immagini più piccole del valore previsto e applica la dimensione originale basandosi sull'immagine aggiunta.
            /// </summary>
            ILC_ORIGINALSIZE = 65536,
            /// <summary>
            /// Riservato.
            /// </summary>
            ILC_HIGHQUALITYSCALE = 131072
        }
    }
}