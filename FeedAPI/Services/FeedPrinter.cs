using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FeedAPI.Models;
using FeedAPI.Services.Interfaces;

namespace FeedAPI.Services
{
    public class FeedPrinter : IFeedPrinter
    {
        private static Enums.FileToRead _fileToRead;

        /// <summary>
        /// PrintTwitterFeed
        /// </summary>
        /// <param name="userTxtFileName"></param>
        /// <param name="tweetTxtFileName"></param>
        public void PrintTwitterFeed(string userTxtFileName, string tweetTxtFileName)
        {
            try
            {
                var users = GetUsersAndTheirFollowers(userTxtFileName);
                var tweets = GetAllTweets(tweetTxtFileName, users);

                //Print tweets for each user, if there are, as well as tweets for users this user has followed
                foreach (var user in users.OrderBy(x => x.Name))
                {
                    Console.WriteLine(user.Name);

                    var usersThisUserFollowed = users.Where(y => y.Followers.Select(z => z.UserId).Contains(user.Id)).ToList();
                    var tweetsToPrint = tweets.Where(x => x.UserId == user.Id || usersThisUserFollowed.Select(y => y.Id).Contains(x.UserId)).ToList();

                    foreach (var tweet in tweetsToPrint)
                    {
                        var userNameOfTweet = users.Single(x => x.Id == tweet.UserId).Name;
                        Console.WriteLine($"\t@{userNameOfTweet}: {tweet.Content}");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"{_fileToRead}.txt does not exist. Please ensure that {_fileToRead}.txt is in the same directory as the application exe.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static IEnumerable<string> StringReader(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                // Read each line to list
                var linesOfText = new List<string>();
                while (!sr.EndOfStream)
                {
                    linesOfText.Add(sr.ReadLine());
                }

                return linesOfText;
            }
        }

        /// <summary>
        /// Get all users and all the followers for each user
        /// </summary>
        /// <param name="userTxtFileName"></param>
        /// <returns></returns>
        public List<User> GetUsersAndTheirFollowers(string userTxtFileName)
        {
            _fileToRead = Enums.FileToRead.user;
            var lines = StringReader(userTxtFileName).ToList();

            // Build list of users
            var names = lines.SelectMany(x => x.Replace(" follows", ",").Replace(" ", "").Split(',')).Distinct().ToList();

            var users = names.Select(name => new User
            {
                Id = names.IndexOf(name) + 1,
                Name = name
            }).ToList();

            // Build list of followers
            var namesOfFollowers = lines.Select(x => x.Substring(0, x.IndexOf("follows", StringComparison.Ordinal) - 1)).Distinct().ToList();

            var followers = namesOfFollowers
            .Select(nameOfFollower =>
                {
                    var linesWhereUserFollowed = lines.Where(line => line.Substring(0, line.IndexOf("follows", StringComparison.Ordinal) - 1).Equals(nameOfFollower));
                    var usersFollowed = linesWhereUserFollowed.SelectMany(line => line.Substring(line.IndexOf("follows", StringComparison.Ordinal) + 8, line.Length - line.IndexOf("follows", StringComparison.Ordinal) - 8).Replace(" ", "").Split(',')).Distinct().ToList();
                    var user = users.Single(x => x.Name.Equals(nameOfFollower));

                    return new Follower
                    {
                        Id = namesOfFollowers.IndexOf(nameOfFollower) + 1,
                        UserId = user.Id,
                        Name = nameOfFollower,
                        UserIdsFollowed = usersFollowed.Select(x => users.Single(y => y.Name.Equals(x)).Id).ToList()
                    };
                }).ToList();

            // Map followers into User.Followers
            AddFollowerToUser(users, followers);

            return users;
        }

        /// <summary>
        /// This api endpoint can add one or more followers to one or more users
        /// </summary>
        /// <param name="users"></param>
        /// <param name="followers"></param>
        public void AddFollowerToUser(List<User> users, List<Follower> followers)
        {
            foreach (var user in users)
            {
                user.Followers = followers.Where(x => x.UserIdsFollowed.Contains(user.Id)).ToList();
            }
        }

        /// <summary>
        /// This api endpoint deletes a follower from a user
        /// </summary>
        /// <param name="followerId"></param>
        public void DeleteFollowerFromUser(int followerId)
        {
            //Implement code that will set IsDeleted to 1 in the database for this particular follower record
        }

        /// <summary>
        /// Get all tweets
        /// </summary>
        /// <param name="tweetTxtFileName"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        public List<Tweet> GetAllTweets(string tweetTxtFileName, List<User> users)
        {
            _fileToRead = Enums.FileToRead.tweet;
            var lines = StringReader(tweetTxtFileName).ToList();

            // Build list of tweets
            return lines.Select(line =>
            {
                var content = line.Substring(line.IndexOf(">", StringComparison.Ordinal) + 1, line.Length - line.IndexOf(">", StringComparison.Ordinal) - 1);
                var nameOfUserOfTweet = line.Substring(0, line.IndexOf(">", StringComparison.Ordinal));
                var user = users.SingleOrDefault(x => x.Name.Equals(nameOfUserOfTweet));// Use SingleOrDefault as it is possible the file contains a tweet with a name of someone that isn't in the users file. So potentially null

                return new Tweet
                {
                    Id = lines.IndexOf(line) + 1,
                    Content = content,
                    UserId = user?.Id ?? 0
                };
            }).ToList();
        }
    }
}
