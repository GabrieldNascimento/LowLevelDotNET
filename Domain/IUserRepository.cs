namespace LowLevelDotNET.Domain.Users;

public interface IUserRepository
{
    User? GetById(Guid id);
}