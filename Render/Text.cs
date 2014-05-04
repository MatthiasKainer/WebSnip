namespace WebSnip.Render
{
    public class Text : BaseRenderer
    {
        public override string Render()
        {
            return Html.DocumentNode.InnerText;
        }
    }
}
