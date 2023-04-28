using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Window
{
    /// <summary>
    /// Enumerazioni relative alle finestre.
    /// </summary>
    internal static class WindowEnumerations
    {
        /// <summary>
        /// Tipo di finestra associata.
        /// </summary>
        internal enum WindowAncestor : uint
        {
            /// <summary>
            /// Finestra padre.
            /// </summary>
            /// <remarks>Non include la proprietaria.</remarks>
            GA_PARENT = 1,
            /// <summary>
            /// Finestra root.
            /// </summary>
            GA_ROOT,
            /// <summary>
            /// Finestra root di cui si è il proprietario.
            /// </summary>
            GA_ROOTOWNER
        }
    }
}