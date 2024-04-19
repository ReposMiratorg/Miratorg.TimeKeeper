using System.ComponentModel.DataAnnotations;

namespace Miratorg.TimeKeeper.Host.Models;

public class LoginViewModel
{
    [Required]
    [StringLength(50, ErrorMessage = "Max length 50")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [StringLength(50, ErrorMessage = "Max length 50")]
    public string Password { get; set; }

    public bool IsRememberMe { get; set; }

    public LoginViewModel()
    {
        IsRememberMe = true;
    }
}
