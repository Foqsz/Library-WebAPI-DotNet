using System.ComponentModel.DataAnnotations;

namespace BibliotecaApi.Library.Application.DTOs
{
    public class RegisterModelDTO
    {
        [Required(ErrorMessage = "User name is required.")]
        public string? Name { get; set; }

        [EmailAddress(ErrorMessage = "Email is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
