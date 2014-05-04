namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlUtils;
    using Render;
    using Websites;

    public class DefaultTransformWebContent : ITransformWebContentForUrl
    {
        RenderSet renderSet;

        public DefaultTransformWebContent(RenderSet renderSet = null)
        {
            this.renderSet = renderSet ?? TagSetFactoryFor<DefaultWebsite>.Get();
        }

        public bool CanTransform(Uri url)
        {
            return true;
        }

        public ITransformWebContentForUrl With(RenderSet set)
        {
            renderSet = set;
            return this;
        }

        public WebSnippet Transform(Uri forUrl, string webContent)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            var webSnippet = new WebSnippet(forUrl).WithFullContent(webContent);

            return renderSet.Where(renderItem => webContent.HasTag(renderItem.Key))
                .Aggregate(webSnippet, (current, renderItem) => current.AddRenderer(renderSet[renderItem.Key].WithContent(forUrl, webContent.GetFullTag(renderItem.Key))));
        }
    }
}
