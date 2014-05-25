namespace WebSnip.Transform.Websites
{
    using System;

    public interface IAmAWebsite
    {
        bool OptimizedFor(Uri uri);

        RenderSet Get();
    }
}