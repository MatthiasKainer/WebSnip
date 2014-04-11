namespace WebSnip.Transform
{
    using System.Collections.Generic;
    using Render;

    public static class DefaultTagSetFactory
    {
        public static Dictionary<string, IRenderToHtml> Create()
        {
            return new Dictionary<string, IRenderToHtml>();
        }
    }
}