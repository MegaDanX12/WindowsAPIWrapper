using Microsoft.Win32.SafeHandles;
using System;
using static WindowsAPI.DesktopWindowManagerWrapper.Native.DWMFunctions;
using static WindowsAPI.ErrorHandlingWrapper.Native.HRESULTErrorConstants;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Handle a una miniatura DWM.
    /// </summary>
    public sealed class SafeDwmThumbnailHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeDwmThumbnailHandle"/>.
        /// </summary>
        /// <param name="Handle">Handle non gestito alla miniatura.</param>
        internal SafeDwmThumbnailHandle(IntPtr Handle) : base(true)
        {
            SetHandle(Handle);
        }

        /// <summary>
        /// Permette l'assegnamento di un <see cref="IntPtr"/> a un'istanza di <see cref="SafeDwmThumbnailHandle"/>.
        /// </summary>
        /// <param name="Pointer">Puntatore a memoria non gestita.</param>
        public static implicit operator SafeDwmThumbnailHandle(IntPtr Pointer)
        {
            return new SafeDwmThumbnailHandle(Pointer);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeDwmThumbnailHandle"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Pointer">Istanza di <see cref="SafeDwmThumbnailHandle"/> che punta a memoria non gestita.</param>
        public static implicit operator IntPtr(SafeDwmThumbnailHandle Pointer)
        {
            return Pointer is null ? IntPtr.Zero : Pointer.handle;
        }

        protected override bool ReleaseHandle()
        {
            return UnregisterThumbnailRelationship(handle) is S_OK;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}