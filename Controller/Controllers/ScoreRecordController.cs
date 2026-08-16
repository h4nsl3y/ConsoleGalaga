using BusinessLayer.BL.ScoreRecordBL;
using BusinessLayer.Models.ScoreRecordModel;
using Microsoft.AspNetCore.Mvc;
using SVC.Models.Response;
using ILogger = Helpers.LoggerHelper.ILogger;

namespace SVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreRecordController(IScoreRecordBL scoreRecordBL, ILogger logger) : ControllerBase
    {
        #region Public Methods
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IScoreRecordModel request)
        {
            var result = await scoreRecordBL.AddScoreRecordAsync(request.PlayerId, request.Score);
            if (!result.Success)
            {
                logger.LogError($"Failed to add score record for player {request.PlayerId}: {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }

        [HttpGet]
        public async Task<IActionResult> GetScoreRecord([FromQuery] int scoreRecordId)
        {
            var result = await scoreRecordBL.GetScoreRecordAsync(scoreRecordId);
            if (!result.Success)
            {
                logger.LogError($"Failed to get score record with ID {scoreRecordId}: {result.Message}");
                return NotFound(Response<object>.FailureResult(result.Message));
            }
            return Ok(Response<object>.SuccessResult(result.Data!, result.Message));
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopScoreRecords([FromQuery] int page = 0, [FromQuery] int limit = 10)
        {
            var result = await scoreRecordBL.GetTopScoreRecordsAsync(page, limit);
            if (!result.Success)
            {
                logger.LogError($"Failed to get top score records for limit {limit}: {result.Message}");
                return BadRequest(Response<object>.FailureResult(result.Message));
            }
            return Ok(Response<object>.SuccessResult(result.Data!, result.Message));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int scoreRecordId)
        {
            var result = await scoreRecordBL.DeleteScoreRecordAsync(scoreRecordId);
            if (!result.Success)
            {
                logger.LogError($"Failed to delete score record with ID {scoreRecordId}: {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IScoreRecordModel request)
        {
            var result = await scoreRecordBL.EditScoreRecordAsync(request.ScoreRecordId, request.PlayerId, request.Score);
            if (!result.Success)
            {
                logger.LogError($"Failed to edit score record with ID {request.ScoreRecordId}: {result.Message}");
                return BadRequest(Response<bool>.FailureResult(result.Message));
            }
            return Ok(Response<bool>.SuccessResult(result.Data, result.Message));
        }
        #endregion
    }
}
