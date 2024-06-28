using System.Configuration;

namespace RedisCache.Demo02.Extension
{
    public static class RedisCacheExtension
    {
        public static void RedisCacheConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = configuration.GetConnectionString("RedisConn");
                option.InstanceName = "GamesCatalog_";
            });
        }

        public static void SessionConfiguration(this IServiceCollection services)
        {
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromSeconds(60);
            });
        }
    }
}
