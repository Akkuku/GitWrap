using GitWrap.Application.Enums;
using GitWrap.Application.Interfaces;
using GitWrap.Domain.Models;

namespace GitWrap.Application.Commands.CloseIssueCommand;

public class CloseIssueCommand : IGitCommand<Issue>
{
    public required GitHostingServiceType GitHostingServiceType { get; init; }
    public required RepositoryIdentifier RepositoryIdentifier { get; init; }
    public required int IssueId { get; init; }
}