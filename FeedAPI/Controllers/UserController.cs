using System.Web.Http;
using FeedAPI.Services.Interfaces;

namespace FeedAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IFeedPrinter _feedPrinter;

        public UserController(IFeedPrinter feedPrinter)
        {
            _feedPrinter = feedPrinter;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTxtFileName"></param>
        /// <param name="tweetTxtFileName"></param>
        public void PrintTwitterFeed(string userTxtFileName, string tweetTxtFileName)
        {
            _feedPrinter.PrintTwitterFeed(userTxtFileName, tweetTxtFileName);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }
    }
}
