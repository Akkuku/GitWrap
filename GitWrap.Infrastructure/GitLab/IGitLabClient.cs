using System.Threading.Tasks;
using GitWrap.Application.Models;
using GitWrap.Infrastructure.GitLab.Issues;

namespace GitWrap.Infrastructure.GitLab;

internal interface IGitLabClient
{
    Task<GitLabIssue> CreateIssue(GitLabRepositoryIdentifier repositoryIdentifier, GitLabIssue issue);
    Task<GitLabIssue> UpdateIssue(GitLabRepositoryIdentifier repositoryIdentifier, int issueIid, UpdateIssueDefinition updateDefinition);
}