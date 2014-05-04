namespace WebSnip.Render
{
    using System;

    public class Attribute : BaseRenderer
    {
        readonly string attribute;

        public Attribute(string attribute)
        {
            if (attribute == null) throw new ArgumentNullException("attribute");
            this.attribute = attribute;
        }

        public override string Render()
        {
            return Html.DocumentNode.FirstChild.Attributes[attribute] != null
                       ? Html.DocumentNode.FirstChild.Attributes[attribute].Value
                       : string.Empty;
        }
    }
}
