using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Reservator.Models;

namespace Reservator.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(UserManager<AppUser> um)
        {
            _userManager = um;
        }

        protected UserManager<AppUser> _userManager { get; }

        protected Task<AppUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
