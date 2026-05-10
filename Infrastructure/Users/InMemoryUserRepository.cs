using LowLevelDotNET.Domain.Users;

namespace LowLevelDotNET.Infrastructure.Users;

public class InMemoryUserRepository : IUserRepository
{
    private readonly List<User> _users = new()
    {
        new User("Alice", "alice@email.com", new DateTime(1990, 1, 1)),
        new User("Bob", "bob@email.com", new DateTime(1995, 5, 10))
    };

    public User? GetById(Guid id)
    {
        return _users.FirstOrDefault(x => x.Id == id);
    }

    public User? GetByEmail(string email)
    {
        return _users.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public void Add(User user)
    {
        _users.Add(user);
    }   

    public void Update(User user)
    {
        var existingUser = GetById(user.Id);
        if (existingUser != null)
        {
            _users.Remove(existingUser);
            _users.Add(user);
        }
    }

    public void Delete(Guid id)
    {
        var user = GetById(id);
        if (user != null)
        {
            _users.Remove(user);
        }
    }   
}