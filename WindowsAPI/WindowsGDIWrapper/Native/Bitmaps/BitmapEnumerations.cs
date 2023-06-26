using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.Window.WindowEnumerations;

namespace WindowsAPI.WindowsGDIWrapper.Native.Bitmaps
{
    /// <summary>
    /// Enumerazioni relative a bitmap.
    /// </summary>
    internal static class BitmapEnumerations
    {
        /// <summary>
        /// Compressione di un bitmap.
        /// </summary>
        internal enum BitmapCompression
        {
            BI_RGB,
            BI_RLE8,
            BI_RLE4,
            BI_RLEBITFIELDS,
            BI_JPEG,
            BI_PNG
        }

        /// <summary>
        /// Colori contenuti nella tabella dei colori di un bitmap.
        /// </summary>
        internal enum ColorTable : uint
        {
            /// <summary>
            /// Valore RGB letterali.
            /// </summary>
            DIB_RGB_COLORS,
            /// <summary>
            /// Indici a 16-bit nella tavolozza logica del contesto dispositivo.
            /// </summary>
            DIB_PAL_COLORS
        }

        /// <summary>
        /// Tipo di immagine.
        /// </summary>
        internal enum ImageType
        {
            /// <summary>
            /// Bitmap.
            /// </summary>
            IMAGE_BITMAP,
            /// <summary>
            /// Icona.
            /// </summary>
            IMAGE_ICON,
            /// <summary>
            /// Cursore.
            /// </summary>
            IMAGE_CURSOR,

            IMAGE_ENHMETAFILE
        }

        /// <summary>
        /// Opzioni di caricamento immagine.
        /// </summary>
        [Flags]
        internal enum LoadingOptions
        {
            /// <summary>
            /// Nessun effetto.
            /// </summary>
            /// <remarks>Opzione predefinita.</remarks>
            LR_DEFAULTCOLOR,
            /// <summary>
            /// Carica l'immagine in bianco e nero.
            /// </summary>
            LR_MONOCHROME,
            /// <summary>
            /// 
            /// </summary>
            LR_COLOR,
            /// <summary>
            /// 
            /// </summary>
            LR_COPYRETURNORG = 4,
            /// <summary>
            /// 
            /// </summary>
            LR_COPYDELETEORG = 8,
            /// <summary>
            /// Carica l'immagine da un file.
            /// </summary>
            LR_LOADFROMFILE = 16,
            /// <summary>
            /// Recupera il valore del colore del primo pixel nell'immagine e sostituisce la corrispondente voce nella tabella dei colori con il colore predefinito della finestra.
            /// </summary>
            /// <remarks>Tutti i pixel nell'immagine che usano quella voce diventeranno del colore predefinito della finestra.<br/>
            /// Questo valore si applica solamente a immagini che hanno corrispondenti tabelle di colori.<br/><br/>
            /// Non usare questa opzione se il bitmap da caricare ha una profondità di colore superiore agli 8 bit.</remarks>
            LR_LOADTRANSPARENT = 32,
            /// <summary>
            /// Usa la larghezza e l'altezza specificate dalla metrica di sistema per i cursori o per le icone, se non esplicitamente indicati.
            /// </summary>
            /// <remarks>Se questo valore non è specificato e le dimensioni non sono indicate, viene usata l'effettiva dimensione della risorsa.<br/>
            /// Se la risorsa contiene immagini multiple, viene usata la dimensione della prima immagine.</remarks>
            LR_DEFAULTSIZE = 64,
            /// <summary>
            /// Usa veri colori VGA.
            /// </summary>
            LR_VGACOLOR = 128,
            /// <summary>
            /// Cerca nella tabella dei colori dell'immagine le seguenti tonalità di grigio e le sostituisce con i colori 3D corrispondenti:<br/><br/>
            /// Grigio scuro (128, 128, 128) -> <see cref="SystemColors.COLOR_3DSHADOW"/><br/>
            /// Grigio (192, 192, 192) -> <see cref="SystemColors.COLOR_3DFACE"/><br/>
            /// Grigio chiaro (223, 223, 223) -> <see cref="SystemColors.COLOR_3DLIGHT"/>
            /// </summary>
            /// <remarks>Non usare questa opzione se si sta caricando un bitmap con una profondita di colore maggiore di 8 bit.</remarks>
            LR_LOADMAP3DCOLORS = 4096,
            /// <summary>
            /// Se l'immagine caricata è un bitmap, viene restituito un DIB piuttosto che un bitmap compatibile.
            /// </summary>
            /// <remarks>Questa opzione è utile per caricare un bitmap senza mapparlo ai colori del dispositivo di visualizzazione.</remarks>
            LR_CREATEDIBSECTION = 8192,
            /// <summary>
            /// 
            /// </summary>
            LR_COPYFROMRESOURCE = 16384,
            /// <summary>
            /// Condivide l'handle all'immagine se l'immagine viene caricata più volte.
            /// </summary>
            /// <remarks>Se questa opzione non è inclusa, caricare una seconda volta la stessa risorsa ricaricherà l'immagine e verra restituito un handle differente.<br/><br/>
            /// Quando viene utilizzata questa opzione, il sistema eliminerà la risorsa quando non è più necessaria.<br/><br/>
            /// Questa opzione non dovrebbe essere usata per immagini con dimensioni non standard, che possono cambiare dopo il caricamento, o che sono caricate da un file.<br/><br/>
            /// Questa opzione deve essere usata quando si carica un'icona o un cursore di sistema.</remarks>
            LR_SHARED = 32768
        }
    }
}