using IPLocation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Repository
{
    public interface IIPInfoRepository
    {
        Task<IEnumerable<IPInfo>> GetAllIP();

        // query with a IP address
        IEnumerable<IPInfo> GetIP(byte[] IPAddr);

        // add new note document
        Task AddIP(IPInfo item);

        // update just a single document / IP
        Task<bool> UpdateNote(string id, string content);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllIP();
    }
}
