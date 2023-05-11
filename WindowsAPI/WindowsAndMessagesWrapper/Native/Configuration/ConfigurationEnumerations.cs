using System;

namespace WindowsAPI.WindowsAndMessagesWrapper.Native.Configuration
{
    /// <summary>
    /// Enumerazioni usate dalle funzioni di configurazione.
    /// </summary>
    internal static class ConfigurationEnumerations
    {
        /// <summary>
        /// Opzioni di aggiornamento profilo utente.
        /// </summary>
        [Flags]
        internal enum UserProfileUpdateOptions : uint
        {
            /// <summary>
            /// Scrive la nuova impostazione nel profilo utente.
            /// </summary>
            SPIF_UPDATEINIFILE = 1,
            /// <summary>
            /// Invia il messaggio WM_SETTINGCHANGE dopo aver aggiornato il profilo utente.
            /// </summary>
            SPIF_SENDWININICHANGE = 2
        }


        internal enum SystemMetric
        {
            /// <summary>
            /// Modo in cui il sistema dispone le finestre minimizzate.
            /// </summary>
            /// <remarks>Uno dei valori di <see cref="MinimizedWindowsArrangeMode"/>.</remarks>
            SM_ARRANGE = 56,
            /// <summary>
            /// Specifica come è stato avviato il sistema.
            /// </summary>
            /// <remarks>Uno dei seguenti valori: <br/><br/>
            /// 0 -> avvio normale<br/>
            /// 1 -> avvio in modalità sicura<br/>
            /// 2 -> avvio in modalità sicura con rete</remarks>
            SM_CLEANBOOT = 67,
            /// <summary>
            /// Numero di display.
            /// </summary>
            SM_CMONITORS = 80,
            /// <summary>
            /// Numero di pulsanti del mouse.
            /// </summary>
            /// <remarks>Il valore restituto è 0 se il mouse non è installato.</remarks>
            SM_CMOUSEBUTTONS = 43,
            /// <summary>
            /// Stato del laptop.
            /// </summary>
            /// <remarks>Il valore restituito è 0 se il laptop è in modalità coperta, diverso da zero altrimenti.</remarks>
            SM_CONVERTIBLESLATEMODE = 8195,
            /// <summary>
            /// Larghezza del bordo di una finestra, in pixel.
            /// </summary>
            SM_CXBORDER = 5,
            /// <summary>
            /// Larghezza nominale di un cursore, in pixel.
            /// </summary>
            SM_CXCURSOR = 13,
            /// <summary>
            /// Spessore della cornice attorno al perimetro di una finestra che ha una barra del titolo ma non è ridimensionabile, in pixel.
            /// </summary>
            /// <remarks>Questo valore rappresenta l'altezza del bordo orizzontale.</remarks>
            SM_CXDLGFRAME = 7,
            /// <summary>
            /// Larghezza del rettangolo attorno alla posizione del primo click in una sequenza di doppio click, in pixel.
            /// </summary>
            SM_CSDOUBLECLK = 36,
            /// <summary>
            /// Numero di pixel su entrambi i lati di un punto di pressione del mouse per i quali il puntatore del mouse può muoversi prima che inizi un'operazione di trascinamento.
            /// </summary>
            /// <remarks>Se questo valore è negativo, viene sottratto dalla sinistra del punto di pressione e aggiunto alla sua destra.</remarks>
            SM_CXDRAG = 68,
            /// <summary>
            /// Larghezza, di un bordo 3D, in pixel.
            /// </summary>
            SM_CXEDGE = 45,
            /// <summary>
            /// Spessore della cornice attorno al perimetro di una finestra che ha una barra del titolo ma non è ridimensionabile, in pixel.
            /// </summary>
            /// <remarks>Questo valore rappresenta l'altezza del bordo orizzontale.</remarks>
            SM_CXFIXEDFRAME = SM_CXDLGFRAME,
            /// <summary>
            /// Larghezza dei bordi sinistro e destro del rettangolo di focus, in pixel.
            /// </summary>
            SC_CXFOCUSBORDER = 83,
            /// <summary>
            /// Lo spessore di un bordo di ridimensionamento attorno al perimetro di una finestra che può essere ridimensionata, in pixel.
            /// </summary>
            /// <remarks>Questo valore è la larghezza del bordo orizzontale.</remarks>
            SM_CXFRAME = 32,
            /// <summary>
            /// Larghezza dell'area client per una finestra a schermo intero sul monitor primario, in pixel.
            /// </summary>
            SM_CXFULLSCREEN = 16,
            /// <summary>
            /// Larghezza del bitmap di una freccia sulla barra di scorrimento orizzontale, in pixel.
            /// </summary>
            SM_CXHSCROLL = 21,
            /// <summary>
            /// Larghezza del thumb box nella barra di scorrimento orizzontale, in pixel.
            /// </summary>
            SM_CXHTHUMB = 10,
            /// <summary>
            /// Larghezza predefinita di un'icona, in pixel.
            /// </summary>
            SM_CXICON = 11,
            /// <summary>
            /// Larghezza di una cella della griglia per oggetti nella vista a icone grandi, in pixel.
            /// </summary>
            SM_CXICONSPACING = 38,
            /// <summary>
            /// Larghezza predefinita, in pixel, di una finestra top level ingrandita sul monitor primario.
            /// </summary>
            SM_CXMAXIMIZED = 61,
            /// <summary>
            /// Larghezza massima predefinita di una finestra che ha una barra del titolo e un bordo di ridimensionamento, in pixel.
            /// </summary>
            /// <remarks>Questa metrica si riferisce a tutto il desktop, l'utente non può trascinare la cornice della finestra a una dimensione più larga di questa.</remarks>
            SM_CXMAXTRACK = 59,
            /// <summary>
            /// Larghezza del bitmap del check-mark predefinito, in pixel.
            /// </summary>
            SM_CXMENUCHECK = 71,
            /// <summary>
            /// Larghezza dei pulsanti della barra dei menù, in pixel.
            /// </summary>
            SM_CXMENUSIZE = 54,
            /// <summary>
            /// Larghezza minima di una finestra, in pixel.
            /// </summary>
            SM_CXMIN = 28,
            /// <summary>
            /// Larghezza di una finestra ridotta a icona, in pixel.
            /// </summary>
            SM_CXMINIMIZED = 57,
            /// <summary>
            /// Larghezza di una cella della griglia per una finestra minimizzata, in pixel.
            /// </summary>
            SM_CXMINSPACING = 47,
            /// <summary>
            /// Larghezza minima di tracciamento di una finestra, in pixel.
            /// </summary>
            /// <remarks>L'utente non può ridimensionare la cornice di una finestra a una dimensione minore di questa.</remarks>
            SM_CXMINTRACK = 34,
            /// <summary>
            /// Quantità di padding per finestre con una barra del titolo, in pixel.
            /// </summary>
            SM_CXPADDEDBORDER = 92,
            /// <summary>
            /// Larghezza dello schermo del monitor primario, in pixel.
            /// </summary>
            SM_CXSCREEN = 0,
            /// <summary>
            /// Larghezza di un pulsante nella barra del titolo, in pixel.
            /// </summary>
            SM_CXSIZE = 30,
            /// <summary>
            /// Lo spessore di un bordo di ridimensionamento attorno al perimetro di una finestra che può essere ridimensionata, in pixel.
            /// </summary>
            /// <remarks>Questo valore è la larghezza del bordo orizzontale.</remarks>
            SM_CXSIZEFRAME = SM_CXFRAME,
            /// <summary>
            /// Larghezza raccomandata di un'icona piccola, in pixel.
            /// </summary>
            SM_CXSMICON = 49,
            /// <summary>
            /// Larghezza dei pulsanti piccoli della barra del titolo, in pixel.
            /// </summary>
            SM_CXSMSIZE = 52,
            /// <summary>
            /// Larghezza dello schermo virtuale, in pixel.
            /// </summary>
            /// <remarks>Questa metrica sono le coordinate per la parte sinistra dello schermo virtuale.</remarks>
            SM_CXVIRTUALSCREEN = 78,
            /// <summary>
            /// Larghezza di una barra di scorrimento verticale, in pixel.
            /// </summary>
            SM_CXVSCROLL = 2,
            /// <summary>
            /// Altezza del bordo di una finestra, in pixel.
            /// </summary>
            SM_CYBORDER = 6,
            /// <summary>
            /// Altezza della barra del titolo, in pixel.
            /// </summary>
            SM_CYCAPTION = 4,
            /// <summary>
            /// Altezza nominale del cursore, in pixel.
            /// </summary>
            SM_CYCURSOR = 14,
            /// <summary>
            /// Spessore della cornice attorno al perimetro di una finestra che ha una barra del titolo ma non è ridimensionabile, in pixel.
            /// </summary>
            /// <remarks>Questo valore è la larghezza del bordo verticale.</remarks>
            SM_CYDLGFRAME = 8,
            /// <summary>
            /// Altezza del rettangolo attorno alla posizione del primo click in una sequenza di doppio click, in pixel.
            /// </summary>
            SM_CYDOUBLECLK = 37,
            /// <summary>
            /// Numero di pixel sopra e sotto un punto di pressione del mouse dove il puntatore può muoversi prima di iniziare un'operazione di trascinamento.
            /// </summary>
            /// <remarks>Se questo valore è negativo, viene sottratto dal punto superiore e aggiungo a quello inferiore.</remarks>
            SM_CYDRAG = 69,
            /// <summary>
            /// Altezza del bordo 3D.
            /// </summary>
            SM_CYEDGE = 46,
            /// <summary>
            /// Spessore della cornice attorno al perimetro di una finestra che ha una barra del titolo ma non è ridimensionabile, in pixel.
            /// </summary>
            /// <remarks>Questo valore è la larghezza del bordo verticale.</remarks>
            SM_CYFIXEDFRAME = SM_CYDLGFRAME,
            /// <summary>
            /// Altezza dei bordi superiore e inferiore del rettangolo di focus, in pixel.
            /// </summary>
            SM_CYFOCUSBORDER = 84,
            /// <summary>
            /// Spessore del bordo di ridimensionamento attorno al perimetro di una finestra che può essere ridimensionata, in pixel.
            /// </summary>
            /// <remarks>Questa metrica rappresenta l'altezza del bordo verticale.</remarks>
            SM_CYFRAME = 33,
            /// <summary>
            /// Altezza dell'area client di una finestra a schermo intero sul monitor primario, in pixel.
            /// </summary>
            SM_CYFULLSCREEN = 17,
            /// <summary>
            /// Altezza di una barra di scorrimento orizzontale, in pixel.
            /// </summary>
            SM_CYHSCROLL = 3,
            /// <summary>
            /// Altezza predefinita di un'icona, in pixel.
            /// </summary>
            SM_CYICON = 12,
            /// <summary>
            /// Altezza della cella della griglia per oggetti in vista ad icone grandi, in pixel.
            /// </summary>
            SM_CYICONSPACING = 39,
            /// <summary>
            /// Altezza della finestra Kanji nella parte inferiore dello schermo, in pixel.
            /// </summary>
            SM_CYKANJIWINDOW = 18,
            /// <summary>
            /// Altezza predefinita, in pixel, di una finestra top-level ingrandita sul monitor primario.
            /// </summary>
            SM_CYMAXIMIZED = 62,
            /// <summary>
            /// Altezza massima predefinita di una finestra che ha una barra del titolo e un bordo di ridimensionamento, in pixel.
            /// </summary>
            /// <remarks>Questa metrica si riferisce a tutto il desktop.</remarks>
            SM_CYMAXTRACK = 60,
            /// <summary>
            /// Altezza di una barra dei menù a singola linea, in pixel.
            /// </summary>
            SM_CYMENU = 15,
            /// <summary>
            /// Altezza del bitmap del check-mark predefinito di un menù, in pixel.
            /// </summary>
            SM_CYMENUCHECK = 72,
            /// <summary>
            /// Altezza dei pulsanti della barra dei menù.
            /// </summary>
            SM_CYMENUSIZE = 55,
            /// <summary>
            /// Altezza minima di una finestra, in pixel.
            /// </summary>
            SM_CYMIN = 29,
            /// <summary>
            /// Altezza di una finestra minimizzata, in pixel.
            /// </summary>
            SM_CYMINIMIZED = 58,
            /// <summary>
            /// Altezza di una cella della griglia per una finestra minimizzata, in pixel.
            /// </summary>
            SM_CYMINSPACING = 48,
            /// <summary>
            /// Altezza minima di tracciamento di una finestra, in pixel.
            /// </summary>
            SM_CYMINTRACK = 35,
            /// <summary>
            /// Altezza dello schermo del monitor primario, in pixel.
            /// </summary>
            SM_CYSCREEN = 1,
            /// <summary>
            /// Altezza del pulsante nella barra del titolo di una finestra, in pixel.
            /// </summary>
            SM_CYSIZE = 31,
            /// <summary>
            /// Spessore del bordo di ridimensionamento attorno al perimetro di una finestra che può essere ridimensionata, in pixel.
            /// </summary>
            /// <remarks>Questa metrica rappresenta l'altezza del bordo verticale.</remarks>
            SM_CYSIZEFRAME = SM_CYFRAME,
            /// <summary>
            /// Altezza della barra del titolo piccola, in pixel.
            /// </summary>
            SM_CYSMCAPTION = 51,
            /// <summary>
            /// Altezza raccomandata di un'icona piccola, in pixel.
            /// </summary>
            SM_CYSMICON = 50,
            /// <summary>
            /// Altezza di un pulsante di una barra del titolo piccola, in pixel.
            /// </summary>
            SM_CYSMSIZE = 53,
            /// <summary>
            /// Altezza dello schermo virtuale, in pixel.
            /// </summary>
            SM_CYVIRUALSCREEN = 79,
            /// <summary>
            /// Altezza del bitmap della freccia su una barra di scorrimento verticale, in pixel.
            /// </summary>
            SM_CYVSCROLL = 20,
            /// <summary>
            /// Altezza di un thumb box in una barra di scorrimento verticale, in pixel.
            /// </summary>
            SM_CYVTHUMB = 9,
            /// <summary>
            /// Indica se User32.dll supporta DBCS (diverso da 0) o meno (0).
            /// </summary>
            SM_DBCSENABLED = 42,
            /// <summary>
            /// Indica se la versione di debug di User.exe è installata (diverso da 0) o meno (0).
            /// </summary>
            SM_DEBUG = 22,
            /// <summary>
            /// Indica se il sistema operativo corrente è Windows 7 oppure Windows Server 2008 R2 e il servizio Tablet PC Input Service è in funzione (diverso da 0) o meno (0).
            /// </summary>
            /// <remarks>Il valore restituito è una maschera di bit che specifica il tipo di input digitalizzatore supportato dal dispositivo (<see cref="DigitizerInputType"/>).</remarks>
            SM_DIGITIZER = 94,
            /// <summary>
            /// Indica se il gestore del metodo di input/editor del metodo di input è abilitato (diverso da 0) o meno (0).
            /// </summary>
            /// <remarks>Questo valore indica se il sistema è pronto a usare un IME basato su Unicode in un'applicazione Unicode.</remarks>
            SM_IMMENABLED = 82,
            /// <summary>
            /// Indica se esistono digitalizzatori nel sistema (diverso da 0) o meno (0).
            /// </summary>
            /// <remarks>Questa metrica restituisce il numero massimo aggregato del massimo numero di contatti supportati da ogni digitalizzatore nel sistema.</remarks>
            SM_MAXIMUMTOUCHES = 95,
            /// <summary>
            /// Indica se il sistema operativo è Windows XP Media Center Edition (diverso da 0) o meno (0).
            /// </summary>
            SM_MEDIACENTER = 87,
            /// <summary>
            /// Indica se i menù a discesa sono allineati a destra con il corrispondente oggetto della barra dei menù (diverso da 0) o se sono allineati a sinistra (0).
            /// </summary>
            SM_MENUDROPALIGNMENT = 40,
            /// <summary>
            /// Indica se il sistema è abilitato per Ebreo e lingue arabiche (diverso da 0) o meno (0).
            /// </summary>
            SM_MIDEASTENABLED = 74,
            /// <summary>
            /// Indica se il mouse è installato (diverso da 0) o meno (0).
            /// </summary>
            SM_MOUSEPRESENT = 19,
            /// <summary>
            /// Indica se il mouse ha una rotellina di scorrimento orizzontale (diverso da 0) o meno (0).
            /// </summary>
            SM_MOUSEHORIZONTALWHEELPRESENT = 91,
            /// <summary>
            /// Indica se il mouse ha una rotellina di scorrimento verticale (diverso da 0) o meno (0).
            /// </summary>
            SM_MOUSEWHEELPRESENT = 75,
            /// <summary>
            /// Indica se esiste o meno una rete.
            /// </summary>
            /// <remarks>il bit meno significativo del valore restituito indica la presenza o meno di una rete, gli altri sono riservati per uso futuro.</remarks>
            SM_NETWORK = 63,
            /// <summary>
            /// Indica se l'estensione Microsoft Windows for Pen computing è installata (diverso da 0) o meno (0).
            /// </summary>
            SM_PENWINDOWS = 41,
            /// <summary>
            /// Indica se la sessione corrente è controllata da remoto (diverso da 0) o meno (0).
            /// </summary>
            SM_REMOTECONTROL = 8193,
            /// <summary>
            /// Indica se il processo chiamante è associato con una sessione client Terminal Services (diverso da 0) oppure se è associato a una sessione console Terminal Services (0).
            /// </summary>
            SM_REMOTESESSION = 4096,
            /// <summary>
            /// Indica se tutti i monitor hanno lo stesso formato dei colori (diverso da 0) o meno (0).
            /// </summary>
            SM_SAMEDISPLAYFORMAT = 81,
            /// <summary>
            /// Indica se il numero di build del sistema è quello di Windows Server 2003 R2 (diverso da 0) o meno (0).
            /// </summary>
            SM_SERVER2 = 89,
            /// <summary>
            /// Indica se un utente richiede che un'applicazione presenti informazioni visuali in situazione dove normalmente ci sarebbero informazioni in forma di suono (diverso da 0) o meno (0).
            /// </summary>
            SM_SHOWSOUNDS = 70,
            /// <summary>
            /// Indica se la sessione corrente è in corso di spegnimento (diverso da 0) o meno (0).
            /// </summary>
            SM_SHUTTINGDOWN = 8192,
            /// <summary>
            /// Indica se il computer ha un processore lento (diverso da 0) o meno (0).
            /// </summary>
            SM_SLOWMACHINE = 73,
            /// <summary>
            /// Indica se il sistema operativo attuale è Windows 7 Starter Edition, Windows Vista Starter oppure Windows XP Starter Edition (diverso da 0) o meno (0).
            /// </summary>
            SM_STARTER = 88,
            /// <summary>
            /// Indica se i significati dei pulsanti destro e sinistro del mouse sono invertiti (diverso da 0) o meno (0).
            /// </summary>
            SM_SWAPBUTTON = 23,
            /// <summary>
            /// Indica se il sistema è in modalità docking (diverso da 0) o meno (0).
            /// </summary>
            SM_SYSTEMDOCKED = 8196,
            /// <summary>
            /// Indica se il sistema operativo attuale è Windows XP Tablet PC Edition oppure Windows Vista, Windows 7 e il servizio Tablet PC Input Service è in funzione (diverso da 0) o meno (0).
            /// </summary>
            SM_TABLETPC = 86,
            /// <summary>
            /// Coordinate per la parte sinistra dello schermo virtuale.
            /// </summary>
            SM_XVIRTUALSCREEN = 76,
            /// <summary>
            /// Coordinate per la parte superiore dello schermo virtuale.
            /// </summary>
            SM_YVIRTUALSCREEN = 77
        }

