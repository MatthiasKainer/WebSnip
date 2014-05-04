namespace WebSnip.Transform
{
    using System;
    using Websites;

    public static class TagSetFactoryFor<TWebSite> where TWebSite : IAmAWebsite
    {
        public static RenderSet Get()
        {
            var type = typeof (TWebSite);
            var webSiteConstructor = type.GetConstructor(Type.EmptyTypes);
            if (webSiteConstructor == null) throw new Exception(string.Format("WebSite of type {0} has no empty constructor", type));
            
            var website = (TWebSite) webSiteConstructor.Invoke(null);
            return website.Get();
        }
    }
}