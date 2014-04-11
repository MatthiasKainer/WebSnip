namespace WebSnip.Request
{
    using System;
    using System.Net;

    public class WebSnippetRequest : IRequestWebSnippets
    {
        public string GetContent(Uri uri)
        {
            using (var webRequest = new WebClient())
            {
                return webRequest.DownloadString(uri);
            }
        }
    }
}