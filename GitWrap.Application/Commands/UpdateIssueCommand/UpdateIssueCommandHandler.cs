using GitWrap.Application.Interfaces;
using GitWrap.Domain.Models;
using MediatR;

namespace GitWrap.Application.Commands.UpdateIssueCommand;

internal class UpdateIssueCommandHandler(
    IGitHostingServiceRepositoryResolver<IGitIssuesRepository> gitHostingServiceRepositoryResolver
) : IRequestHandler<UpdateIssueCommand, Issue>
{
    public Task<Issue> Handle(UpdateIssueCommand request, CancellationToken cancellationToken)
    {
        var gitIssuesRepository = gitHostingServiceRepositoryResolver.Resolve(request.GitHostingServiceType);

        return gitIssuesRepository.UpdateIssue(request.RepositoryIdentifier, request.IssueId, request.UpdateIssueDefinition);
    }
}