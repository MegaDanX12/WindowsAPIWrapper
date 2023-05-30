using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSEnumerations;
using static WindowsAPI.NationalLanguageSupportWrapper.Native.NLSConstants;

namespace WindowsAPI.NationalLanguageSupportWrapper.Native
{
    /// <summary>
    /// Callback NLS.
    /// </summary>
    internal static class NLSCallbacks
    {
        /// <summary>
        /// Callback per l'enumerazione delle informazioni di un calendario.
        /// </summary>
        /// <param name="CalendarInfoString">Informazione.</param>
        /// <param name="CalID">Caledario associato all'informazione.</param>
        /// <param name="Reserved">Riservato, deve essere <see cref="IntPtr.Zero"/>.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool CalendarInfoEnumProcedure(string CalendarInfoString, CalendarID CalID, string Reserved, IntPtr lParam);

        /// <summary>
        /// Callback per l'enumerazione dei formati data.
        /// </summary>
        /// <param name="DateFormatString">Stringa di formato data.</param>
        /// <param name="CalID">Calendario associato alla stringa.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool DateFormatEnumProcedure(string DateFormatString, CalendarID CalID, IntPtr lParam);

        /// <summary>
        /// Callback per l'enumerazione delle code page.
        /// </summary>
        /// <param name="CodePageString">Identificatore code page.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool CodePagesEnumProcedure(string CodePageString);

        /// <summary>
        /// Callback per l'enumerazione delle località.
        /// </summary>
        /// <param name="LocaleName">Nome località.</param>
        /// <param name="LocaleInfo">Informazioni sulla località.</param>
        /// <param name="lParam">Valore fornito dal'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        /// <remarks>LocaleInfo può includere una combinazione dei seguenti valori:<br/><br/>
        /// <see cref="LOCALE_ALL"/><br/>
        /// <see cref="LOCALE_ALTERNATE_SORTS"/><br/>
        /// <see cref="LOCALE_NEUTRALDATA"/><br/>
        /// <see cref="LOCALE_REPLACEMENT"/><br/>
        /// <see cref="LOCALE_SUPPLEMENTAL"/><br/>
        /// <see cref="LOCALE_WINDOWS"/><br/>
        /// <see cref="LOCALE_NEUTRALDATA"/><br/>
        /// <see cref="LOCALE_SPECIFICDATA"/></remarks>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool LocaleEnumProcedure(string LocaleName, uint LocaleInfo, IntPtr lParam);

        /// <summary>
        /// Callback per l'enumerazione dei formati ora.
        /// </summary>
        /// <param name="TimeFormatString">Stringa di formato ora.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool TimeFormatEnumProcedure(string TimeFormatString, IntPtr lParam);

        /// <summary>
        /// Callback per l'enumerazione dei nomi geografici.
        /// </summary>
        /// <param name="GeoNameString">Nome geografico.</param>
        /// <param name="lParam">Valore fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        [return: MarshalAs(UnmanagedType.Bool)]
        internal delegate bool GeoNameEnumProcedure(string GeoNameString, IntPtr lParam);
    }
}