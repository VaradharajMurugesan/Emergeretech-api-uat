using System.Data;
using Dapper;
using MySqlConnector;
//using Microsoft.Data.SqlClient;
//using MySql.Data.MySqlClient;

namespace EmergereJobCareerWebApi.Services
{
    public class DBService : IDBService
    {
        private readonly IDbConnection _db;  

        public DBService(IConfiguration configuration)
        {
            _db = new MySqlConnection(configuration.GetConnectionString("SqlConnection"));
        }

        public async Task<T> GetAsync<T>(string command, object parms)
        {
            try
            {
                T result;
                result = (await _db.QueryAsync<T>(command, parms).ConfigureAwait(false)).FirstOrDefault();
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<List<T>> GetAll<T>(string command, object parms)
        {
            try
            {
                List<T> result = new List<T>();
                result = (await _db.QueryAsync<T>(command, parms)).ToList();
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<int> EditData(string command, object parms)
        {
            try
            {

                int result;
                result = await _db.ExecuteAsync(command, parms);
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
