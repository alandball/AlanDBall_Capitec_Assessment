using AlanDBall_Capitec_Assessment.Services.Interfaces;

namespace AlanDBall_Capitec_Assessment.Controllers
{
    internal class TwitterFeedController
    {
        private readonly IFeedPrinter _feedPrinter;

        public TwitterFeedController(IFeedPrinter feedPrinter)
        {
            _feedPrinter = feedPrinter;
        }

        /// <summary>
        /// PrintTwitterFeed
        /// </summary>
        public void PrintTwitterFeed()
        {
            
        }
    }
}
