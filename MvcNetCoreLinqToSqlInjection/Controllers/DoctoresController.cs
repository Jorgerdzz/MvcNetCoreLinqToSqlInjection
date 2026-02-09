using Microsoft.AspNetCore.Mvc;
using MvcNetCoreLinqToSqlInjection.Models;
using MvcNetCoreLinqToSqlInjection.Repositories;

namespace MvcNetCoreLinqToSqlInjection.Controllers
{
    public class DoctoresController : Controller
    {
        private IRepositoryDoctores repo;

        public DoctoresController(IRepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            await this.repo.InsertDoctor(idDoctor, apellido, especialidad, salario, idHospital);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteDoctorAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            Doctor doctor = this.repo.FindDoctorById(id);
            return View(doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int idHospital, int idDoctor, string apellido, string especialidad, int salario)
        {
            await this.repo.UpdateDoctorAsync(idHospital, idDoctor, apellido, especialidad, salario);
            return RedirectToAction("Index");
        }

        public IActionResult Buscador()
        {
            List<Doctor> doctores = this.repo.GetDoctores();
            return View(doctores);
        }

        [HttpPost]
        public IActionResult Buscador(string especialidad)
        {
            List<Doctor> doctores = this.repo.BuscadorDoctores(especialidad);
            return View(doctores);
        }

    }
}
