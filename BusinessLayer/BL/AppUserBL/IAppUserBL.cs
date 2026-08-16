using BusinessLayer.Models.AppDataModel;
using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.ServiceResponse;
using DataLayer.DTO.DatabaseResponse;

namespace BusinessLayer.BL.AppUserBL
{
    public interface IAppUserBL
    {
        Task<IServiceResponse<bool>> AuthenticateAsync(string username, string password);
        Task<IServiceResponse<bool>> AddUserAsync(string username, string password);
        Task<IServiceResponse<IGridModel<IAppUserModel>>> GetAllUsersAsync(int page = 0, int limit = 10);
        Task<IServiceResponse<bool>> DeleteAppUserByIdAsync(int userId);
        Task<IServiceResponse<bool>> UpdateUserAsync(string username, string password);
    }
}
