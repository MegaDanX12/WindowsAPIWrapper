using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.WindowsGDIWrapper.Native.DeviceContexts;

namespace WindowsAPI.WindowsGDIWrapper.DeviceContexts
{
    /// <summary>
    /// Metodi per utilizzare contesti dispositivo.
    /// </summary>
    public static class DeviceContexts
    {
        /// <summary>
        /// Elimina un oggetto GDI.
        /// </summary>
        /// <param name="ObjectHandle">Handle all'oggetto.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool DeleteObject(IntPtr ObjectHandle)
        {
            if (ObjectHandle == IntPtr.Zero)
            {
                throw new ArgumentNullException(nameof(ObjectHandle), "No handle provided.");
            }
            return DeviceContextsFunctions.DeleteObject(ObjectHandle);
        }
    }
}