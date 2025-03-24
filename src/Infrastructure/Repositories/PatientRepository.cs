using ClinAgenda.src.Application.DTOs.Patient;
using Dapper;
using MySql.Data.MySqlClient;

namespace ClinAgenda.src.Infrastructure.Repositories
{
    public class PatientRepository
    {
        private readonly MySqlConnection _connection;

        public PatientRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

         public async Task<PatientDTO> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT 
                    ID, 
                    NAME,
                    PHONENUMBER,
                    DOCUMENTNUMBER,
                    STATUSID,
                    BIRTHDATE 
                FROM PATIENT
                WHERE ID = @Id";

            var patient = await _connection.QueryFirstOrDefaultAsync<PatientDTO>(query, new { Id = id });

            return patient;
        }
    }
}