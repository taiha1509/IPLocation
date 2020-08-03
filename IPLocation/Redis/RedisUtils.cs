using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPLocation.Redis
{
    public class RedisUtils<T>
    {
        private static ConnectionMultiplexer _conn;
        public string _connection;

        // some method using API of redis

        public RedisUtils( string connection)
        {
            _connection = connection;

            _conn = ConnectionMultiplexer.Connect(_connection);
        }
        private IDatabase GetRedisDb()
        {
            return _conn.GetDatabase();
        }
        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
        private T Deserialize<T>(string serialized)
        {
            return JsonConvert.DeserializeObject<T>(serialized);
        }
        public void Insert(int index, T item, string key)
        {
            var db = GetRedisDb();
            var before = db.ListGetByIndex(key, index);
            db.ListInsertBefore(key, before, Serialize(item));
           
        }
        public void RemoveAt(int index, string key)
        {
            var db = GetRedisDb();
            var value = db.ListGetByIndex(key, index);
            if (!value.IsNull)
            {
                db.ListRemove(key, value);
            }
        }
        
        //add an element at the tail of list, if specific list doesnt exist then a empty list will be created
        public void Add(T item, string key)
        {
            GetRedisDb().ListRightPush(key, Serialize(item));
        }
        // remove an element with a specific key
        public void Clear(string key)
        {
            GetRedisDb().KeyDelete(key);
        }

        public bool IsContainKey(string key)
        {
            return GetRedisDb().KeyExists(key);
        }

        public bool Contains(T item, string key)
        {
            for (int i = 0; i < Count(key); i++)
            {
                if (GetRedisDb().ListGetByIndex(key, i).ToString().Equals(Serialize(item)))
                {
                    return true;
                }
            }
            return false;
        }
        
        // get index of an element from a list with key
        public int IndexOf(string key, T item)
        {
            for (int i = 0; i < Count(key); i++)
            {
                if (GetRedisDb().ListGetByIndex(key, i).ToString().Equals(Serialize(item)))
                {
                    return i;
                }
            }
            return -1;
        }

        //counting size of list with key
        public int Count(string key)
        {
            return (int)GetRedisDb().ListLength(key); 
        }
        

        public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        // remove an item from a list with key
        public bool Remove(T item, string key)
        {
            return GetRedisDb().ListRemove(key, Serialize(item)) > 0;
        }
       
    }
}
