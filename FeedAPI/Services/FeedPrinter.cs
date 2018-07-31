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
                var users = ExtractUsersFromUserTxt(userTxtFileName);
                var tweets = ExtractTweetsFromTweetTxt(tweetTxtFileName, users);

                //Print tweets for each user, if there are
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

        private List<User> ExtractUsersFromUserTxt(string userTxtFileName)
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
            foreach (var user in users)
            {
                user.Followers = followers.Where(x => x.UserIdsFollowed.Contains(user.Id)).ToList();
            }

            return users;
        }

        private List<Tweet> ExtractTweetsFromTweetTxt(string tweetTxtFileName, List<User> users)
        {
            _fileToRead = Enums.FileToRead.tweet;
            var lines = StringReader(tweetTxtFileName).ToList();

            // Build list of users
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
