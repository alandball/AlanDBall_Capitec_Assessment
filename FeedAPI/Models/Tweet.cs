namespace FeedAPI.Models
{
    class Tweet
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
    }
}
