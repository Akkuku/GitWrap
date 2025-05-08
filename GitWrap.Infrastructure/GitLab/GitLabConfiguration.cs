namespace GitWrap.Infrastructure.GitLab;

public class GitLabConfiguration
{
    public required string BaseAddress { get; init; }
    public required string AccessToken { get; init; }
}