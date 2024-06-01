using Miratorg.TimeKeeper.BusinessLogic.Models.api;

namespace Miratorg.TimeKeeper.BusinessLogic.Interfaces;

public interface IApiService
{
    Task<ResponseDto> GetBoimetry(RequestDto requestDto);
    Task<ResponseDto> GetFiscal(RequestDto dto);
    Task<ResponseDto> GetManual(RequestDto dto);
}
