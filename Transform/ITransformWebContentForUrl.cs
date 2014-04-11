namespace WebSnip.Transform
{
    using System;

    public interface ITransformWebContentForUrl
    {
        bool CanTransform(Uri url);

        WebSnippet Transform(string webContent);
    }
}