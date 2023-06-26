using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.SafeHandles;
using WindowsAPI.UserInterfaceElementsWrapper.Native.Carets;
using static WindowsAPI.General.Native.GeneralStructures;

namespace WindowsAPI.UserInterfaceElementsWrapper.Carets
{
    /// <summary>
    /// Metodi per l'interazione con il cursore di inserimento testo.
    /// </summary>
    public static class Caret
    {
        /// <summary>
        /// Crea una nuova forma per il cursore di inserimento di sistema.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra proprietaria del cursore.</param>
        /// <param name="Shape">Bitmap usato per creare la forma del cursore.</param>
        /// <param name="Width">Larghezza del cursore, in unità logica.</param>
        /// <param name="Height">Altezza del cursore, in unità logica.</param>
        /// <param name="CaretIsGray">Indica se rendere il cursore grigio.</param>
        /// <remarks><paramref name="Width"/>, <paramref name="Height"/> e <paramref name="CaretIsGray"/> vengono ignorati se <paramref name="Shape"/> non è nullo.</remarks>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void CreateCaret(IntPtr WindowHandle, Bitmap Shape, int Width, int Height, bool CaretIsGray = false)
        {
            if (WindowHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(WindowHandle), "No window handle provided.");
            }
            if (Shape != null)
            {
                using (SafeBitmapHandle BitmapHandle = new SafeBitmapHandle(Shape.GetHbitmap(), true))
                {
                    if (!CaretFunctions.CreateCaret(WindowHandle, BitmapHandle, 0, 0))
                    {
                        throw new Win32Exception(Marshal.GetLastWin32Error());
                    }
                }
            }
            else
            {
                IntPtr CaretShape = IntPtr.Zero;
                if (CaretIsGray)
                {
                    CaretShape = new IntPtr(1);
                }
                if (!CaretFunctions.CreateCaret(WindowHandle, CaretShape, Width, Height))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Elimina la forma corrente del cursore di inserimento testo, lo libera dalla finestra proprietaria e lo rimuove dallo schermo.
        /// </summary>
        /// <exception cref="Win32Exception"></exception>
        public static void DestroyCaret()
        {
            if (!CaretFunctions.DestroyCaret())
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Recupera il tempo di lampeggiamento del cursore di inserimento.
        /// </summary>
        /// <returns>Il tempo di lampeggiamento in secondi.</returns>
        public static int GetCaretBlinkTime()
        {
            uint BlinkTime = CaretFunctions.GetCaretBlinkTime();
            if (BlinkTime is uint.MaxValue)
            {
                return -1;
            }
            else if (BlinkTime is 0)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
            else
            {
                return (int)(BlinkTime / 1000);
            }
        }

        /// <summary>
        /// Recupera la posizione del cursore di inserimento.
        /// </summary>
        /// <returns>Struttura <see cref="Point"/> contenente le coordinate del cursore.</returns>
        /// <exception cref="Win32Exception"></exception>
        public static Point GetCaretPosition()
        {
            if (CaretFunctions.GetCaretPosition(out POINT Coordinates))
            {
                return new Point(Coordinates.x, Coordinates.y);
            }
            else
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Nasconde il cursore di inserimento testo.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra proprietario del cursore.</param>
        /// <remarks><paramref name="WindowHandle"/> può essere impostato a <see cref="IntPtr.Zero"/>, in questo caso la finestra proprietaria del cursore viene trovata automaticamente.</remarks>
        /// <exception cref="Win32Exception"></exception>
        public static void HideCaret(IntPtr WindowHandle)
        {
            if (!CaretFunctions.HideCaret(WindowHandle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Imposta il tempo di lampeggiamento, in secondi, per il cursore di inserimento del testo.
        /// </summary>
        /// <param name="BlinkTime">Tempo di lampeggiamento, in secondi.</param>
        /// <exception cref="Win32Exception"></exception>
        public static void SetCaretBlinkTime(int BlinkTime)
        {
            if (BlinkTime <= 0)
            {
                if (!CaretFunctions.SetCaretBlinkTime(uint.MaxValue))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
            else
            {
                if (!CaretFunctions.SetCaretBlinkTime((uint)(BlinkTime * 1000)))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Imposta la posizione del cursore di inserimento testo.
        /// </summary>
        /// <param name="Coordinates">Coordinate dove spostare il cursore.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void SetCaretPosition(Point Coordinates)
        {
            if (Coordinates.IsEmpty)
            {
                throw new ArgumentException("Invalid coordinates.", nameof(Coordinates));
            }
            else
            {
                if (!CaretFunctions.SetCaretPosition(Coordinates.X, Coordinates.Y))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }
            }
        }

        /// <summary>
        /// Rende il cursore di inserimento testo visibile.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra proprietaria del cursore.</param>
        /// <remarks><paramref name="WindowHandle"/> può essere impostato a <see cref="IntPtr.Zero"/>, in questo caso la finestra proprietaria del cursore viene trovata automaticamente.<br/><br/>
        /// Questo metodo deve essere chiamato lo stesso numero di volte <see cref="HideCaret"/> è stato chiamato per visualizzare il cursore.</remarks>
        public static void ShowCaret(IntPtr WindowHandle)
        {
            if (!CaretFunctions.ShowCaret(WindowHandle))
            {
                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }
    }
}