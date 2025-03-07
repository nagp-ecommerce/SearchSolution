
namespace SearchApiService.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string? Description { get; set; }
        //public ProductCategory Category { get; set; }
        //public int InstockQuanity { get; set; }
        public double Price { get; set; }
        public string? Brand { get; set; }
        //public List<ProductImage>? ProductImages { get; set; }
    }
}
