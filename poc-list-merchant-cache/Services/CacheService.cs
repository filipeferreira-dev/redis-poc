using Newtonsoft.Json;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace pocListMerchantCache.Services
{
    public class CacheService
    {
        public static ConnectionMultiplexer Redis { get; private set; }

        public CacheService()
        {
            if (Redis == null) Redis = ConnectionMultiplexer.Connect("localhost");
        }

        public async Task SetAsync(string key, object value)
        {
            var db = Redis.GetDatabase();
            await db.StringSetAsync(key, JsonConvert.SerializeObject(value));
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var db = Redis.GetDatabase();
            var value = await db.StringGetAsync(key);

            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : (default);
        }

        public async Task<bool> DelAsync(string key)
        {
            var db = Redis.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }

        public async Task ClearAsyn()
        {
            var db = Redis.GetDatabase();
            await db.ExecuteAsync("flushall");
        }
    }
}
