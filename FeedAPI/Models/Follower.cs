using System.Collections.Generic;

namespace FeedAPI.Models
{
    public class Follower
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<int> UserIdsFollowed { get; set; }
    }
}
