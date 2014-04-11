namespace WebSnip.Render
{
    using System;
    using HtmlAgilityPack;

    public class ImageRenderer : BaseRenderer
    {
        public override string Render()
        {
            var image = Html.DocumentNode.FirstChild;
            if (image.Name.ToUpper() != "IMG") return string.Empty;

            var imageSrc = image.Attributes["src"];
            if (imageSrc == null) return string.Empty;

            Uri imageUrl;
            if (!Uri.TryCreate(imageSrc.Value, UriKind.RelativeOrAbsolute, out imageUrl))
            {
                return string.Empty;
            }

            if (!imageUrl.IsAbsoluteUri)
            {
                imageUrl = new Uri(CurrentUrl, imageUrl);
            }

            var alternateText = image.Attributes["alt"];
            var title = image.Attributes["title"];

            if (alternateText == null || string.IsNullOrWhiteSpace(alternateText.Value))
            {
                if (title != null) alternateText = Html.CreateAttribute("alt", title.Value);
                else alternateText = Html.CreateAttribute("alt", "no alternate text provided");
            }

            if (title == null || string.IsNullOrWhiteSpace(title.Value))
            {
                if (alternateText != null) title = Html.CreateAttribute("title", alternateText.Value);
            }

            return string.Format("<img src=\"{0}\" alt=\"{1}\" title=\"{2}\" class=\"websnippets-image\" />",
                imageUrl, alternateText.Value, title.Value);
        }
    }
}
