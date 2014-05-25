namespace WebSnip
{
    using System.Linq;
    using System.Web;
    using HttpRouting;
    using Transform;
    using Utils.Json;

    public class HttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var jsonSerializer = new JsonSerializer();
            var snipMaker = new SnipMaker();
            var tagSetFactory = new TagSetFactory();
            var routeHandlers = new ICanHandleThisRoute[] { new EvaluateUrl(jsonSerializer, snipMaker, tagSetFactory), new NoRouteFound() };

            routeHandlers.First(_ => _.CanHandle(new HttpContextWrapper(context))).Handle();
        }

        public bool IsReusable { get { return true; } }
    }
}
