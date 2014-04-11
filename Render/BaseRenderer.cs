﻿namespace WebSnip.Render
{
    using System;
    using HtmlAgilityPack;

    public abstract class BaseRenderer : IRenderToHtml
    {
        protected HtmlDocument Html { get; private set; }

        public string FullContent { get; private set; }

        public string Name
        {
            get
            {
                if (Has("name")) return Get("name");

                if (Has("id")) return Get("id");

                return GetType().Name;
            }
        }

        public IRenderToHtml WithContent(string webContent)
        {
            if (string.IsNullOrWhiteSpace(webContent)) throw new ArgumentNullException("webContent");

            FullContent = webContent;
            Html = new HtmlDocument();
            Html.LoadHtml(FullContent);
            return this;
        }

        public abstract string Render();

        private bool Has(string name)
        {
            return Html.DocumentNode.Attributes[name] != null;
        }

        private string Get(string name)
        {
            return Html.DocumentNode.Attributes[name].Value;
        }
    }
}