namespace WebSnip.Templating
{
    using System.Linq;
    using System.Text;
    using Render;

    public class EmptyTemplate : ICanDoTemplatingForWebSnippets
    {
        readonly StringBuilder builder = new StringBuilder();

        public ICanDoTemplatingForWebSnippets ApplyToTemplate(params IRenderToHtml[] renderers)
        {
            renderers.ToList().ForEach(_ => builder.Append(_.Render()));
            return this;
        }

        public string ToHtml()
        {
            return builder.ToString();
        }
    }
}