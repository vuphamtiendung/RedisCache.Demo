using RedisCache.Demo02.Model;
using System.Text.Json;

namespace RedisCache.Demo02.Data
{
    public class GamesDataService
    {
        public Games[] LoadGame()
        {
            using var streamReader = new StreamReader("Data/GamesData.json");
            var gamesData = streamReader.ReadToEnd();
            var games = JsonSerializer.Deserialize<Games[]>(gamesData);
            return games;
        }
    }
}
