using Sympli.Application.Enums;

namespace Sympli.Application.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class SearchEngineTypeAttribute(SearchEngineType type) : Attribute
{
    public SearchEngineType Type { get; } = type;
}
