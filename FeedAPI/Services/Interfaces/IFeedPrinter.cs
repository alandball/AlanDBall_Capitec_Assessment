using System.Collections.Generic;
using FeedAPI.Models;

namespace FeedAPI.Services.Interfaces
{
    public interface IFeedPrinter
    {
        void PrintTwitterFeed(string userTxtFileName, string tweetTxtFileName);
        List<User> GetUsersAndTheirFollowers(string userTxtFileName);
        void AddFollowerToUser(List<User> users, List<Follower> followers);
        List<Tweet> GetAllTweets(string tweetTxtFileName, List<User> users);
        void DeleteFollowerFromUser(int followerId);
    }
}
