using System;
using GitWrap.Domain.Enums;
using GitWrap.Domain.Models;
using GitWrap.Infrastructure.Utils;

namespace GitWrap.Infrastructure.GitLab.Issues;

public class GitLabIssue
{
    public required int Iid { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required string State { get; init; }

    public string ToQueryString()
        => new QueryStringBuilder()
            .AddParam("title", Title)
            .AddParamIfNotNull("description", Description)
            .Build();

    public static GitLabIssue FromIssue(Issue issue)
        => new()
        {
            Iid = issue.InternalId,
            Title = issue.Name,
            Description = issue.Description,
            State = issue.State switch
            {
                IssueState.Open => "opened",
                IssueState.Closed => "closed",
                _ => throw new NotSupportedException()
            }
        };

    public Issue ToIssue()
        => new()
        {
            InternalId = Iid,
            Name = Title,
            Description = Description,
            State = State switch
            {
                "opened" => IssueState.Open,
                "closed" => IssueState.Closed,
                _ => throw new NotSupportedException()
            }
        };
}