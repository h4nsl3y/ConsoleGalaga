using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.LeaderBoardRecordModel;
using BusinessLayer.Models.ServiceResponse;

namespace BusinessLayer.Service.LeaderBoardService
{
    public interface ILeaderBoardService
    {
        Task<IServiceResponse<bool>> AddScoreAsync(string name, int score);
        Task<IServiceResponse<IGridModel<ILeaderBoardRecordModel>>> GetTopScoresAsync(int page = 0, int limit = 10);
    }
}
