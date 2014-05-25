namespace WebSnip
{
    using System;
    using Transform;

    public interface ISnipMaker
    {
        void WithSnipRequest(IRequestWebSnippets snipRequest);
        void WithTransformSnippet(ITransformWebContentToWebSnippets transformWebContentToWebSnippets);

        WebSnippet GetSnippetFor(Uri uri);
        SnipMaker WithBaseUrl(Uri uri);
    }
}