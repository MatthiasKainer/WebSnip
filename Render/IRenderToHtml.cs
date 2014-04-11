namespace WebSnip.Render
{
    public interface IRenderToHtml
    {
        string FullContent { get; }
        string Name { get; }
        IRenderToHtml WithContent(string content);
        string Render();
    }
}