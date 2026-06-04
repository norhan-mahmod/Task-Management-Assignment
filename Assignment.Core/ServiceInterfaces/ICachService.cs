using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Assignment.Core.ServiceInterfaces
{
    public interface ICachService
    {
        Task Set(string key, string value);
        Task<RedisValue> Get(string key);
        Task Delete(string key);
    }
}
