namespace WebSnip.Transform.Websites
{
    using HtmlUtils;
    using Render;

    public class Amazon : IAmAWebsite
    {
        public RenderSet Get()
        {
            var dict = new RenderSet
                {
                    {new TagBuilder("h1"), new TagRenderer()},
                    {new TagBuilder("div").WithId("productDescription"), new HtmlRenderer()}
                };

            for (byte i = 0; i < 4; i++)
            {
                dict.Add(new TagBuilder("img").WithCssClass("thumb" + i), new Image());
            }
            return dict;
        }
    }
}