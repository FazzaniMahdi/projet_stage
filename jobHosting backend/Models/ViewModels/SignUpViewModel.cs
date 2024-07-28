using System.ComponentModel.DataAnnotations;

namespace JobHosting.Models.ViewModels
{
    public class SignUpViewModel
    {
        public string Email { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserType { get; set; } = string.Empty;

    }
}
