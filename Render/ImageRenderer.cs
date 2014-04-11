namespace WebSnip.Render
{
    public class ImageRenderer : BaseRenderer
    {
        public override string Render()
        {
            if (Html.DocumentNode.Name.ToUpper() != "IMG") return string.Empty;

            var imageSrc = Html.DocumentNode.Attributes["src"];
            if (imageSrc == null) return string.Empty;

            var alternateText = Html.DocumentNode.Attributes["alt"];
            var title = Html.DocumentNode.Attributes["title"];

            if (alternateText == null || string.IsNullOrWhiteSpace(alternateText.Value))
            {
                if (title != null) alternateText = title;
            }

            if (title == null || string.IsNullOrWhiteSpace(title.Value))
            {
                if (alternateText != null) title = alternateText;
            }

            return string.Format("<img src=\"{0}\" alt=\"{1}\" title=\"{2}\" class=\"websnippets-image\" />", 
                imageSrc, alternateText, title);
        }
    }
}
