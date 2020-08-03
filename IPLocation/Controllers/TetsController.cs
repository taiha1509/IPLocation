using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPLocation.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace IPLocation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TetsController : ControllerBase
    {
        // GET: api/Tets
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Tets/5
        [HttpGet("{IPAddr}", Name = "Get")]
        public string Get(string IPAddr)
        {
            string MongoConn = "mongodb://localhost:27017";
            string DBName = "fortest";
            string CollectionName = "IP";

            Mongo.MongoUtils mongo = new Mongo.MongoUtils(MongoConn);
            var database = mongo.dbClient.GetDatabase(DBName);
            var collection = database.GetCollection<BsonDocument>(CollectionName);


            IPHandlers IPH = new IPHandlers();
            string temp = IPH.IPToString(IPAddr);
            string res = "";
            for(int i = 0; i < 24; i++)
            {
                res += "0";
            }
            res += temp;
            return res;
        }

        // POST: api/Tets
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Tets/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
