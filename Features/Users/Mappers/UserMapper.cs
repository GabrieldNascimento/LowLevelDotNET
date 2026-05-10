using LowLevelDotNET.Domain.Users;
using LowLevelDotNET.Features.Users.Responses;

namespace LowLevelDotNET.Features.Users.Mappers;
public class UserMapper : IMapper<User, GetUserResponse>
{
    //Tem que trocar esse nome depois
    public GetUserResponse Map(User user)
    {
        return new GetUserResponse(
            user.Id,
            user.Name,
            user.Email,
            user.Age
        );
    }
}