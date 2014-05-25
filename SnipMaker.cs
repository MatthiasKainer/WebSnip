namespace WebSnip
{
    using System;
    using Request;
    using Transform;

    public class SnipMaker : ISnipMaker
    {
        IRequestWebSnippets snipRequest;
        ITransformWebContentToWebSnippets transformWebSnippet;

        protected Uri BaseUri { get; private set; }

        public SnipMaker(IRequestWebSnippets snipRequest = null, 
            ITransformWebContentToWebSnippets transformWebSnippet = null)
        {
            this.snipRequest = snipRequest ?? new WebSnippetRequest();
            this.transformWebSnippet = transformWebSnippet ?? null;
        }

        public void WithSnipRequest(IRequestWebSnippets requestWebSnippets)
        {
            snipRequest = requestWebSnippets;
        }

        public void WithTransformSnippet(ITransformWebContentToWebSnippets transformWebContentToWebSnippets)
        {
            transformWebSnippet = transformWebContentToWebSnippets;
        }

        public WebSnippet GetSnippetFor(Uri uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (!uri.IsAbsoluteUri && BaseUri == null) throw new NoBaseUrlProvidedButRelativeUrlRequestedException();
            if (!uri.IsAbsoluteUri) uri = new Uri(BaseUri, uri);

            var webContent = snipRequest.GetContent(uri);
            var transform = transformWebSnippet ?? new TransformBuilder().Using(new TagSetFactory().GetFor(uri)).Build();
            return transform.ToWebSnippet(uri, webContent);
        }

        public SnipMaker WithBaseUrl(Uri uri)
        {
            BaseUri = uri;
            return this;
        }
    }
}
