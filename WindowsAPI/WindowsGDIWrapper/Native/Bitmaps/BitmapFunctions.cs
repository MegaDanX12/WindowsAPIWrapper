using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapStructures;
using static WindowsAPI.WindowsGDIWrapper.Native.Bitmaps.BitmapEnumerations;

namespace WindowsAPI.WindowsGDIWrapper.Native.Bitmaps
{
    /// <summary>
    /// Funzioni relative ai bitmap.
    /// </summary>
    internal static class BitmapFunctions
    {
        /// <summary>
        /// Crea un bitmap indipendente dal dispositivo sul quale le applicazioni possono scrivere direttamente.
        /// </summary>
        /// <param name="DeviceContextHandle">Handle al contesto dispositivo.</param>
        /// <param name="Info">Struttura <see cref="BITMAPINFO"/> che specifica gli attributi del bitmap.</param>
        /// <param name="Usage">Tipo di dati contenuti nel campo <see cref="BITMAPINFO.Colors"/> del parametro <paramref name="Info"/>.</param>
        /// <param name="Bits">Puntatore alla variabile che riceve il puntatore alle posizione dei valori di bit del bitmap.</param>
        /// <param name="Section">Handle all'oggetto di file mapping da usare per creare il bitmap.</param>
        /// <param name="Offset">Offset a partire dall'inizio dell'oggetto di file mapping indicato da <paramref name="Section"/> dopo i valori di bit iniziano.</param>
        /// <returns>Handle al bitmap creato se l'operazione ha successo, <see cref="IntPtr.Zero"/> altrimenti</returns>
        /// <remarks>Se <paramref name="Usage"/> ha valore <see cref="ColorTable.DIB_PAL_COLORS"/>, la funzione usa la tavolozza del contesto dispositivo indicato da <paramref name="DeviceContextHandle"/>.<br/><br/>
        /// Se <paramref name="Section"/> non è impostato <see cref="IntPtr.Zero"/> deve essere un handle a un oggetto di file mapping con il permesso PAGE_READWRITE oppure PAGE_WRITECOPY.<br/>
        /// DIB di sola lettura non sono supportati.<br/>
        /// La funziona individua i valori di bit del bitmap all'offset indicato da <paramref name="Offset"/> nell'oggetto di file mapping riferito da <paramref name="Section"/>, questo handle può essere recuperato in altro momento.<br/>
        /// Se <paramref name="Section"/> ha valore <see cref="IntPtr.Zero"/>, il sistema alloca memoria per il bitmap, l'handle a questa memoria non può essere recuperato più tardi.<br/><br/>
        /// Il valore di <paramref name="Offset"/> viene ignorato se <paramref name="Section"/> è impostato a <see cref="IntPtr.Zero"/>, i valori di bit del bitmap sono allineati sui confini a 4 byte, quindi <paramref name="Offset"/> deve essere un multiplo di 4.<br/><br/>
        /// Se l'operazione ha successo <paramref name="Bits"/> punta ai valori di bit del bitmap, altrimenti è impostato a <see cref="IntPtr.Zero"/>.</remarks>
        [DllImport("Gdi32.dll", EntryPoint = "CreateDIBSection", SetLastError = true)]
        internal static extern IntPtr CreateDIBSection(IntPtr DeviceContextHandle, ref BITMAPINFO Info, ColorTable Usage, out IntPtr Bits, IntPtr Section, uint Offset);

        /// <summary>
        /// Carica un'icona, un cursore, un cursore animato o un bitmap.
        /// </summary>
        /// <param name="InstanceHandle">Istanza al modulo che contiene l'immagine da caricare.</param>
        /// <param name="ImageName">Immagine da caricare.</param>
        /// <param name="Type">Tipo di immagine.</param>
        /// <param name="Width">Larghezza, in pixel, dell'icona o del cursore.</param>
        /// <param name="Height">Altezza, in pixel, dell'icona o del cursore.</param>
        /// <param name="Options">Opzioni di caricamento dell'immagine.</param>
        /// <returns>Handle all'immagine appena caricata se l'operazione ha successo, <see cref="IntPtr.Zero"/> altrimenti.</returns>
        /// <remarks><paramref name="InstanceHandle"/> deve essere impostato a <see cref="IntPtr.Zero"/> se si sta caricando un'immagine OEM o una risorsa stand alone.<br/><br/>
        /// Se <paramref name="InstanceHandle"/> non è impostato a <see cref="IntPtr.Zero"/> e <paramref name="Options"/> non include <see cref="LoadingOptions.LR_LOADFROMFILE"/>, <paramref name="ImageName"/> specifica la risorsa nel modulo.<br/>
        /// Se la risorsa deve essere caricata in base al nome, <paramref name="ImageName"/> è una stringa che contiene il nome della risorsa.<br/>
        /// Se la risorsa deve essere caricata in base all'ordinale, <paramref name="ImageName"/> deve avere il formato #ID.<br/>
        /// Se <paramref name="InstanceHandle"/> è impostato a <see cref="IntPtr.Zero"/> e <paramref name="Options"/> non include <see cref="LoadingOptions.LR_LOADFROMFILE"/>, <paramref name="ImageName"/> specifica l'identificativo dell'immagine OEM da caricare.<br/>
        /// Se <paramref name="Options"/> include <see cref="LoadingOptions.LR_LOADFROMFILE"/>, <paramref name="ImageName"/> indica il nome del file che contiene la risorsa stand alone, <paramref name="InstanceHandle"/> deve essere <see cref="IntPtr.Zero"/>.<br/><br/>
        /// Se <paramref name="Width"/> o <paramref name="Height"/> sono impostati a 0 e <paramref name="Options"/> include <see cref="LoadingOptions.LR_DEFAULTSIZE"/>, la funzione usa le metriche di sistema per impostarne i valori, se tale opzione non è inclusa, viene usata l'effettiva dimensione della risorsa.</remarks>
        [DllImport("User32.dll", EntryPoint = "LoadImageW", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern IntPtr LoadImage(IntPtr InstanceHandle, string ImageName, ImageType Type, int Width, int Height, LoadingOptions Options);
    }
}