using ApplicationLayer.CQRS.Commands.Auth;
using ApplicationLayer.DTOs.Auth.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EVO.ParkingGo.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var  result = await _mediator.Send(new AuthLoginCommand() { AuthLoginRequest = loginRequest});

            if(result.IsSuccessful)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
