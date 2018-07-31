using FeedAPI.Services;
using FeedAPI.Services.Interfaces;

namespace FeedAPI
{
    /// <summary>
    /// Dependency resolver for Ninject which allows to inject interfaces into Web API controller classes
    /// </summary>
    public class NinjectDependencyResolver //: NinjectModule
    {
        //public override void Load()
        //{
        //    Bind<IFeedPrinter>().To<FeedPrinter>();
        //}
    }
}
