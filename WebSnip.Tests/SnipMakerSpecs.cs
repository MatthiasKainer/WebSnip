namespace WebSnip.Tests
{
    using System;
    using Machine.Fakes;
    using Machine.Specifications;
    using Transform;

    public class SnipMakerSpecs : WithSubject<SnipMaker>
    {
        public class When_Requesting_No_Url
        {
            Because of = () => _exception = Catch.Exception(() => _result = Subject.GetSnippetFor((Uri) null));

            It should_throw_an_exception = () => _exception.ShouldNotBeNull();

            It should_throw_an_argumentnull_exception = () => _exception.ShouldBeOfExactType<ArgumentNullException>();
        }

        public class When_Requesting_A_Valid_Relative_Url
        {
            Establish context = () => uri = new Uri("/test", UriKind.Relative);
            static Uri uri;

            public class With_No_Base_Url
            {
                Because of = () => _exception = Catch.Exception(() => _result = Subject.GetSnippetFor(uri));

                It should_throw_an_exception = () => _exception.ShouldNotBeNull();

                It should_throw_an_argumentnull_exception = () => _exception.ShouldBeOfExactType<NoBaseUrlProvidedButRelativeUrlRequestedException>();
            }

            public class With_Base_Url
            {
                Establish context =
                    () =>
                        {
                            _withBaseUrl = new Uri("http://www.myurl.com");
                            _forUrl = new Uri(_withBaseUrl, uri);
                            The<ITransformWebContentToWebSnippets>()
                                .WhenToldTo(_ => _.ToWebSnippet(_forUrl, Param<string>.IsAnything))
                                .Return(new WebSnippet(_forUrl));
                        };

                Because of = () =>
                    {
                        Subject.WithBaseUrl(_withBaseUrl);
                        _exception = Catch.Exception(() => _result = Subject.GetSnippetFor(uri));
                    };

                It should_not_throw_an_exception = () => _exception.ShouldBeNull();

                It should_have_requested_the_content_for_this_url =
                    () => The<IRequestWebSnippets>().WasToldTo(_ => _.GetContent(_forUrl)).OnlyOnce();

                It should_have_transformed_the_content_to_a_snippet =
                    () => The<ITransformWebContentToWebSnippets>().WasToldTo(_ => _.ToWebSnippet(_forUrl, Param<string>.IsAnything)).OnlyOnce();

                It should_return_a_websnip = () => _result.ShouldNotBeNull();
                static Uri _withBaseUrl;
                static Uri _forUrl;
            }
        }

        public class When_Requesting_A_Valid_Absolute_Url
        {
            Establish context = () =>
                {
                    uri = new Uri("http://www.myurl.com/test");
                    The<ITransformWebContentToWebSnippets>()
                        .WhenToldTo(_ => _.ToWebSnippet(uri, Param<string>.IsAnything))
                        .Return(new WebSnippet(uri));
                };
            static Uri uri;

            Because of = () =>
            {
                _exception = Catch.Exception(() => _result = Subject.GetSnippetFor(uri));
            };

            It should_not_throw_an_exception = () => _exception.ShouldBeNull();

            It should_have_requested_the_content_for_this_url =
                () => The<IRequestWebSnippets>().WasToldTo(_ => _.GetContent(uri)).OnlyOnce();

            It should_have_transformed_the_content_to_a_snippet =
                () => The<ITransformWebContentToWebSnippets>().WasToldTo(_ => _.ToWebSnippet(uri, Param<string>.IsAnything)).OnlyOnce();

            It should_return_a_websnip = () => _result.ShouldNotBeNull();
        }

        static Exception _exception; 
        static WebSnippet _result;
    }
}
