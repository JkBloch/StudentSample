using Microsoft.AspNetCore.Mvc;
using StudentSample.Models;
using System.Diagnostics;

namespace StudentSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILanguageRepository _languageRepository;


        public HomeController(ILogger<HomeController> logger,ILanguageRepository languageRepository)
        {
            _logger = logger;
            _languageRepository=languageRepository;
        }

        public async Task<IActionResult> Index()
        {
            var lstLanguage = await _languageRepository.GetAllLanguage();       
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}