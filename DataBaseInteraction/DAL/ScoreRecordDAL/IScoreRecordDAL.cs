using DataLayer.DTO.DatabaseResponse;
using DataLayer.DTO.ScoreRecordDTO;

namespace DataLayer.DAL.ScoreRecordDAL
{
    public interface IScoreRecordDAL
    {
        Task<IDatabaseResponse<int>> AddScoreRecordAsync(int playerId, int score);
        Task<IDatabaseResponse<IScoreRecordDTO>> GetScoreRecordAsync(int scoreRecordId);
        Task<IDatabaseResponse<List<IScoreRecordDTO>>> GetTopScoreRecordAsync(int page, int limit = 10);
        Task<IDatabaseResponse<int>> GetTopScoreRecordCountAsync();
        Task<IDatabaseResponse<int>> EditScoreRecordAsync(IScoreRecordDTO scoreRecord);
        Task<IDatabaseResponse<int>> DeleteScoreRecordAsync(int scoreRecordId);
    }
}
