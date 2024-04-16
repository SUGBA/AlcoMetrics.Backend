using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Data.ViewModel.Request.Account;
using WebApp.Services.AccountServices.Abstract;

namespace WebApp.Controllers
{
    /// <summary>
    /// Контроллер для работы с аккаунтами
    /// </summary>
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Войти в систему и вернуть token авторизации
        /// </summary>
        /// <param name="model"> Модель пользовательских данных </param>
        /// <returns></returns>
        [HttpPost("Login")]
        [Authorize]
        public async Task<string?> Login([FromBody] LoginViewModel model)
        {
            return await _accountService.Login(model);
        }

        /// <summary>
        /// Регистрация администратора
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("RegisterAdmin")]
        [Authorize(Roles ="Admin")]
        public async Task<IEnumerable<string>> RegisterAdmin([FromBody] RegisterAdminViewModel model)
        {
            return await _accountService.RegisterAdmin(model);
        }
    }
}
