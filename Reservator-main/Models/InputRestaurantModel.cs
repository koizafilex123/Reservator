namespace Reservator.Models
{
    public class InputRestaurantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Town { get; set; }
        public string Address { get; set; }
        public string? MainPic { get; set; }
        public string? Pictures { get; set; }
        public int? Rating { get; set; }
        public string ImgURL { get; set; }
        public IFormFile Image { get; set; }
        public double? Price { get; set; }
        public string? IsReserved { get; set; }

    }
}
