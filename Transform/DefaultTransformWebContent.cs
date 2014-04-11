namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlUtils;
    using Render;

    public class DefaultTransformWebContent : ITransformWebContentForUrl
    {
        readonly IDictionary<string, IRenderToHtml> renderSet;

        public DefaultTransformWebContent(IDictionary<string, IRenderToHtml> renderSet)
        {
            this.renderSet = renderSet;
        }

        public bool CanTransform(Uri url)
        {
            return true;
        }

        public WebSnippet Transform(string webContent)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            var webSnippet = new WebSnippet().WithFullContent(webContent);

            return renderSet.Where(tagSet => webContent.HasTag(tagSet.Key))
                .Aggregate(webSnippet, (current, tagSet) => current.AddRenderer(renderSet[tagSet.Key].WithContent(webContent.GetFullTag(tagSet.Key))));
        }
    }
}
