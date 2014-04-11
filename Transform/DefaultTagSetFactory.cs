namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;

    public static class DefaultTagSetFactory
    {
        public static Dictionary<TagBuilder, IRenderToHtml> Create()
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>();
            dict.Add(new TagBuilder("title"), new TextRenderer());
            dict.Add(new TagBuilder("img"), new ImageRenderer());
            dict.Add(new TagBuilder("meta").WithName("description"), new AttributeRenderer("content"));
            return dict;
        }

        public static Dictionary<TagBuilder, IRenderToHtml> CreateForAmazon(byte withHowManyThumbnails = 4)
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>();
            dict.Add(new TagBuilder("h1"), new TagRenderer());
            for (byte i = 0; i < withHowManyThumbnails; i++)
            {
                dict.Add(new TagBuilder("img").WithCssClass("thumb" + i), new ImageRenderer());
            }
            dict.Add(new TagBuilder("div").WithId("productDescription"), new HtmlRenderer());
            return dict;
        }
    }
}