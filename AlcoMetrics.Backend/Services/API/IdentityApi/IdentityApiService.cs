using IdentityModel.Client;
using WebApp.Extensions;
using WebApp.Services.API.IdentityApi.Abstract;

namespace WebApp.Services.API.IdentityApi
{
    /// <summary>
    /// Сервис связи с Identity server
    /// </summary>
    public class IdentityApiService : IIdentityApiService
    {
        private readonly IConfiguration _configuration;

        public IdentityApiService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string?> GetTokenByPasswordGrantType(string login, string password)
        {
            var httpClient = new HttpClient();

            var domenPath = _configuration.TryGetValue("AuthSetting:ShareSettings:AuthenticationServicePath", "Конфигурация не содержит доменный путь");
            var getTokenPath = _configuration.TryGetValue("AuthSetting:ShareSettings:GetTokenPath", "Конфигурация не содержит путь для получения токена");
            var scope = _configuration.TryGetValues("AuthSetting:WineClientSettings:Scopes", "Конфигурация не содержит scopes");
            var clientId = _configuration.TryGetValue("AuthSetting:WineClientSettings:ClientId", "Конфигурация не содержит id клиента");
            var secret = _configuration.TryGetValue("AuthSetting:WineClientSettings:WineAuthSecret", "Конфигурация не содержит пользовательский секрет");
            var grantType = _configuration["AuthSetting:PasswordGrantTypeAuth:GrantType"] ?? "password";

            var path = $"{domenPath}/{getTokenPath}";

            var tokenRequest = new PasswordTokenRequest
            {
                Address = path,
                GrantType = grantType,
                ClientId = clientId,
                ClientSecret = secret,
                Scope = string.Join(" ", scope),
                UserName = login,
                Password = password
            };

            var identityServerResponse = await httpClient.RequestPasswordTokenAsync(tokenRequest);

            if (!identityServerResponse.IsError)
            {
                return identityServerResponse.AccessToken!;
            }

            return null;
        }

        public Task<string?> RegisterByPasswordGrantType(string login, string password)
        {
            throw new NotImplementedException();
        }
    }
}
