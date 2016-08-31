using System.Runtime.Serialization;

namespace Core.Models
{
    //Note: To use DataContract, System.Runtime.Serialization reference should be added
    [DataContract]
    public class Filter
    {
        [DataMember]
        public string field { get; set; }

        [DataMember]
        public string op { get; set; }

        [DataMember]
        public string data { get; set; }
    }
}
