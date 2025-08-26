namespace Domain.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double NewPrice { get; set; }
        public double OldPrice { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

    }
}
