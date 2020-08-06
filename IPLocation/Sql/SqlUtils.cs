using IPLocation.Model;
using Microsoft.Data.SqlClient;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IPLocation.Sql
{
    public class SqlUtils
    {
        public SqlConnection GetDBConnection(string datasource, string database, string username, string password)
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }

        public SqlConnection GetDBConnection()
        {
            string dataSource = "DATABASE";
            dataSource += "\\";
            dataSource += "RDDATABASE";
            string database = "IPInfo";
            string username = "sa";
            string password = "12345678@Abc";

            return GetDBConnection(dataSource, database, username, password);
        }

        // return total number of rows of table
        public int getTotalRows(SqlConnection conn)
        {
            SqlCommand cmd = new SqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            string query = "select count(ID) from dbo.IPInfo";

            cmd.CommandText = query;

            using (DbDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                return Convert.ToInt32(reader.GetValue(0));
            }
        }

        public void getIP(SqlConnection conn, int beginAt, int endAt)
        {


            // Tạo một đối tượng Command.
            SqlCommand cmd = new SqlCommand();

            // Liên hợp Command với Connection.
            cmd.Connection = conn;
            string query = "select * from dbo.IPInfo where ID >= '" + beginAt + "'" + "and ID < '" + endAt + "'";

            cmd.CommandText = query;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        BsonDocument bson = new BsonDocument();

                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                            if (reader[j].GetType() == typeof(String))
                                bson.Add(new BsonElement(reader.GetName(j), reader[j].ToString()));
                            else if ((reader[j].GetType() == typeof(Int32)))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetInt32(j))));
                            }
                            else if (reader[j].GetType() == typeof(Int16))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetInt16(j))));
                            }
                            else if (reader[j].GetType() == typeof(Int64))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetInt64(j))));
                            }
                            else if (reader[j].GetType() == typeof(float))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetFloat(j))));
                            }
                            else if (reader[j].GetType() == typeof(Double))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetDouble(j))));
                            }
                            else if (reader[j].GetType() == typeof(DateTime))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetDateTime(j))));
                            }
                            else if (reader[j].GetType() == typeof(Guid))
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetGuid(j))));
                            else if (reader[j].GetType() == typeof(Boolean))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetBoolean(j))));
                            }
                            else if (reader[j].GetType() == typeof(DBNull))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonNull.Value));
                            }
                            else if (reader[j].GetType() == typeof(Byte))
                            {
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader.GetByte(j))));
                            }
                            else if (reader[j].GetType() == typeof(Byte[]))
                            {                               
                                bson.Add(new BsonElement(reader.GetName(j), BsonValue.Create(reader[j] as byte[])));
                            }
                            else
                                throw new Exception();
                        }
                        Data.bsonData.Add(bson);
                    }


                }
            }
        }

        public string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

    }
}
