using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WindowsAPI.HighDPIWrapper.Native.HighDPIEnumerations;

namespace WindowsAPI.HighDPIWrapper.DataClasses
{
    /// <summary>
    /// Comportamenti adottati dalle finestre di dialogo in risposta al cambiamento dei DPI.
    /// </summary>
    public class DialogDPIChangeBehavior
    {
        /// <summary>
        /// Indica se il comportamento predefinito è attivato.
        /// </summary>
        public bool IsDefaultBehaviourEnabled { get; }

        /// <summary>
        /// Indica se il cambiamento dei DPI non comporta alcuna reazione.
        /// </summary>
        public bool AllBehaviorsDisabled { get; }

        /// <summary>
        /// Indica se il ridimensionamento del dialogo è attivato.
        /// </summary>
        public bool IsDialogResizeEnabled { get; }

        /// <summary>
        /// Indica se la ridistribuzione dei controlli è attivata.
        /// </summary>
        public bool IsControlRelayoutEnabled { get; }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DialogDPIChangeBehavior"/>.
        /// </summary>
        /// <param name="Behaviors">Valore composito dell'enumerazione</param>
        internal DialogDPIChangeBehavior(DIALOG_DPI_CHANGE_BEHAVIORS Behaviors)
        {
            if (Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DEFAULT))
            {
                IsDefaultBehaviourEnabled = true;
                AllBehaviorsDisabled = false;
                IsDialogResizeEnabled = true;
                IsControlRelayoutEnabled = true;
            }
            else
            {
                if (Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_ALL))
                {
                    IsDefaultBehaviourEnabled = false;
                    AllBehaviorsDisabled = true;
                    IsDialogResizeEnabled = false;
                    IsControlRelayoutEnabled = false;
                }
                else
                {
                    if (Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_RESIZE) && !Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_CONTROL_RELAYOUT))
                    {
                        IsDefaultBehaviourEnabled = false;
                        AllBehaviorsDisabled = false;
                        IsDialogResizeEnabled = false;
                        IsControlRelayoutEnabled = true;
                    }
                    else if (Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_RESIZE) && Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_CONTROL_RELAYOUT))
                    {
                        IsDefaultBehaviourEnabled = false;
                        AllBehaviorsDisabled = false;
                        IsDialogResizeEnabled = false;
                        IsControlRelayoutEnabled = false;
                    }
                    else if (!Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_RESIZE) && !Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_CONTROL_RELAYOUT))
                    {
                        IsDefaultBehaviourEnabled = false;
                        AllBehaviorsDisabled = false;
                        IsDialogResizeEnabled = true;
                        IsControlRelayoutEnabled = true;
                    }
                    else if (!Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_RESIZE) && Behaviors.HasFlag(DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_CONTROL_RELAYOUT))
                    {
                        IsDefaultBehaviourEnabled = false;
                        AllBehaviorsDisabled = false;
                        IsDialogResizeEnabled = true;
                        IsControlRelayoutEnabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Inizializza una nuova istanza di <see cref="DialogDPIChangeBehavior"/>.
        /// </summary>
        /// <param name="isDefaultBehaviourEnabled">Indica se il comportamento predefinito è attivato.</param>
        /// <param name="allBehaviorsDisabled">Indica se il cambiamento dei DPI non comporta alcuna reazione.</param>
        /// <param name="isDialogResizeEnabled">Indica se il ridimensionamento del dialogo è attivato.</param>
        /// <param name="isControlRelayoutEnabled">Indica se la ridistribuzione dei controlli è attivata.</param>
        public DialogDPIChangeBehavior(bool isDefaultBehaviourEnabled, bool allBehaviorsDisabled, bool isDialogResizeEnabled, bool isControlRelayoutEnabled)
        {
            if (isDefaultBehaviourEnabled)
            {
                IsDefaultBehaviourEnabled = true;
                AllBehaviorsDisabled = false;
                IsDialogResizeEnabled = true;
                IsControlRelayoutEnabled = true;
            }
            else
            {
                if (allBehaviorsDisabled) 
                {
                    IsDefaultBehaviourEnabled = false;
                    AllBehaviorsDisabled = true;
                    IsDialogResizeEnabled = false;
                    IsControlRelayoutEnabled = false;
                }
                else
                {
                    IsDefaultBehaviourEnabled = false;
                    AllBehaviorsDisabled = false;
                    IsDialogResizeEnabled = isDialogResizeEnabled;
                    IsControlRelayoutEnabled = isControlRelayoutEnabled;
                }
            }
        }

        /// <summary>
        /// Costruisce il valore enumerativo <see cref="DIALOG_DPI_CHANGE_BEHAVIORS"/> corrispondente ai dati di questa istanza.
        /// </summary>
        /// <returns>Il valore <see cref="DIALOG_DPI_CHANGE_BEHAVIORS"/> che rappresenta i dati di questa istanza.</returns>
        internal DIALOG_DPI_CHANGE_BEHAVIORS ToEnumValue()
        {
            DIALOG_DPI_CHANGE_BEHAVIORS Value = 0;
            if (IsDefaultBehaviourEnabled)
            {
                Value |= DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DEFAULT;
            }
            if (AllBehaviorsDisabled)
            {
                Value |= DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_ALL;
            }
            if (!IsDialogResizeEnabled)
            {
                Value |= DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_RESIZE;
            }
            if (!IsControlRelayoutEnabled)
            {
                Value |= DIALOG_DPI_CHANGE_BEHAVIORS.DDC_DISABLE_CONTROL_RELAYOUT;
            }
            return Value;
        }
    }
}