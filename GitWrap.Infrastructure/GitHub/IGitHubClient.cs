using System.Threading.Tasks;
using GitWrap.Application.Models;
using GitWrap.Infrastructure.GitHub.Issues;

namespace GitWrap.Infrastructure.GitHub;

internal interface IGitHubClient
{
    Task<GitHubIssue> CreateIssue(GitHubRepositoryIdentifier repositoryIdentifier, GitHubIssue gitHubIssue);
    Task<GitHubIssue> UpdateIssue(GitHubRepositoryIdentifier repositoryIdentifier, int issueNumber, UpdateIssueDefinition updateResuest);
}