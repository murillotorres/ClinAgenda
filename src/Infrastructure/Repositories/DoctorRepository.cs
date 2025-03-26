using System.Text;
using ClinAgenda.src.Application.DTOs.Patient;
using ClinAgenda.src.Core.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace ClinAgenda.src.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MySqlConnection _connection;

        public DoctorRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<DoctorListDTO>> GetDoctorAsync(string? name, int? speacialtyId, int? statusId, int offset, int itemsPerPage)
        {
            var innerJoins = new StringBuilder(@"
                FROMT DOCTOR D
                    INNER JOIN STATUS S ON D.STATUSID = S.ID
                    INNER JOIN DOCTOR_SPECIALTY DSPE ON DSPE.DOCTORID = D.ID
                    WHERE 1 = 1;
            ");
            
            var parameters = new DynamicParameters();

            if(!string.IsNullOrEmpty(name))
            {
                innerJoins.Append("AND D.NAME LIKE @Name");
                parameters.Add("Name, $"%{name}%"");
            }

            if (speacialtyId.HasValue)
            {
                innerJoins.Append("AND DSPE.SPECIALTYID = @SpecialtyId");
                parameters.Add("SpecialtyId", speacialtyId.Value);
            }
        }
    }
}