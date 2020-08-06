using IPLocation.Model;
using IPLocation.Service;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Repository
{
    public class IPRepository : IIPInfoRepository
    {
        private readonly MongodbContext _context = null;

        public IPRepository(IOptions<Settings> settings)
        {
            _context = new MongodbContext(settings);
        }

        public Task<IEnumerable<IPInfo>> GetAllIP()
        {
            throw new NotImplementedException();
        }

        // get IP info from given ip
        // done
        public IEnumerable<IPInfo> GetIP(byte[] IPAddr)
        {
            string networkIP = "AAAAAAAAAAAAAAAAAAAAAA==";
            string IPEncodeBase64 = IPHandlers.IPByteArrayToStringEncodeBase64(IPAddr);

            string queryString = "{$and: [{IPFrom: {$lte: BinData(0, " + IPEncodeBase64 + " )}}" +
                                 ",{IPTo: {$gte: BinData(0, " + IPEncodeBase64 + " )}}" +
                                 ",{IPFrom : {$ne: BinData(0, " + networkIP + ")}}]}";

            string collectionName = "IP";

            try
            {

                byte[] networkAddr = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                return _context.getCollection(collectionName).Find(ip => ip.IPFrom <= new BsonBinaryData(IPAddr)
                                                                   && ip.IPTo >= new BsonBinaryData(IPAddr)
                                                                   && ip.IPFrom != new BsonBinaryData(networkAddr)).ToList();

            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task AddIP(IPInfo item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllIP()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateNote(string id, string content)
        {
            throw new NotImplementedException();
        }


    }
}
