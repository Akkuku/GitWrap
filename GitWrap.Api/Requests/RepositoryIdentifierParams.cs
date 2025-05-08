using GitWrap.Application.Enums;
using GitWrap.Application.Models;
using GitWrap.Domain.Exceptions;
using GitWrap.Domain.Models;

namespace GitWrap.Api.Requests;

public class RepositoryIdentifierParams
{
    public string? Owner { get; init; }
    public string? Repo { get; init; }
    public int? ProjectId { get; init; }

    public (GitHostingServiceType, RepositoryIdentifier) ResolveGitHostingService()
    {
        if (ProjectId.HasValue)
            return (GitHostingServiceType.GitLab, new GitLabRepositoryIdentifier(ProjectId.Value));

        if (Owner is not null && Repo is not null)
            return (GitHostingServiceType.GitHub, new GitHubRepositoryIdentifier(Owner, Repo));

        throw new HttpResponseException("Not all parameters required for repository identification are not provided");
    }
}