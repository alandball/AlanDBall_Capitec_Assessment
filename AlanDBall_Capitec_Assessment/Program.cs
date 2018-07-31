using System;
using System.Linq;
using System.Reflection;
using AlanDBall_Capitec_Assessment.Controllers;
using AlanDBall_Capitec_Assessment.Models;
using AlanDBall_Capitec_Assessment.Services.Interfaces;
using Ninject;
using System.Net.Http;

namespace AlanDBall_Capitec_Assessment
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Start Ninject Dependency Injector
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var feedPrinter = kernel.Get<IFeedPrinter>();

            var result = ValidateArgs(args);
            if ((result & ArgsValidationResult.Ok) == ArgsValidationResult.Ok)
            {
                var twitterFeed = new TwitterFeedController(feedPrinter);
                twitterFeed.PrintTwitterFeed();
            }
            else
            {
                if ((result & ArgsValidationResult.UserTxtArgumentMissing) == ArgsValidationResult.UserTxtArgumentMissing)
                    Console.WriteLine("Missing argument: \"user.txt\"");

                if ((result & ArgsValidationResult.TweetTxtArgumentMissing) == ArgsValidationResult.TweetTxtArgumentMissing)
                    Console.WriteLine("Missing argument: \"tweet.txt\"");
            }

            // Keep console open so we can read the feed.
            // Done here instead of in PrintTwitterFeed as there could potentially be an exception, in which case we want the console to remain open
            // to be able to read the exception
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        private static ArgsValidationResult ValidateArgs(string[] args)
        {
            var result = ArgsValidationResult.Default;

            // Error Handling
            // Check for null or empty
            if (args == null)
            {
                result |= ArgsValidationResult.ArgumentsNull;
                return result;
            }

            // Check arguments contains user.txt
            if (!args.Contains("user.txt"))
            {
                result |= ArgsValidationResult.UserTxtArgumentMissing;
            }

            // Check arguments contains tweet.txt
            if (!args.Contains("tweet.txt"))
            {
                result |= ArgsValidationResult.TweetTxtArgumentMissing;
            }

            // Check we haven't changed validation result from default, because if we did then we are missing an argument
            if (result == ArgsValidationResult.Default)
            {
                result = ArgsValidationResult.Ok;
            }

            return result;
        }
    }
}
