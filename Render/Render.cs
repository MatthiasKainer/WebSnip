namespace WebSnip.Render
{
    using System;
    using System.Linq;

    public static class Render
    {
        public static T A<T>(params object[] content) where T : BaseRenderer
        {
            var constructorInfo = typeof (T).GetConstructor(content.Select(_ => _.GetType()).ToArray());
            if (constructorInfo == null) throw new Exception(string.Format("Invalid number of arguments for constructor of type {0}", typeof(T)));
            
            return (T) constructorInfo.Invoke(content);
        }
    }
}