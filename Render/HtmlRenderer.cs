namespace WebSnip.Render
{
    public class HtmlRenderer : BaseRenderer
    {
        public override string Render()
        {
            return Html.DocumentNode.InnerHtml;
        }
    }
}
