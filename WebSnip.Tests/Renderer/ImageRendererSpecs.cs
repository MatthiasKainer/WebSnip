namespace WebSnip.Tests.Renderer
{
    using System;
    using Machine.Fakes;
    using Machine.Specifications;
    using Render;

    public class ImageRendererSpecs : WithSubject<Image>
    {
        public class When_Trying_To_Render_An_Empty_Content
        {
            Because of = () => _exception = Catch.Exception(() => Subject.WithContent(_forUrl, string.Empty));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Trying_To_Render_A_Content_That_Is_Not_An_Image
        {
            Establish context = () => Subject.WithContent(_forUrl, "no image");

            Because of = () => _result = Subject.Render();

            It should_return_an_empty_result = () => _result.ShouldEqual(string.Empty);
        }

        public class When_Trying_To_Render_A_Content_With_A_Relative_Image
        {
            Establish context = () => Subject.WithContent(_forUrl, "<img src=\"expectedResult\" alt=\"lala\" />");

            Because of = () => _result = Subject.Render();

            It should_return_correct_image = () => _result.ShouldEqual(_expectedResult);
        }

        public class When_Trying_To_Render_A_Content_With_An_Absolute_Image
        {
            Establish context = () => Subject.WithContent(new Uri("http://www.other.com"), "<img src=\"http://www.url.com/expectedResult\" alt=\"lala\" />");

            Because of = () => _result = Subject.Render();

            It should_return_correct_image = () => _result.ShouldEqual(_expectedResult);
        }

        static string _expectedResult = "<img src=\"http://www.url.com/expectedResult\" alt=\"lala\" title=\"lala\" class=\"websnippets-image\" />";
        static string _result;
        static Exception _exception;
        static Uri _forUrl = new Uri("http://www.url.com");
    }
}