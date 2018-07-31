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
                _fileToRead = Enums.FileToRead.user;
                var linesOfUserTxt = StringReader(userTxtFileName);

                _fileToRead = Enums.FileToRead.tweet;
                var linesOfTweetTxt = StringReader(tweetTxtFileName);

                var users = ExtractUsersFromUserTxt(linesOfUserTxt.ToList());
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

        private List<User> ExtractUsersFromUserTxt(List<string> lines)
        {
            // Build list of users
            var names = lines.SelectMany(x => x.Replace(" follows", ",").Replace(" ", "").Split(',')).Distinct().ToList();

            var users = names.Select(name => new User
            {
                Id = names.IndexOf(name),
                Name = name
            }).ToList();

            // Build list of followers
            //var followers = lines.SelectMany(x => x.Substring(x.IndexOf(",", StringComparison.Ordinal) + 7, x.Length - x.IndexOf("follows", StringComparison.Ordinal) - 7).Replace(" ", "").Split(','))


            var namesOfFollowers = lines.Select(x => x.Substring(0, x.IndexOf("follows", StringComparison.Ordinal))).Distinct().ToList();

            var followers = namesOfFollowers
            .Select(nameOfFollower => new Follower
            {
                Id = namesOfFollowers.IndexOf(nameOfFollower),
                Name = nameOfFollower,
                UserIdsFollowed = new List<int>()

            }).ToList();



            //var users = lines.SelectMany(x => x.Replace(" follows", ",").Replace(" ", "").Split(',')).Distinct()
            //    .Select(x =>
            //    {
            //        return new User
            //        {
            //            Id = lines.IndexOf(x) + 1,
            //            Name = x.Substring(0, x.IndexOf(" ", StringComparison.Ordinal)),
            //            Followers = followers
            //        };
            //    }).ToList();

            return new List<User>();
        }
    }
}
