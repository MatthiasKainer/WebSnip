namespace WebSnip.Tests
{
    using System;
    using System.Collections.Generic;
    using HtmlUtils;
    using Machine.Fakes;
    using Machine.Specifications;
    using Render;
    using Transform;

    public class DefaultTransformWebContentSpecs : WithSubject<DefaultTransformWebContent>
    {
        public class When_Transforming_An_Empty_Content
        {
            Because of = () => _exception = Catch.Exception(() => Subject.Transform(_forUrl, String.Empty));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Transforming_Content_With_Only_Spaces
        {
            Because of = () => _exception = Catch.Exception(() => Subject.Transform(_forUrl, String.Empty));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Transforming_Content_With_A_Tag_That_Is_Registered
        {
            Establish context = () =>
            {
                _renderToHtml = An<IRenderToHtml>();
                _renderToHtml.WhenToldTo(_ => _.WithContent(_forUrl, Param<string>.IsAnything)).Return(_renderToHtml);
                var renderSet = new RenderSet() { { new TagBuilder("h1"), _renderToHtml } };
                Configure(x => x.For<RenderSet>().Use(() => renderSet));
            };

            Because of = () => _result = Subject.Transform(_forUrl, "<body><h1>my heading</h1><p>some text</p></body>");

            It should_return_a_result_with_a_renderer = () => _result.CurrentRenderers.ShouldEqual(1);

            static IRenderToHtml _renderToHtml;
        }

        static WebSnippet _result;

        static Exception _exception;
        static Uri _forUrl = new Uri("http://www.url.com");
    }
}
