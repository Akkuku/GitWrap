using GitWrap.Application.Enums;
using GitWrap.Application.Interfaces;

namespace GitWrap.Application.Services;

public class GitHostingServiceRepositoryResolver<TGitRepository>(
    IEnumerable<TGitRepository> gitHostingServiceRepositories
) : IGitHostingServiceRepositoryResolver<TGitRepository>
    where TGitRepository : IGitHostingServiceRepository
{
    public TGitRepository Resolve(GitHostingServiceType gitHostingServiceType)
    {
        var gitHostingServiceRepository = gitHostingServiceRepositories
            .SingleOrDefault(repository => repository.GitHostingServiceType == gitHostingServiceType);

        return gitHostingServiceRepository
               ?? throw new NotSupportedException(
                   $"Git hosting service '{gitHostingServiceType}' is not currently supported");
    }
}