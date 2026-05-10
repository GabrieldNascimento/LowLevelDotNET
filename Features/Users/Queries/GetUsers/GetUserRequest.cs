using LowLevelDotNET.Features.Users.Interfaces;
using LowLevelDotNET.Features.Users.Responses;
namespace LowLevelDotNET.Features.Users
{
    public record GetUserRequest(Guid Id) : IRequest<GetUserResponse>;
}