using GitWrap.Application.Interfaces;
using GitWrap.Domain.Models;
using MediatR;

namespace GitWrap.Application.Commands.CreateIssueCommand;

internal class CreateIssueCommandHandler(
    IGitHostingServiceRepositoryResolver<IGitIssuesRepository> gitHostingServiceRepositoryResolver
) : IRequestHandler<CreateIssueCommand, Issue>
{
    public Task<Issue> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
    {
        var gitIssuesRepository = gitHostingServiceRepositoryResolver.Resolve(request.GitHostingServiceType);

        return gitIssuesRepository.CreateIssue(request.RepositoryIdentifier, request.Issue);
    }
}