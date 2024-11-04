using Sympli.Application.Enums;

namespace Sympli.Application.Abstractions;

public interface IUrlLookupUseCase
{
    Task<string> HandleAsync(string keywords, 
        string url,
        SearchEngineType engineType, 
        bool enableCache = true,
        CancellationToken cancellationToken = default);
}
