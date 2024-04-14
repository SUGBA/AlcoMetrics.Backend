namespace WebApp.Data.ViewModel.Request.Account
{
    /// <summary>
    /// Логин и пароль часто используются в модельках, поэтому вынесены в отдельный компонент
    /// </summary>
    public abstract class BaseAccountViewModel
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string? Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string? Password { get; set; }
    }
}
