using GitWrap.Domain.Models;

namespace GitWrap.Application.Models;

public class GitLabRepositoryIdentifier(int projectId): RepositoryIdentifier
{
    public int ProjectId { get; } = projectId;
}