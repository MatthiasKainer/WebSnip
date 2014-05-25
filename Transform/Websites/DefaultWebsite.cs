namespace WebSnip.Transform.Websites
{
    using System;
    using HtmlUtils;
    using Render;
    using Attribute = Render.Attribute;

    public class DefaultWebsite : IAmAWebsite
    {
        public bool OptimizedFor(Uri uri)
        {
            return true;
        }

        public RenderSet Get()
        {
            var dict = new RenderSet
                {
                    {TagBuilder.Create("title"), Render.A<Text>()},
                    {TagBuilder.Create("img"), Render.A<Image>()},
                    {TagBuilder.Create("meta").WithName("description"), 
                        Render.A<Attribute>("content")}
                };
            return dict;
        }
    }
}