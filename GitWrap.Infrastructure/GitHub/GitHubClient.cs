using System.Net.Http;
using System.Threading.Tasks;
using GitWrap.Application.Models;
using GitWrap.Infrastructure.Constants;
using GitWrap.Infrastructure.Extensions;
using GitWrap.Infrastructure.GitHub.Issues;

namespace GitWrap.Infrastructure.GitHub;

internal class GitHubClient(IHttpClientFactory httpClientFactory) : IGitHubClient
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(HttpClientName.GitHub);

    public Task<GitHubIssue> CreateIssue(GitHubRepositoryIdentifier repositoryIdentifier, GitHubIssue issue)
    {
        var endpoint = GitHubEndpoints.Issues(repositoryIdentifier);

        return httpClient.PostJsonAsync<GitHubIssue, GitHubIssue>(endpoint, issue);
    }

    public Task<GitHubIssue> UpdateIssue(GitHubRepositoryIdentifier repositoryIdentifier, int issueNumber, UpdateIssueDefinition updateDefinition)
    {
        var endpoint = GitHubEndpoints.Issue(repositoryIdentifier, issueNumber);
        var updateRequest = new UpdateGitHubIssueRequest(updateDefinition);

        return httpClient.PatchJsonAsync<GitHubIssue, UpdateGitHubIssueRequest>(endpoint, updateRequest);
    }
}

file static class GitHubEndpoints
{
    public static string Issues(GitHubRepositoryIdentifier repositoryIdentifier)
        => $"repos/{repositoryIdentifier.Owner}/{repositoryIdentifier.Repo}/issues";

    public static string Issue(GitHubRepositoryIdentifier repositoryIdentifier, int issueNumber)
        => $"{Issues(repositoryIdentifier)}/{issueNumber}";
}