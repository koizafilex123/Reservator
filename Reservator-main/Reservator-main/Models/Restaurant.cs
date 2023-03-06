namespace Reservator.Models
{
    public class Restaurant : BaseEntity
    {
        public Restaurant()
        {
            Places = new HashSet<Place>();
            RestaurantUserGrades = new HashSet<RestaurantUserGrade>();
        }
        public string Name { get; set; }
        public Town Town { get; set; }
        public string Address { get; set; }
        public string MainPic { get; set; }
        public string Pictures { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<Place> Places { get; set; }
        public virtual ICollection<RestaurantUserGrade> RestaurantUserGrades { get; set; }
    }



}
