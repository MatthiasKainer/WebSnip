namespace WebSnip
{
    using System;
    using Transform;

    public class SnipMaker
    {
        readonly IRequestWebSnippets snipRequest;
        readonly ITransformWebContentToWebSnippets transformWebSnippet;

        protected Uri BaseUri { get; private set; }

        public SnipMaker(IRequestWebSnippets snipRequest, ITransformWebContentToWebSnippets transformWebSnippet)
        {
            this.snipRequest = snipRequest;
            this.transformWebSnippet = transformWebSnippet;
        }

        public WebSnippet GetSnippetFor(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (!uri.IsAbsoluteUri && BaseUri == null) throw new NoBaseUrlProvidedButRelativeUrlRequestedException();
            if (!uri.IsAbsoluteUri) uri = new Uri(BaseUri, uri);

            var webContent = snipRequest.GetContent(uri);
            return transformWebSnippet.ToWebSnippet(uri, webContent);
        }

        public SnipMaker WithBaseUrl(Uri uri)
        {
            BaseUri = uri;
            return this;
        }
    }
}
