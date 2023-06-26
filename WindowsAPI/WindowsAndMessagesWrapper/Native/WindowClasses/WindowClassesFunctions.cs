using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses.WindowClassesEnumerations;
using static WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses.WindowClassesConstants;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.WindowClasses
{
    /// <summary>
    /// Funzioni relative alle classi delle finestre.
    /// </summary>
    internal static class WindowClassesFunctions
    {
        /// <summary>
        /// Recupera informazioni su una finestra specificata o un valore all'offset specificato nella memoria aggiuntiva.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="Info">Informazione da recuperare.</param>
        /// <returns>L'informazione richiesta se l'operazione ha successo, <see cref="IntPtr.Zero"/> in caso contrario.</returns>
        /// <remarks><paramref name="Info"/> può essere impostato a un offset all'interno della memoria aggiuntiva della finestra, un valore dell'enumerazione <see cref="WindowInfo"/> oppure, se la finestra è un box di dialogo, a uno dei seguenti valori:<br/><br/>
        /// <see cref="DWLP_MSGRESULT"/><br/>
        /// <see cref="DWLP_DLGPROC"/><br/>
        /// <see cref="DWLP_USER"/><br/><br/>
        /// Se le informazioni non sono state precedentemente impostate, la funzione restituisce <see cref="IntPtr.Zero"/> per offset nella memoria aggiuntiva della finestra o della classe.</remarks>
        [DllImport("User32.dll", EntryPoint = "GetWindowLongPtrW", SetLastError = true)]
        internal static extern IntPtr GetWindowLongPtr(IntPtr WindowHandle, int Info);
    }
}