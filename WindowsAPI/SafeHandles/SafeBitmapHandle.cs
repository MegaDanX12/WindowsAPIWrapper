using System;
using System.Drawing;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Handle a un bitmap.
    /// </summary>
    internal sealed class SafeBitmapHandle : SafeGraphicsHandle
    {
        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeBitmapHandle"/>.
        /// </summary>
        /// <param name="Handle">Handle non gestito al bitmap.</param>
        /// <param name="OwnsHandle">Indica se l'handle appartiene al processo.</param>
        internal SafeBitmapHandle(IntPtr Handle, bool OwnsHandle) : base(Handle, OwnsHandle)
        {
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeBitmapHandle"/>.
        /// </summary>
        /// <param name="ownsHandle">Indica se l'handle appartiene al processo.</param>
        internal SafeBitmapHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeBitmapHandle"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Handle">Istanza di <see cref="SafeBitmapHandle"/> associata all'handle.</param>
        public static implicit operator IntPtr(SafeBitmapHandle Handle)
        {
            return Handle is null ? IntPtr.Zero : Handle.handle;
        }

        /// <summary>
        /// Crea un'istanza di <see cref="Bitmap"/> a partire dall'handle gestito da questa istanza.
        /// </summary>
        /// <returns>Istanza di <see cref="Bitmap"/>.</returns>
        internal Bitmap GetBitmap()
        {
            return Image.FromHbitmap(handle);
        }
    }
}