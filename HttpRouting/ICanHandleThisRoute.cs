namespace WebSnip.HttpRouting
{
    using System.Web;

    public interface ICanHandleThisRoute
    {
        bool CanHandle(HttpContextBase context);

        void Handle();
    }
}