using Microsoft.AspNetCore.Identity;
using WebApp.Data.ViewModel.Request.Account;
using WebApp.Services.AccountServices.Abstract;
using WebApp.Services.API.IdentityApi.Abstract;

namespace WebApp.Services.AccountServices
{
    /// <summary>
    /// Сервси для работы с AccountController
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IIdentityApiService _identityApiService;

        public AccountService(IIdentityApiService identityApiService)
        {
            _identityApiService = identityApiService;
        }

        /// <summary>
        /// Войти в систему и получить токен
        /// </summary>
        /// <param name="model"> Модель с логином и паролем </param>
        /// <returns></returns>
        public async Task<string?> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return null;

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
                return Enumerable.Empty<string>();

            return await _identityApiService.RegisterAdminByPasswordGrantType(model.Login, model.Password) ?? Enumerable.Empty<string>();
        }

        /// <summary>
        /// Зарегестрировать администратора
        /// </summary>
        /// <param name="model"> Модель с логином и паролем </param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> RegisterUser(RegisterUserViewModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return Enumerable.Empty<string>();

            return await _identityApiService.RegisterUserByPasswordGrantType(model.Login, model.Password) ?? Enumerable.Empty<string>();
        }
    }
}
