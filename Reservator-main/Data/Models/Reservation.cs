using System.ComponentModel.DataAnnotations.Schema;

namespace Reservator.Models
{
    public class Reservation : BaseEntity
    {
        public Reservation()
        { }

        public Reservation(DateTime time)
        {
            Time = time;
        }

        public Reservation(int hoursFromNow)
        {
            Time = DateTime.UtcNow;
            Time.AddHours(hoursFromNow);
        }

        [ForeignKey(nameof(Place))]
        public int PlaceId { get; set; }

        public virtual Place Place { get; set; }


        [ForeignKey(nameof(AppUser))]
        public string UserId { get; set; }


        public virtual AppUser AppUser { get; set; }
        public DateTime Time { get; set; }
    }
}