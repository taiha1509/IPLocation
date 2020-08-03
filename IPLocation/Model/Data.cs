using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Model
{
    class Data
    {
        public static List<IPInfo> data;

        public static List<BsonDocument> bsonData;
        public Data()
        {
            data = new List<IPInfo>();
            bsonData = new List<BsonDocument>();
        }
    }
}
