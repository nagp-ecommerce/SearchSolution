
namespace SearchApiService.Models
{
    public class Product
    {
        public string? Id { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int? InstockQuanity { get; set; }
        public double Price { get; set; }
        public string? Brand { get; set; }
        //public List<ProductImage>? ProductImages { get; set; }

    }
}
