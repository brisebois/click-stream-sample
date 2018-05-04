using System;
using System.Runtime.Serialization;

namespace ClickStream.Core
{
    [DataContract]
    public class ClickEvent
    {
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int CampaignId { get; set; }
    }
}
