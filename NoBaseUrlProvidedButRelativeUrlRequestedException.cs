namespace WebSnip
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class NoBaseUrlProvidedButRelativeUrlRequestedException : Exception
    {
        public NoBaseUrlProvidedButRelativeUrlRequestedException()
        {
        }

        public NoBaseUrlProvidedButRelativeUrlRequestedException(string message) : base(message)
        {
        }

        public NoBaseUrlProvidedButRelativeUrlRequestedException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NoBaseUrlProvidedButRelativeUrlRequestedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}