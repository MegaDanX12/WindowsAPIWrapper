namespace WindowsAPI.NationalLanguageSupportWrapper.DataClasses
{
    /// <summary>
    /// Informazioni su un subset Unicode.
    /// </summary>
    public class UnicodeSubset
    {
        /// <summary>
        /// Intervallo.
        /// </summary>
        public int[] Range { get; }

        /// <summary>
        /// Descrizione.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="UnicodeSubset"/>.
        /// </summary>
        /// <param name="Range">Intervallo.</param>
        /// <param name="Description">Descrizione.</param>
        public UnicodeSubset(int[] Range, string Description)
        {
            this.Range = Range;
            this.Description = Description;
        }
    }
}