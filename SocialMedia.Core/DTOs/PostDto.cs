namespace SocialMedia.Core.DTOs
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; } = null!;
        public string? Image { get; set; }
    }
}