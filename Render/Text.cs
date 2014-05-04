namespace WebSnip.Render
{
    public static class Render
    {
        public static T A<T>(params object[] content)
        {
            return default(T);
        }
    }

    public class Text : BaseRenderer
    {
        public override string Render()
        {
            return Html.DocumentNode.InnerText;
        }
    }
}
