using BusinessLayer.BL.PlayerBL;
using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.PlayerModel;
using Microsoft.AspNetCore.Mvc;
using SVC.Models.Response;
using ILogger = Helpers.LoggerHelper.ILogger;

namespace SVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController(IPlayerBL playerBL, ILogger logger) : ControllerBase
    {
        #region Public Methods
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] string playerName)
        {
            var result = await playerBL.AddPlayerAsync(playerName);
            if (!result.Success)
            {
                logger.LogError($"Failed to add player '{playerName}': {result.Message}");
                return BadRequest(Response<int>.FailureResult(result.Message));
            }

            return Ok(Response<int>.SuccessResult(result.Data, result.Message));
        }

        [HttpGet]
        public async Task<IActionResult> GetPlayer([FromQuery] int playerId)
        {
            var result = await playerBL.GetPlayerByIdAsync(playerId);
            if (!result.Success)
            {
                logger.LogError($"Failed to get player with ID {playerId}: {result.Message}");
                return NotFound(Response<IPlayerModel>.FailureResult(result.Message));
            }

            return Ok(Response<IPlayerModel>.SuccessResult(result.Data!, result.Message));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllPlayers([FromQuery] int page = 0, [FromQuery] int limit = 10)
        {
            var result = await playerBL.GetAllPlayersAsync(page, limit);
            if (result == null)
            {
                logger.LogError($"Failed to get all players for page {page}");
                return BadRequest(Response<IGridModel<IPlayerModel>>.FailureResult("Failed to get all players"));
            }

            return Ok(Response<IGridModel<IPlayerModel>>.SuccessResult(result));
        }

        [HttpDelete("{playerId}")]
        public async Task<IActionResult> Delete([FromQuery] int playerId)
        {
            var result = await playerBL.DeletePlayerByIdAsync(playerId);
            if (!result.Success)
            {
                logger.LogError($"Failed to delete player with ID {playerId}: {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }

            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IPlayerModel request)
        {
            IPlayerModel player = new PlayerModel(request.PlayerId, request.PlayerName, DateTime.Now);
            var result = await playerBL.UpdatePlayerAsync(player);
            if (!result.Success)
            {
                logger.LogError($"Failed to edit player with ID {request.PlayerId}: {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }

            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }
        #endregion
    }
}

