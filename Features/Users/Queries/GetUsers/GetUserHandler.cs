using LowLevelDotNET.Domain.Users;
using LowLevelDotNET.Features.Users.Mappers;
using LowLevelDotNET.Features.Users.Responses;

namespace LowLevelDotNET.Features.Users.Interfaces
{
    public class GetUserHandler : IHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUserRepository _repository;
        private readonly UserMapper _mapper;
        private readonly IValidator<GetUserRequest> _validator;
        public GetUserHandler(IUserRepository repository, UserMapper mapper, IValidator<GetUserRequest> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<GetUserResponse> Handle(GetUserRequest request)
        {
            if(_validator.Validate(request, out string error)){
                //Não deve ter exceção aqui
                throw new Exception(error); 
            }
            var user = _repository.GetById(request.Id);
            var getUserResponse = _mapper.Map(user);
            return Task.FromResult(getUserResponse);
        }
    }     
}