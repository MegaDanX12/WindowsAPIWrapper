using Microsoft.Win32.SafeHandles;
using System;
using static WindowsAPI.WindowsGDIWrapper.Native.DeviceContexts.DeviceContextsFunctions;

namespace WindowsAPI.SafeHandles
{
    /// <summary>
    /// Handle a un oggetto grafico.
    /// </summary>
    internal abstract class SafeGraphicsHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        protected SafeGraphicsHandle(IntPtr Pointer, bool OwnsHandle) : base(OwnsHandle)
        {
            SetHandle(Pointer);
        }

        protected SafeGraphicsHandle(bool ownsHandle) : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            return DeleteObject(handle);
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}