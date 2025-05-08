using GitWrap.Application.Interfaces;
using GitWrap.Domain.Models;
using MediatR;

namespace GitWrap.Application.Commands.CloseIssueCommand;

internal class CloseIssueCommandHandler(
    IGitHostingServiceRepositoryResolver<IGitIssuesRepository> gitHostingServiceRepositoryResolver
) : IRequestHandler<CloseIssueCommand, Issue>
{
    public Task<Issue> Handle(CloseIssueCommand request, CancellationToken cancellationToken)
    {
        var gitIssuesRepository = gitHostingServiceRepositoryResolver.Resolve(request.GitHostingServiceType);

        return gitIssuesRepository.CloseIssue(request.RepositoryIdentifier, request.IssueId);
    }
}