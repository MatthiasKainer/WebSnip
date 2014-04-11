namespace WebSnip.Render
{
    public class TextRenderer : BaseRenderer
    {
        public override string Render()
        {
            return Html.DocumentNode.InnerText;
        }
    }
}
