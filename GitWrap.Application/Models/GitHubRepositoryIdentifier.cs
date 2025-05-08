using GitWrap.Domain.Models;

namespace GitWrap.Application.Models;

public class GitHubRepositoryIdentifier(string owner, string repo): RepositoryIdentifier
{
    public string Owner { get; } = owner;
    public string Repo { get; } = repo;
}