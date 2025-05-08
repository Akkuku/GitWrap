using System;
using GitWrap.Domain.Enums;
using GitWrap.Domain.Models;

namespace GitWrap.Infrastructure.GitHub.Issues;

public class GitHubIssue
{
    public required int Number { get; init; }
    public required string Title { get; init; }
    public string? Body { get; init; }
    public required string State { get; init; }

    public static GitHubIssue FromIssue(Issue issue)
        => new()
        {
            Number = issue.InternalId,
            Title = issue.Name,
            Body = issue.Description,
            State = issue.State switch
            {
                IssueState.Open => "open",
                IssueState.Closed => "closed",
                _ => throw new NotSupportedException()
            }
        };

    public Issue ToIssue()
        => new()
        {
            InternalId = Number,
            Name = Title,
            Description = Body,
            State = State switch
            {
                "open" => IssueState.Open,
                "closed" => IssueState.Closed,
                _ => throw new NotSupportedException()
            }
        };
}