        /// <summary>
        /// Modalità di disposizione delle finestre minimizzate.
        /// </summary>
        [Flags]
        internal enum MinimizedWindowsArrangeMode
        {
            /// <summary>
            /// Inizia dall'angolo inferiore sinistro dello schermo.
            /// </summary>
            /// <remarks>Questa posizione è quella predefinita.</remarks>
            ARW_BOTTOMLEFT = 0,
            /// <summary>
            /// Inizia dall'angolo inferiore destro dello schermo.
            /// </summary>
            ARW_BOTTOMRIGHT,
            /// <summary>
            /// Inizia dall'angolo superiore sinistro dello schermo.
            /// </summary>
            ARW_TOPLEFT,
            /// <summary>
            /// Inizia dall'angolo superiore destro dello schermo.
            /// </summary>
            ARW_TOPRIGHT,
            /// <summary>
            /// Inizia dall'angolo inferiore destro dello schermo.
            /// </summary>
            ARW_STARTRIGHT = 1,
            /// <summary>
            /// Inizia dall'angolo superiore sinistro dello schermo.
            /// </summary>
            ARW_STARTTOP = 2,

            ARW_STARTMASK = 
                ARW_STARTRIGHT |
                ARW_STARTTOP,
            /// <summary>
            /// Ordina orizzontalmente, da sinistra a destra.
            /// </summary>
            ARW_LEFT = 0,
            /// <summary>
            /// Ordina orizzontalmente, da destra a sinistra.
            /// </summary>
            ARW_RIGHT = 0,
            /// <summary>
            /// Ordina verticalmente, dal basso verso l'alto.
            /// </summary>
            ARW_UP = 4,
            /// <summary>
            /// Ordina verticalmente, dall'alto verso il basso.
            /// </summary>
            ARW_DOWN = 4,
            /// <summary>
            /// Nascondi le finestre minimizzate muovendole fuori dall'area visibile dello schermo.
            /// </summary>
            ARW_HIDE = 8
        }

        /// <summary>
        /// Tipo di input dei digitalizzatori.
        /// </summary>
        [Flags]
        internal enum DigitizerInputType
        {
            /// <summary>
            /// Digitalizzatore tocco integrato.
            /// </summary>
            NID_INTEGRATED_TOUCH = 1,
            /// <summary>
            /// Digitalizzatore tocco esterno.
            /// </summary>
            NID_EXTERNAL_TOUCH,
            /// <summary>
            /// Digitalizzatore penna integrato.
            /// </summary>
            NID_INTEGRATED_PEN = 4,
            /// <summary>
            /// Digitalizzatore penna esterno.
            /// </summary>
            NID_EXTERNAL_PEN = 8,
            /// <summary>
            /// Sorgenti multiple.
            /// </summary>
            NID_MULTI_INPUT = 64,
            /// <summary>
            /// Il dispositivo è pronto per ricevere input dal digitalizzatore.
            /// </summary>
            NID_READY = 128
        }
    }
}