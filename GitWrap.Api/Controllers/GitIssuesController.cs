using GitWrap.Api.Requests;
using GitWrap.Application.Commands.CloseIssueCommand;
using GitWrap.Application.Commands.CreateIssueCommand;
using GitWrap.Application.Commands.UpdateIssueCommand;
using GitWrap.Application.Models;
using GitWrap.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GitWrap.Api.Controllers;

[ApiController]
[Route("api/github/repos/{owner}/{repo}/issues")]
[Route("api/gitlab/projects/{projectId:int}/issues")]
public class GitIssuesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<Issue> CreateIssue([FromRoute] RepositoryIdentifierParams repositoryParams, [FromBody] Issue issue)
    {
        var (gitHostingServiceType, repositoryIdentifier) = repositoryParams.ResolveGitHostingService();

        var command = new CreateIssueCommand
        {
            GitHostingServiceType = gitHostingServiceType,
            RepositoryIdentifier = repositoryIdentifier,
            Issue = issue
        };

        return await mediator.Send(command);
    }

    [HttpPatch("{issueId:int}")]
    public async Task<Issue> UpdateIssue([FromRoute] RepositoryIdentifierParams repositoryParams, int issueId, [FromBody] UpdateIssueRequest request)
    {
        var (gitHostingServiceType, repositoryIdentifier) = repositoryParams.ResolveGitHostingService();

        var command = new UpdateIssueCommand
        {
            GitHostingServiceType = gitHostingServiceType,
            RepositoryIdentifier = repositoryIdentifier,
            IssueId = issueId,
            UpdateIssueDefinition = new UpdateIssueDefinition
            {
                Name = request.Name,
                Description = request.Description
            }
        };

        return await mediator.Send(command);
    }

    [HttpPatch("{issueId:int}/close")]
    public async Task<Issue> CloseIssue([FromRoute] RepositoryIdentifierParams repositoryParams, int issueId)
    {
        var (gitHostingServiceType, repositoryIdentifier) = repositoryParams.ResolveGitHostingService();

        var command = new CloseIssueCommand
        {
            GitHostingServiceType = gitHostingServiceType,
            RepositoryIdentifier = repositoryIdentifier,
            IssueId = issueId
        };

        return await mediator.Send(command);
    }
}