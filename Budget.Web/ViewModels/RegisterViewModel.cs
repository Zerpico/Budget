using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле '{0}' не заполнено")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле '{0}' не заполнено")]
        [Display(Name = "Имя")]
        [DataType(DataType.Text)]
        [MaxLength(200, ErrorMessage = "Имя не должно превышать {0} символов")]
        public string DisplayName { get; set; }

        [Required(ErrorMessage = "Поле '{0}' не заполнено")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле '{0}' не заполнено")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
