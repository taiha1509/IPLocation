using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Model
{
    public class IPInfo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int ID { get; set; }
        public byte[] IPFrom { get; set; }
        public byte[] IPTo { get; set; }
        public int IPType { get; set; }
        public string Country { get; set; }
        public string StateProvince { get; set; }
        public string CityDistrict { get; set; }
        public string Latitude { get; set; }
        public string Longtitude { get; set; }
        public string TimeZoneOffset { get; set; }
        public string TimeZoneName { get; set; }
        public string ISPName { get; set; }
        public string ConnectionType { get; set; }
        public string OUName { get; set; }
    }
}
