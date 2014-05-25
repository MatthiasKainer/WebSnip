namespace WebSnip.Tests.HttpRouting
{
    using System.Web;
    using Machine.Fakes;
    using Machine.Specifications;
    using WebSnip.HttpRouting;

    public class NoRouteFoundSpecs : WithSubject<NoRouteFound>
    {
        public class When_Asking_If_Can_Handle_Route
        {
            Because of = () => result = Subject.CanHandle(null) && Subject.CanHandle(An<HttpContextBase>());

            It should_always_say_yes = () => result.ShouldBeTrue();

            static bool result;
        }

        public class When_Handling_Route
        {
            Establish context = () =>
                {
                    var httpContextBase = An<HttpContextBase>();
                    _httpResponseBase = An<HttpResponseBase>();
                    httpContextBase.WhenToldTo(_ => _.Response).Return(_httpResponseBase);
                    Subject.CanHandle(httpContextBase);
                };

            Because of = () => Subject.Handle();

            It should_return_a_not_found_status = () => _httpResponseBase.StatusCode.ShouldEqual(404);

            static bool result;
            static HttpResponseBase _httpResponseBase;
        }
    }
}
