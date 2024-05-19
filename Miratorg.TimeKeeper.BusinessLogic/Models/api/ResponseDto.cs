namespace Miratorg.TimeKeeper.BusinessLogic.Models.api;

public class ResponseDto
{
    public Timesheet[] timesheets { get; set; }
}

public class Timesheet
{
    public string employeeId { get; set; }
    public Dep dep { get; set; }
    public string date { get; set; }
    public int dvalue { get; set; }
    public int nvalue { get; set; }
    public int dovertime { get; set; }
    public int novertime { get; set; }
    public string worktype { get; set; }
    public Worktime[] worktime { get; set; }
}

public class Worktime
{
    public string type { get; set; }
    public int dvalue { get; set; }
    public int nvalue { get; set; }
}
