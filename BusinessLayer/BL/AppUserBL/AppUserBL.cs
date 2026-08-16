using BusinessLayer.Models.AppDataModel;
using BusinessLayer.Models.GridModel;
using BusinessLayer.Models.ServiceResponse;
using DataLayer.DAL.AppUserDAL;
using DataLayer.DTO.AppuserDTO;
using Helpers.LoggerHelper;

namespace BusinessLayer.BL.AppUserBL
{
    public class AppUserBL(IAppUserDAL appUserDAL, ILogger logger) : IAppUserBL
    {
        #region Public Methods
        public async Task<IServiceResponse<bool>> AuthenticateAsync(string username, string password)
        {
            var result = await appUserDAL.GetAppUserHashByNameAsync(username);
            if (!result.Success || result.Data == null)
            {
                logger.LogError($"Authentication failed for user: {username}. Reason: {result.Message}");
                return ServiceResponse<bool>.FailureResult("Authentication failed.");
            }

            bool isValid = CompareHash(password, result.Data);
            return ServiceResponse<bool>.SuccessResult(isValid);
        }

        public async Task<IServiceResponse<bool>> AddUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                return ServiceResponse<bool>.FailureResult("Player name cannot be empty.");

            var result = await appUserDAL.GetAppUserByNameAsync(username);
            if (result.Success && result.Data != null)
            {
                logger.LogError($"User already exists: {username}");
                return ServiceResponse<bool>.FailureResult("User already exists.");
            }

            var hash = Encrypt(password);
            var addResult = await appUserDAL.AddAppUserAsync(username, hash);
            if (!addResult.Success)
            {
                logger.LogError($"Failed to add user: {username}. Reason: {addResult.Message}");
                return ServiceResponse<bool>.FailureResult("Failed to add user.");
            }

            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<bool>> DeleteAppUserByIdAsync(int userId)
        {
            var result = await appUserDAL.DeleteAppUserByIdAsync(userId);
            if (!result.Success)
            {
                logger.LogError($"Failed to delete user: {userId}. Reason: {result.Message}");
                return ServiceResponse<bool>.FailureResult("Failed to delete user.");
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }

        public async Task<IServiceResponse<IGridModel<IAppUserModel>>> GetAllUsersAsync(int page = 0, int limit = 10)
        {
            limit = (limit <= 0) ? 10 : limit;
            page = (page < 0) ? 0 : page;

            var countResponse = await appUserDAL.GetAllAppUsersCountAsync();
            int totalCount = (countResponse.Success) ? countResponse.Data : 0;

            var result = await appUserDAL.GetAllAppUsersAsync(page, limit);
            if (!result.Success)
            {
                logger.LogError($"Failed to retrieve users. Reason: {result.Message}");
                return ServiceResponse<IGridModel<IAppUserModel>>.FailureResult("Failed to retrieve users.");
            }
            var data = ConvertToModel(result.Data);
            var users = data ?? new List<IAppUserModel>();
            var gridTitle = new List<string> { "User" };

            var gridData = new GridModel<IAppUserModel>(gridTitle, users, page, users.Count, limit);

            return ServiceResponse<IGridModel<IAppUserModel>>.SuccessResult(gridData);
        }

        public async Task<IServiceResponse<bool>> UpdateUserAsync(string username, string password)
        {
            var result = await appUserDAL.GetAppUserByNameAsync(username);
            if (!result.Success || result.Data == null)
            {
                logger.LogError($"User not found: {username}");
                return ServiceResponse<bool>.FailureResult("User not found.");
            }
            var updateData = new AppUserDTO(result.Data.UserId, username);

            var response = await appUserDAL.UpdateAppUserAsync(updateData);
            if (!response.Success)
            {
                logger.LogError($"Failed to update user: {username}. Reason: {response.Message}");
                return ServiceResponse<bool>.FailureResult("Failed to update user.");
            }
            return ServiceResponse<bool>.SuccessResult(true);
        }
        #endregion

        #region Private Methods
        private static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool CompareHash(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        private static IList<IAppUserDTO> ConvertToDTO(IList<IAppUserModel> model)
        {
            return model
                .Select(static m => (IAppUserDTO)new AppUserDTO(m.UserId, m.Username))
                .ToList();
        }

        private static IList<IAppUserModel> ConvertToModel(IList<IAppUserDTO> dto)
        {
            return dto
                .Select(static d => (IAppUserModel)new AppUserModel(d.UserId, d.Username))
                .ToList();
        }
        #endregion
    }
}
