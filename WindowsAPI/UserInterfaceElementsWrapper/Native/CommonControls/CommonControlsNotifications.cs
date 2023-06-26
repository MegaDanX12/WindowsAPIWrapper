using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonControls
{
    /// <summary>
    /// Notifiche comuni dei controlli Windows.
    /// </summary>
    internal static class CommonControlsNotifications
    {
        /// <summary>
        /// Primo valore per le notifiche.
        /// </summary>
        internal const uint NM_FIRST = unchecked((uint)0 - 0);

        /// <summary>
        /// Ultimo valore per le notifiche.
        /// </summary>
        private const uint NM_LAST = unchecked((uint)0 - 99);


        internal const uint NM_OUTOFMEMORY = unchecked(NM_FIRST - 1);


        internal const uint NM_CLICK = unchecked(NM_FIRST - 2);


        internal const uint NM_DBLCLK = unchecked(NM_FIRST - 3);


        internal const uint NM_RETURN = unchecked(NM_FIRST - 4);


        internal const uint NM_RCLICK = unchecked(NM_FIRST - 5);


        internal const uint NM_RDBLCLK = unchecked(NM_FIRST - 6);


        internal const uint NM_SETFOCUS = unchecked(NM_FIRST - 7);


        internal const uint NM_KILLFOCUS = unchecked(NM_FIRST - 8);


        internal const uint NM_HOVER = unchecked(NM_FIRST - 13);


        internal const uint NM_NCHITTEST = unchecked(NM_FIRST - 14);


        internal const uint NM_KEYDOWN = unchecked(NM_FIRST - 15);


        internal const uint NM_RELEASEDCAPTURE = unchecked(NM_FIRST - 16);


        internal const uint NM_SETCURSOR =  unchecked(NM_FIRST - 17);


        internal const uint NM_CHAR = unchecked(NM_FIRST - 18);


        internal const uint NM_TOOLTIPSCREATED = unchecked(NM_FIRST - 19);


        internal const uint NM_LDOWN = unchecked(NM_FIRST - 20);


        internal const uint NM_RDOWN = unchecked(NM_FIRST - 21);


        internal const uint NM_THEMECHANGED = unchecked(NM_FIRST - 22);


        internal const uint NM_FONTCHANGED = unchecked(NM_FIRST - 23);


        internal const uint NM_CUSTOMTEXT = unchecked(NM_FIRST - 24);


        internal const uint NM_TVSTATEIMAGECHANGING = unchecked(NM_FIRST - 24);
    }
}