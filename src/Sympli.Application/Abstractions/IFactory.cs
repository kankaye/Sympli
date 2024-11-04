using Sympli.Application.Enums;

namespace Sympli.Application.Abstractions;

public interface ISearchEngineFactory
{
    public ISearchEngine GetEngine(SearchEngineType engineType, bool enableCache = false);
}
