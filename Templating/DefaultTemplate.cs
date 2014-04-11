namespace WebSnip.Templating
{
    using System.Linq;
    using System.Text;
    using Render;

    public class DefaultTemplate : ICanDoTemplatingForWebSnippets
    {
        readonly StringBuilder builder = new StringBuilder();

        public DefaultTemplate()
        {
            builder.Append("<div class=\"websnippets-container\">");
        }

        public ICanDoTemplatingForWebSnippets ApplyToTemplate(params IRenderToHtml[] renderers)
        {
            renderers.ToList().ForEach(_ => builder.Append(_.Render()));
            return this;
        }

        public string ToHtml()
        {
            builder.Append("</div>");
            return builder.ToString();
        }
    }
}