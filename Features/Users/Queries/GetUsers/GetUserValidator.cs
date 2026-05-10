namespace LowLevelDotNET.Features.Users.Interfaces
{
    public class GetUserValidator : IValidator<GetUserRequest>
    {
        public bool Validate(GetUserRequest request, out string error)
        {
            if(request.Id == Guid.Empty)
            {
                error = "Id cannot be empty";
                return false;
            }
            error = null;
            return true;
        }
    }
}