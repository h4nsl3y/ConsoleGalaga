using DataLayer.DTO.DatabaseResponse;
using DataLayer.DTO.PlayerDTO;

namespace DataLayer.DAL.PlayerDAL
{
    public interface IPlayerDAL
    {
        Task<IDatabaseResponse<int>> AddPlayerAsync(string playerName);
        Task<IDatabaseResponse<int>> DeletePlayerByIdAsync(int playerId);
        Task<IDatabaseResponse<IPlayerDTO>> GetPlayerByIdAsync(int playerId);
        Task<IDatabaseResponse<IPlayerDTO>> GetPlayerByNameAsync(string playerName);
        Task<IDatabaseResponse<IList<IPlayerDTO>>> GetAllPlayersAsync(int page, int limit = 10);
        Task<IDatabaseResponse<int>> GetAllPlayersCountAsync();
        Task<IDatabaseResponse<int>> UpdatePlayerAsync(IPlayerDTO player);
    }
}