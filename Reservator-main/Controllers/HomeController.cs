using Microsoft.AspNetCore.Mvc;
using Reservator.Models;
using Reservator.Services;
using System.Diagnostics;

namespace Reservator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IDataBaseSeeder dbSeeder;

        public HomeController(ILogger<HomeController> logger, IDataBaseSeeder dbSeeder)
        {
            _logger = logger;

            this.dbSeeder = dbSeeder;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            
            if (!await this.dbSeeder.HasAnyDataInDBAsync())
            {
                await this.dbSeeder.InsertDataInDBAsync();
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}