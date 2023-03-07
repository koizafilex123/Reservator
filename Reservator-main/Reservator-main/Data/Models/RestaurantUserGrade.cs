namespace Reservator.Models
{
    public class RestaurantUserGrade
    {
        public string AppUserId { get; set; }

        public int RestaurantId { get; set; }

        public virtual AppUser User { get; set; }

        public virtual Restaurant Restaurant { get; set; }

        public Grade Grade { get; set; }

        public DateTime Time { get; set; }
    }

    public enum Grade
    {
        Excelent = 6, VeryGood = 5, Good = 4, Average = 3, Poor = 2
    }
}
