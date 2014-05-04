namespace WebSnip.Transform
{
    using System;

    public interface ITransformWebContentForUrl
    {
        bool CanTransform(Uri url);

        ITransformWebContentForUrl With(RenderSet set);

        WebSnippet Transform(Uri forUrl, string webContent);
    }
}