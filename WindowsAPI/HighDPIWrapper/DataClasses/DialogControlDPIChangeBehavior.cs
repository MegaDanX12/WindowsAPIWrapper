using static WindowsAPI.HighDPIWrapper.Native.HighDPIEnumerations;

namespace WindowsAPI.HighDPIWrapper.DataClasses
{
    /// <summary>
    /// Comportamenti delle finestre figlie di un dialogo in risposta a un cambiamento dei DPI.
    /// </summary>
    public class DialogControlDPIChangeBehavior
    {
        /// <summary>
        /// Indica se il comportamento predefinito è abilitato.
        /// </summary>
        public bool IsDefaultBehaviourEnabled { get; }

        /// <summary>
        /// Indica se l'aggiornamento del font è abilitato.
        /// </summary>
        public bool IsFontUpdateEnabled { get; }

        /// <summary>
        /// Indica se il riposizionamento è abilitato.
        /// </summary>
        public bool IsRelayoutEnabled { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DialogControlDPIChangeBehavior"/>.
        /// </summary>
        /// <param name="Behaviors">Valore <see cref="DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS"/> che descrive il comportamento delle finestre figlie.</param>
        internal DialogControlDPIChangeBehavior(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Behaviors)
        {
            if (Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DEFAULT))
            {
                IsDefaultBehaviourEnabled = true;
                IsFontUpdateEnabled = true;
                IsRelayoutEnabled = true;
            }
            else
            {
                IsDefaultBehaviourEnabled = false;
                if (!Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_FONT_UPDATE) && !Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_RELAYOUT))
                {
                    IsFontUpdateEnabled = true;
                    IsRelayoutEnabled = true;
                }
                else if (Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_FONT_UPDATE) && Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_RELAYOUT))
                {
                    IsFontUpdateEnabled = false;
                    IsRelayoutEnabled = false;
                }
                else if (!Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_FONT_UPDATE) && Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_RELAYOUT))
                {
                    IsFontUpdateEnabled = true;
                    IsRelayoutEnabled = false;
                }
                else if (Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_FONT_UPDATE) && !Behaviors.HasFlag(DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_RELAYOUT))
                {
                    IsFontUpdateEnabled = false;
                    IsRelayoutEnabled = true;
                }
            }
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DialogControlDPIChangeBehavior"/>.
        /// </summary>
        /// <param name="isDefaultBehaviourEnabled">Indica se il comportamento predefinito è abilitato.</param>
        /// <param name="isFontUpdateEnabled">Indica se l'aggiornamento del font è abilitato.</param>
        /// <param name="isRelayoutEnabled">Indica se il riposizionamento è abilitato.</param>
        public DialogControlDPIChangeBehavior(bool isDefaultBehaviourEnabled, bool isFontUpdateEnabled, bool isRelayoutEnabled)
        {
            if (isDefaultBehaviourEnabled)
            {
                IsDefaultBehaviourEnabled = true;
                IsFontUpdateEnabled = true;
                IsRelayoutEnabled = true;
            }
            else
            {
                IsDefaultBehaviourEnabled = false;
                IsFontUpdateEnabled = isFontUpdateEnabled;
                IsRelayoutEnabled = isRelayoutEnabled;
            }
        }

        /// <summary>
        /// Costruisce il valore <see cref="DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS"/> con i dati di questa istanza.
        /// </summary>
        /// <returns>Il valore <see cref="DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS"/> che rappresenta i comportamenti descritti da questa istanza.</returns>
        internal DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS ToEnumValue()
        {
            DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS Value = 0;
            if (IsDefaultBehaviourEnabled)
            {
                Value |= DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DEFAULT;
            }
            if (!IsFontUpdateEnabled)
            {
                Value |= DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_FONT_UPDATE;
            }
            if (!IsRelayoutEnabled)
            {
                Value |= DIALOG_CONTROL_DPI_CHANGE_BEHAVIORS.DCDC_DISABLE_RELAYOUT;
            }
            return Value;
        }
    }
}