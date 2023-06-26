using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.Icons.IconFunctions;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Handle a un'icona.
    /// </summary>
    internal sealed class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Inizializza una nuova istanza di <see cref="SafeIconHandle"/> associandola all'handle indicato.
        /// </summary>
        /// <param name="Handle">Handle all'icona.</param>
        /// <param name="ownsHandle">Indica se l'handle appartiene al processo.</param>
        internal SafeIconHandle(IntPtr Handle, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(Handle);
        }

        /// <summary>
        /// Crea un'istanza di <see cref="Icon"/> dall'handle associato a questa istanza.
        /// </summary>
        /// <returns>Istanza di <see cref="Icon"/> creata dall'handle associato a questa istanza.</returns>
        internal Icon GetIcon()
        {
            return Icon.FromHandle(handle);
        }

        /// <summary>
        /// Permette l'assegnamento di un'istanza <see cref="SafeIconHandle"/> a un <see cref="IntPtr"/>.
        /// </summary>
        /// <param name="Handle">Istanza di <see cref="SafeIconHandle"/> associata all'handle.</param>
        public static implicit operator IntPtr(SafeIconHandle Handle)
        {
            return Handle is null ? IntPtr.Zero : Handle.handle;
        }

        protected override bool ReleaseHandle()
        {
            return DestroyIcon(handle);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}