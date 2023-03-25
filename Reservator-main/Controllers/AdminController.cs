using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reservator.Models;
using System.Data;

namespace Reservator.Controllers
{



    public class AdminController : BaseController
    {
        public AdminController(UserManager<AppUser> um) : base(um)
        {
        }

        [Authorize(Roles = "Admin")]
        //[Authorize]
        public IActionResult Index()
        {
            return View();
        }

    }
}
