using LowLevelDotNET.Infrastructure.DI;
using LowLevelDotNET.Features.Users.Interfaces;
using System.Reflection;
namespace LowLevelDotNET.Features.Mediator
{
    public class Mediator : IMediator
{
    private readonly Container _container;

    public Mediator(Container container)
    {
        _container = container;
    }

    public async Task<TResponse> Send<TResponse>(
        IRequest<TResponse> request)
    {
        var requestType = request.GetType();

        var handlerType = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(type =>
                type.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IHandler<,>) &&
                    i.GenericTypeArguments[0] == requestType
                ));

        if (handlerType == null)
            throw new Exception("Handler not found");

        var handler = _container.Resolve(handlerType);

        var method = handlerType.GetMethod("Handle");

        var result = await (Task<TResponse>)
            method.Invoke(handler, new object[] { request });

        return result;
    }
}    
}
