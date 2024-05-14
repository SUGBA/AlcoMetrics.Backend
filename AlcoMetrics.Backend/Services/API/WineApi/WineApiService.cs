using WebApp.Extensions;
using WebApp.Services.API.WineApi.Abstract;

namespace WebApp.Services.API.WineApi
{
    /// <summary>
    /// Сервис API для виноделия
    /// </summary>
    public class WineApiService : IAccountWineApiService
    {
        private readonly IConfiguration _configuration;

        public WineApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> RegisterUser(int id)
        {
            var domenPath = _configuration.TryGetValue("ApiIntegrationSettings:WineService:WineServicePath", "Конфигурация не содержит доменный путь до Wine Server");
            var registerPath = _configuration.TryGetValue("ApiIntegrationSettings:WineService:RegisterUserPath", "Конфигурация не содержит путь для регистрации пользователя в Wine Server");

            var path = $"{domenPath}/{registerPath}/{id}";

            var httpClient = new HttpClient();
            using var response = await httpClient.PostAsync(path, null);

            return await response.Content.ReadFromJsonAsync<bool>();
        }
    }
}
