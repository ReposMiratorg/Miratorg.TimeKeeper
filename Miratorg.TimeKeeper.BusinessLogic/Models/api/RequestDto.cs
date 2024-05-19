namespace Miratorg.TimeKeeper.BusinessLogic.Models.api;

public class RequestDto
{
    public Gettimesheets getTimesheets { get; set; }
}

public class Gettimesheets
{
    public Authdata authData { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public Dep dep { get; set; }
    public string source { get; set; }
}

public class Authdata
{
    public string login { get; set; }
    public string password { get; set; }
}
