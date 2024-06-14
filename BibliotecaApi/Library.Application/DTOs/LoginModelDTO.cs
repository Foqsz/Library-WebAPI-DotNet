using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Library.Application.DTOs
{
    public class LoginModelDTO
    {
        [Required(ErrorMessage = "User name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
