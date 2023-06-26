using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.General.Native.GeneralConstants;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.Carets
{
    /// <summary>
    /// Funzioni relative al cursore di inserimento testo.
    /// </summary>
    internal static class CaretFunctions
    {
        /// <summary>
        /// Crea una nuova forma per il cursore di inserimento testo di sistema e ne assegna la proprietà alla finestra specificata.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra proprietaria del cursore.</param>
        /// <param name="BitmapHandle">Handle al bitmap che definisce la forma del cursore.</param>
        /// <param name="Width">Larghezza del cursore, in unità logiche.</param>
        /// <param name="Height">Altezza del cursore, in unità logiche.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="BitmapHandle"/> è impostato <see cref="IntPtr.Zero"/>, il cursore è solido.<br/>
        /// Se <paramref name="BitmapHandle"/> è impostato a 1, il cursore è grigio.<br/>
        /// Se <paramref name="BitmapHandle"/> è un effettivo handle a un bitmap, <paramref name="Width"/> e <paramref name="Height"/> vengono ignorati.<br/><br/>
        /// Se <paramref name="Width"/> e <paramref name="Height"/> sono impostati a 0, vengono usati i valori definiti dal sistema per la larghezza e l'altezza del cursore.<br/><br/>
        /// Il cursore è nascosto fino a quando non viene reso visibile.</remarks>
        [DllImport("User32.dll", EntryPoint = "CreateCaret", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CreateCaret(IntPtr WindowHandle, IntPtr BitmapHandle, int Width, int Height);

        /// <summary>
        /// Elimina l'attuale forma del cursore di inserimento, lo rilascia dalla finestra e lo rimuove dallo schermo.
        /// </summary>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Il cursore viene eliminato solo se la finestra nella task attuale ne è la proprietaria.</remarks>
        [DllImport("User32.dll", EntryPoint = "DestroyCaret", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyCaret();

        /// <summary>
        /// Recupera il tempo necessario per invertire i pixel del cursore.
        /// </summary>
        /// <returns>Tempo di lampeggiamento, in millisecondi, se l'operazione è riuscita, 0 altrimenti.</returns>
        /// <remarks>Se viene restituito <see cref="INFINITE"/>, il cursore non lampeggia.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetCaretBlinkTime", SetLastError = true)]
        internal static extern uint GetCaretBlinkTime();

        /// <summary>
        /// Recupera la posizione del cursore di inserimento.
        /// </summary>
        /// <param name="Coordinates">Struttura <see cref="POINT"/> che riceve la posizione.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Le coordinate sono sempre coordinate client della finestra che contiene il cursore di inserimento.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetCaretPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCaretPosition(out POINT Coordinates);

        /// <summary>
        /// Rimuove il cursore di inserimento dallo schermo.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra proprietaria del cursore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Nascondere il cursore non elimina l'attuale forma e il punto di inserimento resta valido.<br/><br/>
        /// <paramref name="WindowHandle"/> può essere impostato a <see cref="IntPtr.Zero"/>, la funzione ricerca la finestra proprietaria del cursore.<br/><br/>
        /// La funzione nasconde il cursore solo se la finestra ne è la proprietaria.</remarks>
        [DllImport("User32.dll", EntryPoint = "HideCaret", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HideCaret(IntPtr WindowHandle);

        /// <summary>
        /// Imposta il tempo di lampeggiamento ai millisecondi specificati.
        /// </summary>
        /// <param name="BlinkTimeMilliseconds">Tempo di lampeggiamento, in millisecondi.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SetCaretBlinkTime", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetCaretBlinkTime(uint BlinkTimeMilliseconds);

        /// <summary>
        /// Muove il cursore alle coordinate specificate.
        /// </summary>
        /// <param name="X">Nuova coordinata X del cursore.</param>
        /// <param name="Y">Nuova coordinata Y del cursore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SetCaretPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetCaretPosition(int X, int Y);

        /// <summary>
        /// Rende il cursore di inserimento visibile sullo schermo alla sua attuale posizione.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra proprietaria del cursore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <remarks>Se <paramref name="WindowHandle"/> è nullo, la funzione ricerca la proprietaria del cursore.<br/><br/>
        /// Il cursore inizia a lampeggiare non appena è visibile.<br/><br/>
        /// Questa funzione deve essere chiamata lo stesso numero di volte <see cref="HideCaret"/> è stata chiamata.</remarks>
        [DllImport("User32.dll", EntryPoint = "ShowCaret", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowCaret(IntPtr WindowHandle);
    }
}