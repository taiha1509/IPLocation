using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPLocation.Model;
using IPLocation.Redis;
using IPLocation.Repository;
using IPLocation.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IPLocation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        private readonly IIPInfoRepository _IPInfoRepository;

        public LocationController(IIPInfoRepository IPInfoRepository)
        {
            _IPInfoRepository = IPInfoRepository;
        }

        // GET: api/Location
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Location/10.0.0.8
        [HttpGet("{IP}", Name = "Get")]
        public List<string> Get(string IP)
        {


            IPInfo ip = new IPInfo();

            RedisUtils<IPInfo> redis = new RedisUtils<IPInfo>("10.0.6.58:30378");


            // if the key is exist in cache then get the value ditectly
            if (redis.IsContainKey(IP))
            {
                ip = redis.GetValue<IPInfo>(IP);
                List<string> res = new List<string>();
                res.Add(ip._id);
                res.Add(ip.ID.ToString());
                res.Add(IPHandlers.ByteArrayToString(ip.IPFrom));
                res.Add(IPHandlers.ByteArrayToString(ip.IPTo));
                res.Add(ip.IPType.ToString());
                res.Add(ip.ISPName);
                res.Add(ip.Latitude.ToString());
                res.Add(ip.Longtitude.ToString());
                res.Add(ip.OUName);
                res.Add(ip.StateProvince);
                res.Add(ip.TimeZoneName);
                res.Add(ip.TimeZoneOffset);
                res.Add(ip.CityDistrict);
                res.Add(ip.ConnectionType);
                res.Add(ip.Country);

                return res;
            }
            // else get value from mongodb and cache the key and value relatively
            else
            {
                List<string> res = new List<string>();
                byte[] IPByteArray = IPHandlers.StringToByteArray(IP);
                ip = _IPInfoRepository.GetIP(IPByteArray).First();

                res.Add(ip._id);
                res.Add(ip.ID.ToString());
                res.Add(IPHandlers.ByteArrayToString(ip.IPFrom));
                res.Add(IPHandlers.ByteArrayToString(ip.IPTo));
                res.Add(ip.IPType.ToString());
                res.Add(ip.ISPName);
                res.Add(ip.Latitude.ToString());
                res.Add(ip.Longtitude.ToString());
                res.Add(ip.OUName);
                res.Add(ip.StateProvince);
                res.Add(ip.TimeZoneName);
                res.Add(ip.TimeZoneOffset);
                res.Add(ip.CityDistrict);
                res.Add(ip.ConnectionType);
                res.Add(ip.Country);

                redis.Add(ip, IP);

                return res;
            }

        }

        // POST api/<LocationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<LocationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LocationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
