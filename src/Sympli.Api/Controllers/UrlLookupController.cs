using Microsoft.AspNetCore.Mvc;
using Sympli.Application.Abstractions;
using Sympli.Application.Enums;

namespace Sympli.Api.Controllers;

[ApiController]
[Route("{searchEngineType}/[controller]")]
public class UrlLookupController(IServiceProvider serviceProvider) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetResult(
     [FromRoute] SearchEngineType searchEngineType,
     [FromQuery] string keywords = "e-settlements",
     [FromQuery] string url = "https://www.sympli.com.au",
     [FromQuery] bool enableCache = true,
     CancellationToken cancellationToken = default)
    {
        var usecase = serviceProvider.GetRequiredService<IUrlLookupUseCase>();
        var result = await usecase.HandleAsync(keywords, url, searchEngineType, enableCache, cancellationToken);
        return Ok(result);
    }

}
