using System.ComponentModel.DataAnnotations;

namespace projects.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name="Email or nickname")]
        public string EmailOrNickname { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name="Remember me")]
        public bool RememberMe { get; set; }
    }
}
