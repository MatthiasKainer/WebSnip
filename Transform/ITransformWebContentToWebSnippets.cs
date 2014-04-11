namespace WebSnip.Transform
{
    using System;

    public interface ITransformWebContentToWebSnippets
    {
        WebSnippet ToWebSnippet(Uri forUrl, string webContent);
    }
}