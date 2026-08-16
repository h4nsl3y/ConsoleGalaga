using BusinessLayer.Models.ServiceResponse;
using DataLayer.DAL.ScoreRecordDAL;
using DataLayer.DTO.ScoreRecordDTO;
using Helpers.LoggerHelper;

namespace BusinessLayer.BL.ScoreRecordBL
{
    public class ScoreRecordBL(IScoreRecordDAL scoreRecordDAL, ILogger logger) : IScoreRecordBL
    {
        #region Public Methods
        public async Task<IServiceResponse<bool>> AddScoreRecordAsync(int playerId, int score)
        {
            if (playerId <= 0)
                return ServiceResponse<bool>.FailureResult("Player ID must be positive.");
            if (score < 0)
                return ServiceResponse<bool>.FailureResult("Score cannot be negative.");

            var response = await scoreRecordDAL.AddScoreRecordAsync(playerId, score);

            if (!response.Success)
            {
                string errorMessage = "Failed to add score record";
                logger.LogError($"{errorMessage}: for Player ID {playerId} with Score {score}: {response.Message}");
                return ServiceResponse<bool>.FailureResult(errorMessage);
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<bool>> DeleteScoreRecordAsync(int scoreRecordId)
        {
            var response = await scoreRecordDAL.DeleteScoreRecordAsync(scoreRecordId);

            if (!response.Success)
            {
                string errorMessage = "Failed to delete score record";
                logger.LogError($"{errorMessage} with ID {scoreRecordId}: {response.Message}");
                return ServiceResponse<bool>.FailureResult(errorMessage);
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<bool>> EditScoreRecordAsync(int scoreRecordId, int playerId, int score)
        {
            var request = new ScoreRecordDTO(scoreRecordId, playerId, score);
            var response = await scoreRecordDAL.EditScoreRecordAsync(request);

            if (!response.Success)
            {
                string errorMessage = "Failed to edit score record";
                logger.LogError($"{errorMessage}: {response.Message}");
                return ServiceResponse<bool>.FailureResult(errorMessage);
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<IScoreRecordDTO>> GetScoreRecordAsync(int scoreRecordId)
        {
            var response = await scoreRecordDAL.GetScoreRecordAsync(scoreRecordId);

            if (!response.Success || response.Data == null)
            {
                string errorMessage = "Failed to retrieve score record";
                logger.LogError($"{errorMessage}: {response.Message}");
                return ServiceResponse<IScoreRecordDTO>.FailureResult(errorMessage);
            }
            return ServiceResponse<IScoreRecordDTO>.SuccessResult(response.Data);
        }

        public async Task<IServiceResponse<IList<IScoreRecordDTO>>> GetTopScoreRecordsAsync(int page, int limit = 10)
        {
            var response = await scoreRecordDAL.GetTopScoreRecordAsync(page, limit);

            if (!response.Success)
            {
                string errorMessage = "Failed to retrieve top score records";
                logger.LogError($"{errorMessage}: {response.Message}");
                return ServiceResponse<IList<IScoreRecordDTO>>.FailureResult(errorMessage);
            }

            IList<IScoreRecordDTO> results = response.Data ?? new List<IScoreRecordDTO>();
            return ServiceResponse<IList<IScoreRecordDTO>>.SuccessResult(results);
        }

        public async Task<IServiceResponse<int>> GetTopScoreRecordsCountAsync()
        {
            var response = await scoreRecordDAL.GetTopScoreRecordCountAsync();

            if (!response.Success)
            {
                string errorMessage = "Failed to retrieve score record count";
                logger.LogError($"{errorMessage}: {response.Message}");
                return ServiceResponse<int>.FailureResult(errorMessage);
            }
            return ServiceResponse<int>.SuccessResult(response.Data);
        }
        #endregion
    }
}
