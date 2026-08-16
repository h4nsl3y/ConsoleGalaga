using DataLayer.DBConnection;
using DataLayer.DTO.DatabaseResponse;
using DataLayer.DTO.PlayerDTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.DAL.PlayerDAL
{
    public class PlayerDAL : IPlayerDAL
    {
        #region Fields
        private readonly IDBConnection _dataBaseConnection;
        #endregion

        #region Constructor
        public PlayerDAL(IDBConnection dBConnection)
        {
            _dataBaseConnection = dBConnection;
        }
        #endregion

        #region Public Methods
        public async Task<IDatabaseResponse<int>> AddPlayerAsync(string playerName)
        {
            string query = "INSERT INTO Player (PlayerName, DateCreated) VALUES (@PlayerName, @DateCreated)";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PlayerName", playerName),
                    new SqlParameter("@DateCreated", DateTime.UtcNow)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<int>> DeletePlayerByIdAsync(int playerId)
        {
            string query = "DELETE FROM Player WHERE PlayerId = @PlayerId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PlayerId", playerId)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<IPlayerDTO>> GetPlayerByIdAsync(int playerId)
        {
            string query = "SELECT * FROM Player WHERE PlayerId = @PlayerId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PlayerId", playerId)
                };

            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null || result.Data.Rows.Count == 0)
            {
                return DatabaseResponse<IPlayerDTO>.FailureResult("Player not found.");
            }

            var player = ConvertToPlayer(result.Data.Rows[0]);
            return DatabaseResponse<IPlayerDTO>.SuccessResult(player);
        }

        public async Task<IDatabaseResponse<IPlayerDTO>> GetPlayerByNameAsync(string playerName)
        {
            string query = "SELECT * FROM Player WHERE PlayerName = @PlayerName";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PlayerName", playerName)
                };

            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null || result.Data.Rows.Count == 0)
            {
                return DatabaseResponse<IPlayerDTO>.FailureResult("Player not found.");
            }

            IPlayerDTO player = ConvertToPlayer(result.Data.Rows[0]);
            return DatabaseResponse<IPlayerDTO>.SuccessResult(player);
        }

        public async Task<IDatabaseResponse<IList<IPlayerDTO>>> GetAllPlayersAsync(int page, int limit = 10)
        {
            string query = "SELECT * FROM Player ORDER BY PlayerId OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Offset", page * limit),
                    new SqlParameter("@PageSize", limit)
                };

            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null)
            {
                return DatabaseResponse<IList<IPlayerDTO>>.FailureResult("Failed to retrieve players.");
            }

            IList<IPlayerDTO> players = new List<IPlayerDTO>();
            foreach (DataRow row in result.Data.Rows)
            {
                players.Add(ConvertToPlayer(row));
            }

            return DatabaseResponse<IList<IPlayerDTO>>.SuccessResult(players);
        }

        public async Task<IDatabaseResponse<int>> GetAllPlayersCountAsync()
        {
            string query = "SELECT COUNT(*) FROM Player";

            var result = await _dataBaseConnection.ExecuteScalarAsync(query);

            if (!result.Success || result.Data == null)
            {
                return DatabaseResponse<int>.FailureResult("Failed to retrieve player count.");
            }

            return DatabaseResponse<int>.SuccessResult(int.Parse(result.Data));
        }

        public async Task<IDatabaseResponse<int>> UpdatePlayerAsync(IPlayerDTO player)
        {
            string query = "UPDATE Player SET PlayerName = @PlayerName WHERE PlayerId = @PlayerId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PlayerId", player.PlayerId),
                    new SqlParameter("@PlayerName", player.PlayerName)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }
        #endregion

        #region Private Methods
        private IPlayerDTO ConvertToPlayer(DataRow row)
        {
            int id = Convert.ToInt32(row["PlayerId"]);
            string name = row["PlayerName"]?.ToString() ??
                throw new InvalidOperationException("Player name cannot be null.");

            DateTime dateCreated = Convert.ToDateTime(row["DateCreated"]);

            return new PlayerDTO(id, name, dateCreated);
        }
        #endregion
    }
}