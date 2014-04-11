namespace WebSnip
{
    using System;
    using System.Collections.Generic;
    using Render;
    using Templating;

    public class WebSnippet
    {
        readonly List<IRenderToHtml> registedRenderers;

        protected ICanDoTemplatingForWebSnippets Template { get; private set; }

        public int CurrentRenderers { get { return registedRenderers.Count;  } }

        public string FullContent { get; private set; }

        public WebSnippet()
            : this(new EmptyTemplate())
        {
        }

        public WebSnippet(ICanDoTemplatingForWebSnippets template)
        {
            registedRenderers = new List<IRenderToHtml>();
            Template = template;
        }

        public WebSnippet WithTemplate(ICanDoTemplatingForWebSnippets template)
        {
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
    }
}