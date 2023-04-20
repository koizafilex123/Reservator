using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservator.Data;
using Reservator.Data.Models;
using Reservator.DTO;
using Reservator.Models;
using System.Data;

namespace Reservator.Controllers
{
    public class RestaurantController : BaseController
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        private string[] Extention = new[] { "png", "jpg", "jpeg" };
        public RestaurantController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, UserManager<AppUser> um) : base(um)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            //List<Restaurant> allRestaurants = db.Restaurants
            //    .Include(r => r.Places)
            //    .Where(x => x.IsDeleted == false)
            //    //.ThenInclude(p=>p.Reservations)
            //    .ToList();
            string myId = (await GetCurrentUserAsync()).Id;


            var model = db.Restaurants.Where(x => x.IsDeleted == false && x.IsReserved == null).Select(x => new InputRestaurantModel
            {
                Id = x.Id,
                Name = x.Name,
                MainPic = x.MainPic,
                Address = x.Address,
                Pictures = x.Pictures,
                //Rating = x.Rating,
                Town = x.Town,
                Price = (double)x.Price,
                IsReserved = x.IsReserved,
                ImgURL = $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}", //прочитене на снимката от базата данни
            }).ToList();

            return View(model);

            //return View(allRestaurants);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(InputRestaurantModel model)
        {
            var restaurant = new Restaurant
            {
                Name = model.Name,
                MainPic = model.MainPic,
                Address = model.Address,
                Pictures = model.Pictures,
                Town = model.Town,
                Rating = model.Rating,
                Price = model.Price
            };
            var extention = Path.GetExtension(model.Image.FileName).TrimStart('.');
            var image = new Image
            {
                Extention = extention
            };
            string path = $"{webHostEnvironment.WebRootPath}/img/{image.Id}.{extention}";

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                model.Image.CopyTo(fs);
            }

            restaurant.Images.Add(image);
            db.Restaurants.Add(restaurant);
            db.SaveChanges();
            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var rest = db.Restaurants.Where(s => s.Id == id).FirstOrDefault();
            var model = new InputRestaurantModel
            {
                Id = rest.Id,
                Name = rest.Name,
                Address = rest.Address,
                Price = rest.Price,
                Town = rest.Town,
                //ImgURL = $"/img/{rest.Images.FirstOrDefault().Id}.{rest.Images.FirstOrDefault().Extention}",


            };
            return this.View(model);
        }
        [HttpPost]
        public IActionResult Edit(int id, InputRestaurantModel model) //update
        {
            var rest = db.Restaurants.Where(s => s.Id == model.Id).FirstOrDefault(); //търсене
            rest.Id = model.Id;
            rest.Name = model.Name;
            rest.Address = model.Address;
            rest.Price = model.Price;
            rest.Town = model.Town;
            //model.ImgURL = $"/img/{rest.Images.FirstOrDefault().Id}.{rest.Images.FirstOrDefault().Extention}";

            db.SaveChanges();
            return this.RedirectToAction("Index");
        }


        public IActionResult DeleteRest(int id)
        {
            var rst = db.Restaurants.Where(s => s.Id == id).FirstOrDefault(); //търсене
            db.Restaurants.Remove(rst);
            db.SaveChanges();
            return this.RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Restaurant restaurantFd = db.Restaurants.FirstOrDefault(r => r.Id == id);
            restaurantFd.IsDeleted = true;
            db.Update(restaurantFd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> IsReserved(int id)
        {
            string myId = (await GetCurrentUserAsync()).Id;

            Restaurant restaurantFd = db.Restaurants.FirstOrDefault(r => r.Id == id);
            restaurantFd.IsReserved = myId;
            db.Update(restaurantFd);
            db.SaveChanges();
            return RedirectToAction("Booked");
        }
        //Unsubscribe
        public async Task<IActionResult> Unsubscribe(int id)
        {
            string myId = (await GetCurrentUserAsync()).Id;

            Restaurant restaurantFd = db.Restaurants.FirstOrDefault(r => r.Id == id);

            restaurantFd.IsReserved = null;
            db.Update(restaurantFd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Booked()
        {
            string myId = (await GetCurrentUserAsync()).Id;

            var model = db.Restaurants.Where(x => x.IsDeleted == false && x.IsReserved == myId).Select(x => new InputRestaurantModel
            {
                Id = x.Id,
                Name = x.Name,
                MainPic = x.MainPic,
                Address = x.Address,
                Pictures = x.Pictures,
                //Rating = x.Rating,
                Price = (double)x.Price,
                IsReserved = x.IsReserved,
                ImgURL = $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}", //прочитене на снимката от базата данни
            }).ToList();

            return View(model);
        }

        public IActionResult Details(int id)
        {


            //string myId = (await GetCurrentUserAsync()).Id;
            var model = db.Restaurants.Where(x => id == x.Id).Select(x => new InputRestaurantModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                ImgURL = $"/img/{x.Images.FirstOrDefault().Id}.{x.Images.FirstOrDefault().Extention}",
                Town = x.Town,
                IsReserved = x.IsReserved,
                Address = x.Address
            }).FirstOrDefault();
            return this.View(model);
        }

        public IActionResult Contacts()
        {

            return View();
        }
        public IActionResult AboutMe()
        {
            return View();
        }
        //Restaurant restaurantFd = db.Restaurants.Where(x => x.IsDeleted == false)
        //    .Include(r => r.Places).FirstOrDefault(r => r.Id == id);

        //var result = new DetailsRestaurantDTO_out();
        //result.Restarant = restaurantFd;

        //var t = db.Reservations.Include(r => r.Place).ToArray();
        //var a = db.Reservations.Where(r => r.Place.RestaurantId == id).ToArray();
        //var b = db.Reservations.Where(r => r.Place.RestaurantId == id).Select(r => r.AppUser).ToArray();

        //result.AllUsersData = db.Reservations.Where(r => r.Place.RestaurantId == id).Select(r => r.AppUser)
        //    .Select(c => new MiniUserData
        //    {
        //        UserName = c.UserName,
        //        Id = c.Id
        //    }).ToArray();

        //return View(result);


    }

    //[Authorize(Roles = "Admin")]






}

