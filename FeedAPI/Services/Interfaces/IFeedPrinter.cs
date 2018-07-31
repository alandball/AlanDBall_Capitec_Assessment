namespace FeedAPI.Services.Interfaces
{
    public interface IFeedPrinter
    {
        void PrintTwitterFeed(string userTxtFileName, string tweetTxtFileName);
    }
}
