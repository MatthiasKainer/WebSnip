namespace WebSnip.Tests
{
    using System;
    using HtmlUtils;
    using Machine.Specifications;

    public class HtmlUtilsSpecs
    {
        public class When_Trying_To_Check_The_Existance_Of_A_Tag
        {
            public class When_Searching_For_An_Empty_Content
            {
                Because of = () => _exception = Catch.Exception(() => string.Empty.HasTag(new TagBuilder("tag")));

                It should_throw_an_exception = () => _exception.ShouldNotBeNull();

                It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
            }

            public class When_Searching_For_An_Empty_Tag
            {
                Because of = () => _exception = Catch.Exception(() => "no tag in content".HasTag(new TagBuilder(string.Empty)));

                It should_throw_an_exception = () => _exception.ShouldNotBeNull();

                It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
            }

            public class When_Searching_A_String_Without_The_Tag
            {
                Because of = () => _result = "no tag inside".HasTag(new TagBuilder("p"));

                It should_tell_us_it_has_not_found_the_tag = () => _result.ShouldEqual(false);
            }

            public class When_Searching_A_Good_String
            {
                Because of = () => _result = HtmlContent.HasTag(new TagBuilder("p"));

                It should_tell_us_it_has_found_the_tag = () => _result.ShouldEqual(true);
            }

            const string HtmlContent = "with <p>with content</p> in content";
            static bool _result;
            static Exception _exception;
        }

        public class When_Trying_To_Get_The_Content_Of_A_Tag
        {
            public class When_Searching_For_An_Empty_Content
            {
                Because of = () => _exception = Catch.Exception(() => string.Empty.GetFullTag(new TagBuilder("tag")));

                It should_throw_an_exception = () => _exception.ShouldNotBeNull();

                It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
            }

            public class When_Searching_For_An_Empty_Tag
            {
                Because of = () => _exception = Catch.Exception(() => "no tag in content".GetFullTag(new TagBuilder(string.Empty)));

                It should_throw_an_exception = () => _exception.ShouldNotBeNull();

                It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
            }

            public class When_Searching_For_A_Tag_That_Does_Not_Exist
            {
                Because of = () => _result = HtmlContent.GetFullTag(new TagBuilder("div"));

                It should_return_an_empty_string = () => _result.ShouldEqual(string.Empty);
            }

            public class When_Searching_A_Good_String
            {
                Because of = () => _result = HtmlContent.GetFullTag(new TagBuilder("p"));

                It should_return_the_correct_text = () => _result.ShouldEqual(TagContent);
            }

            public class When_Searching_A_Good_String_With_Attributes
            {
                Because of = () => _result = HtmlContent.Replace("<p>", "<p attr='lala'>").GetFullTag(new TagBuilder("p"));

                It should_return_the_correct_text = () => _result.ShouldEqual(TagContent.Replace("<p>", "<p attr='lala'>"));
            }

            const string TagContent = "<p>with content</p>";
            const string HtmlContent = "with <p>with content</p> in content";
            static string _result;
            static Exception _exception;
        }
    }
}