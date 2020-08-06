using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPLocation.Model;
using IPLocation.Service;
using IPLocation.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPLocation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class ConverterController : ControllerBase
    {

        // api for convert data from sql server to mongodb 
        [HttpGet]
        public IActionResult Get()
        {
            Console.WriteLine("11111111111111111111111");
            string MongoConn = "mongodb://localhost:27017";
            Transfering transfer = new Transfering();
            transfer.TransferSqlToMongo(new SqlUtils().GetDBConnection(), MongoConn, "fortest", "IP");

            return Ok("OK");
        }

    }
}