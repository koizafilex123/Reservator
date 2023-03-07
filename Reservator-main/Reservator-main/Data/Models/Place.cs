using System.ComponentModel.DataAnnotations.Schema;

namespace Reservator.Models
{
    public class Place : BaseEntity
    {
        public string Location { get; set; }
        public string MainPic { get; set; }
        public string Pictures { get; set; }
        public int Seats { get; set; }

        [ForeignKey(nameof(Restaurant))]
        public int RestaurantId { get; set; }

        public virtual Restaurant Restaurant { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }
                                                = new HashSet<Reservation>();
    
    }




}
