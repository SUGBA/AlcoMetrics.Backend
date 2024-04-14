using WebApp.Data.ViewModel.Request.Account;

namespace WebApp.Services.AccountServices.Abstract
{
    /// <summary>
    /// Сервси для работы с AccountController
    /// </summary>
    public interface IAccountService
    {
        /// <summary>
        /// Войти в систему и получить токен
        /// </summary>
        /// <param name="model"> Модель с логином и паролем </param>
        /// <returns></returns>
        public Task<string?> Login(LoginViewModel model);

        /// <summary>
        /// Зарегестрироваться
        /// </summary>
        /// <param name="model"> Модель с логином и паролем </param>
        /// <returns></returns>
        public Task<string?> Register(RegisterViewModel model);
    }
}
