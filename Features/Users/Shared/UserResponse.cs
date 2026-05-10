namespace LowLevelDotNET.Features.Users.Responses
{
    public record GetUserResponse(
        Guid Id,
        string Name,
        string Email,
        int Age
    );
}