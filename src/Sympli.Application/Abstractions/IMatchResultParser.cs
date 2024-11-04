using Sympli.Application.Domain;

namespace Sympli.Application.Abstractions;

public interface IMatchResultParser<T>
{
    T Parse(List<SearchResult> results);
}
