namespace WebSnip.Templating
{
    using Render;

    public interface ICanDoTemplatingForWebSnippets
    {
        ICanDoTemplatingForWebSnippets ApplyToTemplate(params IRenderToHtml[] renderers);

        string ToHtml();
    }
}