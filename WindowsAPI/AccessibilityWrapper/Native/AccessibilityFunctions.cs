using static WindowsAPI.AccessibilityWrapper.Native.AccessibilityEnumerations;
using System;
using System.Runtime.InteropServices;

namespace WindowsAPI.AccessibilityWrapper.Native
{
    /// <summary>
    /// Funzioni accessibilità.
    /// </summary>
    internal static class AccessibilityFunctions
    {
        /// <summary>
        /// Registra una finestra come bersaglio dell'input di un tipo di puntatore specifico.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="PointerType">Tipo di puntatore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "RegisterPointerInputTarget", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool RegisterPointerInputTarget(IntPtr WindowHandle, POINTER_INPUT_TYPE PointerType);

        /// <summary>
        /// Annula la registrazione di una finestra come bersaglio dell'input di un tipo di puntatore specifico.
        /// </summary>
        /// <param name="WindowHandle">Handle alla finestra.</param>
        /// <param name="PointerType">Tipo di puntatore.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "UnregisterPointerInputTarget", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnregisterPointerInputTarget(IntPtr WindowHandle, POINTER_INPUT_TYPE PointerType);

        /// <summary>
        /// Recupera o imposta i parametri di sistema.
        /// </summary>
        /// <param name="Parameter">Parametro da recuperare o da impostare.</param>
        /// <param name="ParameterData1">Dati relativi al parametro.</param>
        /// <param name="ParameterData2">Dati relativi al parametro.</param>
        /// <param name="Options">Opzioni di aggiornamento profilo utente.</param>
        /// <returns>true se l'operazione è riuscita, false altrimenti.</returns>
        [DllImport("User32.dll", EntryPoint = "SystemParametersInfoW", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SystemParametersInfo(AccessibilityParameter Parameter, uint ParameterData1, IntPtr ParameterData2, UserProfileUpdateOptions Options);
    }
}