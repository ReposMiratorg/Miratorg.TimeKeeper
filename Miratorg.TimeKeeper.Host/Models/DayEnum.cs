using System.ComponentModel.DataAnnotations;

namespace Miratorg.TimeKeeper.Host.Models;

public enum DayEnum
{
    [Display(Name = "Пн")]
    Monday = 1,
    
    [Display(Name = "Вт")]
    Tuesday = 2,
    
    [Display(Name = "Ср")]
    Wednesday = 3,

    [Display(Name = "Чт")]
    Thursday = 4,

    [Display(Name = "Пт")]
    Friday = 5,

    [Display(Name = "Сб")]
    Saturday = 6,

    [Display(Name = "Вс")]
    Sunday = 7
}
