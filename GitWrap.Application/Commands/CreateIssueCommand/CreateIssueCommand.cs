using GitWrap.Application.Enums;
using GitWrap.Application.Interfaces;
using GitWrap.Domain.Models;

namespace GitWrap.Application.Commands.CreateIssueCommand;

public class CreateIssueCommand : IGitCommand<Issue>
{
    public required GitHostingServiceType GitHostingServiceType { get; init; }
    public required RepositoryIdentifier RepositoryIdentifier { get; init; }
    public required Issue Issue { get; init; }
}