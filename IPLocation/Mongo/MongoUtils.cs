using IPLocation.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Mongo
{
    public class MongoUtils
    {
        public MongoClient dbClient;

        public MongoUtils(string connection)
        {
            dbClient = new MongoClient(connection);
        }
        
        public MongoClient getMongo()
        {
            return dbClient;   
        }
    }
}   
