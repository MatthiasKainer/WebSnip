namespace WebSnip.HttpRouting
{
    using System.Net;
    using System.Web;

    public class NoRouteFound : ICanHandleThisRoute
    {
        HttpContextBase httpContext;

        public bool CanHandle(HttpContextBase context)
        {
            httpContext = context;
            return true;
        }

        public void Handle()
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}