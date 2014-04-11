namespace WebSnip.HtmlUtils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TagBuilder
    {
        protected string TagName { get; private set; }

        protected Dictionary<string, string> Attributes { get; private set; }
        protected string CssClass { get; private set; }

        public TagBuilder(string tagName)
        {
            if (string.IsNullOrWhiteSpace(tagName)) throw new ArgumentNullException("tag");

            WithTagName(tagName);
            Attributes = new Dictionary<string, string>();
        }

        public TagBuilder WithTagName(string name)
        {
            TagName = name;
            return this;
        }

        public TagBuilder WithName(string name)
        {
            Attributes.Add("name", name);
            return this;
        }

        public TagBuilder WithId(string value)
        {
            Attributes.Add("id", value);
            return this;
        }

        public TagBuilder WithCssClass(string classname)
        {
            CssClass = classname;
            return this;
        }

        public string Build()
        {
            var attributes = Attributes.Select(_ => string.Format("@{0}='{1}'", _.Key, _.Value)).ToList();
            if (!string.IsNullOrWhiteSpace(CssClass))
                attributes.Add(string.Format(@"contains(concat(' ', @class, ' '), ' {0} ')", CssClass));
            var attributeValues = string.Join(" and ", attributes.ToArray());
            if (!string.IsNullOrWhiteSpace(attributeValues)) attributeValues = string.Concat("[", attributeValues, "]");
            return string.Concat("//", TagName, attributeValues);
        }
    }
}