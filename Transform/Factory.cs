namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using HtmlUtils;
    using Render;
    using Utils.SyntaticSugar.Switch;
    using Websites;

    public static class Factory
    {
        public static Dictionary<TagBuilder, IRenderToHtml> CreateFor(Uri uri)
        {
            Dictionary<TagBuilder, IRenderToHtml> dict = null;
            Switch<string>.With(uri.Host.ToUpperInvariant())
                .Case(host => HostMatch(host, "amazon"), host => dict = TagSetFactoryFor<Amazon>.Get())
                .Default(host => dict = TagSetFactoryFor<DefaultWebsite>.Get());
            return dict;
        }

        static bool HostMatch(string host, string search)
        {
            search = search.ToUpperInvariant();
            return host.StartsWith(string.Format("{0}.", search)) || 
                host.Contains(string.Format(".{0}.", search));
        }
    }
}