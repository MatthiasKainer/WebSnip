namespace WebSnip.HtmlUtils
{
    using System;
    using System.Linq;
    using HtmlAgilityPack;

    public static class Html
    {
        public static bool HasTag(this string webContent, TagBuilder tag)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            if (tag == null) throw new ArgumentNullException("tag");

            var html = new HtmlDocument();
            html.LoadHtml(webContent);

            var xpath = tag.Build();
            return html.DocumentNode.SelectNodes(xpath) != null && html.DocumentNode.SelectNodes(xpath).Any();
        }

        public static string GetFullTag(this string webContent, TagBuilder tag)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            if (tag == null) throw new ArgumentNullException("tag");

            var html = new HtmlDocument();
            html.LoadHtml(webContent);

            var xpath = tag.Build();
            return html.DocumentNode.SelectNodes(xpath) != null && html.DocumentNode.SelectNodes(xpath).Any() ?
                html.DocumentNode.SelectNodes(xpath).First().OuterHtml : 
                string.Empty;
        }
    }
}
