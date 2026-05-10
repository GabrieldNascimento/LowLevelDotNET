using LowLevelDotNET.Features.Users.Interfaces;
namespace LowLevelDotNET.Features.Mediator
{
    public interface IMediator
    {
        Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request);
    }
}