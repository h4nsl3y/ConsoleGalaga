using BusinessLayer.BL.AppUserBL;
using BusinessLayer.Models.AppDataModel;
using Microsoft.AspNetCore.Mvc;
using SVC_API.Models.Response;
using ILogger = Helpers.LoggerHelper.ILogger;

namespace SVC_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController(IAppUserBL appUserBL, ILogger logger) : ControllerBase
    {
        #region Public Methods
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AppUserModel request)
        {
            var result = await appUserBL.AuthenticateAsync(request.Username, request.Password);
            if (!result.Success)
            {
                logger.LogError($"Failed to authenticate user '{request.Username}': {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] IAppUserModel request)
        {
            var result = await appUserBL.AddUserAsync(request.Username, request.Password);
            if (!result.Success)
            {
                logger.LogError($"Failed to register user '{request.Username}': {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int page = 0, [FromQuery] int limit = 10)
        {
            var result = await appUserBL.GetAllUsersAsync(page, limit);
            if (!result.Success)
            {
                logger.LogError($"Failed to get all users with limit {limit}: {result.Message}");
                return BadRequest(Response<object>.FailureResult(result.Message));
            }
            return Ok(Response<object>.SuccessResult(result.Data, result.Message));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromQuery] int userId)
        {
            var result = await appUserBL.DeleteAppUserByIdAsync(userId);
            if (!result.Success)
            {
                logger.LogError($"Failed to delete user with ID {userId}: {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IAppUserModel request)
        {
            var result = await appUserBL.UpdateUserAsync(request.Username, request.Password);
            if (!result.Success)
            {
                logger.LogError($"Failed to update user '{request.Username}': {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }
        #endregion
    }
}
