namespace WebSnip.Utils.SyntaticSugar.Switch
{
    using System;

    public static class SwitchExtensions
    {
        public static Switch<T> Case<T>(this Switch<T> switchObject, Func<T, bool> @if, Action<T> then)
        {
            if (switchObject == null)
            {
                return null;
            }

            if (@if(switchObject.ObjectToSwitch))
            {
                then(switchObject.ObjectToSwitch);
                return null;
            }

            return switchObject;
        }

        public static void Default<T>(this Switch<T> switchObject, Action<T> action)
        {
            if (switchObject != null)
            {
                action(switchObject.ObjectToSwitch);
            }
        }
    }
}