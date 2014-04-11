namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;

    public static class DefaultTagSetFactory
    {
        public static Dictionary<TagBuilder, IRenderToHtml> CreateForAmazon()
        {
            var dict = new Dictionary<TagBuilder, IRenderToHtml>();
            dict.Add(new TagBuilder("h1"), new TagRenderer());
            dict.Add(new TagBuilder("img").WithCssClass("thumb0"), new ImageRenderer());
            dict.Add(new TagBuilder("div").WithId("technicalProductFeaturesATF"), new HtmlRenderer());
            return dict;
        }
    }
}