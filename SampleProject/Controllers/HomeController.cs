using System.Web.Mvc;

namespace SampleProject.Controllers
{
    using System;
    using WebSnip;
    using WebSnip.Request;
    using WebSnip.Transform;

    public class HomeController : Controller
    {
        readonly SnipMaker snipMaker;

        public HomeController()
        {
            snipMaker = new SnipMaker(new WebSnippetRequest(), 
                new TransformWebContentToWebSnippets(new DefaultTransformWebContent(DefaultTagSetFactory.CreateForAmazon())));
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadUrl(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                ModelState.AddModelError("", "Invalid url");
                PartialView("ShowSnippet", new WebSnippet());
            }

            return PartialView("ShowSnippet", snipMaker.GetSnippetFor(uri));
        }
    }
}
