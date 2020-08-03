using IPLocation.Model;
using IPLocation.Sql;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation
{
    public class Test
    {
        public Test()
        {
            SqlUtils sql = new SqlUtils();

            SqlConnection conn = sql.GetDBConnection();

            conn.Open();

            int total = sql.getTotalRows(conn);

            Console.WriteLine("total row of table: " + total);

            new Data();

            sql.getIP(conn, 1, 2);

            Console.WriteLine(Data.data.Count);

            Console.WriteLine(Data.data[0].IPFrom);
        }
    }
}
