namespace WebSnip.Tests.Renderer
{
    using System;
    using Machine.Fakes;
    using Machine.Specifications;
    using Render;

    public class TextRendererSpecs : WithSubject<Text>
    {
        public class When_Trying_To_Render_An_Empty_Content
        {
            Because of = () => _exception = Catch.Exception(() => Subject.WithContent(_forUrl, string.Empty));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Trying_To_Render_A_Content_Without_Html
        {
            Establish context = () => Subject.WithContent(_forUrl, _expectedResult);

            Because of = () => _result = Subject.Render();

            It should_return_the_complete_text = () => _result.ShouldEqual(_expectedResult);
        }

        public class When_Trying_To_Render_A_Content_With_Html
        {
            Establish context = () =>
                {
                    var content = string.Format("<p>{0}</p>", _expectedResult);
                    Subject.WithContent(_forUrl, content);
                };

            Because of = () => _result = Subject.Render();

            It should_return_the_inner_text = () => _result.ShouldEqual(_expectedResult);
        }

        static string _expectedResult = "expectedResult";
        static string _result;
        static Exception _exception;
        static Uri _forUrl = new Uri("http://www.url.com");
    }
}
