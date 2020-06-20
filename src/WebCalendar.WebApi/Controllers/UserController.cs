using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models.User;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // POST: /api/user/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<IdentityResult>> Register([FromBody] UserRegisterRequestModel model)
        {
            UserRegisterServiceModel userRegisterServiceModel = _mapper
                .Map<UserRegisterRequestModel, UserRegisterServiceModel>(model);
            IdentityResult result = await _userService.RegisterAsync(userRegisterServiceModel);

            if (!result.Succeeded)
            {
                return BadRequest(new
                {
                    message = result.Errors
                });
            }
            
            return Ok();
        }

        //POST: /api/user/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<UserAuthenticateResponseModel>> Authenticate(
            [FromBody] UserAuthenticateRequestModel model)
        {
            UserAuthenticateServiceModel userAuthenticateServiceModel = _mapper
                .Map<UserAuthenticateRequestModel, UserAuthenticateServiceModel>(model);
            UserTokenServiceModel userTokenServiceModel = await _userService
                .AuthenticateAsync(userAuthenticateServiceModel);

            if (userAuthenticateServiceModel == null)
            {
                return Unauthorized(new { message = "Username or password is incorrect" });
            }

            UserAuthenticateResponseModel userAuthenticateResponseModel = _mapper
                .Map<UserTokenServiceModel, UserAuthenticateResponseModel>(userTokenServiceModel);
            return Ok(userAuthenticateResponseModel);
        }
    }
}