using LowLevelDotNET.Features.Users;
using LowLevelDotNET.Features.Users.Interfaces;
using LowLevelDotNET.Logging;
using LowLevelDotNET.Features.Users.Shared;
using LowLevelDotNET.Features.Users.Responses;
using LowLevelDotNET.Features.Mediator;
namespace LowLevelDotNET.Controllers
{   
    public class UserController
    {

        private readonly GetUserHandler _handler;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        public UserController(GetUserHandler handler, IMediator mediator, ILogger logger)
        {
            _handler = handler;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<ControllerResult<GetUserResponse>> GetUser(string input)
        {
            _logger.Info(
                $"Input received for search a user (id): {input}");

            if (!Guid.TryParse(input, out var id))
            {
                _logger.Error("Invalid id");

                return ControllerResult<GetUserResponse>
                .BadRequest("Invalid id");
            }

            try
            {
                var request = new GetUserRequest(id);

                var response =
                    await _mediator.Send(request);

                if (response == null)
                {
                    _logger.Warning($"There is no user with that id: {id}");

                    return ControllerResult<GetUserResponse>
                    .NotFound($"There is no user with that id: {id}");
                }

                _logger.Info($"User found! (id): {id}");

                return ControllerResult<GetUserResponse>
                    .Ok(response);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                return ControllerResult<GetUserResponse>
                .InternalError("An error occurred while fetching the user");
            }
        }
    }
}