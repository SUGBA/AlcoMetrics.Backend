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

        public async Task<string?> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
                return null;

            return await _identityApiService.GetTokenByPasswordGrantType(model.Login, model.Password);
        }

        public Task<string?> Register(RegisterViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
