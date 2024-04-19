namespace WebApp.Services.API.WineApi.Abstract
{
    /// <summary>
    /// Сервис API для связи с сервисом виноделия, для работы с аккаунтами
    /// </summary>
    public interface IAccountWineApiService
    {
        Task<bool> RegisterUser(string Login, string Password, int id);
    }
}
