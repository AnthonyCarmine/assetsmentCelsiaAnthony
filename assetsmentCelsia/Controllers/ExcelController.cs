using Microsoft.AspNetCore.Mvc;
using assetsmentCelsia.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using assetsmentCelsia.Models;

namespace assetsmentCelsia.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IExcelRepository _excelRepository;

        public ExcelController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        // Acción para mostrar la vista principal con la lista de usuarios
        public async Task<IActionResult> Index()
        {
            var users = await _excelRepository.GetAllUsersAsync();
            return View(users);
        }

        // Método para exportar la lista de usuarios a un archivo Excel
        [HttpGet]
        public async Task<IActionResult> ExportarExcel()
        {
            var users = await _excelRepository.GetAllUsersAsync();
            var nombreArchivo = $"Users.xlsx";
            return _excelRepository.ExportUsersToExcel(nombreArchivo, users);
        }

        // Método para importar una lista de usuarios desde un archivo Excel
        [HttpPost]
        public async Task<IActionResult> ImportarExcel(IFormFile archivo)
        {
            var users = await _excelRepository.ImportUsersFromExcelAsync(archivo);
            await _excelRepository.AddUsersAsync(users);
            return View("Index");
        }

       


    }
}
