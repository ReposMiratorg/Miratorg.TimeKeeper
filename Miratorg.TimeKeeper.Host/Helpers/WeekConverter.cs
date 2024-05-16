using Miratorg.TimeKeeper.Host.Models;

namespace Miratorg.TimeKeeper.Host.Helpers;

public class WeekConverter
{
    public static DayEnum Convert(DateTime dateTime)
    {
        switch (dateTime.DayOfWeek)
        {
            case DayOfWeek.Sunday:
                return DayEnum.Sunday;
            case DayOfWeek.Monday:
                return DayEnum.Monday;
            case DayOfWeek.Tuesday:
                return DayEnum.Tuesday;
            case DayOfWeek.Wednesday:
                return DayEnum.Wednesday;
            case DayOfWeek.Thursday:
                return DayEnum.Thursday;
            case DayOfWeek.Friday:
                return DayEnum.Friday;
            case DayOfWeek.Saturday:
                return DayEnum.Saturday;
            default:
                break;
        }

        return DayEnum.Sunday;
    }
}
