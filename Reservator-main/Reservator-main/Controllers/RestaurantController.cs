using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reservator.Data;
using Reservator.DTO;
using Reservator.Models;
using System.Data;

namespace Reservator.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext db;

        public RestaurantController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            List<Restaurant> allRestaurants = db.Restaurants
                .Include(r => r.Places)
                .Where(x => x.IsDeleted == false)
                //.ThenInclude(p=>p.Reservations)
                .ToList();

            return View(allRestaurants);
        }

        public IActionResult Details(int id)
        {
            Restaurant restaurantFd = db.Restaurants.Where(x => x.IsDeleted == false)
                .Include(r => r.Places).FirstOrDefault(r => r.Id == id);

            var result = new DetailsRestaurantDTO_out();
            result.Restarant = restaurantFd;

            var t = db.Reservations.Include(r => r.Place).ToArray();
            var a = db.Reservations.Where(r => r.Place.RestaurantId == id).ToArray();
            var b = db.Reservations.Where(r => r.Place.RestaurantId == id).Select(r => r.AppUser).ToArray();

            result.AllUsersData = db.Reservations.Where(r => r.Place.RestaurantId == id).Select(r => r.AppUser)
                .Select(c => new MiniUserData
                {
                    UserName = c.UserName,
                    Id = c.Id
                }).ToArray();

            return View(result);
        }

        [Authorize(Roles = "Admin")]
       
        [HttpPost]
        public IActionResult Delete(int id)
        {                      
            Restaurant restaurantFd = db.Restaurants.FirstOrDefault(r => r.Id == id);
            restaurantFd.IsDeleted = true;
            db.Update(restaurantFd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //TODO 
            return View();
        }

    }
}
