namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;

    public interface IAmAWebsite
    {
        Dictionary<TagBuilder, IRenderToHtml> Get();
    }
}