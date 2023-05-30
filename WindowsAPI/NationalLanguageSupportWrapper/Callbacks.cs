using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAPI.NationalLanguageSupportWrapper.DataClasses;
using static WindowsAPI.NationalLanguageSupportWrapper.Enumerations;

namespace WindowsAPI.NationalLanguageSupportWrapper
{
    /// <summary>
    /// Callback NLS.
    /// </summary>
    public static class Callbacks
    {
        /// <summary>
        /// Callback per l'enumerazione delle informazioni di un calendario.
        /// </summary>
        /// <param name="InfoString">Informazione recuperata.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="Object">Oggetto fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        public delegate bool CalendarInfoEnumCallback(string InfoString, Calendar Calendar, object Object);

        /// <summary>
        /// Callback per l'enumerazione delle stringhe di formato data.
        /// </summary>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="Calendar">Calendario.</param>
        /// <param name="Object">Oggetto fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        public delegate bool DateFormatsEnumCallback(string FormatString, Calendar Calendar, object Object);

        /// <summary>
        /// Callback per l'enumerazione delle code page.
        /// </summary>
        /// <param name="CodePage">Code page.</param>
        /// <returns>true per continuare l'enumerazione, false altrimenti.</returns>
        public delegate bool CodePagesEnumCallback(string CodePage);

        /// <summary>
        /// Callback per l'enumerazione delle località.
        /// </summary>
        /// <param name="LocaleInfo">Informazioni sulla località.</param>
        /// <param name="Object">Oggetto fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        public delegate bool LocaleEnumCallback(LocaleInfo LocaleInfo, object Object);

        /// <summary>
        /// Callback per l'enumerazione dei formati tempo.
        /// </summary>
        /// <param name="FormatString">Stringa di formato.</param>
        /// <param name="Object">Oggetto fornito dall'applicazione.</param>
        /// <returns>true per contnuare l'enumerazione, false per fermarla.</returns>
        public delegate bool TimeFormatsEnumCallback(string FormatString, object Object);

        /// <summary>
        /// Callback per l'enumerazione dei nomi geografici.
        /// </summary>
        /// <param name="GeoName">Nome geografico.</param>
        /// <param name="Object">Oggetto fornito dall'applicazione.</param>
        /// <returns>true per continuare l'enumerazione, false per fermarla.</returns>
        public delegate bool GeoNamesEnumCallback(string GeoName, object Object);
    }
}