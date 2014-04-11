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
                new TransformWebContentToWebSnippets(new DefaultTransformWebContent(DefaultTagSetFactory.CreateForAmazon())));
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
            tagSet.Add(new TagBuilder("img").WithCssClass("avatar size73"), new ImageRenderer().WithName("image"));
            tagSet.Add(new TagBuilder("h1").WithCssClass("fullname"), new TextRenderer().WithName("name"));
            tagSet.Add(new TagBuilder("h2").WithCssClass("username"), new TextRenderer().WithName("user"));
            tagSet.Add(new TagBuilder("div").WithCssClass("bio-container"), new TextRenderer().WithName("bio"));
            tagSet.Add(new TagBuilder("div").WithCssClass("profile-header-inner"), new AttributeRenderer("data-background-image").WithName("background"));
            return new DefaultTransformWebContent(tagSet);
        }
    }
}
