using GitWrap.Domain.Enums;

namespace GitWrap.Application.Models;

public class UpdateIssueDefinition
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public IssueState? State { get; init; }
}