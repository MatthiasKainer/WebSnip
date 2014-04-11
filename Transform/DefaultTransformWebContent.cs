namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlUtils;
    using Render;

    public class DefaultTransformWebContent : ITransformWebContentForUrl
    {
        readonly IDictionary<TagBuilder, IRenderToHtml> renderSet;

        public DefaultTransformWebContent(IDictionary<TagBuilder, IRenderToHtml> renderSet)
        {
            this.renderSet = renderSet;
        }

        public bool CanTransform(Uri url)
        {
            return true;
        }

        public WebSnippet Transform(Uri forUrl, string webContent)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            var webSnippet = new WebSnippet().WithFullContent(webContent);

            return renderSet.Where(renderItem => webContent.HasTag(renderItem.Key))
                .Aggregate(webSnippet, (current, tagSet) => current.AddRenderer(renderSet[tagSet.Key].WithContent(forUrl, webContent.GetFullTag(tagSet.Key))));
        }
    }
}
