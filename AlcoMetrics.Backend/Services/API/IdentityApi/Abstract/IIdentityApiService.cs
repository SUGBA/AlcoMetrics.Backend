﻿namespace WebApp.Services.API.IdentityApi.Abstract
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
        /// Регистрация по логину и паролю
        /// </summary>
        /// <param name="login"> Логин </param>
        /// <param name="password"> Пароль </param>
        /// <returns></returns>
        public Task<string?> RegisterByPasswordGrantType(string login, string password);
    }
}
