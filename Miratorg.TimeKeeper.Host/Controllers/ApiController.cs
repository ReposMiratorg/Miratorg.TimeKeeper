using Microsoft.AspNetCore.Mvc;
using Miratorg.Common;
using Miratorg.TimeKeeper.BusinessLogic.Services;

namespace Miratorg.TimeKeeper.Host.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ApiController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IApiService _apiService;
    private readonly ILogger<ApiController> _logger;

    public ApiController(IApiService apiService, ILogger<ApiController> logger)
    {
        Guard.NotNull(apiService, nameof(apiService));
        Guard.NotNull(logger, nameof(logger));

        _apiService = apiService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<BusinessLogic.Models.api.ResponseDto> Rest(BusinessLogic.Models.api.RequestDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        if (dto.getTimesheets?.authData?.login != "miratorg1c@verme.ru")
        {
            HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return null;
        }

        if (dto.getTimesheets?.authData?.password != "EvHR9MQXpb5R")
        {
            HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return null;
        }

        switch (dto.getTimesheets.source)
        {
            case "biometry":
                //var responseBoimetry = await _apiService.GetBoimetry(dto);
                var responseBoimetry = await _apiService.GetBoimetry(dto);
                return responseBoimetry;

            case "fiscal":
                var responseFiscal = await _apiService.GetFiscal(dto);
                return responseFiscal;

            case "manual":
                //var responseManual = await _apiService.GetManual(dto);
                var responseManual = await _apiService.GetFiscal(dto);
                return responseManual;

            default:
                throw new Exception("incorrect source");
        }
    }
}