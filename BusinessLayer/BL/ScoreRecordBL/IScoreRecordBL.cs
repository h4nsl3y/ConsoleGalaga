using BusinessLayer.Models.ServiceResponse;
using DataLayer.DTO.ScoreRecordDTO;

namespace BusinessLayer.BL.ScoreRecordBL
{
    public interface IScoreRecordBL
    {
        Task<IServiceResponse<bool>> AddScoreRecordAsync(int playerId, int score);
        Task<IServiceResponse<bool>> DeleteScoreRecordAsync(int scoreRecordId);
        Task<IServiceResponse<bool>> EditScoreRecordAsync(int scoreRecordId, int playerId, int score);
        Task<IServiceResponse<IScoreRecordDTO>> GetScoreRecordAsync(int scoreRecordId);
        Task<IServiceResponse<IList<IScoreRecordDTO>>> GetTopScoreRecordsAsync(int page = 0, int limit = 10);
        Task<IServiceResponse<int>> GetTopScoreRecordsCountAsync();
    }
}
