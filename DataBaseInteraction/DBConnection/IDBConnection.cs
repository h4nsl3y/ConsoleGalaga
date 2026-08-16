using DataLayer.DTO.DatabaseResponse;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.DBConnection
{
    public interface IDBConnection
    {
        Task<IDatabaseResponse<int>> ExecuteNonQueryAsync(string query, params SqlParameter[] parameters);
        Task<IDatabaseResponse<DataTable>> ExecuteQueryAsync(string query, params SqlParameter[] parameters);
        Task<IDatabaseResponse<string>> ExecuteScalarAsync(string query, params SqlParameter[] parameters);
    }
}