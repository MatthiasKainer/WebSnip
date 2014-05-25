namespace WebSnip.Transform
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Websites;

    public class TagSetFactory : ITagSetFactory
    {
        static readonly List<IAmAWebsite> WebSiteList = new List<IAmAWebsite>();

        private static IEnumerable<IAmAWebsite> Websites
        {
            get
            { 
                if (!WebSiteList.Any())
                {
                    var types = typeof (TagSetFactory).Assembly.GetTypes().Where(_ => _.IsClass && _.IsAssignableFrom(typeof (IAmAWebsite)) && _ != typeof(DefaultWebsite));
                    foreach (var type in types)
                    {
                        var webSiteConstructor = type.GetConstructor(Type.EmptyTypes);
                        if (webSiteConstructor == null) throw new Exception(string.Format("WebSite of type {0} has no empty constructor", type));

                        var website = (IAmAWebsite) (webSiteConstructor.Invoke(null));
                        WebSiteList.Add(website);
                    }

                    WebSiteList.Add(new DefaultWebsite());
                }

                return WebSiteList;
            }
        }

        public RenderSet GetFor(Uri uri)
        {
            return Websites.First(_ => _.OptimizedFor(uri)).Get();
        }
    }
}