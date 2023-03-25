using Reservator.Models;

namespace Reservator.DTO
{
    public class DetailsRestaurantDTO_out
    {

        public Restaurant Restarant { get; set; }

        public ICollection<MiniUserData> AllUsersData { get; set; }

    }


    public class MiniUserData
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }

}
