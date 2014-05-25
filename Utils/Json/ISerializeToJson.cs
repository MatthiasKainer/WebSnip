namespace WebSnip.Utils.Json
{
    using System;
    using Newtonsoft.Json;

    public interface ISerializeToJson
    {
        string Serialize(object objectToSerialize);
    }

    public class JsonSerializer : ISerializeToJson
    {
        public string Serialize(dynamic objectToSerialize)
        {
            try
            {
                return JsonConvert.SerializeObject(new
                {
                    success = true,
                    data = objectToSerialize
                });
            }
            catch (Exception exception)
            {
                return JsonConvert.SerializeObject(new
                {
                    success = false,
                    data = "Unable to serialize object"
                });
            }
        }
    }
}