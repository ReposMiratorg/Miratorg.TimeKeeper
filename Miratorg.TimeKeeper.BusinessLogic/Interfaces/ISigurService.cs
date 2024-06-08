namespace Miratorg.TimeKeeper.BusinessLogic.Interfaces;

public interface ISigurService
{
    public Task<List<SigurEventModel>> GetSigurEventModelsAsync(DateTime begin, DateTime end, string codeNav);
}
