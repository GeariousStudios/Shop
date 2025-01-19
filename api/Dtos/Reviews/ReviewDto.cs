namespace Shop.api.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int Score { get; set; }
    }
}
