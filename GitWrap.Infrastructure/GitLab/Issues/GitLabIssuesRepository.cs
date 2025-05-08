using System;
using System.Threading.Tasks;
using GitWrap.Application.Enums;
using GitWrap.Application.Interfaces;
using GitWrap.Application.Models;
using GitWrap.Domain.Enums;
using GitWrap.Domain.Models;

namespace GitWrap.Infrastructure.GitLab.Issues;

internal class GitLabIssuesRepository(IGitLabClient gitLabClient) : IGitIssuesRepository
{
    public GitHostingServiceType GitHostingServiceType => GitHostingServiceType.GitLab;

    public async Task<Issue> CreateIssue(RepositoryIdentifier repositoryIdentifier, Issue issue)
    {
        var identifier = ValidateRepositoryIdentifier(repositoryIdentifier);
        var createdIssue = await gitLabClient.CreateIssue(identifier, GitLabIssue.FromIssue(issue));

        return createdIssue.ToIssue();
    }

    public async Task<Issue> UpdateIssue(RepositoryIdentifier repositoryIdentifier, int issueId, UpdateIssueDefinition updateDefinition)
    {
        var identifier = ValidateRepositoryIdentifier(repositoryIdentifier);
        var updatedIssue = await gitLabClient.UpdateIssue(identifier, issueId, updateDefinition);

        return updatedIssue.ToIssue();
    }

    public async Task<Issue> CloseIssue(RepositoryIdentifier repositoryIdentifier, int issueId)
    {
        var identifier = ValidateRepositoryIdentifier(repositoryIdentifier);
        var updateDefinition = new UpdateIssueDefinition
        {
            State = IssueState.Closed
        };

        var updatedIssue = await gitLabClient.UpdateIssue(identifier, issueId, updateDefinition);

        return updatedIssue.ToIssue();
    }
    
    private static GitLabRepositoryIdentifier ValidateRepositoryIdentifier(RepositoryIdentifier repositoryIdentifier)
    {
        if (repositoryIdentifier is not GitLabRepositoryIdentifier identifier)
            throw new ArgumentException("Incorrect repository identifier");

        return identifier;
    }
}