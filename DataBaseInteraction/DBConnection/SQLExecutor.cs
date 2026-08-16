using DataLayer.DTO.DatabaseResponse;
using Helpers.LoggerHelper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.DBConnection
{
    public class SQLExecutor : IDBConnection
    {
        #region Fields
        private readonly ILogger _logger;
        private SqlConnection? _sqlConnection;
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public SQLExecutor(ILogger logger, string connString)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _connectionString = !string.IsNullOrWhiteSpace(connString)
                ? connString
                : throw new ArgumentException("Connection string cannot be null or empty.", nameof(connString));
        }
        #endregion

        #region Public Methods
        public async Task<IDatabaseResponse<int>> ExecuteNonQueryAsync(string query, params SqlParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();
                await using SqlCommand command = new SqlCommand(query, _sqlConnection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                int rowsAffected = await command.ExecuteNonQueryAsync();
                return DatabaseResponse<int>.SuccessResult(rowsAffected);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error executing non-query: {ex.Message}");
                return DatabaseResponse<int>.FailureResult(ex.Message);
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<IDatabaseResponse<DataTable>> ExecuteQueryAsync(string query, params SqlParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();
                using var command = new SqlCommand(query, _sqlConnection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                await using SqlDataReader reader = await command.ExecuteReaderAsync();

                var table = new DataTable();
                table.Load(reader);

                return DatabaseResponse<DataTable>.SuccessResult(table);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error executing query: {ex.Message}");
                return DatabaseResponse<DataTable>.FailureResult(ex.Message);
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }

        public async Task<IDatabaseResponse<string>> ExecuteScalarAsync(string query, params SqlParameter[] parameters)
        {
            try
            {
                await OpenConnectionAsync();
                await using SqlCommand command = new SqlCommand(query, _sqlConnection);
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                var scalarResult = await command.ExecuteScalarAsync();
                string result = scalarResult != null ? Convert.ToString(scalarResult) ?? "" : "";
                return DatabaseResponse<string>.SuccessResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error executing scalar query: {ex.Message}");
                return DatabaseResponse<string>.FailureResult(ex.Message);
            }
            finally
            {
                await CloseConnectionAsync();
            }
        }
        #endregion

        #region Private Methods
        private async Task OpenConnectionAsync()
        {
            _sqlConnection = new SqlConnection(_connectionString);
            await _sqlConnection.OpenAsync();
        }

        private async Task CloseConnectionAsync()
        {
            if (_sqlConnection != null)
            {
                await _sqlConnection.CloseAsync();
                await _sqlConnection.DisposeAsync();
                _sqlConnection = null;
            }
        }
        #endregion
    }
}
