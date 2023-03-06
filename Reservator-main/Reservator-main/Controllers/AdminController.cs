using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Reservator.Controllers
{



    public class AdminController : Controller
    {
    [Authorize(Roles = "Admin")]
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

    }
}
