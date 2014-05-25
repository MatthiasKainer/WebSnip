namespace WebSnip.Transform
{
    using System;

    public interface ITagSetFactory
    {
        RenderSet GetFor(Uri uri);
    }
}