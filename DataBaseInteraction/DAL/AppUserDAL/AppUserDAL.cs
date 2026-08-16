using DataLayer.DBConnection;
using DataLayer.DTO.AppuserDTO;
using DataLayer.DTO.DatabaseResponse;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.DAL.AppUserDAL
{
    public class AppUserDAL : IAppUserDAL
    {
        #region Fields
        private readonly IDBConnection _dataBaseConnection;
        #endregion

        #region Constructor
        public AppUserDAL(IDBConnection dataBaseConnection)
        {
            _dataBaseConnection = dataBaseConnection;
        }
        #endregion

        #region Public Methods
        public async Task<IDatabaseResponse<int>> AddAppUserAsync(string userName, string password)
        {
            string query = "INSERT INTO AppUser (Username, Password, LastModified) VALUES (@Username, @Password, @LastModified)";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", userName),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@LastModified", DateTime.UtcNow)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<int>> DeleteAppUserByIdAsync(int userId)
        {
            string query = "DELETE FROM AppUser WHERE UserId = @UserId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", userId)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<int>> UpdateAppUserAsync(IAppUserDTO appUser)
        {
            string query = "UPDATE AppUser SET Username = @Username, LastModified = @LastModified WHERE UserId = @UserId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", appUser.UserId),
                    new SqlParameter("@Username", appUser.Username),
                    new SqlParameter("@LastModified", DateTime.UtcNow)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<IList<IAppUserDTO>>> GetAllAppUsersAsync(int page, int limit)
        {
            limit = (limit <= 0) ? 10 : limit;
            int offset = page * limit;

            string query = "SELECT UserId, Username FROM AppUser ORDER BY UserId OFFSET @Offset ROWS FETCH NEXT @Limit ROWS ONLY";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Offset", offset),
                    new SqlParameter("@Limit", limit)
                };
            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null || result.Data.Rows.Count == 0)
            {
                return DatabaseResponse<IList<IAppUserDTO>>.FailureResult("AppUser not found.");
            }

            IList<IAppUserDTO> appUsers = new List<IAppUserDTO>();
            foreach (DataRow row in result.Data.Rows)
            {
                appUsers.Add(ConvertToAppUser(row));
            }
            return DatabaseResponse<IList<IAppUserDTO>>.SuccessResult(appUsers);
        }

        public async Task<IDatabaseResponse<IAppUserDTO>> GetAppUserByIdAsync(int userId)
        {
            string query = "SELECT UserId, Username FROM AppUser WHERE UserId = @UserId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@UserId", userId)
                };
            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null || result.Data.Rows.Count == 0)
            {
                return DatabaseResponse<IAppUserDTO>.FailureResult("AppUser not found.");
            }

            IAppUserDTO appUser = ConvertToAppUser(result.Data.Rows[0]);
            return DatabaseResponse<IAppUserDTO>.SuccessResult(appUser);
        }

        public async Task<IDatabaseResponse<IAppUserDTO>> GetAppUserByNameAsync(string userName)
        {
            string query = "SELECT UserId, Username FROM AppUser WHERE Username = @Username";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", userName)
                };
            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null || result.Data.Rows.Count == 0)
            {
                return DatabaseResponse<IAppUserDTO>.FailureResult("AppUser not found.");
            }

            IAppUserDTO appUser = ConvertToAppUser(result.Data.Rows[0]);
            return DatabaseResponse<IAppUserDTO>.SuccessResult(appUser);
        }

        public async Task<IDatabaseResponse<int>> GetAllAppUsersCountAsync()
        {
            string query = "SELECT COUNT(*) FROM AppUser";

            var result = await _dataBaseConnection.ExecuteScalarAsync(query);

            if (!result.Success || result.Data == null)
            {
                return DatabaseResponse<int>.FailureResult("Failed to retrieve player count.");
            }

            return DatabaseResponse<int>.SuccessResult(int.Parse(result.Data));
        }

        public async Task<IDatabaseResponse<string>> GetAppUserHashByNameAsync(string userName)
        {
            string query = "SELECT Password FROM AppUser WHERE Username = @Username";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Username", userName)
                };
            return await _dataBaseConnection.ExecuteScalarAsync(query, parameters);
        }
        #endregion

        #region  Private Method
        private IAppUserDTO ConvertToAppUser(DataRow row)
        {
            int id = Convert.ToInt32(row["UserId"]);
            string name = row["Username"]?.ToString() ??
                throw new InvalidOperationException("User name cannot be null.");

            return new AppUserDTO(id, name);
        }
        #endregion
    }
}
