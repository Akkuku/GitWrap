using GitWrap.Application.Enums;

namespace GitWrap.Application.Interfaces;

public interface IGitHostingServiceRepositoryResolver<out TGitRepository>
    where TGitRepository : IGitHostingServiceRepository 
{
    TGitRepository Resolve(GitHostingServiceType gitHostingServiceType);
}