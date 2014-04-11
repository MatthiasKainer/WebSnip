namespace WebSnip.Render
{
    public class TagRenderer : BaseRenderer
    {
        public override string Render()
        {
            return string.Format("<{0}>{1}</{0}>", Html.DocumentNode.FirstChild.Name, Html.DocumentNode.InnerText);
        }
    }
}