using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.PlayerModel;
using BusinessLayer.Models.ServiceResponse;

namespace BusinessLayer.BL.PlayerBL
{
    public interface IPlayerBL
    {
        Task<IServiceResponse<int>> AddPlayerAsync(string playerName);
        Task<IServiceResponse<bool>> DeletePlayerByIdAsync(int playerId);
        Task<IServiceResponse<bool>> UpdatePlayerAsync(IPlayerModel player);
        Task<IServiceResponse<IPlayerModel>> GetPlayerByIdAsync(int playerId);
        Task<IGridModel<IPlayerModel>> GetAllPlayersAsync(int page = 0, int limit = 10);
    }
}
