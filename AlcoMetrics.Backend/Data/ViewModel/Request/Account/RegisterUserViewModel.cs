namespace WebApp.Data.ViewModel.Request.Account
{
    /// <summary>
    /// View-Model для регистрации пользователя
    /// </summary>
    public class RegisterUserViewModel : BaseAccountViewModel
    {
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public int UserRole { get; set; } = 2;//Дефолтное значение = винодел, когда появится возможность регистрироваться по ролям убрать
    }
}
