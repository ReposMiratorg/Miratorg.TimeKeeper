using System.ComponentModel.DataAnnotations;

namespace Miratorg.TimeKeeper.Host.Models;

public enum DayEnum
{
    [Display(Name = "Пн")]
    Monday = 0,
    
    [Display(Name = "Вт")]
    Tuesday = 1,
    
    [Display(Name = "Ср")]
    Wednesday = 2,

    [Display(Name = "Чт")]
    Thursday = 3,

    [Display(Name = "Пт")]
    Friday = 4,

    [Display(Name = "Сб")]
    Saturday = 5,

    [Display(Name = "Вс")]
    Sunday = 6
}
