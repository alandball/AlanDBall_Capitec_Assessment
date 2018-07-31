using System;
using System.Collections.Generic;
using AlanDBall_Capitec_Assessment.Services.Interfaces;

namespace AlanDBall_Capitec_Assessment.Services
{
    public class FeedPrinter : IFeedPrinter
    {
        /// <summary>
        /// PrintTwitterFeed
        /// </summary>
        public void PrintTwitterFeed()
        {
            try
            {
                var users = new List<string> { "Alan", "Ward", "Martin" };

                foreach (var user in users)
                {
                    Console.WriteLine(user);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
