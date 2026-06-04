using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.ServiceInterfaces;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace Assignment.Service
{
    public class CachService : ICachService
    {
        private readonly IDatabase redisDb;

        public CachService(IConnectionMultiplexer redis)
        {
            redisDb = redis.GetDatabase();
        }
        public async Task Delete(string key)
            => await redisDb.KeyDeleteAsync(key);

        public async Task<RedisValue> Get(string key)
            => await redisDb.StringGetAsync(key);

        public async Task Set(string key, string value)
            => await redisDb.StringSetAsync(key, value);
    }
}
