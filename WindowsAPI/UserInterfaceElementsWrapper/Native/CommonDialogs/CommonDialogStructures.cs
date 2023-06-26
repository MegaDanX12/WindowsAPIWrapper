using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs.CommonDialogEnumerations;
using static WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs.CommonDialogCallbacks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs
{
    internal static class CommonDialogStructures
    {
        /// <summary>
        /// Informazioni di inizializzazione della finestra di dialogo Colori.
        /// </summary>
        /// <remarks>Questa struttura contiene le opzioni scelte dall'utente al momento della chiusura della finestra.</remarks>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CHOOSECOLOR
        {
            /// <summary>
            /// Dimensione, in byte, della struttura.
            /// </summary>
            public uint Size;
            /// <summary>
            /// Handle alla finestra proprietaria del dialogo.
            /// </summary>
            public IntPtr OwnerHandle;
            /// <summary>
            /// Il contenuto di questo campo dipende da <see cref="Flags"/>.
            /// </summary>
            /// <remarks>Se <see cref="ColorDialogInitializationFlags.CC_ENABLETEMPLATEHANDLE"/> è incluso in <see cref="Flags"/>, questo campo è un handle a un oggetto di memoria che contiene un modello di finestra di dialogo.<br/>
            /// Se <see cref="ColorDialogInitializationFlags.CC_ENABLETEMPLATE"/> è incluso in <see cref="Flags"/>, questo campo è un handle a un modulo che contiene il modella di finestra di dialogo, il cui nome è indicato in <see cref="TemplateName"/>.</remarks>
            public IntPtr InstanceHandle;
            /// <summary>
            /// Colore selezionato.
            /// </summary>
            /// <remarks>Se <see cref="Flags"/> include <see cref="ColorDialogInitializationFlags.CC_RGBINIT"/>, questo campo specifica il colore inizialmente selezionato.<br/>
            /// Se il colore indicato non esiste, il sistema seleziona quello solido più vicino.<br/><br/>
            /// Se <see cref="Flags"/> non include <see cref="ColorDialogInitializationFlags.CC_RGBINIT"/> oppure se questo campo è impostato a 0, il colore inizialmente selezionato è il nero.<br/><br/>
            /// Quanto l'utente clicca il tasto OK, questo campo specifica il colore selezionato dall'utente.</remarks>
            public uint SelectedColor;
            /// <summary>
            /// Puntatore a un array di 16 elementi che contengono i colori personalizzati.
            /// </summary>
            /// <remarks>Se l'utente modifica questi colori, il sistema aggiorna l'array.</remarks>
            public IntPtr CustomColors;
            /// <summary>
            /// Opzioni di inizializzazione della finestra di dialogo.
            /// </summary>
            public ColorDialogInitializationFlags Flags;
            /// <summary>
            /// Dati definiti dall'applicazione passati alla procedura di hook.
            /// </summary>
            public IntPtr CustomData;
            /// <summary>
            /// Procedura di hook che elabora i messaggi intesi per la finestra di dialogo.
            /// </summary>
            ///<remarks>Questo campo viene ignorato se <see cref="Flags"/> non include <see cref="ColorDialogInitializationFlags.CC_ENABLEHOOK"/>.</remarks> 
            [MarshalAs(UnmanagedType.FunctionPtr)]
            public DialogMessageHandler MessageHandler;
            /// <summary>
            /// Nome del modella della finestra di dialogo nel modulo identificato da <see cref="InstanceHandle"/>.
            /// </summary>
            /// <remarks>Questo campo viene ignorato se <see cref="Flags"/> non include <see cref="ColorDialogInitializationFlags.CC_ENABLETEMPLATE"/>.</remarks>
            [MarshalAs(UnmanagedType.LPWStr)]
            public string TemplateName;

            public IntPtr EditInfo;
        }
    }
}