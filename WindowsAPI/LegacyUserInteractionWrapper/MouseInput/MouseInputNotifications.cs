using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.LegacyUserInteractionWrapper.MouseInput.MouseInputEnumerations;

namespace WindowsAPI.LegacyUserInteractionWrapper.MouseInput
{
    /// <summary>
    /// Notifiche relative all'input del mouse.
    /// </summary>
    internal static class MouseInputNotifications
    {
        /// <summary>
        /// Postato quando l'utente preme il tasto sinistro del mouse mentre il cursore si trova nell'area client di una finestra.
        /// </summary>
        /// <remarks>Se il mouse non è stato catturato, il messaggio viene postato alla finestra sotto il cursore, altrimenti, il messaggio viene postato alla finestra che ha catturato il mouse.<br/><br/>
        /// wParam: indica se i tasti virtuali sono premuti, può essere uno o una combinazione dei valori dell'enumerazione <see cref="VirtualKeys"/>.<br/>
        /// lParam: gli ultimi due byte specificano la coordinata x del cursore, i primi due byte specificano la coordinata y del cursore.<br/>
        /// Le coordinate si riferiscono all'angolo superiore sinistro dell'area client.<br/><br/>
        /// Se un'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        internal const int WM_LBUTTONDOWN = 513;

        /// <summary>
        /// Postato quando l'utente rilascia il tasto sinistro del mouse mentre il cursore si trova nell'area client di una finestra.
        /// </summary>
        /// <remarks>Se il mouse non è stato catturato, il messaggio viene postato alla finestra sotto il cursore, altrimenti, il messaggio viene postato alla finestra che ha catturato il mouse.<br/><br/>
        /// wParam: indica se i tasti virtuali sono premuti, può essere uno o una combinazione dei valori dell'enumerazione <see cref="VirtualKeys"/>.<br/>
        /// lParam: gli ultimi due byte specificano la coordinata x del cursore, i primi due byte specificano la coordinata y del cursore.<br/>
        /// Le coordinate si riferiscono all'angolo superiore sinistro dell'area client.<br/><br/>
        /// Se un'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        internal const int WM_LBUTTONUP = 514;
    }
}