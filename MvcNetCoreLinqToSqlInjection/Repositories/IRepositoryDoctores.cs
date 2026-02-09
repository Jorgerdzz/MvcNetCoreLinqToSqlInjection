using MvcNetCoreLinqToSqlInjection.Models;

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public interface IRepositoryDoctores
    {
        List<Doctor> GetDoctores();

        Task InsertDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital);

    }
}
