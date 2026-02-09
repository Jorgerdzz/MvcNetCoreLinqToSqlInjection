using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using MvcNetCoreLinqToSqlInjection.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using static Azure.Core.HttpHeader;

#region STORED PROCEDURES
//create or replace procedure SP_DELETE_DOCTOR
//(p_iddoctor DOCTOR.DOCTOR_NO%type)
//AS
//BEGIN   
//    delete from DOCTOR where DOCTOR_NO = p_iddoctor;
//commit;
//END;

//create or replace procedure SP_UPDATE_DOCTOR
//(p_idhospital DOCTOR.HOSPITAL_COD%type, p_iddoctor DOCTOR.DOCTOR_NO%type, p_apellido DOCTOR.APELLIDO%type, p_especialidad DOCTOR.ESPECIALIDAD%type, p_salario DOCTOR.SALARIO%type)
//AS
//BEGIN
//    update DOCTOR set DOCTOR.HOSPITAL_COD = p_idhospital, DOCTOR.APELLIDO = p_apellido,
//    DOCTOR.ESPECIALIDAD = p_especialidad, DOCTOR.SALARIO = p_salario
//    where DOCTOR.DOCTOR_NO = p_iddoctor;
//commit;
//END;
#endregion

namespace MvcNetCoreLinqToSqlInjection.Repositories
{
    public class RepositoryDoctoresOracle: IRepositoryDoctores
    {
        private DataTable tablaDoctor;
        private OracleConnection cn;
        private OracleCommand com; 
        
        public RepositoryDoctoresOracle()
        {
            string connectionString = @"Data Source=LOCALHOST:1521/FREEPDB1; Persist Security Info=true;User Id=SYSTEM;Password=oracle";
            this.cn = new OracleConnection(connectionString);
            this.com = new OracleCommand();
            this.com.Connection = this.cn;
            this.tablaDoctor = new DataTable();
            string sql = "select * from DOCTOR";
            OracleDataAdapter ad = new OracleDataAdapter(sql, connectionString);
            ad.Fill(this.tablaDoctor);
        }

        public List<Doctor> GetDoctores()
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD"),
                };
                doctores.Add(doctor);
            }
            return doctores;
        }

        public Doctor FindDoctorById(int idDoctor)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           where datos.Field<int>("DOCTOR_NO") == idDoctor
                           select datos;
            var row = consulta.First();
            Doctor doctor = new Doctor
            {
                IdDoctor = row.Field<int>("DOCTOR_NO"),
                Apellido = row.Field<string>("APELLIDO"),
                Especialidad = row.Field<string>("ESPECIALIDAD"),
                Salario = row.Field<int>("SALARIO"),
                IdHospital = row.Field<int>("HOSPITAL_COD"),
            };
          
            return doctor;
        }

        public async Task InsertDoctor(int idDoctor, string apellido, string especialidad, int salario, int idHospital)
        {
            string sql = "insert into DOCTOR values(:idHospital, :idDoctor, :apellido, :especialidad, :salario)";
            OracleParameter pamIdHospital = new OracleParameter(":idHospital", idHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":idDoctor", idDoctor);
            OracleParameter pamApellido = new OracleParameter(":apellido", apellido);
            OracleParameter pamEspecialidad = new OracleParameter(":especialidad", especialidad);
            OracleParameter pamSalario = new OracleParameter(":salario", salario);
            this.com.Parameters.Add(pamIdHospital);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamSalario);

            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeleteDoctorAsync(int idDoctor)
        {
            string sql = "SP_DELETE_DOCTOR";
            OracleParameter pamIdDoctor = new OracleParameter(":p_iddoctor", idDoctor);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task UpdateDoctorAsync(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            string sql = "SP_UPDATE_DOCTOR";
            OracleParameter pamIdHospital = new OracleParameter(":p_idhospital", idHospital);
            OracleParameter pamIdDoctor = new OracleParameter(":p_iddoctor", idDoctor);
            OracleParameter pamApellido = new OracleParameter(":p_apellido", apellido);
            OracleParameter pamEspecialidad = new OracleParameter(":p_especialidad", especialidad);
            OracleParameter pamSalario = new OracleParameter(":p_salario", salario);
            this.com.Parameters.Add(pamIdHospital);
            this.com.Parameters.Add(pamIdDoctor);
            this.com.Parameters.Add(pamApellido);
            this.com.Parameters.Add(pamEspecialidad);
            this.com.Parameters.Add(pamSalario);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public List<Doctor> BuscadorDoctores(string especilidad)
        {
            var consulta = from datos in this.tablaDoctor.AsEnumerable()
                           where datos.Field<string>("ESPECIALIDAD") == especilidad
                           select datos;
            List<Doctor> doctores = new List<Doctor>();
            foreach (var row in consulta)
            {
                Doctor doctor = new Doctor
                {
                    IdDoctor = row.Field<int>("DOCTOR_NO"),
                    Apellido = row.Field<string>("APELLIDO"),
                    Especialidad = row.Field<string>("ESPECIALIDAD"),
                    Salario = row.Field<int>("SALARIO"),
                    IdHospital = row.Field<int>("HOSPITAL_COD"),
                };
                doctores.Add(doctor);
            }
            return doctores;
        }

    }
}
