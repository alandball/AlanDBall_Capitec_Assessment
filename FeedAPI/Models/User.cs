using System.Collections.Generic;

namespace FeedAPI.Models
{
    class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public List<Follower> Followers { get; set; }
    }
}
