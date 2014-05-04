WebSnip
=======

A small library that allows to load parts of websites to display a preview of it. Like Facebook's preview in the chat, but not as sophisticated I guess.

## Install

    PM> Install-Package WebSnip

## Quickstart

    public string GetContent(Uri uri) {
        return new SnipMaker()
            .GetSnippetFor(uri)
            .ToHtml();
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
    
### DefaultTransformWebContent

The default implementation for ITransformWebContentForUrl is very flexible, and can be used in a lot of scenarios. By adding "RenderSets" to it, you can specify how the result should be rendered. Let's look at a example. 

We want to get a snippet with the fullname and the username from a twitterpage:

    var tagSet = new Dictionary<TagBuilder, IRenderToHtml>
        {
            {new TagBuilder("h1").WithCssClass("ProfileHeaderCard-name"), Render.A<Text>().WithName("name")},
            {new TagBuilder("img").WithCssClass("ProfileAvatar-image"), Render.A<Image>().WithName("image")}
        };
    return tagSet;
    
First we create two entries in a Dictionary. The Key is the tag that we search for on the page. We want the h1 and img tag, and find them by css class. As renderer we use the TextRenderer - it takes the InnerText of an element and image renderer - it, well, takes the image. 
