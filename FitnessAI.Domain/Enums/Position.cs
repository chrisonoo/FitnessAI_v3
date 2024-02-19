using System.ComponentModel.DataAnnotations;

namespace FitnessAI.Domain.Enums;

public enum Position
{
    [Display(Name = "Trener personalny")]
    Trainer,

    [Display(Name = "Recepcjonista/ka")]
    Receptionits,

    [Display(Name = "Asystent/ka")]
    Assistance
}