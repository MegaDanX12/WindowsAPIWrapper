namespace WindowsAPI.DesktopWindowManagerWrapper.Native
{
    /// <summary>
    /// Messaggi DWM.
    /// </summary>
    public static class DWMMessages
    {
        /// <summary>
        /// Inviato a una finestra per determinare quale parte di essa corrisponde a determinate coordinate schermo.
        /// </summary>
        /// <remarks>wParam: non usato<br/><br/>
        /// lParam: gli ultimi 16 sono la coordinata x del cursore, i primi 16 bit sono la coordinata y del cursore.<br/><br/>
        /// Le coordinate sono relative all'angolo superiore sinistro dello schermo.<br/><br/>
        /// Il valore restituito è uno di quelli presenti nell'enumerazione <see cref="DWMEnumerations.HitTestReturnValue"/>.</remarks>
        public const uint WM_NCHITTEST = 132;

        /// <summary>
        /// Postato quando l'utente fa doppio click con il tasto sinistro del mouse quando il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se una finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCLBUTTONDBLCLK = 163;

        /// <summary>
        /// Postato quando l'utente preme il tasto sinistro del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCLBUTTONDOWN = 161;

        /// <summary>
        /// Postato quando l'utente rilascia il tasto sinistro del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCLBUTTONUP = 162;

        /// <summary>
        /// Postato quando l'utente fa doppio click con il tasto centrale del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCMBUTTONDBLCLK = 169;

        /// <summary>
        /// Postato quando l'utente preme il tasto centrale del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCMBUTTONDOWN = 167;

        /// <summary>
        /// Postato quando l'utente rilascia il tasto centrale del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCMBUTTONUP = 168;

        /// <summary>
        /// Postato alla finestra quando il cursore passa sopra l'area non client di una finestra per un periodo di tempo precedentemente specificato.
        /// </summary>
        /// <remarks>wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCMOUSEHOVER = 672;

        /// <summary>
        /// Postato alla finestra quando il cursore lascia l'area non client di una finestra per un periodo di tempo precedentemente specificato.
        /// </summary>
        /// <remarks>wParam: non usato<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'applicazione elabora questo messaggi, dovrebbe restituire 0.</remarks>
        public const uint WM_NCMOUSELEAVE = 674;

        /// <summary>
        /// Postato a una finestra quando il cursore viene mosso mentre si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCMOUSEMOVE = 160;

        /// <summary>
        /// Postato quando l'utente fa doppio click con il tasto destro del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCRBUTTONDBLCLK = 166;

        /// <summary>
        /// Postato quando l'utente preme il tasto destro del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCRBUTTONDOWN = 164;

        /// <summary>
        /// Postato quando l'utente rilascia il tasto destro del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/><br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_NCRBUTTONUP = 165;

        /// <summary>
        /// Postato quando l'utente fa doppio click con il primo o il secondo tasto X del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: gli ultimi 16 bit specificano il valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/>, i primi 16 bit indicano se è stato premuto il primo pulsante X (1) o il secondo (2).<br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire TRUE.</remarks>
        public const uint WM_NCXBUTTONDBLCLK = 173;

        /// <summary>
        /// Postato quando l'utente preme il primo o il secondo tasto X del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: gli ultimi 16 bit specificano il valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/>, i primi 16 bit indicano se è stato premuto il primo pulsante X (1) o il secondo (2).<br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire TRUE.</remarks>
        public const uint WM_NCXBUTTONDOWN = 171;

        /// <summary>
        /// Postato quando l'utente rilascia il primo o il secondo tasto X del mouse mentre il cursore si trova nell'area non client di una finestra.
        /// </summary>
        /// <remarks>Questo messaggio viene postato alla finestra che contiene il cursore, se la finestra ha catturato il mouse questo messaggio non viene postato.<br/><br/>
        /// wParam: gli ultimi 16 bit specificano il valore hit test risultato dell'elaborazione del messaggio <see cref="WM_NCHITTEST"/>, i primi 16 bit indicano se è stato premuto il primo pulsante X (1) o il secondo (2).<br/>
        /// lParam: struttura <see cref="DWMStructures.POINTS"/> che contiene le coordinate x e y del cursore, esse sono relative all'angolo superiore sinistro dello schermo<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire TRUE.</remarks>
        public const uint WM_NCXBUTTONUP = 172;

        /// <summary>
        /// Informa tutte le finestre top-level che il colore della colorizzazione DWM è cambiato.
        /// </summary>
        /// <remarks>wParam: il nuovo colore (formato: 0xAARRGGBB)<br/>
        /// lParam: indica se il nuovo colore è opaco<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_DWMCOLORIZATIONCOLORCHANGED = 800;

        /// <summary>
        /// Inviato quando i criteri di rendering dell'area non client sono cambiati.
        /// </summary>
        /// <remarks>wParam: indica se il rendering dell'area non client è abilitato (TRUE) o meno (FALSE)<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_DWMNCRENDERINGCHANGED = 799;

        /// <summary>
        /// Comanda una finestra di fornire un bitmap statico da usare come live preview di essa.
        /// </summary>
        /// <remarks>wParam: non usato<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_DWMSENDICONICLIVEPREVIEWBITMAP = 806;

        /// <summary>
        /// Comanda una finestra di fornire un bitmap statico da usare come miniatura di essa.
        /// </summary>
        /// <remarks>wParam: non usato<br/>
        /// lParam: i primi 16 bit contengono la coordinata x massima della miniatura, gli ultimi 16 bit contengono la coordinata y massima della miniatura<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.<br/><br/>
        /// Se la miniatura ha dimensione che eccedono una o entrambe le coordinate x e y, essa non viene accettata da DWM.</remarks>
        public const uint WM_DWMSENDICONICTHUMBNAIL = 803;

        /// <summary>
        /// Inviato quando una finestra composta da DWM viene ingrandita.
        /// </summary>
        /// <remarks>wParam: TRUE per specificare che la finestra è stata ingrandita<br/>
        /// lParam: non usato<br/><br/>
        /// Se l'applicazione elabora questo messaggio, dovrebbe restituire 0.</remarks>
        public const uint WM_DWMWINDOWMAXIMIZEDCHANGE = 801;
    }
}