using Microsoft.AspNetCore.Identity;

namespace Reservator.Models
{
    public class AppUser : IdentityUser
    {
        public string EGN { get; set; } = "12345";

        public virtual ICollection<Reservation> Reservations { get; set; }
                                               = new HashSet<Reservation>();


        public virtual ICollection<RestaurantUserGrade> UserRestaurantGrades { get; set; }
        = new HashSet<RestaurantUserGrade>();
    }
}