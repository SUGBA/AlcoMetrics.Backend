namespace WebApp.Extensions
{
    /// <summary>
    /// Расширения для Iconfigurations
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Получить значение или выкинуть исключение
        /// </summary>
        /// <param name="config"> Конфиги </param>
        /// <param name="key"> Ключ значение которого нужно получить </param>
        /// <param name="exceptionMessage"> Сообщение в случае ошибки </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string TryGetValue(this IConfiguration config, string key, string exceptionMessage)
        {
            return config[key] ?? throw new Exception(exceptionMessage);
        }

        /// <summary>
        /// Получить значения или выкинуть исключение
        /// </summary>
        /// <param name="config"> Конфиги </param>
        /// <param name="key"> Ключ значение которого нужно получить </param>
        /// <param name="exceptionMessage"> Сообщение в случае ошибки </param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string[] TryGetValues(this IConfiguration config, string key, string exceptionMessage)
        {
            return config.GetSection(key).Get<string[]>() ?? throw new Exception(exceptionMessage);
        }
    }
}
