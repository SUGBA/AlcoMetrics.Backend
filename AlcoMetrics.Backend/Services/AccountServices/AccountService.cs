using WebApp.Data.ViewModel.Request.Account;
using WebApp.Services.AccountServices.Abstract;
using WebApp.Services.API.IdentityApi.Abstract;
using WebApp.Services.API.WineApi.Abstract;

namespace WebApp.Services.AccountServices
{
    /// <summary>
    /// Сервси для работы с AccountController
    /// </summary>
    public class AccountService : IAccountService
    {
        private const string WINE_API_ERROR = "Ошибка при создании пользователе в виноделии. Возможно сервис не доступен в данный момент.";

        private const string IDENTITY_API_ERROR = "Ошибка при создании авторизационного пользователе . Возможно сервис авторизаци не доступен в данный момент.";

        private const string INVALID_DATA_ERROR = "Логин или пароль не были заполнены";

        private readonly IIdentityApiService _identityApiService;

        private readonly IAccountWineApiService _accountWineApi;

        public AccountService(IIdentityApiService identityApiService, IAccountWineApiService accountWineApi)
        {
            _identityApiService = identityApiService;
            _accountWineApi = accountWineApi;
        }

        /// <summary>
        /// Войти в систему и получить токен
        /// </summary>
        /// <param name="model"> Модель с логином и паролем </param>
        /// <returns></returns>
        public async Task<string?> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return INVALID_DATA_ERROR;

            return await _identityApiService.GetTokenByPasswordGrantType(model.Login, model.Password);
        }

        /// <summary>
        /// Зарегестрировать пользователя
        /// </summary>
        /// <param name="model"> Модель с логином и паролем </param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> RegisterAdmin(RegisterAdminViewModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return new List<string> { INVALID_DATA_ERROR };

            return (await _identityApiService.RegisterAdminByPasswordGrantType(model.Login, model.Password))?.Errors ?? new List<string> { IDENTITY_API_ERROR };
        }

        /// <summary>
        /// Зарегестрировать администратора
        /// Сначала создаем аутентификационного пользователя
        /// Затем пользователя для виноделия
        /// </summary>
        /// <param name="model"> Модель для регистрации пользователя</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> RegisterUser(RegisterUserViewModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return new List<string> { INVALID_DATA_ERROR };

            var identityResult = await _identityApiService.RegisterUserByPasswordGrantType(model.Login, model.Password, model.UserRole);
            if (identityResult == null) return new List<string> { IDENTITY_API_ERROR };
            if (identityResult.Errors.Count() > 0) return identityResult.Errors;

            var wineResult = await _accountWineApi.RegisterUser(identityResult.UserId);
            if (!wineResult) return new List<string> { WINE_API_ERROR };

            return Enumerable.Empty<string>();
        }
    }
}
