namespace WebSnip.HtmlUtils
{
    using System;
    using System.Linq;
    using HtmlAgilityPack;

    public static class Html
    {
        public static bool HasTag(this string webContent, string tag)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            if (string.IsNullOrWhiteSpace(tag)) throw new ArgumentNullException("tag");

            var html = new HtmlDocument();
            html.LoadHtml(webContent);

            return html.DocumentNode.SelectNodes(string.Concat("//", tag)) != null && html.DocumentNode.SelectNodes(string.Concat("//", tag)).Any();
        }

        public static string GetFullTag(this string webContent, string tag)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");
            if (string.IsNullOrWhiteSpace(tag)) throw new ArgumentNullException("tag");

            var html = new HtmlDocument();
            html.LoadHtml(webContent);

            return html.DocumentNode.SelectNodes(string.Concat("//", tag)) != null && html.DocumentNode.SelectNodes(string.Concat("//", tag)).Any() ? 
                html.DocumentNode.SelectNodes(string.Concat("//", tag)).First().OuterHtml : 
                string.Empty;
        }
    }
}
