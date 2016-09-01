using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Core
{
    public class JsonSerializer<T>
    {
        public static T ToObject(string json)
        {
            var ser = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var result = (T)ser.ReadObject(stream);
                return result;
            }
        }

        public static string ToJsonString(T obj)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(typeof(T));
                    ser.WriteObject(ms, obj);
                    byte[] json = ms.ToArray();
                    return Encoding.UTF8.GetString(json, 0, json.Length);
                }
            }
            catch (Exception ex)
            {
                return "Exception:" + ex.InnerException;
            }
        }
    }
}
