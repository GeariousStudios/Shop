namespace Shop.api.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int[]? ImageIds { get; set; }
        public int[] CategoryIds { get; set; }
        public string AppUserId { get; set; }
    }
}
