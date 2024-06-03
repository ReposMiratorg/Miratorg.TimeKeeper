using Miratorg.TimeKeeper.BusinessLogic.Models.api;

namespace Miratorg.TimeKeeper.BusinessLogic.Interfaces;

public interface IApiService
{
    Task<ResponseDto> GetFacts(RequestDto requestDto);
    Task<ResponseDto> GetPlans(RequestDto dto);
    Task<ResponseDto> GetManual(RequestDto dto);
}
