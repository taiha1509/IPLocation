using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPLocation.Service
{
    public class IPHandlers
    {
        public IPHandlers()
        {

        }

        public static string IPToString(string IPAddr)
        {
            var arr = IPAddr.Split(".");
            string x1 = Int32.Parse(arr[0]).ToString("X2");
            string x2 = Int32.Parse(arr[1]).ToString("X2");
            string x3 = Int32.Parse(arr[2]).ToString("X2");
            string x4 = Int32.Parse(arr[3]).ToString("X2");
            return x1 + x2 + x3 + x4;
        }

        public static string IPByteArrayToStringEncodeBase64(byte[] IPAddr)
        {     
            var plainTextBytes = Convert.ToBase64String(IPAddr);

            return plainTextBytes;
        }

        // convert byte array to string
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        // convert string to byte array with 2 character in an element in array
        public static byte[] StringToByteArray(string str)
        {
            byte[] res = new byte[16];
            for (int i = 0; i < 12; i += 1)
            {
                res[i] = 0;
            }
            string[] temp = str.Split(".");

            res[12] = byte.Parse(temp[0]);
            res[13] = byte.Parse(temp[1]);
            res[14] = byte.Parse(temp[2]);
            res[15] = byte.Parse(temp[3]);

            return res;
        }
    }
}
