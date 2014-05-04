namespace WebSnip.Transform.Websites
{
    using HtmlUtils;
    using Render;

    public class DefaultWebsite : IAmAWebsite
    {
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