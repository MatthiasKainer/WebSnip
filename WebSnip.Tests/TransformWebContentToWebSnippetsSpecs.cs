namespace WebSnip.Tests
{
    using System;
    using System.Collections.Generic;
    using Machine.Fakes;
    using Machine.Specifications;
    using Transform;

    public class TransformWebContentToWebSnippetsSpecs : WithSubject<TransformWebContentToWebSnippets>
    {
        public class When_Trying_To_Transform_An_Empty_WebContent
        {
            Because of = () => _exception = Catch.Exception(() => Subject.ToWebSnippet(_url, String.Empty));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Trying_To_Transform_WebContent_With_Only_Spaces
        {
            Because of = () => _exception = Catch.Exception(() => Subject.ToWebSnippet(_url, String.Empty));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Trying_To_Transform_Without_An_Url
        {
            Because of = () => _exception = Catch.Exception(() => Subject.ToWebSnippet(null, AnyWebContent));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argument_null_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Transforming_Valid_Content
        {
            public class When_With_Custom_Transformer_For_This_Url
            {
                Establish context = () =>
                    {
                        transformWebContentForUrl = An<ITransformWebContentForUrl>();
                        transformWebContentForUrl.WhenToldTo(_ => _.CanTransform(_url)).Return(true);
                        Configure(x => x.For<IEnumerable<ITransformWebContentForUrl>>()
                                        .Use(() =>
                                            new List<ITransformWebContentForUrl> { transformWebContentForUrl }));
                    };

                Because of = () => _exception = Catch.Exception(() => Subject.ToWebSnippet(_url, AnyWebContent));

                It should_not_throw_an_exception = () => _exception.ShouldBeNull();

                It should_have_used_the_custom_transformer =
                    () => transformWebContentForUrl.WasToldTo(_ => _.Transform(_url, AnyWebContent));

                static ITransformWebContentForUrl transformWebContentForUrl;
            }

            public class When_No_Custom_Transformer_For_This_Url
            {
                Because of = () => _exception = Catch.Exception(() => Subject.ToWebSnippet(_url, AnyWebContent));

                It should_not_throw_an_exception = () => _exception.ShouldBeNull();

                It should_have_used_the_default_transformer =
                    () => The<ITransformWebContentForUrl>().WasToldTo(_ => _.Transform(_url, AnyWebContent));
            }
        }

        static Uri _url = new Uri("http://www.someurl.com");
        static Exception _exception;
        const string AnyWebContent = "lala";
    }
}
