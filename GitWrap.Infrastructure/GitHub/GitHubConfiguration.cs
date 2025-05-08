namespace GitWrap.Infrastructure.GitHub;

public class GitHubConfiguration
{
    public required string BaseAddress { get; init; }
    public required string AccessToken { get; init; }
    public required string ApiVersion { get; init; }
}