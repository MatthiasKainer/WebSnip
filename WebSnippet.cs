namespace WebSnip
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Render;
    using Templating;

    public class WebSnippet
    {
        readonly List<IRenderToHtml> registedRenderers;

        public Uri ForUrl { get; set; }

        protected ICanDoTemplatingForWebSnippets Template { get; private set; }

        public int CurrentRenderers { get { return registedRenderers.Count;  } }

        public string FullContent { get; private set; }
        
        public WebSnippet(Uri forUrl)
            : this(forUrl, new EmptyTemplate())
        {
        }

        public WebSnippet(Uri forUrl, ICanDoTemplatingForWebSnippets template)
        {
            if (template == null) throw new ArgumentNullException("template");
            registedRenderers = new List<IRenderToHtml>();
            ForUrl = forUrl;
            Template = template;
        }

        public WebSnippet WithTemplate(ICanDoTemplatingForWebSnippets template)
        {
            if (template == null) throw new ArgumentNullException("template");
            Template = template;
            return this;
        }

        public WebSnippet WithFullContent(string content)
        {
            FullContent = content;
            return this;
        }

        public WebSnippet AddRenderer(IRenderToHtml renderer)
        {
            if (renderer == null) throw new ArgumentNullException("renderer");
            registedRenderers.Add(renderer);
            return this;
        }

        public string ToHtml()
        {
            return Template.ApplyToTemplate(registedRenderers.ToArray()).ToHtml();
        }

        public string GetRenderedPartByName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            var renderer = registedRenderers.FirstOrDefault(_ => _.Name == name);
            return renderer == null ? null : renderer.Render();
        }
    }
}