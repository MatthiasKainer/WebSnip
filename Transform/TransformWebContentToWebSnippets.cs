namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TransformWebContentToWebSnippets : ITransformWebContentToWebSnippets
    {
        readonly IEnumerable<ITransformWebContentForUrl> customTransformers;
        readonly ITransformWebContentForUrl defaultTransformer;

        public TransformWebContentToWebSnippets(
            ITransformWebContentForUrl defaultTransformer = null,
            IEnumerable<ITransformWebContentForUrl> customTransformers = null)
        {
            this.customTransformers = customTransformers ?? new List<ITransformWebContentForUrl>();
            this.defaultTransformer = defaultTransformer ?? new DefaultTransformWebContent();
        }

        public WebSnippet ToWebSnippet(Uri forUrl, string webContent)
        {
            if (forUrl == null) throw new ArgumentNullException("forUrl");
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");

            ITransformWebContentForUrl transformer;
            if ((transformer = customTransformers.FirstOrDefault(_ => _.CanTransform(forUrl))) == null)
                transformer = defaultTransformer;

            return transformer.Transform(forUrl, webContent);
        }
    }
}