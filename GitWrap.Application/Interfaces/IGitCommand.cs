using GitWrap.Application.Enums;
using MediatR;

namespace GitWrap.Application.Interfaces;

internal interface IGitCommand : IRequest
{
    public GitHostingServiceType GitHostingServiceType { get; init; }
}

internal interface IGitCommand<out TResult> : IRequest<TResult>
{
    public GitHostingServiceType GitHostingServiceType { get; init; }
}

