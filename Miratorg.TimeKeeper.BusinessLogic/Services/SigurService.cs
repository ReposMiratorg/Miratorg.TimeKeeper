namespace Miratorg.TimeKeeper.BusinessLogic.Services;

public class SigurService : ISigurService
{
    private readonly IStaffControlDbContextFactory _staffControlDbContextFactory;
    private readonly ILogger _logger;

    public SigurService(IStaffControlDbContextFactory staffControlDbContextFactory, ILogger<SigurService> logger)
    {
        _staffControlDbContextFactory = staffControlDbContextFactory;
        _logger = logger;
    }

    public async Task<List<SigurEventModel>> GetSigurEventModelsAsync(DateTime begin, DateTime end, string codeNav)
    {
        using var sigurDbContext = new SigurDbContext();
        using var stuffDbContext = await _staffControlDbContextFactory.Create();

        var scudStaffEntity = stuffDbContext.SkudStaffs.First(x => x.Code == codeNav && x.CodeDataCenter == "mhb-sql");
        int sigurUserId = scudStaffEntity != null ? (int) scudStaffEntity.Id : 0;

        var eventTimes = sigurDbContext.Logs.Where(x => x.Emphint == sigurUserId).OrderBy(x => x.Logtime).Select(X => X.Logtime).ToList();

        List<SigurEventModel> models = new List<SigurEventModel>();

        foreach (var time in eventTimes)
        {
            if(time != null)
            {
                var model = Convert((DateTime)time, codeNav);
                models.Add(model);
            }
        }

        return models;
    }

    private SigurEventModel Convert(DateTime time, string codeName)
    {
        SigurEventModel model = new SigurEventModel()
        {
            CodeNav = codeName,
            EventTime = time
        };

        return model;
    }
}
