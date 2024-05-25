namespace Miratorg.TimeKeeper.BusinessLogic.Models;

public class EmployeeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Position { get; set; }
    public string CodeNav { get; set; }
    public Guid? StoreId { get; set; }
    public Dictionary<DateTime, double> MountHours { get; set; } = new Dictionary<DateTime, double>();
    public List<PlanDetailModel> Plans { get; set; } = new List<PlanDetailModel>();
    public List<Schedule1CPlanModel> WorkDates { get; set; } = new List<Schedule1CPlanModel>();
    public List<ScudInfoModel> ScudInfos { get; set; } = new List<ScudInfoModel>();
    public List<AbsenceModel> Absences { get; set; } = new List<AbsenceModel>();
}

public class PlanDetailModel
{
    public Guid Id { get; set; }
    public DateTime Begin { get; set; }
    public DateTime End { get; set; }
    public PlanType PlanType { get; set; }
    public Guid? StoreId { get; set; }
    public string TypeOverWorkName { get; set; }
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

