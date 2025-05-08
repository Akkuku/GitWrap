using System;
using System.Text.Json.Serialization;
using GitWrap.Application.Models;
using GitWrap.Domain.Enums;

namespace GitWrap.Infrastructure.GitHub.Issues;

public class UpdateGitHubIssueRequest(UpdateIssueDefinition updateDefinition)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; } = updateDefinition.Name;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Body { get; } = updateDefinition.Description;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? State { get; } = updateDefinition.State switch
    {
        IssueState.Closed => "closed",
        IssueState.Open => "open",
        null => null,
        _ => throw new NotSupportedException()
    };
}