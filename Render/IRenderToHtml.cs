namespace WebSnip.Render
{
    using System;

    public interface IRenderToHtml
    {
        string FullContent { get; }
        string Name { get; }
        IRenderToHtml WithContent(Uri forUrl, string content);
        string Render();
    }
}