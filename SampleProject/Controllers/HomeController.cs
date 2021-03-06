﻿using System.Web.Mvc;

namespace SampleProject.Controllers
{
    using System;
    using WebSnip;
    using WebSnip.HtmlUtils;
    using WebSnip.Render;
    using WebSnip.Transform;
    using WebSnip.Transform.Websites;
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
                new TransformBuilder().Using(TagSetFactoryFor<Amazon>.Get()).Build());
            return PartialView("ShowSnippet", snipMaker.GetSnippetFor(uri));
        }

        public ActionResult LoadTwitterUrl()
        {
            var uri = new Uri("https://twitter.com/MatKainer/");
            var snipMaker = new SnipMaker(transformWebSnippet: new TransformBuilder().Using(CreateForTwitter()).Build());
            return PartialView("ShowCustomSnippet", snipMaker.GetSnippetFor(uri));
        }

        RenderSet CreateForTwitter()
        {
            var tagSet = new RenderSet();
            tagSet.Add(new TagBuilder("img").WithCssClass("ProfileAvatar-image"), Render.A<Image>().WithName("image"));
            tagSet.Add(new TagBuilder("h1").WithCssClass("ProfileHeaderCard-name"), Render.A<Text>().WithName("name"));
            tagSet.Add(new TagBuilder("h2").WithCssClass("ProfileHeaderCard-screenname"), Render.A<Text>().WithName("user"));
            tagSet.Add(new TagBuilder("p").WithCssClass("ProfileHeaderCard-bio"), Render.A<Text>().WithName("bio"));
            tagSet.Add(new TagBuilder("div").WithCssClass("ProfileCanopy-header"), Render.A<Attribute>("style").WithName("background"));
            return tagSet;
        }
    }
}
