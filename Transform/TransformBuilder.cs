namespace WebSnip.Transform
{
    using System;
    using Websites;

    public class TransformBuilder
    {
        ITransformWebContentForUrl transformWebContentForUrl = new DefaultTransformWebContent();
        RenderSet renderSetFor = TagSetFactoryFor<DefaultWebsite>.Get();

        public TransformBuilder Using(ITransformWebContentForUrl transformWebContent)
        {
            if (transformWebContent == null) throw new ArgumentNullException("transformWebContent");
            transformWebContentForUrl = transformWebContent;
            return this;
        }

        public TransformBuilder Using(RenderSet renderSet)
        {
            if (renderSet == null) throw new ArgumentNullException("renderSet");
            renderSetFor = renderSet;
            return this;
        }

        public ITransformWebContentToWebSnippets Build()
        {
            transformWebContentForUrl.With(renderSetFor);
            return new TransformWebContentToWebSnippets(transformWebContentForUrl);
        }
    }
}