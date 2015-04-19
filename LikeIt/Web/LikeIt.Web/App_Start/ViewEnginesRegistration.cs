namespace LikeIt.Web
{
    using System.Web.Mvc;

    public class ViewEnginesConfig
    {
        internal static void RegisterViewEngine(ViewEngineCollection viewEngineCollection, IViewEngine viewEngine)
        {
            viewEngineCollection.Clear();
            viewEngineCollection.Add(viewEngine);
        }
    }
}
