namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;

    public class Amazon : IAmAWebsite
    {
        public Dictionary<TagBuilder, IRenderToHtml> Get()
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>
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