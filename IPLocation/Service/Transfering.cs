using IPLocation.Model;
using IPLocation.Mongo;
using IPLocation.Redis;
using IPLocation.Sql;
using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Service
{
    public class Transfering
    {
        public Transfering()
        {
           
        }

        // trasfer data from sql server to cache server (redis)
        public void TransferSqlToCache()
        {
            SqlUtils sql = new SqlUtils();
            SqlConnection conn = sql.GetDBConnection();
            conn.Open();

            string redisConnectionString = "10.0.6.58:30378";
            RedisUtils<string> redis = new RedisUtils<string>(redisConnectionString);


            int numOfRow = sql.getTotalRows(conn);

            int p = numOfRow / 1000;
            int q = numOfRow % 1000;

            for (int i = 0; i < numOfRow; i += 1000)
            {
                sql.getIP(conn, i, i + 1000);
                while (Data.data.Count > 0)
                {
                    byte[] IPFrom = Data.data[0].IPFrom;
                    byte[] IPTo = Data.data[0].IPTo;
                    string IPFStr = sql.ByteArrayToString(IPFrom);
                    string IPTStr = sql.ByteArrayToString(IPTo);


                }
            }


        }

        public void TransferSqlToMongo(SqlConnection conn, string MongoConn, string DBName, string CollectionName)
        {
            new Data();

            conn.Open();

            SqlUtils sql = new SqlUtils();

            Mongo.MongoUtils mongo = new Mongo.MongoUtils(MongoConn);

            var database = mongo.dbClient.GetDatabase(DBName);
            var collection = database.GetCollection<BsonDocument>(CollectionName);

            int numOfRow = sql.getTotalRows(conn);

            for (int i = 0; i < numOfRow; i += 1000)
            {
                sql.getIP(conn, i, i + 1000);

                while (Data.bsonData.Count > 0)
                {
                    collection.InsertOne(Data.bsonData[0]);
                    Data.bsonData.RemoveAt(0);
                }
            }

            int p = numOfRow % 1000;
            int q = numOfRow / 1000;

            sql.getIP(conn, q * 1000, numOfRow);
            while (Data.bsonData.Count > 0)
            {
                collection.InsertOne(Data.bsonData[0]);
                Data.bsonData.RemoveAt(0);
            }
        }
    }
}
