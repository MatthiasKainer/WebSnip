namespace WebSnip.Utils.SyntaticSugar.Switch
{
    public class Switch<T>
    {
        public static Switch<T> With(T objectToSwitch) { return new Switch<T>(objectToSwitch); } 

        private Switch(T objectToSwitch)
        {
            ObjectToSwitch = objectToSwitch;
        }

        public T ObjectToSwitch { get; private set; }
    }
}