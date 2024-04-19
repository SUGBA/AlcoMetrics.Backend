using WebApp.Data.ViewModel.Response.Account;

namespace WebApp.Services.API.IdentityApi.Abstract
{
    /// <summary>
    /// Сервиc связи с identity server
    /// </summary>
    public interface IIdentityApiService
    {
        /// <summary>
        /// Получить токен по логину и паролю
        /// </summary>
        /// <param name="login"> Логин и пароль </param>
        /// <param name="password"> Пароль </param>
        /// <returns></returns>
        public Task<string?> GetTokenByPasswordGrantType(string login, string password);

        /// <summary>
        /// Регистрация пользователя по логину и паролю
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <param name="userRole"> Роль пользователя </param>
        /// <returns></returns>
        public Task<UserRegisterResponse?> RegisterUserByPasswordGrantType(string login, string password, int userRole);

        /// <summary>
        /// Регистрация администратора по логину и паролю
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <returns></returns>
        public Task<AdminRegisterResponse?> RegisterAdminByPasswordGrantType(string login, string password);
    }
}
