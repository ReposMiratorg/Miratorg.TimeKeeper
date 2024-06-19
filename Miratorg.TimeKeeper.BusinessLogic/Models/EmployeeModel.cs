namespace Miratorg.TimeKeeper.BusinessLogic.Models;

public class EmployeeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string CodeNav { get; set; }
    public Guid? StoreId { get; set; }

    /// <summary>
    /// ОБЩИЙ (план + подработка) за месяц
    /// </summary>
    public Dictionary<DateTime, double> MountPlanUseMinuts { get; set; } = new Dictionary<DateTime, double>();

    /// <summary>
    /// Общий план (план + подработка) за день
    /// </summary>
    public Dictionary<DateTime, double> DayPlanUseMinutes { get; set; } = new Dictionary<DateTime, double>();
    
    /// <summary>
    /// Данные по СКУД за месяц
    /// </summary>
    public Dictionary<DateTime, double> MountScudUseMinutes { get; set; } = new Dictionary<DateTime, double>();
    
    /// <summary>
    /// Данные за день СКУД
    /// </summary>
    public Dictionary<DateTime, double> DayScudUseMinutes { get; set; } = new Dictionary<DateTime, double>();

    /// <summary>
    /// Все планы (план + подработка) - как в БД
    /// </summary>
    public List<PlanDetailModel> Plans { get; set; } = new List<PlanDetailModel>();
    public List<Schedule1CPlanModel> WorkDates { get; set; } = new List<Schedule1CPlanModel>();

    /// <summary>
    /// Данные СКУД - зафиксированные как вход/выход
    /// </summary>
    public List<ScudInfoModel> ScudInfos { get; set; } = new List<ScudInfoModel>();

    /// <summary>
    /// Причины отсуствия по 1С
    /// </summary>
    public List<AbsenceModel> Absences { get; set; } = new List<AbsenceModel>();

    /// <summary>
    /// Экспорт в 1С план
    /// </summary>
    public List<ExportTime> ExportPlanTimes { get; set; } = new List<ExportTime>();

    /// <summary>
    /// Экспорт в 1С факт (по СКУД)
    /// </summary>
    public List<ExportTime> ExportFactTimes { get; set; } = new List<ExportTime>();
}

public class ExportTime
{
    public DateOnly Date { get; set; }

    public DateTime Begin { get; set; }
    public DateTime End { get; set; }

    public string WorkTime { get; set; }
    public int DayMinutes { get; set; }
    public int NightMinutes { get; set; }
}

public class ExportTimeFact : ExportTime
{

}

public class PlanDetailModel
{
    public Guid Id { get; set; }
    public DateTime OriginalBegin { get; set; }
    public DateTime OriginalEnd { get; set; }
    public DateTime PlanCalcBegin { get; set; }
    public DateTime PlanCalcEnd { get; set; }
    public DateTime FactCalcBegin { get; set; }
    public DateTime FactCalcEnd { get; set; }
    public PlanType PlanType { get; set; }
    public Guid? StoreId { get; set; }
    public string WorkTime { get; set; }

    /// <summary>
    /// Расчитанное время для работы
    /// </summary>
    public int CaclWorkTimeMinutes { get; set; }
    public int CalcWorkDayMinutes { get; set; }
    public int CalcWorkNightMinutes { get; set; }

    /// <summary>
    /// Рачетное время обеда
    /// </summary>
    public int PlanObedTimeMinutes { get; set; }
    public int FactObedTimeMinutes { get; set; }
}

public class Schedule1CPlanModel
{
    public DateTime Begin { get; set; }
    public DateTime? End { get; set; }
}

public class ScudInfoModel
{
    public Guid Id { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public ScudInfoType ScudInfoType { get; set; }
}

public enum ScudInfoType
{
    /// <summary>
    /// Данные получены из СКУДа
    /// </summary>
    Scud = 1,

    /// <summary>
    /// Данные введены в ручную
    /// </summary>
    Manual = 2
}

/// <summary>
/// Причина отсуствия
/// </summary>
public class AbsenceModel
{
    public DateTime RepDate { get; internal set; }
    public string Description { get; internal set; }
}

