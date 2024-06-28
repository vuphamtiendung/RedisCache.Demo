using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RedisCache.Demo02.Data;
using RedisCache.Demo02.Model;
using RedisCache.Demo02.Services;
using System.Diagnostics.Contracts;

namespace RedisCache.Demo02.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RedisCachingService _cachingServices;
        private readonly GamesDataService _gameServices;
        public Games[] _games;
        public bool IsFromCache = false;
        public IndexModel(RedisCachingService cachingServices, GamesDataService gameServices)
        {
            _cachingServices = cachingServices;
            _gameServices = gameServices;
        }

        private string GetInstanceId()
        {
            var instanceId = HttpContext.Session.GetString("InstanceId");
            if (string.IsNullOrEmpty(instanceId))
            {
                instanceId = Guid.NewGuid().ToString();
                HttpContext.Session.SetString("InstanceId", instanceId);
            }
            return instanceId;
        }

        public void OnPostListGames()
        {
            var instanceId = GetInstanceId();
            var cacheKey = $"Game_Cache_{instanceId}";
            _games = _cachingServices.GetCacheData<Games[]>(cacheKey);
            if(_games == null)
            {
                _games = _gameServices.LoadGame();
                _cachingServices.SetCacheData(cacheKey, _games, TimeSpan.FromSeconds(15));
                IsFromCache = false;
            }
            else
            {
                IsFromCache = true;
            }
        }

        public void OnPostRemoveCache()
        {
            var instanceId = GetInstanceId();
            string cacheKey = $"Games_Cache_{instanceId}";
            _cachingServices.RemoveCacheData(cacheKey);
            OnPostListGames();
        }
    }
}
