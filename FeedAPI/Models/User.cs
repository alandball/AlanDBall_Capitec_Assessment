using System.Collections.Generic;

namespace FeedAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Follower> Followers { get; set; }
    }
}
