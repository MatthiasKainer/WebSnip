namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;

    public class RenderSet : Dictionary<TagBuilder, IRenderToHtml>
    {
    }
}