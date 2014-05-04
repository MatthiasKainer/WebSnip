namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;

    public class DefaultWebsite : IAmAWebsite
    {
        public Dictionary<TagBuilder, IRenderToHtml> Get()
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>
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