namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using HtmlUtils;
    using Render;
    using Websites;

    public class TransformWebSnippet
    {
        ITransformWebContentForUrl transformWebContentForUrl;
        IDictionary<TagBuilder, IRenderToHtml> renderSetFor;

        public TransformWebSnippet Using(ITransformWebContentForUrl transformWebContent)
        {
            transformWebContentForUrl = transformWebContent;
            return this;
            //return new TransformWebContentToWebSnippets(new DefaultTransformWebContent(TagSetFactoryFor<Amazon>.Get())))
        }

        public TransformWebSnippet Using(IDictionary<TagBuilder, IRenderToHtml> renderSet)
        {
            renderSetFor = renderSet;
            return this;
            //return new TransformWebContentToWebSnippets(new DefaultTransformWebContent(TagSetFactoryFor<Amazon>.Get())))
        }

        public ITransformWebContentToWebSnippets Create()
        {
            if (transformWebContentForUrl == null) transformWebContentForUrl = new DefaultTransformWebContent();
            
            return new TransformWebContentToWebSnippets(transformWebContentForUrl);
        }
    }

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
                .Aggregate(webSnippet, (current, tagSet) => current.AddRenderer(renderSet[tagSet.Key].WithContent(forUrl, webContent.GetFullTag(tagSet.Key))));
        }
    }
}
