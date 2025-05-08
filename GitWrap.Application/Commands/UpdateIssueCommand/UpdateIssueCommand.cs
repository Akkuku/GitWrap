using GitWrap.Application.Enums;
using GitWrap.Application.Interfaces;
using GitWrap.Application.Models;
using GitWrap.Domain.Models;

namespace GitWrap.Application.Commands.UpdateIssueCommand;

public class UpdateIssueCommand : IGitCommand<Issue>
{
    public required GitHostingServiceType GitHostingServiceType { get; init; }
    public required RepositoryIdentifier RepositoryIdentifier { get; init; }
    public required int IssueId { get; init; }
    public required UpdateIssueDefinition UpdateIssueDefinition { get; init; }
}