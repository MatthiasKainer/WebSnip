namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;
    using Utils.SyntaticSugar.Switch;

    public static class DefaultTagSetFactory
    {
        public static Dictionary<TagBuilder, IRenderToHtml> CreateFor(Uri uri)
        {
            Dictionary<TagBuilder, IRenderToHtml> dict = null;
            Switch<string>.With(uri.Host.ToUpperInvariant())
                .Case(host => HostMatch(host, "amazon"), host => dict = CreateForAmazon())
                .Default(host => dict = Create());
            return dict;
        }

        static bool HostMatch(string host, string search)
        {
            search = search.ToUpperInvariant();
            return host.StartsWith(string.Format("{0}.", search)) || host.Contains(string.Format(".{0}.", search));
        }

        public static Dictionary<TagBuilder, IRenderToHtml> Create()
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>
                {
                    {new TagBuilder("title"), new TextRenderer()},
                    {new TagBuilder("img"), new ImageRenderer()},
                    {new TagBuilder("meta").WithName("description"), new AttributeRenderer("content")}
                };
            return dict;
        }

        public static Dictionary<TagBuilder, IRenderToHtml> CreateForAmazon(byte withHowManyThumbnails = 4)
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>
                {
                    {new TagBuilder("h1"), new TagRenderer()},
                    {new TagBuilder("div").WithId("productDescription"), new HtmlRenderer()}
                };

            for (byte i = 0; i < withHowManyThumbnails; i++)
            {
                dict.Add(new TagBuilder("img").WithCssClass("thumb" + i), new ImageRenderer());
            }
            return dict;
        }
    }
}