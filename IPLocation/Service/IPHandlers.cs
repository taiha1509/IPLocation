using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Service
{
    public class IPHandlers
    {
        public IPHandlers()
        {

        }

        public string IPToString(string IPAddr)
        {
            var arr = IPAddr.Split(".");
            string x1 = Int32.Parse(arr[0]).ToString("X2");
            string x2 = Int32.Parse(arr[1]).ToString("X2");
            string x3 = Int32.Parse(arr[2]).ToString("X2");
            string x4 = Int32.Parse(arr[3]).ToString("X2");
            return x1 + x2 + x3 + x4;
        }
    }
}
