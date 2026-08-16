using DataLayer.DBConnection;
using DataLayer.DTO.DatabaseResponse;
using DataLayer.DTO.ScoreRecordDTO;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.DAL.ScoreRecordDAL
{
    public class ScoreRecordDAL : IScoreRecordDAL
    {
        #region Fields
        private readonly IDBConnection _dataBaseConnection;
        #endregion

        #region Constructor
        public ScoreRecordDAL(IDBConnection dBConnection)
        {
            _dataBaseConnection = dBConnection;
        }
        #endregion

        #region Public Methods
        public async Task<IDatabaseResponse<int>> AddScoreRecordAsync(int playerId, int score)
        {
            string query = "INSERT INTO ScoreRecord (PlayerId, Score, LastModified) VALUES (@PlayerId, @Score, @LastModified)";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@PlayerId", playerId),
                    new SqlParameter("@Score", score),
                    new SqlParameter("@LastModified", DateTime.UtcNow)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<int>> DeleteScoreRecordAsync(int scoreRecordId)
        {
            string query = "DELETE FROM ScoreRecord WHERE ScoreRecordId = @ScoreRecordId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@ScoreRecordId", scoreRecordId)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<int>> EditScoreRecordAsync(IScoreRecordDTO scoreRecord)
        {
            string query = "UPDATE ScoreRecord SET Score = @Score WHERE ScoreRecordId = @ScoreRecordId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Score", scoreRecord.Score),
                    new SqlParameter("@ScoreRecordId", scoreRecord.ScoreRecordId)
                };
            return await _dataBaseConnection.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task<IDatabaseResponse<IScoreRecordDTO>> GetScoreRecordAsync(int scoreRecordId)
        {
            string query = "SELECT * FROM ScoreRecord WHERE ScoreRecordId = @ScoreRecordId";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@ScoreRecordId", scoreRecordId)
                };
            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null || result.Data.Rows.Count == 0)
            {
                return DatabaseResponse<IScoreRecordDTO>.FailureResult("Score record not found.");
            }

            var scoreRecord = ConvertToScore(result.Data.Rows[0]);
            return DatabaseResponse<IScoreRecordDTO>.SuccessResult(scoreRecord);
        }

        public async Task<IDatabaseResponse<List<IScoreRecordDTO>>> GetTopScoreRecordAsync(int page, int limit = 10)
        {
            string query = "SELECT * FROM ScoreRecord ORDER BY Score DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@Offset", page * limit),
                    new SqlParameter("@PageSize", limit)
                };
            var result = await _dataBaseConnection.ExecuteQueryAsync(query, parameters);

            if (!result.Success || result.Data == null)
            {
                return DatabaseResponse<List<IScoreRecordDTO>>.FailureResult(result.Message);
            }

            List<IScoreRecordDTO> scoreRecords = new List<IScoreRecordDTO>();
            foreach (DataRow row in result.Data.Rows)
            {
                scoreRecords.Add(ConvertToScore(row));
            }
            return DatabaseResponse<List<IScoreRecordDTO>>.SuccessResult(scoreRecords);
        }

        public async Task<IDatabaseResponse<int>> GetTopScoreRecordCountAsync()
        {
            string query = "SELECT COUNT(*) FROM ScoreRecord";

            var result = await _dataBaseConnection.ExecuteScalarAsync(query);

            if (!result.Success || result.Data == null)
            {
                return DatabaseResponse<int>.FailureResult("Failed to retrieve score record count.");
            }

            return DatabaseResponse<int>.SuccessResult(int.Parse(result.Data));
        }
        #endregion

        #region Private Methods
        private IScoreRecordDTO ConvertToScore(DataRow row)
        {
            int id = Convert.ToInt32(row["ScoreRecordID"]);
            int playerId = Convert.ToInt32(row["PlayerId"]);
            int score = Convert.ToInt32(row["Score"]);
            DateTime lastModified = Convert.ToDateTime(row["LastModified"]);
            return new ScoreRecordDTO(id, playerId, score, lastModified);
        }
        #endregion
    }
}
