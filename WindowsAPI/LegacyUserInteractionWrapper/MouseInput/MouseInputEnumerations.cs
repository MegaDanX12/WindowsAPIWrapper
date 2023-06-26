using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.LegacyUserInteractionWrapper.MouseInput
{
    /// <summary>
    /// Enumerazioni relative all'input del mouse.
    /// </summary>
    internal static class MouseInputEnumerations
    {
        /// <summary>
        /// Tasti virtuali.
        /// </summary>
        [Flags]
        internal enum VirtualKeys
        {
            /// <summary>
            /// Il tasto CTRL è premuto.
            /// </summary>
            MK_CONTROL = 8,
            /// <summary>
            /// Il tasto sinistro del mouse è premuto.
            /// </summary>
            MK_LBUTTON = 1,
            /// <summary>
            /// Il tasto centrale del mouse è premuto.
            /// </summary>
            MK_MBUTTON = 16,
            /// <summary>
            /// Il tasto destro del mouse è premuto.
            /// </summary>
            MK_RBUTTON = 2,
            /// <summary>
            /// Il tasto SHIFT è premuto.
            /// </summary>
            MK_SHIFT = 4,
            /// <summary>
            /// Il primo tasto X è premuto.
            /// </summary>
            MK_XBUTTON1 = 32,
            /// <summary>
            /// Il secondo tasto X è premuto.
            /// </summary>
            MK_XBUTTON2 = 64
        }
    }
}