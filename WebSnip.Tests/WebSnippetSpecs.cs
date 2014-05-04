namespace WebSnip.Tests
{
    using System;
    using Machine.Fakes;
    using Machine.Specifications;
    using Render;
    using Templating;

    public class WebSnippetSpecs : WithSubject<WebSnippet>
    {
        Establish context = () =>
            {
                Configure<ICanDoTemplatingForWebSnippets>(new EmptyTemplate());
                Configure<Uri>(new Uri("http://www.anurl.com"));
            };

        public class When_Adding_An_Empty_Renderer
        {
            Because of = () => _exception = Catch.Exception(() => Subject.AddRenderer(null));

            It should_have_thrown_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();

            static Exception _exception;
        }

        public class When_Adding_A_Valid_Renderer
        {
            Because of = () => Subject.AddRenderer(An<IRenderToHtml>());

            It should_have_added_the_renderer = () => Subject.CurrentRenderers.ShouldEqual(1);
        }

        public class When_Transforming_Content_With_A_Tag_That_Is_Registered
        {
            Establish context = () =>
                {
                    _renderToHtml = An<IRenderToHtml>();
                    _expectedResult = "expected result";
                    _renderToHtml.WhenToldTo(_ => _.Render()).Return(_expectedResult);
                    Subject.AddRenderer(_renderToHtml);
                };

            Because of = () => _result = Subject.ToHtml();

            It should_have_used_the_renderer = () => _renderToHtml.WasToldTo(_ => _.Render()).OnlyOnce();

            It should_return_the_correct_result = () => _result.ShouldEqual(_expectedResult);

            static IRenderToHtml _renderToHtml;
            static string _result;
            static string _expectedResult;
        }
        
        public class When_Transforming_Content_With_The_Empty_Template
        {
            Establish context = () => Subject.WithTemplate(new EmptyTemplate());

            Because of = () => _result = Subject.ToHtml();

            It should_return_the_correct_result = () => _result.ShouldEqual(string.Empty);

            static string _result;
        }

        public class When_Transforming_Content_With_The_Default_Template
        {
            Establish context = () => Subject.WithTemplate(new DefaultTemplate());

            Because of = () => _result = Subject.ToHtml();

            It should_return_the_correct_result = () => _result.ShouldEqual(_expectedResult);

            static string _result;
            static string _expectedResult = "<div class=\"websnippets-container\"></div>";
        }

        public class When_Requesting_A_Rendered_Part_By_Name
        {
            Establish context = () =>
            {
                _renderToHtml = An<IRenderToHtml>();
                _renderToHtml.WhenToldTo(_ => _.Name).Return("name");
                _renderToHtml.WhenToldTo(_ => _.Render()).Return(ExpectedResult);
                Subject.AddRenderer(_renderToHtml);
            };

            Because of = () => _result = Subject.GetRenderedPartByName("name");

            It should_return_the_correct_result = () => _result.ShouldEqual(ExpectedResult);

            static string _result;
            static IRenderToHtml _renderToHtml;
            const string ExpectedResult = "expected result";
        }
    }
}
