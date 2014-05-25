namespace WebSnip.HttpRouting
{
    using System;
    using System.Net;
    using System.Web;
    using Transform;
    using Utils.Json;

    public class EvaluateUrl : ICanHandleThisRoute
    {
        readonly ISerializeToJson serializer;
        readonly ISnipMaker snipMaker;
        readonly ITagSetFactory tagSetFactory;
        HttpContextBase httpContext;
        Uri uri;

        public EvaluateUrl(ISerializeToJson serializer, ISnipMaker snipMaker, ITagSetFactory tagSetFactory)
        {
            this.serializer = serializer;
            this.snipMaker = snipMaker;
            this.tagSetFactory = tagSetFactory;
        }

        public bool CanHandle(HttpContextBase context)
        {
            httpContext = context;
            var url = httpContext.Request.Path;
            var forUrl = httpContext.Request.QueryString["for"];

            return url.ToLowerInvariant().EndsWith("/get") && Uri.TryCreate(forUrl, UriKind.Absolute, out uri);
        }

        public void Handle()
        {
            snipMaker.WithTransformSnippet(new TransformBuilder().Using(tagSetFactory.GetFor(uri)).Build());

            httpContext.Response.Write(serializer.Serialize(snipMaker.GetSnippetFor(uri).ToRenderedParts()));
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}