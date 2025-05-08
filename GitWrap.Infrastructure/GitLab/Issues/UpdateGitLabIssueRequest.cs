using System;
using System.Text.Json.Serialization;
using GitWrap.Application.Models;
using GitWrap.Domain.Enums;

namespace GitWrap.Infrastructure.GitLab.Issues;

public class UpdateGitLabIssueRequest(UpdateIssueDefinition updateDefinition)
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; } = updateDefinition.Name;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; } = updateDefinition.Description;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? StateEvent { get; } = updateDefinition.State switch
    {
        IssueState.Closed => "close",
        IssueState.Open => "reopen",
        null => null,
        _ => throw new NotSupportedException()
    };
}