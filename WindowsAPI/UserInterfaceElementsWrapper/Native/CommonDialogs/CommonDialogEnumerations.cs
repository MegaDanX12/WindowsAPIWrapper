using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsAPI.UserInterfaceElementsWrapper.Native.CommonDialogs
{
    internal static class CommonDialogEnumerations
    {
        /// <summary>
        /// Opzioni di inizializzazione della finestra di dialogo Colori.
        /// </summary>
        [Flags]
        public enum ColorDialogInitializationFlags
        {
            /// <summary>
            /// La finestra di dialogo visualizza tutti i colori disponbili nel set di base.
            /// </summary>
            CC_ANYCOLOR = 256,
            /// <summary>
            /// Abilita la procedura di hook.
            /// </summary>
            /// <remarks>Questo valore è usato per inizializzare la finestra di dialogo.</remarks>
            CC_ENABLEHOOK = 16,
            /// <summary>
            /// Viene utilizzato un modello di finestra di dialogo al posto di quello predefinito.
            /// </summary>
            ///<remarks>Questo valore è usato per inizializzare la finestra di dialogo.</remarks> 
            CC_ENABLETEMPLATE = 32,
            /// <summary>
            /// Viene utilizzato un blocco di memoria che contiene un modello di finestra di dialogo precaricato.
            /// </summary>
            /// <remarks>Questo valore è usato per inizializzare la finestra di dialogo.</remarks>
            CC_ENABLETEMPLATEHANDLE = 64,
            /// <summary>
            /// La finestra di dialogo visualizza controlli addizionali che permettono al'utente di creare colori personalizzati.
            /// </summary>
            CC_FULLOPEN = 2,
            /// <summary>
            /// Disabilita il pulsante "Definisci colori personalizzati".
            /// </summary>
            CC_PREVENTFULLOPEN = 4,
            /// <summary>
            /// La finestra di dialogo usa il colore specificato come selezione iniziale.
            /// </summary>
            CC_RGBINIT = 1,
            /// <summary>
            /// La finestra di dialogo visualizza il pulsante "Aiuto".
            /// </summary>
            /// <remarks>Deve essere specificato il proprietario.</remarks>
            CC_SHOWHELP = 8,
            /// <summary>
            /// La finestra di dialogo visualizza solamente colori solidi nel set di base.
            /// </summary>
            CC_SOLIDCOLOR = 128
        }

        /// <summary>
        /// Errori finestre di dialogo comuni.
        /// </summary>
        public enum CommonDialogErrorCode
        {
            #region Common Errors
            /// <summary>
            /// Impossibile creare la finestra di dialogo.
            /// </summary>
            CDERR_DIALOGFAILURE = 65535,
            /// <summary>
            /// 
            /// </summary>
            CDERR_GENERALCODES = 0,
            /// <summary>
            /// Dimensione della struttura non valida.
            /// </summary>
            CDERR_STRUCTSIZE,
            /// <summary>
            /// Errore durante l'inizializzazione.
            /// </summary>
            /// <remarks>Questo errore si verifica spesso se non è disponibile memoria sufficiente.</remarks>
            CDERR_INITIALIZATION,
            /// <summary>
            /// Modello non fornito.
            /// </summary>
            CDERR_NOTEMPLATE,
            /// <summary>
            /// Handle all'istanza non fornito.
            /// </summary>
            CDERR_NOHINSTANCE,
            /// <summary>
            /// Impossibile caricare la stringa specificata.
            /// </summary>
            CDERR_LOADSTRFAILURE,
            /// <summary>
            /// Impossibile trovare la risorsa specificata.
            /// </summary>
            CDERR_FINDRESFAILURE,
            /// <summary>
            /// Impossibile caricare la risorsa specificata.
            /// </summary>
            CDERR_LOADRESFAILURE,
            /// <summary>
            /// Impossibile bloccare la risorsa specificata.
            /// </summary>
            CDERR_LOCKRESFAILURE,
            /// <summary>
            /// Impossibile allocare la memoria per le strutture interne.
            /// </summary>
            CDERR_MEMALLOCFAILURE,
            /// <summary>
            /// Impossibile bloccare la memoria associata a un handle.
            /// </summary>
            CDERR_MEMLOCKFAILURE,
            /// <summary>
            /// Puntatore a procedura di hook non fornito.
            /// </summary>
            CDERR_NOHOOK,
            /// <summary>
            /// Errore durante la registrazione di un messaggio.
            /// </summary>
            CDERR_REGISTERMSGFAIL,
            #region Print Dialog Errors

            PDERR_PRINTERCODES = 4096,
            /// <summary>
            /// Impossibile caricare la risorsa necessaria.
            /// </summary>
            PDERR_SETUPFAILURE,
            /// <summary>
            /// Errore durante l'interpretazione delle stringhe nella sezione [devices] del file WIN.INI.
            /// </summary>
            PDERR_PARSEFAILURE,
            /// <summary>
            /// Struttura <see cref="CommonDialogStructures.DEVMODE"/> o <see cref="CommonDialogStructures.DEVNAMES"/> fornita quando non necessario.
            /// </summary>
            PDERR_RETDEFFAILURE,
            /// <summary>
            /// Impossibile caricare il driver per la stampante specificata.
            /// </summary>
            PDERR_LOADDRVFAILURE,
            /// <summary>
            /// Il driver della stampante non è riuscito a inizializzare la struttura <see cref="CommonDialogStructures.DEVMODE"/>.
            /// </summary>
            PDERR_GETDEVMODEFAIL,
            /// <summary>
            /// Errore durante l'inizializzazione.
            /// </summary>
            PDERR_INITFAILURE,
            /// <summary>
            /// Nessun driver di stampante trovato.
            /// </summary>
            PDERR_NODEVICES,
            /// <summary>
            /// Non esiste alcuna stampante predefinita.
            /// </summary>
            PDERR_NODEFAULTPRN,
            /// <summary>
            /// Le stampanti descritte sono differenti.
            /// </summary>
            PDERR_DNDMISMATCH,
            /// <summary>
            /// Errore durante la creazione di un contesto d'informazione.
            /// </summary>
            PDERR_CREATEICFAILURE,
            /// <summary>
            /// La sezione [devices] del file WIN.INI non contiene una voce per la stampante richiesta.
            /// </summary>
            PDERR_PRINTERNOTFOUND,
            /// <summary>
            /// La stampante descritta non è quella predefinita.
            /// </summary>
            PDERR_DEFAULTDIFFERENT,
            #region Choose Font Dialog Errors

            CFERR_CHOOSEFONTCODES = 8192,
            /// <summary>
            /// Non esistono font.
            /// </summary>
            CFERR_NOFONTS,
            /// <summary>
            /// La dimensione massima indicata è minore di quella minima.
            /// </summary>
            CFERR_MAXLESSTHANMIN,
            #region Find Name Dialog Errors

            FNERR_FILENAMECODES = 12288,
            /// <summary>
            /// Un tentativo di creare una sottoclasse di un listbox non è riuscito per mancanza di memoria.
            /// </summary>
            FNERR_SUBCLASSFAILURE,
            /// <summary>
            /// Nome file non valido.
            /// </summary>
            FNERR_INVALIDFILENAME,
            /// <summary>
            /// Dimensione buffer insufficiente.
            /// </summary>
            FNERR_BUFFERTOOSMALL,
            #region Find Replace Dialog Errors

            FRERR_FINDREPLACECODES = 16384,
            /// <summary>
            /// Buffer non valido.
            /// </summary>
            FRERR_BUFFERLENGTHZERO,
            #region Choose Color Dialog Errors

            CCERR_CHOOSECOLORCODES = 20480
            #endregion
            #endregion
            #endregion
            #endregion
            #endregion
        }
    }
}