using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.LeaderBoardRecordModel;
using BusinessLayer.Service.LeaderBoardService;
using Microsoft.AspNetCore.Mvc;
using SVC.Models.Response;
using ILogger = Helpers.LoggerHelper.ILogger;

namespace SVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController(ILeaderBoardService leaderBoardService, ILogger logger) : ControllerBase
    {
        #region Public Methods
        [HttpGet("top")]
        public async Task<IActionResult> GetTopScoresAsync([FromQuery] int page = 0, [FromQuery] int limit = 10)
        {
            var result = await leaderBoardService.GetTopScoresAsync(page, limit);
            if (!result.Success)
            {
                logger.LogError($"Failed to get top scores for page {page}: {result.Message}");
                return BadRequest(Response<string>.FailureResult(result.Message));
            }

            return Ok(Response<IGridModel<ILeaderBoardRecordModel>>.SuccessResult(result.Data, result.Message));
        }
        #endregion
    }
}
