using System.ComponentModel.DataAnnotations;

namespace BizSecureDemo_22180050.ViewModels
{
    public class LoginVm
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
