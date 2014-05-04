using System.Web.Mvc;

namespace SampleProject.Controllers
{
    using System;
    using System.Collections.Generic;
    using WebSnip;
    using WebSnip.HtmlUtils;
    using WebSnip.Render;
    using WebSnip.Request;
    using WebSnip.Transform;
    using Attribute = WebSnip.Render.Attribute;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadUrl(string url)
        {
            var snipMaker = new SnipMaker();
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                ModelState.AddModelError("", "Invalid url");
                PartialView("ShowSnippet", new WebSnippet(uri));
            }

            return PartialView("ShowSnippet", snipMaker.GetSnippetFor(uri));
        }

        public ActionResult LoadAmazonUrl()
        {
            var uri = new Uri("http://www.amazon.de/Salutoo-Skin-f%C3%BCr-Iphone-Metal/dp/B00APJA9UC/");
            var snipMaker = new SnipMaker(transformWebSnippet:
                new TransformWebContentToWebSnippets(new DefaultTransformWebContent(TagSetFactoryFor<Amazon>.Get())));
            return PartialView("ShowSnippet", snipMaker.GetSnippetFor(uri));
        }

        public ActionResult LoadTwitterUrl()
        {
            var uri = new Uri("https://twitter.com/MatKainer/");
            var snipMaker = new SnipMaker(transformWebSnippet: new TransformWebContentToWebSnippets(CreateForTwitter()));
            return PartialView("ShowCustomSnippet", snipMaker.GetSnippetFor(uri));
        }

        ITransformWebContentForUrl CreateForTwitter()
        {
            var tagSet = new Dictionary<TagBuilder, IRenderToHtml>();
            tagSet.Add(new TagBuilder("img").WithCssClass("ProfileAvatar-image"), new Image().WithName("image"));
            tagSet.Add(new TagBuilder("h1").WithCssClass("ProfileHeaderCard-name"), new Text().WithName("name"));
            tagSet.Add(new TagBuilder("h2").WithCssClass("ProfileHeaderCard-screenname"), new Text().WithName("user"));
            tagSet.Add(new TagBuilder("p").WithCssClass("ProfileHeaderCard-bio"), new Text().WithName("bio"));
            tagSet.Add(new TagBuilder("div").WithCssClass("ProfileCanopy-header"), new Attribute("style").WithName("background"));
            return new DefaultTransformWebContent(tagSet);
        }
    }
}
