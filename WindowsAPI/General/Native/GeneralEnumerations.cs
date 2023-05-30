using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.General.Native
{
    /// <summary>
    /// Enumerazioni generali.
    /// </summary>
    internal static class GeneralEnumerations
    {
        /// <summary>
        /// Mese.
        /// </summary>
        internal enum Month : ushort
        {
            January = 1, 
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

        /// <summary>
        /// Giorno della settimana.
        /// </summary>
        internal enum DayOfWeek : ushort
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }
    }
}