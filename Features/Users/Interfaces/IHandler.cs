namespace LowLevelDotNET.Features.Users.Interfaces
{
    public interface IHandler<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request);
    }
}