using DataLayer.DTO.AppuserDTO;
using DataLayer.DTO.DatabaseResponse;

namespace DataLayer.DAL.AppUserDAL
{
    public interface IAppUserDAL
    {
        Task<IDatabaseResponse<int>> AddAppUserAsync(string userName, string password);
        Task<IDatabaseResponse<int>> DeleteAppUserByIdAsync(int userId);
        Task<IDatabaseResponse<int>> UpdateAppUserAsync(IAppUserDTO appUser);
        Task<IDatabaseResponse<IAppUserDTO>> GetAppUserByIdAsync(int userId);
        Task<IDatabaseResponse<IAppUserDTO>> GetAppUserByNameAsync(string userName);
        Task<IDatabaseResponse<IList<IAppUserDTO>>> GetAllAppUsersAsync(int page, int limit);
        Task<IDatabaseResponse<int>> GetAllAppUsersCountAsync();
        Task<IDatabaseResponse<string>> GetAppUserHashByNameAsync(string userName);
    }
}
