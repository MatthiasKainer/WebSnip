namespace WebSnip
{
    using System;

    public interface IRequestWebSnippets
    {
        string GetContent(Uri uri);
    }
}