namespace WebSnip.Tests.HttpRouting
{
    using System;
    using System.Collections.Specialized;
    using System.Web;
    using Machine.Fakes;
    using Machine.Specifications;
    using Transform;
    using Utils.Json;
    using WebSnip.HttpRouting;

    public class EvaluateUrlSpecs : WithSubject<EvaluateUrl>
    {
        Establish context = () =>
        {
            _httpContextBase = An<HttpContextBase>();
            _httpRequestBase = An<HttpRequestBase>();
            _httpContextBase.WhenToldTo(_ => _.Request).Return(_httpRequestBase);
            _httpRequestBase.WhenToldTo(_ => _.QueryString).Return(_validQueryString);
        };

        public class When_Asking_For_Routes
        {
            public class When_Asking_If_Can_Handle_A_Valid_Route
            {
                Establish context = () =>
                    {
                        _httpRequestBase.WhenToldTo(_ => _.Path).Return("websnip.axd/get");
                        _httpRequestBase.WhenToldTo(_ => _.QueryString).Return(_validQueryString);
                    };

                Because of = () => result = Subject.CanHandle(_httpContextBase);

                It should_say_yes = () => result.ShouldBeTrue();
            }

            public class When_Asking_If_Can_Handle_An_InValid_Route
            {
                Establish context = () =>
                {
                    _httpRequestBase.WhenToldTo(_ => _.Path).Return("websnip.axd/get");
                    _httpRequestBase.WhenToldTo(_ => _.QueryString).Return(_invalidQueryString);
                };

                Because of = () => result = Subject.CanHandle(_httpContextBase);

                It should_say_no = () => result.ShouldBeFalse();
            }

            public class When_Asking_If_Can_Handle_Another_Route
            {
                Establish context = () => _httpRequestBase.WhenToldTo(_ => _.Path).Return("websnip.axd/blabla");

                Because of = () => result = Subject.CanHandle(_httpContextBase);

                It should_say_no = () => result.ShouldBeFalse();
            }

            static bool result;
        }

        public class When_Handling_Valid_Routes
        {
            Establish context = () =>
                {
                    The<ISerializeToJson>().WhenToldTo(_ => _.Serialize(Param<object>.IsAnything)).Return("success");
                    The<ITagSetFactory>().WhenToldTo(_ => _.GetFor(ForUrl)).Return(new RenderSet());
                    The<ISnipMaker>().WhenToldTo(_ => _.GetSnippetFor(ForUrl)).Return(new WebSnippet(ForUrl));
                    _httpRequestBase.WhenToldTo(_ => _.Path).Return("websnip.axd/get");
                    _httpResponseBase = An<HttpResponseBase>();
                    _httpContextBase.WhenToldTo(_ => _.Response).Return(_httpResponseBase);
                    Subject.CanHandle(_httpContextBase);
                };

            Because of = () => Subject.Handle();

            It should_return_the_correct_content_type = () => _httpResponseBase.StatusCode.ShouldEqual(200);

            It should_return_a_success_status = () => _httpResponseBase.StatusCode.ShouldEqual(200);

            It should_return_the_serialized_result = () => _httpResponseBase.WasToldTo(_ => _.Write("success"));

            static bool result;
            static HttpResponseBase _httpResponseBase;
            static readonly Uri ForUrl = new Uri("http://www.example.url");
        }

        static HttpContextBase _httpContextBase;
        static HttpRequestBase _httpRequestBase;
        static NameValueCollection _validQueryString = new NameValueCollection { { "for", "http://www.example.url" } };
        static NameValueCollection _invalidQueryString = new NameValueCollection { { "for", "invalid" } };
    }
}
