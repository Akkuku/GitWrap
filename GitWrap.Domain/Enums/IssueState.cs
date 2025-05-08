using System.Text.Json.Serialization;

namespace GitWrap.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum IssueState
{
    Open,
    Closed
}