using GitWrap.Application.Enums;

namespace GitWrap.Application.Interfaces;

public interface IGitHostingServiceRepository
{
    public GitHostingServiceType GitHostingServiceType { get; }
}