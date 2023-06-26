using Microsoft.Win32.SafeHandles;
using System;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.ImageLists.ImageListFunctions;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Handle a una lista immagini.
    /// </summary>
    internal sealed class SafeImageListHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeImageListHandle"/>, associandola all'handle specificato.
        /// </summary>
        /// <param name="Handle">Handle alla lista immagini.</param>
        /// <param name="ownsHandle">Indica se l'handle appartiene al processo.</param>
        internal SafeImageListHandle(IntPtr Handle, bool OwnsHandle) : base(OwnsHandle)
        {
            SetHandle(Handle);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeImageListHandle"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeImageListHandle"/> che si riferisce a una lista di immagini.</param>
        public static implicit operator IntPtr(SafeImageListHandle Pointer)
        {
            return Pointer is null ? IntPtr.Zero : Pointer.handle;
        }

        protected override bool ReleaseHandle()
        {
            return DestroyImageList(handle);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}