using Reservator.Data.Models;

namespace Reservator.Models
{
    public class Restaurant : BaseEntity
    {
        public Restaurant()
        {
            Places = new HashSet<Place>();
            RestaurantUserGrades = new HashSet<RestaurantUserGrade>();
            Images = new HashSet<Image>();
        }
        public string Name { get; set; }
        public string? Town { get; set; }
        public string Address { get; set; }
        public string? MainPic { get; set; }
        public string? Pictures { get; set; }
        public double? Rating { get; set; }
        public double? Price { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<RestaurantUserGrade> RestaurantUserGrades { get; set; }
    }



}
