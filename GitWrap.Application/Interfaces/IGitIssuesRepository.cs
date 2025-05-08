using GitWrap.Application.Models;
using GitWrap.Domain.Models;

namespace GitWrap.Application.Interfaces;

public interface IGitIssuesRepository : IGitHostingServiceRepository
{
    Task<Issue> CreateIssue(RepositoryIdentifier repositoryIdentifier, Issue issue);
    Task<Issue> UpdateIssue(RepositoryIdentifier repositoryIdentifier, int issueId, UpdateIssueDefinition updateDefinition);
    Task<Issue> CloseIssue(RepositoryIdentifier repositoryIdentifier, int issueId);
}