namespace LowLevelDotNET.Features.Users
{
    public interface IValidator<TRequest>
    {
        bool Validate(TRequest request, out string error);
    }   
}