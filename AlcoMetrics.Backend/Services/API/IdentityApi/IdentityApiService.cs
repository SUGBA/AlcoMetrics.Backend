using IdentityModel.Client;
using Microsoft.Net.Http.Headers;
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

        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityApiService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получить токен по логину и паролю
        /// </summary>
        /// <param name="login"> Логин и пароль </param>
        /// <param name="password"> Пароль </param>
        /// <returns></returns>
        public async Task<string?> GetTokenByPasswordGrantType(string login, string password)
        {
            var httpClient = new HttpClient();

            var domenPath = _configuration.TryGetValue("ApiIntegrationSettings:IdentityService:AuthenticationServicePath", "Конфигурация не содержит доменный путь");
            var getTokenPath = _configuration.TryGetValue("ApiIntegrationSettings:IdentityService:GetTokenPath", "Конфигурация не содержит путь для получения токена");
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

        /// <summary>
        /// Регистрация администратора по логину и паролю
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <returns></returns>
        public async Task<IEnumerable<string>?> RegisterAdminByPasswordGrantType(string login, string password)
        {
            if (_httpContextAccessor.HttpContext == null) throw new NullReferenceException("HttpContext имеет значение null, при попытке регистрации администратора");
            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization];     //Токен с ключевым словом Bearer

            var domenPath = _configuration.TryGetValue("ApiIntegrationSettings:IdentityService:AuthenticationServicePath", "Конфигурация не содержит доменный путь до Identity Server");
            var registerAdminPath = _configuration.TryGetValue("ApiIntegrationSettings:IdentityService:RegisterAdminPath", "Конфигурация не содержит путь для регистрации пользователя до Identity Server");

            var path = $"{domenPath}/{registerAdminPath}";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, jwtToken.ToString());
            using var response = await httpClient.PostAsJsonAsync(path, new { Login = login, Password = password });

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }

        /// <summary>
        /// Регистрация пользователя по логину и паролю
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <param name="userRole"> Роль пользователя </param>
        /// <returns></returns>
        public async Task<IEnumerable<string>?> RegisterUserByPasswordGrantType(string login, string password, int userRole)
        {
            var domenPath = _configuration.TryGetValue("ApiIntegrationSettings:IdentityService:AuthenticationServicePath", "Конфигурация не содержит доменный путь до Identity Server");
            var registerUserPath = _configuration.TryGetValue("ApiIntegrationSettings:IdentityService:RegisterUserPath", "Конфигурация не содержит путь для регистрации пользователя до Identity Server");

            var path = $"{domenPath}/{registerUserPath}";

            var httpClient = new HttpClient();
            using var response = await httpClient.PostAsJsonAsync(path, new { Login = login, Password = password, UserRole = userRole });

            return await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
        }
    }
}
