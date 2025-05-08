using GitWrap.Domain.Enums;

namespace GitWrap.Domain.Models;

public class Issue
{
    public int InternalId { get; init; }
    public required string Name { get; init; }
    public IssueState State { get; init; } = IssueState.Open; 
    public string? Description { get; init; }
}