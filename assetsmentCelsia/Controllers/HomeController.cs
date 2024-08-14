using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using assetsmentCelsia.Models;
using assetsmentCelsia.Services.Repositories;
using assetsmentCelsia.Services.Interfaces;

namespace assetsmentCelsia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserLoginRepository _userLoginRepository;

        public HomeController(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        public IActionResult Index()
        {
            // Pass any TempData messages to the view
            ViewBag.ConsoleMessage = TempData["ConsoleMessage"];
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    TempData["ConsoleMessage"] = "Entering try block";
                    var user = await _userLoginRepository.AuthenticateAsync(email, password);
                    return RedirectToAction("Index", "Excel"); 
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            return RedirectToAction("Index");
        }
    }
}

