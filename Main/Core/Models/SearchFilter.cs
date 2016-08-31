using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Core.Models
{

    [DataContract]
    public class SearchFilter
    {
        [DataMember]
        public string groupOp { get; set; }

        [DataMember]
        public IList<Filter> rules { get; set; }
    }


}
