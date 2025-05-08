using System;
using System.Net.Http;
using System.Threading.Tasks;
using GitWrap.Application.Models;
using GitWrap.Domain.Enums;
using GitWrap.Infrastructure.Constants;
using GitWrap.Infrastructure.Extensions;
using GitWrap.Infrastructure.GitLab.Issues;
using GitWrap.Infrastructure.Utils;

namespace GitWrap.Infrastructure.GitLab;

internal class GitLabClient(IHttpClientFactory httpClientFactory) : IGitLabClient
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(HttpClientName.GitLab);

    public async Task<GitLabIssue> CreateIssue(GitLabRepositoryIdentifier repositoryIdentifier, GitLabIssue issue)
    {
        var endpoint = GitLabEndpoints.Issues(repositoryIdentifier) + issue.ToQueryString();

        return await httpClient.PostJsonAsync<GitLabIssue>(endpoint);
    }

    public async Task<GitLabIssue> UpdateIssue(GitLabRepositoryIdentifier repositoryIdentifier, int issueIid, UpdateIssueDefinition updateDefinition)
    {
        var endpoint = GitLabEndpoints.Issue(repositoryIdentifier, issueIid) + CreateIssueUpdateQueryString(updateDefinition);

        return await httpClient.PutJsonAsync<GitLabIssue>(endpoint);
    }

    private static string CreateIssueUpdateQueryString(UpdateIssueDefinition updateDefinition)
        => new QueryStringBuilder()
            .AddParamIfNotNull("title", updateDefinition.Name)
            .AddParamIfNotNull("description", updateDefinition.Description)
            .AddParamIfNotNull("state_event", updateDefinition.State switch
            {
                IssueState.Open => "reopen",
                IssueState.Closed => "close",
                null => null,
                _ => throw new NotSupportedException()
            })
            .Build();
}

file static class GitLabEndpoints
{
    public static string Issues(GitLabRepositoryIdentifier repositoryIdentifier)
        => $"api/v4/projects/{repositoryIdentifier.ProjectId}/issues";

    public static string Issue(GitLabRepositoryIdentifier repositoryIdentifier, int issueIid)
        => $"{Issues(repositoryIdentifier)}/{issueIid}";
}