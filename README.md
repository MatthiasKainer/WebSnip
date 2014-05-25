WebSnip
=======

A small library that allows to load parts of websites to display a preview of it. Like Facebook's preview in the chat, but not as sophisticated I guess.

## Install

    PM> Install-Package WebSnip

## Quickstart in Razor

    @Html.Raw(new SnipMaker()
            .GetSnippetFor("http://www.amazon.de/Salutoo-Skin-f%C3%BCr-Iphone-Metal/dp/B00APJA9UC/")
            .ToHtml());

## Quickstart in JSON

Add the following handler registration to your web.config: 

    <system.webServer>
      <handlers>
        <add name="WebSnip" path="websnip.axd" verb="GET" type="WebSnip.HttpHandler, WebSnip, Version=1.0.0.0, Culture=neutral" preCondition="integratedMode" />
      </handlers>
    </system.webServer>

Now you can get a JSON result by calling the new endpoint: 

> http://localhost:2822/websnip.axd/get?for=http://www.amazon.de/Salutoo-Skin-f%C3%BCr-Iphone-Metal/dp/B00APJA9UC/

Will thus return the following JSON: 

    {
      "success": true,
      "data": 
      {
        "Text": "Salutoo Skin für Iphone 5 - Metal: Amazon.de: Elektronik",
        "Image": "<img src=\"http://g-ecx.images-amazon.com/images/G/03/gno/sprites/global-sprite-v1._V339352741_.png\" alt=\"no alternate text provided\" title=\"no alternate text provided\" class=\"websnippets-image\" />",
        "Attribute": "Salutoo Skin für Iphone 5 - Metal: Amazon.de: Elektronik"
      }
    }

## Usage

### WebSnippet

You can see a more complete usage example in the samples project. But for a start: 

A _WebSnippet_ is the one object that's really important here. It consists of renderers, a template and a the url it originates from. So the WebSnippet basically looks like this: 

    WebSnippet
        Uri ForUrl
        ICanDoTemplatingForWebSnippets Template
        List<IRenderToHtml> Renderers

You will never get to see the Renderers or Template, so this is more an fyi. In most cases you'll just want to get the content of the WebSnippet, and this is done by calling the method _ToHtml()_

    string WebSnippet.ToHtml()

### SnipMaker

The *creation* of the WebSnippet is done using the _SnipMaker_. The SnipMaker can take a _IRequestWebSnippets_ and a _ITransformWebContentToWebSnippets_. The first is processing the request and loads the content as string. It can be overridden to add stuff like authentication to the request. The latter is transforming the output from the first to a WebSnippet. 
To apply all of this, call _GetSnippetFor(uri)_ and you will receive a WebSnippet for the provided Uri. 

A SnipMaker for Amazon would look something like this: 

    var snipMaker = new SnipMaker(new WebSnippetRequest(),
                new TransformBuilder().Using(TagSetFactoryFor<Amazon>.Get()).Build());
    snipMaker.GetSnippetFor(uri);
    
### TransformBuilder

The default implementation for ITransformWebContentForUrl is very flexible, and can be used in a lot of scenarios. By adding "RenderSets" to it, you can specify how the result should be rendered. Let's look at a example. 

We want to get a snippet with the fullname and the username from a twitterpage:

    var tagSet = new RenderSet
        {
            {new TagBuilder("h1").WithCssClass("ProfileHeaderCard-name"), Render.A<Text>().WithName("name")},
            {new TagBuilder("img").WithCssClass("ProfileAvatar-image"), Render.A<Image>().WithName("image")}
        };
    return new TransformBuilder().Using(tagSet).Build();
    
First we create two entries in a Dictionary. The Key is the tag that we search for on the page. We want the h1 and img tag, and find them by css class. As renderer we use the TextRenderer - it takes the InnerText of an element and image renderer - it, well, takes the image. 

### Showing it on the page

There are two ways to display the output on the page. The first is very intuitiv: 

    @model WebSnip.WebSnippet
    @Html.Raw(Model.ToHtml())
    
But this cannot be customized. To have more control you can request the fields for the item: 

    @model WebSnip.WebSnippet
    <h1>@Model.GetRenderedPartByName("name")</h1>
    <img src="@Model.GetRenderedPartByName("image")" alt="@Model.GetRenderedPartByName("name")" />
    
